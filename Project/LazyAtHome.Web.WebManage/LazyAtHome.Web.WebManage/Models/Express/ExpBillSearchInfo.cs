using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 账单列表查询 基类
    /// </summary>
    [Serializable]
    public class ExpBillSearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 日期段
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// 日期段
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 账单类型
        /// 0：未结算 1：部分结算 2：已结算
        /// </summary>
        public int? BillStatus { get; set; }
    }

    /// <summary>
    /// 佣金账单
    /// </summary>
    public class ExpCommissionBillSearchInfo : ExpBillSearchInfo
    { }


    /// <summary>
    /// 佣金账单
    /// </summary>
    public class ExpPaymentBillSearchInfo : ExpBillSearchInfo
    { }
}