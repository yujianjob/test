using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Category
{
    /// <summary>
    /// 产品列表页绑定实体
    /// </summary>
    [Serializable]
    public class CategoryListModel
    {
        public CategorySearchInfo SearchInfo;
        public IList<Wash_CategoryDC> CategoryList;
    }
}