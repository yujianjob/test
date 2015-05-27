using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_MailSubscriptionDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 订阅类别(1:干洗)
        /// </summary>
        [DataMember]
        public int ProjectType { set; get; }

        /// <summary>
        /// 分组ID(用户分组)
        /// </summary>
        [DataMember]
        public int GroupID { set; get; }

        /// <summary>
        /// 类型(1:帐号变动 2:账户变动)
        /// </summary>
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        [DataMember]
        public int Site { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_MailSubscriptionDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_MailSubscriptionDC GetEntity(IDataReader reader)
        {
            User_MailSubscriptionDC entity = null;

            entity = new User_MailSubscriptionDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
