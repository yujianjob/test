using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.ClientProxy;
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
        //private LazyAtHome.Library.Partners.SFExpress.Business.OrderService SFOrderService = new Library.Partners.SFExpress.Business.OrderService();
        //private LazyAtHome.Library.Partners.QFExpress.Business.OrderService QFOrderService = new Library.Partners.QFExpress.Business.OrderService();

        /// <summary>
        /// 完成物流推送
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_FinishExpress(int iOrderID)
        {
            //快递信息记录
            orderDAL.Order_Express_ADD(new Order_ExpressDC()
            {
                Code = string.Empty,
                Type = (int)ExpressType.Get,
                Content = "取件",
                OID = iOrderID,
            });

            //更新主单信息
            orderDAL.Order_Order_FinishExpress_Get(iOrderID);

            ////步骤添加
            //orderDAL.Order_Step_ADD(iOrderID, WashStepType.GetPackage, "取件中");

            ////推送消息
            //orderDAL.Order_Finish_Message(iOrderID);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 顺丰客户下单
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <param name="j_Company"></param>
        /// <param name="j_Contact"></param>
        /// <param name="j_Tel"></param>
        /// <param name="j_Address"></param>
        /// <param name="j_City"></param>
        /// <param name="j_Province"></param>
        /// <param name="d_Company"></param>
        /// <param name="d_Contact"></param>
        /// <param name="d_Tel"></param>
        /// <param name="d_Address"></param>
        /// <param name="d_City"></param>
        /// <param name="d_Province"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<SF_OrderInfoDC> SF_CreateReverseOrder(string iOrderNumber,
            Order_ConsigneeAddressDC iGetAddress, DateTime? iExpectTime)
        {
            throw new Exception("已取消");
            //try
            //{
            //    var rtn = SFOrderService.CreateReverseOrder(iOrderNumber,
            //               iGetAddress.Consignee,
            //               iGetAddress.Consignee,
            //               iGetAddress.Mpno,
            //               iGetAddress.Address,
            //               iGetAddress.CityName,
            //               iGetAddress.ProvinceName,

            //               Config.FactoryAddress.Company,
            //               Config.FactoryAddress.Contact,
            //               Config.FactoryAddress.Phone,
            //               Config.FactoryAddress.Address,
            //               Config.FactoryAddress.City,
            //               Config.FactoryAddress.Province,
            //               iExpectTime);
            //    if (rtn.Success)
            //    {
            //        if (rtn.OBJ != null)
            //        {
            //            return new ReturnEntity<SF_OrderInfoDC>(new SF_OrderInfoDC()
            //            {
            //                //destcode = rtn.OBJ.destcode,
            //                mailno = rtn.OBJ.mailno,
            //                orderid = rtn.OBJ.orderid,
            //                //origincode = rtn.OBJ.origincode,
            //                filter_result = rtn.OBJ.filter_result,
            //                remark = rtn.OBJ.remark,
            //            });
            //        }
            //        else
            //        {
            //            return new ReturnEntity<SF_OrderInfoDC>(-1, "顺丰返回对象空");
            //        }
            //    }
            //    else
            //    {
            //        return new ReturnEntity<SF_OrderInfoDC>(rtn.Code, rtn.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log_Fatal("SF_CreateReverseOrder:" + iOrderNumber + " " + ex.Message);
            //    return new ReturnEntity<SF_OrderInfoDC>(-999, ex.Message);
            //}
        }

        /// <summary>
        /// 订单取消
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<SF_OrderInfoDC> SF_CancelOrder(string iOrderid)
        {

            throw new Exception("已取消");

            //try
            //{
            //    var rtn = SFOrderService.CancelOrder(iOrderid);
            //    if (rtn.Success)
            //    {
            //        if (rtn.OBJ != null)
            //        {
            //            return new ReturnEntity<SF_OrderInfoDC>(new SF_OrderInfoDC()
            //            {
            //                res_status = rtn.OBJ.res_status,
            //                mailno = rtn.OBJ.mailno,
            //                orderid = rtn.OBJ.orderid,
            //            });
            //        }
            //        else
            //        {
            //            return new ReturnEntity<SF_OrderInfoDC>(-1, "顺丰返回对象空");
            //        }
            //    }
            //    else
            //    {
            //        return new ReturnEntity<SF_OrderInfoDC>(rtn.Code, rtn.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log_Fatal("SF_CancelOrder:" + iOrderid + " " + ex.Message);
            //    return new ReturnEntity<SF_OrderInfoDC>(-999, ex.Message);
            //}
        }

        /// <summary>
        /// 订单结果查询
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<SF_OrderInfoDC> SF_OrderSearch(string iOrderid)
        {
            throw new Exception("已取消");
            //try
            //{
            //    var rtn = SFOrderService.OrderSearch(iOrderid);
            //    if (rtn.Success)
            //    {
            //        if (rtn.OBJ != null)
            //        {
            //            return new ReturnEntity<SF_OrderInfoDC>(new SF_OrderInfoDC()
            //            {
            //                destcode = rtn.OBJ.destcode,
            //                mailno = rtn.OBJ.mailno,
            //                orderid = rtn.OBJ.orderid,
            //                origincode = rtn.OBJ.origincode,
            //                filter_result = rtn.OBJ.filter_result,
            //                remark = rtn.OBJ.remark,
            //            });
            //        }
            //        else
            //        {
            //            return new ReturnEntity<SF_OrderInfoDC>(-1, "顺丰返回对象空");
            //        }
            //    }
            //    else
            //    {
            //        return new ReturnEntity<SF_OrderInfoDC>(rtn.Code, rtn.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log_Fatal("SF_OrderSearch:" + iOrderid + " " + ex.Message);
            //    return new ReturnEntity<SF_OrderInfoDC>(-999, ex.Message);
            //}
        }

        /// <summary>
        /// 顺丰物流推送
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iDatetime"></param>
        /// <param name="iOpCode"></param>
        /// <param name="iRemark"></param>
        public ReturnEntity<bool> SF_Express_RoutePush(IList<RoutePushDataDC> iRouteList)
        {
            throw new Exception("已取消");
            //if (iRouteList == null || iRouteList.Count == 0)
            //{
            //    return new ReturnEntity<bool>(true);
            //}

            //foreach (var item in iRouteList)
            //{
            //    if (item == null) continue;

            //    if (item.OrderNumber.Length > 15)
            //    {
            //        item.OrderNumber = item.OrderNumber.Substring(0, 15);
            //    }

            //    var order = orderDAL.Order_Order_SELECT_Entity(item.OrderNumber, false, false, false, false, false, false);
            //    if (order == null)
            //    {
            //        Log_Fatal("订单[" + item.OrderNumber + "]顺丰回调失败,找不到该订单");
            //        continue;
            //    }

            //    //收件
            //    if (item.ExpressNumber == order.GetExpressNumber)
            //    {
            //        orderDAL.Order_Express_ADD(new Order_ExpressDC()
            //        {
            //            OID = order.ID,
            //            Code = item.ExpressNumber,
            //            Type = (int)ExpressType.Get,
            //            Content = item.AcceptAddress + "(" + item.OpCode + ")",
            //            Obj_Remark = item.Remark,
            //            AcceptTime = item.AcceptTime,
            //        });

            //        //步骤在取件中
            //        if (order.Step == (int)WashStepType.GetPackage)
            //        {
            //            //步骤添加
            //            orderDAL.Order_Step_ADD(order.ID, WashStepType.SendFactory, "送洗中");
            //        }
            //        //派件成功
            //        else if (item.OpCode == "80")
            //        {
            //            if (order.Step != (int)WashStepType.Wash)
            //            {
            //                orderDAL.Order_Step_ADD(order.ID, WashStepType.Wash, "洗涤中");
            //            }
            //        }
            //    }
            //    //送件
            //    else if (item.ExpressNumber == order.SendExpressNumber)
            //    {
            //        orderDAL.Order_Express_ADD(new Order_ExpressDC()
            //        {
            //            OID = order.ID,
            //            Code = item.ExpressNumber,
            //            Type = (int)ExpressType.Send,
            //            Content = item.AcceptAddress + "(" + item.OpCode + ")",
            //            Obj_Remark = item.Remark,
            //            AcceptTime = item.AcceptTime
            //        });

            //        //步骤在取件中
            //        if (order.Step == (int)WashStepType.Delivery)
            //        {
            //            //步骤添加
            //            orderDAL.Order_Step_ADD(order.ID, WashStepType.SendPackage, "送件中");

            //            orderDAL.Order_OutFactory_Message(order.ID);
            //        }
            //        //派件成功
            //        else if (item.OpCode == "80")
            //        {
            //            if (order.Step != (int)WashStepType.Finish)
            //            {
            //                orderDAL.Order_Step_ADD(order.ID, WashStepType.Finish, "完成");
            //                //更新全部完成时间
            //                orderDAL.Order_Order_UPDATE_AllFinish(order.ID, item.AcceptTime);

            //                orderDAL.Order_AllFinish_Message(order.ID);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Log_Fatal("订单[" + item.OrderNumber + "]顺丰回调失败,无法匹配到快递单号");
            //        continue;
            //    }
            //}
            //return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 生成快递单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <param name="iChargeFee"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_CreateExpressOrder(string iOrderNumber,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            DateTime iExpectTime, string iPackageInfo, int iPackageCount, decimal iChargeFee,
            string iInviteCode)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(

                   c => c.Proxy.Exp_Order_Create(
                        iOrderNumber,
                        iGetAddress.DistrictID, iGetAddress.Address, iGetAddress.Consignee, iGetAddress.Mpno, iExpectTime,
                        iSendAddress.DistrictID, iSendAddress.Address, iSendAddress.Consignee, iSendAddress.Mpno, iExpectTime,
                        iPackageInfo, iPackageCount, iChargeFee, null, iInviteCode)
                         );
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_CreateExpressOrder:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }


        /// <summary>
        /// 更新物流收费金额
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressOrderChargeFee(string iOrderNumber, decimal iChargeFee)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(

                   c => c.Proxy.Exp_Order_UPDATE_Send_ChargeFee(iOrderNumber, iChargeFee));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressOrderChargeFee:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 更新物流收费金额
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressOrderChargeFee(string iOrderNumber, decimal iChargeFee, string iPackageInfo, int iPackageCount)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(

                   c => c.Proxy.Exp_Order_UPDATE_Send_ChargeFee(iOrderNumber, iChargeFee, iPackageInfo, iPackageCount));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressOrderChargeFee:" + iOrderNumber + " " + iChargeFee + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressOrderGetFinish(string iOrderNumber, string iExpressNumber)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(

                   c => c.Proxy.Exp_Order_UPDATE_Get_Finish(iOrderNumber, iExpressNumber));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressOrderGetFinish:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 内部物流推送
        /// </summary>
        /// <param name="iRouteList"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressRoutePush(IList<RoutePushDataDC> iRouteList)
        {
            if (iRouteList == null || iRouteList.Count == 0)
            {
                return new ReturnEntity<bool>(true);
            }

            foreach (var item in iRouteList)
            {
                if (item == null) continue;

                //if (item.OrderNumber.Length > 15)
                //{
                //    item.OrderNumber = item.OrderNumber.Substring(0, 15);
                //}

                var order = orderDAL.Order_Order_SELECT_Entity(item.OrderNumber, false, false, false, false, false, false);
                if (order == null)
                {
                    Log_Fatal("订单[" + item.OrderNumber + "]内部物流回调失败,找不到该订单");
                    continue;
                }
                //物流单号生成 更新到数据库
                if (item.OpCode == "21")
                {
                    Order_Step_SendFactory(order, item.ExpressNumber, true);
                }
                //修改取件时间
                else if (item.OpCode == "1000")
                {
                    orderDAL.Order_Express_ADD(new Order_ExpressDC()
                    {
                        OID = order.ID,
                        Code = item.ExpressNumber,
                        Type = (int)ExpressType.Send,
                        Content = item.RouteInfo + "(" + item.OpCode + ")",
                        Obj_Remark = item.Remark,
                        AcceptTime = item.AcceptTime
                    });
                    if (!string.IsNullOrWhiteSpace(item.Remark))
                    {
                        Order_UPDATE_ExpectTime(order.ID, DateTime.ParseExact(item.Remark, "yyyy-MM-dd HH:mm:ss", null));
                    }
                }
                //收件
                else if (item.ExpressNumber == order.GetExpressNumber)
                {
                    orderDAL.Order_Express_ADD(new Order_ExpressDC()
                    {
                        OID = order.ID,
                        Code = item.ExpressNumber,
                        Type = (int)ExpressType.Get,
                        Content = item.RouteInfo + "(" + item.OpCode + ")",
                        Obj_Remark = item.Remark,
                        AcceptTime = item.AcceptTime,
                    });

                    //步骤在取件中
                    if (order.Step == (int)WashStepType.GetPackage)
                    {
                        Order_Step_SendFactory(order, null, false);
                        //步骤添加
                        //orderDAL.Order_Step_ADD(order.ID, WashStepType.SendFactory, "送洗中");
                    }
                }
                //送件
                else if (item.ExpressNumber == order.SendExpressNumber)
                {
                    orderDAL.Order_Express_ADD(new Order_ExpressDC()
                    {
                        OID = order.ID,
                        Code = item.ExpressNumber,
                        Type = (int)ExpressType.Send,
                        Content = item.RouteInfo + "(" + item.OpCode + ")",
                        Obj_Remark = item.Remark,
                        AcceptTime = item.AcceptTime
                    });

                    //派件成功
                    if (item.OpCode == "80")
                    {
                        if (order.Step != (int)WashStepType.Finish)
                        {
                            //订单完成
                            Order_Step_AllFinish(order, item.AcceptTime, true);

                            //Order_Order_AllFinish(order.ID, item.AcceptTime);

                            ////更新订单状态
                            //orderDAL.Order_Step_ADD(order.ID, WashStepType.Finish, "完成");
                            ////更新全部完成时间
                            //orderDAL.Order_Order_UPDATE_AllFinish(order.ID, item.AcceptTime);
                            ////发送完成短信
                            //orderDAL.Order_AllFinish_Message(order.ID);
                        }
                    }
                    ////步骤在取件中
                    else if (order.Step == (int)WashStepType.Delivery)
                    {
                        //出库中
                        Order_Step_SendPackage(order);

                        //步骤添加
                        //orderDAL.Order_Step_ADD(order.ID, WashStepType.SendPackage, "送件中");

                        //orderDAL.Order_OutFactory_Message(order.ID);
                    }
                }
                else
                {
                    Log_Fatal("订单[" + item.OrderNumber + "]内部物流回调失败,无法匹配到快递单号");
                    continue;
                }
            }
            return new ReturnEntity<bool>(true);
        }

        ///// <summary>
        ///// 订单完全完成
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iAcceptTime"></param>
        //private void Order_Order_AllFinish(int iOrderID, DateTime iAcceptTime, bool iNoMessage = true)
        //{
        //    //更新订单状态
        //    orderDAL.Order_Step_ADD(iOrderID, WashStepType.Finish, "完成");
        //    //更新全部完成时间
        //    orderDAL.Order_Order_UPDATE_AllFinish(iOrderID, iAcceptTime);

        //    if (iNoMessage)
        //    {
        //        //发送完成短信
        //        orderDAL.Order_AllFinish_Message(iOrderID);
        //    }
        //}


        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressChangeExpectTime(string iOrderNumber, DateTime iExpectTime)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(

                   c => c.Proxy.Exp_Order_Get_ExpectTime_Change(iOrderNumber, iExpectTime));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressChangeExpectTime:" + iOrderNumber + " " + iExpectTime + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 取消物流单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressCancel(string iOrderNumber)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(

                   c => c.Proxy.Exp_Order_Cancel(iOrderNumber));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressCancel:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 修改物流站点
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressNodeChange(string iOrderNumber, int iType)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(

                   c => c.Proxy.Exp_Order_NodeChange(iOrderNumber, iType));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressNodeChange:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 地址更新
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iTransportType"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iContacts"></param>
        /// <param name="iMpno"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressAddressChange(string iOrderNumber, int iTransportType,
             int iDistrictID, string iAddress, string iContacts, string iMpno)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(
                   c => c.Proxy.Exp_Order_UPDATE_Address(iOrderNumber, iTransportType, iDistrictID, iAddress, iContacts, iMpno));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressAddressChange:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }

        }

        /// <summary>
        /// 工厂拆包分拣入库
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_Storage_BreakPackage(string iOrderNumber, IList<Order_ProductDC> iProductList, int iMuser)
        {
            try
            {
                var storageItemList = new List<LazyAtHome.WCF.Express.Contract.DataContract.Exp_StorageItemDC>();

                foreach (var product in iProductList)
                {
                    storageItemList.Add(new Express.Contract.DataContract.Exp_StorageItemDC()
                    {
                        Number = iOrderNumber,
                        OtherNumber = product.Code,
                        ItemName = product.Name,
                        ItemDetail = product.Attribute,
                    });
                }

                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(
                   c => c.Proxy.Exp_StorageItem_BreakPackage(iOrderNumber, storageItemList, iMuser));
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressAddressChange:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 工厂出库
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iProductCode"></param>
        /// <param name="iOutType"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_Storage_FactoryPreOut(string iOrderNumber, string iProductCode, int iOutType, int iMuser)
        {
            try
            {
                if (iOutType != 1 && iOutType != 2)
                {
                    iOutType = 1;
                }

                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(
                   c => c.Proxy.Exp_StorageItem_FactoryPreOut(iOrderNumber, iProductCode, iOutType, iMuser));
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_StorageItem_FactoryPreOut:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 订单刷新(修改产品内容)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_Storage_OrderReNew(string iOrderNumber, int iMuser)
        {
            try
            {
                return WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(
                   c => c.Proxy.Exp_Storage_OrderReNew(iOrderNumber, iMuser));
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_StorageItem_FactoryPreOut:" + iOrderNumber + " " + ex.Message);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 检查地址
        /// </summary>
        /// <param name="iAddress"></param>
        /// <returns></returns>
        public bool Internal_CheckAddress(string iAddress)
        {
            try
            {
                var rtn = WCFInvokeHelper<ApiExpressClient>.Invoke<ReturnEntity<bool>>(
                   c => c.Proxy.CheckNode_Address(iAddress));
                if (rtn != null && rtn.OBJ == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_CheckAddress:" + iAddress + " " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 快递当面下单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Onekey_Create_Express(string iOrderNumber, string iInviteCode)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderNumber, false, false, true, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单不存在");
            }
            else
            {
                var createRtn = Order_Create_Onekey_0929(order.UserID, 0, Channel.Express,
                      new Order_ConsigneeAddressDC()
                      {
                          Address = order.GetAddress.Address,
                          Consignee = order.GetAddress.Consignee,
                          DistrictID = order.GetAddress.DistrictID,
                          Mpno = order.GetAddress.Mpno,
                      },
                      new Order_ConsigneeAddressDC()
                      {
                          Address = order.GetAddress.Address,
                          Consignee = order.GetAddress.Consignee,
                          DistrictID = order.GetAddress.DistrictID,
                          Mpno = order.GetAddress.Mpno,
                      },
                      null, null, null, null, null, iInviteCode, 0);

                if (createRtn.Success && createRtn.OBJ != null)
                {
                    //Order_Order_Audit(createRtn.OBJ.ID, true, -1);
                    return new ReturnEntity<bool>(true);
                }
                else
                {
                    return new ReturnEntity<bool>(createRtn.Code, createRtn.Message);
                }

                //if (createRtn.Success && createRtn.OBJ != null)
                //{
                //    Order_Order_Audit(createRtn.OBJ.ID, true, -1);
                //    return new ReturnEntity<bool>(true);
                //}
                //else
                //{
                //    return new ReturnEntity<bool>(createRtn.Code, createRtn.Message);
                //}
            }
        }
    }
}
