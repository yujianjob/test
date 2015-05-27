using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 日志相关wcf代理类
    /// </summary>
    public class LogProxy
    {
        /// <summary>
        /// 后台操作日志列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<OperatorLogDC> GetOperatorLogList(int? iType, string iOperatorName, string iLogContent,  DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<OperatorLogDC> rtn = null;
            ReturnEntity<RecordCountEntity<OperatorLogDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<RecordCountEntity<OperatorLogDC>>>
                    (client => client.Proxy.PR_OperatorLog_SELECT_List(iType, iOperatorName, iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|LogProxy GetOperatorLogList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 后台操作日志列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Wash_Store_OperatorLogDC> GetStoreOperatorLogList(int? iType, string iOperatorName, string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Wash_Store_OperatorLogDC> rtn = null;
            ReturnEntity<RecordCountEntity<Wash_Store_OperatorLogDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<RecordCountEntity<Wash_Store_OperatorLogDC>>>
                    (client => client.Proxy.Wash_Store_OperatorLog_SELECT_List(iType, iOperatorName, iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|LogProxy GetStoreOperatorLogList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 系统日志列表
        /// </summary>
        /// <param name="iAppDomainName"></param>
        /// <param name="iTitle"></param>
        /// <param name="iEventType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Base_LogDC> GetBaseLogList(string iAppDomainName, string iTitle, int? iEventType, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Base_LogDC> rtn = null;
            ReturnEntity<RecordCountEntity<Base_LogDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<RecordCountEntity<Base_LogDC>>>
                    (client => client.Proxy.Base_Log_SELECT_List(iAppDomainName, iTitle, iEventType, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|LogProxy GetBaseLogList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }
    }
}