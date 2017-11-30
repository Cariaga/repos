using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    public class Account : INotifyPropertyChanged
    {
        private string fname;
        private string lname;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyname="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        }

        public string Lname { get => lname; set { lname = value; NotifyPropertyChanged("lname"); }}
        public string Fname { get => fname; set { fname = value; NotifyPropertyChanged("fname"); }}
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Account s = new Account();
    public MainWindow()
        {
            InitializeComponent();
        s.Fname = "Mahak";
        s.Lname = "Garg";
        this.Login.DataContext = s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            s.Lname = "";
            s.Fname = "";
         
        }
    }
}
