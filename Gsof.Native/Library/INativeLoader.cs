using System;

namespace Gsof.Native.Library
{
    interface INativeLoader
    {
        IntPtr dlopen(string? fileName);
        IntPtr dlsym(IntPtr handle, string symbol);
        int dlclose(IntPtr handle);
        IntPtr dlerror();
        void memcpy(IntPtr dest, IntPtr src, uint size);
    }
}
