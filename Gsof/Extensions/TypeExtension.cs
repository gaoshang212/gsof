using System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsof.Extensions
{
    public static class TypeExtension
    {
        public static Type?[]? GetTypes(this IEnumerable<object?> p_objects)
        {
            return p_objects?.Select(i => i?.GetType()).ToArray();
        }

        public static object? CreateInstance(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }
    }
}
