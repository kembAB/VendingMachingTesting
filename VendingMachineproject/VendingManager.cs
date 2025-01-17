﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineproject
{
    class VendingManager
    {
       
            private bool IsRunning = false;
            private VendingMachine VendingMachineInstance;

            public VendingManager()
            {
                VendingMachineInstance = new VendingMachine();
            }

            public void Run()
            {
                IsRunning = true;

                while (IsRunning)
                {
                    IsRunning = RunMenu();
                }
            }

            private bool RunMenu()
            {
                bool continueToRun = true;

                PrintMenu();

                char menuChoice;

                Char.TryParse(Console.ReadLine(), out menuChoice);

                switch (menuChoice)
                {
                    case 'i':
                        Console.WriteLine("Insert Money");
                        StartInsertingMoney();
                        break;

                    case 'e':
                        Console.WriteLine("End transaction");
                        StartEndingTransaction();
                        break;

                    case 'b':
                        Console.WriteLine("Buy product");
                        StartBuyProduct();
                        break;

                    case 'q':
                        Console.WriteLine("Quit");
                        continueToRun = false;
                        break;

                    default:
                        Console.WriteLine("\ninvalid menu choice!");
                        break;
                }
                return continueToRun;
            }

            private void PrintMenu()
            {
                Console.Clear();

                
               
                Console.WriteLine("vending machine welcome!!");
               
               
                Console.WriteLine();

              

                Console.WriteLine();

                PrintChoices();

                Console.WriteLine("\nCurrent balance is : " + this.VendingMachineInstance.Money);

                Console.WriteLine();

                Console.Write("Select an option : ");
            }

            private void PrintChoices()
            {
            
                
               
                Console.WriteLine("i: insert money ");
                Console.WriteLine("e: end transaction");
                Console.WriteLine("b: buy an item ");
             
                Console.WriteLine("q: quit ");
               
            }

            private void PrintProducts()
            {
                foreach (KeyValuePair<string, Product> pair in this.VendingMachineInstance.ShowAll())
                {
                    Console.WriteLine(pair.Key + ": " + pair.Value.Name);
                }
            }

            private void PrintProductsWithInfo()
            {
                Console.WriteLine("Products availible: \n");

                foreach (KeyValuePair<string, Product> pair in this.VendingMachineInstance.ShowAll())
                {
                    Console.WriteLine(pair.Key + ": " + pair.Value.Name);
                    Console.WriteLine(pair.Value.Examine() + "\n");
                }
            }

            private void StartEndingTransaction()
            {

                int[] money = this.VendingMachineInstance.EndTransaction();

                Console.Clear();
                Console.WriteLine("Your change is :");

                for (int i = 0; i < this.VendingMachineInstance.MoneyDenominations.Length; i++)
                {
                    Console.WriteLine(this.VendingMachineInstance.MoneyDenominations[i] + ": " + money[i]);
                }

                Console.Write("Continue..");
                Console.ReadKey();
            }

            private void StartInsertingMoney()
            {
                bool validDenomination = false;

                while (validDenomination == false)
                {
                    Console.Clear();

                    Console.WriteLine("Insert money, you can insert these denominations:");

                    foreach (int denomination in this.VendingMachineInstance.MoneyDenominations)
                    {
                        Console.Write(denomination + " ");
                    }

                    Console.WriteLine("\nq: quit to main menu\n");
                    Console.Write("Insert money or quit: ");

                    string input = Console.ReadLine();

                    if (input == "q")
                        break;

                    int money = 0;

                    bool validInt = int.TryParse(input, out money);
                    if (validInt)
                    {
                        validDenomination = this.VendingMachineInstance.InsertMoney(money);
                        Console.WriteLine("You successfully inserted " + money + "!");
                        Console.Write("Continue..");
                        Console.ReadKey();
                    }
                    else
                    {
                       
                        Console.WriteLine("Your deposit was not valid!");
                        
                        Console.Write("Continue..");
                        Console.ReadKey();
                    }
                }
            }

            private void StartBuyProduct()
            {
                bool QuitPurchasing = false;

                while (QuitPurchasing == false)
                {
                    Console.Clear();
                    PrintProductsWithInfo();

                   
                    Console.WriteLine("q: quit to main menu\n");
                    Console.ResetColor();

                    Console.Write("Pick the product you want to buy:");

                    string pickedProduct = Console.ReadLine();

                    if (pickedProduct == "q")
                        break;
                    else
                    {
                        Product product;
                        string message;

                        bool PurchaseResult = this.VendingMachineInstance.Purchase(pickedProduct, out product, out message);

                        if (PurchaseResult)
                        {
                            Console.WriteLine(product.Use());
                            QuitPurchasing = true;
                            Console.Write("Press Enter to continue..");
                            Console.ReadLine();
                        }
                        else
                        {
                           
                            Console.WriteLine(message);
                            Console.ResetColor();
                            Console.Write("Press Enter to continue..");
                            Console.ReadLine();
                        }
                    }
                }
            }
        }
        
    }

