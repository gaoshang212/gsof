using System;

namespace Gsof.Native.Library.OSX
{
    [Flags]
    enum DLOpenFlag : uint
    {
        /// <summary>
        /// Lazy function call binding.
        /// </summary>
        RTLD_LAZY = 0x1,
        /// <summary>
        /// Immediate function call binding.
        /// </summary>
        RTLD_NOW = 0x2,

        RTLD_LOCAL = 0x4,
        /// <summary>
        /// Use deep binding.
        /// </summary>
        RTLD_GLOBAL = 0x8,
    }
}
