using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MultiThreading2
{
    class ThreadLocking
    {
        public void Display()
        {
            Console.Write("Csharp is an");
            Thread.Sleep(5000);
            Console.WriteLine("object oriented language");
        }

        public void Main()
        {
            ThreadLocking th = new ThreadLocking();
            th.Display();
            th.Display();
            th.Display();
        }
    }

    
}
