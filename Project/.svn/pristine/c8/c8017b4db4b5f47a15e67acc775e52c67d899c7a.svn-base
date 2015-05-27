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
    /// 合作门店wcf代理类
    /// </summary>
    public class StoreProxy
    {
        /// <summary>
        /// 获取合作门店列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Wash_StoreDC> GetStoreList(string iName, string iCode, int? iSite, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Wash_StoreDC> rtn = null;
            ReturnEntity<RecordCountEntity<Wash_StoreDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<RecordCountEntity<Wash_StoreDC>>>
                    (client => client.Proxy.Wash_Store_SELECT_List(iName, iCode, iSite, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|StoreProxy GetStoreList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 新增合作门店信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> AddStore(Wash_StoreDC entity)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Store_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|StoreProxy AddStore|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取合作门店信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Wash_StoreDC> GetStoreBYID(int id)
        {
            ReturnEntity<Wash_StoreDC> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_StoreDC>>
                   (client => client.Proxy.Wash_Store_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|StoreProxy GetStoreBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 根据ID获取合作门店信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Wash_StoreDC> GetStoreBYID(Guid id)
        {
            ReturnEntity<Wash_StoreDC> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_StoreDC>>
                   (client => client.Proxy.Wash_Store_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|StoreProxy GetStoreBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新合作门店信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateStore(Wash_StoreDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Store_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|StoreProxy UpdateStore|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 根据门店id 获取操作员列表
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        public static IList<Wash_Store_OperatorDC> GetStoreOperatorListBYStoreID(int iStoreID)
        {
            IList<Wash_Store_OperatorDC> list = null;
            ReturnEntity<IList<Wash_Store_OperatorDC>> re = null;
            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<IList<Wash_Store_OperatorDC>>>
                    (client => client.Proxy.Wash_Store_Operator_SELECT_ALL(iStoreID));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|StoreProxy GetStoreOperatorListBYStoreID|" + ex.Message);
            }
            if (re != null && re.Success)
            {
                list = re.OBJ;
            }
            return list;
        }
    }
}