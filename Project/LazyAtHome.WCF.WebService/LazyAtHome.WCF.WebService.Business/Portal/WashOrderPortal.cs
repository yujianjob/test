using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using LazyAtHome.WCF.WebService.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Portal
{
    /// <summary>
    /// 
    /// </summary>
    public class WashOrderPortal : BasePortal, IWashOrder
    {
        protected LazyAtHome.WCF.WebService.Business.Business.WashOrder WashOrderInstance = LazyAtHome.WCF.WebService.Business.Business.WashOrder.Instance;

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
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_OrderNumber(LoginCredentials iCredentials,
            string iOrderNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<Order_OrderDC>(-99, "操作员验证错误");
            }
            return WashOrderInstance.Order_Order_SELECT_Entity_OrderNumber(iOrderNumber,
              iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
        }

        /// <summary>
        /// 根据工厂条形码查订单
        /// </summary>
        /// <param name="iExpressNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_Express(LoginCredentials iCredentials,
            string iExpressNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<Order_OrderDC>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Order_SELECT_Entity_Express(iExpressNumber,
              iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
        }

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
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_FactoryCode(LoginCredentials iCredentials,
            string iCodeNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<Order_OrderDC>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Order_SELECT_Entity_FactoryCode(iCodeNumber,
              iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
        }

        ///// <summary>
        ///// 工厂出库添加快递单号
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iExpressNumber"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> Order_Order_CreateSendExpress(LoginCredentials iCredentials,
        //    int iOrderID, string iExpressNumber)
        //{
        //    if (!base.LoginCredentials_Check(iCredentials))
        //    {
        //        return new ReturnEntity<bool>(-99, "操作员验证错误");
        //    }

        //    return WashOrderInstance.Order_Order_CreateSendExpress(iOrderID, iExpressNumber);
        //}

        /// <summary>
        /// 为一键下单订单添加产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Factory_AddProduct(LoginCredentials iCredentials, int iOrderID, IList<Order_ProductDC> iProductList, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Factory_AddProduct(iOrderID, iProductList, iMuser);
        }

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Express_ADD_Factory(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            return WashOrderInstance.Order_Express_ADD_Factory(iOrderID, iExpressNumber, iRelationID, iMuser);
        }

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Express_ADD_Factory_Re(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            return WashOrderInstance.Order_Express_ADD_Factory_Re(iOrderID, iExpressNumber, iRelationID, iMuser);
        }

        /// <summary>
        /// 订单记录快递单号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_FinishExpress(int iOrderID, string iExpressNumber)
        {
            return WashOrderInstance.Order_Order_FinishExpress(iOrderID, iExpressNumber);
        }


        /// <summary>
        /// 修改产品步骤
        /// </summary>
        /// <param name="iOrderProductID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_Step(int iOrderID, int iOrderProductID, int iStep)
        {
            return WashOrderInstance.Order_Product_UPDATE_Step(iOrderID, iOrderProductID, iStep);
        }

        /// <summary>
        /// 更新外部编号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iOtherCode"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_OtherCode(LoginCredentials iCredentials, int iOrderID, int iOrderProductID, string iOtherCode)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Product_UPDATE_OtherCode(iOrderID, iOrderProductID, iOtherCode);
        }

        /// <summary>
        /// 工厂出库
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<int> Order_Product_OutFactory(LoginCredentials iCredentials, int iOrderID, int iOrderProductID, string iExpressNumber, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<int>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Product_OutFactory(iOrderID, iOrderProductID, iExpressNumber, iMuser);
        }

        //工厂入库条码扫不出
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_NotFound(LoginCredentials iCredentials, string iExpressNumber, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Order_InFactory_FailNotify_NotFound(iExpressNumber, iMuser);
        }

        //工厂入库产品不对
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_ItemError(LoginCredentials iCredentials, string iOrderNumber, string iContent, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Order_InFactory_FailNotify_ItemError(iOrderNumber, iContent, iMuser);
        }

        //工厂入库条码扫不出
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_NotFound(LoginCredentials iCredentials, string iCode, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Order_OutFactory_FailNotify_NotFound(iCode, iMuser);
        }

        //工厂入库产品不对
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_ItemBad(LoginCredentials iCredentials, string iOrderNumber, string iContent, int iMuser)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<bool>(-99, "操作员验证错误");
            }

            return WashOrderInstance.Order_Order_OutFactory_FailNotify_ItemBad(iOrderNumber, iContent, iMuser);
        }
    }
}
