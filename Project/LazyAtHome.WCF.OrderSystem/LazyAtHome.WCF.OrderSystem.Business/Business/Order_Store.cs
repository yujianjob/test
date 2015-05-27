using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Business.Business
{
    public partial class Order
    {
        #region 合作门店

        /// <summary>
        /// 门店订单生成
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_Create_Store(Guid iStoreID, Order_ConsigneeAddressDC iGetAddress)
        {
            //查询是否存在未提交订单
            var order = orderDAL.Order_Order_SELECT_Entity(iStoreID);
            if (order != null)
            {
                return new ReturnEntity<Order_OrderDC>(order);
            }
            else
            {
                iGetAddress.Type = (int)ConsigneeAddressType.Send;
                order = new Order_OrderDC()
                {
                    OrderClass = (int)OrderClass.Store,
                    OrderType = (int)OrderType.Normal,
                    UserID = iStoreID,
                    Title = "合作门店订单",
                    TotalAmount = 0,
                    PayAmount = 0,
                    OrderStatus = (int)OrderStatus.Create,
                    Site = 0,
                    Channel = (int)Channel.PartnerStore,
                    GetAddress = iGetAddress,
                    SendAddress = new Order_ConsigneeAddressDC()
                    {
                        Consignee = Config.FactoryAddress.Contact,
                        Address = Config.FactoryAddress.Address,
                        CityName = Config.FactoryAddress.City,
                        Email = string.Empty,
                        Mpno = Config.FactoryAddress.Phone,
                        Phone = Config.FactoryAddress.Tel,
                        Type = (int)ConsigneeAddressType.Get,
                        ProvinceName = Config.FactoryAddress.Province,
                        ZipCode = Config.FactoryAddress.ZipCode,
                    },
                };

                var orderid = orderDAL.Order_Order_ADD(order);

                return new ReturnEntity<Order_OrderDC>(orderDAL.Order_Order_SELECT_Entity(iStoreID));
            }
        }

        /// <summary>
        /// 根据门店ID查询订单主表
        /// </summary>
        /// <param name="iStoreID">门店ID</param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity(Guid iStoreID)
        {
            return new ReturnEntity<Order_OrderDC>(orderDAL.Order_Order_SELECT_Entity(iStoreID));
        }

        /// <summary>
        /// 门店订单添加产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_ADD_Store(int iOrderID, IList<Order_ProductDC> iProductList)
        {
            foreach (var item in iProductList)
            {
                var entity = orderDAL.Wash_Product_SELECT_Entity(item.ProductID);
                if (entity == null)
                {
                    return new ReturnEntity<bool>(-1, "产品不存在");
                }

                item.OID = iOrderID;
                item.ProductID = entity.ID;
                item.Price = entity.SalesPrice;
                item.Name = entity.Name;
                item.Type = 1;

                orderDAL.Order_Product_ADD(item);
            }
            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 门店订单删除产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_DELETE_Store(int iOrderID, int iOrderProductID)
        {
            return new ReturnEntity<bool>(orderDAL.Order_Product_DELETE_Store(iOrderID, iOrderProductID));
        }

        /// <summary>
        /// iOrderID
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_Submit_Store(int iOrderID, string iExpressNumber)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, true, false, true, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单不存在");
            }
            if (order.OrderType != (int)OrderType.Normal)
            {
                return new ReturnEntity<bool>(-1, "订单状态错误OrderType");
            }
            if (order.OrderClass != (int)OrderClass.Store)
            {
                return new ReturnEntity<bool>(-1, "订单状态错误OrderClass");
            }
            if (order.OrderStatus == (int)OrderStatus.Finish)
            {
                return new ReturnEntity<bool>(true);
            }
            if (order.OrderStatus != (int)OrderStatus.Create)
            {
                return new ReturnEntity<bool>(-1, "订单状态错误OrderStatus");
            }
            if (order.ProductList == null || order.ProductList.Count == 0)
            {
                return new ReturnEntity<bool>(-1, "订单无产品");
            }

            //更新主单信息
            orderDAL.Order_Order_UPDATE_StoreFinish(order.ID, "门店订单(" + order.ProductList.Count + "件)",
                order.ProductList.Sum(p => p.Price), iExpressNumber);

            //快递推送
            var expressNumber = Order_Express_Create_QF(order.OrderNumber, iExpressNumber,
                order.GetAddress, ExpressType.Get, order.ProductList.Count);

            //快递信息记录
            orderDAL.Order_Express_ADD(new Order_ExpressDC()
            {
                Code = iExpressNumber,
                Type = (int)ExpressType.Get,
                Content = "取件",
                OID = order.ID,
            });

            //步骤添加
            orderDAL.Order_Step_ADD(order.ID, WashStepType.GetPackage, "取件中");

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 快递推送(全峰)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iExpressType"></param>
        /// <param name="iProductCount"></param>
        /// <returns></returns>
        private bool Order_Express_Create_QF(string iOrderNumber, string iExpressNumber,
          Order_ConsigneeAddressDC iGetAddress,
          ExpressType iExpressType, int iProductCount)
        {
            throw new Exception("已取消");
            
            //ReturnEntity<bool> rtn = null;

            //try
            //{
            //    //取件
            //    if (iExpressType == ExpressType.Get)
            //    {
            //        rtn = QFOrderService.CreateOrder(iOrderNumber, iExpressNumber
            //            , iGetAddress.Consignee, iGetAddress.ZipCode, iGetAddress.Phone
            //            , iGetAddress.Mpno, iGetAddress.ProvinceName, iGetAddress.CityName, iGetAddress.Address
            //            , Config.FactoryAddress.Contact, Config.FactoryAddress.ZipCode, Config.FactoryAddress.Tel
            //            , Config.FactoryAddress.Phone, Config.FactoryAddress.Province, Config.FactoryAddress.City
            //            , Config.FactoryAddress.Address
            //            , new Dictionary<string, int>() { { "衣物", iProductCount } });
            //    }
            //    //送件
            //    else
            //    {
            //        rtn = QFOrderService.CreateOrder(iOrderNumber, iExpressNumber
            //              , Config.FactoryAddress.Contact, Config.FactoryAddress.ZipCode, Config.FactoryAddress.Tel
            //            , Config.FactoryAddress.Phone, Config.FactoryAddress.Province, Config.FactoryAddress.City
            //            , Config.FactoryAddress.Address
            //             , iGetAddress.Consignee, iGetAddress.ZipCode, iGetAddress.Phone
            //            , iGetAddress.Mpno, iGetAddress.ProvinceName, iGetAddress.CityName, iGetAddress.Address
            //             , new Dictionary<string, int>() { { "衣物", iProductCount } });
            //    }
            //    if (rtn.Success && rtn.OBJ == true)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        Log_Fatal("全峰快递推送失败!订单号:" + iOrderNumber + " 物流号:" + iExpressNumber
            //               + " 错误内容:" + rtn.Message);
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log_Fatal("全峰快递推送失败!订单号:" + iOrderNumber + " 物流号:" + iExpressNumber
            //            + " 错误内容:" + ex.Message);
            //    return true;
            //}
        }

        /// <summary>
        /// 门店签收用户订单产品
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iUserName"></param>
        /// <param name="iUserSignType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_UserSignType(int iOrderProductID, UserSignType iUserSignType)
        {
            return new ReturnEntity<bool>(orderDAL.Order_Product_UPDATE_UserSignType(iOrderProductID, iUserSignType));
        }

        /// <summary>
        /// 门店查询用户未签收订单产品
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iUserName"></param>
        /// <param name="iUserSignType"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Order_ProductDC>> Order_Product_SELECT_List_Store(Guid iStoreID, string iUserMPNo,
            string iUserName, UserSignType? iUserSignType, DateTime iStartDate, DateTime iEndDate)
        {
            return new ReturnEntity<IList<Order_ProductDC>>(orderDAL.Order_Product_SELECT_List_Store(iStoreID, iUserMPNo,
                iUserName, iUserSignType, iStartDate, iEndDate));
        }

        #endregion
    }
}
