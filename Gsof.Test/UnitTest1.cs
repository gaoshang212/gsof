using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Gsof.Extensions;

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

            var list = new List<Dog>();

            var nlist = list.Distinct(i => i.Id);
        }
    }
}
