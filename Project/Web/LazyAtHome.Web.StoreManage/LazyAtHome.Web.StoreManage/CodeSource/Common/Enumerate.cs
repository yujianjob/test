using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.StoreManage.CodeSource.Common
{
    /// <summary>
    /// 信息类型
    /// </summary>
    [Serializable]
    public enum MessageTypeEnum
    {
        /// <summary>
        /// 一般信息
        /// </summary>
        Info,
        /// <summary>
        /// 警告信息
        /// </summary>
        Alear,
        /// <summary>
        /// 错误信息
        /// </summary>
        Error
    }

    /// <summary>
    /// 信息类型
    /// </summary>
    /// 
    [Serializable]
    public enum OperateLogTypeEnum
    {
        ProductCategory = 1001,//产品运价
        Order = 2001,//订单
        Store = 3001,//合作门店
        User = 5001,//用户
        Sms = 6001,//短信
        Manager = 9001,//账户管理
        

        StoreOrder = 2002,//合作门店订单
        StoreUser = 5002,//用户签收
        StoreManager = 9002,//B端账户管理
    }

    /// <summary>
    /// 项目编号
    /// </summary>
    public enum ProjectType
    { 
        /// <summary>
        /// 干洗
        /// </summary>
        Wash = 1
    }
}