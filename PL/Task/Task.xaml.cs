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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        bool isAdd;

        public BO.Task TaskItem
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
       DependencyProperty.Register("TaskItem", typeof(BO.Task), typeof(Task), new PropertyMetadata(null));

        private List<BO.TaskOnList> _dependenciesInput;
        public List<BO.TaskOnList> DependenciesInput
        {
            get { return _dependenciesInput; }
            set
            {
                _dependenciesInput = value;
                // No need to parse the input string, as it's already a List<BO.TaskOnList>
                DependenciesList = value.Select(task => task.TaskNumber).ToArray();
            }
        }

        private int[] _dependenciesList;
        public int[] DependenciesList
        {
            get { return _dependenciesList; }
            set
            {
                _dependenciesList = value;
            }
        }

        public Task(int id=0)
        {
            isAdd=(id==0)?true:false;
            InitializeComponent();
            try
            {
                TaskItem = (id == 0) ? new BO.Task() : s_bl?.Task.GetTaskDetails(id)!;
            }
            catch(Exception ex) { }
        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isAdd)
                {
                    s_bl.Task.AddTask(TaskItem);
                    MessageBox.Show("Task successfully added");
                    Close();
                }
                else
                {
                    s_bl.Task.UpdateTask(TaskItem);
                    MessageBox.Show("Task successfully updated");
                    Close();
                }
            }
            catch (Exception ex) { }
        }
        private List<BO.TaskOnList> ParseStringToList(string input)
        {
            // Split the input string by commas and convert each part to an integer
            string[] parts = input.Split(',');
            List<int> numbers = new List<int>();
            List<BO.Task> allTasks = s_bl.Task.GetAllTasks().ToList();
            List<BO.TaskOnList> dependence = new List<BO.TaskOnList>();
            foreach (string part in parts)
            {
                if (int.TryParse(part.Trim(), out int number))
                {
                    numbers.Add(number);
                }
            }
            foreach (int number in numbers)
            {
                BO.Task task = allTasks.Find(t => t.TaskNumber == number);
                if (task != null)
                {
                    BO.TaskOnList taskOnList = new BO.TaskOnList
                    {
                        TaskNumber = task.TaskNumber,
                        Description = task.Description // You can add other properties as needed
                    };
                    dependence.Add(taskOnList);
                }
            }
            return dependence;
        }
    }
}
