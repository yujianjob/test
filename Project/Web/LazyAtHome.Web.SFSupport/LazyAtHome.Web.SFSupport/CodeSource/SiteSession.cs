using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.SFSupport.CodeSource
{
    public class SiteSession
    {
        /// <summary>
        /// 管理员信息
        /// </summary>
        public static OperatorDC OperatorInfo
        {
            get { return (OperatorDC)HttpContext.Current.Session["Operator"]; }
            set { HttpContext.Current.Session["Operator"] = value; }
        }
    }
}