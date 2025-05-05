/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using System.Reflection;

namespace Steve.ManagerHero.UserService.Infrastructure.Shared;

public static class ScimFilterExtensions
{
    public static Expression<Func<T, bool>> ParseSimpleScimFilter<T>(this string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
            return x => true;

        var parts = filter.Split(' ', 3);
        if (parts.Length < 3)
            throw new ArgumentException("Invalid SCIM filter format");

        var propertyPath = parts[0];
        var op = parts[1].ToLowerInvariant();
        var value = parts[2].Trim('"');

        var param = Expression.Parameter(typeof(T), "x");
        var propertyExpr = BuildNestedPropertyExpression(propertyPath, param);

        // Convert value to match property type if needed
        Expression constant = Expression.Constant(value);

        // Auto-convert non-string to string (e.g. for Value Objects)
        if (propertyExpr.Type != typeof(string))
        {
            // First try ".Value" for value objects
            var valueProp = propertyExpr.Type.GetProperty("Value");
            if (valueProp != null && valueProp.PropertyType == typeof(string))
            {
                propertyExpr = Expression.Property(propertyExpr, valueProp);
            }
            else
            {
                // Fallback to .ToString()
                var toStringMethod = propertyExpr.Type.GetMethod("ToString", Type.EmptyTypes);
                if (toStringMethod == null)
                    throw new NotSupportedException($"Cannot convert type {propertyExpr.Type} to string.");
                propertyExpr = Expression.Call(propertyExpr, toStringMethod);
            }
        }

        // Prepare comparison
        Expression comparison;
        MethodInfo? method;

        switch (op)
        {
            case "eq":
                comparison = Expression.Equal(propertyExpr, constant);
                break;
            case "ne":
                comparison = Expression.NotEqual(propertyExpr, constant);
                break;
            case "co":
                method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                comparison = Expression.Call(propertyExpr, method!, constant);
                break;
            case "sw":
                method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                comparison = Expression.Call(propertyExpr, method!, constant);
                break;
            case "ew":
                method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                comparison = Expression.Call(propertyExpr, method!, constant);
                break;
            default:
                throw new NotSupportedException($"Operator '{op}' is not supported");
        }

        return Expression.Lambda<Func<T, bool>>(comparison, param);
    }

    private static Expression BuildNestedPropertyExpression(string propertyPath, Expression param)
    {
        var props = propertyPath.Split('.');
        Expression current = param;

        foreach (var prop in props)
        {
            var propInfo = current.Type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null)
                throw new ArgumentException($"Property '{prop}' not found on type '{current.Type.Name}'");
            current = Expression.Property(current, propInfo);
        }

        return current;
    }
}