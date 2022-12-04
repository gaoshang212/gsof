using System;

namespace Gsof.Native.Linux
{
    [Flags]
    public enum DLOpenFlag : uint
    {
        /// <summary>
        /// Lazy function call binding.
        /// </summary>
        RTLD_LAZY = 0x00001,
        /// <summary>
        /// Immediate function call binding.
        /// </summary>
        RTLD_NOW = 0x00002,
        /// <summary>
        /// Mask of binding time value.
        /// </summary>
        RTLD_BINDING_MASK = 0x3,
        /// <summary>
        /// Do not load the object.
        /// </summary>
        RTLD_NOLOAD = 0x00004,
        /// <summary>
        /// Use deep binding.
        /// </summary>
        RTLD_DEEPBIND = 0x00008,
    }
}
