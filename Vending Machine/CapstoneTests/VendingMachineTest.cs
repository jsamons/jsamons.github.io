using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void DispenseItem_BalanceTest()
        {
            VendingMachine vm = new VendingMachine();
            string slotNumber = "A1";
            vm.Balance = new DollarAmount(500);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(195, vm.Balance.TotalAmountInCents);

            vm = new VendingMachine();
            slotNumber = "B1";
            vm.Balance = new DollarAmount(500);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(320, vm.Balance.TotalAmountInCents);

            vm = new VendingMachine();
            slotNumber = "C1";
            vm.Balance = new DollarAmount(500);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(375, vm.Balance.TotalAmountInCents);

            vm = new VendingMachine();
            slotNumber = "D1";
            vm.Balance = new DollarAmount(500);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(415, vm.Balance.TotalAmountInCents);
        }

        [TestMethod]
        public void DispenseItem_QuantityTest()
        {
            VendingMachine vm = new VendingMachine();
            string slotNumber = "A1";
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(4, vm.VMContents[slotNumber].Quantity);

            vm = new VendingMachine();
            slotNumber = "B1";
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(4, vm.VMContents[slotNumber].Quantity);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(3, vm.VMContents[slotNumber].Quantity);

            vm = new VendingMachine();
            slotNumber = "C1";
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(4, vm.VMContents[slotNumber].Quantity);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(3, vm.VMContents[slotNumber].Quantity);

            vm = new VendingMachine();
            slotNumber = "D1";
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(4, vm.VMContents[slotNumber].Quantity);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(3, vm.VMContents[slotNumber].Quantity);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(2, vm.VMContents[slotNumber].Quantity);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(1, vm.VMContents[slotNumber].Quantity);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(0, vm.VMContents[slotNumber].Quantity);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual(0, vm.VMContents[slotNumber].Quantity);
        }

        [TestMethod]
        public void DispenseItem_StringTest()
        {
            VendingMachine vm = new VendingMachine();
            string slotNumber = "A1";
            Assert.AreEqual("Salty goodness.", vm.DispenseItem(slotNumber));

            vm = new VendingMachine();
            slotNumber = "B1";
            Assert.AreEqual("Enjoy that sugar rush.....before you crash.", vm.DispenseItem(slotNumber));

            vm = new VendingMachine();
            slotNumber = "C1";
            Assert.AreEqual("CAFFEINECAFFEINECAFFEINE!!!", vm.DispenseItem(slotNumber));

            vm = new VendingMachine();
            slotNumber = "D1";
            Assert.AreEqual("PSA: Peanutbutter removes gum from hair!", vm.DispenseItem(slotNumber));

            vm = new VendingMachine();
            slotNumber = "C8";
            vm.DispenseItem(slotNumber);
            Assert.AreEqual("Invalid product code.", vm.DispenseItem(slotNumber));

            vm = new VendingMachine();
            slotNumber = "D1";
            vm.DispenseItem(slotNumber);
            vm.DispenseItem(slotNumber);
            vm.DispenseItem(slotNumber);
            vm.DispenseItem(slotNumber);
            vm.DispenseItem(slotNumber);
            Assert.AreEqual("SOLD OUT", vm.DispenseItem(slotNumber));
        }

        [TestMethod]
        public void DispenseChangeTest()
        {
            VendingMachine vm = new VendingMachine();
            vm.Balance = new DollarAmount(130);
            Assert.AreEqual("Your change is 5 quarter(s), 0 dime(s), 1 nickel(s).", vm.DispenseChange());
            Assert.AreEqual(0, vm.Balance.TotalAmountInCents);

            vm.Balance = new DollarAmount(510);
            Assert.AreEqual("Your change is 20 quarter(s), 1 dime(s), 0 nickel(s).", vm.DispenseChange());
            Assert.AreEqual(0, vm.Balance.TotalAmountInCents);

            vm.Balance = new DollarAmount(90);
            Assert.AreEqual("Your change is 3 quarter(s), 1 dime(s), 1 nickel(s).", vm.DispenseChange());
            Assert.AreEqual(0, vm.Balance.TotalAmountInCents);
        }

        [TestMethod]
        public void AddMoneyTest_Balance()
        {
            VendingMachine vm = new VendingMachine();
            vm.AddMoneyToBalance(5);
            Assert.AreEqual(500, vm.Balance.TotalAmountInCents);

            vm = new VendingMachine();
            vm.AddMoneyToBalance(10);
            Assert.AreEqual(1000, vm.Balance.TotalAmountInCents);

            vm = new VendingMachine();
            vm.AddMoneyToBalance(2);
            Assert.AreEqual(200, vm.Balance.TotalAmountInCents);
        }
    }
}
