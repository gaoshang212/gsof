using System;
using System.Runtime.InteropServices;

namespace Gsof.Native.Library.Windows
{
    class WindowsLoader : INativeLoader
    {
        const string DLLNAME = "Kernel32.dll";

        IntPtr INativeLoader.dlopen(string? fileName) => LoadLibraryEx(fileName, IntPtr.Zero, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH);

        IntPtr INativeLoader.dlsym(IntPtr handle, string symbol) => GetProcAddress(handle, symbol);

        int INativeLoader.dlclose(IntPtr handle) => FreeLibrary(handle) ? 1 : 0;

        IntPtr INativeLoader.dlerror() => IntPtr.Zero;


        void INativeLoader.memcpy(IntPtr dest, IntPtr src, uint size) => CopyMemory(dest, src, size);

        [DllImport(DLLNAME, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr handle, string funcname);

        [DllImport(DLLNAME, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string funcname);

        [DllImport(DLLNAME, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr handle);

        [DllImport(DLLNAME, SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string? lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport(DLLNAME, EntryPoint = "CopyMemory", SetLastError = true)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint size);
    }
}
