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


            if (currentTask != null)
            {
                //IdTask = currentTask != null ? currentTask.TaskNumber.ToString() : string.Empty;

                IdTask = "Id Task: " + currentTask.TaskNumber.ToString();
                TaskName = "Task Name: " + currentTask.Description;
                NicnameTask = "Nicname Task: " + currentTask.Nickname;
            }
            


        }


        //public CurrentEngineerWindow(int idEngineer)
        //{

        //    try
        //    {
        //        // Appeler la méthode BL pour récupérer l'objet existant avec l'ID spécifié
        //        //var currentTask = s_bl.Task.ReadTaskInEngineer(idEngineer);
        //        _idEngineer = idEngineer;
        //        var tasks = s_bl.Task.RequestListTask().Where(t => t.Engineer.Id == idEngineer).ToList();
        //        var currentTask = tasks.FirstOrDefault(t => t.Status == STATUS.OnTrack);

        //        // Vérifier si l'objet existe
        //        if (currentTask != null)
        //        {
        //            CurrentTask = currentTask;
        //            InitializeComponent();
        //        }

        //        else
        //        {
        //            // Gérer le cas où l'objet n'existe pas
        //            MessageBox.Show($"Task of the Engineer with ID {idEngineer} not found.");
        //            InitializeComponent();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        // Gérer les exceptions, par exemple, afficher un message d'erreur
        //        MessageBox.Show($"Error: {ex.Message}");
        //    }
        //}




        private void showAllTasks(object sender, RoutedEventArgs e)
        {
            new TasksForEngineer(current).Show();
            Close();
        }

       

        private void updateCurrentTask(object sender, RoutedEventArgs e)
        {
            new Task.Task(int.Parse(IdTask)).Show();
            Close() ;
        }
    }
}
