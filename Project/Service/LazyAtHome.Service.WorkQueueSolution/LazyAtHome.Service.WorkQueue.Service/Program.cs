using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LazyAtHome.Service.WorkQueue.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "WorkQueue-ExpressNotify";

            WorkQueueTask_ExpressNotify expressNotifyTaskServer = new WorkQueueTask_ExpressNotify();
            Thread expressNotifyThread = new Thread(new ThreadStart(expressNotifyTaskServer.Serve));
            expressNotifyThread.IsBackground = true;
            expressNotifyThread.Start();

            WorkQueueTask_OrderFinishProcess orderFinishTaskServer = new WorkQueueTask_OrderFinishProcess();
            orderFinishTaskServer.SleepTime = 60000;
            Thread ofThread = new Thread(new ThreadStart(orderFinishTaskServer.Serve));
            ofThread.IsBackground = true;
            ofThread.Start();


            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}
