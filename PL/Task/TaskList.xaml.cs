using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskList : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Levels TaskLevel { get; set; }=BO.Levels.None;

        public ObservableCollection<BO.Task> TasksList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty=
            DependencyProperty.Register("TasksList",typeof(ObservableCollection<BO.Task>),typeof(TaskList),new PropertyMetadata(null));

        public TaskList()
        {
            var temp = s_bl?.Task.GetAllTasks().ToList();
            TasksList = temp == null ? new() : new(temp);
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Task? t = (sender as ListView)?.SelectedItem as BO.Task;
            new Task(t.TaskNumber).ShowDialog();
            var temp = s_bl?.Task.GetAllTasks();
            TasksList = temp == null ? new() : new(temp);
        }

        private void add_button(object sender, RoutedEventArgs e)
        {
            new Task().ShowDialog();
            var temp = s_bl?.Task.GetAllTasks();
            TasksList = temp == null ? new() : new(temp);
        }

        private void SortByExperience(object sender, SelectionChangedEventArgs e)
        {
            var temp = TaskLevel == BO.Levels.None ?
            s_bl?.Task.GetAllTasks() :
            s_bl?.Task.GetAllTasks().Where(item => item.DifficultyLevel == TaskLevel);
            TasksList = temp == null ? new() : new(temp);
        }
    }
}
