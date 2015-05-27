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
        /// 一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_App_Onekey_Create(Guid iUserID, int iSite,
               Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            DateTime? iExpectTime, string iUserRemark, string iCouponCode = null,
            int? iSendType = 1, string iInviteCode = null, int iUseMoney = 0)
        {
            return Order_Create_Onekey_0929(iUserID, iSite, Channel.APP,
                iGetAddress, iSendAddress, iExpectTime, iUserRemark, null, iCouponCode, iSendType, iInviteCode, iUseMoney);
            //Order_OrderDC order = new Order_OrderDC()
            //{
            //    OrderClass = (int)OrderClass.OneKey,
            //    OrderType = (int)OrderType.Normal,
            //    UserID = user.ID,
            //    Title = "一键下单",
            //    TotalAmount = 0,
            //    PayAmount = 0,
            //    ExpectTime = iExpectTime,
            //    //0:一键下单(未审核)
            //    OrderStatus = 0,
            //    Site = iSite,
            //    Channel = (int)Channel.APP,
            //    UserRemark = iUserRemark,
            //};

            //iGetAddress.Type = (int)ConsigneeAddressType.Get;
            //order.GetAddress = iGetAddress;
            //iSendAddress.Type = (int)ConsigneeAddressType.Send;
            //order.SendAddress = iSendAddress;

            //var orderID = orderDAL.Order_Order_ADD(order);

            //order = orderDAL.Order_Order_SELECT_Entity(orderID, false, false, true, false, false, false);

            //return new ReturnEntity<Order_OrderDC>(order);
        }

        /// <summary>
        /// 普通订单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iCouponIDList"></param>
        /// <param name="iActivityList"></param>
        /// <param name="iServiceList"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_App_Create(Guid iUserID, int iSite,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            IList<int> iProductIDList,
            decimal iExpressFee, decimal iExpressFeeDiscount,
            decimal iUserMoney, IDictionary<int, decimal> iLazyCardList, IList<int> iCouponIDList,
            IDictionary<string, decimal> iActivityList, IDictionary<string, decimal> iServiceList,
            DateTime? iExpectTime, string iUserRemark, int? iSendType = 1, string iInviteCode = null)
        {
            var user = orderDAL.User_Info_SELECT_Entity(iUserID);
            if (user == null)
            {
                return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
            }

            int? iUserCouponID = null;

            if (iCouponIDList != null && iCouponIDList.Count > 0)
            {
                iUserCouponID = iCouponIDList.First();
            }

            var orderCreateRtn = Order_Create_0929(iUserID, iSite, Channel.APP, iProductIDList
                , iExpressFee, iExpressFeeDiscount
                , 0, null, iGetAddress, iSendAddress
                , 0, iActivityList, iUserCouponID, iUserRemark, true, null, iExpectTime, iSendType, iInviteCode);

            if (!orderCreateRtn.Success || orderCreateRtn.OBJ == null)
            {
                return orderCreateRtn;
            }

            #region 剩余支付处理
            var totalMoney = orderCreateRtn.OBJ.TotalAmount - orderCreateRtn.OBJ.PayAmount;
            //懒人卡支付
            if (iLazyCardList != null)
            {
                foreach (var lazyCardID in iLazyCardList.Keys)
                {
                    //剩余需支付金额为0
                    if (totalMoney <= 0) break;

                    //此次懒人卡需支付的金额
                    var lazyCardPayMoney = 0m;

                    var userCard = orderDAL.User_Card_SELECT_Entity(user.ID, lazyCardID);
                    if (userCard == null)
                    {
                        Log_Info("懒人卡不存在" + user.ID + "_" + lazyCardID);
                        return new ReturnEntity<Order_OrderDC>(-1, "懒人卡不存在" + user.ID + "_" + lazyCardID);
                    }

                    //计算需支付的金额
                    if (userCard.Balance > totalMoney)
                    {
                        lazyCardPayMoney = totalMoney;
                    }
                    else
                    {
                        lazyCardPayMoney = userCard.Balance;
                    }

                    //支付
                    if (lazyCardPayMoney > 0)
                    {
                        var payRtn = Order_Order_Pay(orderCreateRtn.OBJ.OrderNumber, lazyCardPayMoney, PayMoneyType.LazyCard, PayMoneyChannel.APP, DateTime.Now, lazyCardID.ToString());
                        if (!payRtn.Success || payRtn.OBJ == false)
                        {
                            Log_Fatal("用户卡支付失败 " + lazyCardID + "_" + lazyCardPayMoney + "_" + payRtn.Message);
                            return new ReturnEntity<Order_OrderDC>(-1, "用户卡支付失败" + lazyCardID);
                        }

                        totalMoney -= lazyCardPayMoney;
                    }
                }
            }

            //使用余额支付
            if (iUserMoney > 0 && totalMoney > 0)
            {
                if (user.Money <= 0)
                {
                    iUserMoney = 0;
                }
                else if (user.Money > totalMoney)
                {
                    iUserMoney = totalMoney;
                }
                else
                {
                    iUserMoney = user.Money;
                }

                //支付
                if (iUserMoney > 0)
                {
                    var payRtn = Order_Order_Pay(orderCreateRtn.OBJ.OrderNumber, iUserMoney, PayMoneyType.Balance, PayMoneyChannel.APP, DateTime.Now, null);
                    if (!payRtn.Success || payRtn.OBJ == false)
                    {
                        return new ReturnEntity<Order_OrderDC>(-1, payRtn.Message);
                    }
                }
            }

            #endregion

            //Order_Order_Finish();

            var order = orderDAL.Order_Order_SELECT_Entity(orderCreateRtn.OBJ.OrderNumber, false, true, false, false, false, false);

            return new ReturnEntity<Order_OrderDC>(order);

            //if (iGetAddress == null)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-1, "取货地址错误");
            //}
            //if (iSendAddress == null)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-1, "送货地址错误");
            //}
            //if (iProductIDList == null || iProductIDList.Count == 0)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-1, "未选择产品");
            //}

            //var user = orderDAL.User_Info_SELECT_Entity(iUserID);
            //if (user == null)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
            //}
            //if (user.AccountStatus != 1)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-1, "用户已冻结");
            //}
            //if (user.UserStatus != 1)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-1, "用户已锁定");
            //}
            ////if (user.Money < iUserMoney)
            ////{
            ////    return new ReturnEntity<Order_OrderDC>(-1, "用户余额不足");
            ////}

            ////跳转第三方支付的金额
            //var couponPayAmount = 0m;
            //var cardPayAmount = 0m;
            //var expressPayAmount = 0m;
            //var activityPayAmount = 0m;
            //var productPayAmount = 0m;

            //var order = new Order_OrderDC()
            //{
            //    OrderClass = (int)OrderClass.Normal,
            //    OrderType = (int)OrderType.Normal,
            //    UserID = user.ID,
            //    OrderStatus = (int)OrderStatus.Create,
            //    Site = iSite,
            //    Channel = (int)Channel.APP,

            //    Title = "",
            //    TotalAmount = 0,
            //    PayAmount = 0,

            //    UserRemark = iUserRemark,
            //};

            //order.ProductList = new List<Order_ProductDC>();

            //#region 产品

            ////18元3件免洗0元
            ////int Count_18 = 0;
            ////decimal Money_18 = 0;
            //decimal Money_69 = 0;
            //decimal RealProductAmount = 0;

            //foreach (var item in iProductIDList)
            //{
            //    var entity = orderDAL.Wash_Product_SELECT_Entity(item);
            //    if (entity == null)
            //    {
            //        return new ReturnEntity<Order_OrderDC>(-1, "无效产品");
            //    }
            //    else
            //    {
            //        var tmp_product = new Order_ProductDC()
            //           {
            //               ProductID = entity.ID,
            //               Name = entity.Name,
            //               Type = 1,
            //               //Price = entity.SalesPrice,
            //               CategoryName = entity.CategoryName,
            //               WebName = entity.WebName,
            //           };

            //        tmp_product.Price = entity.SalesPrice;
            //        RealProductAmount += tmp_product.Price;

            //        if (tmp_product.Price == 6 || tmp_product.Price == 9)
            //        {
            //            if (Money_69 < tmp_product.Price)
            //            {
            //                Money_69 = tmp_product.Price;
            //            }
            //        }

            //        ////if (mianxi_18.Contains(tmp_product.ProductID) && Count_18 < 3)
            //        //if (mianxi_18.Contains(tmp_product.ProductID))
            //        //{
            //        //    tmp_product.Price = entity.MarketPrice;
            //        //    //18元快递费免洗3件
            //        //    Money_18 += tmp_product.Price;

            //        //    Count_18++;
            //        //}
            //        //else
            //        //{
            //        //    tmp_product.Price = entity.SalesPrice;

            //        //    RealProductAmount += tmp_product.Price;
            //        //}

            //        order.ProductList.Add(tmp_product);
            //    }
            //}
            //#endregion


            //order.Title = order.ProductList.Count == 1
            //        ? order.ProductList.First().CategoryName : order.ProductList.First().CategoryName + " 等(" + order.ProductList.Count + ")";

            //iGetAddress.Type = (int)ConsigneeAddressType.Get;
            //order.GetAddress = iGetAddress;
            //iSendAddress.Type = (int)ConsigneeAddressType.Send;
            //order.SendAddress = iSendAddress;


            //order.AmountList = new List<Order_AmountDC>();

            //#region 快递费
            //if (iExpressFee != 0)
            //{
            //    if (Money_69 > 0 && iExpressFee != 100)
            //    {
            //        //用免洗,不用快递费
            //    }
            //    else
            //    {
            //        order.AmountList.Add(new Order_AmountDC()
            //        {
            //            Money = Math.Abs(iExpressFee),
            //            Type = (int)AmountType.ExpressFee,
            //            Content = "快递费",
            //        });

            //        //第三方支付的金额 快递费
            //        expressPayAmount += Math.Abs(iExpressFee);

            //        if (iExpressFeeDiscount != 0)
            //        {
            //            order.AmountList.Add(new Order_AmountDC()
            //            {
            //                Money = -Math.Abs(iExpressFeeDiscount),
            //                Type = (int)AmountType.ExpressFeeReduce,
            //                Content = "快递费抵扣",
            //            });

            //            //第三方支付的金额 快递费
            //            expressPayAmount -= Math.Abs(iExpressFeeDiscount);
            //        }
            //    }
            //}
            //#endregion

            //#region [免洗优惠]

            ////if (Money_18 > 0)
            ////{
            ////    order.AmountList.Add(new Order_AmountDC()
            ////    {
            ////        Money = -Math.Abs(Money_18),
            ////        Type = (int)AmountType.Activity,
            ////        Content = "活动优惠",
            ////    });

            ////    activityPayAmount += Math.Abs(Money_18);
            ////}

            //#endregion

            ////产品金额
            //order.TotalAmount += RealProductAmount;
            ////快递费
            //order.TotalAmount += expressPayAmount;

            //#region 活动

            //if (iActivityList != null)
            //{
            //    foreach (var item in iActivityList)
            //    {
            //        order.AmountList.Add(new Order_AmountDC()
            //        {
            //            Money = -Math.Abs(item.Value),
            //            Type = (int)AmountType.Activity,
            //            Content = item.Key,
            //        });
            //        //已支付金额是优惠掉的钱
            //        order.PayAmount += Math.Abs(item.Value);
            //        //第三方支付的金额 减去优惠
            //        activityPayAmount += Math.Abs(item.Value);
            //    }
            //}

            //#endregion

            //IList<UserSystem.Contract.DataContract.User_CouponDC> useCouponList = new List<UserSystem.Contract.DataContract.User_CouponDC>();

            //#region 优惠券检验
            //if (iCouponIDList != null)
            //{
            //    foreach (var item in iCouponIDList)
            //    {
            //        var coupon = orderDAL.User_Coupon_SELECT_Entity(user.ID, item);
            //        if (coupon == null)
            //        {
            //            Log_Info("优惠券不存在" + user.ID + "_" + item);
            //            return new ReturnEntity<Order_OrderDC>(-1, "优惠券不存在");
            //        }
            //        else if (coupon.UseBeginDate >= DateTime.Now)
            //        {
            //            Log_Info("优惠券未开始" + user.ID + "_" + item);
            //            return new ReturnEntity<Order_OrderDC>(-1, "优惠券未开始");
            //        }
            //        else if (coupon.UseEndDate <= DateTime.Now)
            //        {
            //            Log_Info("优惠券已过期" + user.ID + "_" + item);
            //            return new ReturnEntity<Order_OrderDC>(-1, "优惠券已过期");
            //        }

            //        //6选1单品试洗券 修改为 免费洗产品18元
            //        if (coupon.CouponID == 4)
            //        {
            //            if (Money_69 > 0)
            //            {
            //                coupon.FaceValue = Money_69;
            //            }
            //            else
            //            {
            //                return new ReturnEntity<Order_OrderDC>(-1, "未选择可优惠产品");
            //            }
            //            //if (Count_18 == 0)
            //            //{
            //            //    return new ReturnEntity<Order_OrderDC>(-1, "未选择可优惠产品");
            //            //}
            //            //else if (Count_18 <= 3 && RealProductAmount > 0)
            //            //{
            //            //    coupon.FaceValue = Count_18 * 6;
            //            //}
            //            //else
            //            //{
            //            //    coupon.FaceValue = 18;
            //            //}
            //        }
            //        //0:全类别使用 1:限品类
            //        else if (coupon.UseClass == 1)
            //        {
            //            if (coupon.CouponProductList == null)
            //            {
            //                return new ReturnEntity<Order_OrderDC>(-1, "优惠券限品类1");
            //            }
            //            foreach (var couponProduct in coupon.CouponProductList)
            //            {
            //                //购买产品中包含优惠券限制品类
            //                if (iProductIDList.Contains(couponProduct.ProductID))
            //                {
            //                    //优惠券实际面值小于可用品类的金额
            //                    if (coupon.FaceValue < couponProduct.SalesPrice)
            //                    {
            //                        coupon.FaceValue = couponProduct.SalesPrice;
            //                    }
            //                }
            //            }
            //            if (coupon.FaceValue <= 0)
            //            {
            //                //购买产品中没有找到限制的品类
            //                return new ReturnEntity<Order_OrderDC>(-1, "优惠券限品类");
            //            }
            //        }
            //        //第三方支付的金额 优惠券抵扣
            //        couponPayAmount += coupon.FaceValue;

            //        useCouponList.Add(coupon);
            //    }
            //}
            //#endregion

            ////零时订单总金额
            //var tmp_TotalAmount = order.TotalAmount;

            ////减去优惠券抵扣的金额
            //tmp_TotalAmount -= couponPayAmount;

            //#region 懒人卡检验
            //if (iLazyCardList != null)
            //{
            //    var keys = iLazyCardList.Keys.ToArray();
            //    foreach (var itemKey in keys)
            //    {
            //        var userCard = orderDAL.User_Card_SELECT_Entity(user.ID, itemKey);
            //        if (userCard == null)
            //        {
            //            Log_Info("懒人卡不存在" + user.ID + "_" + itemKey + "_" + iLazyCardList[itemKey]);
            //            return new ReturnEntity<Order_OrderDC>(-1, "懒人卡余额不足");
            //        }
            //        if (userCard.Balance > tmp_TotalAmount)
            //        {
            //            //余额足够
            //            iLazyCardList[itemKey] = tmp_TotalAmount;
            //        }
            //        else
            //        {
            //            iLazyCardList[itemKey] = userCard.Balance;
            //        }

            //        if (!orderDAL.User_Card_CheckBalance(user.ID, itemKey, iLazyCardList[itemKey]))
            //        {
            //            Log_Info("懒人卡余额不足" + user.ID + "_" + itemKey + "_" + iLazyCardList[itemKey]);
            //            return new ReturnEntity<Order_OrderDC>(-1, "懒人卡余额不足");
            //        }
            //        else
            //        {
            //            //第三方支付的金额 懒人卡抵扣
            //            cardPayAmount += iLazyCardList[itemKey];
            //        }
            //    }
            //}
            //#endregion

            ////减去懒人卡支付的金额
            //tmp_TotalAmount -= cardPayAmount;

            //#region 计算金额

            ////app需计算使用余额
            //if (iUserMoney > 0)
            //{
            //    if (tmp_TotalAmount <= 0)
            //    {
            //        //其他支付方式已抵扣全额
            //        iUserMoney = 0;
            //    }
            //    else if (user.Money <= 0)
            //    {
            //        //用户无余额
            //        iUserMoney = 0;
            //    }
            //    else
            //    {
            //        if (user.Money > tmp_TotalAmount)
            //        {
            //            //用户余额大于总金额
            //            iUserMoney = tmp_TotalAmount;
            //        }
            //        else
            //        {
            //            iUserMoney = user.Money;
            //        }
            //    }
            //}

            ////订单总金额-优惠券抵扣
            ////realPayAmount = order.TotalAmount - couponPayAmount;
            ////if (realPayAmount < 0)
            ////{
            ////    //优惠券抵扣完整个订单
            ////    realPayAmount = 0;
            ////}
            //////减活动
            ////realPayAmount -= activityPayAmount;
            //////减懒人卡
            ////realPayAmount -= cardPayAmount;
            //////减用户余额
            ////realPayAmount -= iUserMoney;
            ////if (realPayAmount < 0)
            ////{
            ////    var err = string.Format("抵扣金额错误Total:{0} coupon:{1} activity:{2} UserMoney:{3} card:{4}",
            ////                                    order.TotalAmount, couponPayAmount, activityPayAmount,
            ////                                    iUserMoney, cardPayAmount);
            ////    return new ReturnEntity<Order_OrderDC>(-2, err);
            ////}
            ////if (iRealAmount != realPayAmount)
            ////{
            ////    return new ReturnEntity<Order_OrderDC>(-2, "总金额错误");
            ////}
            //#endregion

            //var orderid = orderDAL.Order_Order_ADD(order);

            //if (string.IsNullOrEmpty(order.OrderNumber))
            //{
            //    Log_Fatal("订单完成失败" + order.OrderNumber);
            //    return new ReturnEntity<Order_OrderDC>(-1, "订单创建失败,订单号生成空");
            //}

            ////order = orderDAL.Order_Order_SELECT_Entity(orderid, false, false, false, false, false, false);

            ////0元订单 可能是优惠或免单
            //if (order.TotalAmount == 0)
            //{
            //    var finishRtn = Order_Order_Finish(order.OrderNumber);
            //    if (!finishRtn.Success || finishRtn.OBJ == false)
            //    {
            //        Log_Fatal("订单完成失败" + order.OrderNumber);
            //        return new ReturnEntity<Order_OrderDC>(-1, "订单创建失败" + order.OrderNumber);
            //    }
            //}
            //else
            //{
            //    //优惠券支付
            //    if (useCouponList != null)
            //    {
            //        foreach (var item in useCouponList)
            //        {
            //            var payRtn = Order_Order_Pay(order.OrderNumber, item.FaceValue, PayMoneyType.Coupon, PayMoneyChannel.Weixin, DateTime.Now, item.ID.ToString());
            //            if (!payRtn.Success || payRtn.OBJ == false)
            //            {
            //                Log_Fatal("用户优惠券支付失败" + item + payRtn.Message);
            //                return new ReturnEntity<Order_OrderDC>(-1, "用户优惠券支付失败" + item);
            //            }
            //        }
            //    }
            //    //余额支付
            //    if (iUserMoney > 0)
            //    {
            //        var payRtn = Order_Order_Pay(order.OrderNumber, iUserMoney, PayMoneyType.Balance, PayMoneyChannel.Weixin, DateTime.Now, null);
            //        if (!payRtn.Success || payRtn.OBJ == false)
            //        {
            //            return new ReturnEntity<Order_OrderDC>(-1, payRtn.Message);
            //        }
            //    }

            //    //懒人卡支付
            //    if (iLazyCardList != null)
            //    {
            //        foreach (var item in iLazyCardList)
            //        {
            //            var payRtn = Order_Order_Pay(order.OrderNumber, item.Value, PayMoneyType.LazyCard, PayMoneyChannel.Weixin, DateTime.Now, item.Key.ToString());
            //            if (!payRtn.Success || payRtn.OBJ == false)
            //            {
            //                Log_Fatal("用户卡支付失败 " + item.Key + "_" + item.Value + "_" + payRtn.Message);
            //                return new ReturnEntity<Order_OrderDC>(-1, "用户卡支付失败" + item.Key);
            //            }
            //        }
            //    }
            //}

            //order = orderDAL.Order_Order_SELECT_Entity(orderid, false, true, false, false, false, false);

            //return new ReturnEntity<Order_OrderDC>(order);
        }
    }
}
