using System.Collections.Generic;

namespace Gsof.Extensions
{
    /// <summary>
    /// Dictionary 字典类型扩展
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 添加一个 IDictionary 字典。
        /// </summary>
        public static void AddRange<T1, T2, T3>(this IDictionary<T1, T2> p_dic, IDictionary<T1, T3> p_source) where T3 : T2
        {
            var dic = p_dic;
            var source = p_source;
            if (dic == null
                || source == null)
            {
                return;
            }

            foreach (var kv in source)
            {
                dic.Add(kv.Key, kv.Value);
            }
        }
    }
}
