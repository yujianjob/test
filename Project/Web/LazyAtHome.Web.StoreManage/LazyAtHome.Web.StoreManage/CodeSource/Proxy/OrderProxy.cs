using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.StoreManage.CodeSource.Common;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.StoreManage.CodeSource.Proxy
{
    /// <summary>
    /// 合作门店订单wcf代理类
    /// </summary>
    public class OrderProxy
    {
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_OrderDC> GetOrderList(Guid iUserID, OrderStatus? iOrderStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Order_OrderDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_OrderDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>
                    (client => client.Proxy.Order_Order_SELECT_List(iUserID, iOrderStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }



        /// <summary>
        /// 获取当前未打包的订单
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <param name="iGetAddress"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> GetStoreOrder(Guid iStoreID, Order_ConsigneeAddressDC iGetAddress)
        {
            //Order_OrderDC entity = null;
            ReturnEntity<Order_OrderDC> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_Order_Create_Store(iStoreID, iGetAddress));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetStoreOrder|" + ex.Message);
            }

            return re;
        }

        /// <summary>
        /// 根据门店ID号获取当前未打包的订单
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> GetStoreOrderByStoreID(Guid iStoreID)
        {
            ReturnEntity<Order_OrderDC> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_Order_SELECT_Entity(iStoreID));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetStoreOrderByNum|" + ex.Message);
            }

            return re;
        }


        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> GetStoreOrderByNum(string iOrderNumber)
        {
            ReturnEntity<Order_OrderDC> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_Order_SELECT_Entity(iOrderNumber, true, false, false, false, false, false));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetStoreOrderByNum|" + ex.Message);
            }

            return re;
        }


        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <param name="iGetAddress"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> AddOrder(Guid iStoreID, Order_ConsigneeAddressDC iGetAddress)
        {
            ReturnEntity<Order_OrderDC> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_Order_Create_Store(iStoreID, iGetAddress));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy AddOrder|" + ex.Message);
            }

            return re;
        }


        /// <summary>
        /// 添加产品到订单
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> AddProductToOrder(int iOrderID, IList<Order_ProductDC> iProductList)
        {
            ReturnEntity<bool> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Product_ADD_Store(iOrderID, iProductList));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy AddProductToOrder|" + ex.Message);
            }

            return re;
        }

        /// <summary>
        /// 从订单中删除产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> DeleteProductFromOrder(int iOrderID, int iOrderProductID)
        {
            ReturnEntity<bool> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Product_DELETE_Store(iOrderID, iOrderProductID));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy DeleteProductFromOrder|" + ex.Message);
            }

            return re;
        }

        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNum"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> PackageOrder(int iOrderID, string iExpressNum)
        {

            ReturnEntity<bool> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_Submit_Store(iOrderID, iExpressNum));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy PackageOrder|" + ex.Message);
            }

            return re;
        }



        ///// <summary>
        ///// 获取用户的产品列表
        ///// </summary>
        ///// <param name="iUserMPNo"></param>
        ///// <param name="iUserName"></param>
        ///// <param name="iUserSignType"></param>
        ///// <returns></returns>
        //public static ReturnEntity<IList<Order_ProductDC>> GetProductListByUser(string iUserMPNo, string iUserName, UserSignType? iUserSignType)
        //{
        //    ReturnEntity<IList<Order_ProductDC>> re = null;
        //    try
        //    {
        //        re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<IList<Order_ProductDC>>>
        //           (client => client.Proxy.Order_Product_SELECT_List_Store(iUserMPNo, iUserName, iUserSignType));
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetProductListByUser|" + ex.Message);
        //    }

        //    return re;
            
        //}
    }
}