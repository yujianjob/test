using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_WeixinAttentionLogDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 第三方ID
        /// </summary>
        [Display(Name = "第三方ID")]
        [DataMember]
        public string OpenID { set; get; }

        /// <summary>
        /// 关注时间
        /// </summary>
        [Display(Name = "关注时间")]
        [DataMember]
        public DateTime AttentionTime { set; get; }

        /// <summary>
        /// 来源
        /// </summary>
        [Display(Name = "来源")]
        [DataMember]
        public string Source { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_WeixinAttentionLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_WeixinAttentionLogDC GetEntity(IDataReader reader)
        {
            User_WeixinAttentionLogDC entity = null;

            entity = new User_WeixinAttentionLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
