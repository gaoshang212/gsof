using System.Runtime.InteropServices;

namespace Gsof.Native.Test
{
    [TestClass]
    public class Native
    {
        [NativeFuncton("test")]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int Test(int p_sleep);

        private static string DllName = NativeMethods.IsWin ? "Gsof.Test.Lib.dll" : "libGsof.Test.Lib.so";

        private static string Dir = NativeMethods.IsWin ? "Windows" : "Linux";

        private static string Filename = Path.Combine(Dir, DllName);


        [TestMethod]
        public void DelegateFunction()
        {
            int input = 0;
            int result = -1;
            using (var native = NativeFactory.Create(Filename))
            {
                using (var native1 = NativeFactory.Create(Filename))
                {
                    var result1 = native1.Invoke<int, Test>(input);

                    var test = native1.GetFunction<Test>();
                    result = test(input);
                }
            }

            Assert.AreEqual(input, result);

        }

        [TestMethod]
        public void AutoFunction()
        {
            int input = 0;
            int result = -1;
            using (var native = NativeFactory.Create(Filename))
            {
                result = native.Invoke<int>("test", input);
            }

            Assert.AreEqual(input, result);

        }

        [TestMethod]
        public void DynamicFunction()
        {
            int input = 0;
            int result = -1;
            using (dynamic native = NativeFactory.Create(Filename))
            {
                result = native.test<int>(input);
            }

            Assert.AreEqual(input, result);

        }

        [TestMethod]
        public void CreateTest()
        {
            string input = Filename;

            try
            {
                using (dynamic native = NativeFactory.Create(input, CallingConvention.ThisCall))
                {

                }
            }
            catch
            {
                throw;
            }
        }

        [TestMethod]
        public void CallingConventionFunction()
        {
            string input = Filename;

            try
            {
                using (var native = NativeFactory.Create(input))
                {
                    var result = native.Invoke<int>("test", CallingConvention.StdCall, input);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}