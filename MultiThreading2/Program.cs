using System;
using System.Threading;

namespace MultiThreading2
{
    class Program
    { 

        static void test1()
        {
            Console.WriteLine("Thread 1 started");
            for (int i = 1; i<=70; i++)
            {
                Console.WriteLine("test1: "+i);
            }
            Console.WriteLine("Thread 1 ending");

        }
        static void test2()
        {
            Console.WriteLine("Thread 2 started");
            for (int i = 1; i <= 70; i++)
            {
                Console.WriteLine("test2: " + i);
            }
            Thread.Sleep(5000);
            Console.WriteLine("Thread 2 ending");
        }
        static void test3()
        {
            Console.WriteLine("Thread 3 started");
            for (int i = 1; i <= 70; i++)
            {
                Console.WriteLine("test3: " + i);
            }
            Console.WriteLine("Thread 3 ending");
        }
    
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread started");

            Thread t1 = new Thread(test1);
            Thread t2 = new Thread(test2);
            Thread t3 = new Thread(test3);

            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();t2.Join();t3.Join();
            Console.WriteLine("Main Thread Exiting");
        }
    }
}
