using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.OrderSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Order_AmountDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 订单表ID
        /// </summary>
        [Display(Name = "订单表ID")]
        [DataMember]
        public int OID { set; get; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        [DataMember]
        public decimal Money { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 关联ID
        /// </summary>
        [Display(Name = "关联ID")]
        [DataMember]
        public string RelationID { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Order_AmountDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_AmountDC GetEntity(IDataReader reader)
        {
            Order_AmountDC entity = null;

            entity = new Order_AmountDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
