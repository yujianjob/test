using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.WebManage.Models.Manager
{
    /// <summary>
    /// 管理员页面展示实体类
    /// </summary>
    [Serializable]
    public class ManagerModel
    {
        /// <summary>
        /// 物流订单信息
        /// </summary>
        public OperatorDC ManagerInfo { get; set; }
    }
}