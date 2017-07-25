using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class GumTest
    {
        [TestMethod]
        public void GumToStringTest()
        {
            DollarAmount dollar = new DollarAmount(0);
            Gum test = new Gum("gum", dollar);
            Assert.AreEqual("PSA: Peanutbutter removes gum from hair!", test.ToString());
        }
    }
}
