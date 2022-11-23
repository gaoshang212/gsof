using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Gsof.Shared.Extensions
{
    public static class ReflectionExtension
    {
        public static IEnumerable<FieldInfo> GetStaticFields(this Type p_type)
        {
            var type = p_type;
            if (type == null)
            {
                yield break;
            }

            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var field in fields)
            {
                yield return field;
            }
        }

        public static IEnumerable<FieldInfo> GetStaticFields(this object p_obj, bool p_withInherit = false)
        {
            if (p_obj == null)
            {
                yield break;
            }

            var type = p_obj.GetType();
            var fields = type.GetStaticFields();

            foreach (var field in fields)
            {
                yield return field;
            }

            if (!p_withInherit)
            {
                yield break;
            }

            var tmp = type;

            while (tmp != null && tmp != typeof(object))
            {
                var tmpFields = tmp.GetStaticFields();
                foreach (var field in tmpFields)
                {
                    yield return field;
                }
                
                tmp = tmp.BaseType;
            }
        }
    }
}
