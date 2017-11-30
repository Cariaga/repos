using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public List<string> DaysSelected = new List<string>();
        public Window1()
        {
            InitializeComponent();
            refreshall();//doublechecker
        }
        
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void Monday_Checked(object sender, RoutedEventArgs e)
        {
            monday();
        }

        private void monday()
        {
            if (Monday.IsChecked == true)
            {
                DaysSelected.Add("Monday");
            }
            else
            {
                DaysSelected.Remove("Monday");
            }
        }

        private void Tueday_Checked(object sender, RoutedEventArgs e)
        {
            tueday();
        }

        private void tueday()
        {
            if (Tueday.IsChecked == true)
            {
                DaysSelected.Add("Tueday");
            }
            else
            {
                DaysSelected.Remove("Tueday");
            }
        }

        private void Wednesday_Checked(object sender, RoutedEventArgs e)
        {
            wednesday();
        }

        private void wednesday()
        {
            if (Wednesday.IsChecked == true)
            {
                DaysSelected.Add("Wednesday");
            }
            else
            {
                DaysSelected.Remove("Wednesday");
            }
        }

        private void Thursday_Checked(object sender, RoutedEventArgs e)
        {
            thursday();
        }

        private void thursday()
        {
            if (Thursday.IsChecked == true)
            {
                DaysSelected.Add("Thursday");
            }
            else
            {
                DaysSelected.Remove("Thursday");
            }
        }

        private void Friday_Checked(object sender, RoutedEventArgs e)
        {
            friday();
        }

        private void friday()
        {
            if (Friday.IsChecked == true)
            {
                DaysSelected.Add("Friday");
            }
            else
            {
                DaysSelected.Remove("Friday");
            }
        }

        private void Saturday_Checked(object sender, RoutedEventArgs e)
        {
            saturday();
        }

        private void saturday()
        {
            if (Saturday.IsChecked == true)
            {
                DaysSelected.Add("Saturday");
            }
            else
            {
                DaysSelected.Remove("Saturday");
            }
        }

        private void Sunday_Checked(object sender, RoutedEventArgs e)
        {
            sunday();
        }

        private void sunday()
        {
            if (Sunday.IsChecked == true)
            {
                DaysSelected.Add("Sunday");
            }
            else
            {
                DaysSelected.Remove("Sunday");
            }
        }
        void refreshall()
        {
            monday();
            tueday();
            wednesday();
            thursday();
            friday();
            saturday();
            sunday();

            DaysSelected= DaysSelected.Distinct().ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            refreshall();//doublechecker
            this.Close();
      
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Monday.IsChecked = true;
            Tueday.IsChecked = true;
            Wednesday.IsChecked = true;
            Thursday.IsChecked = true;
            Friday.IsChecked = true;
            Saturday.IsChecked = true;
            Sunday.IsChecked = true;
            refreshall();//doublechecker
        }
    }
}
