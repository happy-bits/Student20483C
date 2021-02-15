using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FourthCoffee.MethodTestHarness
{
    public class Utilities
    {
        public Utilities() 
        {
            // TODO: 02: Invoke the Initialize method.
            Initialize();
        }

        // TODO: 01: Define the Initialize method.
        
        bool Initialize() // Alt-Enter => hjälper till med allt
        {
            var path = GetApplicationPath(); // F12 (dyk in i funktionen)

            return !string.IsNullOrEmpty(path);
        }




        string GetApplicationPath() // Shift-F12 (var används funktionen?)   
        {
            // Ctrl minus = backa

            return Assembly.GetExecutingAssembly().Location;
        }

    }
}
