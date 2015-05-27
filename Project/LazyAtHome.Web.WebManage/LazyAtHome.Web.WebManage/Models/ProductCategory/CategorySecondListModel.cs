using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.ProductCategory
{
    /// <summary>
    /// 产品品类列表页绑定实体
    /// </summary>
    [Serializable]
    public class CategorySecondListModel
    {
        public CategorySecondSearchInfo SearchInfo;

        public int PID;//父ID
        public IList<Wash_ClassDC> CategorySecondList;
    }
}