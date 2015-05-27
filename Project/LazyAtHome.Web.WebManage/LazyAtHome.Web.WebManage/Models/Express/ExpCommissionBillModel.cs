using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 佣金列表页绑定实体
    /// </summary>
    [Serializable]
    public class ExpCommissionBillModel
    {
        /// <summary>
        /// 佣金账单信息
        /// </summary>
        public Exp_Preson_CommissionBillDC ExpCommissionBillInfo { get; set; }
    }
}