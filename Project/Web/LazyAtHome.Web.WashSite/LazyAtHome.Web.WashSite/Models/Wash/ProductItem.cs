using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WashSite.Models.Wash
{
    public class ProductItem
    {
        public ProductItem()
        { }

        public ProductItem(WCF.OrderSystem.Contract.DataContract.Order_ProductDC product, int count)
        {
            ProductInfo = product;
            Count = count;
        }

        public WCF.OrderSystem.Contract.DataContract.Order_ProductDC ProductInfo { get; set; }
        public int Count { get; set; }
    }
}