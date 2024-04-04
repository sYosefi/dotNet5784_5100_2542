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
            var dependentTasks=task.DependenciesList.FindAll(t => t.Status != BO.Status.Done);
            if (dependentTasks!=null)
            {
                return false;
            }
            return true;
        }
        public TasksForEngineer(BO.Engineer engineer)
        {

            if (engineer == null || engineer.IdEngineer==null)
            {
                MessageBox.Show("Engineer not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

           if(engineer.CurrentTask==null)
            {
                List<BO.Task> allTasks = new List<BO.Task>();

                //List<BO.Task> taskFilter = new List<BO.Task>();

                allTasks = s_bl.Task.GetAllTasks().ToList();
                
                var taskFiltered = allTasks.FindAll(task => task.eng.IdEngineer == engineer.IdEngineer
                 && isDependendCompleted(task) && task.DifficultyLevel<=(BO.Levels)engineer.EngineerLevel).ToList();
                TasksToEngineerList = new ObservableCollection<BO.Task>(taskFiltered);

            }
            else
            {

            }
            InitializeComponent();

        }


    }
}
