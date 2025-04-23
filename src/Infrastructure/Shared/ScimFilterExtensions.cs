using System.Linq.Expressions;

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

        var property = parts[0];
        var op = parts[1].ToLower();
        var value = parts[2].Trim('"');

        var param = Expression.Parameter(typeof(T), "x");
        Expression propertyExpr = property.Contains(".") 
            ? BuildNestedPropertyExpression(property, param)
            : Expression.Property(param, property);

        var constant = Expression.Constant(value);
        Expression comparison;

        switch (op)
        {
            case "eq":
                comparison = Expression.Equal(propertyExpr, constant);
                break;
            case "co":
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                comparison = Expression.Call(propertyExpr, containsMethod, constant);
                break;
            case "sw":
                var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                comparison = Expression.Call(propertyExpr, startsWithMethod, constant);
                break;
            default:
                throw new NotSupportedException($"Operator '{op}' is not supported");
        }

        return Expression.Lambda<Func<T, bool>>(comparison, param);
    }

    private static Expression BuildNestedPropertyExpression(string propertyPath, ParameterExpression param)
    {
        var properties = propertyPath.Split('.');
        Expression propertyExpr = param;
        
        foreach (var prop in properties)
        {
            propertyExpr = Expression.Property(propertyExpr, prop);
        }
        
        return propertyExpr;
    }
}