using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class ChipsTest
    {
        [TestMethod]
        public void ChipsToStringTest()
        {
            DollarAmount dollar = new DollarAmount(0);
            Chips test = new Chips("candy", dollar);
            Assert.AreEqual("Salty goodness.", test.ToString());
        }
    }
}
