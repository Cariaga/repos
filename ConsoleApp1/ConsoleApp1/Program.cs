using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alea.CSharp;
using Alea;
using Alea.Parallel;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var devices = Device.Devices;
            var numGpus = devices.Length;
            foreach (var device in devices)
            {
                device.Print();

                // note that device ids for all GPU devices in a system does not need to be continuous
                var id = device.Id;
                var arch = device.Arch;
                var numMultiProc = device.Attributes.MultiprocessorCount;
            }

            // all device ids
            var deviceIds = devices.Select(device => device.Id);
            Console.ReadKey();
            var Length = 100000;
            var gpu = Gpu.Default;
            var arg1 = Enumerable.Range(0, Length).ToArray();
            var arg2 = Enumerable.Range(0, Length).ToArray();
            var result = new int[Length];
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //----------CPU Eample
             for(var i = 0; i < Length; i++)
              {
                  for (var k = 0; k < 100000; ++k)
                  {
                      result[i] = arg1[i] + arg2[i];
                  }
              }
            stopWatch.Stop();


            TimeSpan ts = stopWatch.Elapsed;

    
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime CPU " + elapsedTime);

            Console.ReadKey();


            //-------------Alea GPU example
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            gpu.For(0, result.Length, i => {
                for (var k = 0; k < 100000; ++k){
                    result[i] = arg1[i] + arg2[i];
                }
            });
            stopWatch2.Stop();
            TimeSpan ts2 = stopWatch2.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts2.Hours, ts2.Minutes, ts2.Seconds,
                ts2.Milliseconds / 10);
            Console.WriteLine("RunTime GPU " + elapsedTime2);
            Console.ReadKey();
        }
    }
}
