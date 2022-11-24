using System;
using System.Runtime.InteropServices;

namespace Gsof.Xaml.BlankWindow.Native
{
    [Obsolete("Use Standard.APPBARDATA instead.")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct APPBARDATA
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uCallbackMessage;
        public int uEdge;
        public RECT rc;
        public bool lParam;
    }
}
