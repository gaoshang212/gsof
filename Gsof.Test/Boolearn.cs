using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Gsof.Extensions;
using System;

namespace Gsof.Test
{
    [TestClass]
    public class BoolearnTest
    {
        [TestMethod]
        public void ToInt()
        {
            var result = false.ToInt();

            Assert.AreEqual(result, 0);

            result = true.ToInt();

            Assert.AreEqual(result, 1);
        }
    }
}
