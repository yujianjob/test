using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin3.Models
{
    public class UserModel
    {
        CartModel _Cart = new CartModel();
        RHModel _RHCart = new RHModel();
        public WCF.UserSystem.Contract.DataContract.weixin.User_WeixinDC _User = null;

        /// <summary>
        /// 洗衣篮
        /// </summary>
        public CartModel Cart
        {
            get { return _Cart; }
            set { _Cart = value; }
        }

        /// <summary>
        /// 若海洗衣篮
        /// </summary>
        public RHModel RHCart
        {
            get { return _RHCart; }
            set { _RHCart = value; }
        }

        public WCF.UserSystem.Contract.DataContract.weixin.User_WeixinDC User
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