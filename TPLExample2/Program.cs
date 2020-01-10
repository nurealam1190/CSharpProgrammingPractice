using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace TPLExample2
{
    class Program
    {
        static void DoImportantTask(int id, int sleepTime, CancellationToken token)
        {
            if(token.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation requested");
                token.ThrowIfCancellationRequested();
            }
            Console.WriteLine("Task {0} has started", id);
            Thread.Sleep(1500);
            Console.WriteLine("Task {0} has completed", id);
        }
        static void DoAnotherImpTask(int id, int sleepTime, CancellationToken token)
        {
            if (token.IsCancellationRequested) //checking if cancellation requested.
            {
                Console.WriteLine("Cancellation requested");
                token.ThrowIfCancellationRequested(); // throwing the cancellation request details here.
            }

            Console.WriteLine("Another imp task {0} has started", id);
            Thread.Sleep(3000);
            Console.WriteLine("Another imp task {0} has completd", id);
        }
        static void Main(string[] args)
        {
            //Task t1 = new Task(() => DoImportantTask(1, 1500));
            //t1.Start();
            //Task t2 = new Task(() => DoImportantTask(2, 3000));
            //t2.Start();
            //Task t3 = new Task(() => DoImportantTask(3, 1000));
            //t3.Start();

            //var list = new List<int> { 1,2,3,4,5,6,7,8,9,11,12,13,14,15};
            // Parallel.ForEach(list, (i)=>Console.WriteLine(i)); // When we use Parallel then it has kind of iinbuilt WaitAll            
            //Parallel.For(1, 100, (i) => Console.WriteLine(i));// functionality which will not let any line execute till it finish it's execution

            var source = new CancellationTokenSource(); // This allow us to send a cancellation request and stop the execution.
            // This can be use in an scenario where we found something wrong and want to cancel the execution.
            try
            {
                var t1 = Task.Factory.StartNew(() => DoImportantTask(1, 1500, source.Token)).ContinueWith((prev) => DoAnotherImpTask(1, 2000, source.Token));
                source.Cancel();// Here we are calling to cancel the execution.
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetType());
            }
           
            
          
            var t2 = Task.Factory.StartNew(() => DoImportantTask(2, 4000, source.Token));
            var t3 = Task.Factory.StartNew(() => DoImportantTask(3, 1000, source.Token));

            Task.Run(() => DoImportantTask(1, 1000, source.Token));


            var tasklist = new List<Task> { t2, t3 };
           // Task.WaitAny(tasklist.ToArray()); //WaitAny method wait for any of the method to complete it's execution from given list;

           Task.WaitAll(tasklist.ToArray()); //WaitAll method wait for all the provided list of task to complete it's execution;
            for(int i =0; i<10;i++)
            {
                Console.WriteLine("DOing some other work");
                Thread.Sleep(1000);
                Console.WriteLine("i= {0}", i);                   

            }

       
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
