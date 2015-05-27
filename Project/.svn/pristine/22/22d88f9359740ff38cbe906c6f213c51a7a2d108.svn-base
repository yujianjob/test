using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.Enumerate
{
    /// <summary>
    /// 短信优先级
    /// </summary>
    [DataContract]
    public enum Sms_Priority
    {
        /// <summary>
        /// 最慢
        /// </summary>
        [EnumMember]
        Slowest = 1,

        /// <summary>
        /// 慢
        /// </summary>
        [EnumMember]
        Slow = 2,

        /// <summary>
        /// 普通
        /// </summary>
        [EnumMember]
        Normal = 3,

        /// <summary>
        /// 快
        /// </summary>
        [EnumMember]
        Fast = 4,

        /// <summary>
        /// 最快
        /// </summary>
        [EnumMember]
        Fastest = 5,
    }

    /// <summary>
    /// 短信类型
    /// </summary>
    [DataContract]
    public enum Sms_Type
    {
        /// <summary>
        /// 验证短信
        /// </summary>
        [EnumMember]
        Verify = 1,

        /// <summary>
        /// 普通
        /// </summary>
        [EnumMember]
        Normal = 2,

        /// <summary>
        /// 合作商
        /// </summary>
        [EnumMember]
        Ad = 3,
    }

    /// <summary>
    /// 调用渠道
    /// </summary>
    [DataContract]
    public enum Sms_Source
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        CustomerService = -1,

        /// <summary>
        /// 无
        /// </summary>
        [EnumMember]
        Null = 0,

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
        /// 微信
        /// </summary>
        [EnumMember]
        Weixin = 4,
    }

    /// <summary>
    /// 发送渠道
    /// </summary>
    [DataContract]
    public enum Sms_Channel
    {
        /// <summary>
        /// 建周短信
        /// </summary>
        [EnumMember]
        JZ = 1,

        /// <summary>
        /// 亿美软通
        /// </summary>
        [EnumMember]
        YM = 2,
    }
}
