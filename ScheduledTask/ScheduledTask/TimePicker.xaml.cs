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
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker : Window
    {
        public TimePicker()
        {
            InitializeComponent();
        }
        public string time { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            time = Hour.SelectedValue + ":" + Minute.SelectedValue + " " + Sufix.SelectedValue;
            this.Close();
        }

        private void Minute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
