using System;
using System.Runtime.InteropServices;

namespace Gsof.Native
{
    public static class NativeMethods
    {
        [DllImport("Kernel32", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr handle, string funcname);

        [DllImport("Kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string funcname);

        [DllImport("Kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = true)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
    }
}