using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.Express.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Portal
{
    public partial class ExpressPortal : IApiExpress
    {
        protected LazyAtHome.WCF.Express.Business.Business.ApiExpress ApiExpressInstance = LazyAtHome.WCF.Express.Business.Business.ApiExpress.Instance;


        public ReturnEntity<bool> Exp_Order_Create(string iOutNumber,
            int iGetDistrictID, string iGetAddress, string iGetContacts, string iGetMpno, DateTime iGetExpTime,
            int iSendDistrictID, string iSendAddress, string iSendContacts, string iSendMpno, DateTime iSendExpTime,
            string iPackageInfo, int iPackageCount, decimal iChargeFee, string iUserRemark, string iInviteCode
           )
        {
            Log_DeBug("Exp_Order_Create", iOutNumber,
             iGetDistrictID, iGetAddress, iGetContacts, iGetMpno, iGetExpTime,
             iSendDistrictID, iSendAddress, iSendContacts, iSendMpno, iSendExpTime,
              iPackageInfo, iPackageCount, iChargeFee, iUserRemark, iInviteCode);

            var rtn = ApiExpressInstance.Exp_Order_Create(iOutNumber,
             iGetDistrictID, iGetAddress, iGetContacts, iGetMpno, iGetExpTime,
             iSendDistrictID, iSendAddress, iSendContacts, iSendMpno, iSendExpTime,
              iPackageInfo, iPackageCount, iChargeFee, iUserRemark, iInviteCode);

            return rtn;
        }

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
        public ReturnEntity<bool> Exp_Order_UPDATE_Address(string iOutNumber, int iTransportType,
              int iDistrictID, string iAddress, string iContacts, string iMpno)
        {
            Log_DeBug("Exp_Order_UPDATE_Address", iOutNumber, iTransportType, iDistrictID, iAddress, iContacts, iMpno);

            var rtn = ApiExpressInstance.Exp_Order_UPDATE_Address(iOutNumber, iTransportType, iDistrictID, iAddress, iContacts, iMpno);

            return rtn;
        }

        /// <summary>
        /// 工厂创建物流送货单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_OrderDC> Exp_Order_Create_SendNumber(string iOutNumber)
        {
            Log_DeBug("Exp_Order_Create_SendNumber", iOutNumber);

            var rtn = ApiExpressInstance.Exp_Order_Create_SendNumber(iOutNumber);

            return rtn;
        }

        /// <summary>
        /// 更新代收货款
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee)
        {
            Log_DeBug("Exp_Order_UPDATE_Send_ChargeFee", iOutNumber, iChargeFee);

            var rtn = ApiExpressInstance.Exp_Order_UPDATE_Send_ChargeFee(iOutNumber, iChargeFee);

            return rtn;
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
            Log_DeBug("Exp_Order_UPDATE_Send_ChargeFee", iOutNumber, iChargeFee, iPackageInfo, iPackageCount);

            var rtn = ApiExpressInstance.Exp_Order_UPDATE_Send_ChargeFee(iOutNumber, iChargeFee, iPackageInfo, iPackageCount);

            return rtn;
        }

        /// <summary>
        /// 物流订单更新成完成
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Get_Finish(string iOutNumber, string iExpNumber)
        {
            Log_DeBug("Exp_Order_UPDATE_Get_Finish", iOutNumber, iExpNumber);

            var rtn = ApiExpressInstance.Exp_Order_UPDATE_Get_Finish(iOutNumber, iExpNumber);

            return rtn;
        }

        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Get_ExpectTime_Change(string iOutNumber, DateTime iExpectTime)
        {
            Log_DeBug("Exp_Order_Get_ExpectTime_Change", iOutNumber, iExpectTime);

            var rtn = ApiExpressInstance.Exp_Order_Get_ExpectTime_Change(iOutNumber, iExpectTime);

            return rtn;
        }

        /// <summary>
        /// 取消物流单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Cancel(string iOutNumber)
        {
            Log_DeBug("Exp_Order_Cancel", iOutNumber);

            var rtn = ApiExpressInstance.Exp_Order_Cancel(iOutNumber);

            return rtn;
        }

        /// <summary>
        /// 物流站点切换
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_NodeChange(string iOutNumber, int iNodeType)
        {
            Log_DeBug("Exp_Order_NodeChange", iOutNumber);

            var rtn = ApiExpressInstance.Exp_Order_NodeChange(iOutNumber, iNodeType);

            return rtn;
        }

        /// <summary>
        /// 订单分拣拆包入库
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iStorageItemList"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_StorageItem_BreakPackage(string iOutNumber,
            IList<Exp_StorageItemDC> iStorageItemList, int iOperatorID)
        {
            Log_DeBug("Exp_StorageItem_BreakPackage", iOutNumber);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Exp_StorageItem_BreakPackage(iOutNumber, iStorageItemList, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_StorageItem_BreakPackage", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_StorageItem_BreakPackage");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }


        /// <summary>
        /// 工厂打包出库
        /// </summary>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iOutType">1:一起出库 2:单件出库</param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_StorageItem_FactoryPreOut(string iNumber,
            string iOtherNumber, int iOutType, int iOperatorID)
        {
            Log_DeBug("Exp_StorageItem_FactoryPreOut", iNumber, iOtherNumber, iOutType, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Exp_StorageItem_FactoryPreOut(iNumber, iOtherNumber, iOutType, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_StorageItem_FactoryPreOut", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_StorageItem_FactoryPreOut");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 检查地址是否可下单
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public ReturnEntity<bool> CheckNode_Address(string iAddress)
        {
            Log_DeBug("CheckNode_Address", iAddress);

            ReturnEntity<bool> rtn = null;

            try
            {
                var node = LazyAtHome.WCF.Express.Business.Business.Allocation.Instance.GetNode_Address(iAddress);
                if (node != null)
                {
                    Console.WriteLine("CheckNode_Address\t正常");
                    return new ReturnEntity<bool>(true);
                }
                else
                {
                    Console.WriteLine("CheckNode_Address\t不在范围内");
                    return new ReturnEntity<bool>(false);
                }
            }
            catch (Exception ex)
            {
                Log_Fatal("CheckNode_Address", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("CheckNode_Address");
            }
            return rtn;
        }

        /// <summary>
        /// 获取地址站点
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_NodeDC> GetNode_Address(string iAddress)
        {
            Log_DeBug("GetNode_Address", iAddress);

            ReturnEntity<Exp_NodeDC> rtn = null;

            try
            {
                var node = LazyAtHome.WCF.Express.Business.Business.Allocation.Instance.GetNode_Address(iAddress);
                if (node != null)
                {
                    return new ReturnEntity<Exp_NodeDC>(node);
                }
                else
                {
                    return new ReturnEntity<Exp_NodeDC>(-1, "未找到站点");
                }
            }
            catch (Exception ex)
            {
                Log_Fatal("GetNode_Address", ex);
                rtn = new ReturnEntity<Exp_NodeDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("GetNode_Address");
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_NodeDC> GetNode_Location(decimal iLatitude, decimal iLongitude)
        {
            Log_DeBug("GetNode_Location", iLatitude, iLongitude);

            ReturnEntity<Exp_NodeDC> rtn = null;

            try
            {
                var node = LazyAtHome.WCF.Express.Business.Business.Allocation.Instance.GetNode_Location(iLatitude, iLongitude);
                if (node != null)
                {
                    return new ReturnEntity<Exp_NodeDC>(node);
                }
                else
                {
                    return new ReturnEntity<Exp_NodeDC>(-1, "未找到站点");
                }
            }
            catch (Exception ex)
            {
                Log_Fatal("GetNode_Location", ex);
                rtn = new ReturnEntity<Exp_NodeDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("GetNode_Location");
            }
            return rtn;
        }

        //工厂入库条码扫不出
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_NotFound(string iExpressNumber, int iMuser)
        {
            Log_DeBug("Order_Order_InFactory_FailNotify_NotFound", iExpressNumber, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Order_Order_InFactory_FailNotify_NotFound(iExpressNumber, iMuser);

            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_InFactory_FailNotify_NotFound", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_InFactory_FailNotify_NotFound");
            }
            return rtn;
        }

        //工厂入库产品不对
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_ItemError(string iOrderNumber, string iContent, int iMuser)
        {
            Log_DeBug("Order_Order_InFactory_FailNotify_ItemError", iOrderNumber, iContent, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Order_Order_InFactory_FailNotify_ItemError(iOrderNumber, iContent, iMuser);

            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_InFactory_FailNotify_ItemError", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_InFactory_FailNotify_ItemError");
            }
            return rtn;
        }

        //工厂入库条码扫不出
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_NotFound(string iCode, int iMuser)
        {
            Log_DeBug("Order_Order_OutFactory_FailNotify_NotFound", iCode, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Order_Order_OutFactory_FailNotify_NotFound(iCode, iMuser);

            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_OutFactory_FailNotify_NotFound", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_OutFactory_FailNotify_NotFound");
            }
            return rtn;
        }

        //工厂入库产品不对
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_ItemBad(string iOrderNumber, string iContent, int iMuser)
        {
            Log_DeBug("Order_Order_OutFactory_FailNotify_ItemBad", iOrderNumber, iContent, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Order_Order_OutFactory_FailNotify_ItemBad(iOrderNumber, iContent, iMuser);

            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_OutFactory_FailNotify_ItemBad", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_OutFactory_FailNotify_ItemBad");
            }
            return rtn;
        }



        /// <summary>
        /// 订单刷新(修改产品内容)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Storage_OrderReNew(string iOrderNumber, int iMuser)
        {
            Log_DeBug("Exp_Storage_OrderReNew", iOrderNumber, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Exp_Storage_OrderReNew(iOrderNumber, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_OrderReNew", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_OrderReNew");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 工厂揽干线完成
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete_Line(int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            Log_DeBug("Exp_Storage_FactoryTakeLine_Complete_Line", iStorageID, iOrderNumberList, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Exp_Storage_FactoryTakeLine_Complete_Line(iStorageID, iOrderNumberList, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_FactoryTakeLine_Complete_Line", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_FactoryTakeLine_Complete_Line");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 工厂出库产品不对
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber(int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            Log_DeBug("Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber", iStorageID, iOrderNumberList, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber(iStorageID, iOrderNumberList, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber");
            }
            return rtn;
        }

        /// <summary>
        /// 工厂出库产品不对
        /// </summary>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount(int iStorageID, int iMuser)
        {
            Log_DeBug("Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount", iStorageID, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = ApiExpressInstance.Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount(iStorageID, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount");
            }
            return rtn;
        }

    }
}
