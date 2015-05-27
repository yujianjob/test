using LazyAtHome.WCF.Common.Business.Business;
using LazyAtHome.WCF.Common.Business.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Console_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CommonService";

            CacheTimer.Init();

            Console.WriteLine("开启相关服务:");
            ServiceHost CommonHost = new ServiceHost(typeof(CommonPortal));
            CommonHost.Opened += CommonHost_Opened;
            CommonHost.Open();

            Console.WriteLine("开启相关服务完成");
            Console.ReadLine();
        }

        static void CommonHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启CommonPortal服务..");
        }
    }
}
