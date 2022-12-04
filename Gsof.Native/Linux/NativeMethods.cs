using System;
using System.Runtime.InteropServices;

namespace Gsof.Native.Linux
{
    public static class NativeMethods
    {
        [DllImport("libdl.so", SetLastError = true)]
        public static extern IntPtr dlopen(string filename, DLOpenFlag flags);

        [DllImport("libdl.so", SetLastError = true)]
        public static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libdl.so", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool dlclose(IntPtr handle);

        [DllImport("libdl.so", SetLastError = true)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, uint size);
    }
}