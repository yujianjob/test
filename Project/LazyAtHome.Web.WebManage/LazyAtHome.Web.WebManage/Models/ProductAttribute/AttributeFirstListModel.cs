using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.ProductAttribute
{
    /// <summary>
    /// 产品属性列表页绑定实体
    /// </summary>
    [Serializable]
    public class AttributeFirstListModel
    {
        public AttributeFirstSearchInfo SearchInfo;
        public IList<Wash_AttributeDC> AttributeFirstList;
    }
}