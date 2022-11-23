namespace Gsof.Extensions
{
    /// <summary>
    /// Boolean 类型扩展
    /// </summary>
    public static class BooleanExtension
    {
        /// <summary>
        /// Boolean To Int(0 or 1)
        /// </summary>
        /// <param name="p_bool"></param>
        public static int ToInt(this bool p_bool)
        {
            return p_bool ? 1 : 0;
        }

        /// <summary>
        /// Boolean To UInt(0 or 1)
        /// </summary>
        /// <param name="p_bool"></param>
        public static uint ToUint(this bool p_bool)
        {
            return (uint)p_bool.ToInt();
        }

        /// <summary>
        /// Nullable boolearn to Int(0 or 1)
        /// </summary>
        /// <param name="p_bool"></param>
        /// <returns></returns>
        public static int ToInt(this bool? p_bool)
        {
            return p_bool?.ToInt() ?? 0;
        }

        /// <summary>
        /// Nullable boolearn to UInt(0 or 1)
        /// </summary>
        /// <param name="p_bool"></param>
        /// <returns></returns>
        public static uint ToUint(this bool? p_bool)
        {
            return (uint)p_bool.ToInt();
        }
    }
}
