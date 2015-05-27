using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Base
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Base_LogDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int LogID { set; get; }

        /// <summary>
        /// 程序名称
        /// </summary>
        [Display(Name = "程序名称")]
        [DataMember]
        public string AppDomainName { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 分类，以逗号分隔
        /// </summary>
        [Display(Name = "分类，以逗号分隔")]
        [DataMember]
        public string Categories { set; get; }

        /// <summary>
        /// 信息内容
        /// </summary>
        [Display(Name = "信息内容")]
        [DataMember]
        public string Messages { set; get; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [Display(Name = "事件类型")]
        [DataMember]
        public int EventType { set; get; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        [Display(Name = "扩展属性")]
        [DataMember]
        public string ExtendedProperties { set; get; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [Display(Name = "日志级别")]
        [DataMember]
        public int? Priority { set; get; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Display(Name = "操作员")]
        [DataMember]
        public string Operator { set; get; }

        /// <summary>
        /// 日志时间
        /// </summary>
        [Display(Name = "日志时间")]
        [DataMember]
        public DateTime? LogTime { set; get; }

        /// <summary>
        /// 是否通知
        /// </summary>
        [Display(Name = "是否通知")]
        [DataMember]
        public bool IsNotify { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [DataMember]
        public DateTime CreateTime { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Base_LogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_LogDC GetEntity(IDataReader reader)
        {
            Base_LogDC entity = null;

            entity = new Base_LogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
