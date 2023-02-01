using System;

namespace Gsof.Native.Library.Linux
{
    [Flags]
    enum DLOpenFlag : uint
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
        /// <summary>
        ///  The symbols defined by this shared object will be made available for symbol resolution of subsequently loaded shared objects.
        /// </summary>
        RTLD_GLOBAL = 0x000100,
    }
}
