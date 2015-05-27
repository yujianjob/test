using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_AmountLogDC : EntityBase
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
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 变更前
        /// </summary>
        [Display(Name = "变更前")]
        [DataMember]
        public decimal BeforeValue { set; get; }

        /// <summary>
        /// 变更余额
        /// </summary>
        [Display(Name = "变更余额")]
        [DataMember]
        public decimal ChangeValue { set; get; }

        /// <summary>
        /// 变更后
        /// </summary>
        [Display(Name = "变更后")]
        [DataMember]
        public decimal AfterValue { set; get; }

        /// <summary>
        /// 渠道ID(订单ID)
        /// </summary>
        [Display(Name = "渠道ID(订单ID)")]
        [DataMember]
        public string SourceID { set; get; }

        #endregion Model

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        public string MPNo { set; get; }

        /// <summary>
        /// 根据Reader生成User_AmountLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_AmountLogDC GetEntity(IDataReader reader)
        {
            User_AmountLogDC entity = null;

            entity = new User_AmountLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
