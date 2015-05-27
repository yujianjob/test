﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.Wash2.Models
{
    public class UserModel
    {
        CartModel _Cart = new CartModel();
        public WCF.UserSystem.Contract.DataContract.User_InfoDC _User = null;

        /// <summary>
        /// 洗衣篮
        /// </summary>
        public CartModel Cart
        {
            get { return _Cart; }
            set { _Cart = value; }
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