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
    public class Order_OrderDC : EntityBase
    {
        #region Model
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
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int OrderClass { set; get; }

        /// <summary>
        /// 订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [DataMember]
        public int OrderType { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 订单原金额
        /// </summary>
        [Display(Name = "订单原金额")]
        [DataMember]
        public decimal SourceAmount { set; get; }

        /// <summary>
        /// 总金额
        /// </summary>
        [Display(Name = "总金额")]
        [DataMember]
        public decimal TotalAmount { set; get; }

        /// <summary>
        /// 已支付金额
        /// </summary>
        [Display(Name = "已支付金额")]
        [DataMember]
        public decimal PayAmount { set; get; }

        /// <summary>
        /// 支付状态
        /// </summary>
        [Display(Name = "支付状态")]
        [DataMember]
        public int PayStatus { set; get; }

        /// <summary>
        /// 支付类型  0到付 1余额 2懒人卡 3支付宝4 腾讯财付通 5微信支付 9优惠券
        /// </summary>
        [Display(Name = "支付类型")]
        [DataMember]
        public int PayType { set; get; }

        /// <summary>
        /// 用户备注
        /// </summary>
        [Display(Name = "用户备注")]
        [DataMember]
        public string UserRemark { set; get; }

        /// <summary>
        /// 预期时间
        /// </summary>
        [Display(Name = "预期时间")]
        [DataMember]
        public DateTime? ExpectTime { set; get; }

        /// <summary>
        /// 客服备注
        /// </summary>
        [Display(Name = "客服备注")]
        [DataMember]
        public string CSRemark { set; get; }

        /// <summary>
        /// 系统备注
        /// </summary>
        [Display(Name = "系统备注")]
        [DataMember]
        public string SystemRemark { set; get; }

        /// <summary>
        /// 关联的主单ID
        /// </summary>
        [Display(Name = "关联的主单ID")]
        [DataMember]
        public int? RelationID { set; get; }

        /// <summary>
        /// 关联的主单编号
        /// </summary>
        [Display(Name = "关联的主单编号")]
        [DataMember]
        public string RelationNo { set; get; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [Display(Name = "订单状态")]
        [DataMember]
        public int OrderStatus { set; get; }

        /// <summary>
        /// 完成订单时间
        /// </summary>
        [Display(Name = "完成订单时间")]
        [DataMember]
        public DateTime? CompleteTime { set; get; }

        /// <summary>
        /// 全部完成时间
        /// </summary>
        [Display(Name = "全部完成时间")]
        [DataMember]
        public DateTime? AllFinishTime { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        [Display(Name = "站点")]
        [DataMember]
        public int Site { set; get; }

        /// <summary>
        /// 渠道
        /// </summary>
        [Display(Name = "渠道")]
        [DataMember]
        public int Channel { set; get; }

        /// <summary>
        /// 取件快递单号
        /// </summary>
        [Display(Name = "取件快递单号")]
        [DataMember]
        public string GetExpressNumber { set; get; }

        /// <summary>
        /// 取件快递类型
        /// </summary>
        [Display(Name = "取件快递类型")]
        [DataMember]
        public int GetExpressType { set; get; }

        /// <summary>
        /// 送件快递单号
        /// </summary>
        [Display(Name = "送件快递单号")]
        [DataMember]
        public string SendExpressNumber { set; get; }

        /// <summary>
        /// 步骤
        /// </summary>
        [Display(Name = "步骤")]
        [DataMember]
        public int Step { set; get; }

        /// <summary>
        /// 步骤1时间
        /// </summary>
        [Display(Name = "步骤1时间")]
        [DataMember]
        public DateTime? Step1Time { set; get; }

        /// <summary>
        /// 步骤2时间
        /// </summary>
        [Display(Name = "步骤2时间")]
        [DataMember]
        public DateTime? Step2Time { set; get; }

        /// <summary>
        /// 步骤3时间
        /// </summary>
        [Display(Name = "步骤3时间")]
        [DataMember]
        public DateTime? Step3Time { set; get; }

        /// <summary>
        /// 步骤4时间
        /// </summary>
        [Display(Name = "步骤4时间")]
        [DataMember]
        public DateTime? Step4Time { set; get; }

        /// <summary>
        /// 步骤5时间
        /// </summary>
        [Display(Name = "步骤5时间")]
        [DataMember]
        public DateTime? Step5Time { set; get; }

        /// <summary>
        /// 步骤6时间
        /// </summary>
        [Display(Name = "步骤6时间")]
        [DataMember]
        public DateTime? Step6Time { set; get; }

        /// <summary>
        /// 步骤7时间
        /// </summary>
        [Display(Name = "步骤7时间")]
        [DataMember]
        public DateTime? Step7Time { set; get; }

        /// <summary>
        /// 快递状态
        /// </summary>
        [Display(Name = "快递状态")]
        [DataMember]
        public int ExpressStatus { set; get; }

        /// <summary>
        /// 推送物流时间
        /// </summary>
        [Display(Name = "推送物流时间")]
        [DataMember]
        public DateTime? PushExpressTime { set; get; }

        /// <summary>
        /// 面单备注
        /// </summary>
        [Display(Name = "面单备注")]
        [DataMember]
        public string PrintRemark { set; get; }

        /// <summary>
        /// 送货类型 1:全单配送 2:可先分送
        /// </summary>
        [Display(Name = "送货类型")]
        [DataMember]
        public int SendType { set; get; }

        /// <summary>
        /// 邀请码
        /// </summary>
        [Display(Name = "邀请码")]
        [DataMember]
        public string InviteCode { set; get; }

        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "用户手机")]
        [DataMember]
        public string MPNo { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "用户名称")]
        [DataMember]
        public string Consignee { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "用户地址")]
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 产品列表
        /// </summary>
        [DataMember]
        public IList<Order_ProductDC> ProductList { get; set; }

        /// <summary>
        /// 取货地址列表
        /// </summary>
        [DataMember]
        public Order_ConsigneeAddressDC GetAddress { get; set; }

        /// <summary>
        /// 送货
        /// </summary>
        [DataMember]
        public Order_ConsigneeAddressDC SendAddress { get; set; }

        /// <summary>
        /// 支付列表
        /// </summary>
        [DataMember]
        public IList<Order_PaymentDC> PaymentList { get; set; }

        /// <summary>
        /// 金额列表
        /// </summary>
        [DataMember]
        public IList<Order_AmountDC> AmountList { get; set; }

        /// <summary>
        /// 物流列表
        /// </summary>
        [DataMember]
        public IList<Order_ExpressDC> ExpressList { get; set; }

        /// <summary>
        /// 步骤列表
        /// </summary>
        [DataMember]
        public IList<Order_StepDC> StepList { get; set; }

        /// <summary>
        /// 评价列表
        /// </summary>
        [DataMember]
        public Order_FeedbackDC Feedback { get; set; }

        /// <summary>
        /// 客服建议操作
        /// </summary>
        public string CSSuggest
        {
            get
            {
                if (this.OrderClass == 2 && this.OrderStatus == 0)
                {
                    //一键订单未审核;
                    return "审核订单";
                }
                else if ((this.OrderClass == 1 || this.OrderClass == 2 || this.OrderClass == 4) && this.OrderStatus == 2 && this.ExpressStatus == 0)
                {
                    return "快递推送失败,请重新推送或人工处理";
                }
                //else if (this.OrderClass == 2 && this.OrderStatus == 2 && string.IsNullOrWhiteSpace(this.GetExpressNumber))
                //{
                //    return "快递推送失败,请重新推送或人工处理";
                //}
                return null;
            }
        }

        /// <summary>
        /// 根据Reader生成Order_OrderDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_OrderDC GetEntity(IDataReader reader)
        {
            Order_OrderDC entity = null;

            entity = new Order_OrderDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
