using System;
using System.Text;

namespace Gsof.Extensions
{
    /// <summary>
    /// Byte 类型扩展
    /// </summary>
    public static class ByteExtension
    {
        /// <summary>
        /// 转换为 Hex
        /// </summary>
        /// <param name="p_buffer">一个 byte 数组</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToHex(this byte[] p_buffer)
        {
            var buffer = p_buffer;
            if (buffer == null)
            {
                throw new ArgumentNullException("p_buffer");
            }

            var sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
