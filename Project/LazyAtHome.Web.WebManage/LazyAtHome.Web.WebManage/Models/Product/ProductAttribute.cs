using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Product
{
    /// <summary>
    /// 运价属性备选
    /// </summary>
    [Serializable]
    public class ProductAttribute
    {

        /// <summary>
        /// 大类信息
        /// </summary>
        public Wash_AttributeDC ParentAttribute { set; get; }

        /// <summary>
        /// 属性小类
        /// </summary>
        public IList<WashProductAttribute> SubAttributeList { set; get; }
    }

    ///// <summary>
    ///// 运价属性备选列表
    ///// </summary>
    //[Serializable]
    //public class ProductAttributeList
    //{
    //    public IList<ProductAttribute> ProductAttributeListSelect { set; get; }
    //}
}