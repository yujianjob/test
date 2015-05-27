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
    [DataContract]
    [Serializable]
    public class Partner_Order_ExpressDC : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Display(Name = "订单编号")]
        [DataMember]
        public string OrderNumber { set; get; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Display(Name = "收货人")]
        [DataMember]
        public string UserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "用户手机")]
        [DataMember]
        public string MPNo { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 预期时间
        /// </summary>
        [Display(Name = "预期时间")]
        [DataMember]
        public DateTime? ExpectTime { set; get; }

        /// <summary>
        /// 完成订单时间
        /// </summary>
        [Display(Name = "完成订单时间")]
        [DataMember]
        public DateTime? CompleteTime { set; get; }

        /// <summary>
        /// 取件快递单号
        /// </summary>
        [Display(Name = "取件快递单号")]
        [DataMember]
        public string GetExpressNumber { set; get; }

        /// <summary>
        /// 快递状态
        /// </summary>
        [Display(Name = "快递状态")]
        [DataMember]
        public int ExpressStatus { set; get; }

        /// <summary>
        /// 根据Reader生成Partner_Order_ExpressDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Partner_Order_ExpressDC GetEntity(IDataReader reader)
        {
            Partner_Order_ExpressDC entity = null;

            entity = new Partner_Order_ExpressDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
