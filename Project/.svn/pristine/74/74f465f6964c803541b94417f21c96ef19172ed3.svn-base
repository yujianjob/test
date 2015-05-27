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
    public class User_CouponDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
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
        /// 优惠券ID
        /// </summary>
        [Display(Name = "优惠券ID")]
        [DataMember]
        public int CouponID { set; get; }

        /// <summary>
        /// 持券状态
        /// </summary>
        [Display(Name = "持券状态")]
        [DataMember]
        public int CouponStatus { set; get; }

        /// <summary>
        /// 有效期开始
        /// </summary>
        [Display(Name = "有效期开始")]
        [DataMember]
        public DateTime UseBeginDate { set; get; }

        /// <summary>
        /// 有效期结束
        /// </summary>
        [Display(Name = "有效期结束")]
        [DataMember]
        public DateTime UseEndDate { set; get; }

        /// <summary>
        /// 使用订单号
        /// </summary>
        [Display(Name = "使用订单号")]
        [DataMember]
        public string UseOrderNumber { set; get; }

        /// <summary>
        /// 使用时间
        /// </summary>
        [Display(Name = "使用时间")]
        [DataMember]
        public DateTime? UseDate { set; get; }

        #endregion Model

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 使用类别
        /// </summary>
        [Display(Name = "使用类别")]
        [DataMember]
        public int UseClass { set; get; }

        /// <summary>
        /// 使用类型
        /// </summary>
        [Display(Name = "使用类型")]
        [DataMember]
        public int UseType { set; get; }

        /// <summary>
        /// 获取数量
        /// </summary>
        [Display(Name = "获取数量")]
        [DataMember]
        public int GetCount { set; get; }

        /// <summary>
        /// 面值
        /// </summary>
        [Display(Name = "面值")]
        [DataMember]
        public decimal FaceValue { set; get; }

        /// <summary>
        /// 最低消费额
        /// </summary>
        [Display(Name = "最低消费额")]
        [DataMember]
        public decimal MinMoney { set; get; }

        /// <summary>
        /// 最高消费额
        /// </summary>
        [Display(Name = "最高消费额")]
        [DataMember]
        public decimal MaxMoney { set; get; }

        /// <summary>
        /// 总数量
        /// </summary>
        [Display(Name = "总数量")]
        [DataMember]
        public int TotalCount { set; get; }

        /// <summary>
        /// 已领取数量
        /// </summary>
        [Display(Name = "已领取数量")]
        [DataMember]
        public int SendCount { set; get; }

        /// <summary>
        /// 领取后有效天
        /// </summary>
        [Display(Name = "领取后有效天")]
        [DataMember]
        public int ValidDay { set; get; }

        /// <summary>
        /// 用户手机
        /// </summary>
        [Display(Name = "用户手机")]
        [DataMember]
        public string MPNo { set; get; }

        /// <summary>
        /// 优惠券限制品类
        /// </summary>
        [DataMember]
        public IList<Base_CouponProductDC> CouponProductList { set; get; }

        /// <summary>
        /// 根据Reader生成User_CouponDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_CouponDC GetEntity(IDataReader reader)
        {
            User_CouponDC entity = null;

            entity = new User_CouponDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
