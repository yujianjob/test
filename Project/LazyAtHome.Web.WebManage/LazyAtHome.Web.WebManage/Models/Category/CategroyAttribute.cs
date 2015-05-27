using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Category
{
    /// <summary>
    /// 产品属性备选
    /// </summary>
    [Serializable]
    public class CategroyAttribute
    {
        /// <summary>
        /// 属性大类
        /// </summary>
        public Wash_AttributeDC ParentAttribute { set; get; }

        /// <summary>
        /// 属性小类
        /// </summary>
        public IList<WashAttribute> SubAttributeList { set; get; }
    }
}