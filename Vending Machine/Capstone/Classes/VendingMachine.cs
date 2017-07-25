using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{ 
    public class VendingMachine
    {
        private DollarAmount balance = new DollarAmount(0);
        private Dictionary<string, VMItem> vmContents = new Dictionary<string, VMItem>();

        public VendingMachine()
        {
            string filePath = "vendingmachine.csv";
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string itemInfo = sr.ReadLine();

                        if (itemInfo[0] == 'A')
                        {
                            string[] chipsInfo = itemInfo.Split('|');
                            double doublePrice = double.Parse(chipsInfo[2]);
                            int intPrice = (int)(doublePrice * 100);
                            DollarAmount price = new DollarAmount(intPrice);
                            vmContents.Add(chipsInfo[0], new Chips(chipsInfo[1], price));
                            //string[0]== slot which is key of dictionary
                            //string[1]== item to be passed into object
                            //string[2]== price to be passed into object
                        }

                        else if (itemInfo[0] == 'B')
                        {
                            string[] candyInfo = itemInfo.Split('|');
                            double doublePrice = double.Parse(candyInfo[2]);
                            int intPrice = (int)(doublePrice * 100);
                            DollarAmount price = new DollarAmount(intPrice);
                            vmContents.Add(candyInfo[0], new Candy(candyInfo[1], price));
                            //string[0]== slot which is key of dictionary
                            //string[1]== item to be passed into object
                            //string[2]== price to be passed into object
                        }

                        if (itemInfo[0] == 'C')
                        {
                            string[] beveragesInfo = itemInfo.Split('|');
                            double doublePrice = double.Parse(beveragesInfo[2]);
                            int intPrice = (int)(doublePrice * 100);
                            DollarAmount price = new DollarAmount(intPrice);
                            vmContents.Add(beveragesInfo[0], new Beverages(beveragesInfo[1], price));
                            //string[0]== slot which is key of dictionary
                            //string[1]== item to be passed into object
                            //string[2]== price to be passed into object
                        }

                        if (itemInfo[0] == 'D')
                        {
                            string[] gumInfo = itemInfo.Split('|');
                            double doublePrice = double.Parse(gumInfo[2]);
                            int intPrice = (int)(doublePrice * 100);
                            DollarAmount price = new DollarAmount(intPrice);
                            vmContents.Add(gumInfo[0], new Gum(gumInfo[1], price));
                            //string[0]== slot which is key of dictionary
                            //string[1]== item to be passed into object
                            //string[2]== price to be passed into object
                        }
                    }
                }
            }

            catch (Exception)
            {
                Console.WriteLine("File Not Found. Please re-enter file path.");
                filePath = Console.ReadLine();
            }
        }

        public DollarAmount Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }

        public Dictionary<string, VMItem> VMContents
        {
            get
            {
                return vmContents;
            }
        }

        public string DispenseItem(string slotNumber)
        {
            if (VMContents.ContainsKey(slotNumber))
            {
                if (VMContents[slotNumber].Quantity == 0)
                {
                    return "SOLD OUT";
                }

                else
                {
                    VMContents[slotNumber].Quantity -= 1;
                    Balance = Balance.Minus(VMContents[slotNumber].Price);
                    return VMContents[slotNumber].ToString();
                }
            }

            else
            {
                return "Invalid product code.";
            }
        }

        public string DispenseChange()
        {
            int numOfQuarters = Balance.TotalAmountInCents / 25;
            Balance = Balance.Minus(new DollarAmount(numOfQuarters * 25));

            int numOfDimes = Balance.TotalAmountInCents / 10;
            Balance = Balance.Minus(new DollarAmount(numOfDimes * 10));

            int numOfNickels = Balance.TotalAmountInCents / 05;
            Balance = Balance.Minus(new DollarAmount(numOfNickels * 5));

            return $"Your change is {numOfQuarters} quarter(s), {numOfDimes} dime(s), {numOfNickels} nickel(s).";
        }

        public string DisplayItems()
        {

            string slot = "Slot";
            string item = "Item";
            string price = "Price";
            string quantity = "Quantity";
            string itemList = $"{slot.PadRight(5,' ')} {item.PadRight(20,' ')} {price.PadRight(10, ' ')} {quantity.PadRight(10,' ')}\n";

            foreach (KeyValuePair<string, VMItem> kvp in VMContents)

            {
                if (kvp.Value.Quantity == 0)
                {
                    string soldOut = "SOLD OUT";
                    itemList += $"{kvp.Key.PadRight(5, ' ')} {kvp.Value.Item.PadRight(20, ' ')} {kvp.Value.Price.ToString().PadRight(10,' ')} {soldOut.PadRight(10,' ')}\n";
                }

                else
                {
                    itemList += $"{kvp.Key.PadRight(5, ' ')} {kvp.Value.Item.PadRight(20, ' ')} {kvp.Value.Price.ToString().PadRight(10, ' ')} {kvp.Value.Quantity.ToString().PadRight(10,' ')}\n";
                }
            }

            return itemList;
        }

        public void AddMoneyToBalance(int paymentAmount)
        {
            Balance = Balance.Plus(new DollarAmount(paymentAmount * 100));
        }

        public void FeedMoneyToVMLog(int paymentAmount)
        {
            string logDirectory = Environment.CurrentDirectory;
            string fileName = "Log.txt";
            string filePath = logDirectory + fileName;

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    string feedMoney = "FEED MONEY:";
                    sw.Write($"{DateTime.Now}   ");
                    sw.Write(feedMoney.PadRight(20, ' '));
                    sw.Write($"{new DollarAmount(paymentAmount * 100).ToString().PadRight(10,' ')}");
                    sw.Write(Balance);
                    sw.WriteLine();
                }
            }

            catch (Exception)
            {

            }
        }

        public void ItemDispensedToLog(string slotNumber)
        {
            string logDirectory = Environment.CurrentDirectory;
            string fileName = "Log.txt";
            string filePath = logDirectory + fileName;

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    string itemDispensed = VMContents[slotNumber].Item + " " + slotNumber;
                    string startingTransactionBalance = Balance.Plus(VMContents[slotNumber].Price).ToString();
                    sw.Write($"{DateTime.Now}   ");
                    sw.Write(itemDispensed.PadRight(20,' '));
                    sw.Write(startingTransactionBalance.PadRight(10, ' '));
                    sw.Write(Balance);
                    sw.WriteLine();
                }
            }

            catch (Exception)
            {

            }
        }

        public void ChangeDispensedToLog()
        {
            string logDirectory = Environment.CurrentDirectory;
            string fileName = "Log.txt";
            string filePath = logDirectory + fileName;

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    string giveChange = "GIVE CHANGE:";
                    sw.Write($"{DateTime.Now}   ");
                    sw.Write(giveChange.PadRight(20, ' '));
                    sw.Write(Balance.ToString().PadRight(10, ' '));
                    sw.Write(Balance.Minus(Balance));
                    sw.WriteLine();
                }
            }

            catch (Exception)
            {

            }
        }

        public void SalesReport()
        {
            string logDirectory = Environment.CurrentDirectory;
            string fileName = "SalesReport.txt";
            string filePath = logDirectory + fileName;

            try
            {
                DollarAmount totalSales = new DollarAmount(0);
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"SALES REPORT: {DateTime.Now}");
                    sw.WriteLine();

                    foreach (KeyValuePair<string, VMItem> kvp in VMContents)
                    {
                        int itemsSold = 5 - kvp.Value.Quantity;
                        DollarAmount itemSalesAmount = new DollarAmount(itemsSold * kvp.Value.Price.TotalAmountInCents);
                        totalSales = totalSales.Plus(itemSalesAmount);
                        sw.WriteLine($"{kvp.Value.Item}|{itemsSold}");

                    }

                    sw.WriteLine();
                    sw.WriteLine($"**TOTAL SALES** {totalSales}");
                    sw.WriteLine();
                }
            }

            catch (Exception)
            {

            }
        }
    }
}
