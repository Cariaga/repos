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

namespace ScheduledTask
{
    /// <summary>
    /// Interaction logic for TaskAssign.xaml
    /// </summary>
    public partial class TaskAssign : Window
    {
        public TaskAssign()
        {
            InitializeComponent();
            var x = new List<string>();
            
            x.Add("Reminder");
            x.Add("Take Medicine");
            x.Add("Do Task");
            x.Add("Do Study");
            x.Add("Other");
            x.Add("");
            TaskType.ItemsSource = x;
            TaskType.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TaskDescription_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
