using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract;
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
    public interface IWashOrder
    {
        /// <summary>
        /// 根据订单号查订单
        /// </summary>
        /// <param name="iCredentials"></param>
        /// <param name="iOrderNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_OrderNumber(LoginCredentials iCredentials,
          string iOrderNumber,
          bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
          bool iGetExpress, bool iGetPayment, bool iGetStep);

        /// <summary>
        /// 根据物流订单号查订单
        /// </summary>
        /// <param name="iExpressNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_Express(LoginCredentials iCredentials,
          string iExpressNumber,
          bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
          bool iGetExpress, bool iGetPayment, bool iGetStep);

        /// <summary>
        /// 根据工厂条形码查订单
        /// </summary>
        /// <param name="iCodeNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_FactoryCode(LoginCredentials iCredentials,
          string iCodeNumber,
          bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
          bool iGetExpress, bool iGetPayment, bool iGetStep);


        ///// <summary>
        ///// 工厂出库添加快递单号
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iExpressNumber"></param>
        ///// <returns></returns>
        //[OperationContract]
        //ReturnEntity<bool> Order_Order_CreateSendExpress(LoginCredentials iCredentials,
        //  int iOrderID, string iExpressNumber);

        /// <summary>
        /// 为一键下单订单添加产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Order_Factory_AddProduct(LoginCredentials iCredentials, int iOrderID, IList<Order_ProductDC> iProductList, int iMuser);

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Order_Express_ADD_Factory(int iOrderID, string iExpressNumber, string iRelationID, int iMuser);

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Order_Express_ADD_Factory_Re(int iOrderID, string iExpressNumber, string iRelationID, int iMuser);

        /// <summary>
        /// 订单记录快递单号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Order_Order_FinishExpress(int iOrderID, string iExpressNumber);

        /// <summary>
        /// 修改产品步骤
        /// </summary>
        /// <param name="iOrderProductID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Order_Product_UPDATE_Step(int iOrderID, int iOrderProductID, int iStep);

        /// <summary>
        /// 更新外部编号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iOtherCode"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Order_Product_UPDATE_OtherCode(LoginCredentials iCredentials, int iOrderID, int iOrderProductID, string iOtherCode);

        /// <summary>
        /// 工厂出库
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Order_Product_OutFactory(LoginCredentials iCredentials, int iOrderID, int iOrderProductID, string iExpressNumber, int iMuser);

        //工厂入库条码扫不出
        [OperationContract]
        ReturnEntity<bool> Order_Order_InFactory_FailNotify_NotFound(LoginCredentials iCredentials, string iExpressNumber, int iMuser);


        //工厂入库产品不对
        [OperationContract]
        ReturnEntity<bool> Order_Order_InFactory_FailNotify_ItemError(LoginCredentials iCredentials, string iOrderNumber, string iContent, int iMuser);


        //工厂入库条码扫不出
        [OperationContract]
        ReturnEntity<bool> Order_Order_OutFactory_FailNotify_NotFound(LoginCredentials iCredentials, string iCode, int iMuser);


        //工厂入库产品不对
        [OperationContract]
        ReturnEntity<bool> Order_Order_OutFactory_FailNotify_ItemBad(LoginCredentials iCredentials, string iOrderNumber, string iContent, int iMuser);

    }
}
