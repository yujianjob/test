using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IInternalExpress
    {

        [OperationContract]
        ReturnEntity<bool> CreateExpressOrder(string iOutNumber,
              int iGetDistrictID, string iGetAddress, string iGetContacts, string iGetMpno, DateTime iGetExpTime,
              int iSendDistrictID, string iSendAddress, string iSendContacts, string iSendMpno, DateTime iSendExpTime,
              string iPackageInfo, int iPackageCount, decimal iChargeFee, string iUserRemark);

        /// <summary>
        /// 工厂创建物流送货单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_OrderDC> Exp_Order_Create_SendNumber(LoginCredentials iCredentials, string iOutNumber);

        /// <summary>
        /// 更新代收货款
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee, string iPackageInfo, int iPackageCount);

        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_Get_ExpectTime_Change(string iOutNumber, DateTime iExpectTime);


        /// <summary>
        /// 站点列表
        /// </summary>
        /// <param name="iDistrictID"></param>
        /// <param name="iName"></param>
        /// <param name="iType"></param>
        /// <param name="iParentID"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_NodeDC>> Exp_Node_SELECT_List(
            LoginCredentials iCredentials,
          int? iDistrictID, string iName,
          int? iType,
          DateTime? iStartDate, DateTime? iEndDate,
          int iPageIndex, int iPageSize, int? iParentID);

        /// <summary>
        /// 在库一览
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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_StorageItemDC>> Exp_StorageItem_SELECT_List(
            LoginCredentials iCredentials,
            int? iStorageID, string iNumber, string iOtherNumber,
            int? iItemType, int? iNodeID, int? iLineID, int? iTargetType, int? iStatus,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 入库完成
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete(
            LoginCredentials iCredentials, int iStorageID, IList<string> iOrderNumberList, int iMuser);

        /// <summary>
        /// 上报错误编号
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_FactoryTakeLine_ErrorNumber(
            LoginCredentials iCredentials, int iStorageID, IList<string> iOrderNumberList, int iMuser);

        /// <summary>
        /// 上报错误数量
        /// </summary>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_FactoryTakeLine_ErrorCount(
            LoginCredentials iCredentials, int iStorageID, int iMuser);
    }
}
