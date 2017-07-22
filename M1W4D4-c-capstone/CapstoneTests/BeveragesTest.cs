using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class BeveragesTest
    {
        [TestMethod]
        public void BeveragesToStringTest()
        {
            DollarAmount dollar = new DollarAmount(0);
            Beverages test = new Beverages("beverage", dollar);
            Assert.AreEqual("CAFFEINECAFFEINECAFFEINE!!!", test.ToString());
        }
    }
}
