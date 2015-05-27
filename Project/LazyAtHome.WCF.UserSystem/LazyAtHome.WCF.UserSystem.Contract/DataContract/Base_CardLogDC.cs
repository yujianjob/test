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
    public class Base_CardLogDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "ID")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Display(Name = "日志类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 卡ID
        /// </summary>
        [Display(Name = "卡ID")]
        [DataMember]
        public int CardID { set; get; }

        /// <summary>
        /// 卡号
        /// </summary>
        [Display(Name = "卡号")]
        [DataMember]
        public string CardNumber { set; get; }

        /// <summary>
        /// 之前余额
        /// </summary>
        [Display(Name = "之前余额")]
        [DataMember]
        public decimal BeforeBalance { set; get; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [Display(Name = "交易金额")]
        [DataMember]
        public decimal Money { set; get; }

        /// <summary>
        /// 交易内容
        /// </summary>
        [Display(Name = "交易内容")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 相关编号
        /// </summary>
        [Display(Name = "相关编号")]
        [DataMember]
        public string SourceID { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Base_CardLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_CardLogDC GetEntity(IDataReader reader)
        {
            Base_CardLogDC entity = null;

            entity = new Base_CardLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
