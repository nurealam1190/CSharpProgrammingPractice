using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadLocking
{
    class LockPriorities
    {
        static long count1, count2;

        public static void incrementcount1()
        { 
            while(true)
            {
                count1 += 1;
            }
        }

        public static void incrementcount2()
        {
            while (true)
            {
                count2 += 1;
            }
        }

        static void Main()
        {
            Thread t1 = new Thread(incrementcount1);
            Thread t2 = new Thread(incrementcount2);

            t1.Start();
            t2.Start();

            Console.WriteLine("Main thread is going to sleep.");
            Thread.Sleep(10000);
            Console.WriteLine("Main thread wokeup.");

            //t1.Abort();
            //t2.Abort();

            t1.Join();
            t2.Join();

            Console.WriteLine("Count1: " + count1);
            Console.WriteLine("Count2: " + count2);

            Console.WriteLine("Main Thread exiting.");
        }
    }
}
