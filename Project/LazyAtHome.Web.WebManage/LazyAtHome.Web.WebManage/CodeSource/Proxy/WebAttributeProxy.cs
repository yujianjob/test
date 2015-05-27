using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 页面属性wcf代理类
    /// </summary>
    public class WebAttributeProxy
    {
        /// <summary>
        /// 获取页面属性列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iPage"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Base_WebAttributeDC> GetWebAttributeList(string iName, string iPage, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Base_WebAttributeDC> rtn = null;
            ReturnEntity<RecordCountEntity<Base_WebAttributeDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<RecordCountEntity<Base_WebAttributeDC>>>
                    (client => client.Proxy.Base_WebAttribute_SELECT_List(iName, iPage, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|WebAttributeProxy GetWebAttributeList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 新增页面属性信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddWebAttribute(Base_WebAttributeDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Base_WebAttribute_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|WebAttributeProxy AddWebAttribute|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取页面属性信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Base_WebAttributeDC> GetWebAttributeBYID(int id)
        {
            ReturnEntity<Base_WebAttributeDC> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<Base_WebAttributeDC>>
                   (client => client.Proxy.Base_WebAttribute_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|WebAttributeProxy GetWebAttributeBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新页面属性信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateWebAttribute(Base_WebAttributeDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Base_WebAttribute_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|WebAttributeProxy UpdateWebAttribute|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 删除页面属性信息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> DeleteWebAttribute(int iID)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Base_WebAttribute_DELETE(iID));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|WebAttributeProxy DeleteWebAttribute|" + ex.Message);
            }
            return re;
        }
    }
}