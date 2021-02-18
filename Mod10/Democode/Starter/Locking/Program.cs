using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Locking
{  
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Coffee instance with enough stock to make 1000 coffees.

            //OneThread();

            MultipleThreads();

            Console.ReadLine();
        }


        private static void OneThread()
        {
            Coffee coffee = new Coffee(1000);

            Random r = new Random();

            for (int i = 0; i < 100; i++)
            {
                coffee.MakeCoffees(r.Next(1, 100));
            }

        }

        private static void MultipleThreads()
        {
            Coffee coffee = new Coffee(1000);

            Random r = new Random();
            
            Parallel.For(0, 100, index => // OO: index går från 0 till 100
            {
                coffee.MakeCoffees(r.Next(1, 100));   // OO: Dra en slumpmässig mängd kaffe
            });
        }

    }
}
