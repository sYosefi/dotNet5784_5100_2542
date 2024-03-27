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
    }
}
