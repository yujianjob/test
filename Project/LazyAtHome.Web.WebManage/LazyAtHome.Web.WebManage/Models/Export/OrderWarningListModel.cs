using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Export
{
    public class OrderWarningListModel
    {
        public OrderWarningSearchInfo SearchInfo;
        public IList<Order_Order_StatDC> OrderWarningList;
    }
}