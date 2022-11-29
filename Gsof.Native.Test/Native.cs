using System.Runtime.InteropServices;

namespace Gsof.Native.Test
{
    [TestClass]
    public class Native
    {
        [NativeFuncton("test")]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int Test(int p_sleep);

        [TestMethod]
        public void DelegateFunction()
        {
            int input = 0;
            int result = -1;
            using (var native = NativeFactory.Create(@"./libtest.dll"))
            {
                using (var native1 = NativeFactory.Create(@"./libtest.dll"))
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
            using (var native = NativeFactory.Create(@"./libtest.dll"))
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
            using (dynamic native = NativeFactory.Create(@"./libtest.dll"))
            {
                result = native.test<int>(input);
            }

            Assert.AreEqual(input, result);

        }

        [TestMethod]
        public void CreateTest()
        {
            string input = @"./libtest.dll";

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
            string input = @"./libtest.dll";

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