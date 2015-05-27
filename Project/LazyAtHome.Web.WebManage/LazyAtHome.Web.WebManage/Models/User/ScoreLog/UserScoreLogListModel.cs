using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.User
{
    /// <summary>
    /// 用户积分日志列表页绑定实体
    /// </summary>
    [Serializable]
    public class UserScoreLogListModel
    {
        public UserScoreLogSearchInfo SearchInfo;
        public IList<User_ScoreLogDC> UserScoreLogList;
    }
}