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
    public interface IApiExpress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iGetDistrictID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iGetContacts"></param>
        /// <param name="iGetMpno"></param>
        /// <param name="iGetExpTime"></param>
        /// <param name="iSendDistrictID"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iSendContacts"></param>
        /// <param name="iSendMpno"></param>
        /// <param name="iSendExpTime"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iUserRemark"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_Create(string iOutNumber,
            int iGetDistrictID, string iGetAddress, string iGetContacts, string iGetMpno, DateTime iGetExpTime,
            int iSendDistrictID, string iSendAddress, string iSendContacts, string iSendMpno, DateTime iSendExpTime,
            string iPackageInfo, int iPackageCount, decimal iChargeFee, string iUserRemark, string iInviteCode
            );

        /// <summary>
        /// 工厂创建物流送货单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_OrderDC> Exp_Order_Create_SendNumber(string iOutNumber);

        /// <summary>
        /// 更新代收货款
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <returns></returns>
        [OperationContract(Name = "Exp_Order_UPDATE_Send_ChargeFee_Simple")]
        ReturnEntity<bool> Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee);

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
        /// 物流订单更新成完成
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_UPDATE_Get_Finish(string iOutNumber, string iExpNumber);

        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_Get_ExpectTime_Change(string iOutNumber, DateTime iExpectTime);

        /// <summary>
        /// 取消物流单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_Cancel(string iOutNumber);

        /// <summary>
        /// 物流站点切换
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeType"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_NodeChange(string iOutNumber, int iNodeType);

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
        [OperationContract]
        ReturnEntity<bool> Exp_Order_UPDATE_Address(string iOutNumber, int iTransportType,
            int iDistrictID, string iAddress, string iContacts, string iMpno);

        /// <summary>
        /// 订单分拣拆包入库
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iStorageItemList"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_StorageItem_BreakPackage(string iOutNumber, IList<Exp_StorageItemDC> iStorageItemList,
            int iOperatorID);

        /// <summary>
        /// 工厂打包出库
        /// </summary>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iOutType">1:一起出库 2:单件出库</param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_StorageItem_FactoryPreOut(string iNumber,
            string iOtherNumber, int iOutType, int iOperatorID);

        /// <summary>
        /// 检查地址是否可下单
        /// </summary>
        /// <param name="iAddress"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> CheckNode_Address(string iAddress);

        /// <summary>
        /// 获取地址站点
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_NodeDC> GetNode_Address(string iAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Exp_NodeDC> GetNode_Location(decimal iLatitude, decimal iLongitude);

        //工厂入库条码扫不出
        [OperationContract]
        ReturnEntity<bool> Order_Order_InFactory_FailNotify_NotFound(string iExpressNumber, int iMuser);

        //工厂入库产品不对
        [OperationContract]
        ReturnEntity<bool> Order_Order_InFactory_FailNotify_ItemError(string iOrderNumber, string iContent, int iMuser);


        //工厂入库条码扫不出
        [OperationContract]
        ReturnEntity<bool> Order_Order_OutFactory_FailNotify_NotFound(string iCode, int iMuser);


        //工厂入库产品不对
        [OperationContract]
        ReturnEntity<bool> Order_Order_OutFactory_FailNotify_ItemBad(string iOrderNumber, string iContent, int iMuser);

        /// <summary>
        /// 订单刷新(修改产品内容)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_OrderReNew(string iOrderNumber, int iMuser);

        /// <summary>
        /// 工厂揽干线完成
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete_Line(int iStorageID, IList<string> iOrderNumberList, int iMuser);

        /// <summary>
        /// 工厂出库产品不对
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber(int iStorageID, IList<string> iOrderNumberList, int iMuser);

        /// <summary>
        /// 工厂出库产品不对
        /// </summary>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount(int iStorageID, int iMuser);

    }
}
