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

namespace PL.Engineer;


/// <summary>
/// Interaction logic for Engineer.xaml
/// </summary>
public partial class Engineer : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


    bool isAdd;
    public BO.Engineer EngineerItem
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("EngineerItem", typeof(BO.Engineer), typeof(Engineer), new PropertyMetadata(null));


    public Engineer(int id=0)
    {
        isAdd=(id==0)?true:false;
        
        //EngineerItem = new BO.Engineer();
        InitializeComponent();
        try
        {
            EngineerItem = (id == 0) ? new BO.Engineer() : s_bl?.Engineer.GetEngineerDetails(id)!;
        }
        catch(Exception ex) 
        { }
    }

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (isAdd)
            {
                s_bl.Engineer.AddEngineer(EngineerItem);
                MessageBox.Show("Engineer successfully added");
                Close();
            }
            else
            {
                s_bl.Engineer.UpdateEngineerDetails(EngineerItem);
                MessageBox.Show("Engineer successfully updated");
                Close();
            }
        }
        catch (Exception ex) { }
   
    }
}
