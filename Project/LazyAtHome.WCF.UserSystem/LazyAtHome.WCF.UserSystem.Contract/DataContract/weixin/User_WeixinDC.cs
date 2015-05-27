using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_WeixinDC : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public User_InfoDC UserInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public User_DetailDC UserDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public User_ConsigneeAddressDC iConsigneeAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public IList<User_CardDC> CardList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public IList<User_CouponDC> CouponList { get; set; }
    }
}
