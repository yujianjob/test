using LazyAtHome.WCF.UserSystem.Contract.DataContract;

namespace LazyAtHome.Web.Site.Areas.Member.Models
{
    public class UserDetailViewModel
    {
        /// <summary>
        /// 用户基础信息
        /// </summary>
        public User_InfoDC UserInfo { get; set; }
        /// <summary>
        /// 用户详细信息
        /// </summary>
        public User_DetailDC UserDetail { get; set; }
    }
}