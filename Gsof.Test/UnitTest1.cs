using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Gsof.Extensions;
using System;

namespace Gsof.Test
{
    [TestClass]
    public class UnitTest1
    {
        public class Dog
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            // ÏÖÔÚ 18:30 
            var n = 18;   // 18:00 ~ 17:59

            // 18 19 20 21 22 23 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17   
            // 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23   =>  x
            // n = 18 

            var arr = Enumerable.Range(0, 24).ToArray();

            var result = Enumerable.Range(18, 24).Select(i => arr[i % 24]);
        }
    }
}
