using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Interface.IDAL
{
    public interface IExpressDAL
    {
        int Exp_Node_ADD(Exp_NodeDC iExp_NodeDC);

        bool Exp_Node_UPDATE(Exp_NodeDC iExp_NodeDC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        OperatorDC PR_Operator_SELECT_BYID(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns></returns>
        OperatorDC PR_Operator_SELECT_BYCode(string iCode);

        /// <summary>
        /// 根据ID查询Exp_Node
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Exp_NodeDC Exp_Node_SELECT_Entity(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<Exp_NodeDC> Exp_Node_SELECT_List_Allocation();

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
        RecordCountEntity<Exp_NodeDC> Exp_Node_SELECT_List(
            int? iAreaID, string iName,
            int? iType, int? iParentID,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize);

        ///// <summary>
        ///// 根据ID查询Exp_Operator
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <returns></returns>
        //Exp_OperatorDC Exp_Operator_SELECT_Entity(int iID);

        //int Exp_Operator_ADD(Exp_OperatorDC iExp_OperatorDC);

        //bool Exp_Operator_UPDATE(Exp_OperatorDC iExp_OperatorDC);

        int Exp_Order_ADD(Exp_OrderDC iExp_OrderDC);

        ///// <summary>
        ///// 查询全部
        ///// </summary>
        ///// <returns></returns>
        //RecordCountEntity<Exp_OperatorDC> Exp_Operator_SELECT_List(
        //  string iLoginName, string iName, string iMpNo,
        //  int? iType,
        //  string iNodeName,
        //  DateTime? iStartDate, DateTime? iEndDate,
        //  int iPageIndex, int iPageSize);

        /// <summary>
        /// 查询全部Exp_Order
        /// </summary>
        /// <returns></returns>
        RecordCountEntity<Exp_OrderDC> Exp_Order_SELECT_List(
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
        RecordCountEntity<Exp_OrderDC> Exp_Order_SELECT_List_UnAllocation(
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
        bool Exp_Order_UPDATE(Exp_OrderDC iExp_OrderDC);

        /// <summary>
        /// 物流更新地址 
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iTransportType"></param>
        /// <param name="iGetDistrictID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iGetContacts"></param>
        /// <param name="iGetMpno"></param>
        /// <param name="iGetExpTime"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_Address(string iOutNumber, int iTransportType,
            int iDistrictID, string iAddress, string iContacts, string iMpno);

        /// <summary>
        /// 根据ID查询Exp_Order
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Exp_OrderDC Exp_Order_SELECT_Entity(int iID);

        /// <summary>
        /// 根据OutNumber查询Exp_Order
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Exp_OrderDC Exp_Order_SELECT_Entity(string iOutNumber, int iTransportType);

        ///// <summary>
        ///// 手动分配
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iOperatorID"></param>
        ///// <param name="iNodeID"></param>
        ///// <param name="iMuser"></param>
        ///// <returns></returns>
        //bool Exp_Order_Allocation(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser);

        ///// <summary>
        ///// 强制分配
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iOperatorID"></param>
        ///// <param name="iNodeID"></param>
        ///// <param name="iMuser"></param>
        ///// <returns></returns>
        //bool Exp_Order_Allocation_Forced(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser);

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStep"></param>
        /// <param name="iStepRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        bool Exp_Order_SetStep(int iOrderID, int iStep, string iStepRemark, int iMuser);

        /// <summary>
        /// 更新物流单号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_ExpNumber(int iOrderID, string iExpNumber);

        /// <summary>
        /// 更新订单收费金额
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee);

        /// <summary>
        /// 更新订单收费金额
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee, string iPackageInfo, int iPackageCount);

        /// <summary>
        /// 物流订单更新成完成
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_Get_Finish(string iOutNumber, string iExpNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Exp_RoutePush_ADD(Exp_RoutePushDC iExp_RoutePushDC);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<Exp_RoutePushDC> Exp_RoutePush_SELECT_List();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRoutePushIDList"></param>
        /// <returns></returns>
        bool Exp_RoutePush_UPDATE_PushStatus(int[] iRoutePushIDList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        bool Exp_Order_Exists_ExpNumber(string iExpNumber);

        bool Exp_Order_Exists_ExpNumberIn12(string iExpNumber);

        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        bool Exp_Order_Get_ExpectTime_Change(int iOrderID, DateTime iExpectTime);

        /// <summary>
        /// 取消物流单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        bool Exp_Order_Cancel(string iOutNumber);

        /// <summary>
        /// 查询未分配订单
        /// </summary>
        /// <returns></returns>
        IList<Exp_OrderDC> Exp_Order_SELECT_List_UnAllocation();

        /// <summary>
        /// 更新经纬度
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_Location(int iOrderID, decimal iLatitude, decimal iLongitude);

        ///// <summary>
        ///// 分配成功
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iNodeID"></param>
        ///// <returns></returns>
        //bool Exp_Order_UPDATE_Allocation(int iOrderID, int iNodeID);

        /// <summary>
        /// 直接到站点下保安
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iAddress"></param>
        /// <returns></returns>
        bool Exp_Order_Allocation_ToOperator(int iOrderID, string iOutNumber, int iNodeID, int iOperatorID, string iAddress);

        /// <summary>
        /// 分配成功
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <returns></returns>
        bool Exp_Order_Allocation_Success(int iOrderID, int iNodeID, string iNodeCode, string iAddress, int iAlarmType);

        /// <summary>
        /// 分配失败
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iAllotStatus"></param>
        /// <returns></returns>
        bool Exp_Order_Allocation_Fail(string iOutNumber);

        /// <summary>
        /// 订单清空重新分配
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        bool Exp_Order_Clear(int iOrderID, string iOutNumber);

        /// <summary>
        /// 更新客服备注
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iCSRemark"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_CSRemark(int iOrderID, string iCSRemark);

        /// <summary>
        /// 物流站点切换
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        bool Exp_Order_NodeChange(string iOutNumber, int iNodeID, int iOperatorID);

        /// <summary>
        /// 物流站点切换
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iTransportType"></param>
        /// <returns></returns>
        bool Exp_Order_NodeChange(string iOutNumber, int iNodeID, int iOperatorID, int iTransportType);

        /// <summary>
        /// 新增 Exp_Storage
        /// </summary>
        /// <param name="iExp_StorageDC"></param>
        /// <returns></returns>
        int Exp_Storage_ADD(Exp_StorageDC iExp_StorageDC);

        /// <summary>
        /// 更新Exp_Storage
        /// </summary>
        /// <param name="iExp_StorageDC"></param>
        /// <returns></returns>
        bool Exp_Storage_UPDATE(Exp_StorageDC iExp_StorageDC);

        /// <summary>
        /// 根据ID查询Exp_Storage
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Exp_StorageDC Exp_Storage_SELECT_Entity(int iID);

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
        RecordCountEntity<Exp_StorageDC> Exp_Storage_SELECT_List(
            int? iType, string iName,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize);


        /// <summary>
        /// 删除Exp_StorageItem
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        bool Exp_StorageItem_DELETE(int iID, int iMuser);

        ///// <summary>
        ///// 在库搜索
        ///// </summary>
        ///// <param name="iStorageID"></param>
        ///// <param name="iNumber"></param>
        ///// <param name="iOtherNumber"></param>
        ///// <param name="iPageIndex"></param>
        ///// <param name="iPageSize"></param>
        ///// <returns></returns>
        //RecordCountEntity<Exp_StorageItemDC> Exp_StorageItem_SELECT_List(
        //    int? iStorageID, string iNumber, string iOtherNumber,
        //    int iPageIndex, int iPageSize);

        /// <summary>
        /// 在库搜索
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iItemType"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iLineID"></param>
        /// <param name="iTargetType"></param>
        /// <param name="iStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<Exp_StorageItemDC> Exp_StorageItem_SELECT_List(
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
        RecordCountEntity<Exp_StorageLogDC> Exp_StorageLog_SELECT_List(
            int? iStorageID, int? iType, string iNumber, string iOtherNumber,
            int? iItemType,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 新品入库
        /// </summary>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iStorageID"></param>
        /// <param name="iItemType"></param>
        /// <param name="iItemName"></param>
        /// <param name="iItemDetail"></param>
        /// <param name="iManagerID"></param>
        /// <param name="iTargetType"></param>
        /// <param name="iMuser"></param>
        /// <param name="iSourceName"></param>
        /// <returns></returns>
        int Exp_StorageItem_In(string iNumber, string iOtherNumber, int iStorageID, int iItemType,
          string iItemName, string iItemDetail, int iManagerID, int iTargetType, int iMuser, string iSourceName);

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
        int Exp_StorageItem_IO(int iItemID, int iSourceStorageID, int iTargetStorageID, int iTargetType,
            bool iAdmin, int iMuser);

        /// <summary>
        /// 根据ID查询Exp_StorageItem
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Exp_StorageItemDC Exp_StorageItem_SELECT_Entity(int iID);

        /// <summary>
        /// 根据ID查询Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Exp_Preson_AccountDC Exp_Preson_Account_SELECT_Entity_UserID(int iUserID);

        /// <summary>
        /// 查询全部Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<Exp_Preson_CommissionLogDC> Exp_Preson_CommissionLog_SELECT_List(
            int? iUserID, int iPageIndex, int iPageSize);

        /// <summary>
        /// 15分钟带邀请的订单未取件完成
        /// </summary>
        /// <returns></returns>
        IList<Exp_OrderDC> Exp_Order_Alarm_UnTakeComplete_Invite();

        /// <summary>
        /// 30分钟未接单
        /// </summary>
        /// <returns></returns>
        IList<string> Exp_Order_Alarm_UnAccept();

        /// <summary>
        /// 60分钟未取件完成
        /// </summary>
        /// <returns></returns>
        IList<string> Exp_Order_Alarm_UnTakeComplete();


        /// <summary>
        /// 30分钟无人接单关闭订单
        /// </summary>
        /// <returns></returns>
        bool Exp_Order_Alarm_UnAccept_Close(int iOrderID);

        /// <summary>
        /// 60分钟未取件完成关闭订单
        /// </summary>
        /// <returns></returns>
        bool Exp_Order_Alarm_UnTakeComplete_Close(int iOrderID);

        /// <summary>
        /// 查询全部Exp_Preson_CommissionBill
        /// </summary>
        /// <returns></returns>
        RecordCountEntity<Exp_Preson_CommissionBillDC> Exp_Preson_CommissionBill_SELECT_List(
            string iPeriod, string iOperatorName, int? iBillStatus,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Exp_Preson_CommissionBillDC Exp_Preson_CommissionBill_SELECT_Entity(int iID);

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        bool Exp_Preson_CommissionBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iBillStatus, int iMuser, int iOperatorID);

        /// <summary>
        /// 查询全部Exp_Preson_PaymentBill
        /// </summary>
        /// <returns></returns>
        RecordCountEntity<Exp_Preson_PaymentBillDC> Exp_Preson_PaymentBill_SELECT_List(
            string iPeriod, string iOperatorName, int? iBillStatus,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Exp_Preson_PaymentBillDC Exp_Preson_PaymentBill_SELECT_Entity(int iID);

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iBillStatus"></param>
        /// <param name="iMuser"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        bool Exp_Preson_PaymentBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iBillStatus, int iMuser, int iOperatorID);

        /// <summary>
        /// 查询订单日志
        /// </summary>
        /// <returns></returns>
        RecordCountEntity<Exp_OrderDC> Exp_Order_SELECT_List_Log(
            int iTargetTimeType,
            int? iTransportType,
            int? iOperatorID,
            int? iLineID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 新增 Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        int Exp_Area_ADD(Exp_AreaDC iExp_AreaDC);

        /// <summary>
        /// 更新Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        bool Exp_Area_UPDATE(Exp_AreaDC iExp_AreaDC);

        /// <summary>
        /// 查询全部Exp_Area
        /// </summary>
        /// <returns></returns>
        IList<Exp_AreaDC> Exp_Area_SELECT_List();

        /// <summary>
        /// 根据ID查询Exp_Area
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Exp_AreaDC Exp_Area_SELECT_Entity(int iID);

        IList<string[]> Exp_Preson_CommissionBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize);
        IList<string[]> Exp_Preson_PaymentBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize);

    }
}
