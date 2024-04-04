using PL.Task;
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
    /// Interaction logic for EngineerView.xaml
    /// </summary>
    public partial class EngineerView : Window
    {
        BO.Engineer current=new BO.Engineer();
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        //public BO.Task currentTask
        //{
        //    get { return (BO.Task)GetValue(currentTaskProperty); }
        //    set { SetValue(currentTaskProperty, value); }
        //}

        //public static readonly DependencyProperty currentTaskProperty =
        //    DependencyProperty.Register("currentTask", typeof(BO.Task), typeof(EngineerView), new PropertyMetadata(null));

        public string EngineerName
        {
            get { return (string)GetValue(EngineerNameProperty); }
            set { SetValue(EngineerNameProperty, value); }
        }

        public BO.Task EngineerTask
        {
            get { return (BO.Task)GetValue(EngineerTaskProperty); }
            set { SetValue(EngineerTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerNameProperty =
            DependencyProperty.Register("EngineerName", typeof(string), typeof(EngineerView), new PropertyMetadata(null));

        public static readonly DependencyProperty EngineerTaskProperty =
          DependencyProperty.Register("EngineerTask", typeof(string), typeof(EngineerView), new PropertyMetadata(null));
        public EngineerView()
        {
            InitializeComponent();

        }

        public EngineerView(BO.Engineer engineer)
        {
            //current = s_bl.Engineer.GetEngineerDetails((engineer.IdEngineer));
            var tempTasks=s_bl.Task.GetAllTasks().Where(t=>t.eng.IdEngineer==engineer.IdEngineer).ToList();
            var currentTask = tempTasks.FirstOrDefault(t => t.Status == BO.Status.OnTrack);


            InitializeComponent();
            EngineerName = engineer.Name; // Assuming engineer has a property Name
            EngineerTask = engineer.CurrentTask;


        }

        private void showAllTasks(object sender, RoutedEventArgs e)
        {
            new TasksForEngineer(current).Show();
            Close();
        }

        private void showCurrentTask(object sender, RoutedEventArgs e)
        {

            //new Task().show();
            //new CurrentTaskForEngineer(EngineerTaskProperty).Show();
            Close() ;
        }
    }
}
