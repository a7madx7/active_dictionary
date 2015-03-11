using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Active_Dictionary.Pages
{
    /// <summary>
    ///     Interaction logic for aboutAH.xaml
    /// </summary>
    public partial class aboutAH : UserControl
    {
        public aboutAH()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start(@"http://www.facebook.com/ph.ahmed.hamdy.emara");
        }
    }
}