using System;
using System.Collections.Generic;
using System.Text;
using Gsof.Windows.WinApi;

namespace Gsof.Windows
{
    public class Kernel
    {
        public static void SetThreadExecutionState(ThreadExecutionState state)
        {
            NativeMethods.SetThreadExecutionState((uint)state);
        }
    }
}
