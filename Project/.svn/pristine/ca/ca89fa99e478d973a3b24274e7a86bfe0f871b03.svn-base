using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_InfoDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public Guid ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "编号")]
        [DataMember]
        public int SeedID { set; get; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录名")]
        [DataMember]
        public string LoginName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber)]
        public string MPNo { set; get; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [DataMember]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string Email { set; get; }

        ///// <summary>
        ///// 微信ID
        ///// </summary>
        //[Display(Name = "微信ID")]
        //[DataMember]
        //public string WeiXinID { set; get; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        [DataMember]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string LoginPassword { set; get; }

        /// <summary>
        /// 支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [DataMember]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string PayPassword { set; get; }

        /// <summary>
        /// 类型(1:个人用户)
        /// </summary>
        [Display(Name = "用户类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 会员等级
        /// </summary>
        [Display(Name = "会员等级")]
        [DataMember]
        public int Level { set; get; }

        /// <summary>
        /// 站点(注册时的城市,可修改)
        /// </summary>
        [Display(Name = "注册站点")]
        [DataMember]
        public int? Site { set; get; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Display(Name = "最后登录时间")]
        [DataMember]
        public DateTime LastLoginTime { set; get; }

        /// <summary>
        /// 成长值
        /// </summary>
        [Display(Name = "成长值")]
        [DataMember]
        public int Growth { set; get; }

        /// <summary>
        /// 余额
        /// </summary>
        [Display(Name = "余额")]
        [DataMember]
        public decimal Money { set; get; }

        /// <summary>
        /// 积分
        /// </summary>
        [Display(Name = "积分")]
        [DataMember]
        public int Score { set; get; }

        /// <summary>
        /// 交易冻结状态(1:正常,冻结)
        /// </summary>
        [Display(Name = "交易冻结状态")]
        [DataMember]
        public int AccountStatus { set; get; }

        /// <summary>
        /// 用户状态(1:正常 6:注销 2:锁定)
        /// </summary>
        [Display(Name = "用户状态")]
        [DataMember]
        public int UserStatus { set; get; }

        /// <summary>
        /// 注册来源(1:网站)
        /// </summary>
        [Display(Name = "注册渠道")]
        [DataMember]
        public int RegistSource { set; get; }

        /// <summary>
        /// 推荐码(发给别人用)
        /// </summary>
        [Display(Name = "推荐码")]
        [DataMember]
        public string RecommendedCode { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_InfoDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_InfoDC GetEntity(IDataReader reader)
        {
            User_InfoDC entity = null;

            entity = new User_InfoDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
