using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WeiXin2.App_Code.Proxy
{
    public class OrderProxy
    {
        public static ReturnEntity<Order_OrderDC> Order_Weixin_Create(string iOpenid, int iSite, IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount, decimal iUserMoney, IDictionary<int, decimal> iLazyCardList, Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, decimal TotalAmount, IDictionary<string, decimal> iActivityList, IList<int> iCouponIDList = null)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Weixin_Create(iOpenid, iSite, iProductIDList, iExpressFee, iExpressFeeDiscount, iUserMoney, iLazyCardList, iGetAddress, iSendAddress, TotalAmount, iActivityList, iCouponIDList));
            return rtn;
        }

        public static ReturnEntity<Order_OrderDC> Order_Create_Common(Guid iUserID, int iSite, IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount, decimal iUserMoney, IDictionary<int, decimal> iLazyCardList, Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, decimal TotalAmount, IDictionary<string, decimal> iActivityList = null, IList<int> iCouponIDList = null, bool iSalesPrice = true)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Web_Create(iUserID, iSite, iProductIDList, iExpressFee, iExpressFeeDiscount, iUserMoney, iLazyCardList, iGetAddress, iSendAddress, TotalAmount, iActivityList, iCouponIDList, iSalesPrice));
            return rtn;
        }

        public static ReturnEntity<Order_OrderDC> Select_OrderEntity(string iOrderNumber, bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress, bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Order_SELECT_Entity(iOrderNumber, iGetProduct, iGetAmount, iGetConsigneeAddress, iGetExpress, iGetPayment, iGetStep));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<Order_OrderDC>> Select_OrderList(Guid iUserID, OrderStatus? iOrderStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>(c => c.Proxy.Order_Order_SELECT_List(iUserID, iOrderStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_SELECT_List(Guid iUserID, OrderStatus? iOrderStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>(c => c.Proxy.Order_Order_SELECT_List(iUserID, iOrderStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<bool> Update_OrderPay(string iOrderNumber, decimal iPayMoney, PayMoneyType iPayType, PayMoneyChannel iPayChannel, DateTime iPayDate, string iPayRelationID)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.Order_Order_Pay(iOrderNumber, iPayMoney, iPayType, iPayChannel, iPayDate, iPayRelationID));
            return rtn;
        }

        public static ReturnEntity<Order_OrderDC> Order_MallCreate(Guid iUserID, int iSite, IDictionary<int, int> iProductList, decimal iExpressFee, decimal iExpressFeeDiscount, Order_ConsigneeAddressDC iSendAddress, decimal TotalAmount)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Mall_Web_Create(iUserID, iSite, iProductList, iExpressFee, iExpressFee, iSendAddress, TotalAmount));
            return rtn;
        }

        public static ReturnEntity<Order_OrderDC> Order_Onekey_Create(Guid iUserID, int iSite, Channel iChannel, Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, DateTime? iExpectTime)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(c => c.Proxy.Order_Onekey_Create(iUserID, iSite, iChannel, iGetAddress, iSendAddress, iExpectTime));
            return rtn;
        }

        public static ReturnEntity<bool> Order_Order_Cancel(int iID, OrderStatus iOrderStatus)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.Order_Order_Cancel(iID, iOrderStatus));
            return rtn;
        }
    }
}