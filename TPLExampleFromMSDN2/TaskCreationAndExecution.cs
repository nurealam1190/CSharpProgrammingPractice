using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPLExampleFromMSDN2
{
    class TaskCreationAndExecution
    {
      //  static Random rand = new Random();
      
         //One example 
        public void TaskExecution()
        {
            Task taskA = Task.Run(() => Thread.Sleep(2000));
            Console.WriteLine("taskA status: {0}", taskA.Status);
            try
            {
                taskA.Wait();
                Console.WriteLine("taskA status: {0}", taskA.Status);
            }
            catch(AggregateException)
            {
                Console.WriteLine("Exception in taskA");
            }
        }

        //Another example

        public void TaskExecution2()
        {

            // Wait on a single task with a timeout specified.
            Task taskA = Task.Run(() => Thread.Sleep(2000));
            try
            {
                taskA.Wait(1000);       // Wait for 1 second.
                bool completed = taskA.IsCompleted;
                Console.WriteLine("Task A completed: {0}, Status: {1}",
                                 completed, taskA.Status);
                if (!completed)
                    Console.WriteLine("Timed out before task A completed.");
            }
            catch (AggregateException)
            {
                Console.WriteLine("Exception in taskA.");
            }
        }

        public void TaskExecution3()
        {
            var tasks = new Task[3];
            var rand = new Random();

            for (var ctr = 0; ctr <= 2; ctr++)
                tasks[ctr] = Task.Run(() => Thread.Sleep(rand.Next(500, 3000)));

            try
            {
                //int index = Task.WaitAny(tasks);
                 Task.WaitAll(tasks);
              //  Console.WriteLine("Task #{0} completed first.\n", tasks[index].Id);
                Console.WriteLine("Status of all tasks:");
                foreach (var t in tasks)
                    Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
            }
            catch (AggregateException)
            {
                Console.WriteLine("An exception occurred.");
            }
        }

        public void TaskExecution4()
        {
            // Create a cancellation token and cancel it.
            var source1 = new CancellationTokenSource();
            var token1 = source1.Token;
            source1.Cancel();
            // Create a cancellation token for later cancellation.
            var source2 = new CancellationTokenSource();
            var token2 = source2.Token;

            // Create a series of tasks that will complete, be cancelled, 
            // timeout, or throw an exception.
            Task[] tasks = new Task[12];
            for (int i = 0; i < 12; i++)
            {
                switch (i % 4)
                {
                    // Task should run to completion.
                    case 0:
                        tasks[i] = Task.Run(() => Thread.Sleep(2000));
                        break;
                    // Task should be set to canceled state.
                    case 1:
                        tasks[i] = Task.Run(() => Thread.Sleep(2000),
                                 token1);
                        break;
                    case 2:
                        // Task should throw an exception.
                        tasks[i] = Task.Run(() => { throw new NotSupportedException(); });
                        break;
                    case 3:
                        // Task should examine cancellation token.
                        tasks[i] = Task.Run(() => {
                            Thread.Sleep(2000);
                            if (token2.IsCancellationRequested)
                                token2.ThrowIfCancellationRequested();
                            Thread.Sleep(500);
                        }, token2);
                        break;
                }
            }
            Thread.Sleep(250);
            source2.Cancel();

            try
            {
                Task.WaitAll(tasks);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("One or more exceptions occurred:");
                foreach (var ex in ae.InnerExceptions)
                    Console.WriteLine("   {0}: {1}", ex.GetType().Name, ex.Message);
            }

            Console.WriteLine("\nStatus of tasks:");
            foreach (var t in tasks)
            {
                Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
                if (t.Exception != null)
                {
                    foreach (var ex in t.Exception.InnerExceptions)
                        Console.WriteLine("      {0}: {1}", ex.GetType().Name,
                                          ex.Message);
                }
            }
        }
            
        public static void Main()
        {
            TaskCreationAndExecution te = new TaskCreationAndExecution();
            // te.TaskExecution();
            //te.TaskExecution2();
            //te.TaskExecution3();
            te.TaskExecution4();
        }
    }
}
