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
    public class User_RegisterSourceDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 内部ID
        /// </summary>
        [Display(Name = "内部ID")]
        [DataMember]
        public string InternalKey { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 来源ID
        /// </summary>
        [Display(Name = "来源ID")]
        [DataMember]
        public string SourceID { set; get; }

        /// <summary>
        /// 来源内容
        /// </summary>
        [Display(Name = "来源内容")]
        [DataMember]
        public string Content { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_RegisterSourceDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_RegisterSourceDC GetEntity(IDataReader reader)
        {
            User_RegisterSourceDC entity = null;

            entity = new User_RegisterSourceDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
