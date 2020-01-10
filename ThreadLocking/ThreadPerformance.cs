using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace ThreadLocking
{
    class ThreadPerformance
    {
        public static void increasecounter1()
        {
            long count1 = 0;

            for(int i= 0; i<=1000000; i++)
           
                count1++;
                Console.WriteLine("counter1 value is "+ count1);

            

        }

        public static void increasecounter2()
        {
            long count2 = 0;

            for (int i = 0; i <= 1000000; i++)
             count2++;
                Console.WriteLine("counter2 value is " + count2);

            


        }

        static void Main()
        {
            Stopwatch s1 = new Stopwatch();

            // Below lines of code to check performance using single thread environment.

            s1.Start();
            increasecounter1();
            increasecounter2();
            s1.Stop();

            // Below lines of code for multithread environment.

            Thread t1 = new Thread(increasecounter1);
            Thread t2 = new Thread(increasecounter2);

            Stopwatch s2 = new Stopwatch();
            s2.Start();
            t1.Start();
            t2.Start();
            s2.Stop();

            t1.Join();
            t2.Join();

            Console.WriteLine("Time taken using single threaded "+ s1.ElapsedMilliseconds);
            Console.WriteLine("Time taken using multi threaded " + s2.ElapsedMilliseconds);
        }
    }
}
