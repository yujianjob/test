using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using System.Web.Mvc;

namespace LazyAtHome.Web.WebManage.Models.User
{
    /// <summary>
    /// 用户详细
    /// </summary>
    [Serializable]
    public class UserModel
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public User_InfoDC UserInfo { get; set; }

        /// <summary>
        /// 用户详细信息
        /// </summary>
        public User_DetailDC UserDetailInfo { get; set; }

        /// <summary>
        /// 用户收获地址
        /// </summary>
        public IList<User_ConsigneeAddressDC> User_ConsigneeAddressList { get; set; }

        /// <summary>
        /// 行政区域
        /// </summary>
        public IList<Base_DistrictDC> AllDistrictList { get; set; }


        /// <summary>
        /// 已绑优惠券
        /// </summary>
        public IList<User_CouponDC> User_CouponList { get; set; }

        //public IList<SelectListItem> C_DistrictList { get; set; }
        //public IList<SelectListItem> P_DistrictList { get; set; }
        //public IList<SelectListItem> D_DistrictList { get; set; }

        //public int DID = 0;
        //public int PID = 0;
        //public int CID = 0;
    }
}