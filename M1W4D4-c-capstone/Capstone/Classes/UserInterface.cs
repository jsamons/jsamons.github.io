using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vm = new VendingMachine();

        public UserInterface(VendingMachine vm)
        {
            this.vm = vm;
        }

        public void RunVendingMachine()
        {
            Console.WriteLine("THANK YOU FOR USING THE VENDO-MATIC 500");
            Console.WriteLine();

            int selectedChoice = 0;
            bool validAnswer = false;
            bool returnToMainMenu = true;

            while (returnToMainMenu)
            {
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("Please select from the following:");
                Console.WriteLine("1. Display Vending Machine Items \n2. Purchase \n3. Turn Off Vending Machine");
                Console.WriteLine();

                while (!validAnswer)
                {
                    try
                    {
                        selectedChoice = int.Parse(Console.ReadLine());

                        if (selectedChoice == 1 || selectedChoice == 2 || selectedChoice == 3)
                        {
                            validAnswer = true; ;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }

                    catch (Exception)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please make a valid selection: 1, 2 or 3.");
                        Console.WriteLine("1. Display Vending Machine Items \n2. Purchase \n3. Turn Off Vending Machine");
                        Console.WriteLine();
                    }
                }

                if (selectedChoice == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine(vm.DisplayItems());
                    Console.WriteLine("Press any button to return to the Main Menu.");
                    Console.ReadLine();
                    Console.WriteLine();
                    validAnswer = false;
                }

                else if (selectedChoice == 2)
                {
                    returnToMainMenu = false;
                    bool returnToPurchaseMenu = true;

                    while (returnToPurchaseMenu)
                    {
                        Console.WriteLine();
                        Console.WriteLine("PURCHASE MENU");
                        Console.WriteLine("Please select from the following:");
                        Console.WriteLine("1. Feed Money \n2. Select Product \n3. Complete Transaction");
                        Console.WriteLine("Your current available balance is " + vm.Balance + ".");
                        Console.WriteLine();
                        validAnswer = false;

                        while (!validAnswer)
                        {
                            try
                            {
                                selectedChoice = int.Parse(Console.ReadLine());

                                if (selectedChoice == 1 || selectedChoice == 2 || selectedChoice == 3)
                                {
                                    validAnswer = true;
                                    returnToPurchaseMenu = false;
                                }
                                else
                                {
                                    throw new Exception();                                    
                                }
                            }

                            catch (Exception)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Please make a valid selection: 1, 2 or 3.");
                                Console.WriteLine("1. Feed Money \n2. Select Product \n3. Complete Transaction");
                                Console.WriteLine();
                            }
                        }

                        if (selectedChoice == 1)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please insert a $1, $2, $5, or $10 bill.");
                            Console.WriteLine("To add $1: select 1. \nTo add $2: select 2. \nTo add $5: select 5. \nTo add $10: select 10.");
                            Console.WriteLine();

                            int paymentAmount = 0;
                            validAnswer = false;

                            while (!validAnswer)
                            {
                                try
                                {
                                    paymentAmount = int.Parse(Console.ReadLine());

                                    if (paymentAmount == 1 || paymentAmount == 2 || paymentAmount == 5 || paymentAmount == 10)
                                    {
                                        validAnswer = true;
                                        returnToPurchaseMenu = false;
                                    }
                                    else
                                    {
                                        throw new Exception();                                       
                                    }
                                }

                                catch (Exception)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Please make a valid selection: 1, 2, 5, or 10.");
                                    Console.WriteLine();
                                }
                            }

                            vm.AddMoneyToBalance(paymentAmount);
                            vm.FeedMoneyToVMLog(paymentAmount);
                            returnToPurchaseMenu = true;
                        }

                        else if (selectedChoice == 2)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter product code.");
                            Console.WriteLine();
                            string slotNumber = " ";

                            validAnswer = false;

                            while (!validAnswer)
                            {
                                try
                                {
                                    slotNumber = Console.ReadLine().ToUpper();
                                    
                                    if (vm.VMContents.ContainsKey(slotNumber))
                                    {
                                        validAnswer = true;
                                        returnToPurchaseMenu = false;                                   
                                    }

                                    else
                                    {
                                        throw new Exception();
                                    }
                                }

                                catch (Exception)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("You have entered a non-existant product code. Returning to Purchase Menu.");
                                    Console.WriteLine("To view item list, complete transaction and return to Main Menu.");
                                    validAnswer = true;
                                    returnToPurchaseMenu = true;
                                }
                            }

                            while (!returnToPurchaseMenu)
                            {
                                if (vm.VMContents[slotNumber].Quantity > 0)
                                {
                                    vm.DispenseItem(slotNumber);

                                    if (vm.Balance.IsNegative)
                                    {
                                        vm.Balance = vm.Balance.Plus(vm.VMContents[slotNumber].Price);
                                        vm.VMContents[slotNumber].Quantity += 1;
                                        Console.WriteLine();
                                        Console.WriteLine("Your current balance is too low to make this purchase. Please add more money or select a different product.");
                                        returnToPurchaseMenu = true;
                                    }

                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine(vm.VMContents[slotNumber].ToString());
                                        returnToPurchaseMenu = true;
                                        vm.ItemDispensedToLog(slotNumber);
                                    }
                                }

                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"{vm.DispenseItem(slotNumber)}");
                                    Console.WriteLine("Returning to Purchase Menu.");
                                    returnToPurchaseMenu = true;
                                }
                            }
                        }

                        else if (selectedChoice == 3)
                        {
                            Console.WriteLine();
                            vm.ChangeDispensedToLog();
                            Console.WriteLine(vm.DispenseChange());
                            Console.WriteLine();
                            Console.WriteLine("Returning to main menu.");
                            Console.WriteLine();
                            returnToMainMenu = true;
                            validAnswer = false;
                        }
                    }
                }

                else if (selectedChoice == 3)
                {
                    vm.SalesReport();
                    Environment.Exit(0);
                }
            }
        }
    }
}