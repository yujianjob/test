using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.SFExpress
{
    public static class Config
    {
        //public static class FactoryInfo
        //{
        //    /// <summary>
        //    /// 寄件方公司
        //    /// </summary>
        //    public const string Company = "陈英辉";

        //    /// <summary>
        //    /// 寄件方联系人
        //    /// </summary>
        //    public const string Contact = "陈英辉";

        //    /// <summary>
        //    /// 寄件方联系电话
        //    /// </summary>
        //    public const string Tel = "陈英辉电话";

        //    /// <summary>
        //    /// 寄件方地址
        //    /// </summary>
        //    public const string Address = "陈英辉地址";

        //    /// <summary>
        //    /// 寄件方省
        //    /// </summary>
        //    public const string Province = "上海";

        //    /// <summary>
        //    /// 寄件方市
        //    /// </summary>
        //    public const string City = "上海";
        //}

        /// <summary>
        /// 
        /// </summary>
        public static class Account
        {
            /// <summary>
            /// 客户卡号
            /// </summary>
            //public const string CustomerID = "SHLDJ";
            public static string CustomerID = System.Configuration.ConfigurationManager.AppSettings["SF_Account_CustomerID"];
            /// <summary>
            /// 校验码
            /// </summary>
            //public const string Check = "U;q[8D4enR8Zq3Cw";
            public static string Check = System.Configuration.ConfigurationManager.AppSettings["SF_Account_Check"];

            /// <summary>
            /// 月结卡号
            /// </summary>
            //public const string CardID = "0210481559";
            public static string CardID = System.Configuration.ConfigurationManager.AppSettings["SF_Account_CardID"];
        }

    }
}
