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
    public class Order_StepDC : EntityBase
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
        /// 步骤代码
        /// </summary>
        [Display(Name = "步骤代码")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 步骤说明
        /// </summary>
        [Display(Name = "步骤说明")]
        [DataMember]
        public string Content { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Order_StepDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_StepDC GetEntity(IDataReader reader)
        {
            Order_StepDC entity = null;

            entity = new Order_StepDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
