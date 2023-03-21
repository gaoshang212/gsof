using Gsof.Native.Extensions;
using Gsof.Native.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gsof.Native.Test
{
    [TestClass]
    public class NativeBuffer
    {
        [NativeFuncton("test_ptr")]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int TestPtr(IntPtr input, int size, IntPtr buffer, int bsize);

        private static string DllName = NativeLoader.OSPlatform == Enums.OSPlatform.Windows ? "Gsof.Test.Lib" : "libGsof.Test.Lib";

        private static string Dir = NativeLoader.IsWin ? "Windows" : "Linux";

        private static string Filename = Path.Combine(Dir, DllName);

        [TestMethod]
        public void DelegateFunction()
        {
            var data = "this is a test string.";
            using (var input = Buffer.From(data))

            using (var result = Buffer.Alloc(input.Length))

            using (var native = NativeFactory.Create(Filename))
            {
                var testptr = native.GetFunction<TestPtr>();
                var r = testptr(input, input.Length, result, result.Length);
                Assert.AreEqual(result.ReadString(Encoding.UTF8), data);
            }
        }


        [TestMethod]
        public void AutoFunction()
        {
            var data = "this is a test string.";

            using (var input = Buffer.From(data))
            using (var result = Buffer.Alloc(input.Length))
            using (var native = NativeFactory.Create(Filename))
            {
                var d = native.Invoke<int>("test_ptr", input, input.Length, result, result.Length);
                Assert.AreEqual(result.ReadString(Encoding.UTF8), data);
            }
        }

        [TestMethod]
        public void ReAlloc()
        {
            var result = Buffer.Alloc(32);
            result.ReAlloc(64);
            Assert.AreEqual(result.Length, 64);
            Marshal.WriteByte(result.Point + 63, 0x4F);

            Assert.AreEqual(result.ReadByte(63), 0x4F);
        }
    }
}
