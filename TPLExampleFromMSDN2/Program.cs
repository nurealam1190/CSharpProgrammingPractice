using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPLExampleFromMSDN2
{
    class Program
    {
        //one way of to start the task
        //public static async Task Main(string[] args)
        //{
        //    await Task.Run(() =>
        //    {
        //        int ctr = 0;
        //        for(ctr=0; ctr<100; ctr++ )
        //        {

        //        }
        //        Console.WriteLine("finished {0} loop iterations", ctr);

        //    });
        //}

        //another way to start the task
        public static void Main(string[] args)
        {

            Task t = Task.Factory.StartNew(() =>
            {
                int ctr = 0;
                for (ctr = 0; ctr < 100; ctr++)
                {

                }
                Console.WriteLine("finished {0} loop iterations", ctr);

            });
            t.Wait();
        }




    }
}
