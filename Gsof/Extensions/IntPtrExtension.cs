using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gsof.Extensions
{
    /// <summary>
    /// IntPtr 扩展
    /// </summary>
    public static class IntPtrExtension
    {
        /// <summary>
        /// Byte 数组转 IntPtr , 注意：使用后要 Free 。
        /// </summary>
        /// <param name="p_bytes">源数据</param>
        /// <returns></returns>
        public static IntPtr ToIntPtr(this byte[] p_bytes)
        {
            var bytes = p_bytes;
            if (bytes == null || bytes.Length == 0)
            {
                return IntPtr.Zero;
            }

            IntPtr p = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, p, bytes.Length);

            return p;
        }


        /// <summary>
        /// Byte 数组转 IntPtr , 注意：使用后要 Free 。
        /// </summary>
        /// <param name="p_bytes">源数据</param>
        /// <param name="p_len">长度</param>
        /// <returns></returns>
        /// <exception cref="RankException"></exception>
        public static IntPtr ToIntPtr(this byte[] p_bytes, int p_len)
        {
            var bytes = p_bytes;
            if (bytes == null)
            {
                return IntPtr.Zero;
            }

            if (bytes.Length > p_len)
            {
                throw new RankException();
            }

            var cbytes = Enumerable.Repeat<byte>(0x00, p_len).ToArray();
            bytes.CopyTo(cbytes, 0);

            IntPtr p = Marshal.AllocHGlobal(cbytes.Length);
            Marshal.Copy(cbytes, 0, p, cbytes.Length);

            return p;
        }

        /// <summary>
        /// IntPtr(指针)转字符串
        /// </summary>
        /// <param name="p_intPtr">IntPtr(指针)</param>
        /// <param name="p_charSet">字符编码</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string? ToString(this IntPtr p_intPtr, CharSet p_charSet)
        {
            if (p_intPtr == IntPtr.Zero)
            {
                throw new ArgumentNullException("p_intPtr");
            }

            string? result = null;
            switch (p_charSet)
            {
                case CharSet.Auto:
                    result = Marshal.PtrToStringAuto(p_intPtr);
                    break;
                case CharSet.Ansi:
                    result = Marshal.PtrToStringAnsi(p_intPtr);
                    break;
                case CharSet.Unicode:
                    result = Marshal.PtrToStringUni(p_intPtr);
                    break;
                case CharSet.None:
                    result = Marshal.PtrToStringBSTR(p_intPtr);
                    break;
            }

            return result;
        }

        /// <summary>
        /// 字符串转 IntPtr(指针)，注意：使用后要 Free 。
        /// </summary>
        /// <param name="p_data">字符串</param>
        /// <param name="p_encoding">字符编码</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IntPtr ToIntPtr(this string p_data, Encoding p_encoding)
        {
            if (p_data == null)
            {
                throw new ArgumentNullException("p_data");
            }

            var bytes = p_encoding.GetBytes(p_data);

            return bytes.ToIntPtr(bytes.Length + 1);
        }

        /// <summary>
        /// 回收有申请的空间的 IntPtr。如 ToIntPtr
        /// </summary>
        /// <param name="p_prt"></param>
        public static void Free(this IntPtr p_prt)
        {
            if (p_prt == IntPtr.Zero)
            {
                return;
            }

            Marshal.FreeHGlobal(p_prt);
        }

        /// <summary>
        /// 回收有申请的空间的 IntPtr 数组。
        /// </summary>
        /// <param name="p_ptrs"></param>
        public static void Free(this IntPtr[] p_ptrs)
        {
            var ptrs = p_ptrs;
            if (ptrs == null)
            {
                return;
            }

            foreach (var ptr in ptrs)
            {
                ptr.Free();
            }
        }

        /// <summary>
        /// 判断 IntPtr 是否为 Zero。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static bool IsZero(this IntPtr p_intPtr)
        {
            return p_intPtr == IntPtr.Zero;
        }

        /// <summary>
        /// 从 IntPtr 获得指定长度的 Byte 数据。
        /// </summary>
        /// <param name="p_intPtr">源 IntPtr</param>
        /// <param name="p_length">长度</param>
        /// <returns></returns>
        public static byte[] GetBytes(this IntPtr p_intPtr, int p_length)
        {
            byte[] buffer = new byte[p_length];
            Marshal.Copy(p_intPtr, buffer, 0, p_length);
            return buffer;
        }

        /// <summary>
        /// 从 IntPtr 读取 Int32 。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static int ToInt(this IntPtr p_intPtr)
        {
            return Marshal.ReadInt32(p_intPtr);
        }

        /// <summary>
        /// 从 IntPtr 读取 Byte 。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static byte ToByte(this IntPtr p_intPtr)
        { 
            return Marshal.ReadByte(p_intPtr);
        }

        /// <summary>
        /// 从 IntPtr 读取 Short 。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static short ToShort(this IntPtr p_intPtr)
        {
            return Marshal.ReadInt16(p_intPtr);
        }

        /// <summary>
        /// 从 IntPtr 读取 Long 。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static long ToLong(this IntPtr p_intPtr)
        {
            return Marshal.ReadInt64(p_intPtr);
        }

        /// <summary>
        /// 从 IntPtr 读取 IntPtr 。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static IntPtr ToIntPtr(this IntPtr p_intPtr)
        {
            return Marshal.ReadIntPtr(p_intPtr);
        }

        /// <summary>
        /// 从 IntPtr 读取 float 。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static float ToFloat(this IntPtr p_intPtr)
        {
#if NET451_OR_GREATER
            return Marshal.PtrToStructure<float>(p_intPtr);
#else
            var floats = new float[1];
            Marshal.Copy(p_intPtr, floats, 0, 1);

            return floats[0];
#endif
        }

        /// <summary>
        /// 从 IntPtr 读取 double 。
        /// </summary>
        /// <param name="p_intPtr"></param>
        /// <returns></returns>
        public static double ToDouble(this IntPtr p_intPtr)
        {
#if NET451_OR_GREATER
            return Marshal.PtrToStructure<double>(p_intPtr);
#else
            var doubles = new double[1];
            Marshal.Copy(p_intPtr, doubles, 0, 1);

            return doubles[0];
#endif
        }
    }
}
