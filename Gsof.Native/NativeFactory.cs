using System;
using System.Runtime.InteropServices;

namespace Gsof.Native
{
    public class NativeFactory
    {   
        /// <summary>
        /// 创建INative 对象
        /// </summary>
        /// <param name="p_fileName">文件路径</param>
        /// <returns></returns>
        public static INative Create(string p_fileName)
        {
            lock (typeof(NativeFactory))
            {
                var native = new Native(p_fileName);

                return native;
            }
        }

        public static INative Create(string p_fileName, IDisposable p_disposable)
        {
            lock (typeof(NativeFactory))
            {
                var native = new Native(p_fileName, p_disposable);

                return native;
            }
        }

        /// <summary>
        /// 创建INative 对象
        /// </summary>
        /// <param name="p_fileName">文件路径</param>
        /// <param name="p_calling">调用转换方式（同PInvoke CallingConvention）</param>
        /// <returns></returns>
        public static INative Create(string p_fileName, CallingConvention p_calling)
        {
            lock (typeof(NativeFactory))
            {
                var native = new Native(p_fileName, p_calling);
                return native;
            }
        }

        /// <summary>
        /// 销毁INative， 也可以调用 Native的Dispose方法
        /// </summary>
        /// <param name="p_native"></param>
        public static void Free(INative p_native)
        {
            var native = p_native;
            if (native == null)
            {
                return;
            }

            native.Dispose();
        }
    }
}