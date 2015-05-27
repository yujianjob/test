using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 活动wcf代理类
    /// </summary>
    public class ActivityProxy
    {

        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iEnableType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Wash_ActivityDC> GetActivityList(string iName, int? iSite, int? iChannel, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Wash_ActivityDC> rtn = null;
            ReturnEntity<RecordCountEntity<Wash_ActivityDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<RecordCountEntity<Wash_ActivityDC>>>
                    (client => client.Proxy.Wash_Activity_SELECT_List(iName, iSite, iChannel, iCommitStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ActivityProxy GetActivityList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }



        /// <summary>
        /// 新增活动信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddActivity(Wash_ActivityDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Wash_Activity_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ActivityProxy AddActivity|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Wash_ActivityDC> GetActivityBYID(int id)
        {
            ReturnEntity<Wash_ActivityDC> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_ActivityDC>>
                   (client => client.Proxy.Wash_Activity_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ActivityProxy GetActivityBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新活动信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateActivity(Wash_ActivityDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Activity_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ActivityProxy UpdateActivity|" + ex.Message);
            }
            return re;
        }


    }
}