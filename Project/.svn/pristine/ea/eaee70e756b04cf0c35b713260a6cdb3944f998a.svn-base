using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Order_Order_StatDC : EntityBase
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
        /// 渠道
        /// </summary>
        [DataMember]
        public int Channel { set; get; }

        /// <summary>
        /// 水洗条码
        /// </summary>
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string Mpno { set; get; }

        /// <summary>
        /// 收货人
        /// </summary>
        [DataMember]
        public string Consignee { set; get; }

        /// <summary>
        /// 用户地址
        /// </summary>
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 预约日期
        /// </summary>
        [DataMember]
        public DateTime? ExpectTime { set; get; }

        /// <summary>
        /// 进程
        /// </summary>
        [DataMember]
        public int? Step { set; get; }

        /// <summary>
        /// 完成订单时间
        /// </summary>
        [DataMember]
        public DateTime? CompleteTime { set; get; }

        /// <summary>
        /// 全部完成时间
        /// </summary>
        [DataMember]
        public DateTime? AllFinishTime { set; get; }

        //订单派发时间	物流取件完成时间	入库时间	出库时间

        /// <summary>
        /// 推送物流时间
        /// </summary>
        [DataMember]
        public DateTime? PushExpressTime { set; get; }

        /// <summary>
        /// 订单完成时间
        /// </summary>
        [DataMember]
        public DateTime? StepTime1 { set; get; }

        /// <summary>
        /// 物流取件完成时间
        /// </summary>
        [DataMember]
        public DateTime? StepTime3 { set; get; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [DataMember]
        public DateTime? StepTime4 { set; get; }

        /// <summary>
        /// 出库时间
        /// </summary>
        [DataMember]
        public DateTime? StepTime5 { set; get; }

        /// <summary>
        /// 全部完成时间
        /// </summary>
        [DataMember]
        public DateTime? StepTime7 { set; get; }

        [DataMember]
        public bool StepTime1Alert { set; get; }
        [DataMember]
        public bool StepTime3Alert { set; get; }
        [DataMember]
        public bool StepTime4Alert
        {
            set { }
            get
            {
                if (ExpectTime.HasValue)
                {
                    if (StepTime4.HasValue)
                    {
                        return (StepTime4.Value - ExpectTime.Value).TotalHours >= 12;
                    }
                    else
                    {
                        return (DateTime.Now - ExpectTime.Value).TotalHours >= 12;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        [DataMember]
        public bool StepTime5Alert
        {
            set { }
            get
            {
                if (StepTime4.HasValue)
                {
                    if (StepTime5.HasValue)
                    {
                        return (StepTime5.Value - StepTime4.Value).TotalHours >= 24;
                    }
                    else
                    {
                        return (DateTime.Now - StepTime4.Value).TotalHours >= 24;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        [DataMember]
        public bool StepTime7Alert
        {
            set { }
            get
            {
                if (StepTime5.HasValue)
                {
                    if (StepTime7.HasValue)
                    {
                        return (StepTime7.Value - StepTime5.Value).TotalHours >= 12;
                    }
                    else
                    {
                        return (DateTime.Now - StepTime5.Value).TotalHours >= 12;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        [DataMember]
        public bool StepTimeAllAlert
        {
            set { }
            get
            {
                if (ExpectTime.HasValue)
                {
                    if (StepTime7.HasValue)
                    {
                        return (StepTime7.Value - ExpectTime.Value).TotalHours >= 48;
                    }
                    else
                    {
                        return (DateTime.Now - ExpectTime.Value).TotalHours >= 48;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 产品列表
        /// </summary>
        [DataMember]
        public IList<Order_ProductDC> ProductList { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int OrderClass { set; get; }

        ///// <summary>
        ///// 订单类型
        ///// </summary>
        //[Display(Name = "订单类型")]
        //[DataMember]
        //public int OrderType { set; get; }

        ///// <summary>
        ///// 用户ID
        ///// </summary>
        //[Display(Name = "用户ID")]
        //[DataMember]
        //public Guid UserID { set; get; }

        ///// <summary>
        ///// 标题
        ///// </summary>
        //[Display(Name = "标题")]
        //[DataMember]
        //public string Title { set; get; }

        ///// <summary>
        ///// 订单原金额
        ///// </summary>
        //[Display(Name = "订单原金额")]
        //[DataMember]
        //public decimal SourceAmount { set; get; }

        ///// <summary>
        ///// 总金额
        ///// </summary>
        //[Display(Name = "总金额")]
        //[DataMember]
        //public decimal TotalAmount { set; get; }

        ///// <summary>
        ///// 已支付金额
        ///// </summary>
        //[Display(Name = "已支付金额")]
        //[DataMember]
        //public decimal PayAmount { set; get; }

        ///// <summary>
        ///// 支付状态
        ///// </summary>
        //[Display(Name = "支付状态")]
        //[DataMember]
        //public int PayStatus { set; get; }

        ///// <summary>
        ///// 支付类型 1:到付 2:在线支付
        ///// </summary>
        //[Display(Name = "支付类型")]
        //[DataMember]
        //public int PayType { set; get; }

        ///// <summary>
        ///// 用户备注
        ///// </summary>
        //[Display(Name = "用户备注")]
        //[DataMember]
        //public string UserRemark { set; get; }

        ///// <summary>
        ///// 预期时间
        ///// </summary>
        //[Display(Name = "预期时间")]
        //[DataMember]
        //public DateTime? ExpectTime { set; get; }

        ///// <summary>
        ///// 客服备注
        ///// </summary>
        //[Display(Name = "客服备注")]
        //[DataMember]
        //public string CSRemark { set; get; }

        ///// <summary>
        ///// 系统备注
        ///// </summary>
        //[Display(Name = "系统备注")]
        //[DataMember]
        //public string SystemRemark { set; get; }

        ///// <summary>
        ///// 关联的主单ID
        ///// </summary>
        //[Display(Name = "关联的主单ID")]
        //[DataMember]
        //public int? RelationID { set; get; }

        ///// <summary>
        ///// 关联的主单编号
        ///// </summary>
        //[Display(Name = "关联的主单编号")]
        //[DataMember]
        //public string RelationNo { set; get; }

        ///// <summary>
        ///// 订单状态
        ///// </summary>
        //[Display(Name = "订单状态")]
        //[DataMember]
        //public int OrderStatus { set; get; }

        ///// <summary>
        ///// 完成订单时间
        ///// </summary>
        //[Display(Name = "完成订单时间")]
        //[DataMember]
        //public DateTime? CompleteTime { set; get; }

        ///// <summary>
        ///// 全部完成时间
        ///// </summary>
        //[Display(Name = "全部完成时间")]
        //[DataMember]
        //public DateTime? AllFinishTime { set; get; }

        ///// <summary>
        ///// 站点
        ///// </summary>
        //[Display(Name = "站点")]
        //[DataMember]
        //public int Site { set; get; }


        ///// <summary>
        ///// 取件快递单号
        ///// </summary>
        //[Display(Name = "取件快递单号")]
        //[DataMember]
        //public string GetExpressNumber { set; get; }

        ///// <summary>
        ///// 送件快递单号
        ///// </summary>
        //[Display(Name = "送件快递单号")]
        //[DataMember]
        //public string SendExpressNumber { set; get; }

        ///// <summary>
        ///// 步骤
        ///// </summary>
        //[Display(Name = "步骤")]
        //[DataMember]
        //public int Step { set; get; }

        ///// <summary>
        ///// 快递状态
        ///// </summary>
        //[Display(Name = "快递状态")]
        //[DataMember]
        //public int ExpressStatus { set; get; }

        ///// <summary>
        ///// 推送物流时间
        ///// </summary>
        //[Display(Name = "推送物流时间")]
        //[DataMember]
        //public DateTime? PushExpressTime { set; get; }


        ///// <summary>
        ///// 
        ///// </summary>
        //[Display(Name = "用户手机")]
        //[DataMember]
        //public string MPNo { set; get; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[Display(Name = "用户名称")]
        //[DataMember]
        //public string Consignee { set; get; }



        /// <summary>
        /// 根据Reader生成Order_Order_StatDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_Order_StatDC GetEntity(IDataReader reader)
        {
            Order_Order_StatDC entity = null;

            entity = new Order_Order_StatDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
