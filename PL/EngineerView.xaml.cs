using BO;
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
        BO.Task currentTask= new BO.Task();
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


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

        public string IdTask
        {
            get { return (string)GetValue(IdTaskProperty); }
            set { SetValue(IdTaskProperty, value); }
        }

        public string TaskName
        {
            get { return (string)GetValue(TaskNameProperty); }
            set { SetValue(TaskNameProperty, value); }
        }

        public string NicnameTask
        {
            get { return (string)GetValue(NicnameTaskProperty); }
            set { SetValue(NicnameTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EngineerNameProperty =
            DependencyProperty.Register("EngineerName", typeof(string), typeof(EngineerView), new PropertyMetadata(null));

        public static readonly DependencyProperty EngineerTaskProperty =
          DependencyProperty.Register("EngineerTask", typeof(BO.Task), typeof(EngineerView), new PropertyMetadata(null));

        public static readonly DependencyProperty IdTaskProperty =
          DependencyProperty.Register("IdTask", typeof(string), typeof(EngineerView), new PropertyMetadata(null));

        public static readonly DependencyProperty TaskNameProperty =
          DependencyProperty.Register("TaskName", typeof(string), typeof(EngineerView), new PropertyMetadata(null));

        public static readonly DependencyProperty NicnameTaskProperty =
          DependencyProperty.Register("NicnameTask", typeof(string), typeof(EngineerView), new PropertyMetadata(null));

        public EngineerView()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show("An error occurred while initializing EngineerView: " + ex.Message);
            }
        }
        //public EngineerView()
        //{
        //    InitializeComponent();

        //}

        public EngineerView(BO.Engineer engineer)
        {
            try
            {
                //current = s_bl.Engineer.GetEngineerDetails((engineer.IdEngineer));
                //var tempTasks = s_bl.Task.GetAllTasks().Where(t => t.eng?.IdEngineer == engineer.IdEngineer).ToList();
                //var currentTask = tempTasks.FirstOrDefault(t => t.Status == BO.Status.OnTrack);
                current = engineer;
                EngineerName = engineer.Name; // Assuming engineer has a property Name
                EngineerTask = engineer.CurrentTask;

                if (current.CurrentTask != null)
                {
                    //IdTask = currentTask != null ? currentTask.TaskNumber.ToString() : string.Empty;

                    IdTask = "Id Task: " + currentTask.TaskNumber.ToString();
                    TaskName = "Task Name: " + currentTask.Description;
                    NicnameTask = "Nicname Task: " + currentTask.Nickname;
                }

                InitializeComponent();

            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show("An error occurred while initializing EngineerView: " + ex.Message);
            }
        }



        //public EngineerView(BO.Engineer engineer)
        //{
        //    //current = s_bl.Engineer.GetEngineerDetails((engineer.IdEngineer));
        //    var tempTasks=s_bl.Task.GetAllTasks().Where(t=>t.eng.IdEngineer==engineer.IdEngineer).ToList();
        //    var currentTask = tempTasks.FirstOrDefault(t => t.Status == BO.Status.OnTrack);


        //    InitializeComponent();
        //    EngineerName = engineer.Name; // Assuming engineer has a property Name
        //    EngineerTask = engineer.CurrentTask;


        //    if (currentTask != null)
        //    {
        //        //IdTask = currentTask != null ? currentTask.TaskNumber.ToString() : string.Empty;

        //        IdTask = "Id Task: " + currentTask.TaskNumber.ToString();
        //        TaskName = "Task Name: " + currentTask.Description;
        //        NicnameTask = "Nicname Task: " + currentTask.Nickname;
        //    }
        //}

        private void showAllTasks(object sender, RoutedEventArgs e)
        {
            try
            {
                new TasksForEngineer(current).Show();
                Close();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show("An error occurred while showing all tasks: " + ex.Message);
            }
        }


        //private void showAllTasks(object sender, RoutedEventArgs e)
        //{
        //    new TasksForEngineer(current).Show();
        //    Close();
        //}

        private void updateCurrentTask(object sender, RoutedEventArgs e)
        {
            try
            {
                new Task.Task(int.Parse(IdTask)).Show();
                Close();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show("An error occurred while updating the current task: " + ex.Message);
            }
        }

        private void EndofTask(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentTask != null)
                {
                currentTask = EngineerTask;
                currentTask.Status = Status.Done;
                currentTask.ActualEndDate = s_bl.Clock;

                MessageBoxResult mbresult =
                    MessageBox.Show("Are you sure you want to end a task?",
                                 "Done",
                                 MessageBoxButton.YesNoCancel,
                                 MessageBoxImage.Question,
                                 MessageBoxResult.Cancel);
                switch (mbresult)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            s_bl.Task.UpdateTask(currentTask);
                        }
                        catch (Exception ex)
                        {
                            // Handle or log the exception
                            MessageBox.Show("An error occurred while updating the task: " + ex.Message);
                        }
                        break;
                    case MessageBoxResult.No: break;
                    case MessageBoxResult.Cancel: break;
                    default: break;
                }
                }
                //אפשר לזרוק כאן שלא קיימת משימה נוכחית
               
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //private void EndofTask(object sender, RoutedEventArgs e)
        //{

        //    currentTask = EngineerTask;
        //    currentTask.Status = Status.Done;
        //    currentTask.ActualEndDate = s_bl.Clock;

        //    MessageBoxResult mbresult =
        //        MessageBox.Show("Are you sure you want to end a task?",
        //                     "Done",
        //                     MessageBoxButton.YesNoCancel,
        //                     MessageBoxImage.Question,
        //                     MessageBoxResult.Cancel);
        //    switch (mbresult)
        //    {
        //        case MessageBoxResult.Yes:
        //            s_bl.Task.UpdateTask(currentTask);
        //            break;
        //        case MessageBoxResult.No: break;
        //        case MessageBoxResult.Cancel: break;
        //        default: break;
        //    }

        //}

        //private void updateCurrentTask(object sender, RoutedEventArgs e)
        //{
        //    new Task.Task(int.Parse(IdTask)).Show();
        //    Close() ;
        //}
    }

    
}
