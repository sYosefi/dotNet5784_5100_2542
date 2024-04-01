using PL.Engineer;
using PL.Task;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
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

        private void ShowList_click(object sender, RoutedEventArgs e)
        {
            new EngineerList().Show();
        }

        private void Initializatoin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbresult=
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
                case MessageBoxResult.No:break;
                case MessageBoxResult.Cancel:break;
                default:break;
            }
        }

        private void ShowTaskList_click(object sender, RoutedEventArgs e)
        {
            new TaskList().Show();
        }

        private void makeChart_button(object sender, RoutedEventArgs e)
        {

        }

        private void reset_button(object sender, RoutedEventArgs e)
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
                    DalTest.Initialization.ResetByManeger();
                    break;
                case MessageBoxResult.No: break;
                case MessageBoxResult.Cancel: break;
                default: break;
            }
        }
    }
}
