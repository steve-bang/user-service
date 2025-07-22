
using System.Reflection;
using System.Text.Json;

namespace Steve.ManagerHero.BuildingBlocks.Utilities
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Convert an object to a Dictionary<string, string> using reflection.
        /// </summary>
        public static Dictionary<string, string> ToDictionary(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var dict = new Dictionary<string, string>();

            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object? value = property.GetValue(obj, null);
                if (value != null)
                {
                    dict[property.Name] = value.ToString()!;
                }
            }

            return dict;
        }

        /// <summary>
        /// Deep clone an object using System.Text.Json serialization.
        /// </summary>
        public static T Clone<T>(T source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var json = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<T>(json)!;
        }

        /// <summary>
        /// Copies matching properties from source to destination object.
        /// Only public instance properties with setters are copied.
        /// </summary>
        public static void CopyProperties<TSource, TTarget>(TSource source, TTarget target)
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
