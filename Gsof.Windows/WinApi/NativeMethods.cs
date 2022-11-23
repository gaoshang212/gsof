using System.Runtime.InteropServices;

namespace Gsof.WinApi
{
    internal class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern uint SetThreadExecutionState(uint esFlags);

        [DllImport("ntdll.dll")]
        internal static extern void RtlGetNtVersionNumbers(ref uint major, ref uint minor, ref uint build);
    }
}
