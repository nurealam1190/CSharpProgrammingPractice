using System;
using System.Threading;

namespace Multithreading
{
    class Program
    {

        //static void test1()
        //{ 
        //    for(int i=1; i<=500;i++)
        //    {
        //        Console.WriteLine("test1: " + i);
        //    }
        //}

        static void test1(object max1)
        {
            int max = Convert.ToInt32(max1);
            for (int i = 1; i <= max; i++)
            {
                Console.WriteLine("test1: " + i);
            }
        }
        static void test2()
        {
            for (int i = 1; i <= 500; i++)
            {
                Console.WriteLine("test2: " + i);
                if(i==50)
                {
                    Console.WriteLine("Thread 2 is going to sleep");
                    Thread.Sleep(10000);
                }
            }
        }
        static void test3()
        {
            for(int i = 1; i <= 500; i++)
            {
                Console.WriteLine("test3: " + i);
            }
        }
        static void Main(string[] args)
        {
            //Thread t = Thread.CurrentThread;
            //t.Name = "Main Thread";
            //Console.WriteLine("Current runing thread is " + Thread.CurrentThread.Name);

            //we can write like below 
           // ThreadStart obj = new ThreadStart(test1);

            // Above line can also be write like below two line;
            //ThreadStart obj = test1;  //OR Like below

           // ThreadStart obj = delegate () { test1(); }; // OR like below

           // ThreadStart obj = () => test1();

            //To create a delegate which hold the refrence of parameterized method then need to use below 

            ParameterizedThreadStart obj = new ParameterizedThreadStart(test1);

           Thread t1 = new Thread(obj);  // Here we are exolicitly creating object for ThreadStart() and then creating object of thread;

            //OR directly write like below.

          // Thread t1 = new Thread(test1); // here we are directly passing method name as param. so CLR will be
                        //instansitating  the object of ThreadStart() and willl pass test1 as param. It is called implicit call of ThreadStart()

           Thread t2 = new Thread(test2);
           Thread t3 = new Thread( test3);

            t1.Start(100); 
            t2.Start();

            t3.Start();

        }
    }
}
