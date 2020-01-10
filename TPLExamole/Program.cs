using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPLExample
{
    class Program
    {

      

        static void DoWork(int id, int sleep)
        {
            Console.WriteLine("Task {0} is begining:", id);
            Thread.Sleep(sleep);
            Console.WriteLine("Task {0} is completed:", id);
        }
        static void DoOtherWork(int id, int sleep)
        {
            Console.WriteLine("Other Task {0} is begining:", id);
            Thread.Sleep(sleep);
            Console.WriteLine("Other Task {0} is completed:", id);
        }
        static void Main(string[] args)
        {
            //----------- Example code 1--------------
           // int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Parallel.For(0, 10, i =>
             {
                 Console.WriteLine("i = "+ i);
                 Thread.Sleep(1000);

             });

            //----------- Example code 2---------------

            //    var t1 = new Task(() => DoWork(1, 1000));
            //    t1.Start();
            //    var t2 = new Task(() => DoWork(2, 1000));
            //    t2.Start();
            //    var t3 = new Task(() => DoWork(3, 1000));
            //    t3.Start();

            //above code can also be rewrite using factory

            //Task t1 = Task.Factory.StartNew(() => DoWork(1, 1000));

            //We can also do the chaining by using continueWith() available with task. below ex.
            Task t1 = Task.Factory.StartNew(() => DoWork(1, 1000)).ContinueWith((prev) => DoOtherWork(1, 1000));
            Task t2 = Task.Factory.StartNew(() => DoWork(2, 1500));
            Task t3 = Task.Factory.StartNew(() => DoWork(3, 2000));

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
          
        }
    }
}
