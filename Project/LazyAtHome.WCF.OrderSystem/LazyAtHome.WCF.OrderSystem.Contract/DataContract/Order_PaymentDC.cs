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
    public class Order_PaymentDC : EntityBase
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
        /// 支付金额
        /// </summary>
        [Display(Name = "支付金额")]
        [DataMember]
        public decimal PayMoney { set; get; }

        /// <summary>
        /// 支付类型
        /// </summary>
        [Display(Name = "支付类型")]
        [DataMember]
        public int PayMoneyType { set; get; }

        /// <summary>
        /// 支付渠道
        /// </summary>
        [Display(Name = "支付渠道")]
        [DataMember]
        public int PayChannel { set; get; }

        /// <summary>
        /// 支付时间
        /// </summary>
        [Display(Name = "支付时间")]
        [DataMember]
        public DateTime PayDate { set; get; }

        /// <summary>
        /// 关联ID
        /// </summary>
        [Display(Name = "关联ID")]
        [DataMember]
        public string RelationID { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Order_PaymentDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_PaymentDC GetEntity(IDataReader reader)
        {
            Order_PaymentDC entity = null;

            entity = new Order_PaymentDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
