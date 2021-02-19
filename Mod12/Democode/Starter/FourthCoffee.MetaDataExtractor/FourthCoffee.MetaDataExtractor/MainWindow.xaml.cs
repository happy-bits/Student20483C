using System.Reflection;
using System.Windows;
using FourthCoffee.Core.CustomAttributes;
using Microsoft.Win32;
using System.Linq;
using FourthCoffee.Core;
using System;

namespace FourthCoffee.MetaDataExtractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ExtractAssemblyAttributes();
                load.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error");
            }
        }

        // OO: Körs vid knapptryck
        private void ExtractAssemblyAttributes()
        {
            var type = typeof(Encryptor);

            /*
               OO: 
            
                "GetCustomAttribute" finns i System.Refection. Hämtar bara ett attribut för typen. (Finns även GetCustomAttributes)

                DeveloperInfo är ett attribut vi skapat själva (håller epostadress och revision)

            */

            // TODO: 01: Invoke the Type.GetCustomAttribute method.
            var typeAttribute = type.GetCustomAttribute<DeveloperInfo>(false); //OO: false = inspektera inte föräldrar

            results.Items.Add(this.FormatComment(typeAttribute, type.Name, "Type"));

            // OO: Hämta members för vår klass Encryptor
            foreach (var member in type.GetMembers())
            {
                // TODO: 02: Invoke the MemberInfo.GetCustomAttribute method.
                var memberAttribute = member.GetCustomAttribute<DeveloperInfo>(false);

                results.Items.Add(this.FormatComment(memberAttribute, member.Name, member.MemberType.ToString()));
            }

            // OO: "results" är listboxen i vyn (så den har uppdaterats nu)
        }

        private string FormatComment(DeveloperInfo attribute, string codeElement, string elementType)
        {
            if (attribute == null)
            {
                return string.Format(
                    "{0}: {1}, No DeveloperInfo attribute",
                    elementType,
                    codeElement);
            }
            else
            {
                return string.Format(
                 "{0}: {1}, Developed By: {2}, Revision: {3}",
                 elementType,
                 codeElement,
                 attribute.EmailAddress,
                 attribute.Revision);
            }
        }
    }
}
