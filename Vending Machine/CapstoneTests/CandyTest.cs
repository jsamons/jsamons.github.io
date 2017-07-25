using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class CandyTest
    {
        [TestMethod]
        public void CandyToStringTest()
        {
            DollarAmount dollar = new DollarAmount(0);
            Candy test = new Candy("candy", dollar);
            Assert.AreEqual("Enjoy that sugar rush.....before you crash.", test.ToString());
        }
    }
}
