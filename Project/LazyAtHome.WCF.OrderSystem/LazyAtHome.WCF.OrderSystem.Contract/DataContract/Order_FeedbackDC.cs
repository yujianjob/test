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
    /// 订单评价表
    /// </summary>
    [DataContract]
    [Serializable]
    public class Order_FeedbackDC : EntityBase
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
        /// 快递响应
        /// </summary>
        [Display(Name = "快递响应")]
        [DataMember]
        public int Score1 { set; get; }

        /// <summary>
        /// 客服态度
        /// </summary>
        [Display(Name = "客服态度")]
        [DataMember]
        public int Score2 { set; get; }

        /// <summary>
        /// 快递态度
        /// </summary>
        [Display(Name = "快递态度")]
        [DataMember]
        public int Score3 { set; get; }

        /// <summary>
        /// 洗涤效果
        /// </summary>
        [Display(Name = "洗涤效果")]
        [DataMember]
        public int Score4 { set; get; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [Display(Name = "评论内容")]
        [DataMember]
        public string Content1 { set; get; }
      
        #endregion Model
         
        /// <summary>
        /// 根据Reader生成Order_OrderDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_FeedbackDC GetEntity(IDataReader reader)
        {
            Order_FeedbackDC entity = null;

            entity = new Order_FeedbackDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
