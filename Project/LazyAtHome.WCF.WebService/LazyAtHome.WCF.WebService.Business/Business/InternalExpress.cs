using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.ClientProxy;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Business
{
    public class InternalExpress
    {
        public InternalExpress()
        {

        }

        static InternalExpress _InternalExpress;
        public static InternalExpress Instance
        {
            get
            {
                if (_InternalExpress == null)
                {
                    _InternalExpress = new InternalExpress();
                }
                return _InternalExpress;
            }
        }


        public ReturnEntity<bool> CreateExpressOrder(string iOutNumber,
            int iGetDistrictID, string iGetAddress, string iGetContacts, string iGetMpno, DateTime iGetExpTime,
            int iSendDistrictID, string iSendAddress, string iSendContacts, string iSendMpno, DateTime iSendExpTime,
            string iPackageInfo, int iPackageCount, decimal iChargeFee, string iUserRemark)
        {
            try
            {
                return WCFInvokeHelper<LazyAtHome.WCF.Express.Contract.ClientProxy.ApiExpressClient>.Invoke<ReturnEntity<bool>>
                     (client => client.Proxy.Exp_Order_Create(
                        iOutNumber,
                        iGetDistrictID, iGetAddress, iGetContacts, iGetMpno, iGetExpTime,
                        iSendDistrictID, iSendAddress, iSendContacts, iSendMpno, iSendExpTime,
                        iPackageInfo, iPackageCount, iChargeFee, iUserRemark, null)
                         );
            }
            catch (Exception ex)
            {
                //Log_Fatal("Internal_CreateExpressOrder:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 工厂创建物流送货单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_OrderDC> Exp_Order_Create_SendNumber(string iOutNumber)
        {
            try
            {
                return WCFInvokeHelper<LazyAtHome.WCF.Express.Contract.ClientProxy.ApiExpressClient>.Invoke<ReturnEntity<Exp_OrderDC>>
                        (client => client.Proxy.Exp_Order_Create_SendNumber(iOutNumber)
                        );
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
                return WCFInvokeHelper<LazyAtHome.WCF.Express.Contract.ClientProxy.ApiExpressClient>.Invoke<ReturnEntity<bool>>
                        (client => client.Proxy.Exp_Order_UPDATE_Send_ChargeFee(iOutNumber, iChargeFee, iPackageInfo, iPackageCount)
                        );
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
                return WCFInvokeHelper<LazyAtHome.WCF.Express.Contract.ClientProxy.ApiExpressClient>.Invoke<ReturnEntity<bool>>
                        (client => client.Proxy.Exp_Order_Get_ExpectTime_Change(iOutNumber, iExpectTime)
                        );
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
            int? iDistrictID, string iName,
            int? iType,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize, int? iParentID)
        {
            return WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_NodeDC>>>
           (client => client.Proxy.Exp_Node_SELECT_List(iDistrictID, iName,
                iType,
                iStartDate, iEndDate,
                iPageIndex, iPageSize, iParentID));
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
            int? iStorageID, string iNumber, string iOtherNumber,
            int? iItemType, int? iNodeID, int? iLineID, int? iTargetType, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            try
            {
                return WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_StorageItemDC>>>
                    (client => client.Proxy.Exp_StorageItem_SELECT_List(
                    iStorageID, iNumber, iOtherNumber,
                    iItemType, iNodeID, iLineID, iTargetType, iStatus, iPageIndex, iPageSize)
                    );
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
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete(int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Exp_Storage_FactoryTakeLine_Complete_Line(iStorageID, iOrderNumberList, iMuser
                    ));
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
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_ErrorNumber(int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber(iStorageID, iOrderNumberList, iMuser
                    ));
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
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_ErrorCount(int iStorageID, int iMuser)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount(iStorageID, iMuser
                    ));
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }
    }
}
