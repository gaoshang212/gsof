using System;
using System.Runtime.InteropServices;

namespace Gsof.Native.Library.Linux
{
    class LinuxLoader : INativeLoader
    {
        private static INativeLoader? _loader;
        internal static INativeLoader Instance
        {
            get
            {
                if (_loader is null)
                {
                    try
                    {
                        _loader = new Linux2Loader();
                        _loader.dlerror();
                    }
                    catch (DllNotFoundException)
                    {
                        _loader = new LinuxLoader();
                    }
                }

                return _loader;
            }

        }
        private LinuxLoader()
        {

        }

        public const string DLLNAME = "libdl.so";

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
    public class Linux2Loader : INativeLoader
    {
        public const string DLLNAME = "libdl.so.2";

        public IntPtr dlopen(string? fileName) => dlopen(fileName, DLOpenFlag.RTLD_NOW | DLOpenFlag.RTLD_GLOBAL);

        IntPtr INativeLoader.dlsym(IntPtr handle, string symbol) => Linux2Loader.dlsym(handle, symbol);

        int INativeLoader.dlclose(IntPtr handle) => Linux2Loader.dlclose(handle);

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