using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 订单wcf代理类
    /// </summary>
    public class OrderProxy
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="iOrderNuber"></param>
        /// <param name="iUserID"></param>
        /// <param name="iMPNO"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iOrderClass"></param>
        /// <param name="iOrderType"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iTotalAmountMax"></param>
        /// <param name="iTotalAmountMin"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_OrderDC> GetOrderList(string iOrderNuber, Guid? iUserID, string iMPNO, string iLoginName, OrderClass? iOrderClass, OrderType? iOrderType, OrderStatus? iOrderStatus, int? iSite, int? iChannel, decimal? iTotalAmountMax, decimal? iTotalAmountMin, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize, string Consignee = null, int SortType = 0, DateTime? iExpStartDate = null, DateTime? iExpEndDate = null, int? iStep = null, int? iGetExpressType = null)
        {
            RecordCountEntity<Order_OrderDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_OrderDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>
                    (client => client.Proxy.Order_Order_SELECT_List(iOrderNuber, iUserID, iMPNO, iLoginName, iOrderClass, iOrderType, iOrderStatus, iSite, iChannel, iTotalAmountMax, iTotalAmountMin, iStartDate, iEndDate, iPageIndex, iPageSize, Consignee, SortType, iExpStartDate, iExpEndDate, iStep, iGetExpressType));
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
        /// 获取逾期订单列表
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_OrderDC> GetExpireOrderList(DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Order_OrderDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_OrderDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_OrderDC>>>
                    (client => client.Proxy.Order_Order_SELECT_List(null, null, null, null, null, WCF.OrderSystem.Contract.Enumerate.OrderType.NoBack, WCF.OrderSystem.Contract.Enumerate.OrderStatus.UnFinish, null, null, null, null, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetExpireOrderList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 根据ID获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> GetOrderBYID(int id)
        {
            ReturnEntity<Order_OrderDC> re = null;
            try
            {

                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_Order_SELECT_Entity(id, true, true, true, true, true, true, true));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> GetOrderBYOrderNumber(string ordernumber)
        {
            ReturnEntity<Order_OrderDC> re = null;
            try
            {

                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_Order_SELECT_Entity(ordernumber, true, true, true, true, true, true));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderBYOrderNumber|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 根据ID获取订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> GetOrderConsigneeAddressBYOrderID(int id)
        {
            ReturnEntity<Order_OrderDC> re = null;
            try
            {

                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_Order_SELECT_Entity(id, false, false, true, false, false, false));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderBYID|" + ex.Message);
            }
            return re;

        }




        /// <summary>
        /// 一键下单审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> AuditOrder(int id, bool flag, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_Audit(id, flag, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy AuditOrder|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 修改客服备注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CSRemark"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditCSRemark(int id, string CSRemark)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_UPDATE_CSRemark(id, CSRemark));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy EditCSRemark|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 修改取件收件地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditAddress(Order_ConsigneeAddressDC entity)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_ConsigneeAddress_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy EditAddress|" + ex.Message);
            }
            return re;
        }



        /// <summary>
        /// 退单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> RepayOrder(int id)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_ChargeBack(id, Channel.CustomerService));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy RepayOrder|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> CancelOrder(int id)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_Cancel(id, OrderStatus.SystemChannel));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy RepayOrder|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 反洗
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iPidList"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> WashAgain(int iID, IList<int> iPidList)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_Return(iID, iPidList));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy WashAgain|" + ex.Message);
            }
            return re;
        }



        /// <summary>
        /// 订单合并
        /// </summary>
        /// <param name="iPrimaryOrderID">主订单ID</param>
        /// <param name="iSlaveOrderID">副订单ID</param>
        /// <returns></returns>
        public static ReturnEntity<bool> MergerOrder(int iPrimaryOrderID, int iSlaveOrderID)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_Merger(iPrimaryOrderID, iSlaveOrderID));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy MergerOrder|" + ex.Message);
            }
            return re;
        }



        /// <summary>
        /// 写入快递订单号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> FinishExpress(int id, string iExpressNumber)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_FinishExpress(id, iExpressNumber));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy FinishExpress|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 修改期望收件时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditExpectTime(int id, DateTime iExpectTime)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_ExpectTime_Change(id, iExpectTime));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy EditExpectTime|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 修改取件物流类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="GetExpressType"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditGetExpressType(int id, int GetExpressType)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_UPDATE_GetExpressType(id, GetExpressType));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy EditGetExpressType|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 自主物流重新下单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> ReCreateInternalExpressOrder(string iOrderNumber)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_InternalExpressOrder_ReCreate(iOrderNumber));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy ReCreateInternalExpressOrder|" + ex.Message);
            }
            return re;
        }






        /// <summary>
        /// 修改订单进程
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditOrderStep(int oid, int step)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Step_Change(oid, (WashStepType)step));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy EditOrderStep|" + ex.Message);
            }
            return re;
        }




        /// <summary>
        /// 客服一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <param name="iUserCouponID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<Order_OrderDC> OnekeyOrder(Guid iUserID, Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, DateTime? iExpectTime, string iUserRemark, int? iUserCouponID, int iMuser)
        {
            ReturnEntity<Order_OrderDC> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                   (client => client.Proxy.Order_CustomerService_Onekey_Create(iUserID, iGetAddress, iSendAddress, iExpectTime, iUserRemark, iUserCouponID, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy OnekeyOrder|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 企业客户下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iProductName"></param>
        /// <param name="iPrice"></param>
        /// <param name="iCount"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> CreateBusinessOrder(Guid iUserID, Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, string iProductName, decimal iPrice, int iCount, DateTime? iExpectTime, string iUserRemark, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_CustomerService_BusinessOrder_Create(iUserID, iGetAddress, iSendAddress, iProductName, iPrice, iCount, iExpectTime, iUserRemark, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy CreateBusinessOrder|" + ex.Message);
            }
            return re;
        }




        /// <summary>
        /// 订单改产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditOrderProduct(int iOrderID, IList<Order_ProductDC> iProductList, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Order_EditProduct(iOrderID, iProductList, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy EditOrderProduct|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 修改工厂洗涤状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductID"></param>
        /// <param name="iWashStatus"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditOrderProductWashStatus(int iOrderID, int iProductID, int iWashStatus, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Order_Product_WashStatus(iOrderID, iProductID, iWashStatus, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy EditOrderProductWashStatus|" + ex.Message);
            }
            return re;
        }






        /// <summary>
        /// 水洗条码报表
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_Order_StatDC> GetOrderCodeReportList(DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {

            RecordCountEntity<Order_Order_StatDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_Order_StatDC>>>
                    (client => client.Proxy.Order_Product_WashStep_Code_Stat(iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderCodeReportList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 订单跟踪报表
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_Order_StatDC> GetOrderStepReportList(DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {

            RecordCountEntity<Order_Order_StatDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_Order_StatDC>>>
                    (client => client.Proxy.Order_Order_StepTime_Stat(iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderStepReportList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 入库报表
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_Order_StatDC> GetStockInReportList(DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {

            RecordCountEntity<Order_Order_StatDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_Order_StatDC>>>
                    (client => client.Proxy.Order_Order_InFactory_Stat(iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetStockInReportList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 出库报表
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_Order_StatDC> GetStockOutReportList(DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {

            RecordCountEntity<Order_Order_StatDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_Order_StatDC>>>
                    (client => client.Proxy.Order_Order_OutFactory_Stat(iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetStockOutReportList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 订单超时
        /// </summary>
        /// <param name="iGetPackage"></param>
        /// <param name="iWash"></param>
        /// <param name="iSendPackage"></param>
        /// <param name="iAll"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_Order_StatDC> GetOrderAlarmList(bool iGetPackage, bool iWash, bool iSendPackage,bool iAll, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {

            RecordCountEntity<Order_Order_StatDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_Order_StatDC>>>
                    (client => client.Proxy.Order_Order_Alarm_Stat(iGetPackage, iWash, iSendPackage, iAll, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderAlarmList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 订单预警
        /// </summary>
        /// <param name="iGetPackage"></param>
        /// <param name="iWash"></param>
        /// <param name="iSendPackage"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Order_Order_StatDC> GetOrderWarningList(bool iGetPackage, bool iWash, bool iSendPackage, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {

            RecordCountEntity<Order_Order_StatDC> rtn = null;
            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Order_Order_StatDC>>>
                    (client => client.Proxy.Order_Order_Warning_Stat(iGetPackage, iWash, iSendPackage, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderWarningList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }




        /// <summary>
        /// 订单详情 导出所用
        /// </summary>
        /// <param name="iKeyword"></param>
        /// <param name="iCreateStartDate"></param>
        /// <param name="iCreateEndDate"></param>
        /// <param name="iFinishStartDate"></param>
        /// <param name="iFinishEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static IList<string[]> OrderExport(string iKeyword, DateTime? iCreateStartDate, DateTime? iCreateEndDate, DateTime? iFinishStartDate, DateTime? iFinishEndDate, int iPageIndex, int iPageSize)
        {
            IList<string[]> rtn = null;
            ReturnEntity<IList<string[]>> re = null;
            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<IList<string[]>>>
                    (client => client.Proxy.Order_Order_Export(iKeyword, iCreateStartDate, iCreateEndDate, iFinishStartDate, iFinishEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetOrderWarningList|" + ex.Message);
            }
            if (re != null && re.Success)
            {
                rtn = re.OBJ;
            }
            return rtn;
        }






        /// <summary>
        /// 根据订单号 查询顺丰订单
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public static ReturnEntity<SF_OrderInfoDC> GetSFOrderBYOrderNum(string OrderNum)
        {
            ReturnEntity<SF_OrderInfoDC> re = null;
            try
            {

                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<SF_OrderInfoDC>>
                   (client => client.Proxy.SF_OrderSearch(OrderNum));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy GetSFOrderBYOrderNum|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 顺丰订单下单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public static ReturnEntity<SF_OrderInfoDC> CreateSFOrder(string iOrderNumber, Order_ConsigneeAddressDC iGetAddress, DateTime? iExpectTime)
        {
            ReturnEntity<SF_OrderInfoDC> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<SF_OrderInfoDC>>
                   (client => client.Proxy.SF_CreateReverseOrder(iOrderNumber, iGetAddress, iExpectTime));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy CreateSFOrder|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 顺丰订单撤单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public static ReturnEntity<SF_OrderInfoDC> CancelSFOrder(string iOrderNumber)
        {
            ReturnEntity<SF_OrderInfoDC> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<SF_OrderInfoDC>>
                   (client => client.Proxy.SF_CancelOrder(iOrderNumber));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy CancelSFOrder|" + ex.Message);
            }
            return re;
        }
    }
}