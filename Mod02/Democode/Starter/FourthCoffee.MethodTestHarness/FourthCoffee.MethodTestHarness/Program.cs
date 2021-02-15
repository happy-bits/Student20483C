using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourthCoffee.MethodTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var utilities = new Utilities();

            int number = 123;
            
            int number2;

            MyMethod(ref number);

            MyMethod2(out number2);

        }

        private static void MyMethod(ref int number)
        {
            //number = 400;
            number++;

        }

        private static void MyMethod2(out int number)
        {
            number = 400;
            //number++;

        }
    }
}
