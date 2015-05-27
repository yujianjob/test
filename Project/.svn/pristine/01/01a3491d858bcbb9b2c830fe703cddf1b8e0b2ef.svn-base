using LazyAtHome.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.SMS
{
    internal class JZService : IService
    {
        /// <summary>
        /// 帐号
        /// </summary>
        //public const string Account = "sdk_landaojia";
        public static string Account = System.Configuration.ConfigurationManager.AppSettings["Account"];

        /// <summary>
        /// 密码
        /// </summary>
        //public const string Password = "15618543";
        public static string Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

        public int Service_SendSMS(string iMobile, string iContent, int iPriority, int iType)
        {
            var rtn = WCFInvokeHelper<JZServiceClient>.Invoke<int>(client =>
                  client.SendBatchMessage(iMobile, iContent + Global.Sign));
            if (rtn > 0)
            {
                return 1;
            }
            else
            {
                return rtn;
            }

        }

        public bool Service_Register()
        {
            return true;
        }

        public string Service_Balance()
        {
            return WCFInvokeHelper<JZServiceClient>.Invoke<int>
                      (client => client.GetUserBalance()).ToString();
        }

        public bool Service_UnRegister()
        {
            return true;
        }
    }
}
