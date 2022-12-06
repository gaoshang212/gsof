using System;
using System.Runtime.InteropServices;

namespace Gsof.Native.Linux
{
    public static class NativeMethods
    {
        public const string DLLNAME = "libdl.so.2";

        [DllImport(DLLNAME, SetLastError = true)]
        public static extern IntPtr dlopen(string filename, DLOpenFlag flags);

        [DllImport(DLLNAME, SetLastError = true)]
        public static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport(DLLNAME, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool dlclose(IntPtr handle);

        [DllImport(DLLNAME, SetLastError = true)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, uint size);
    }
}