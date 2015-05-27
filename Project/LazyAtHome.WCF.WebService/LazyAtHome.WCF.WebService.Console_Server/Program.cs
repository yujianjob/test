using LazyAtHome.WCF.WebService.Business.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Console_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "WebService";

            Console.WriteLine("开启相关服务:");

            ServiceHost CommonHost = new ServiceHost(typeof(PRPortal));
            CommonHost.Opened += CommonHost_Opened;
            CommonHost.Open();

            ServiceHost WashOrderHost = new ServiceHost(typeof(WashOrderPortal));
            WashOrderHost.Opened += WashOrderHost_Opened;
            WashOrderHost.Open();

            ServiceHost SFExpressHost = new ServiceHost(typeof(SFExpressPortal));
            SFExpressHost.Opened += SFExpressHost_Opened;
            SFExpressHost.Open();

            ServiceHost QFExpressHost = new ServiceHost(typeof(QFExpressPortal));
            QFExpressHost.Opened += QFExpressHost_Opened;
            QFExpressHost.Open();

            ServiceHost WashProductHost = new ServiceHost(typeof(WashProductPortal));
            WashProductHost.Opened += WashProductHost_Opened;
            WashProductHost.Open();

            ServiceHost InternalExpressHost = new ServiceHost(typeof(InternalExpressPortal));
            InternalExpressHost.Opened += InternalExpressHost_Opened;
            InternalExpressHost.Open();

            Console.WriteLine("开启相关服务完成");
            Console.ReadLine();

        }

        static void WashProductHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启WashProductPortal服务..");
        }

        static void SFExpressHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启SFExpressPortal服务..");
        }

        static void QFExpressHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启QFExpressPortal服务..");
        }

        static void WashOrderHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启WashOrderPortal服务..");
        }

        static void CommonHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启PRPortal服务..");
        }
        static void InternalExpressHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启InternalExpressHostPortal服务..");
        }
    }
}
