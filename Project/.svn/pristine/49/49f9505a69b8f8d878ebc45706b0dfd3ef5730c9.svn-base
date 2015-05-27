using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.ClientProxy;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Business
{
    public class WashOrder
    {
        public WashOrder()
        {

        }

        static WashOrder _WashOrder;
        public static WashOrder Instance
        {
            get
            {
                if (_WashOrder == null)
                {
                    _WashOrder = new WashOrder();
                }
                return _WashOrder;
            }
        }

        /// <summary>
        /// 根据订单号查订单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_OrderNumber(string iOrderNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                (client => client.Proxy.Order_Order_SELECT_Entity(iOrderNumber,
              iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep));
        }

        /// <summary>
        /// 根据物流订单号查订单
        /// </summary>
        /// <param name="iExpressNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_Express(string iExpressNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                (client => client.Proxy.Order_Order_SELECT_Entity_Express(iExpressNumber,
              iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep));
        }

        /// <summary>
        /// 根据工厂条形码查订单
        /// </summary>
        /// <param name="iCodeNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_FactoryCode(string iCodeNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                (client => client.Proxy.Order_Order_SELECT_Entity_FactoryCode(iCodeNumber,
              iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep));
        }

        ///// <summary>
        ///// 工厂出库添加快递单号
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iExpressNumber"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> Order_Order_CreateSendExpress(int iOrderID, string iExpressNumber)
        //{
        //    return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
        //          (client => client.Proxy.Order_Express_ADD_Factory(iOrderID, iExpressNumber));
        //}

        /// <summary>
        /// 为一键下单订单添加产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Factory_AddProduct(int iOrderID, IList<Order_ProductDC> iProductList, int iMuser)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                  (client => client.Proxy.Order_Factory_AddProduct(iOrderID, iProductList, iMuser));
        }

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Express_ADD_Factory(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                 (client => client.Proxy.Order_Express_ADD_Factory(iOrderID, iExpressNumber, iRelationID, iMuser));
        }

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Express_ADD_Factory_Re(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                (client => client.Proxy.Order_Express_ADD_Factory_Re(iOrderID, iExpressNumber, iRelationID, iMuser));
        }

        /// <summary>
        /// 订单记录快递单号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_FinishExpress(int iOrderID, string iExpressNumber)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                (client => client.Proxy.Order_Order_FinishExpress(iOrderID, iExpressNumber));
        }

        /// <summary>
        /// 修改产品步骤
        /// </summary>
        /// <param name="iOrderProductID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_Step(int iOrderID, int iOrderProductID, int iStep)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
              (client => client.Proxy.Order_Product_UPDATE_Step(iOrderID, iOrderProductID, iStep));
        }

        /// <summary>
        /// 更新外部编号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iOtherCode"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_OtherCode(int iOrderID, int iOrderProductID, string iOtherCode)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
              (client => client.Proxy.Order_Product_UPDATE_OtherCode(iOrderID, iOrderProductID, iOtherCode));
        }

        /// <summary>
        /// 工厂出库
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<int> Order_Product_OutFactory(int iOrderID, int iOrderProductID, string iExpressNumber, int iMuser)
        {
            return WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<int>>
           (client => client.Proxy.Order_Product_OutFactory(iOrderID, iOrderProductID, iExpressNumber, iMuser));
        }

        //工厂入库条码扫不出
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_NotFound(string iExpressNumber, int iMuser)
        {
            return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                (client => client.Proxy.Order_Order_InFactory_FailNotify_NotFound(iExpressNumber, iMuser));
        }

        //工厂入库产品不对
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_ItemError(string iOrderNumber, string iContent, int iMuser)
        {
            return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                (client => client.Proxy.Order_Order_InFactory_FailNotify_ItemError(iOrderNumber, iContent, iMuser));
        }

        //工厂入库条码扫不出
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_NotFound(string iCode, int iMuser)
        {
            return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                (client => client.Proxy.Order_Order_OutFactory_FailNotify_NotFound(iCode, iMuser));
        }

        //工厂入库产品不对
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_ItemBad(string iOrderNumber, string iContent, int iMuser)
        {
            return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                (client => client.Proxy.Order_Order_OutFactory_FailNotify_ItemBad(iOrderNumber, iContent, iMuser));
        }


    }
}
