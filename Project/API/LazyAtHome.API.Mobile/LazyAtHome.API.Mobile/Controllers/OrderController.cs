using LazyAtHome.API.Mobile.App_Code.Proxy;
using LazyAtHome.API.Mobile.Models.JsonResultModels;
using LazyAtHome.API.Mobile.Models.LocalModels;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.API.Mobile.Controllers
{
    public partial class HomeController
    {
        /// <summary>
        /// 4.6	获取用户订单
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult OrderList(string cid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            try
            {
                var orderRtn = OrderProxy.app_Order_Order_SELECT_List(userid, null, 1, 5);
                if (orderRtn == null || orderRtn.OBJ == null || orderRtn.OBJ.ReturnList == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取订单失败"));
                }
                var rtn = new OrderListResult()
                {
                    orderlist = new List<Models.LocalModels.OrderModel>(),
                };
                foreach (var orderItem in orderRtn.OBJ.ReturnList)
                {
                    OrderModel order = new OrderModel()
                    {
                        orderid = orderItem.ID,
                        orderno = orderItem.OrderNumber,
                        amount = Convert.ToInt32(orderItem.TotalAmount * 100),
                        payamount = Convert.ToInt32((orderItem.TotalAmount - orderItem.PayAmount) * 100),
                        ptime = orderItem.CompleteTime != null ? orderItem.CompleteTime.Value.ToString("yyyy.MM.dd") : orderItem.Obj_Cdate.Value.ToString("yyyy.MM.dd"),
                    };

                    var expTime = orderItem.CompleteTime != null ? orderItem.CompleteTime.Value : orderItem.Obj_Cdate.Value;
                    expTime = expTime.AddMinutes(60 - expTime.Minute).AddHours(1);
                    if (orderItem.ExpectTime.HasValue)
                    {
                        expTime = orderItem.ExpectTime.Value;
                    }

                    order.SetExpressModel(expTime);

                    #region 洗涤状态
                    //1.	未支付
                    //2.	待取件
                    //3.	送洗中
                    //4.	洗涤中
                    //5.	送还中
                    //6.	完成
                    if (orderItem.OrderStatus == 0)
                    {
                        //审核中
                        order.status = 2;
                    }
                    else if (orderItem.OrderStatus == 1)
                    {
                        //未付款
                        order.status = 1;
                    }
                    else if (orderItem.OrderStatus == 2)
                    {
                        if (orderItem.Step == (int)WashStepType.CreateOrder || orderItem.Step == (int)WashStepType.GetPackage)
                        {
                            //待取件
                            order.status = 2;
                        }
                        else if (orderItem.Step == (int)WashStepType.SendFactory)
                        {
                            //送往工厂
                            order.status = 3;
                        }
                        else if (orderItem.Step == (int)WashStepType.Wash)
                        {
                            //洗涤中
                            order.status = 4;
                        }
                        else if (orderItem.Step == (int)WashStepType.Finish)
                        {
                            //完成
                            order.status = 6;
                        }
                        else
                        {
                            //送还中
                            order.status = 5;
                        }
                    }
                    else
                    {
                        order.status = 6;
                    }
                    #endregion

                    if (orderItem.OrderStatus == 2)
                    {
                        order.washstatus = 2;
                    }
                    else
                    {
                        order.washstatus = 0;
                    }
                    if (orderItem.OrderStatus == 1 && (orderItem.TotalAmount - orderItem.PayAmount) > 0)
                    {
                        order.paybtn = 1;
                        order.payresult = LazyAtHome.API.Mobile.App_Code.PublicFun.CreateAlipayPayParam(
                            orderItem.OrderNumber, orderItem.Title, orderItem.Title, orderItem.TotalAmount - orderItem.PayAmount
                        );
                    }
                    else
                    {
                        order.paybtn = 0;
                    }
                    if (orderItem.OrderStatus == 0 || orderItem.OrderStatus == 1)
                    {
                        order.cancelbtn = 1;
                    }
                    else
                    {
                        order.cancelbtn = 0;
                    }

                    #region 产品
                    order.productlist = new List<WashProductModel>();
                    if (orderItem.ProductList != null)
                    {
                        IDictionary<int, WashProductModel> di_orderProduct = new Dictionary<int, WashProductModel>();
                        foreach (var orderProductItem in orderItem.ProductList)
                        {
                            if (di_orderProduct.ContainsKey(orderProductItem.ProductID))
                            {
                                di_orderProduct[orderProductItem.ProductID].count++;
                            }
                            else
                            {
                                di_orderProduct.Add(orderProductItem.ProductID, new WashProductModel()
                                {
                                    id = orderProductItem.ProductID,
                                    name = orderProductItem.Name,
                                    price = Convert.ToInt32(orderProductItem.Price * 100),
                                    count = 1,
                                });
                            }
                        }
                        order.productlist = di_orderProduct.Values.ToList();
                    }
                    #endregion

                    #region 金额
                    order.amountlist = new List<OrderAmountModel>();
                    if (orderItem.AmountList != null)
                    {
                        foreach (var orderAmountItem in orderItem.AmountList)
                        {
                            order.amountlist.Add(new OrderAmountModel()
                            {
                                id = orderAmountItem.ID,
                                name = orderAmountItem.Content,
                                money = Convert.ToInt32(orderAmountItem.Money * 100),
                            });
                        }
                    }
                    if (orderItem.PaymentList != null)
                    {
                        foreach (var orderPaymentItem in orderItem.PaymentList)
                        {
                            var tmp = new OrderAmountModel()
                            {
                                id = orderPaymentItem.ID,
                                money = Convert.ToInt32(-orderPaymentItem.PayMoney * 100),
                            };
                            switch (orderPaymentItem.PayMoneyType)
                            {
                                case 1:
                                    tmp.name = "余额抵扣";
                                    break;
                                case 2:
                                    tmp.name = "懒人卡抵扣";
                                    break;
                                case 3:
                                    tmp.name = "支付宝支付";
                                    break;
                                case 9:
                                    tmp.name = "优惠券抵扣";
                                    break;
                                default:
                                    continue;
                            }
                            order.amountlist.Add(tmp);
                        }
                        if (orderItem.OrderStatus == 1 && (orderItem.TotalAmount - orderItem.PayAmount) > 0)
                        {
                            //未支付完成
                            order.amountlist.Add(new OrderAmountModel()
                            {
                                id = 999,
                                name = "应付金额",
                                money = Convert.ToInt32((orderItem.TotalAmount - orderItem.PayAmount) * 100),
                            });
                        }
                    }

                    #endregion

                    rtn.orderlist.Add(order);
                }
                return MyJson(rtn);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.7	订单支付
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="orderno"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult OrderPay(string cid, string orderno, int type)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            var orderRtn = OrderProxy.Order_Order_SELECT_Entity(orderno, true, false, false, false, false, false);
            if (orderRtn == null)
            {
                return MyJson(BaseResult.EmptyResult);
            }
            if (orderRtn.OBJ == null)
            {
                return MyJson(BaseResult.BadResult(-1, "订单不存在"));
            }

            var order = orderRtn.OBJ;
            if (order.OrderStatus != 1)
            {
                return MyJson(BaseResult.BadResult(-1, "订单状态错误"));
            }
            if (order.TotalAmount - order.PayAmount <= 0)
            {
                return MyJson(BaseResult.BadResult(-1, "订单无需支付"));
            }

            OrderPayResult rtn = new OrderPayResult();

            rtn.payresult = LazyAtHome.API.Mobile.App_Code.PublicFun.CreateAlipayPayParam(
                order.OrderNumber, order.Title, order.Title, order.TotalAmount - order.PayAmount
                );

            return MyJson(rtn);
        }


        /// <summary>
        /// 4.8	获取单笔订单详情
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public JsonResult OrderGet(string orderno)
        {
            if (string.IsNullOrWhiteSpace(orderno))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var orderRtn = OrderProxy.Order_Order_SELECT_Entity(orderno, true, true, false, false, false, false);
                if (orderRtn == null || orderRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取订单失败"));
                }
                var rtn = new OrderGetResult();
                var orderItem = orderRtn.OBJ;

                OrderModel order = new OrderModel()
                {
                    orderid = orderItem.ID,
                    orderno = orderItem.OrderNumber,
                    amount = Convert.ToInt32(orderItem.TotalAmount * 100),
                    payamount = Convert.ToInt32((orderItem.TotalAmount - orderItem.PayAmount) * 100),
                    ptime = orderItem.CompleteTime != null ? orderItem.CompleteTime.Value.ToString("yyyy.MM.dd") : orderItem.Obj_Cdate.Value.ToString("yyyy.MM.dd"),
                };

                //order.getexp = new OrderExpressModel()
                //{
                //    sdate = "星期三 7月25日",
                //    stime = "13:00",
                //    status = "取件",
                //    pic = string.Empty,
                //};
                //order.sendexp = new OrderExpressModel()
                //{
                //    sdate = "星期四 7月26日",
                //    stime = "--:-",
                //    status = "送件",
                //    pic = string.Empty,
                //};
                #region 洗涤状态
                //1.	未支付
                //2.	待取件
                //3.	送洗中
                //4.	洗涤中
                //5.	送还中
                //6.	完成
                if (orderItem.OrderStatus == 0)
                {
                    //审核中
                    order.status = 2;
                }
                else if (orderItem.OrderStatus == 1)
                {
                    //未付款
                    order.status = 1;
                }
                else if (orderItem.OrderStatus == 2)
                {
                    if (orderItem.Step == (int)WashStepType.SendFactory)
                    {
                        //送往工厂
                        order.status = 3;
                    }
                    else if (orderItem.Step == (int)WashStepType.Wash)
                    {
                        //洗涤中
                        order.status = 4;
                    }
                    else if (orderItem.Step == (int)WashStepType.Finish)
                    {
                        //完成
                        order.status = 6;
                    }
                    else
                    {
                        //送还中
                        order.status = 5;
                    }
                }
                else
                {
                    order.status = 6;
                }
                #endregion

                #region 产品
                order.productlist = new List<WashProductModel>();
                if (orderItem.ProductList != null)
                {
                    IDictionary<int, WashProductModel> di_orderProduct = new Dictionary<int, WashProductModel>();
                    foreach (var orderProductItem in orderItem.ProductList)
                    {
                        if (di_orderProduct.ContainsKey(orderProductItem.ProductID))
                        {
                            di_orderProduct[orderProductItem.ProductID].count++;
                        }
                        else
                        {
                            di_orderProduct.Add(orderProductItem.ProductID, new WashProductModel()
                            {
                                id = orderProductItem.ProductID,
                                name = orderProductItem.Name,
                                price = Convert.ToInt32(orderProductItem.Price * 100),
                                count = 1,
                            });
                        }
                    }
                    order.productlist = di_orderProduct.Values.ToList();
                }
                #endregion

                #region 金额
                order.amountlist = new List<OrderAmountModel>();
                if (orderItem.AmountList != null)
                {
                    foreach (var orderAmountItem in orderItem.AmountList)
                    {
                        order.amountlist.Add(new OrderAmountModel()
                        {
                            id = orderAmountItem.ID,
                            name = orderAmountItem.Content,
                            money = Convert.ToInt32(orderAmountItem.Money * 100),
                        });
                    }
                }
                #endregion

                return MyJson(rtn);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.9	取消订单
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public JsonResult OrderCancel(string cid, string orderno)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(orderno))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var orderRtn = OrderProxy.Order_Order_SELECT_Entity(orderno, false, false, false, false, false, false);
                if (orderRtn == null || orderRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "获取订单失败"));
                }
                var orderItem = orderRtn.OBJ;
                if (orderItem.UserID != userid)
                {
                    return MyJson(BaseResult.BadResult(-1, "订单非该用户"));
                }
                var cancelRtn = OrderProxy.Order_Order_Cancel(orderItem.ID);
                if (cancelRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (cancelRtn.Success == false)
                {
                    if (cancelRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(cancelRtn.Code, cancelRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(cancelRtn.Code, cancelRtn.Message));
                    }
                }
                else if (cancelRtn.OBJ == true)
                {
                    return MyJson(BaseResult.Success);
                }
                else
                {
                    return MyJson(BaseResult.BadResult(cancelRtn.Code, cancelRtn.Message));
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.10	订单评价
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="orderno"></param>
        /// <param name="score"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public JsonResult OrderComment(string cid, string orderno, string score, string content)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(score))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(orderno))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            var scorelist = score.Split('_');
            if (scorelist.Length != 4)
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                var orderRtn = OrderProxy.Order_Order_SELECT_Entity(orderno, false, false, false, false, false, false);
                if (orderRtn == null || orderRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "订单不存在"));
                }
                var orderItem = orderRtn.OBJ;
                if (orderItem.UserID != userid)
                {
                    return MyJson(BaseResult.BadResult(-1, "订单非该用户"));
                }
                var cancelRtn = OrderProxy.Order_Feedback_ADD(
                    new WCF.OrderSystem.Contract.DataContract.Order_FeedbackDC()
                    {
                        OID = orderItem.ID,
                        Score1 = int.Parse(scorelist[0]),
                        Score2 = int.Parse(scorelist[1]),
                        Score3 = int.Parse(scorelist[2]),
                        Score4 = int.Parse(scorelist[3]),
                        Content1 = content,
                    });

                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.13	创建订单(一键下单)
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="exptime"></param>
        /// <param name="getaddsid"></param>
        /// <param name="sendaddsid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public JsonResult OrderCreateOneKey(string cid, string exptime, int getaddsid, int sendaddsid, string remark)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(exptime))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            DateTime expectTime = DateTime.Now;
            if (!DateTime.TryParseExact(exptime, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out expectTime))
            {
                return MyJson(BaseResult.ParametersError);
            }

            if (expectTime <= DateTime.Now)
            {
                return MyJson(BaseResult.BadResult(-1, "预计时间错误"));
            }
            else if (DateTime.Now.Hour >= 12
                && DateTime.Today == expectTime.Date
                && expectTime.Hour >= 12)
            {
                //当前时间12点以后 当天取件
                return MyJson(BaseResult.BadResult(-1, "物流系统升级中,12点以后下单明日取件,请重新选择时间"));
            }
            else if (DateTime.Now.Hour < 12
                && DateTime.Today == expectTime.Date
                && expectTime.Hour <= 14)
            {
                //当前时间12点以后 当天取件
                return MyJson(BaseResult.BadResult(-1, "物流系统升级中,12点以前下单15点后取件,请重新选择时间"));
            }

            #region [2015春节放假]

            if (expectTime.Date >= DateTime.Parse("2015-2-10") && expectTime.Date < DateTime.Parse("2015-3-1"))
            {
                return MyJson(BaseResult.BadResult(-1, "BOSS一任性，让已瘦成马的懒懒2月10号-2月28号回家过大年去了。亲，3月1号再来约哦。"));
            }

            #endregion

            try
            {

                //用户
                var userRtn = UserProxy.User_Info_SELECT_Entity(userid);
                if (userRtn == null || userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                if (string.IsNullOrWhiteSpace(userRtn.OBJ.MPNo))
                {
                    return MyJson(BaseResult.BadResult(-10, "请先至用户中心绑定手机"));
                }

                #region 检查用户同一天不能重复下单(未审核) 2014-10-08

                var checkOrder = OrderProxy.Order_Order_SELECT_List(null, userRtn.OBJ.ID, null, null, OrderClass.OneKey, OrderType.Normal, OrderStatus.Unaudited, null, null, null, null, null, null, 1, 1);
                if (checkOrder.Success && checkOrder.OBJ != null && checkOrder.OBJ.RecordCount >= 1)
                {
                    return MyJson(BaseResult.BadResult(-1, "您的订单正在审核中,请勿重复下单"));
                }

                #endregion

                var getAddressRtn = UserProxy.User_ConsigneeAddress_SELECT_Entity(getaddsid);
                if (getAddressRtn == null || getAddressRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "地址选择错误g"));
                }
                var sendAddressRtn = UserProxy.User_ConsigneeAddress_SELECT_Entity(sendaddsid);
                if (sendAddressRtn == null || sendAddressRtn.OBJ == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "地址选择错误s"));
                }

                var orderRtn = OrderProxy.Order_App_Onekey_Create(userid, 1,
                    new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC()
                    {
                        Consignee = getAddressRtn.OBJ.Consignee,
                        DistrictID = getAddressRtn.OBJ.DistrictID,
                        Address = getAddressRtn.OBJ.Address,
                        Mpno = getAddressRtn.OBJ.MPNo,
                    },
                    new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC()
                    {
                        Consignee = sendAddressRtn.OBJ.Consignee,
                        DistrictID = sendAddressRtn.OBJ.DistrictID,
                        Address = sendAddressRtn.OBJ.Address,
                        Mpno = sendAddressRtn.OBJ.MPNo,
                    },
                    expectTime, remark);

                if (!orderRtn.Success)
                {
                    if (orderRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(orderRtn.Code, orderRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(orderRtn.Code, orderRtn.Message));
                    }
                }
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        ///// <summary>
        ///// 18元免洗包括的产品ID
        ///// </summary>
        //public static IList<int> mianxi_18 = new List<int>()
        //{
        //    1,3,5,6,9,10,12,13,14,16,17,22,23,24,25,26,27,32,33,34,
        //    35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,
        //    54,55,56,57,58,59,60,61,62,63,67,112,113,117,121,123,142
        //};

        /// <summary>
        /// 4.16	订单金额核算
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="itemlist"></param>
        /// <param name="servfeeid"></param>
        /// <returns></returns>
        public JsonResult OrderCreateCalc(string cid, string itemlist, string servfeeid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(itemlist))
            {
                return MyJson(BaseResult.ParametersError);
            }
            if (string.IsNullOrWhiteSpace(servfeeid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            var itemList = new List<int>();
            var serviceList = servfeeid.Split('_');

            try
            {
                foreach (var item in itemlist.Split('_'))
                {
                    itemList.Add(int.Parse(item));
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }

            try
            {
                OrderCreateCalcResult rtn = new OrderCreateCalcResult();
                rtn.amountlist = new List<OrderAmountModel>();

                decimal RealProductAmount = 0;

                //用户
                #region
                var userRtn = UserProxy.User_Info_SELECT_Entity(userid);
                if (userRtn == null || userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                if (string.IsNullOrWhiteSpace(userRtn.OBJ.MPNo))
                {
                    return MyJson(BaseResult.BadResult(-10, "请先至用户中心绑定手机"));
                }

                rtn.money = Convert.ToInt32(userRtn.OBJ.Money * 100);
                #endregion

                //产品
                #region
                var getProductRtn = WashProxy.web_Wash_Product_SELECT_Entity(itemList);
                if (getProductRtn == null || getProductRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                rtn.productlist = new List<WashProductModel>();
                foreach (var item in getProductRtn.OBJ.Values)
                {
                    var tmp_product = new WashProductModel()
                    {
                        id = item.ID,
                        count = itemList.Count(p => p == item.ID),
                        name = item.Name,
                    };

                    if (tmp_product.count <= 0) continue;

                    tmp_product.price = Convert.ToInt32(item.SalesPrice * 100 * tmp_product.count);

                    RealProductAmount += item.SalesPrice * tmp_product.count;

                    //if (userRtn.OBJ.Level > 0 && item.SalesPrice == 9)
                    //{
                    //    tmp_product.price = Convert.ToInt32(6 * 100 * tmp_product.count);

                    //    RealProductAmount += 6 * tmp_product.count;
                    //}
                    //else
                    //{
                    //    tmp_product.price = Convert.ToInt32(item.SalesPrice * 100 * tmp_product.count);

                    //    RealProductAmount += item.SalesPrice * tmp_product.count;
                    //}

                    rtn.productlist.Add(tmp_product);
                }

                #endregion

                //地址
                #region
                var addsRtn = UserProxy.User_ConsigneeAddress_SELECT_List(userid);
                if (addsRtn != null && addsRtn.OBJ != null && addsRtn.OBJ.Count > 0)
                {
                    rtn.address = new UserAddsModel()
                    {
                        id = addsRtn.OBJ[0].ID,
                        name = addsRtn.OBJ[0].Consignee,
                        phone = addsRtn.OBJ[0].MPNo,
                        districtid = addsRtn.OBJ[0].DistrictID,
                        districtname = addsRtn.OBJ[0].DistrictName,
                        adds = addsRtn.OBJ[0].Address,
                        @default = addsRtn.OBJ[0].IsDefault,
                    };
                }
                #endregion

                //卡
                #region
                rtn.card = new List<UserCardModel>();
                var cardRtn = UserProxy.User_Card_SELECT_List(userid);
                if (cardRtn != null && cardRtn.OBJ != null)
                {
                    foreach (var item in cardRtn.OBJ)
                    {
                        rtn.card.Add(new UserCardModel()
                        {
                            id = item.ID,
                            name = "面额:￥" + item.Money.ToString("0") + ",余额:￥" + item.Balance.ToString("0"),
                            no = item.Number,
                            money = Convert.ToInt32(item.Money * 100),
                            balance = Convert.ToInt32(item.Balance * 100),
                        });
                    }
                }
                #endregion

                //优惠券
                #region
                rtn.coupon = new List<UserCouponModel>();
                var couponRtn = UserProxy.User_Coupon_SELECT_List(userid, null, null, LazyAtHome.WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 10);
                if (couponRtn != null && couponRtn.OBJ != null)
                {
                    foreach (var item in couponRtn.OBJ.ReturnList)
                    {
                        rtn.coupon.Add(new UserCouponModel()
                        {
                            id = item.ID,
                            name = item.Name,
                            money = Convert.ToInt32(item.FaceValue < 0 ? 0 : item.FaceValue * 100),
                            expdate = item.UseEndDate.ToString("yyyy.MM.dd HH:mm:ss"),
                            expflag = 0,
                        });
                    }
                }
                #endregion

                if (RealProductAmount < 18)
                {
                    rtn.amountlist.Add(new OrderAmountModel()
                    {
                        id = 2,
                        name = "同城快递",
                        money = Convert.ToInt32((18m - RealProductAmount) * 100),
                    });
                }

                //if (RealProductAmount > 0)
                //{
                //    if (Count_18 > 0)
                //    {
                //        rtn.amountlist.Add(new OrderAmountModel()
                //        {
                //            id = 2,
                //            name = "同城快递",
                //            money = Count_18 * 6 * 100,
                //        });
                //    }
                //}
                //else if (Count_18 > 0 && Count_18 <= 3)
                //{
                //    rtn.amountlist.Add(new OrderAmountModel()
                //    {
                //        id = 2,
                //        name = "同城快递",
                //        money = 18 * 100,
                //    });
                //}
                //else if (Count_18 > 3)
                //{
                //    rtn.amountlist.Add(new OrderAmountModel()
                //    {
                //        id = 2,
                //        name = "同城快递",
                //        money = Count_18 * 6 * 100,
                //    });
                //}

                //服务费
                #region
                //bool fastExp = false;
                foreach (var item in serviceList)
                {
                    switch (item)
                    {
                        //case "1":
                        //    rtn.amountlist.Add(new OrderAmountModel()
                        //    {
                        //        id = 1,
                        //        name = "普通快递",
                        //        money = 800,
                        //    });
                        //    if (rtn.productlist.Sum(p => p.price) >= 2500)
                        //    {
                        //        rtn.amountlist.Add(new OrderAmountModel()
                        //        {
                        //            id = -1,
                        //            name = "快递减免",
                        //            money = -800,
                        //        });
                        //    }
                        //    break;
                        case "3":
                            rtn.amountlist.Add(new OrderAmountModel()
                            {
                                id = 3,
                                name = "加急快递",
                                money = 10000,
                            });
                            //fastExp = true;
                            break;
                        //case "3":
                        //    rtn.amountlist.Add(new OrderAmountModel()
                        //    {
                        //        id = 3,
                        //        name = "普通包装",
                        //        money = 0,
                        //    });
                        //    break;
                        //case "4":
                        //    rtn.amountlist.Add(new OrderAmountModel()
                        //    {
                        //        id = 4,
                        //        name = "精美包装",
                        //        money = 1000,
                        //    });
                        //    break;
                        default:
                            break;
                    }
                }
                ////没有选加急
                //if (!fastExp)
                //{

                //}
                #endregion

                //总金额
                rtn.amount = rtn.productlist.Sum(p => p.price) + rtn.amountlist.Sum(p => p.money);

                return MyJson(rtn);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.17	创建订单(普通订单)
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="itemlist"></param>
        /// <param name="servfeeid"></param>
        /// <returns></returns>
        public JsonResult OrderCreate(string cid, string itemlist, string servfeeid,
            string exptime, int getaddsid, int sendaddsid, int usemoney, string usecard,
            string usecoupon, string remark)
        {

            return MyJson(BaseResult.BadResult(-1, "亲，目前不支持普通订单。"));

            //if (string.IsNullOrWhiteSpace(cid))
            //{
            //    return MyJson(BaseResult.ParametersError);
            //}
            //if (string.IsNullOrWhiteSpace(itemlist))
            //{
            //    return MyJson(BaseResult.ParametersError);
            //}
            //Guid userid = Guid.Empty;

            //if (!Guid.TryParse(cid, out userid))
            //{
            //    return MyJson(BaseResult.ParametersError);
            //}

            //var itemList = new List<int>();
            //var couponIDList = new List<int>();
            //DateTime? expectTime = null;
            //IDictionary<string, decimal> activityList = null;
            //IDictionary<string, decimal> serviceList = new Dictionary<string, decimal>();
            //IDictionary<int, decimal> lazyCardList = null;
            //var expressFee = 0m;
            //var expressFeeDiscount = 0m;
            //try
            //{
            //    foreach (var item in itemlist.Split('_'))
            //    {
            //        itemList.Add(int.Parse(item));
            //    }

            //    DateTime dtNow = DateTime.Now;
            //    if (dtNow.Hour >= 12)
            //    {
            //        //12点以后 明天取件
            //        expectTime = dtNow.Date.AddDays(1).AddHours(10).AddMinutes(1);
            //    }
            //    else
            //    {
            //        expectTime = dtNow.Date.AddHours(15).AddMinutes(1);
            //    }

            //    #region [2015春节放假]

            //    if (expectTime.Value >= DateTime.Parse("2015-2-10") && expectTime.Value < DateTime.Parse("2015-3-1"))
            //    {
            //        return MyJson(BaseResult.BadResult(-1, "BOSS一任性，让已瘦成马的懒懒2月10号-2月28号回家过大年去了。亲，3月1号再来约哦。"));
            //    }

            //    #endregion

            //    ////期望时间
            //    //if (!string.IsNullOrWhiteSpace(exptime))
            //    //{
            //    //    expectTime = DateTime.ParseExact(exptime, "yyyyMMddHHmmss", null);
            //    //    if (expectTime <= DateTime.Now)
            //    //    {
            //    //        return MyJson(BaseResult.BadResult(-1, "预计时间错误"));
            //    //    }
            //    //    else if (DateTime.Now.Hour >= 12
            //    //        && DateTime.Today == expectTime.Value.Date
            //    //        && expectTime.Value.Hour >= 12)
            //    //    {
            //    //        //当前时间12点以后 当天取件
            //    //        return MyJson(BaseResult.BadResult(-1, "物流系统升级中,12点以后下单明日取件,请重新选择时间"));
            //    //    }
            //    //    else if (DateTime.Now.Hour < 12
            //    //        && DateTime.Today == expectTime.Value.Date 
            //    //        && expectTime.Value.Hour <= 14)
            //    //    {
            //    //        //当前时间12点以后 当天取件
            //    //        return MyJson(BaseResult.BadResult(-1, "物流系统升级中,12点以前下单下午3点后取件,请重新选择时间"));
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    DateTime dtNow = DateTime.Now;
            //    //    if (dtNow.Hour >= 12)
            //    //    {
            //    //        //12点以后 明天取件
            //    //        expectTime = dtNow.Date.AddDays(1).AddHours(10).AddMinutes(1);
            //    //    }
            //    //    else
            //    //    {
            //    //        expectTime = dtNow.Date.AddHours(15).AddMinutes(1);
            //    //    }
            //    //}

            //    //产品
            //    var getProductRtn = WashProxy.web_Wash_Product_SELECT_Entity(itemList);
            //    if (getProductRtn == null || getProductRtn.OBJ == null)
            //    {
            //        return MyJson(BaseResult.EmptyResult);
            //    }

            //    //18元3件免洗0元
            //    decimal Money_99 = 0;
            //    decimal Money_99_Sum = 0;
            //    decimal RealProductAmount = 0;

            //    var productlist = new List<WashProductModel>();
            //    foreach (var item in getProductRtn.OBJ.Values)
            //    {
            //        var tmp_product = new WashProductModel()
            //        {
            //            id = item.ID,
            //            count = itemList.Count(p => p == item.ID),
            //            name = item.Name,
            //            price = Convert.ToInt32(item.SalesPrice * 100 * itemList.Count(p => p == item.ID)),
            //        };

            //        if (tmp_product.count <= 0) continue;

            //        tmp_product.price = Convert.ToInt32(item.SalesPrice * 100 * tmp_product.count);

            //        if (item.SalesPrice == 9.9m)
            //        {
            //            Money_99_Sum += item.SalesPrice * tmp_product.count;

            //            Money_99 = item.SalesPrice;
            //        }

            //        RealProductAmount += item.SalesPrice * tmp_product.count;

            //        productlist.Add(tmp_product);
            //    }

            //    if (RealProductAmount < 18)
            //    {
            //        expressFee = 18m - RealProductAmount;
            //    }

            //    //if (RealProductAmount > 0)
            //    //{
            //    //    if (Count_18 > 0)
            //    //    {
            //    //        expressFee = Count_18 * 6;
            //    //    }
            //    //}
            //    //else if (Count_18 > 0 && Count_18 <= 3)
            //    //{
            //    //    expressFee = 18;
            //    //}
            //    //else if (Count_18 > 3)
            //    //{
            //    //    expressFee = Count_18 * 6;
            //    //}

            //    bool fastExp = false;
            //    if (!string.IsNullOrWhiteSpace(servfeeid))
            //    {
            //        var servfeeList = servfeeid.Split('_');
            //        //if (servfeeList.Contains("1"))
            //        //{
            //        //    //25免快递
            //        //    if (productlist.Sum(p => p.price) > 2500)
            //        //    {
            //        //        expressFee = 8;
            //        //        expressFeeDiscount = 8;
            //        //    }
            //        //}
            //        if (servfeeList.Contains("3"))
            //        {
            //            //选择加急
            //            expressFee = 100;
            //            expressFeeDiscount = 0;
            //            fastExp = true;
            //        }
            //        //if (servfeeList.Contains("4"))
            //        //{
            //        //    serviceList.Add("精美包装", 10);
            //        //}
            //    }

            //    //使用券
            //    if (!string.IsNullOrWhiteSpace(usecoupon))
            //    {
            //        couponIDList.Add(int.Parse(usecoupon));
            //    }

            //    //没有选加急
            //    if (!fastExp && expressFee != 0)
            //    {
            //        if (couponIDList.Count > 0)
            //        {
            //            var user_couponlist = UserProxy.User_Coupon_SELECT_List(userid, null, null, CouponStatus.Normal, null, null, 1, 50);
            //            if (user_couponlist != null && user_couponlist.OBJ != null && user_couponlist.OBJ.ReturnList != null)
            //            {
            //                var use_coupon = user_couponlist.OBJ.ReturnList.FirstOrDefault(p => p.ID == couponIDList.First());
            //                if (use_coupon != null)
            //                {

            //                    //18元券 只抵6元和9元
            //                    //19.8元券 只抵9.9元
            //                    if (use_coupon.CouponID == 2
            //                        || use_coupon.CouponID == 4
            //                        || use_coupon.CouponID == 5
            //                        )
            //                    {
            //                        expressFee = 0;
            //                        expressFeeDiscount = 0;
            //                    }
            //                }
            //                else
            //                {
            //                    return MyJson(BaseResult.BadResult(-1, "优惠券不存在"));
            //                }
            //            }
            //            else
            //            {
            //                return MyJson(BaseResult.BadResult(-1, "优惠券不存在"));
            //            }
            //        }
            //    }

            //    //使用卡
            //    if (!string.IsNullOrWhiteSpace(usecard))
            //    {
            //        lazyCardList = new Dictionary<int, decimal>();
            //        lazyCardList.Add(int.Parse(usecard), 0);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    return MyJson(BaseResult.SystemError(ex));
            //}

            //try
            //{
            //    OrderCreateResult rtn = new OrderCreateResult();

            //    var getAddressRtn = UserProxy.User_ConsigneeAddress_SELECT_Entity(getaddsid);
            //    if (getAddressRtn == null || getAddressRtn.OBJ == null)
            //    {
            //        return MyJson(BaseResult.BadResult(-1, "地址选择错误g"));
            //    }
            //    var sendAddressRtn = UserProxy.User_ConsigneeAddress_SELECT_Entity(sendaddsid);
            //    if (sendAddressRtn == null || sendAddressRtn.OBJ == null)
            //    {
            //        return MyJson(BaseResult.BadResult(-1, "地址选择错误s"));
            //    }

            //    var createRtn = OrderProxy.Order_App_Create(userid, 1,
            //       new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC()
            //        {
            //            Consignee = getAddressRtn.OBJ.Consignee,
            //            DistrictID = getAddressRtn.OBJ.DistrictID,
            //            Address = getAddressRtn.OBJ.Address,
            //            Mpno = getAddressRtn.OBJ.MPNo,
            //        },
            //        new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC()
            //        {
            //            Consignee = sendAddressRtn.OBJ.Consignee,
            //            DistrictID = sendAddressRtn.OBJ.DistrictID,
            //            Address = sendAddressRtn.OBJ.Address,
            //            Mpno = sendAddressRtn.OBJ.MPNo,
            //        },
            //        itemList, expressFee, expressFeeDiscount,
            //        usemoney,
            //        lazyCardList, couponIDList,
            //        activityList, serviceList,
            //        expectTime, remark
            //        );
            //    if (createRtn == null)
            //    {
            //        return MyJson(BaseResult.EmptyResult);
            //    }
            //    if (createRtn.Success == false)
            //    {
            //        if (createRtn.Code != -999)
            //        {
            //            return MyJson(BaseResult.BadResult(createRtn.Code, createRtn.Message));
            //        }
            //        else
            //        {
            //            return MyJson(BaseResult.SystemError(createRtn.Code, createRtn.Message));
            //        }
            //    }

            //    rtn.payresult = LazyAtHome.API.Mobile.App_Code.PublicFun.CreateAlipayPayParam(
            //      createRtn.OBJ.OrderNumber, createRtn.OBJ.Title, createRtn.OBJ.Title, createRtn.OBJ.TotalAmount - createRtn.OBJ.PayAmount
            //      );

            //    return MyJson(rtn);
            //}
            //catch (Exception ex)
            //{
            //    return MyJson(BaseResult.SystemError(ex));
            //}
        }
    }
}