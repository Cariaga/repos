using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Data;
using System.Windows.Controls;
using System.Collections;
//on Wait Task Fix Task Getting cut off when saving datagrid
namespace ScheduledTask
{
    public class Data
    {
        public Data()
        {
            Time = "";
            TaskName = "";
            RepeatDays = "";
        }

        public Data(string time, string taskName)
        {


            this.Time = time;
            if (time == null || time == "")
            {
                this.Time = "";
            }
            TaskName = taskName;
        }
        public string Time { get; set; }
        public string TaskName { get; set; }
        public string RepeatDays { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Data> LifeSchedule { get; set; }
         
        public string ShortDate { get { return DateTime.Now.ToShortDateString(); } }
        public string ShortTime { get {return DateTime.Now.ToShortTimeString(); } }

        public SQLiteConnection DbConnection { get => m_dbConnection; set => m_dbConnection = value; }

        void CreateTextFile()
        {
            if (!File.Exists("LifeSchedule.txt"))
            {
                var stringVersion = "[]";
                Console.WriteLine(stringVersion);
                using (StreamWriter writer =
                new StreamWriter("LifeSchedule.txt"))
                {
                    writer.Write(stringVersion);
                }
            }
        }
        void ResetTimeTable()
        {
            LifeSchedule.Clear();
            Task.Run(() => {
                var Sufixes = new string[] { "AM", "PM" };
                foreach (var suffix in Sufixes)
                {
                    foreach (var hour in Enumerable.Range(1, 12))
                    {
                        var MinuteRange = new string[] { "00", "30" };

                        foreach (var minute in MinuteRange)
                        {
                            var time = "";
                            if (hour.ToString().Length < 1)
                            {
                                time += "0";
                            }
                            time += hour + ":" + minute + " " + suffix;


                            Console.WriteLine(time);
                            LifeSchedule.Add(new Data(time, ""));
                        }

                    }
                }
            });
          
            UpdateFromEditSave(LifeSchedule);

        }
        void UpdateFromEditSave(ObservableCollection<Data> observableCollection)
        {
           var x =  Task.Run(() => {
                var stringVersion = JsonConvert.SerializeObject(observableCollection);
                Console.WriteLine(stringVersion);
                using (StreamWriter writer =
                new StreamWriter("LifeSchedule.txt"))
                {
                    writer.Write(stringVersion);
                }
            });
            x.ContinueWith(f=>{
                Console.WriteLine("Saved");
            });
        }
        void ReadLifeSchedule()
        {

            string line;
            using (StreamReader reader = new StreamReader("LifeSchedule.txt"))
            {
                line = reader.ReadLine();
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GridData.ItemsSource = null;
                }));
                 

                if (this.LifeSchedule==null)
                {
                    this.LifeSchedule = new ObservableCollection<Data>();
                }
                this.LifeSchedule.Clear();
                var LifeSchedule = JsonConvert.DeserializeObject<ObservableCollection<Data>>(line);

