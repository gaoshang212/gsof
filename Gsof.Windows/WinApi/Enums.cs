using System;

namespace Gsof.Windows.WinApi
{
    [Flags]
    public enum ThreadExecutionState : uint
    {
        System = 0x00000001,
        Display = 0x00000002,
        Continus = 0x80000000,
    }
}
