using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Gsof.Extensions;

namespace Gsof.Test
{
    [TestClass]
    public class IntPtrEx
    {
        [TestMethod]
        public void ToInt()
        {
            var intptr = Marshal.AllocHGlobal(sizeof(int));
            Marshal.WriteInt32(intptr, 65536);

            Assert.AreEqual(intptr.ToInt(), 65536);
            Marshal.FreeHGlobal(intptr);
        }

        [TestMethod]
        public void ToByte()
        {
            var intptr = Marshal.AllocHGlobal(sizeof(byte));
            Marshal.WriteByte(intptr, 0xAF);

            Assert.AreEqual(intptr.ToByte(), 0xAF);
            Marshal.FreeHGlobal(intptr);
        }

        [TestMethod]
        public void ToShort()
        {
            var intptr = Marshal.AllocHGlobal(sizeof(short));
            Marshal.WriteInt16(intptr, short.MaxValue);

            Assert.AreEqual(intptr.ToShort(), short.MaxValue);
            Marshal.FreeHGlobal(intptr);
        }

        [TestMethod]
        public void ToLong()
        {
            var intptr = Marshal.AllocHGlobal(sizeof(long));
            Marshal.WriteInt64(intptr, long.MaxValue);

            Assert.AreEqual(intptr.ToLong(), long.MaxValue);
            Marshal.FreeHGlobal(intptr);
        }

        [TestMethod]
        public void ToFloat()
        {
            var intptr = Marshal.AllocHGlobal(sizeof(float));
            Marshal.Copy(BitConverter.GetBytes(3.1415926f), 0, intptr, sizeof(float));

            Assert.AreEqual(intptr.ToFloat(), 3.1415926f);
            Marshal.FreeHGlobal(intptr);
        }

        [TestMethod]
        public void ToDouble()
        {
            var intptr = Marshal.AllocHGlobal(sizeof(double));
            Marshal.WriteInt64(intptr, long.MaxValue);

            Marshal.Copy(BitConverter.GetBytes(Math.PI), 0, intptr, sizeof(double));

            Assert.AreEqual(intptr.ToDouble(), Math.PI);
            Marshal.FreeHGlobal(intptr);
        }
    }
}
