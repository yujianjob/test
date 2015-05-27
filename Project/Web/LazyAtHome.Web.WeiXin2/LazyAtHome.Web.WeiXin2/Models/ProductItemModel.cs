using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin2.Models
{
    public class ProductItemModel
    {
        public ProductItemModel()
        { }

        public ProductItemModel(WCF.OrderSystem.Contract.DataContract.Order_ProductDC product, int count)
        {
            ProductInfo = product;
            Count = count;
        }

        public WCF.OrderSystem.Contract.DataContract.Order_ProductDC ProductInfo { get; set; }
        public int Count { get; set; }
    }
}