
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Entrance.xaml
    /// </summary>
    public partial class Entrance : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(Entrance), new PropertyMetadata(null));
        public Entrance()
        {
            InitializeComponent();
            CurrentTime = s_bl.Clock;
        }

        private void initalize_button(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult mbresult =
                    MessageBox.Show("Do you want to initialize the DB?",
                                 "Initialization",
                                 MessageBoxButton.YesNoCancel,
                                 MessageBoxImage.Question,
                                 MessageBoxResult.Cancel);
                switch (mbresult)
                {
                    case MessageBoxResult.Yes:
                        DalTest.Initialization.Do();
                        break;
                    case MessageBoxResult.No: break;
                    case MessageBoxResult.Cancel: break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during initialization: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void reset_button(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult mbresult =
                    MessageBox.Show("Do you want to reset the DB?",
                                 "Reset",
                                 MessageBoxButton.YesNoCancel,
                                 MessageBoxImage.Question,
                                 MessageBoxResult.Cancel);
                switch (mbresult)
                {
                    case MessageBoxResult.Yes:
                        s_bl.Reset();
                        break;
                    case MessageBoxResult.No: break;
                    case MessageBoxResult.Cancel: break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during reset: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //private void initalize_button(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult mbresult =
        //        MessageBox.Show("Do you want to initialize the DB?",
        //                     "Initialization",
        //                     MessageBoxButton.YesNoCancel,
        //                     MessageBoxImage.Question,
        //                     MessageBoxResult.Cancel);
        //    switch (mbresult)
        //    {
        //        case MessageBoxResult.Yes:
        //            DalTest.Initialization.Do();
        //            break;
        //        case MessageBoxResult.No: break;
        //        case MessageBoxResult.Cancel: break;
        //        default: break;
        //    }
        //}

        //private void reset_button(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult mbresult =
        //        MessageBox.Show("Do you want to reset the DB?",
        //                     "Reset",
        //                     MessageBoxButton.YesNoCancel,
        //                     MessageBoxImage.Question,
        //                     MessageBoxResult.Cancel);
        //    switch (mbresult)
        //    {
        //        case MessageBoxResult.Yes:
        //            s_bl.Reset();
        //            break;
        //        case MessageBoxResult.No: break;
        //        case MessageBoxResult.Cancel: break;
        //        default: break;
        //    }
        //}

        private void adminLogin_button(object sender, RoutedEventArgs e)
        {
            new ManagerView().Show();
            this.Close();
        }

        private void engineerLogin_button(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        private void addYear(object sender, RoutedEventArgs e)
        {
            s_bl.InsertYear(1);
        }

        private void addMonth(object sender, RoutedEventArgs e)
        {
            s_bl.InsertMonth(1);
        }

        private void addDay(object sender, RoutedEventArgs e)
        {
            s_bl.InsertDay(1);
        }
    }
}
