using System;
using System.Runtime.InteropServices;
using Gsof.Native.Library.Linux;
using Gsof.Native.Library.OSX;
using Gsof.Native.Library.Windows;

namespace Gsof.Native.Library
{
    public class NativeLoader
    {
        public static bool IsWin
        {
            get
            {
#if NET5_0_OR_GREATER
                return RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
#else
                return true;
#endif
            }
        }

        public static Enums.OSPlatform OSPlatform
        {
            get
            {
                var os = Enums.OSPlatform.Unknow;
#if NET5_0_OR_GREATER

                if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                {
                    os = Enums.OSPlatform.Windows;
                }
                else if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
                {
                    os = Enums.OSPlatform.Linux;
                }
                else if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.FreeBSD))
                {
                    os = Enums.OSPlatform.FreeBSD;
                }
                else if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
                {
                    os = Enums.OSPlatform.OSX;
                }
#else
                os = Enums.OSPlatform.Windows;
#endif
                return os;
            }
        }

        public static Enums.Architecture Arch
        {
            get
            {
#if NET5_0_OR_GREATER
                return (Enums.Architecture)RuntimeInformation.ProcessArchitecture;
#else
                return Environment.Is64BitProcess ? Enums.Architecture.X64 : Enums.Architecture.X86;
#endif
            }
        }

        private static INativeLoader? _loader;
        internal static INativeLoader Instance
        {
            get
            {
                if (_loader is null)
                {
                    switch (OSPlatform)
                    {
                        case Enums.OSPlatform.Windows:
                            _loader = new WindowsLoader();
                            break;
                        case Enums.OSPlatform.OSX:
                            _loader = new OSXLoader();
                            break;
                        case Enums.OSPlatform.Linux:
                            _loader = LinuxLoader.Instance!;
                            break;
                        default:
                            throw new PlatformNotSupportedException("This operating system is not supported");
                    }
                }

                return _loader;
            }

        }
    }
}
