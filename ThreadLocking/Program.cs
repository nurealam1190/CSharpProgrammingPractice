using System;
using System.Threading;

namespace ThreadLocking
{
    class Program
    {
        class ThreadLock
        {

            public void Display()
            {
                lock(this)
                {
                    Console.Write("Csharp is an");
                    Thread.Sleep(5000);
                    Console.WriteLine(" object oriented language");
                }
                
            }
        }
        

        static void Main(string[] args)
        {
          //  Console.WriteLine("Hello World!");
            ThreadLock th = new ThreadLock();
            Thread t1 = new Thread(th.Display);
            Thread t2 = new Thread(th.Display);
            Thread t3 = new Thread(th.Display);
            t1.Start();
            t2.Start();
            t3.Start();
        }
    }
}