                foreach (var i in LifeSchedule)
                {
                    this.LifeSchedule.Add(i);
                }
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GridData.ItemsSource = LifeSchedule;
                    Console.WriteLine(GridData.Items.Count + " " + LifeSchedule.Count);
                    GridData.Items.Refresh();
                }));
            }

        }
        public MainWindow()
        {
            InitializeComponent();
            LifeSchedule = new ObservableCollection<Data>();
           
          
            using (Process p = Process.GetCurrentProcess())
            {
                p.PriorityClass = ProcessPriorityClass.BelowNormal;
            }
            var x = Task.Run(() => {
                DatabaseConnect();
            });
            x.ContinueWith(f => {
                CreateTextFile();//if not exist create it
            });
            x.ContinueWith(f => {
                FixedTimer();
            });
            x.ContinueWith(f => {
                ReadLifeSchedule();
                LifeSchedule.CollectionChanged += CollectionChanged;
            });
        }
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GridData.ItemsSource = null;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                GridData.ItemsSource = LifeSchedule;
            }));
            GridData.Items.Refresh();
        }

        SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
        void DatabaseConnect()
        {
            if (DbConnection == null)
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
                string sql = "CREATE TABLE SleepPattern (" +
                           "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                           "DateIn DATE," +
                           "DateOut DATE," +
                           "TimeIn TIME," +
                           "TimeOut TIME" +
                           ");";
                SQLiteCommand command = new SQLiteCommand(sql, DbConnection);
                command.ExecuteNonQuery();
                string sql2 = "CREATE TABLE LifeTask (" +
                                "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "Date DATE," +
                                "Time       INTEGER," +
                               " TaskName VARCHAR," +
                               " Completion VARCHAR" +
                            ");";
                SQLiteCommand command2 = new SQLiteCommand(sql2, DbConnection);
                command2.ExecuteNonQuery();
                //sqlite always points to its self in the exe where its located
                DbConnection.Close();
            }
            DbConnection.Open();
            if (DbConnection != null)
            {         
               
            }
            DbConnection.Close();
        }
        void CommitTask(string Task,string Completion)
        {
            
            DbConnection.Open();
            var sql = "INSERT INTO LifeTask (" +
                         "Date," +
                         "Time," +
                         "TaskName," +
                         "Completion" +
                     ")" +
                     "VALUES(" +
                       "  '"+DateTime.Now.ToShortDateString()+"'," +
                        " '"+ DateTime.Now.ToShortTimeString() + "'," +
                         "'"+ Task + "'," +
                         "'"+ Completion + "'" +
                     ");";
            SQLiteCommand command = new SQLiteCommand(sql, DbConnection);
            command.ExecuteNonQuery();
            //sqlite always points to its self in the exe where its located
            DbConnection.Close();
        }

        void CheckScheduler()
        {
            var f = DateTime.Now.GetDateTimeFormats()[107];//time format of 12:00 PM
            var len = LifeSchedule.Count;
            for (var i =0;i< len;i++)
            {
                var current = LifeSchedule[i];
                if (f.Equals(current.Time)&&current.RepeatDays.Contains(DateTime.Today.DayOfWeek.ToString()))
                {
                    var x = MessageBox.Show(current.TaskName + " Done?", "", MessageBoxButton.YesNoCancel);
                    if (x==MessageBoxResult.Cancel)
                    {
                        MessageBox.Show(":<");
                    }else if (x== MessageBoxResult.Yes)
                    {
                        CommitTask(current.TaskName, "Ok");
                    }
                }
            }
        }
   
        void FixedTimer()
        {
            UpdateClock();
            var loop1Task = Task.Run(async () => {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    UpdateClock();
                }
            });
        }
  
        void UpdateClock()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                IntervalText.Content ="Time Per 5 Minutes "+DateTime.Now.GetDateTimeFormats()[107];
                CheckScheduler();
            }));
        }
        private void EndDay(object sender, System.ComponentModel.CancelEventArgs e)
        {
           var x = MessageBox.Show("End Day", "End Day", MessageBoxButton.YesNo);
            if (x== MessageBoxResult.Yes)
            {
                MessageBox.Show("Day Ended");
            }
            else
            {
             
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    btn.IsEnabled = false;
                }));
                    RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg.SetValue("AutoRun", System.Reflection.Assembly.GetExecutingAssembly().Location.ToString());
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("Registered at start up");
                    btn.IsEnabled = true;
                }));
            });
        
        }
        public IEnumerable<Data> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            var items = grid.ItemsSource as IEnumerable<Data>;
            return items;
        }
       
        private void SelectedCellChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            SelectedCellChange();
        }

        private void SelectedCellChange()
        {
            var x = GridData.CurrentCell;

            if (x != null && x.Column != null && x.Column.Header != null)
            {
                var dg = GridData as DataGrid;
                if (dg == null) return;
                var index = dg.SelectedIndex;
                if (x.Column.Header as string == "Time")
                {
                    var TimePicker = new TimePicker();
                    TimePicker.ShowDialog();
                    if (GridData.SelectedCells.Count > 0)
                    {

                        if (TimePicker.Hour.Text != "" && TimePicker.Minute.Text != "" && TimePicker.Sufix.Text != "")
                        {

                            LifeSchedule[index].Time = TimePicker.Hour.Text + ":" + TimePicker.Minute.Text + " " + TimePicker.Sufix.Text;
                            GridData.Items.Refresh();//must refresh
                        }
                    }
                }
                else if (x.Column.Header as string == "TaskName")
                {
                    var TaskAssign = new TaskAssign();
                    TaskAssign.ShowDialog();
                    if (GridData.SelectedCells.Count > 0)
                    {
                        var edit =MessageBox.Show("Do You want to Edit", "Edit?", MessageBoxButton.YesNo);
                        
                        if (edit == MessageBoxResult.Yes)
                        {
                            var task = TaskAssign.TaskType.SelectedValue + " " + TaskAssign.TaskDescription.Text;
                            LifeSchedule[index].TaskName = task;
                        }
                        

                        GridData.Items.Refresh();//must refresh
                    }
                }
                else if (x.Column.Header as string == "RepeatDays")
                {
                   
                    if (GridData.SelectedCells.Count > 0)
                    {
                        var Days = new Window1();
                        if (Days!=null)
                        {
                          var dig=  Days?.ShowDialog();
                            if (dig!=null)
                            {
                               var SelectedDays = String.Join(",", Days.DaysSelected.ToArray());

                                if (SelectedDays!="")
                                {
                                    LifeSchedule[index].RepeatDays = SelectedDays;
                                }
                                GridData.Items.Refresh();//must refresh

                            }
                        }
                    }
                }
            }
        }

        private void GridData_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var x = MessageBox.Show("Delete all Schedule?","Delete all",MessageBoxButton.YesNo);
            if (x == MessageBoxResult.Yes)
            {
                ResetTimeTable();
            }
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          
        }

        private void MouseMoved(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Console.WriteLine("Moved "+LifeSchedule.Count);
        }

   

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            UpdateFromEditSave(GridData.ItemsSource as ObservableCollection<Data>);
        }
    }
}
