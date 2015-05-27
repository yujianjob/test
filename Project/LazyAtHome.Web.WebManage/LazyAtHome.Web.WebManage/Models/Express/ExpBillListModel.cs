using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Express.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 佣金账单列表页绑定实体
    /// </summary>
    [Serializable]
    public class ExpCommissionBillListModel
    {
        public ExpCommissionBillSearchInfo SearchInfo;
        public IList<Exp_Preson_CommissionBillDC> ExpCommissionBillList;
    }


    /// <summary>
    /// 收款账单列表页绑定实体
    /// </summary>
    [Serializable]
    public class ExpPaymentBillListModel
    {
        public ExpPaymentBillSearchInfo SearchInfo;
        public IList<Exp_Preson_PaymentBillDC> ExpPaymentBillList;
    }
}