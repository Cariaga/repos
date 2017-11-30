using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
      static  List<int> linked = new List<int>();
        //
        // Use AddLast method to add elements at the end.
        // Use AddFirst method to add element at the start.
        //
       
        static void Main(string[] args)
        {
            linked.Add(0);
            linked.Add(0);
            linked.Add(0);
            linked.Add(0);


            while (!linked.TrueForAll(x => x==26))
            {
                for(var i = 1; i < linked.Count;++i)
                {
                    
                    if (i < linked.Count&&linked[0] == linked[i+1])
                    {
                        linked[0]++;
                        //                       break;
                    }
                    else if(i<linked.Count&&linked[0] < linked[i+1])
                    {
                        linked[i+1]++;
                        break;
                    }
                    





                    var write = "";
                   for(var f= 0; f < linked.Count; ++f)
                    {
                        write += " " + linked[f];
                    }
                    Console.WriteLine(write);
                    if (write == "26 25 25 25")
                    {
                        Console.WriteLine(write);
                    }
                }
               
            }
           


            Console.ReadKey();
        }

    }
}
