using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alea.CSharp;
using Alea;
using Alea.Parallel;
using System.Diagnostics;
using Alea.cuRAND;
using System.IO;
using Alea.FSharp;

namespace ConsoleApp1
{
    class Program
    {

        const RngType rngType = RngType.PSEUDO_MRG32K3A;
        [GpuManaged]
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
           // Console.ReadKey();
            var Length = 100000;
            var gpu = Gpu.Default;
   

            var arg1 = Enumerable.Range(0, Length).ToArray();
            var arg2 = Enumerable.Range(0, Length).ToArray();

            var list = new List<double>();

          

            //-------------Alea GPU example
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            /*  var numBlocks = 4;
           var numSamplesPerBlock = 1 << 16;
           using (var rng = Generator.CreateCpu(rngType))
           {
               var gaussian = new double[numSamplesPerBlock];
               var poisson = new UInt32[numSamplesPerBlock];

               rng.SetPseudoRandomGeneratorSeed(42L);

               for (var block = 0; block < numBlocks; block++)
               {
                   rng.SetGeneratorOffset((ulong)block * (ulong)(numSamplesPerBlock));
                   rng.GenerateNormal(gaussian, 0, 1);
                   rng.GeneratePoisson(poisson, 1.0);
                   var sampleMeanGaussian = gaussian.Average();
                   var sampleMeanPoisson = poisson.Select(p => (double)p).Average();
                   Console.WriteLine(sampleMeanGaussian);

               }
           }*/


            //  for(var i = 0; i < 1; i++)
            // {

            // var result = NewMethod(gpu, arg1, arg2, Length,x.Next());
            //list.AddRange(result);


            /* using (StreamWriter writer =
              new StreamWriter("important.txt"))
             {

                 foreach(var i in list)
                 {
                   //  writer.WriteLine(i);
                 }

             }
             */
         
            var lp = new LaunchParam(16, 1024);//second parameter is number of  threadIdx
            var ar1 = Enumerable.Range(0, Length).ToArray();
            var ar2 = Enumerable.Range(0, Length).ToArray();
            var result = new double[Length];
            for(int i = 0; i < 10000; ++i)
            {
                
                Gpu.Default.Launch(Kernel, lp, result, ar1, ar2,i);

                foreach (var k in result)
                {
                    if (k == 14786)
                    {
                        Console.WriteLine("found "+k);
                       Console.WriteLine("attempt " + i);
                      //  break;
                    }
                    else
                    {
                       // Console.WriteLine(k);
                    }

                }
               
              //  Console.ReadKey();

            }

         

         






            stopWatch2.Stop();
            TimeSpan ts2 = stopWatch2.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts2.Hours, ts2.Minutes, ts2.Seconds,
                ts2.Milliseconds / 10);
            Console.WriteLine("RunTime GPU " + elapsedTime2);

            
            //  Console.WriteLine(string.Join(",", result));
           // Console.WriteLine(list.Count);
            Console.ReadKey();
        }
     
        private static void Kernel(double[] result, int[] arg1, int[] arg2,int f)//kernel is like the main method of alea
        {
            //gridDim: This variable contains the dimensions of the grid.//griddim is consistent based on the first parameter of launchpram

            // blockIdx: This variable contains the block index within the grid.

            //  blockDim: This variable and contains the dimensions of the block.//dim has consistent size based on the second parameter of LaunchParam(griddim,dim) 

            //  threadIdx: This variable contains the thread index within the block.//each threadIdx represents and id counting from 0 to the blockdim

            var start = blockIdx.x * blockDim.x + threadIdx.x;
            var stride = gridDim.x * blockDim.x;
            var len = result.Length;
            for (var i = start; i < len; i += stride)
            {

                if (result[i]==0)
                {
                    result[i] = Math.Round(i * start * Math.Sin(stride));
                }
                else
                {
                    result[i] = Math.Round(i * start * Math.Sin(stride+result[i]));
                }
                    
            }
        }




        /* private static void Kernel<T>(Func<T, T, T> op, T[] result, T[] arg)
         {
             var warps = blockDim.x;
             var warpId = threadIdx.x;
             var laneId = threadIdx.x & 0x1f;
             var input = arg[blockIdx.x * blockDim.x + threadIdx.x];



         }*/





        [GpuParam]
       static Random x = new Random();
     
        [GpuManaged]
        private static double[] NewMethod(Gpu gpu, int[] arg1, int[] arg2,int Length,int rand)
        {
            var result = new double[Length];



                gpu.LongFor(0, Length, i =>
                {


                   /* for (var k = 0; k < 10000; ++k)
                    {

                
                        result[i] = arg1[k]+arg2[k]; 
                    }*/
                });
            
            return result;
        }
    }
}
