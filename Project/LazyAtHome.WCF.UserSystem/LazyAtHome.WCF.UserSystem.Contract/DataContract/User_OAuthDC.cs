using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 用户第三方合作登录
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_OAuthDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 第三方合作类型
        /// </summary>
        [Display(Name = "第三方合作类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "第三方ID")]
        [DataMember]
        public string OpenID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "临时令牌")]
        [DataMember]
        public string AccessToken { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_OAuthDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_OAuthDC GetEntity(IDataReader reader)
        {
            User_OAuthDC entity = null;

            entity = new User_OAuthDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
