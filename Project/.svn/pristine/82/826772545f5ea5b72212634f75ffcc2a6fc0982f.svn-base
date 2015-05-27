using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LazyAtHome.WCF.UserSystem.Contract.Enumerate
{
    [DataContract]
    public enum LoginType
    {
        [EnumMember]
        LoginName = 1,

        [EnumMember]
        MPNo = 2,

        [EnumMember]
        Email = 3,

        /// <summary>
        /// 只做为查询用,无法登录
        /// </summary>
        [EnumMember]
        RecommendedCode = 5,

        /// <summary>
        /// 只做为查询用,无法登录
        /// </summary>
        [EnumMember]
        Weixin = 90,
    }

    [DataContract]
    public enum UserCardType
    {
        /// <summary>
        /// 懒人卡
        /// </summary>
        [EnumMember]
        LazyCard = 1,

        /// <summary>
        /// 储值卡
        /// </summary>
        [EnumMember]
        RechargeCard = 2,
    }

    [DataContract]
    public enum OAuthType
    {
        /// <summary>
        /// 微信
        /// </summary>
        [EnumMember]
        Weixin = 1,

        //[EnumMember]
        //RechargeCard = 2,
    }

    /// <summary>
    /// 用户优惠券状态
    /// </summary>
    [DataContract]
    public enum CouponStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [EnumMember]
        Normal = 1,
        /// <summary>
        /// 已使用
        /// </summary>
        [EnumMember]
        Used = 2,
        /// <summary>
        /// 已过期
        /// </summary>
        [EnumMember]
        Expired = -1,
    }

    /// <summary>
    /// 用户等级
    /// </summary>
    [DataContract]
    public enum UserLevel
    {
        /// <summary>
        /// 正常
        /// </summary>
        [EnumMember]
        Normal = 0,

        /// <summary>
        /// 创始会员
        /// </summary>
        [EnumMember]
        CharterMembers = 100,
    }

    /// <summary>
    /// 渠道
    /// </summary>
    [DataContract]
    public enum RegChannel
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        CustomerService = -1,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        System = 0,

        /// <summary>
        /// 网站
        /// </summary>
        [EnumMember]
        Web = 1,

        /// <summary>
        /// APP
        /// </summary>
        [EnumMember]
        APP = 2,

        /// <summary>
        /// 新浪微博
        /// </summary>
        [EnumMember]
        Sina = 3,

        /// <summary>
        /// 微信
        /// </summary>
        [EnumMember]
        Weixin = 4,

        /// <summary>
        /// 短信
        /// </summary>
        [EnumMember]
        Sms = 6,

        /// <summary>
        /// 合作门店
        /// </summary>
        [EnumMember]
        PartnerStore = 90,

        /// <summary>
        /// 快递继续下单
        /// </summary>
        [EnumMember]
        Express = 91,

        /// <summary>
        /// 若海
        /// </summary>
        [EnumMember]
        Partner_Ruohai = 901,
    }
}
