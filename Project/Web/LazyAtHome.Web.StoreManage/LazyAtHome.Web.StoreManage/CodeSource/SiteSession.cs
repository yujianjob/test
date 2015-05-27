using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;
//using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.StoreManage.CodeSource
{
    public class SiteSession
    {
        /// <summary>
        /// 管理员信息
        /// </summary>
        public static Wash_StoreLoginInfoDC OperatorInfo
        {
            get { return (Wash_StoreLoginInfoDC)HttpContext.Current.Session["Operator"]; }
            set { HttpContext.Current.Session["Operator"] = value; }
        }

        /// <summary>
        /// 密码过于简单
        /// </summary>
        public static bool InitPassword
        {
            get { return (bool)HttpContext.Current.Session["InitPassword"]; }
            set { HttpContext.Current.Session["InitPassword"] = value; }
        }

        ///// <summary>
        ///// 当前的订单号
        ///// </summary>
        //public static object CurrOrderNumber
        //{
        //    get { return HttpContext.Current.Session["CurrOrderNumber"]; }
        //    set { HttpContext.Current.Session["CurrOrderNumber"] = value; }
        //}
    }
}