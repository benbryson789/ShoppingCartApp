using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp
{
    class Program
    {
        private static readonly Dictionary<string, double> products = new Dictionary<string, double>()
        {
            {"soda", 1.00 },
            {"chips", 1.00 },
            {"veggies", 1.50 },
            {"fruits", 1.50 },
            {"water", 1.00 },
            {"milk", 2.50 },
            {"meat", 5.00 }
        };

        private static Dictionary<string, int> cart = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            string userChoice;

            do
            {
                Console.WriteLine("Select a product from the list");
                foreach (KeyValuePair<string, double> productEntry in products)
                {
                    string productName = productEntry.Key;
                    double productPrice = productEntry.Value;
                    Console.WriteLine($"{productName} - " +
                        $"{productPrice.ToString("C", CultureInfo.CurrentCulture)}");
                }

                Console.Write("Your choice: ");

                userChoice = Console.ReadLine().ToLower();
                if (String.IsNullOrWhiteSpace(userChoice))
                {
                    Console.WriteLine();
                    continue;
                }

                if (!products.ContainsKey(userChoice))
                {
                    Console.WriteLine($"{userChoice} is not a valid product\n");
                    continue;
                }

                KeyValuePair<string, double> product = new KeyValuePair<string, double>
                    (userChoice, products[userChoice]);

                Console.Write($"{userChoice} quantity: ");
                int qty = ConvertStringToInt(Console.ReadLine());
                if (qty == 0)
                {
                    Console.Clear();
                    continue;
                }

                if (!cart.ContainsKey(product.Key))
                {
                    if (qty < 0)
                    {
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        cart.Add(product.Key, qty);
                    }
                }
                else
                {
                    int currentQty = cart[product.Key];
                    if (currentQty + qty < 0)
                    {
                        cart.Remove(product.Key);
                    }
                    else
                    {
                        cart[product.Key] = currentQty + qty;
                    }
                }

                Console.Clear();
            }
            while (!String.IsNullOrWhiteSpace(userChoice));

            double total = GetCartTotal();
            Console.WriteLine($"Your total: {total.ToString("C", CultureInfo.CurrentCulture)}");

            Console.ReadLine();
        }

        private static double GetCartTotal()
        {
            double sum = 0.0;
            foreach (KeyValuePair<string, int> item in cart)
            {
                double price = products[item.Key];
                sum += (double)price * item.Value;
            }

            return sum;
        }

        private static int ConvertStringToInt(string s)
        {
            int qty = 0;
            if (String.IsNullOrWhiteSpace(s))
            {
                return qty;
            }

            Int32.TryParse(s, out qty);
            return qty;
        }
    }
}
