using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gsof.Extensions;

namespace Gsof.Test
{
    [TestClass]
    public class Byte
    {
        [TestMethod]
        public void ToHex()
        {
            var result = (new byte[] { 0, 1, 2, 3 }).ToHex();

            Assert.AreEqual(result, "00010203");
        }
    }
}
