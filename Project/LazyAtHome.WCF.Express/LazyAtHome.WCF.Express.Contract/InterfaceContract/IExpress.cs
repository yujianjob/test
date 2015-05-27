using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IExpress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Exp_Node_ADD(Exp_NodeDC iExp_NodeDC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Node_UPDATE(Exp_NodeDC iExp_NodeDC);

        /// <summary>
        /// 根据ID查询Exp_Node
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_NodeDC> Exp_Node_SELECT_Entity(int iID);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_NodeDC>> Exp_Node_SELECT_List(
            int? iAreaID, string iName,
            int? iType,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize, int? iParentID = null);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="iExp_OperatorDC"></param>
        ///// <returns></returns>
        //[OperationContract]
        //ReturnEntity<int> Exp_Operator_ADD(Exp_OperatorDC iExp_OperatorDC);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="iExp_OperatorDC"></param>
        ///// <returns></returns>
        //[OperationContract]
        //ReturnEntity<bool> Exp_Operator_UPDATE(Exp_OperatorDC iExp_OperatorDC);

        ///// <summary>
        ///// 根据ID查询Exp_Operator
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <returns></returns>
        //[OperationContract]
        //ReturnEntity<Exp_OperatorDC> Exp_Operator_SELECT_Entity(int iID);

        ///// <summary>
        ///// 查询全部
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //ReturnEntity<RecordCountEntity<Exp_OperatorDC>> Exp_Operator_SELECT_List(
        //    string iLoginName, string iName, string iMpNo,
        //    int? iType,
        //    string iNodeName,
        //    DateTime? iStartDate, DateTime? iEndDate,
        //    int iPageIndex, int iPageSize);

        /// <summary>
        /// 查询全部Exp_Order
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_List(
            string iExpNumber, string iOutNumber,
            int? iTransportType,
            string iAddress, string iContacts, string iMpno,
            int? iStep,
            string iKeyword,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_List_UnAllocation(
        string iExpNumber, string iOutNumber,
        int? iTransportType,
        string iAddress, string iContacts, string iMpno,
        int? iStep,
        DateTime? iStartDate, DateTime? iEndDate,
        int iPageIndex, int iPageSize);

        /// <summary>
        /// 更新Exp_Order
        /// </summary>
        /// <param name="iExp_OrderDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_UPDATE(Exp_OrderDC iExp_OrderDC);

        /// <summary>
        /// 根据ID查询Exp_Order
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_OrderDC> Exp_Order_SELECT_Entity(int iOrderID);

        /// <summary>
        /// 手工分配
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_Allocation(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser);

        /// <summary>
        /// 强制分配
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iMuser"></param>
        /// <param name="iCSRemark"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_Allocation_Forced(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser, string iCSRemark);

        /// <summary>
        /// 修改步骤
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStep"></param>
        /// <param name="iStepRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_SetStep(int iOrderID, int iStep, string iStepRemark, int iMuser);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_StorageDC>> Exp_Storage_SELECT_List(
               int? iType, string iName,
               DateTime? iStartDate, DateTime? iEndDate,
               int iPageIndex, int iPageSize);

        /// <summary>
        /// 在库搜索
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_StorageItemDC>> Exp_StorageItem_SELECT_List(
            int? iStorageID, string iNumber, string iOtherNumber,
            int? iItemType, int? iNodeID, int? iLineID, int? iTargetType, int? iStatus,
            int iPageIndex, int iPageSize);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_StorageLogDC>> Exp_StorageLog_SELECT_List(
             int? iStorageID, int? iType, string iNumber, string iOtherNumber,
             DateTime? iStartDate, DateTime? iEndDate,
             int iPageIndex, int iPageSize);

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
        [OperationContract]
        ReturnEntity<int> Exp_StorageItem_IO(int iItemID, int iSourceStorageID, int iTargetStorageID, int iTargetType,
          bool iAdmin, int iMuser);

        /// <summary>
        /// 根据ID查询Exp_StorageItem
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_StorageItemDC> Exp_StorageItem_SELECT_Entity(int iID);

        /// <summary>
        /// 根据ID查询Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_Preson_AccountDC> Exp_Preson_Account_SELECT_Entity_UserID(int iUserID);

        /// <summary>
        /// 查询全部Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_Preson_CommissionLogDC>> Exp_Preson_CommissionLog_SELECT_List(
          int? iUserID, int iPageIndex, int iPageSize);



        /// <summary>
        /// 查询全部Exp_Preson_CommissionBill
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_Preson_CommissionBillDC>> Exp_Preson_CommissionBill_SELECT_List(
           string iPeriod, string iOperatorName, int? iBillStatus,
           int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_Preson_CommissionBillDC> Exp_Preson_CommissionBill_SELECT_Entity(int iID);

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Preson_CommissionBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iMuser);

        /// <summary>
        /// 查询全部Exp_Preson_PaymentBill
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_Preson_PaymentBillDC>> Exp_Preson_PaymentBill_SELECT_List(
          string iPeriod, string iOperatorName, int? iBillStatus,
          int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_Preson_PaymentBillDC> Exp_Preson_PaymentBill_SELECT_Entity(int iID);

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Preson_PaymentBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iMuser);

        /// <summary>
        /// 站点地图展示
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<string>> Base_Node_Map();

        /// <summary>
        /// 新增 Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Exp_Area_ADD(Exp_AreaDC iExp_AreaDC);

        /// <summary>
        /// 更新Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Area_UPDATE(Exp_AreaDC iExp_AreaDC);

        /// <summary>
        /// 查询全部Exp_Area
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Exp_AreaDC>> Exp_Area_SELECT_List();

        /// <summary>
        /// 根据ID查询Exp_Area
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_AreaDC> Exp_Area_SELECT_Entity(int iID);

        /// <summary>
        /// 佣金导出
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<string[]>> Exp_Preson_CommissionBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize);

        /// <summary>
        /// 应收货款导出
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<string[]>> Exp_Preson_PaymentBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize);

    }
}
