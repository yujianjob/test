using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.Express.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;


namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 物流wcf代理类
    /// </summary>
    public class ExpressProxy
    {
        #region 物流

        /// <summary>
        /// 获取物流订单列表
        /// </summary>
        /// <param name="iExpNumber"></param>
        /// <param name="iOutNumber"></param>
        /// <param name="iTransportType"></param>
        /// <param name="iAddress"></param>
        /// <param name="iContacts"></param>
        /// <param name="iMpno"></param>
        /// <param name="iStep"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_OrderDC> GetExpOrderList(string iExpNumber, string iOutNumber, int? iTransportType, string iAddress, string iContacts, string iMpno, int? iStep, string iKeyword, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_OrderDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_OrderDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_OrderDC>>>
                    (client => client.Proxy.Exp_Order_SELECT_List(iExpNumber, iOutNumber, iTransportType, iAddress, iContacts, iMpno, iStep, iKeyword, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpOrderList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 获取待处理物流订单列表
        /// </summary>
        /// <param name="iExpNumber"></param>
        /// <param name="iOutNumber"></param>
        /// <param name="iTransportType"></param>
        /// <param name="iAddress"></param>
        /// <param name="iContacts"></param>
        /// <param name="iMpno"></param>
        /// <param name="iStep"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_OrderDC> GetExpOrderListUnAllocation(string iExpNumber, string iOutNumber, int? iTransportType, string iAddress, string iContacts, string iMpno, int? iStep, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_OrderDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_OrderDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_OrderDC>>>
                    (client => client.Proxy.Exp_Order_SELECT_List_UnAllocation(iExpNumber, iOutNumber, iTransportType, iAddress, iContacts, iMpno, iStep, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpOrderList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 根据ID获取物流订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Exp_OrderDC> GetExpOrderBYID(int id)
        {
            ReturnEntity<Exp_OrderDC> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_OrderDC>>
                   (client => client.Proxy.Exp_Order_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpOrderDCBYID|" + ex.Message);
            }
            return re;

        }






        /// <summary>
        /// 获取物流区域列表
        /// </summary>
        /// <returns></returns>
        public static ReturnEntity<IList<Exp_AreaDC>> GetExpAreaList()
        {
            ReturnEntity<IList<Exp_AreaDC>> re = null;
            try
            {
                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<IList<Exp_AreaDC>>>
                    (client => client.Proxy.Exp_Area_SELECT_List());
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpAreaList|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 新增区域
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddExpArea(Exp_AreaDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Exp_Area_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy AddExpArea|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取区域信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Exp_AreaDC> GetExpAreaBYID(int id)
        {
            ReturnEntity<Exp_AreaDC> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_AreaDC>>
                   (client => client.Proxy.Exp_Area_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpAreaBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新区域信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateExpArea(Exp_AreaDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Area_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy UpdateExpArea|" + ex.Message);
            }
            return re;
        }






        /// <summary>
        /// 获取物流存衣点
        /// </summary>
        /// <param name="iDistrictID"></param>
        /// <param name="iName"></param>
        /// <param name="iType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_NodeDC> GetExpNodeList(int? iDistrictID, string iName, int? iType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_NodeDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_NodeDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_NodeDC>>>
                    (client => client.Proxy.Exp_Node_SELECT_List(iDistrictID, iName, iType, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpNodeList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;

            //IList<Exp_NodeDC> list = null;
            //try
            //{
            //    list = ExpressClient.Cache_Exp_Node_GetList();
            //}
            //catch (Exception ex)
            //{
            //    WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpNodeList|" + ex.Message);
            //    return null;
            //}

            //return list;
        }


        /// <summary>
        /// 新增存衣点信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddExpNode(Exp_NodeDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Exp_Node_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy AddExpNode|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取存衣点信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Exp_NodeDC> GetExpNodeBYID(int id)
        {
            ReturnEntity<Exp_NodeDC> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_NodeDC>>
                   (client => client.Proxy.Exp_Node_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpNodeBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新存衣点信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateExpNode(Exp_NodeDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Node_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy UpdateExpNode|" + ex.Message);
            }
            return re;
        }



        ///// <summary>
        ///// 获取快递员信息
        ///// </summary>
        ///// <param name="iLoginName"></param>
        ///// <param name="iName"></param>
        ///// <param name="iType"></param>
        ///// <param name="iMpNo"></param>
        ///// <param name="iNodeName"></param>
        ///// <param name="iStartDate"></param>
        ///// <param name="iEndDate"></param>
        ///// <param name="iPageIndex"></param>
        ///// <param name="iPageSize"></param>
        ///// <returns></returns>
        //public static RecordCountEntity<Exp_OperatorDC> GetExpOperatorList(string iLoginName, string iName, int? iType, string iMpNo, string iNodeName, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        //{
        //    RecordCountEntity<Exp_OperatorDC> rtn = null;
        //    ReturnEntity<RecordCountEntity<Exp_OperatorDC>> rce = null;
        //    try
        //    {
        //        rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_OperatorDC>>>
        //            (client => client.Proxy.Exp_Operator_SELECT_List(iLoginName, iName, iMpNo, iType, iNodeName, iStartDate, iEndDate, iPageIndex, iPageSize));
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpOperatorList|" + ex.Message);
        //    }
        //    if (rce != null && rce.Success)
        //    {
        //        rtn = rce.OBJ;
        //    }
        //    return rtn;
        //}


        ///// <summary>
        ///// 新增快递员信息
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public static ReturnEntity<int> AddExpOperator(Exp_OperatorDC entity)
        //{
        //    ReturnEntity<int> re = null;

        //    try
        //    {
        //        re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<int>>
        //           (client => client.Proxy.Exp_Operator_ADD(entity));
        //    }
        //    catch (System.ServiceModel.FaultException ex)
        //    {
        //        //return ex.Message;
        //        WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy AddExpOperator|" + ex.Message);
        //    }
        //    return re;
        //}


        ///// <summary>
        ///// 根据ID获取快递员信息
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static ReturnEntity<Exp_OperatorDC> GetExpOperatorBYID(int id)
        //{
        //    ReturnEntity<Exp_OperatorDC> re = null;
        //    try
        //    {

        //        re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_OperatorDC>>
        //           (client => client.Proxy.Exp_Operator_SELECT_Entity(id));
        //    }
        //    catch (System.ServiceModel.FaultException ex)
        //    {
        //        //return ex.Message;
        //        WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpOperatorBYID|" + ex.Message);
        //    }
        //    return re;

        //}


        ///// <summary>
        ///// 更新快递员信息
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public static ReturnEntity<bool> UpdateExpOperator(Exp_OperatorDC entity)
        //{
        //    ReturnEntity<bool> re = null;
        //    try
        //    {

        //        re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
        //           (client => client.Proxy.Exp_Operator_UPDATE(entity));
        //    }
        //    catch (System.ServiceModel.FaultException ex)
        //    {
        //        //return ex.Message;
        //        WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy UpdateExpOperator|" + ex.Message);
        //    }
        //    return re;
        //}




        /// <summary>
        /// 物流分配
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> AllocationExpOrder(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Order_Allocation(iOrderID, iOperatorID, iNodeID, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy AllocationExpOrder|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 物流强制分配/取消分配(重新自动分配)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iMuser"></param>
        /// <param name="iCSRemark"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> AllocationForcedExpOrder(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser, string iCSRemark)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Order_Allocation_Forced(iOrderID, iOperatorID, iNodeID, iMuser, iCSRemark));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy AllocationForcedExpOrder|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 修改物流订单中的 客户信息
        /// </summary>
        /// <param name="iExp_OrderDC"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateExpOrder(Exp_OrderDC iExp_OrderDC)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Order_UPDATE(iExp_OrderDC));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy UpdateExpOrder|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 设定订单状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStep"></param>
        /// <param name="iStepRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> SetStepExpOrder(int iOrderID, int iStep, string iStepRemark, int iMuser)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Order_SetStep(iOrderID, iStep, iStepRemark, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy SetStepExpOrder|" + ex.Message);
            }
            return re;

        }


        public static ReturnEntity<IList<string>> GetNodeMap()
        {
            ReturnEntity<IList<string>> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<IList<string>>>
                   (client => client.Proxy.Base_Node_Map());
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetNodeMap|" + ex.Message);
            }
            return re;

        }

        #endregion

        #region 仓储

        /// <summary>
        /// 仓库搜索
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iName"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_StorageDC> GetExpStorageList(int? iType, string iName, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_StorageDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_StorageDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_StorageDC>>>
                    (client => client.Proxy.Exp_Storage_SELECT_List(iType, iName, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpStorageList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 物品查询
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iItemType"></param>
        /// <param name="iTargetType"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_StorageItemDC> GetExpStorageItemList(int? iStorageID, string iNumber, string iOtherNumber, int? iItemType, int? iTargetType, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_StorageItemDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_StorageItemDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_StorageItemDC>>>
                    (client => client.Proxy.Exp_StorageItem_SELECT_List(iStorageID, iNumber, iOtherNumber, iItemType, null, null,iTargetType,null, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpStorageItemList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 出入库日志
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iType"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_StorageLogDC> GetExpStorageLogList(int? iStorageID, int? iType, string iNumber, string iOtherNumber, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_StorageLogDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_StorageLogDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_StorageLogDC>>>
                    (client => client.Proxy.Exp_StorageLog_SELECT_List(iStorageID, iType, iNumber, iOtherNumber, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpStorageLogList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 物品所在地调整
        /// </summary>
        /// <param name="iItemID"></param>
        /// <param name="iSourceStorageID"></param>
        /// <param name="iTargetStorageID"></param>
        /// <param name="iTargeType"></param>
        /// <param name="iAdmin"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<int> EditExpStorageItemIO(int iItemID, int iSourceStorageID, int iTargetStorageID, int iTargeType, bool iAdmin, int iMuser)
        {
            ReturnEntity<int> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Exp_StorageItem_IO(iItemID, iSourceStorageID, iTargetStorageID, iTargeType, iAdmin, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy EditExpStorageItemIO|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 根据ID获取物品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Exp_StorageItemDC> GetExpStorageItem(int id)
        {
            ReturnEntity<Exp_StorageItemDC> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_StorageItemDC>>
                   (client => client.Proxy.Exp_StorageItem_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpStorageItem|" + ex.Message);
            }
            return re;

        }

        #endregion


        #region 对账

        /// <summary>
        /// 获取佣金账单列表
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iBillStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_Preson_CommissionBillDC> GetExpCommissionBillList(string iPeriod, string iOperatorName, int? iBillStatus, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_Preson_CommissionBillDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_Preson_CommissionBillDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_Preson_CommissionBillDC>>>
                    (client => client.Proxy.Exp_Preson_CommissionBill_SELECT_List(iPeriod, iOperatorName, iBillStatus, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpCommissionBillList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 获取佣金账单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Exp_Preson_CommissionBillDC> GetExpCommissionBillBYID(int id)
        {
            ReturnEntity<Exp_Preson_CommissionBillDC> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_Preson_CommissionBillDC>>
                   (client => client.Proxy.Exp_Preson_CommissionBill_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpCommissionBillBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 佣金结账
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> CloseExpCommissionBill(int iBillID, decimal iRealMoney, int iMuser)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Preson_CommissionBill_UPDATE_Close(iBillID, iRealMoney, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy CloseExpCommissionBill|" + ex.Message);
            }
            return re;

        }






        /// <summary>
        /// 获取收款账单列表
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iBillStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Exp_Preson_PaymentBillDC> GetExpPaymentBillList(string iPeriod, string iOperatorName, int? iBillStatus, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Exp_Preson_PaymentBillDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_Preson_PaymentBillDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_Preson_PaymentBillDC>>>
                    (client => client.Proxy.Exp_Preson_PaymentBill_SELECT_List(iPeriod, iOperatorName, iBillStatus, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpPaymentBillList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 获取收款账单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Exp_Preson_PaymentBillDC> GetExpPaymentBillBYID(int id)
        {
            ReturnEntity<Exp_Preson_PaymentBillDC> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_Preson_PaymentBillDC>>
                   (client => client.Proxy.Exp_Preson_PaymentBill_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy GetExpPaymentBillBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 收款账单结账
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> CloseExpPaymentBill(int iBillID, decimal iRealMoney, int iMuser)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Exp_Preson_PaymentBill_UPDATE_Close(iBillID, iRealMoney, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy Exp_Preson_PaymentBill_UPDATE_Close|" + ex.Message);
            }
            return re;

        }



        /// <summary>
        /// 检查地址
        /// </summary>
        /// <param name="iAddress"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> CheckAddress(string iAddress)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.CheckNode_Address(iAddress));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|ExpressProxy CheckAddress|" + ex.Message);
            }
            return re;
        }




        /// <summary>
        /// 佣金导出
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static IList<string[]> CommissionBillExport(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            IList<string[]> rtn = null;
            ReturnEntity<IList<string[]>> re = null;
            try
            {
                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<IList<string[]>>>
                    (client => client.Proxy.Exp_Preson_CommissionBill_Export(iPeriod, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy CommissionBillExport|" + ex.Message);
            }
            if (re != null && re.Success)
            {
                rtn = re.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 应收货款导出
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static IList<string[]> PaymentBillExport(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            IList<string[]> rtn = null;
            ReturnEntity<IList<string[]>> re = null;
            try
            {
                re = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<IList<string[]>>>
                    (client => client.Proxy.Exp_Preson_PaymentBill_Export(iPeriod, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OrderProxy PaymentBillExport|" + ex.Message);
            }
            if (re != null && re.Success)
            {
                rtn = re.OBJ;
            }
            return rtn;
        }
        #endregion
    }
}