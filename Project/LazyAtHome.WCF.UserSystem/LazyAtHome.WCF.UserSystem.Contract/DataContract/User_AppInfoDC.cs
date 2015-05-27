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
    public class User_AppInfoDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
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
        /// 手机类型
        /// </summary>
        [Display(Name = "手机类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 手机标识
        /// </summary>
        [Display(Name = "手机标识")]
        [DataMember]
        public string Flag { set; get; }

        /// <summary>
        /// 客户端版本
        /// </summary>
        [Display(Name = "客户端版本")]
        [DataMember]
        public string Version { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_AppInfoDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_AppInfoDC GetEntity(IDataReader reader)
        {
            User_AppInfoDC entity = null;

            entity = new User_AppInfoDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
