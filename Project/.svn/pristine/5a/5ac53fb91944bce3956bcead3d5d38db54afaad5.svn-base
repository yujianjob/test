using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using LazyAtHome.WCF.WebService.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Portal
{
    public class InternalExpressPortal : BasePortal, IInternalExpress
    {
        protected LazyAtHome.WCF.WebService.Business.Business.InternalExpress InternalExpressInstance = LazyAtHome.WCF.WebService.Business.Business.InternalExpress.Instance;

        public ReturnEntity<bool> CreateExpressOrder(string iOutNumber,
            int iGetDistrictID, string iGetAddress, string iGetContacts, string iGetMpno, DateTime iGetExpTime,
            int iSendDistrictID, string iSendAddress, string iSendContacts, string iSendMpno, DateTime iSendExpTime,
            string iPackageInfo, int iPackageCount, decimal iChargeFee, string iUserRemark)
        {
            try
            {
                return InternalExpressInstance.CreateExpressOrder(
                    iOutNumber,
                    iGetDistrictID, iGetAddress, iGetContacts, iGetMpno, iGetExpTime,
                    iSendDistrictID, iSendAddress, iSendContacts, iSendMpno, iSendExpTime,
                    iPackageInfo, iPackageCount, iChargeFee, iUserRemark);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 工厂创建物流送货单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_OrderDC> Exp_Order_Create_SendNumber(LoginCredentials iCredentials, string iOutNumber)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<Exp_OrderDC>(-99, "操作员验证错误");
            }

            try
            {
                return InternalExpressInstance.Exp_Order_Create_SendNumber(iOutNumber);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<Exp_OrderDC>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 更新代收货款
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee, string iPackageInfo, int iPackageCount)
        {
            try
            {
                return InternalExpressInstance.Exp_Order_UPDATE_Send_ChargeFee(iOutNumber, iChargeFee, iPackageInfo, iPackageCount);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Get_ExpectTime_Change(string iOutNumber, DateTime iExpectTime)
        {
            try
            {
                return InternalExpressInstance.Exp_Order_Get_ExpectTime_Change(iOutNumber, iExpectTime);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

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
        public ReturnEntity<RecordCountEntity<Exp_NodeDC>> Exp_Node_SELECT_List(
            LoginCredentials iCredentials,
            int? iDistrictID, string iName,
            int? iType,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize, int? iParentID)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<RecordCountEntity<Exp_NodeDC>>(-99, "操作员验证错误");
            }
            try
            {
                return InternalExpressInstance.Exp_Node_SELECT_List(iDistrictID, iName,
                    iType,
                    iStartDate, iEndDate,
                    iPageIndex, iPageSize, iParentID);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<RecordCountEntity<Exp_NodeDC>>(-999, ex.Message);
            }
        }

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
        public ReturnEntity<RecordCountEntity<Exp_StorageItemDC>> Exp_StorageItem_SELECT_List(
            LoginCredentials iCredentials,
            int? iStorageID, string iNumber, string iOtherNumber,
            int? iItemType, int? iNodeID, int? iLineID, int? iTargetType, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<RecordCountEntity<Exp_StorageItemDC>>(-99, "操作员验证错误");
            }

            try
            {
                return InternalExpressInstance.Exp_StorageItem_SELECT_List(
                   iStorageID, iNumber, iOtherNumber,
                   iItemType, iNodeID, iLineID, iTargetType, iStatus, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<RecordCountEntity<Exp_StorageItemDC>>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 入库完成
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete(LoginCredentials iCredentials,
           int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            try
            {
                return InternalExpressInstance.Exp_Storage_FactoryTakeLine_Complete(iStorageID, iOrderNumberList, iMuser);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 上报错误编号
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_ErrorNumber(LoginCredentials iCredentials,
            int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            try
            {
                return InternalExpressInstance.Exp_Storage_FactoryTakeLine_ErrorNumber(iStorageID, iOrderNumberList, iMuser);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 上报错误数量
        /// </summary>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_ErrorCount(LoginCredentials iCredentials, int iStorageID, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            try
            {
                return InternalExpressInstance.Exp_Storage_FactoryTakeLine_ErrorCount(iStorageID, iMuser);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

    }
}
