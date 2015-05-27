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
    public class User_RegisterLogDC : EntityBase
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
        /// 渠道(0：通用注册 1：门店注册 2：APP注册 3：新浪微博注册 4：微信注册 5: WEB注册 6:短信注册)
        /// </summary>
        [DataMember]
        public int Channel { set; get; }

        /// <summary>
        /// 来源ID(web时记城市)
        /// </summary>
        [DataMember]
        public string SourceID { set; get; }

        /// <summary>
        /// 邀请人ID
        /// </summary>
        [DataMember]
        public Guid? InviteUserID { set; get; }

        /// <summary>
        /// 邀请码
        /// </summary>
        [DataMember]
        public string InviteCode { set; get; }

        /// <summary>
        /// 注册IP
        /// </summary>
        [DataMember]
        public string RegisterIP { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_RegisterLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_RegisterLogDC GetEntity(IDataReader reader)
        {
            User_RegisterLogDC entity = null;

            entity = new User_RegisterLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
