using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WashSite.Models.User
{
    /// <summary>
    /// 购物车产品对象
    /// </summary>
    public class CartProductItem
    {
        public WCF.Wash.Contract.DataContract.web.web_Wash_ProductDC ProductInfo { get; set; }

        public int Count { get; set; }
    }
}