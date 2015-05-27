using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Models.Common
{
    /// <summary>
    /// 消息通知页绑定实体
    /// </summary>
    [Serializable]
    public class NotifyListModel
    {
        public NotifySearchInfo SearchInfo;
        public IList<Base_NotifyDC> NotifyList;
    }
}