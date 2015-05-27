using LazyAtHome.WCF.Wash.Business.Business;
using LazyAtHome.WCF.Wash.Business.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Console_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //var rtn = LazyAtHome.WCF.Wash.Business.Business.web_Product.Instance.web_Wash_Category_SELECT_Search("xi", 1, 1, 10);
            //Console.WriteLine(rtn);

            Console.Title = "WashService";
            CacheTimer.Init();
            Console.WriteLine("开启相关服务:");

            ServiceHost serviceHost = new ServiceHost(typeof(WashPortal));
            serviceHost.Opened += serviceHost_Opened;
            serviceHost.Open();

            Console.WriteLine("开启相关服务完成");

            Console.ReadLine();
        }
        static void serviceHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启WashPortal服务..");
        }
    }
}
