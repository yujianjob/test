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
        /// <summary>
        /// 更新订单收发货地址
        /// </summary>
        /// <param name="iOrder_ConsigneeAddress"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_ConsigneeAddress_UPDATE(Order_ConsigneeAddressDC iOrder_ConsigneeAddress)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrder_ConsigneeAddress.OID, false, false, true, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单不存在");
            }

            order.GetAddress.DistrictID = iOrder_ConsigneeAddress.DistrictID;
            order.GetAddress.Address = iOrder_ConsigneeAddress.Address;
            order.GetAddress.Consignee = iOrder_ConsigneeAddress.Consignee;
            order.GetAddress.Mpno = iOrder_ConsigneeAddress.Mpno;


            order.SendAddress.DistrictID = iOrder_ConsigneeAddress.DistrictID;
            order.SendAddress.Address = iOrder_ConsigneeAddress.Address;
            order.SendAddress.Consignee = iOrder_ConsigneeAddress.Consignee;
            order.SendAddress.Mpno = iOrder_ConsigneeAddress.Mpno;

            var rtn = orderDAL.Order_ConsigneeAddress_UPDATE(order.GetAddress);
            rtn = orderDAL.Order_ConsigneeAddress_UPDATE(order.SendAddress);
            if (rtn)
            {
                Internal_ExpressAddressChange(order.OrderNumber, iOrder_ConsigneeAddress.Type,
                    iOrder_ConsigneeAddress.DistrictID, iOrder_ConsigneeAddress.Address,
                    iOrder_ConsigneeAddress.Consignee, iOrder_ConsigneeAddress.Mpno);
            }

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 一键下单订单审核
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iAuditStatus"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_Audit(int iOrderID, bool iAuditStatus, int iMuser)
        {
            //获取订单
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, false, false, false, false, true, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            //未完成订单,订单类型错误
            if (order.OrderClass != (int)OrderClass.OneKey || order.OrderStatus != 0)
            {
                return new ReturnEntity<bool>(-2, "订单状态错误");
            }

            var rtn = orderDAL.Order_Order_Audit(iOrderID, iAuditStatus, iMuser);

            //审核成功
            if (rtn && iAuditStatus)
            {
                return Order_Order_Finish(order.OrderNumber);
            }
            else
            {
                //退还优惠券
                if (order.PaymentList != null && order.PaymentList.Count > 0)
                {
                    foreach (var item in order.PaymentList)
                    {
                        if (item.PayMoneyType == (int)PayMoneyType.Coupon)
                        {
                            var userCouponID = int.Parse(item.RelationID);

                            orderDAL.User_Coupon_Back(order.UserID, userCouponID);
                        }
                    }
                }
            }

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_Cancel(int iID, OrderStatus iOrderStatus)
        {
            if (iOrderStatus != OrderStatus.SystemChannel && iOrderStatus != OrderStatus.UserChannel)
            {
                return new ReturnEntity<bool>(-1, "参数错误");
            }

            var order = orderDAL.Order_Order_SELECT_Entity(iID, false, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            if (order.OrderStatus == (int)OrderStatus.SystemChannel || order.OrderStatus == (int)OrderStatus.UserChannel)
            {
                //订单已退单
                return new ReturnEntity<bool>(true);
            }
            else if (order.OrderStatus == (int)OrderStatus.Create || order.OrderStatus == (int)OrderStatus.Unaudited)
            {
                //正常
            }
            else
            {
                return new ReturnEntity<bool>(-1, "订单状态错误");
            }
            if ((order.OrderType != (int)OrderType.Normal && order.OrderType != (int)OrderType.Part && order.OrderType != (int)OrderType.Return))
            {
                return new ReturnEntity<bool>(-1, "订单状态错误");
            }

            //生成退单
            var rtn = orderDAL.Order_Order_Cancel(order.OrderNumber, iOrderStatus);
            switch (rtn)
            {
                case 1:
                    return new ReturnEntity<bool>(true);
                case -1:
                    return new ReturnEntity<bool>(-2, "订单不存在或状态错误");
                case -2:
                    return new ReturnEntity<bool>(-2, "已用其他方式支付,无法退单");
                case -3:
                    return new ReturnEntity<bool>(-2, "余额退还失败");
                case -4:
                    return new ReturnEntity<bool>(-2, "懒人卡退还失败");
                default:
                    return new ReturnEntity<bool>(-2, "无法识别的错误" + rtn);
            }
        }

        /// <summary>
        /// 退单
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iChannel"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_ChargeBack(int iID, Channel iChannel)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iID, false, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            if (order.OrderStatus == (int)OrderStatus.ChargeBack)
            {
                //订单已退单
                return new ReturnEntity<bool>(true);
            }

            //生成退单
            var rtn = orderDAL.Order_Order_ChargeBack(order.OrderNumber, iChannel);
            switch (rtn)
            {
                case 1:

                    //物流退单
                    Internal_ExpressCancel(order.OrderNumber);

                    ////订单已推送物流,需要退单
                    //if (!string.IsNullOrWhiteSpace(order.GetExpressNumber)
                    //     && (order.OrderClass == (int)OrderClass.OneKey || order.OrderClass == (int)OrderClass.Normal)
                    //    && (order.OrderType == (int)OrderType.Normal || order.OrderType == (int)OrderType.Normal
                    //        || order.OrderType == (int)OrderType.Normal)
                    //    && (order.Step == (int)WashStepType.GetPackage || order.Step == (int)WashStepType.SendFactory)
                    //    )
                    //{

                    //    //SF_CancelOrder(order.GetExpressNumber);
                    //}
                    return new ReturnEntity<bool>(true);
                case -1:
                    return new ReturnEntity<bool>(-2, "订单不存在");
                case -2:
                    return new ReturnEntity<bool>(-2, "订单状态错误");
                case -3:
                    return new ReturnEntity<bool>(-2, "余额退还失败");
                case -4:
                    return new ReturnEntity<bool>(-2, "懒人卡退还失败");
                default:
                    return new ReturnEntity<bool>(-2, "无法识别的错误" + rtn);
            }
        }

        /// <summary>
        /// 返洗
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iChannel"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_Return(int iID, IList<int> iPidList)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iID, true, false, true, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            if ((order.OrderType != 1 && order.OrderType != 2 && order.OrderType != 3) || order.OrderStatus != 2)
            {
                return new ReturnEntity<bool>(-2, "订单状态错误");
            }
            //ReturnStatus

            //生成返洗订单

            var productList = new List<Order_ProductDC>();

            foreach (var item in order.ProductList)
            {
                if (item.ReturnStatus != 0) continue;

                if (iPidList.Contains(item.ID))
                {
                    productList.Add(new Order_ProductDC()
                    {
                        ProductID = item.ProductID,
                        Name = item.Name,
                        Type = item.Type,
                        Price = item.Price,
                        Attribute = item.Attribute,
                        Code = item.Code,
                        ReturnStatus = 0,
                    });

                    //修改主订单状态
                    orderDAL.Order_Product_UPDATE_ReturnStatus(item.ID);
                }
            }

            var returnOrder = new Order_OrderDC()
            {
                OrderClass = order.OrderClass,
                OrderType = 3,
                OrderStatus = 1,
                UserID = order.UserID,
                Title = productList.Count == 1
                        ? productList.First().Name : productList.First().Name + " 等(" + productList.Count + ")",
                TotalAmount = 0,
                PayAmount = 0,
                RelationID = order.ID,
                RelationNo = order.OrderNumber,
                Site = order.Site,
                Channel = (int)Channel.CustomerService,
            };

            returnOrder.ProductList = productList;

            returnOrder.SendAddress = order.SendAddress;
            returnOrder.GetAddress = order.GetAddress;

            returnOrder.AmountList = new List<Order_AmountDC>();
            returnOrder.AmountList.Add(new Order_AmountDC()
            {
                Money = productList.Sum(p => p.Price),
                Type = 0,
                Content = "返洗订单抵扣",
            });

            var orderID = orderDAL.Order_Order_ADD(returnOrder);

            Order_Order_Finish(returnOrder.OrderNumber);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 更新客服备注
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iCSRemark"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_UPDATE_CSRemark(int iOrderID, string iCSRemark)
        {
            return new ReturnEntity<bool>(orderDAL.Order_Order_UPDATE_CSRemark(iOrderID, iCSRemark));
        }

        /// <summary>
        /// 合并订单
        /// </summary>
        /// <param name="iPrimaryOrderID"></param>
        /// <param name="iSlaveOrderID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_Merger(int iPrimaryOrderID, int iSlaveOrderID)
        {
            var rtn = orderDAL.Order_Order_Merger(iPrimaryOrderID, iSlaveOrderID);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 客服订单步骤变更
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Step_Change(int iOrderID, WashStepType iStep)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, false, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单ID错误");
            }

            if (iStep == WashStepType.Finish)
            {
                //订单完成
                Order_Step_AllFinish(order, DateTime.Now, false);

                //Order_Order_AllFinish(iOrderID, DateTime.Now, false);

                //更新全部完成时间
                //orderDAL.Order_Order_UPDATE_AllFinish(iOrderID, DateTime.Now);
            }
            else
            {
                orderDAL.Order_Step_ADD(iOrderID, iStep, null);
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 客服订单记录快递单号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_FinishExpress(int iOrderID, string iExpressNumber)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, false, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单ID错误");
            }

            //快递信息记录
            orderDAL.Order_Express_ADD(new Order_ExpressDC()
            {
                Code = iExpressNumber,
                Type = (int)ExpressType.Get,
                Content = "取件",
                OID = iOrderID,
            });

            Order_Step_SendFactory(order, iExpressNumber, false);

            //调用物流系统,更新物流状态
            Internal_ExpressOrderGetFinish(order.OrderNumber, iExpressNumber);

            //if (!string.IsNullOrWhiteSpace(iExpressNumber))
            //{
            //更新主单信息
            //orderDAL.Order_Order_UPDATE_ExpressNumber(iOrderID, iExpressNumber, ExpressType.Get);
            //}

            ////步骤添加
            //orderDAL.Order_Step_ADD(iOrderID, WashStepType.GetPackage, "取件中");

            ////推送消息
            //orderDAL.Order_Finish_Message(iOrderID);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 订单列表(后台)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iOrderType"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iTotalAmountMax"></param>
        /// <param name="iTotalAmountMin"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_Order_SELECT_List(
            string iOrderNumber,
            Guid? iUserID, string iMPNo, string iLoginName,
            OrderClass? OrderClass,
            OrderType? iOrderType, OrderStatus? iOrderStatus,
            int? iSite, int? iChannel,
            decimal? iTotalAmountMax, decimal? iTotalAmountMin,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize,
            string iConsignee = null,
            int iSortType = 0,
            DateTime? iExpStartDate = null, DateTime? iExpEndDate = null,
            int? iStep = null, int? iGetExpressType = null)
        {
            return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(
                orderDAL.Order_Order_SELECT_List(iOrderNumber, iUserID,
                iMPNo, iLoginName, OrderClass, iOrderType, iOrderStatus,
                iSite, iChannel, iTotalAmountMax, iTotalAmountMin,
                iStartDate, iEndDate, iPageIndex, iPageSize,
                iConsignee,
                iSortType,
                iExpStartDate, iExpEndDate, iStep, iGetExpressType));
        }

        /// <summary>
        /// 修改预期时间
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_ExpectTime_Change(int iOrderID, DateTime iExpectTime)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, false, false, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单不存在");
            }

            Internal_ExpressChangeExpectTime(order.OrderNumber, iExpectTime);

            return Order_UPDATE_ExpectTime(iOrderID, iExpectTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        private ReturnEntity<bool> Order_UPDATE_ExpectTime(int iOrderID, DateTime iExpectTime)
        {
            var rtn = orderDAL.Order_ExpectTime_Change(iOrderID, iExpectTime);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 客服一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <param name="iUserCouponID"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_CustomerService_Onekey_Create(Guid iUserID,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            DateTime? iExpectTime, string iUserRemark, int? iUserCouponID, int iMuser,
            string iCouponCode = null, int? iSendType = 1, string iInviteCode = null, int iUseMoney = 0)
        {
            var orderRtn = Order_Create_Onekey_0929(iUserID, 1, Channel.CustomerService,
                iGetAddress, iSendAddress, iExpectTime, iUserRemark, iUserCouponID,
                iCouponCode, iSendType, iInviteCode, iUseMoney);

            //if (orderRtn.Success && orderRtn.OBJ != null)
            //{
            //    Order_Order_Audit(orderRtn.OBJ.ID, true, iMuser);
            //}

            return orderRtn;
        }

        /// <summary>
        /// 工厂洗涤条码导出
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Product_WashStep_Code_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Product_WashStep_Code_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(rtn);
        }

        /// <summary>
        /// 订单步骤时间统计
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_StepTime_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Order_StepTime_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(rtn);
        }

        /// <summary>
        /// 订单出库查询
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_OutFactory_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Order_OutFactory_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(rtn);
        }

        /// <summary>
        /// 订单入库查询
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_InFactory_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Order_InFactory_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(rtn);
        }

        /// <summary>
        /// 修改物流类型
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_UPDATE_GetExpressType(int iOrderID, int iType)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, false, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单ID错误");
            }

            var rtn = orderDAL.Order_Order_UPDATE_GetExpressType(iOrderID, iType);

            if (rtn)
            {
                Internal_ExpressNodeChange(order.OrderNumber, iType);
            }

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 订单步骤时间统计
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_Alarm_Stat(
            bool iGetPackage, bool iWash, bool iSendPackage, bool iAll,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Order_Alarm_Stat(iGetPackage, iWash, iSendPackage, iAll,
              iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(rtn);
        }

        /// <summary>
        /// 订单步骤时间预警
        /// </summary>
        /// <param name="iGetPackage"></param>
        /// <param name="iWash"></param>
        /// <param name="iSendPackage"></param>
        /// <param name="iAll"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_Warning_Stat(
            bool iGetPackage, bool iWash, bool iSendPackage,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Order_Warning_Stat(iGetPackage, iWash, iSendPackage,
              iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(rtn);
        }

        /// <summary>
        /// 客服商务订单下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iProductName"></param>
        /// <param name="iPrice"></param>
        /// <param name="iCount"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_CustomerService_BusinessOrder_Create(Guid iUserID,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            string iProductName, decimal iPrice, int iCount,
            DateTime? iExpectTime, string iUserRemark, int iMuser)
        {
            #region 检查
            if (iGetAddress == null)
            {
                return new ReturnEntity<bool>(-1, "取货地址错误");
            }
            if (string.IsNullOrWhiteSpace(iGetAddress.Mpno))
            {
                return new ReturnEntity<bool>(-1, "取货手机错误");
            }
            if (iSendAddress == null)
            {
                return new ReturnEntity<bool>(-1, "送货地址错误");
            }
            if (string.IsNullOrWhiteSpace(iSendAddress.Mpno))
            {
                return new ReturnEntity<bool>(-1, "送货手机错误");
            }

            if (string.IsNullOrEmpty(iProductName))
            {
                return new ReturnEntity<bool>(-1, "产品错误");
            }
            if (iCount <= 0)
            {
                return new ReturnEntity<bool>(-1, "数量错误");
            }
            if (iPrice < 0)
            {
                return new ReturnEntity<bool>(-1, "金额错误");
            }
            #endregion

            var RealProductAmount = 0m;

            var order = new Order_OrderDC()
            {
                OrderClass = (int)OrderClass.Normal,
                OrderType = (int)OrderType.Normal,
                UserID = iUserID,
                OrderStatus = (int)OrderStatus.Create,
                Site = 0,
                Channel = (int)Channel.CustomerService,

                Title = "商务订单",
                TotalAmount = 0,
                PayAmount = 0,

                UserRemark = iUserRemark,
                ExpectTime = iExpectTime,
                Obj_Cuser = iMuser,
                Obj_Muser = iMuser,
            };

            order.ProductList = new List<Order_ProductDC>();

            for (int i = 0; i < iCount; i++)
            {
                var item = new Order_ProductDC();

                item.Name = iProductName;
                item.Type = 0;
                item.Price = iPrice;
                RealProductAmount += item.Price;

                item.Obj_Status = 1;
                item.Obj_Cuser = iMuser;
                item.Obj_Muser = iMuser;

                order.ProductList.Add(item);
            }

            //取送地址
            iGetAddress.Type = (int)ConsigneeAddressType.Get;
            order.GetAddress = iGetAddress;
            iSendAddress.Type = (int)ConsigneeAddressType.Send;
            order.SendAddress = iSendAddress;

            //计算金额
            order.TotalAmount = RealProductAmount;
            order.SourceAmount = RealProductAmount;

            var orderid = orderDAL.Order_Order_ADD(order);

            return new ReturnEntity<bool>(true);

        }

        /// <summary>
        /// 修改订单产品内容(客服权限)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_EditProduct(int iOrderID, IList<Order_ProductDC> iProductList, int iMuser)
        {
            if (iProductList == null)
            {
                return new ReturnEntity<bool>(-1, "添加产品对象为null");
            }
            //检查订单
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, true, false, false, false, true, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单ID错误");
            }
            if (order.OrderClass != (int)OrderClass.Normal && order.OrderClass != (int)OrderClass.OneKey)
            {
                return new ReturnEntity<bool>(-1, "订单类别错误");
            }
            if (order.OrderStatus != (int)OrderStatus.Finish)
            {
                return new ReturnEntity<bool>(-1, "订单未审核");
            }
            if (order.Step > (int)WashStepType.Delivery)
            {
                return new ReturnEntity<bool>(-1, "订单已洗涤完成");
            }
            //if (order.TotalAmount != 0 || order.PayAmount != 0)
            //{
            //    return new ReturnEntity<bool>(-1, "订单金额错误");
            //}
            //if (order.ProductList != null && order.ProductList.Count > 0)
            //{
            //    return new ReturnEntity<bool>(-1, "订单已添加产品");
            //}

            ////如果进入洗涤状态,切换为送洗中
            //if (order.Step == (int)WashStepType.Wash)
            //{
            //    orderDAL.Order_Step_ADD(iOrderID, WashStepType.SendFactory, "送洗中");
            //}

            decimal RealProductAmount = 0;

            //清除订单内产品
            orderDAL.Order_Product_DEL_OrderID(iOrderID);

            //清除快递费
            orderDAL.Order_Amount_DEL_OrderID(iOrderID);

            //一键下单
            if (order.OrderClass == (int)OrderClass.OneKey)
            {
                order.Title = "修改订单";
            }
            //普通订单
            else if (order.OrderClass == (int)OrderClass.Normal)
            {
                #region

                if (iProductList == null || iProductList.Count == 0)
                {
                    return new ReturnEntity<bool>(-1, "普通订单产品不能为空");
                }

                var checkProduct = Order_Create_Check_Product(order.ID, iProductList, true, out RealProductAmount, iMuser);
                if (!checkProduct.Success)
                {
                    return new ReturnEntity<bool>(checkProduct.Code, checkProduct.Message);
                }

                //添加新产品
                foreach (var item in iProductList)
                {
                    orderDAL.Order_Product_ADD(item);
                }

                if (iProductList.Count == 1)
                {
                    order.Title = iProductList.First().Name;
                }
                else
                {
                    order.Title = iProductList.First().Name + " 等(" + iProductList.Count + ")";
                }
                #endregion
            }

            //快递费
            #region 快递费

            var expressFee = 0m;

            if (RealProductAmount < 18 && iProductList.Count > 0)
            {
                expressFee = 18m - RealProductAmount;
            }
            else
            {
                expressFee = 0;
            }

            #endregion

            //清已付金额
            order.PayAmount = 0m;

            #region 已支付处理

            if (order.PaymentList != null && order.PaymentList.Count > 0)
            {
                foreach (var item in order.PaymentList)
                {
                    if (item.PayMoneyType == (int)PayMoneyType.Coupon)
                    {
                        #region 优惠券处理
                        var userCouponID = int.Parse(item.RelationID);

                        var couponFaceValue = 0m;
                        bool expressFeeFree = false;

                        var checkCoupon = Order_Create_Check_Coupon(order.UserID, userCouponID, false, iProductList, expressFee,
                            out couponFaceValue, out expressFeeFree);
                        if (checkCoupon.Success)
                        {
                            //已付金额
                            order.PayAmount += couponFaceValue;

                            if (expressFeeFree)
                            {
                                expressFee = 0;
                            }
                            orderDAL.Order_Pay_Coupon_EditProduct(order.ID, item.ID, couponFaceValue);
                        }
                        #endregion
                    }
                    else if (item.PayMoneyType == (int)PayMoneyType.Balance)
                    {
                        #region 余额支付处理
                        //退款
                        if (item.PayMoney > 0)
                        {
                            var amountbackRtn = orderDAL.User_Amount_Back(order.UserID, item.PayMoney, "订单重置", order.OrderNumber);
                            if (amountbackRtn != 1)
                            {
                                return new ReturnEntity<bool>(-1, "退还金额失败");
                            }
                            else
                            {
                                orderDAL.Order_Payment_DEL(item.ID);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        order.PayAmount += item.PayMoney;
                    }

                }
            }

            #endregion

            //快递费生成
            if (expressFee > 0)
            {
                orderDAL.Order_Amount_ADD(new Order_AmountDC()
                {
                    OID = order.ID,
                    Money = Math.Abs(expressFee),
                    Type = (int)AmountType.ExpressFee,
                    Content = "快递费",
                });
            }

            //计算金额
            order.TotalAmount = RealProductAmount + expressFee;

            order.SourceAmount = RealProductAmount + expressFee;

            if (order.PayType == (int)PayMoneyType.Balance)
            {
                var user = orderDAL.User_Info_SELECT_Entity(order.UserID);
                if (user != null && user.Money > 0)
                {
                    var payMoney = order.TotalAmount - order.PayAmount;
                    if (payMoney > 0)
                    {
                        if (user.Money < (order.TotalAmount - order.PayAmount))
                        {
                            payMoney = user.Money;
                        }

                        #region 余额支付
                        var useRtn = orderDAL.User_Amount_Use(order.UserID, payMoney, "支付订单:" + order.OrderNumber, order.OrderNumber);
                        if (useRtn != 1)
                        {
                            switch (useRtn)
                            {
                                case -1:
                                    Log_Fatal("一键下单用户使用余额失败:用户不存在 " + order.OrderNumber);
                                    break;
                                //return new ReturnEntity<bool>(-4, "用户不存在");
                                case -2:
                                    Log_Fatal("一键下单用户使用余额失败:余额不足 " + order.OrderNumber);
                                    break;
                                //return new ReturnEntity<bool>(-4, "余额不足");
                                default:
                                    Log_Fatal("一键下单用户使用余额失败:用户状态错误 " + order.OrderNumber);
                                    break;
                                //return new ReturnEntity<bool>(-4, "用户状态错误");
                            }
                        }
                        else
                        {
                            //添加支付信息
                            orderDAL.Order_Payment_ADD(new Order_PaymentDC()
                            {
                                OID = order.ID,
                                PayMoney = payMoney,
                                PayMoneyType = (int)PayMoneyType.Balance,
                                PayChannel = (int)PayMoneyChannel.Factory,
                                PayDate = DateTime.Now,
                                RelationID = iMuser.ToString(),
                            });

                            order.PayAmount += payMoney;
                        }
                        #endregion
                    }
                }
            }

            //更新金额
            if (!orderDAL.Order_Order_UPDATE_Amount_EditProduct(order.ID, order.Title, order.TotalAmount, order.SourceAmount, order.PayAmount))
            {
                return new ReturnEntity<bool>(-1, "订单金额更新失败:" + order.OrderNumber);
            }

            //更新物流收费金额
            Internal_ExpressOrderChargeFee(order.OrderNumber
                , order.TotalAmount - order.PayAmount
                , string.Join(" ", iProductList.Select(p => p.Name).ToArray())
                , iProductList.Count);

            //更新物流库存
            var storageReNewRtn = Internal_Storage_OrderReNew(order.OrderNumber, iMuser);
            if (!storageReNewRtn.Success)
            {
                return new ReturnEntity<bool>(storageReNewRtn.Code, storageReNewRtn.Message);
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 修改产品是否可洗涤(客服权限)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductID"></param>
        /// <param name="iWashStatus">1:可以洗,2:无法洗</param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_WashStatus(int iOrderID, int iProductID, int iWashStatus, int iMuser)
        {
            //检查订单
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, true, true, false, false, true, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单ID错误");
            }
            if (order.OrderClass != (int)OrderClass.Normal && order.OrderClass != (int)OrderClass.OneKey)
            {
                return new ReturnEntity<bool>(-1, "订单类别错误");
            }
            if (order.OrderStatus != (int)OrderStatus.Finish)
            {
                return new ReturnEntity<bool>(-1, "订单未审核");
            }
            if (order.Step > (int)WashStepType.Finish)
            {
                return new ReturnEntity<bool>(-1, "订单已送件完成");
            }

            var orderProduct = order.ProductList.FirstOrDefault(p => p.ID == iProductID);
            if (orderProduct == null)
            {
                return new ReturnEntity<bool>(-1, "订单产品不存在");
            }

            //已经是该状态,无需处理
            if (orderProduct.FactoryWash == iWashStatus)
            {
                return new ReturnEntity<bool>(true);
            }

            //改为不能洗
            if (iWashStatus == 2)
            {
                //产品金额大于0
                if (orderProduct.Price > 0)
                {
                    orderDAL.Order_Amount_ADD(new Order_AmountDC()
                    {
                        OID = order.ID,
                        Money = Math.Abs(orderProduct.Price),
                        Type = (int)AmountType.System,
                        Content = "无法洗涤退款",
                        RelationID = iProductID.ToString(),
                    });
                }

                order.TotalAmount -= orderProduct.Price;
                if (order.TotalAmount <= 0)
                {
                    order.TotalAmount = 0;
                }
                order.SourceAmount -= orderProduct.Price;
                if (order.SourceAmount <= 0)
                {
                    order.SourceAmount = 0;
                }

                //修改订单产品洗涤状态
                orderDAL.Order_Product_UPDATE_FactoryWash(iProductID, iWashStatus);

                //更新金额
                if (!orderDAL.Order_Order_UPDATE_Amount_EditProduct(order.ID, order.Title, order.TotalAmount, order.SourceAmount, order.PayAmount))
                {
                    return new ReturnEntity<bool>(-1, "订单金额更新失败:" + order.OrderNumber);
                }

                //更新物流收费金额
                Internal_ExpressOrderChargeFee(order.OrderNumber
                    , order.TotalAmount - order.PayAmount
                    , string.Join(" ", order.ProductList.Select(p => p.Name).ToArray())
                    , order.ProductList.Count);

            }
            //改回可以洗
            else if (iWashStatus == 1)
            {
                var orderAmount = order.AmountList.FirstOrDefault(p => p.Type == (int)AmountType.System && p.RelationID == iProductID.ToString());
                if (orderAmount != null)
                {
                    //删除原有减免金额
                    orderDAL.Order_Amount_DEL(orderAmount.ID);
                }

                order.TotalAmount += orderProduct.Price;

                order.SourceAmount += orderProduct.Price;

                //修改订单产品洗涤状态
                orderDAL.Order_Product_UPDATE_FactoryWash(iProductID, iWashStatus);

                //更新金额
                if (!orderDAL.Order_Order_UPDATE_Amount_EditProduct(order.ID, order.Title, order.TotalAmount, order.SourceAmount, order.PayAmount))
                {
                    return new ReturnEntity<bool>(-1, "订单金额更新失败:" + order.OrderNumber);
                }

                //更新物流收费金额
                Internal_ExpressOrderChargeFee(order.OrderNumber
                    , order.TotalAmount - order.PayAmount
                    , string.Join(" ", order.ProductList.Select(p => p.Name).ToArray())
                    , order.ProductList.Count);
            }
            else
            {
                return new ReturnEntity<bool>(-1, "无法处理的状态:" + iWashStatus);
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 订单导出
        /// </summary>
        /// <param name="iKeyword"></param>
        /// <param name="iCreateStartDate"></param>
        /// <param name="iCreateEndDate"></param>
        /// <param name="iFinishStartDate"></param>
        /// <param name="iFinishEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<IList<string[]>> Order_Order_Export(
            string iKeyword,
            DateTime? iCreateStartDate, DateTime? iCreateEndDate,
            DateTime? iFinishStartDate, DateTime? iFinishEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Order_Export(
                        iKeyword,
                        iCreateStartDate, iCreateEndDate,
                        iFinishStartDate, iFinishEndDate,
                        iPageIndex, iPageSize);

            return new ReturnEntity<IList<string[]>>(rtn);
        }
    }
}
