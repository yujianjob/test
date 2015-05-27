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
    public class Order_ExpressDC : EntityBase
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
        /// 物流类型
        /// </summary>
        [Display(Name = "物流类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 物流编号
        /// </summary>
        [Display(Name = "物流编号")]
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 物流数据
        /// </summary>
        [Display(Name = "物流数据")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 物流第三方ID
        /// </summary>
        [Display(Name = "物流第三方ID")]
        [DataMember]
        public string RelationID { set; get; }

        /// <summary>
        /// 物流发生时间
        /// </summary>
        [Display(Name = "物流发生时间")]
        [DataMember]
        public DateTime? AcceptTime { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Order_ExpressDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_ExpressDC GetEntity(IDataReader reader)
        {
            Order_ExpressDC entity = null;

            entity = new Order_ExpressDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
