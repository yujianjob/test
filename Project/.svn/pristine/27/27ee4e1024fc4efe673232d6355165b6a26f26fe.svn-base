using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LazyAtHome.Core.Infrastructure.WCF;
using System.Data;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_LoginLogDC : EntityBase
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
        /// 登录类型(1:登录 2:唤醒)
        /// </summary>
        [DataMember]
        public int LogType { set; get; }

        /// <summary>
        /// 登录IP
        /// </summary>
        [DataMember]
        public string LoginIP { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_LoginLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_LoginLogDC GetEntity(IDataReader reader)
        {
            User_LoginLogDC entity = null;

            entity = new User_LoginLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
