using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin3.Models
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
        public IList<ProductItemModel> ProductList { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalPrice { get; set; }

        public int OrderStatus { get; set; }
        public int OrderType { get; set; }
        public int PayStatus { get; set; }

        public int OrderClass { get; set; }
        public DateTime CreateTime { get; set; }
    }
}