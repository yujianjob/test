using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Portal
{
    public partial class ExpressPortal
    {
        protected LazyAtHome.WCF.Express.Business.Business.ManageExpress ManageExpressInstance = LazyAtHome.WCF.Express.Business.Business.ManageExpress.Instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<int> Exp_Node_ADD(Exp_NodeDC iExp_NodeDC)
        {
            Log_DeBug("Exp_Node_ADD", iExp_NodeDC);

            ReturnEntity<int> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Node_ADD(iExp_NodeDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Node_ADD", ex);
                rtn = new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Node_ADD");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Node_UPDATE(Exp_NodeDC iExp_NodeDC)
        {
            Log_DeBug("Exp_Node_UPDATE", iExp_NodeDC);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Node_UPDATE(iExp_NodeDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Node_UPDATE", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Node_UPDATE");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Node
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_NodeDC> Exp_Node_SELECT_Entity(int iID)
        {
            Log_DeBug("Exp_Node_SELECT_Entity", iID);

            var rtn = ManageExpressInstance.Exp_Node_SELECT_Entity(iID);

            return rtn;
        }

        /// <summary>
        /// 查询站点Exp_Node
        /// </summary>
        /// <param name="iDistrictID"></param>
        /// <param name="iName"></param>
        /// <param name="iType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_NodeDC>> Exp_Node_SELECT_List(
            int? iAreaID, string iName,
            int? iType,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize, int? iParentID = null)
        {
            Log_DeBug("Exp_Node_SELECT_List", iAreaID, iName, iType, iStartDate, iEndDate, iPageIndex, iPageSize, iParentID);

            var rtn = ManageExpressInstance.Exp_Node_SELECT_List(iAreaID, iName, iType, iStartDate, iEndDate, iPageIndex, iPageSize, iParentID);

            return rtn;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="iExp_OperatorDC"></param>
        ///// <returns></returns>
        //public ReturnEntity<int> Exp_Operator_ADD(Exp_OperatorDC iExp_OperatorDC)
        //{
        //    Log_DeBug("Exp_Operator_ADD", iExp_OperatorDC);

        //    var rtn = ManageExpressInstance.Exp_Operator_ADD(iExp_OperatorDC);

        //    return rtn;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="iExp_OperatorDC"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> Exp_Operator_UPDATE(Exp_OperatorDC iExp_OperatorDC)
        //{
        //    Log_DeBug("Exp_Operator_UPDATE", iExp_OperatorDC);

        //    var rtn = ManageExpressInstance.Exp_Operator_UPDATE(iExp_OperatorDC);

        //    return rtn;
        //}

        ///// <summary>
        ///// 根据ID查询Exp_Operator
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <returns></returns>
        //public ReturnEntity<Exp_OperatorDC> Exp_Operator_SELECT_Entity(int iID)
        //{
        //    Log_DeBug("Exp_Operator_SELECT_Entity", iID);

        //    var rtn = ManageExpressInstance.Exp_Operator_SELECT_Entity(iID);

        //    return rtn;
        //}

        ///// <summary>
        ///// 查询全部
        ///// </summary>
        ///// <returns></returns>
        //public ReturnEntity<RecordCountEntity<Exp_OperatorDC>> Exp_Operator_SELECT_List(
        //    string iLoginName, string iName, string iMpNo,
        //    int? iType,
        //    string iNodeName,
        //    DateTime? iStartDate, DateTime? iEndDate,
        //    int iPageIndex, int iPageSize)
        //{
        //    Log_DeBug("Exp_Operator_SELECT_List", iLoginName, iName, iMpNo,
        //                     iType, iNodeName, iStartDate, iEndDate, iPageIndex, iPageSize);

        //    var rtn = ManageExpressInstance.Exp_Operator_SELECT_List(iLoginName, iName, iMpNo,
        //                     iType, iNodeName, iStartDate, iEndDate, iPageIndex, iPageSize);

        //    return rtn;
        //}

        /// <summary>
        /// 查询全部Exp_Order
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_List(
            string iExpNumber, string iOutNumber,
            int? iTransportType,
            string iAddress, string iContacts, string iMpno,
            int? iStep,
            string iKeyword,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Order_SELECT_List", iExpNumber, iOutNumber,
                            iTransportType, iAddress, iContacts, iMpno,
                            iStep, iKeyword, iStartDate, iEndDate, iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_Order_SELECT_List(iExpNumber, iOutNumber,
                            iTransportType, iAddress, iContacts, iMpno,
                            iStep, iKeyword, iStartDate, iEndDate, iPageIndex, iPageSize);

            return rtn;
        }



        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_List_UnAllocation(
          string iExpNumber, string iOutNumber,
          int? iTransportType,
          string iAddress, string iContacts, string iMpno,
          int? iStep,
          DateTime? iStartDate, DateTime? iEndDate,
          int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Order_SELECT_List_UnAllocation", iExpNumber, iOutNumber,
                            iTransportType, iAddress, iContacts, iMpno,
                            iStep, iStartDate, iEndDate, iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_Order_SELECT_List_UnAllocation(iExpNumber, iOutNumber,
                            iTransportType, iAddress, iContacts, iMpno,
                            iStep, iStartDate, iEndDate, iPageIndex, iPageSize);

            return rtn;
        }

        /// <summary>
        /// 更新Exp_Order
        /// </summary>
        /// <param name="iExp_OrderDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE(Exp_OrderDC iExp_OrderDC)
        {
            Log_DeBug("Exp_Order_UPDATE", iExp_OrderDC);

            var rtn = ManageExpressInstance.Exp_Order_UPDATE(iExp_OrderDC);

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Order
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_OrderDC> Exp_Order_SELECT_Entity(int iOrderID)
        {
            Log_DeBug("Exp_Order_SELECT_Entity", iOrderID);

            var rtn = ManageExpressInstance.Exp_Order_SELECT_Entity(iOrderID);

            return rtn;
        }

        public ReturnEntity<bool> Exp_Order_Allocation(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser)
        {
            Log_DeBug("Exp_Order_Allocation", iOrderID, iOperatorID, iNodeID, iMuser);

            var rtn = ManageExpressInstance.Exp_Order_Allocation(iOrderID, iOperatorID, iNodeID, iMuser);

            return rtn;
        }

        public ReturnEntity<bool> Exp_Order_Allocation_Forced(int iOrderID, int? iOperatorID, int? iNodeID,
            int iMuser, string iCSRemark)
        {
            Log_DeBug("Exp_Order_Allocation_Forced", iOrderID, iOperatorID, iNodeID, iMuser, iCSRemark);

            var rtn = ManageExpressInstance.Exp_Order_Allocation_Forced(iOrderID, iOperatorID, iNodeID, iMuser, iCSRemark);

            return rtn;
        }

        //设置状态
        public ReturnEntity<bool> Exp_Order_SetStep(int iOrderID, int iStep, string iStepRemark, int iMuser)
        {
            Log_DeBug("Exp_Order_SetStep", iOrderID, iStep, iStepRemark, iMuser);

            var rtn = ManageExpressInstance.Exp_Order_SetStep(iOrderID, iStep, iStepRemark, iMuser);

            return rtn;
        }

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
        public ReturnEntity<RecordCountEntity<Exp_StorageDC>> Exp_Storage_SELECT_List(
               int? iType, string iName,
               DateTime? iStartDate, DateTime? iEndDate,
               int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Storage_SELECT_List", iType, iName, iStartDate, iEndDate, iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_Storage_SELECT_List(iType, iName, iStartDate, iEndDate, iPageIndex, iPageSize);

            return rtn;
        }

        /// <summary>
        /// 在库搜索
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_StorageItemDC>> Exp_StorageItem_SELECT_List(
            int? iStorageID, string iNumber, string iOtherNumber,
            int? iItemType, int? iNodeID, int? iLineID, int? iTargetType, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_StorageItem_SELECT_List", iStorageID, iNumber, iOtherNumber,
                iItemType, iNodeID, iLineID, iTargetType, iStatus, iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_StorageItem_SELECT_List(iStorageID, iNumber, iOtherNumber,
                iItemType, iNodeID, iLineID, iTargetType, iStatus, iPageIndex, iPageSize);

            return rtn;
        }

        /// <summary>
        /// 库存流转搜索
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
        public ReturnEntity<RecordCountEntity<Exp_StorageLogDC>> Exp_StorageLog_SELECT_List(
             int? iStorageID, int? iType, string iNumber, string iOtherNumber,
             DateTime? iStartDate, DateTime? iEndDate,
             int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_StorageLog_SELECT_List", iStorageID, iType, iNumber, iOtherNumber, iStartDate, iEndDate, iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_StorageLog_SELECT_List(iStorageID, iType, iNumber, iOtherNumber, iStartDate, iEndDate, iPageIndex, iPageSize);

            return rtn;
        }

        /// <summary>
        /// 出入库
        /// </summary>
        /// <param name="iItemID"></param>
        /// <param name="iSourceStorageID"></param>
        /// <param name="iTargetStorageID"></param>
        /// <param name="iTargetType"></param>
        /// <param name="iAdmin"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<int> Exp_StorageItem_IO(int iItemID, int iSourceStorageID, int iTargetStorageID, int iTargetType, bool iAdmin, int iMuser)
        {
            Log_DeBug("Exp_StorageItem_IO", iItemID, iSourceStorageID, iTargetStorageID, iTargetType, iAdmin, iMuser);

            ReturnEntity<int> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_StorageItem_IO(iItemID, iSourceStorageID, iTargetStorageID, iTargetType, iAdmin, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_StorageItem_IO", ex);
                rtn = new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_StorageItem_IO");
            }
            if (rtn != null && rtn.Success && rtn.OBJ > 0)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_StorageItem
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_StorageItemDC> Exp_StorageItem_SELECT_Entity(int iID)
        {
            Log_DeBug("Exp_StorageItem_SELECT_Entity", iID);

            var rtn = ManageExpressInstance.Exp_StorageItem_SELECT_Entity(iID);

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_Preson_AccountDC> Exp_Preson_Account_SELECT_Entity_UserID(int iUserID)
        {
            Log_DeBug("Exp_Preson_Account_SELECT_Entity_UserID", iUserID);

            var rtn = ManageExpressInstance.Exp_Preson_Account_SELECT_Entity_UserID(iUserID);

            return rtn;
        }

        /// <summary>
        /// 查询全部Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_Preson_CommissionLogDC>> Exp_Preson_CommissionLog_SELECT_List(
            int? iUserID, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_StorageItem_SELECT_Entity", iUserID, iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_Preson_CommissionLog_SELECT_List(iUserID, iPageIndex, iPageSize);

            return rtn;
        }


        /// <summary>
        /// 查询全部Exp_Preson_CommissionBill
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_Preson_CommissionBillDC>> Exp_Preson_CommissionBill_SELECT_List(
             string iPeriod, string iOperatorName, int? iBillStatus,
             int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Preson_CommissionBill_SELECT_List", iPeriod, iOperatorName, iBillStatus,
               iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_Preson_CommissionBill_SELECT_List(iPeriod, iOperatorName, iBillStatus,
               iPageIndex, iPageSize);

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_Preson_CommissionBillDC> Exp_Preson_CommissionBill_SELECT_Entity(int iID)
        {
            Log_DeBug("Exp_Preson_CommissionBill_SELECT_Entity", iID);

            var rtn = ManageExpressInstance.Exp_Preson_CommissionBill_SELECT_Entity(iID);

            return rtn;
        }

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Preson_CommissionBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iMuser)
        {
            Log_DeBug("Exp_Preson_CommissionBill_UPDATE_Close", iBillID, iRealMoney, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Preson_CommissionBill_UPDATE_Close(iBillID, iRealMoney, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Preson_CommissionBill_UPDATE_Close", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Preson_CommissionBill_UPDATE_Close");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 查询全部Exp_Preson_PaymentBill
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_Preson_PaymentBillDC>> Exp_Preson_PaymentBill_SELECT_List(
          string iPeriod, string iOperatorName, int? iBillStatus,
          int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Preson_PaymentBill_SELECT_List", iPeriod, iOperatorName, iBillStatus,
               iPageIndex, iPageSize);

            var rtn = ManageExpressInstance.Exp_Preson_PaymentBill_SELECT_List(iPeriod, iOperatorName, iBillStatus,
               iPageIndex, iPageSize);

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_Preson_PaymentBillDC> Exp_Preson_PaymentBill_SELECT_Entity(int iID)
        {
            Log_DeBug("Exp_Preson_PaymentBill_SELECT_Entity", iID);

            var rtn = ManageExpressInstance.Exp_Preson_PaymentBill_SELECT_Entity(iID);

            return rtn;
        }

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Preson_PaymentBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iMuser)
        {
            Log_DeBug("Exp_Preson_PaymentBill_UPDATE_Close", iBillID, iRealMoney, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Preson_PaymentBill_UPDATE_Close(iBillID, iRealMoney, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Preson_PaymentBill_UPDATE_Close", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Preson_PaymentBill_UPDATE_Close");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 站点地图展示
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<string>> Base_Node_Map()
        {
            Log_DeBug("Base_Node_Map");

            ReturnEntity<IList<string>> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Base_Node_Map();
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Node_Map", ex);
                rtn = new ReturnEntity<IList<string>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Node_Map");
            }
            return rtn;
        }


        /// <summary>
        /// 新增 Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Exp_Area_ADD(Exp_AreaDC iExp_AreaDC)
        {
            Log_DeBug("Exp_Area_ADD");

            ReturnEntity<int> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Area_ADD(iExp_AreaDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Node_Map", ex);
                rtn = new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Node_Map");
            }
            return rtn;
        }

        /// <summary>
        /// 更新Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Area_UPDATE(Exp_AreaDC iExp_AreaDC)
        {
            Log_DeBug("Exp_Area_UPDATE");

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Area_UPDATE(iExp_AreaDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Node_Map", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Node_Map");
            }
            return rtn;
        }

        /// <summary>
        /// 查询全部Exp_Area
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Exp_AreaDC>> Exp_Area_SELECT_List()
        {
            Log_DeBug("Exp_Area_SELECT_List");

            ReturnEntity<IList<Exp_AreaDC>> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Area_SELECT_List();
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Area_SELECT_List", ex);
                rtn = new ReturnEntity<IList<Exp_AreaDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Area_SELECT_List");
            }
            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Area
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_AreaDC> Exp_Area_SELECT_Entity(int iID)
        {
            Log_DeBug("Exp_Area_SELECT_Entity", iID);

            ReturnEntity<Exp_AreaDC> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Area_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Area_SELECT_Entity", ex);
                rtn = new ReturnEntity<Exp_AreaDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Area_SELECT_Entity");
            }
            return rtn;
        }

        /// <summary>
        /// 佣金导出
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<IList<string[]>> Exp_Preson_CommissionBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Preson_CommissionBill_Export", iPeriod, iPageIndex, iPageSize);

            ReturnEntity<IList<string[]>> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Preson_CommissionBill_Export(iPeriod, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Preson_CommissionBill_Export", ex);
                rtn = new ReturnEntity<IList<string[]>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Preson_CommissionBill_Export");
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
        public ReturnEntity<IList<string[]>> Exp_Preson_PaymentBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Preson_PaymentBill_Export", iPeriod, iPageIndex, iPageSize);

            ReturnEntity<IList<string[]>> rtn = null;

            try
            {
                rtn = ManageExpressInstance.Exp_Preson_PaymentBill_Export(iPeriod, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Preson_PaymentBill_Export", ex);
                rtn = new ReturnEntity<IList<string[]>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Preson_PaymentBill_Export");
            }
            return rtn;
        }

    }
}
