using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gsof.Native.Enums
{
    //
    // 摘要:
    //     Indicates the processor architecture.
    public enum Architecture
    {
        //
        // 摘要:
        //     An Intel-based 32-bit processor architecture.
        X86 = 0,
        //
        // 摘要:
        //     An Intel-based 64-bit processor architecture.
        X64 = 1,
        //
        // 摘要:
        //     A 32-bit ARM processor architecture.
        Arm = 2,
        //
        // 摘要:
        //     A 64-bit ARM processor architecture.
        Arm64 = 3,
        //
        // 摘要:
        //     The WebAssembly platform.
        Wasm = 4
    }
}
