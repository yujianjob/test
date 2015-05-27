using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Contract.Enumerate
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [DataContract]
    public enum ProductType
    {
        /// <summary>
        /// 人工
        /// </summary>
        [EnumMember]
        Manual = 0,
        /// <summary>
        /// 干洗
        /// </summary>
        [EnumMember]
        Wash = 1,
    }

    /// <summary>
    /// 收货地址类型
    /// </summary>
    [DataContract]
    public enum ConsigneeAddressType
    {
        /// <summary>
        /// 取货
        /// </summary>
        [EnumMember]
        Get = 1,
        /// <summary>
        /// 送货
        /// </summary>
        [EnumMember]
        Send = 2,
    }

    /// <summary>
    /// 订单金额变更类型
    /// </summary>
    [DataContract]
    public enum AmountType
    {
        /// <summary>
        /// 系统
        /// </summary>
        [EnumMember]
        System = 0,

        /// <summary>
        /// 快递费
        /// </summary>
        [EnumMember]
        ExpressFee = 1,

        /// <summary>
        /// 快递费减免
        /// </summary>
        [EnumMember]
        ExpressFeeReduce = 2,

        /// <summary>
        /// 活动优惠
        /// </summary>
        [EnumMember]
        Activity = 5,
        /// <summary>
        /// 人工
        /// </summary>
        [EnumMember]
        Manual = 99,
    }

    /// <summary>
    /// 支付类型
    /// </summary>
    [DataContract]
    public enum PayMoneyType
    {
        /// <summary>
        /// 到付
        /// </summary>
        [EnumMember]
        None = 0,

        /// <summary>
        /// 余额
        /// </summary>
        [EnumMember]
        Balance = 1,

        /// <summary>
        /// 懒人卡
        /// </summary>
        [EnumMember]
        LazyCard = 2,

        /// <summary>
        /// 支付宝
        /// </summary>
        [EnumMember]
        AliPay = 3,

        /// <summary>
        /// 腾讯财付通
        /// </summary>
        [EnumMember]
        TenPay = 4,

        /// <summary>
        /// 微信支付
        /// </summary>
        [EnumMember]
        Weixin = 5,

        /// <summary>
        /// 优惠券
        /// </summary>
        [EnumMember]
        Coupon = 9,
    }

    /// <summary>
    /// 支付渠道
    /// </summary>
    [DataContract]
    public enum PayMoneyChannel
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Null = 0,
        /// <summary>
        /// 网站
        /// </summary>
        [EnumMember]
        Web = 1,
        /// <summary>
        /// 微信
        /// </summary>
        [EnumMember]
        Weixin = 4,

        /// <summary>
        /// APP
        /// </summary>
        [EnumMember]
        APP = 2,

        /// <summary>
        /// 工厂
        /// </summary>
        [EnumMember]
        Factory = 92,

    }

    /// <summary>
    /// 物流类型
    /// </summary>
    [DataContract]
    public enum ExpressType
    {
        /// <summary>
        /// 取货
        /// </summary>
        [EnumMember]
        Get = 1,

        /// <summary>
        /// 送货
        /// </summary>
        [EnumMember]
        Send = 2,
    }

    /// <summary>
    /// 步骤类型
    /// </summary>
    [DataContract]
    public enum WashStepType
    {
        /// <summary>
        /// 未生效
        /// </summary>
        [EnumMember]
        Null = 0,
        /// <summary>
        /// 1.下单(下单后)(完成订单)
        /// </summary>
        [EnumMember]
        CreateOrder = 1,
        /// <summary>
        /// 2.取件中(完成支付)
        /// </summary>
        [EnumMember]
        GetPackage = 2,
        /// <summary>
        /// 3.送洗中(收到第一笔物流数据)
        /// </summary>
        [EnumMember]
        SendFactory = 3,
        /// <summary>
        /// 4.洗涤中
        /// </summary>
        [EnumMember]
        Wash = 4,
        /// <summary>
        /// 5.出库中(工厂打包)
        /// </summary>
        [EnumMember]
        Delivery = 5,
        /// <summary>
        /// 6.送件中(物流数据推送)
        /// </summary>
        [EnumMember]
        SendPackage = 6,
        /// <summary>
        /// 7.完成(物流数据推送)
        /// </summary>
        [EnumMember]
        Finish = 7,
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    [DataContract]
    public enum OrderStatus
    {
        /// <summary>
        /// 未审核订单(一键下单)
        /// </summary>
        [EnumMember]
        Unaudited = 0,
        /// <summary>
        /// 创建订单
        /// </summary>
        [EnumMember]
        Create = 1,
        /// <summary>
        /// 完成订单
        /// </summary>
        [EnumMember]
        Finish = 2,
        /// <summary>
        /// 已退单
        /// </summary>
        [EnumMember]
        ChargeBack = 6,

        /// <summary>
        /// 用户取消
        /// </summary>
        [EnumMember]
        UserChannel = 16,

        /// <summary>
        /// 系统取消
        /// </summary>
        [EnumMember]
        SystemChannel = 32,

        /// <summary>
        /// 未付款(不包括未审核)
        /// </summary>
        [EnumMember]
        UnPay = 800,

        /// <summary>
        /// 等待物流收货或者用户收货(不包括一键下单中没有产品的)
        /// </summary>
        [EnumMember]
        WaitExpress = 900,

        /// <summary>
        /// 订单未全部完成(不能作为表中状态设置,只为查询用)
        /// </summary>
        [EnumMember]
        UnFinish = 998,

        /// <summary>
        /// 订单全部完成(不能作为表中状态设置,只为查询用)
        /// </summary>
        [EnumMember]
        AllFinish = 999,

        /// <summary>
        /// 客服需处理
        /// </summary>
        [EnumMember]
        CSProcess = -100,

        /// <summary>
        /// 客服自定义
        /// </summary>
        [EnumMember]
        CSDef = -99,

        /// <summary>
        /// 网站用
        /// </summary>
        [EnumMember]
        Web = 700,
    }

    /// <summary>
    /// 支付类型
    /// </summary>
    [DataContract]
    public enum PayStatus
    {
        /// <summary>
        /// 未支付
        /// </summary>
        [EnumMember]
        UnPaid = 0,
        /// <summary>
        /// 已支付
        /// </summary>
        [EnumMember]
        Paid = 1,
    }

    /// <summary>
    /// 类型
    /// </summary>
    [DataContract]
    public enum OrderClass
    {
        /// <summary>
        /// 正常订单
        /// </summary>
        [EnumMember]
        Normal = 1,

        /// <summary>
        /// 一键下单
        /// </summary>
        [EnumMember]
        OneKey = 2,

        /// <summary>
        /// 门店订单
        /// </summary>
        [EnumMember]
        Store = 3,

        /// <summary>
        /// 奢侈品
        /// </summary>
        [EnumMember]
        Luxury = 4,

        /// <summary>
        /// 商城产品
        /// </summary>
        [EnumMember]
        Mall = 5,

    }

    /// <summary>
    /// 订单类型
    /// </summary>
    [DataContract]
    public enum OrderType
    {
        /// <summary>
        /// 普通(干洗/奢侈品/商城)
        /// </summary>
        [EnumMember]
        Normal = 1,

        /// <summary>
        /// 分单干洗
        /// </summary>
        [EnumMember]
        Part = 2,

        /// <summary>
        /// 干洗退单
        /// </summary>
        [EnumMember]
        Back = 6,

        /// <summary>
        /// 干洗返洗
        /// </summary>
        [EnumMember]
        Return = 3,

        /// <summary>
        /// 非退单
        /// </summary>
        [EnumMember]
        NoBack = 900,
    }

    /// <summary>
    /// 渠道
    /// </summary>
    [DataContract]
    public enum Channel
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
        /// 工厂
        /// </summary>
        [EnumMember]
        Factory = 92,

        /// <summary>
        /// 若海
        /// </summary>
        [EnumMember]
        Partner_Ruohai = 901,
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
    /// 签收状态
    /// </summary>
    [DataContract]
    public enum UserSignType
    {
        /// <summary>
        /// 未签收
        /// </summary>
        [EnumMember]
        UnSign = 0,

        /// <summary>
        /// 已签收
        /// </summary>
        [EnumMember]
        Sign = 1,

    }


}
