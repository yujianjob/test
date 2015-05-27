using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.CodeSource
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

        /// <summary>
        /// 行政区信息
        /// </summary>
        public static IList<Base_DistrictDC> AllDistrictList
        {
            get { return (IList<Base_DistrictDC>)HttpContext.Current.Session["DistrictList"]; }
            set { HttpContext.Current.Session["DistrictList"] = value; }
        }
    }
}