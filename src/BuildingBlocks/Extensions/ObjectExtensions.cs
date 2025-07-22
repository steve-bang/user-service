
using System.Reflection;
using System.Text.Json;

namespace Steve.ManagerHero.BuildingBlocks.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts any object into a dictionary of string to object.
        /// </summary>
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var result = new Dictionary<string, object>();

            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead)
                {
                    object? value = property.GetValue(obj);
                    result[property.Name] = value ?? DBNull.Value;
                }
            }

            return result;
        }

        /// <summary>
        /// Clone an object using System.Text.Json serialization (deep copy).
        /// </summary>
        public static T Clone<T>(this T source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var json = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<T>(json)!;
        }

        /// <summary>
        /// Copy matching properties from source object to target object.
        /// </summary>
        public static void CopyPropertiesTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            if (source == null || target == null)
                throw new ArgumentNullException("Source or/and Target is null");

            var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = Array.Find(targetProps, p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType);
                if (targetProp != null && targetProp.CanWrite)
                {
                    var value = sourceProp.GetValue(source, null);
                    targetProp.SetValue(target, value, null);
                }
            }
        }
    }
}
