using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin.Models.ShopCart
{
    public class ProductItem
    {
        public ProductItem()
        { }

        public ProductItem(WCF.Wash.Contract.DataContract.weixin.wx_Wash_ProductDC product, int count)
        {
            ProductInfo = product;
            Count = count;
        }

        public WCF.Wash.Contract.DataContract.weixin.wx_Wash_ProductDC ProductInfo { get; set; }
        public int Count { get; set; }
    }
}