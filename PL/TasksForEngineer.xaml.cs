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
    /// Interaction logic for TasksForEngineer.xaml
    /// </summary>
    public partial class TasksForEngineer : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
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
            //if (engineer.CurrentTask == null)
            //{
            //    var temp = s_bl.Task.ReadAll(task => task.Engineer == null &&
            //        (task.CopmlexityLevel == engineer.Level || task.CopmlexityLevel < engineer.Level)
            //    );

            //    TaskList = temp == null ? new() : new(temp);
            //}
            //else
            //{
            //    var task = s_bl.Task.Read(engineer.Task.Id);

            //}

            //(Levels)((int)experience)

            InitializeComponent();
           

            if (engineer.CurrentTask==null)
            {

                //List<BO.Task> taskFilter = new List<BO.Task>();
                List<BO.Task> allTasks = new List<BO.Task>();
                allTasks = s_bl.Task.GetAllTasks().ToList();

                //var taskFilter = allTasks.FindAll(task => task.eng.IdEngineer == engineer.IdEngineer
                // && isDependendCompleted(task) && task.DifficultyLevel<=(BO.Levels)engineer.EngineerLevel);
                var taskFilter = allTasks.FindAll(task => task.eng.IdEngineer == engineer.IdEngineer
                && isDependendCompleted(task) && (int)task.DifficultyLevel <= (int)engineer.EngineerLevel);
            }
            else
            {

            }
        }


    }
}
