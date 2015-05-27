using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.ProductCategory
{
    /// <summary>
    /// 产品分类列表页绑定实体
    /// </summary>
    [Serializable]
    public class CategoryFirstListModel
    {
        public CategoryFirstSearchInfo SearchInfo;
        public IList<Wash_ClassDC> CategoryFirstList;
    }
}