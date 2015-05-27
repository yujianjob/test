using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.SFSupport.CodeSource.Common;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.SFSupport.CodeSource.Proxy
{
    /// <summary>
    /// 订单wcf代理类
    /// </summary>
    public class OrderProxy
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="iUserName"></param>
        /// <param name="iMPNO"></param>
        /// <param name="iExpressStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Partner_Order_ExpressDC> GetOrderList(string iUserName, string iMPNO, int? iExpressStatus, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Partner_Order_ExpressDC> rtn = null;
            ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>>>
                    (client => client.Proxy.Partner_Order_Express_SELECT_List(iUserName, iMPNO, iExpressStatus, iPageIndex, iPageSize));
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
        /// 获取历史订单
        /// </summary>
        /// <param name="iUserName"></param>
        /// <param name="iMPNO"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iGetExpressNumber"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Partner_Order_ExpressDC> GetOrderList(string iUserName, string iMPNO, string iGetExpressNumber, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Partner_Order_ExpressDC> rtn = null;
            ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>>>
                    (client => client.Proxy.Partner_Order_Express_SELECT_List(iUserName, iMPNO, iGetExpressNumber, iStartDate, iEndDate, iPageIndex, iPageSize));
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
        /// 更新呼叫快递状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iExpressStatus"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UPDATEExpressStatus(int id, int iExpressStatus)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Partner_Order_Express_UPDATE_ExpressStatus(id, iExpressStatus));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy UPDATEExpressStatus|" + ex.Message);
            }
            return re;
        }
    }
}