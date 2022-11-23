using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gsof.Extensions
{
    public static class ObjectExtension
    {
        public static IDictionary<string, object> GetProperties(this object p_obj)
        {
            var dic = new Dictionary<string, object>();

            var obj = p_obj;
            do
            {
                if (obj == null)
                {
                    break;
                }

                var type = p_obj.GetType();

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(obj, null);

                    var name = property.Name;
                    dic.Add(name, value);
                }

            } while (false);

            return dic;
        }

        /// <summary>
        /// Convert object to querystring
        /// </summary>
        /// <param name="p_obj"></param>
        /// <returns></returns>
        public static string ToQueryString(this object p_obj)
        {
            if (p_obj == null)
            {
                return string.Empty;
            }

            var properties = p_obj.GetProperties();
            if (properties == null || properties.Count <= 0)
            {
                return string.Empty;
            }


            var qslist = properties.Select(i => $"{i.Key}={Uri.EscapeDataString(i.Value?.ToString() ?? "")}");


            return string.Join("&", qslist);
        }

        public static Task<T?> InvokeAsync<T>(this object p_obj, string? name, params object[] paratmers)
        {
            if (string.IsNullOrEmpty(name))
            {
                return TaskExtensions.FromResult(default(T));
            }

            var type = p_obj.GetType();

            var methods = type.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            var method = methods.FirstOrDefault(method => name.Equals(method.Name, StringComparison.OrdinalIgnoreCase));

            if (method == null)
            {
                return TaskExtensions.FromResult(default(T));
            }

            var ps = method.GetParameters();

            object[]? inputs = null;
            if (paratmers.Length != 0 && paratmers.Length >= ps.Length)
            {
                inputs = paratmers.Take(ps.Length).ToArray();
            }

            return TaskExtensions.FromResult((T?)method.Invoke(p_obj, inputs));
        }
    }
}
