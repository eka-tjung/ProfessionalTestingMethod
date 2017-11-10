using System;
using System.Collections.Generic;

namespace ShoppingCartDemo
{
    class Program
    {
        static List<Inventory> _inventoryList;
        static Dictionary<Inventory, int> _cart;
        static Inventory[] mapping;

        static Program()
        {
            // Set up the initial inventory to purchase
            InventoryList defaultList = new InventoryList();
            _inventoryList = defaultList.List;

            // Initialize empty shopping cart
            _cart = new Dictionary<Inventory, int>();
        }

        static void Main(string[] args)
        {
            int userInput;

            // Display shopping cart
            do
            {
                DisplayMenuOptions();
                userInput = GetPurchaseOption();

                // TODO1: Uncomment to be able to respond to user input
                RespondToMenu(userInput);
            } while (userInput != 4);

            // Display the summary of the transaction
            DisplaySummary();

            Console.WriteLine("Press any key to end the program.");
            Console.ReadKey();
        }

        /// <summary>
        /// Display the menu options
        /// </summary>
        static void DisplayMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the shopping cart demo.  Please choose from the following options:");
            Console.WriteLine("1. Purchase items");
            Console.WriteLine("2. Remove items");
            Console.WriteLine("3. Display shopping cart");
            Console.WriteLine("4. Check out");
        }

        /// <summary>
        /// Responds to menu selection
        /// </summary>
        /// <param name="userInput">User selection</param>
        static void RespondToMenu(int userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case 1:
                    AllowPurchasing();
                    break;
                case 2:
                    AllowRemoving();
                    break;
                case 3:
                    DisplayCart();
                    break;
                case 4:
                    // User is exiting.  Do nothing.
                    break;
                default:
                    Console.WriteLine("Invalid input.  Please try again.");
                    break;
            }
        }

        /// <summary>
        /// Display everything currently in the shopping cart.
        /// </summary>
        static void DisplayCart()
        {
            int cartSize = _cart.Count;
            Console.WriteLine("There are {0} items in your cart.", cartSize);
            Console.WriteLine("Your cart has the following items:");
            foreach (KeyValuePair<Inventory, int> entry in _cart)
            {
                Console.WriteLine(entry.Key.DisplayName() + " Quantity: " + entry.Value);
            }

            // Display summary
            Console.WriteLine();
            Console.WriteLine("If you were to check out now:");
            DisplaySummary();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void AllowRemoving()
        {
            int userInput = 0;
            int listSize = _cart.Count;

            if (listSize == 0)
            {
                Console.WriteLine("Your cart is empty!");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }

            do
            {
                // Display removal options
                DisplayRemovalOptions();
                userInput = GetPurchaseOption();

                if (userInput > 0 && userInput <= listSize)
                {
                    //Update shopping cart
                    if(_cart.ContainsKey(mapping[(userInput - 1)]))
                    {
                        // TODO3: Reduce the item's quantity in the cart

                        if (_cart[mapping[(userInput-1)]] == 0)
                        {
                            // TODO3: Quantity is 0, remove item from the cart
                        }
                    }

                    // Show the user what they removed
                    Console.Clear();
                    Console.WriteLine("You removed: {0}", mapping[(userInput - 1)].DisplayName());
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid user input!");
                    break;
                }
            } while (true);
        }

        /// <summary>
        /// Display removal options for user
        /// </summary>
        static void DisplayRemovalOptions()
        {
            int i = 0;
            int listSize = _cart.Count;
            mapping = new Inventory[listSize];

            Console.WriteLine("Please choose from the following menu:");

            foreach (KeyValuePair<Inventory, int> entry in _cart)
            {
                mapping[i++] = entry.Key;
                Console.WriteLine("{0}. {1} Quantity: {2}", i, entry.Key.DisplayName(), entry.Value);
            }

            Console.WriteLine("{0}. Finish removing", (listSize + 1));
            Console.WriteLine("Which one would you like to remove?");
        }

        /// <summary>
        /// Allow user to keep purchasing stuff
        /// </summary>
        static void AllowPurchasing()
        {
            int userInput = 0;
            int listSize = _inventoryList.Count;
            do
            {
                // Display purchase options
                DisplayPurchaseOptions();
                userInput = GetPurchaseOption();

                if (userInput > 0 && userInput <= listSize)
                {
                    //Update shopping cart
                    if(_cart.ContainsKey(_inventoryList[(userInput - 1)]))
                    {
                        // TODO3: Add the quantity to the same item
                    }
                    else
                    {
                        // TODO3: Add the item to the cart
                    }

                    // Show the user what they purchased
                    Console.Clear();
                    Console.WriteLine("You purchased: {0}", _inventoryList[(userInput - 1)].DisplayName());
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid user input!");
                    break;
                }
            } while (true);
        }

        /// <summary>
        /// Display purchase options for user
        /// </summary>
        static void DisplayPurchaseOptions()
        {
            int listSize = _inventoryList.Count;
            Console.WriteLine("Please choose from the following menu:");

            //TODO2: Put this in a loop
            Console.WriteLine("1. {0}", _inventoryList[0].DisplayName());
            Console.WriteLine("2. {0}", _inventoryList[2].DisplayName());
            Console.WriteLine("3. {0}", _inventoryList[0].DisplayName());
            Console.WriteLine("4. Finish purchasing");

            Console.WriteLine("Which one would you like to purchase?");

            Console.ReadLine();
        }

        /// <summary>
        /// Get the purchasing option from the user
        /// </summary>
        /// <returns>0 if user didn't give the correct option, more than 0 if user gives an int</returns>
        static int GetPurchaseOption()
        {
            int userInput = 0;
            try
            {
                userInput = Convert.ToInt16(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Please enter a whole number!");
            }
            return userInput;
        }

        /// <summary>
        /// Get the subtotal from the shopping cart
        /// </summary>
        static int GetSubtotal()
        {
            int subtotal = 0;
            foreach (KeyValuePair<Inventory, int> entry in _cart)
            {
                subtotal += (entry.Key.Price * entry.Value);
            }
            return subtotal;
        }

        /// <summary>
        /// Display summary of the purchase
        /// </summary>
        static void DisplaySummary()
        {
            Console.WriteLine("Checkout functionality has not been implemented");
            Console.WriteLine("Please implement me when you have a chance");
        }
    }
}
