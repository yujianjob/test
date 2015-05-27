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
    /// 商城礼品wcf代理类
    /// </summary>
    public class GiftProxy
    {
        /// <summary>
        /// 获取商城礼品列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iClass"></param>
        /// <param name="iName"></param>
        /// <param name="iSite"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Mall_ProductDC> GetGiftList(int? iType, int? iClass, string iName, int? iSite,  DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Mall_ProductDC> rtn = null;
            ReturnEntity<RecordCountEntity<Mall_ProductDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<RecordCountEntity<Mall_ProductDC>>>
                    (client => client.Proxy.Mall_Product_SELECT_List(iType,iClass, iName, iSite, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|GiftProxy GetGiftList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 新增商城礼品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> AddGift(Mall_ProductDC entity)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Mall_Product_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|GiftProxy AddGift|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 根据ID获取商城礼品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Mall_ProductDC> GetGiftBYID(int id)
        {
            ReturnEntity<Mall_ProductDC> re = null;
            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Mall_ProductDC>>
                   (client => client.Proxy.Mall_Product_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|GiftProxy GetGiftBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新商城礼品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateGift(Mall_ProductDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Mall_Product_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|GiftProxy UpdateGift|" + ex.Message);
            }
            return re;
        }
    }
}