using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Express.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 物流订单页面展示实体类
    /// </summary>
    [Serializable]
    public class ExpOrderModel
    {
        /// <summary>
        /// 物流订单信息
        /// </summary>
        public Exp_OrderDC ExpOrderInfo { get; set; }

        /// <summary>
        /// 0：未分配  1：已分配
        /// </summary>
        public int Type { get; set; }
    }
}