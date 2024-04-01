using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public Login()
        {
            InitializeComponent();
        }

        //private void login_button(object sender, RoutedEventArgs e)
        //{
        //    TextBox textBox=sender as TextBox;

        //    //int id = textBox.Text;
        //    //string username = ((TextBox)loginGrid.Children[0]).Text;
        //    int id = int.Parse(textBox.Text);
        //    //int id = int.Parse(((TextBox)loginGrid.Children[1]).Text);
        //    var user = s_bl.Engineer.GetEngineerDetails(id);
        //    if(user!=null) 
        //    {
        //        new EngineerView().Show();
        //        this.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("engineer not found");

        //    }

        //}

        private void login_button(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text) && int.TryParse(textBox.Text, out int id))
                {
                    var user = s_bl.Engineer.GetEngineerDetails(id);
                    if (user != null)
                    {
                        new EngineerView().Show();
                        this.Close();
                        return;
                    }
                }
            }

            MessageBox.Show("Engineer not found or invalid ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
