using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WeiXin.App_Code.Proxy
{
    public class OrderProxy
    {
        public void aaa()
        {
            OrderClient o;
        }

        public static ReturnEntity<Order_OrderDC> Order_Weixin_Create(string iOpenid, int iSite, IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount, decimal iUserMoney, IDictionary<int, decimal> iLazyCardList, Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, decimal TotalAmount, IDictionary<string, decimal> iActivityList, IList<int> iCouponIDList = null)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Weixin_Create(iOpenid, iSite, iProductIDList, iExpressFee, iExpressFeeDiscount, iUserMoney, iLazyCardList, iGetAddress, iSendAddress, TotalAmount, iActivityList, iCouponIDList));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<Order_OrderDC>> Select_UserOrder(string iOpenid, OrderStatus? iOrderStatus, int iPageIndex = 1, int iPageSize = 5)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>(c => c.Proxy.Order_Order_SELECT_List(iOpenid, iOrderStatus, iPageIndex, iPageSize));
            return rtn;
        }

        /// <summary>
        /// 一键下单
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="site"></param>
        /// <param name="getAddress"></param>
        /// <param name="sendAddress"></param>
        /// <param name="exceptTime"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> Update_UserOrderOneKeyAdd(string openid, Order_ConsigneeAddressDC getAddress, Order_ConsigneeAddressDC sendAddress, DateTime? exceptTime)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Onekey_Create(openid, 1, Channel.Weixin, getAddress, sendAddress, exceptTime));
            return rtn;
        }

        /// <summary>
        /// 变更订单状态
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Update_UserOrderCancel(int iID, OrderStatus iOrderStatus)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.Order_Order_Cancel(iID, iOrderStatus));
            return rtn;
        }

        public static ReturnEntity<bool> Update_OrderPay(string iOrderNumber, decimal iPayMoney, PayMoneyType iPayType, PayMoneyChannel iPayChannel, DateTime iPayDate, string iPayRelationID)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.Order_Order_Pay(iOrderNumber, iPayMoney, iPayType, iPayChannel, iPayDate, iPayRelationID));
            return rtn;
        }

        public static ReturnEntity<Order_OrderDC> Select_UserOrderDetail(string iOrderNumber, bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress, bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Order_SELECT_Entity(iOrderNumber, iGetProduct, iGetAmount, iGetConsigneeAddress, iGetExpress, iGetPayment, iGetStep));
            return rtn;
        }
    }
}