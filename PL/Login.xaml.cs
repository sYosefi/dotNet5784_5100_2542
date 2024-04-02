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

        public string EngineerId
        {
            get { return (string)GetValue(EngineerIdProperty); }
            set { SetValue(EngineerIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerIdProperty =
            DependencyProperty.Register("EngineerId", typeof(string), typeof(Login), new PropertyMetadata(null));



        private void login_button(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Engineer current = s_bl.Engineer.GetEngineerDetails(int.Parse(EngineerId));
                new EngineerView(current).Show();
                Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Engineer not found or invalid ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }


        }

    }
}
