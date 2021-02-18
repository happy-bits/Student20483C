﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Locking
{
    class Coffee
    {
        // OO: låset används för att skydda "stock"
        private object coffeeLock = new object();

        int stock;

        public Coffee(int initialStock)
        {
            stock = initialStock;
        }

        public bool MakeCoffees(int coffeesRequired)
        {
            // This condition will never be true unless you comment out the lock statement.
            if (stock < 0)
            {
                throw new Exception("Stock cannot be negative!");
            }

            lock (coffeeLock)
            {

                // OO: stock = hur många kaffe som finns kvar

                // Check that there is sufficient stock to fulfil the order.
                if (stock >= coffeesRequired)
                {
                    // Introduce a delay to make thread contention more likely.
                    Thread.Sleep(500);

                    Console.WriteLine($"Stock level before making coffee: {stock}");
                    Console.WriteLine("Making coffee...");
                    stock = stock - coffeesRequired;
                    Console.WriteLine($"Stock level after making coffee: {stock}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Insufficient stock to make coffee");
                    return false;
                }
            }
        }
    }
}
