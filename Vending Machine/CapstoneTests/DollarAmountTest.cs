using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class DollarAmountTest
    {
        [TestMethod]
        public void ToStringTest()
        {
            DollarAmount test = new DollarAmount(3210);
            Assert.AreEqual("$32.10", test.ToString());
            DollarAmount test2 = new DollarAmount(1000);
            Assert.AreEqual("$10.00", test2.ToString());
            DollarAmount test3 = new DollarAmount(1);
            Assert.AreEqual("$0.01", test3.ToString());
        }

        [TestMethod]
        public void CentsTest()
        {
            DollarAmount centTest = new DollarAmount(150);
            DollarAmount centTest2 = new DollarAmount(1000);
            DollarAmount centTest3 = new DollarAmount(230);

            Assert.AreEqual(50, centTest.Cents);
            Assert.AreEqual(0, centTest2.Cents);
            Assert.AreEqual(30, centTest3.Cents);

        }

        [TestMethod]
        public void DollarsTest()
        {
            DollarAmount dollarTest = new DollarAmount(150);
            DollarAmount dollarTest2 = new DollarAmount(1000);
            DollarAmount dollarTest3 = new DollarAmount(230);

            Assert.AreEqual(1, dollarTest.Dollars);
            Assert.AreEqual(10, dollarTest2.Dollars);
            Assert.AreEqual(2, dollarTest3.Dollars);

        }

        [TestMethod]
        public void IsNegativeTest()
        {
            DollarAmount myTest = new DollarAmount(150);
            DollarAmount myTest1 = new DollarAmount(-1000);
            DollarAmount myTest2 = new DollarAmount(230);

            Assert.AreEqual(false, myTest.IsNegative);
            Assert.AreEqual(true, myTest1.IsNegative);
            Assert.AreEqual(false, myTest2.IsNegative);

        }

        [TestMethod]
        public void TotalAmountInCentsTest()
        {
            DollarAmount totalCentTest = new DollarAmount(150);
            DollarAmount totalCentTest2 = new DollarAmount(50);
            DollarAmount totalCentTest3 = new DollarAmount(1000);

            Assert.AreEqual(150, totalCentTest.TotalAmountInCents);
            Assert.AreEqual(50, totalCentTest2.TotalAmountInCents);
            Assert.AreEqual(1000, totalCentTest3.TotalAmountInCents);

        }

        [TestMethod]
        public void MinusTest()
        {
            DollarAmount minusTest = new DollarAmount(150);
            DollarAmount amountToSubtract = new DollarAmount(100);
            DollarAmount minusTest1 = new DollarAmount(1000);
            DollarAmount amountToSubtract2 = new DollarAmount(1000);
            DollarAmount minusTest2 = new DollarAmount(230);
            DollarAmount amountToSubtract3 = new DollarAmount(200);

            Assert.AreEqual(50, minusTest.Minus(amountToSubtract).TotalAmountInCents);
            Assert.AreEqual(0, minusTest1.Minus(amountToSubtract2).TotalAmountInCents);
            Assert.AreEqual(30, minusTest2.Minus(amountToSubtract3).TotalAmountInCents);
        }
    }
}
