using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.WorkQueue.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Contract.DataContract.Notify.NotifyMessageItem item = new Contract.DataContract.Notify.NotifyMessageItem();
            item.EventNumber = "001";
            item.Title = "测试title";
            item.IsSms = true;
            item.Content = "我操，二货";

            IList<int> nUserList = new List<int>();
            nUserList.Add(101);
            nUserList.Add(32);
            item.NotifyUserList = nUserList;

            LazyAtHome.Core.Helper.QueueHelper queue = new Core.Helper.QueueHelper();
            try
            {
                queue.Send(item);
                Console.WriteLine("发送完成 " + item.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
