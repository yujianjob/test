using LazyAtHome.WCF.UserSystem.Business.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Console_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "UserService";

            String s;
            StringComparer sc;

            StringBuilder sb;
            

            //var t = LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_BindMPNo(
            //    "ontm8t1tHVVoy1zp0kwH5XKNpeuw", "13621914832", "13621914832", 1, null, null);
            //var t1 = LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_Bind_Part("ontm8t-vzvWPmr_6CsKJoP4wmN9", "13000000031", null, 1, null);

            //var t = LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_Create("wx18616940990", 1, null, null);

            //var t1 = LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_Login("wx18616940990", "18616940990");

            //var t2 = LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_Login("wx18616940990", "18616940990", "123456");

            //var t3 = LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_Reg("wx18616940990", "18616940990", "123456", "a@b.com");

            //var t = "APP12345678901120140501";

            //t = LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(t);

            //t = t.ToUpper();

            //Console.WriteLine(t);

            //t += "lz123456789012345678901234567890";

            //t = LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(t);

            //t = t.ToUpper();

            //Console.WriteLine(t);

            //LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_BindMPNo("111122222", "15026466156", "15026466156", 1, "123", null);
            //LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.wx_User_Weixin_BindMPNo(
            //    "ontm8twl8n_3ksx6Ia55KUHM_6y0", "15201789719", "15201789719", 1, null, null);
            //LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.User_WeixinAttentionLog_Stat("111111", DateTime.Parse("2014/7/1 0:00:00"), DateTime.Parse("2014/7/31 0:00:00"));
            //LazyAtHome.WCF.UserSystem.Business.Business.Extend eInstance = LazyAtHome.WCF.UserSystem.Business.Business.Extend.Instance;
            //eInstance.wx_User_Weixin_BindMPNo(
            //    "wx9ab0f092bce2810D", "18616950990", "18616950990", 1, null);
            //eInstance.User_Card_Bind_Check(Contract.Enumerate.UserCardType.LazyCard, "000000001");
            //eInstance.wx_User_Card_Bind("wx9ab0f092bce2810D", Contract.Enumerate.UserCardType.LazyCard, "000000001");
            // eInstance.wx_User_Weixin_SELECT("wx9ab0f092bce2810D");
            //LazyAtHome.WCF.UserSystem.Business.Business.Base.Instance.User_Reg_Web("guominjie2", Contract.Enumerate.LoginType.LoginName,
            //    "123456", null, "116.231.189.93", 1);
            //LazyAtHome.WCF.UserSystem.Business.Business.User.Instance.User_Coupon_Bind(new Guid("95DD470A-5EE6-485F-ACF6-0F07E774DD07"), "ASRY1V");
            //LazyAtHome.WCF.UserSystem.Business.Business.Base.Instance.web_User_Detail_UPDATE(new Guid("95DD470A-5EE6-485F-ACF6-0F07E774DD07")
            //, "guominjie4"
            //, 1
            //, "18616940991"
            //, "g@1.com"
            //, "guom"
            //, null
            //, null
            //, "123123123123"
            //    );

            Console.WriteLine("开启相关服务:");
            ServiceHost serviceHost = new ServiceHost(typeof(UserPortal));
            serviceHost.Opened += serviceHost_Opened;
            serviceHost.Open();

            Console.WriteLine("开启相关服务完成");

            Console.ReadLine();

        }

        static void serviceHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启UserPortal服务..");
        }
    }
}
