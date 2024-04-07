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
using System.Windows.Shapes;

namespace PL
{

    /// <summary>
    /// Interaction logic for TasksForEngineer.xaml
    /// </summary>
    public partial class TasksForEngineer : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        BO.Engineer currentEngineer=new BO.Engineer();

        public ObservableCollection<BO.Task> TasksToEngineerList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TasksFEListProperty); }
            set { SetValue(TasksFEListProperty, value); }
        }

        public static readonly DependencyProperty TasksFEListProperty =
            DependencyProperty.Register("TasksToEngineerList", typeof(ObservableCollection<BO.Task>), typeof(TasksForEngineer), new PropertyMetadata(null));

       
        public TasksForEngineer()
        {
            InitializeComponent();
        }
        private bool isDependendCompleted(BO.Task task)
        {
            var dependentTasks = task.DependenciesList.Where(t => t?.Status != BO.Status.Done);
            if (dependentTasks!=null && dependentTasks.Count() > 0)
            {
                return false;
            }
            return true;
        }
        public TasksForEngineer(BO.Engineer engineer)
        {
            currentEngineer=engineer;

            if (engineer == null || engineer.IdEngineer==null)
            {
                MessageBox.Show("Engineer not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           if(engineer.CurrentTask==null)
            {
                List<BO.Task> allTasks = new List<BO.Task>();

                //List<BO.Task> taskFilter = new List<BO.Task>();

               var allsTasks = s_bl.Task.GetAllTasks();

                //var taskFiltered = allTasks.
                //    Where(task => task.eng == null || task.eng.IdEngineer == 0).Select(t=> t).ToList();

                var taskDeps = allsTasks.Where(task =>
                  isDependendCompleted(task)).ToList();

                //var allEngineers = allsTasks.Select(t => t.eng).ToList();


                var dificalt = taskDeps.Where(task =>
                 task?.DifficultyLevel <= (BO.Levels)engineer.EngineerLevel
                 && task?.ActualEndDate == null
                 ).ToList();




                
            
                TasksToEngineerList = new ObservableCollection<BO.Task>(dificalt);

            }
            else
            {

            }
            InitializeComponent();

        }

        private void setCurrentTask(object sender, MouseButtonEventArgs e)
        {
            try
            {
                currentEngineer.CurrentTask = (sender as ListView)?.SelectedItem as BO.Task;

                s_bl.Engineer.UpdateEngineerDetails(currentEngineer);
                new EngineerView(currentEngineer).Show();
                Close();
            }
            catch
            {
                MessageBox.Show("Couldn't add this task");
            }
        }
    }
}
