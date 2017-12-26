using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
public enum NotificationStatus
{
    Cancelled, Completed, Ongoing
}
namespace ConsoleApp4
{
    public class Browser
    {
        public async Task<string> Request(string url = "")
        {
            using (var httpClient = new HttpClient())
            using (var httpResonse = await httpClient.GetAsync(url))
            {
                Console.WriteLine(httpResonse.Content.ReadAsStringAsync().Result);
               // Marshal.AllocHGlobal(100000000);
                if (httpResonse.IsSuccessStatusCode)
                {
                    return await httpResonse.Content.ReadAsStringAsync();
                }
                else
                {
                    return "Error from : " + url;
                }
            }
        }
    }
    class Program
    {
        public static object UserInfo { get; private set; }


     

        static void Main(string[] args)
        {
            Console.WriteLine(NotificationStatus.Cancelled);



            /*  Console.WriteLine("Hello World!");
              Dictionary<string, string> dictionary = new Dictionary<string, string>();
              dictionary.TryAdd("Username","123");// try add will fail on duplicate entries
              dictionary.TryAdd("City", "123");
              dictionary.TryAdd("Pass", "123");
              Console.WriteLine(GenerateURLRequest.GenerateGet(dictionary));
              var str = "";*/


            // isUserExist();//newton not desserializing on internal object
            //isNumberExist();
           // isEmailExist();
            //big task test // the slower the task the less memory usage but the faster it is to get there
            //   SingleBigTask();//32 sec//average about 300000 memory
            // SingleBigTask2();//faster combining all async to one TaskRun will run faster 16 sec//350000 memory usage
            // SingleBigTask3();//fastest 15 secs 430000 memory

         //   var x = JsonWebAsync<string>("test").Result;
     

//Console.WriteLine(x);
            //split  test
            // SplitTaskRun();
            //  SplitTaskRunExtream();
            //  SplitTaskRunExtream2();


            Console.ReadKey();

        }

        public static async Task<T> JsonWebAsync<T>(T settingName)
        {
          var x = Task.Run(async () => {
                await Task.Delay(1000);
              
            });
           await Task.WhenAll(x);

            return settingName;

        }

        static void isUserExist()
        {
            var tocheck = "UserName2";
            var UserNameExistRequest = new Browser().Request("http://bvusolutions.com/Geo/Account/isUserExist.php?UserName="+ tocheck);
            var UserExistResult = JsonConvert.DeserializeObject<List<ViewModels.UserNameModel>>(UserNameExistRequest.Result);
            Task.WhenAll(UserNameExistRequest);
            Console.WriteLine(UserExistResult.FirstOrDefault().UserName);
        }
        static void isNumberExist()
        {
            var tocheck = "PhoneNumber";
            var ExistRequest = new Browser().Request("http://bvusolutions.com/Geo/Account/isNumberExist.php?PhoneNumber=" + tocheck);
            var ExistResult = JsonConvert.DeserializeObject<List<ViewModels.PhoneNumberModel>>(ExistRequest.Result);
            Task.WhenAll(ExistRequest);
            Console.WriteLine(ExistResult.FirstOrDefault().PhoneNumber);
        }
        static void isEmailExist()
        {
            var tocheck = "Email";
            var ExistRequest = new Browser().Request("http://bvusolutions.com/Geo/Account/isExistEmail.php?Email="+tocheck);
            var ExistResult = JsonConvert.DeserializeObject<List<ViewModels.EmailViewModel>>(ExistRequest.Result);
            Task.WhenAll(ExistRequest);
            Console.WriteLine(ExistResult.FirstOrDefault().Email);
        }
        static void SingleBigTask()
        {
            List<long> time = new List<long>();
            for (int i = 0; i < 30; i++)
            {
              
                var x = Task.Run(() =>
                {
                    var k = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(k);
                    Console.WriteLine(GC.GetTotalMemory(true));
                });

                Task.WaitAll(x);
              
        
                Console.WriteLine(time.Average());
              
            }
            Console.WriteLine(time.Count);
        }
        static void SingleBigTask2()
        {
            List<long> time = new List<long>();
            for (int i = 0; i < 6; i++)
            {
              
                var x = Task.Run(() =>
                {
                var k = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                var k2 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                var k3 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                var k4 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                var k5 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");

                Task.WaitAll(k, k2, k3, k4, k5);
                Console.WriteLine(GC.GetTotalMemory(true));
                });

                Task.WaitAll(x);
             
           
                Console.WriteLine(time.Average());
            }
         
            Console.WriteLine(time.Count);
        }
        static void SingleBigTask3()
        {
            List<long> time = new List<long>();
            for (int i = 0; i < 3; i++)
            {
                var s1 = Stopwatch.StartNew();
                var x = Task.Run(() =>
                {
                    var k = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k2 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k3 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k4 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k5 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k6 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k7 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k8 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k9 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    var k10 = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(k, k2, k3, k4, k5,k6,k7,k8,k9,k10);
                    Console.WriteLine(GC.GetTotalMemory(true));
                });

                Task.WaitAll(x);
       
         
                Console.WriteLine(time.Average());
            }

            Console.WriteLine(time.Count);
        }

        static void SplitTaskRun()
        {
              List<long> time = new List<long>();

             
               var x = Task.Run(() =>
               {
                   for (int i = 0; i < 15; i++)
                   {
                       var s1 = Stopwatch.StartNew();
                       var s = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                       Task.WaitAll(s);
                       time.Add(s1.ElapsedMilliseconds);
                       s1.Stop();
                   }
               });
               var y = Task.Run(() =>
               {
                   for (int i = 0; i < 15; i++)
                   {
                       var s1 = Stopwatch.StartNew();
                       var s = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                       Task.WaitAll(s);
                       time.Add(s1.ElapsedMilliseconds);
                       s1.Stop();
                   }
               });

               Task.WaitAll(x, y);
            
            Console.WriteLine(time.Average());
          
            Console.WriteLine(time.Count);
        }

        static void SplitTaskRunExtream()
        {
            List<long> time = new List<long>();

          
            var y = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);

                }
            });
            var y1 = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);
                }
            });
            var y2 = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);
                }
            });
            var y3 = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);
                }
            });
            var y4 = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);

                }
            });
            var y5 = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);

                }
            });

            Task.WaitAll(y,y2,y3,y4,y5);
          
            Console.WriteLine(time.Average());
            Console.WriteLine(time.Count);
        }
        static void SplitTaskRunExtream2()
        {
            List<long> time = new List<long>();


            var y = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);

                }
            });
    
            var y1 = y.ContinueWith(f =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);
                }
            });
            var y2 = y1.ContinueWith(f =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);
                }
            });
            var y3 = y2.ContinueWith(async f => {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);
                }
            });
            var y4 = y3.ContinueWith(f =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);

                }
            });
            var y5 = y4.ContinueWith(f =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var s1 = Stopwatch.StartNew();
                    var x = new Browser().Request("http://www.worldslongestwebsite.com/home/about");
                    Task.WaitAll(x);
                    s1.Stop();
                    time.Add(s1.ElapsedMilliseconds);

                }
            });

            Task.WaitAll(y, y2, y3, y4, y5);

            Console.WriteLine(time.Average());
            Console.WriteLine(time.Count);
        }
    }
    public static class GenerateURLRequest
    {
        public static string GenerateGet(Dictionary<string, string> dictionary)
        {
            var result = "?";
            foreach (var dic in dictionary)
            {
                result += dic.Key + "=" + dic.Value;
            }
            return result;
        }
    }
}
