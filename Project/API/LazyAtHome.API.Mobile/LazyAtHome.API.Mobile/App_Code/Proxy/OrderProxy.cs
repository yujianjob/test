using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.App_Code.Proxy
{
    public class OrderProxy
    {




        //4.6	获取用户订单	8
        public static ReturnEntity<RecordCountEntity<Order_OrderDC>> app_Order_Order_SELECT_List(Guid iUserID, OrderStatus? iOrderStatus, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>(
                c => c.Proxy.app_Order_Order_SELECT_List(iUserID, null, iPageIndex, iPageSize));

            return rtn;
        }


        public static ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_Order_SELECT_List(string iOrderNumber, Guid? iUserID, string iMPNo, string iLoginName, OrderClass? OrderClass, OrderType? iOrderType, OrderStatus? iOrderStatus, int? iSite, int? iChannel, decimal? iTotalAmountMax, decimal? iTotalAmountMin, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>(
                c => c.Proxy.Order_Order_SELECT_List(
                    iOrderNumber, iUserID, iMPNo, iLoginName, OrderClass, iOrderType, iOrderStatus, iSite,
                    iChannel, iTotalAmountMax, iTotalAmountMin, iStartDate, iEndDate, iPageIndex, iPageSize));

            return rtn;
        }



        //4.7	订单支付	9
        public static ReturnEntity<bool> Order_Order_Pay(string iOrderNumber, decimal iPayMoney, PayMoneyType iPayType, PayMoneyChannel iPayChannel, DateTime iPayDate, string iPayRelationID)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Order_Order_Pay(iOrderNumber, iPayMoney, iPayType, iPayChannel, iPayDate, iPayRelationID));

            return rtn;
        }




        //4.8	获取单笔订单详情	9
        public static ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity(string iOrderNumber, bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress, bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(
                c => c.Proxy.Order_Order_SELECT_Entity(iOrderNumber, iGetProduct, iGetAmount, iGetConsigneeAddress, iGetExpress, iGetPayment, iGetStep));

            return rtn;
        }

        //4.9	取消订单	9
        public static ReturnEntity<bool> Order_Order_Cancel(int iID)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Order_Order_Cancel(iID, OrderStatus.UserChannel));

            return rtn;
        }

        //4.10	订单评价	10
        public static ReturnEntity<int> Order_Feedback_ADD(Order_FeedbackDC iOrder_FeedbackDC)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<int>>(
                c => c.Proxy.Order_Feedback_ADD(iOrder_FeedbackDC));

            return rtn;
        }

        //4.11	订单分享(暂无)	10
        //4.13	创建订单(一键下单)	10
        public static ReturnEntity<Order_OrderDC> Order_App_Onekey_Create(Guid iUserID, int iSite,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            DateTime? iExpectTime, string iUserRemark)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(
                c => c.Proxy.Order_App_Onekey_Create(iUserID, iSite, iGetAddress, iSendAddress, iExpectTime, iUserRemark));

            return rtn;
        }

        //4.16	订单金额核算	11



        //4.17	创建订单(普通订单)	12
        public static ReturnEntity<Order_OrderDC> Order_App_Create(Guid iUserID, int iSite,
          Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
          IList<int> iProductIDList,
          decimal iExpressFee, decimal iExpressFeeDiscount,
          decimal iUserMoney, IDictionary<int, decimal> iLazyCardList, IList<int> iCouponIDList,
          IDictionary<string, decimal> iActivityList,
          IDictionary<string, decimal> iServiceList,
          DateTime? iExpectTime, string iUserRemark)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>(
            c => c.Proxy.Order_App_Create(iUserID, iSite,
                iGetAddress, iSendAddress,
                iProductIDList,
                iExpressFee, iExpressFeeDiscount,
                iUserMoney, iLazyCardList, iCouponIDList,
                iActivityList,
                iServiceList,
                iExpectTime, iUserRemark));

            return rtn;
        }
    }
}