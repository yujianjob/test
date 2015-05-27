using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.User
{
    /// <summary>
    /// 用户余额日志列表页绑定实体
    /// </summary>
    [Serializable]
    public class UserAmountLogListModel
    {
        public UserAmountLogSearchInfo SearchInfo;
        public IList<User_AmountLogDC> UserScoreLogList;
    }
}