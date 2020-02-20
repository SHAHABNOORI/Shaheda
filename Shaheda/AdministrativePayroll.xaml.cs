using System;
using System.Windows;

namespace Shaheda
{
    /// <summary>
    /// Interaction logic for AdministrativePayroll.xaml
    /// </summary>
    public partial class AdministrativePayroll : Window
    {
        public AdministrativePayroll()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Date and Time top of the page
            int i = 1;
            txtDate.Text= DateTime.Now.ToString("dddd , MMM dd yyyy") + " ( Week "+i+" )";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
