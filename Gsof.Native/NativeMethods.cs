using System;
using System.Runtime.InteropServices;

namespace Gsof.Native
{
    public static class NativeMethods
    {
        public static bool IsWin
        {
            get
            {
#if NET5_0_OR_GREATER
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#else
                return true;
#endif
            }
        }

        public static IntPtr GetProcAddress(IntPtr handle, string funcname)
        {
            IntPtr func;
            if (NativeMethods.IsWin)
            {
                func = Windows.NativeMethods.GetProcAddress(handle, funcname);
            }
            else
            {
                func = Linux.NativeMethods.dlsym(handle, funcname);
            }

            return func;
        }

        public static IntPtr LoadLibrary(string fileanme)
        {
            IntPtr handle;
            if (NativeMethods.IsWin)
            {
                handle = Windows.NativeMethods.LoadLibraryEx(fileanme, IntPtr.Zero, Windows.LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH);
            }
            else
            {
                handle = Linux.NativeMethods.dlopen(fileanme, Linux.DLOpenFlag.RTLD_NOW);
            }

            return handle;
        }

        public static bool FreeLibrary(IntPtr handle)
        {
            bool result;
            if (NativeMethods.IsWin)
            {
                result = Windows.NativeMethods.FreeLibrary(handle);
            }
            else
            {
                result = Linux.NativeMethods.dlclose(handle);
            }

            return result;
        }

        public static void CopyMemory(IntPtr dest, IntPtr src, uint size)
        {
            if (NativeMethods.IsWin)
            {
                Windows.NativeMethods.CopyMemory(dest, src, size);
            }
            else
            {
                Linux.NativeMethods.memcpy(dest, src, size);
            }
        }

        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        //[DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = true)]
        //public static extern void CopyMemory(IntPtr dest, IntPtr src, uint size);


        //[DllImport("libdl.so", SetLastError = true)]
        //public static extern IntPtr dlopen(string filename, int flags);

        //[DllImport("libdl.so", SetLastError = true)]
        //public static extern IntPtr dlsym(IntPtr handle, string symbol);

        //[DllImport("libdl.so", SetLastError = true)]
        //public static extern IntPtr dlclose(IntPtr handle);

        //[DllImport("libdl.so", SetLastError = true)]
        //public static extern IntPtr memcpy(IntPtr dest, IntPtr src, uint size);


        //const int RTLD_NOW = 2; // for dlopen's flags 
    }
}