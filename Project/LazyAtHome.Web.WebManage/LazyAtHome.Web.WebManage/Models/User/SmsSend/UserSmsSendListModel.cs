using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;

namespace LazyAtHome.Web.WebManage.Models.User
{
    /// <summary>
    /// 用户短信下行列表页绑定实体
    /// </summary>
    [Serializable]
    public class UserSmsSendListModel
    {
        public UserSmsSendSearchInfo SearchInfo;
        public IList<Base_SmsSendDC> UserSmsSendList;
    }
}