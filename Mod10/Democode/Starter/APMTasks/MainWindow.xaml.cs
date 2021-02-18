using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APMTasks
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

        private async void btnCheckUrl_Click(object sender, RoutedEventArgs e)
        {
            string url = txtUrl.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    WebRequest request = WebRequest.Create(url);

                    /*
                     Asynchronous Programming Model  är en gammal model, inte längre rekommenderat
                     
                     */
                    // FromAsync följer Asynchronous Programming Model

                    HttpWebResponse response = await Task<WebResponse>.Factory.FromAsync(
                        request.BeginGetResponse, // OO: "beginmetod" = funktionen som börjar den asynkrona operationen (BeginGetResponse = startar requestet)
                        request.EndGetResponse,   // OO: "endmetod" = EndGetResponse = returnerar svaret
                        request                   // OO: "state" = datan som används i beginmetoden
                    ) as HttpWebResponse;

                    lblResult.Content = $"The URL returned the following status code: {response.StatusCode}";

                }
                catch (Exception ex)
                {
                    lblResult.Content = ex.Message;
                }

            }
            else
            {
                lblResult.Content = string.Empty;
            }
        }

    }
}
