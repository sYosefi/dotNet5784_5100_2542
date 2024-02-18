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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerList.xaml
    /// </summary>
    public partial class EngineerList : Window

    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Experience EngineerExperience { get; set; } = BO.Experience.None;
        public ObservableCollection<BO.Engineer> EngineersList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineersList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerList), new PropertyMetadata(null));

        public EngineerList()
        {
            var temp = s_bl?.Engineer.GetListOfEngineers();
            EngineersList = temp == null ? new() : new(temp);
            InitializeComponent();
        }

        private void SortByExperience(object sender, SelectionChangedEventArgs e)
        {

            var temp = EngineerExperience == BO.Experience.None ?
            s_bl?.Engineer.GetListOfEngineers() :
            s_bl?.Engineer.GetListOfEngineers().Where(item => item.EngineerLevel == EngineerExperience);
            EngineersList = temp == null ? new() : new(temp);

        }

        private void Add_button(object sender, RoutedEventArgs e)
        {
            new Engineer().ShowDialog();
            var temp = s_bl?.Engineer.GetListOfEngineers();
            EngineersList = temp == null ? new() : new(temp);

        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? eng=(sender as ListView)?.SelectedItem as BO.Engineer;
            new Engineer(eng.IdEngineer).ShowDialog();
            var temp = s_bl?.Engineer.GetListOfEngineers();
            EngineersList = temp == null ? new() : new(temp);
        }
    }
}
