using System.Security.Cryptography;

namespace Gsof.Extensions
{
    /// <summary>
    /// Md5 扩展
    /// </summary>
    public static class Md5Extension
    {
        /// <summary>
        /// Byte 数组计算 md5
        /// </summary>
        /// <param name="p_buffer"></param>
        /// <returns></returns>
        public static byte[] ToMd5(this byte[] p_buffer)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(p_buffer);
            return hash;
        }
    }
}
