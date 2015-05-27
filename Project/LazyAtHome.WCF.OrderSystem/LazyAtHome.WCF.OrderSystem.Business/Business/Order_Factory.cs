using LazyAtHome.Core.Helper;
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
        #region 工厂处理

        /// <summary>
        /// 为一键下单订单添加产品(工厂处理)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Factory_AddProduct(int iOrderID, IList<Order_ProductDC> iProductList, int iMuser)
        {
            if (iProductList == null || iProductList.Count == 0)
            {
                return new ReturnEntity<bool>(-1, "添加产品对象为空");
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

            if (order.OrderClass == (int)OrderClass.Normal)
            {
                foreach (var item in iProductList)
                {
                    //更新产品条形码
                    orderDAL.Order_Product_UPDATE_Code(item.ID, item.Code, item.Attribute);
                }
            }
            else if (order.OrderClass == (int)OrderClass.OneKey)
            {
                #region 一键下单

                if (order.TotalAmount != 0 || order.PayAmount != 0)
                {
                    return new ReturnEntity<bool>(-1, "订单金额错误");
                }
                if (order.ProductList != null && order.ProductList.Count > 0)
                {
                    return new ReturnEntity<bool>(-1, "订单已添加产品");
                }

                decimal RealProductAmount = 0;

                var checkProduct = Order_Create_Check_Product(order.ID, iProductList, true, out RealProductAmount, iMuser);
                if (!checkProduct.Success)
                {
                    return new ReturnEntity<bool>(checkProduct.Code, checkProduct.Message);
                }
                else
                {
                    foreach (var item in iProductList)
                    {
                        //时效类产品,分开送货
                        if (order.SendType != 2 && slow_item.Contains(item.ProductID))
                        {
                            order.SendType = 2;
                        }

                        orderDAL.Order_Product_ADD(item);
                    }
                }

                //计算名称
                if (iProductList.Count == 1)
                {
                    order.Title = iProductList.First().Name;
                }
                else
                {
                    order.Title = iProductList.First().Name + " 等(" + iProductList.Count + ")";
                }

                //快递费
                #region 快递费

                var expressFee = 0m;

                if (RealProductAmount < 18)
                {
                    expressFee = 18m - RealProductAmount;
                }
                else
                {
                    expressFee = 0;
                }

                #endregion

                #region 优惠券处理


                if (order.PaymentList != null && order.PaymentList.Count > 0)
                {
                    foreach (var item in order.PaymentList)
                    {
                        if (item.PayMoneyType == (int)PayMoneyType.Coupon)
                        {
                            var userCouponID = int.Parse(item.RelationID);

                            var couponFaceValue = 0m;
                            bool expressFeeFree = false;

                            var checkCoupon = Order_Create_Check_Coupon(order.UserID, userCouponID, false, iProductList, expressFee,
                                out couponFaceValue, out expressFeeFree);
                            if (checkCoupon.Success)
                            {
                                order.PayAmount += couponFaceValue;
                                if (expressFeeFree)
                                {
                                    expressFee = 0;
                                }
                                orderDAL.Order_Pay_Coupon_Factory(order.ID, item.ID, couponFaceValue);
                            }
                            //只能使用一张券
                            break;

                            //var userCoupon = orderDAL.User_Coupon_SELECT_Entity(userCouponID);
                            //if (userCoupon != null)
                            //{
                            //    //18元券 只抵6元和9元
                            //    if (userCoupon.CouponID == 2)
                            //    {
                            //        var Money_99_Sum = iProductList.Where(p => p.Price == 9.9m).Sum(p => p.Price);
                            //        if (Money_99_Sum > 19.8m)
                            //        {
                            //            //抵19.8元
                            //            order.PayAmount += 19.8m;
                            //            userCoupon.FaceValue = 19.8m;
                            //            expressFee = 0;
                            //        }
                            //        else if (Money_99_Sum > 0)
                            //        {
                            //            //抵不到18元
                            //            order.PayAmount += Money_99_Sum;
                            //            userCoupon.FaceValue = Money_99_Sum;
                            //            expressFee = 0;
                            //        }
                            //        else
                            //        {
                            //            userCoupon.FaceValue = 0;
                            //        }
                            //    }
                            //    //抵用6元或9元产品
                            //    else if (userCoupon.CouponID == 4 || userCoupon.CouponID == 5)
                            //    {
                            //        if (iProductList.Count(p => p.Price == 9.9m) > 0)
                            //        {
                            //            //抵9.9元
                            //            order.PayAmount += 9.9m;
                            //            userCoupon.FaceValue = 9.9m;
                            //            expressFee = 0;
                            //        }
                            //        //else if (iProductList.Count(p => p.Price == 6) > 0)
                            //        //{
                            //        //    //抵6元
                            //        //    order.PayAmount += 6;
                            //        //    userCoupon.FaceValue = 6;
                            //        //    expressFee = 0;
                            //        //}
                            //        else
                            //        {
                            //            userCoupon.FaceValue = 0;
                            //        }
                            //    }
                            //    else if (userCoupon.CouponID == 6)
                            //    {
                            //        order.PayAmount += RealProductAmount;
                            //        userCoupon.FaceValue = RealProductAmount;
                            //        expressFee = 0;
                            //    }
                            //    else if (userCoupon.FaceValue <= 0)
                            //    {
                            //        userCoupon.FaceValue = 0;
                            //        //return new ReturnEntity<bool>(-1, "产品非普通产品");
                            //        continue;
                            //    }
                            //    else
                            //    {
                            //        if (userCoupon.FaceValue > (RealProductAmount + expressFee))
                            //        {
                            //            userCoupon.FaceValue = (RealProductAmount + expressFee);
                            //            order.PayAmount += userCoupon.FaceValue;
                            //        }
                            //        else
                            //        {
                            //            userCoupon.FaceValue = userCoupon.FaceValue;
                            //            order.PayAmount += userCoupon.FaceValue;
                            //        }
                            //    }

                            //更新优惠券使用金额,更新订单已付金额
                            //orderDAL.Order_Pay_Coupon_Factory(order.ID, item.ID, userCoupon.FaceValue);
                            //}
                        }
                    }
                }

                #endregion

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

                order.SourceAmount = iProductList.Sum(p => p.Price) + expressFee;

                //更新金额
                if (!orderDAL.Order_Order_UPDATE_Amount_OneKey(order.ID, order.Title, order.TotalAmount, order.SourceAmount, order.SendType))
                {
                    return new ReturnEntity<bool>(-1, "订单金额更新失败:" + order.OrderNumber);
                }

                //使用余额 2014-12-10 guominjie
                if (order.PayType == (int)PayMoneyType.Balance)
                {
                    var user = orderDAL.User_Info_SELECT_Entity(order.UserID);
                    if (user != null && user.Money > 0)
                    {
                        var payMoney = order.TotalAmount - order.PayAmount;
                        if (payMoney > 0)
                        {
                            if (user.Money < order.TotalAmount - order.PayAmount)
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

                                //orderDAL.Order_Pay_Balance_Factory(order.ID, payMoney);
                            }
                            #endregion
                        }
                    }
                }

                //更新物流收费金额
                Internal_ExpressOrderChargeFee(order.OrderNumber
                    , order.TotalAmount - order.PayAmount
                    , string.Join(" ", iProductList.Select(p => p.Name).ToArray())
                    , iProductList.Count);

                #endregion
            }

            //更新状态
            Order_Step_Wash(order);

            //推送物流库存系统
            var storageBreakPackageRtn = Internal_Storage_BreakPackage(order.OrderNumber, iProductList, iMuser);
            if (!storageBreakPackageRtn.Success)
            {
                return new ReturnEntity<bool>(-1, "库存系统更新失败");
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 工厂出库 添加快递单号信息(取消)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Express_ADD_Factory(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, false, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            //if (order.ExpressList != null && order.ExpressList.Count(p => p.Type == 2) > 0)
            //{
            //    return new ReturnEntity<bool>(-1, "订单已出库");
            //}

            //更新主单信息
            orderDAL.Order_Order_UPDATE_ExpressNumber(order.ID, iExpressNumber, ExpressType.Send);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 工厂出库 添加快递单号信息(取消)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Express_ADD_Factory_Re(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, false, false, false, true, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            if (order.SendExpressNumber == iExpressNumber)
            {
                return new ReturnEntity<bool>(true);
            }
            if (order.ExpressList != null && order.ExpressList.Count(p => p.Type == 2) > 1)
            {
                return new ReturnEntity<bool>(-1, "订单已出库");
            }

            if (!orderDAL.Order_Express_DELETE(order.ID, 2, iMuser))
            {
                return new ReturnEntity<bool>(-1, "清除已有出库单错误");
            }

            Order_ExpressDC express = new Order_ExpressDC()
            {
                OID = order.ID,
                Code = iExpressNumber,
                Type = 2,
                Content = "出库",
                Obj_Status = 1,
                Obj_Cuser = iMuser,
                Obj_Muser = iMuser,
                RelationID = iRelationID,
            };

            //添加物流
            orderDAL.Order_Express_ADD(express);

            //更新主单信息
            orderDAL.Order_Order_UPDATE_ExpressNumber(order.ID, iExpressNumber, ExpressType.Send);

            //步骤变更
            Order_Step_OutFactory(order);

            ////步骤添加
            //orderDAL.Order_Step_ADD(order.ID, WashStepType.Delivery, "出库中");

            ////工厂出库消息推送
            //orderDAL.Order_OutFactory_Message(order.ID);

            return new ReturnEntity<bool>(true);
        }

        #endregion

        /// <summary>
        /// 修改产品步骤(取消)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_Step(int iOrderID, int iOrderProductID, int iStep)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, true, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }

            var rtn = orderDAL.Order_Product_UPDATE_Step(iOrderProductID, iStep);

            //订单产品已全部出库
            if (order.ProductList.Where(p => p.ID != iOrderProductID).Count(p => p.Step != 5) == 0)
            {
                Order_ExpressDC express = new Order_ExpressDC()
                {
                    OID = order.ID,
                    Code = order.SendExpressNumber,
                    Type = 2,
                    Content = "出库",
                    Obj_Status = 1,
                    Obj_Cuser = -1,
                    Obj_Muser = -1,
                    RelationID = null,
                };

                //添加快递信息
                orderDAL.Order_Express_ADD(express);

                //步骤变更
                Order_Step_OutFactory(order);
            }

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 工厂出库
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<int> Order_Product_OutFactory(int iOrderID, int iOrderProductID, string iExpressNumber, int iMuser)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderID, true, false, false, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<int>(-1, "订单号错误");
            }
            if (order.ProductList == null)
            {
                return new ReturnEntity<int>(-1, "订单内容");
            }
            var product = order.ProductList.FirstOrDefault(p => p.ID == iOrderProductID);
            if (product == null)
            {
                return new ReturnEntity<int>(-1, "产品错误");
            }

            //订单物流单号未更新
            if (!string.IsNullOrWhiteSpace(iExpressNumber) && order.SendExpressNumber != iExpressNumber)
            {
                //更新主单信息
                orderDAL.Order_Order_UPDATE_ExpressNumber(order.ID, iExpressNumber, ExpressType.Send);
            }

            //没有全部出库
            var rtn = 2;

            //已出库过
            if (product.Step5Time.HasValue)
            {
                if (order.ProductList.Count(p => !p.Step5Time.HasValue) > 0)
                {
                    //有其他未出库的
                    rtn = 2;
                }
                else
                {
                    if (order.ProductList.OrderByDescending(p => p.Step5Time).First().ID == product.ID)
                    {
                        //最晚出库
                        rtn = 1;
                    }
                }
            }
            //未出库过
            else
            {
                //更新产品为出库状态
                orderDAL.Order_Product_UPDATE_Step(iOrderProductID, (int)WashStepType.Delivery);

                //订单产品已全部出库
                if (order.ProductList.Where(p => p.ID != iOrderProductID).Count(p => p.Step != 5) == 0)
                {
                    Order_ExpressDC express = new Order_ExpressDC()
                    {
                        OID = order.ID,
                        Code = order.SendExpressNumber,
                        Type = 2,
                        Content = "出库",
                        Obj_Status = 1,
                        Obj_Cuser = -1,
                        Obj_Muser = -1,
                        RelationID = null,
                    };

                    //添加快递信息
                    orderDAL.Order_Express_ADD(express);

                    //步骤变更
                    Order_Step_OutFactory(order);

                    //已全部出库
                    rtn = 1;
                }
            }

            //更新库存系统
            Internal_Storage_FactoryPreOut(order.OrderNumber, product.Code, order.SendType, iMuser);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 更新外部编号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iOtherCode"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_OtherCode(int iOrderID, int iOrderProductID, string iOtherCode)
        {
            //更新主单信息
            var rtn = orderDAL.Order_Product_UPDATE_OtherCode(iOrderID, iOrderProductID, iOtherCode);

            return new ReturnEntity<bool>(rtn);
        }

    }
}
