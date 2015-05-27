using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WashSite.Models.User
{
    public class UserInfo
    {
        Cart _Cart = new Cart();
        Cart _ActiveCart = new Cart();
        public WCF.UserSystem.Contract.DataContract.User_InfoDC _User = null;

        /// <summary>
        /// 购物车
        /// </summary>
        public Cart Cart
        {
            get { return _Cart; }
            set { _Cart = value; }
        }

        /// <summary>
        /// 活动购物车
        /// </summary>
        public Cart ActiveCart
        {
            get { return _ActiveCart; }
            set { _ActiveCart = value; }
        }

        public WCF.UserSystem.Contract.DataContract.User_InfoDC User
        {
            get
            {
                return _User;
            }
            set { _User = value; }
        }
        public WCF.UserSystem.Contract.DataContract.User_DetailDC UserDetail { get; set; }
    }
}