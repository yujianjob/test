using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin.Models.ViewModel
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 产品列表
        /// </summary>
        public IList<Models.ShopCart.ProductItem> ProductList { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}