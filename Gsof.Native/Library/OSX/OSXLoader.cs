using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gsof.Native.Library.OSX
{
    class OSXLoader : INativeLoader
    {
        const string DLLNAME = "/usr/lib/libSystem.dylib";

        IntPtr INativeLoader.dlopen(string? fileName) => dlopen(fileName, DLOpenFlag.RTLD_NOW | DLOpenFlag.RTLD_GLOBAL);

        IntPtr INativeLoader.dlsym(IntPtr handle, string symbol) => dlsym(handle, symbol);

        int INativeLoader.dlclose(IntPtr handle) => dlclose(handle);

        IntPtr INativeLoader.dlerror() => dlerror();

        void INativeLoader.memcpy(IntPtr dest, IntPtr src, uint size) => memcpy(dest, src, size);


        [DllImport(DLLNAME, SetLastError = true)]
        private static extern IntPtr dlopen(string? filename, DLOpenFlag flags);

        [DllImport(DLLNAME, SetLastError = true)]
        private static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport(DLLNAME, SetLastError = true)]
        private static extern int dlclose(IntPtr handle);

        [DllImport(DLLNAME, SetLastError = true)]
        private static extern IntPtr dlerror();

        [DllImport(DLLNAME, SetLastError = true)]
        private static extern void memcpy(IntPtr dest, IntPtr src, uint size);
    }
}
