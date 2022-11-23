namespace Gsof.Shared.Extensions
{
    /// <summary>
    /// 颜色空间转换
    /// </summary>
    public static class ColorSpaceExtension
    {
        /// <summary>
        /// RGBA32 To BGRA32
        /// </summary>
        /// <param name="p_buffer"></param>
        /// <returns></returns>
        public static void ChangeToBgra32(this byte[] p_buffer)
        {
            var buffer = p_buffer;

            for (int i = 0; i < buffer.Length; i += 4)
            {
                var tmp = buffer[i];
                buffer[i] = buffer[i + 2];
                buffer[i + 2] = tmp;
            }
        }

        /// <summary>
        /// RGBA32 To BGRA32
        /// </summary>
        /// <param name="p_buffer"></param>
        /// <returns></returns>
        public static byte[] ToBgra32(this byte[] p_buffer)
        {
            var buffer = new byte[p_buffer.Length];
            p_buffer.CopyTo(buffer, buffer.Length);

            buffer.ChangeToBgra32();

            return buffer;
        }
    }
}
