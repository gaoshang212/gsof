using System;
using System.Collections.Generic;
using System.Text;

namespace Gsof.Shared.Extensions
{
    /// <summary>
    /// 枚举类型扩展
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// 得到指定范围的集合
        /// </summary>
        /// <param name="list">源集合</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">数量</param>
        /// <returns></returns>
        public static IEnumerable<T> Take<T>(this IList<T> list, int start, int length)
        {
            for (int i = start; i < Math.Min(list.Count, start + length); i++)
            {
                yield return list[i];
            }
        }

        /// <summary>
        /// 循环集合并执行
        /// </summary>
        /// <param name="p_enumerable">集合</param>
        /// <param name="p_action">方法委托</param>
        public static void Apply<T>(this IEnumerable<T> p_enumerable, Action<T> p_action)
        {
            if (p_enumerable == null || p_action == null)
            {
                return;
            }

            foreach (T t in p_enumerable)
            {
                p_action(t);
            }
        }

        /// <summary>
        /// 集合去重
        /// </summary>
        /// <param name="p_enumerable">集合</param>
        /// <param name="p_func">委托，返回指定的重复字段</param>
        /// <returns></returns>
        public static IEnumerable<T>? Distinct<T, T1>(this IEnumerable<T> p_enumerable, Func<T, T1> p_func)
        {
            if (p_enumerable == null)
            {
                return null;
            }

            var dic = new Dictionary<T1, T>();

            foreach (var item in p_enumerable)
            {
                var key = p_func(item);
                if (key == null)
                {
                    continue;
                }
                
                dic[key] = item;
            }

            return dic.Values;
        }
    }
}
