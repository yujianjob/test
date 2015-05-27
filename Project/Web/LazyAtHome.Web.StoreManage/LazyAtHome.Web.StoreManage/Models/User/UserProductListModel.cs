using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.StoreManage.Models.User
{
    /// <summary>
    /// 用户衣物列表页绑定实体
    /// </summary>
    [Serializable]
    public class UserProductListModel
    {
        public UserProductSearchInfo SearchInfo;
        public IList<Order_ProductDC> UserProductList;
    }
}