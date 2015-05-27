using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
//using LazyAtHome.Library.Partners.SFExpress.Entity;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Business.Business
{
    public partial class Order
    {
        protected void Log_Info(string iMessage)
        {
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.ForegroundColor = ConsoleColor.Red;
            //VEBS.Core.Helper.LogHelper.Log_Fatal(iMessage);
            Console.WriteLine(iMessage);
            //Console.ResetColor();
        }

        protected void Log_Fatal(string iMessage)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            //VEBS.Core.Helper.LogHelper.Log_Fatal(iMessage);
            Console.WriteLine(iMessage);
            Console.ResetColor();
            Config.WriteToFile(DateTime.Today.ToString("yyyy-MM-dd") + ".txt", iMessage);
        }

        private LazyAtHome.WCF.OrderSystem.Interface.IDAL.IOrderDAL orderDAL;

        public Order()
        {
            if (orderDAL == null)
                orderDAL = new LazyAtHome.WCF.OrderSystem.DAL.OrderDAL();

            //LazyAtHome.Library.Partners.Common.WriteToFileEvent += Config.WriteToFile;
        }

        static Order _Order;

        public static Order Instance
        {
            get
            {
                if (_Order == null)
                {
                    _Order = new Order();
                }
                return _Order;
            }
        }

        #region 普通订单

        ///// <summary>
        ///// 18元免洗包括的产品ID
        ///// </summary>
        //public static IList<int> mianxi_18 = new List<int>()
        //{
        //    1,3,5,6,9,10,12,13,14,16,17,22,23,24,25,26,27,32,33,34,
        //    35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,
        //    54,55,56,57,58,59,60,61,62,63,67,112,113,117,121,123,142
        //};

        public static IList<int> slow_item = new List<int>()
        {
            2,4,7,8,11,15,18,19,20,21,28,29,30,31,66,68,93,94,95,96,107,111,112,113,114,115,116,117,
            118,119,120,122,124,125,138,139,141,142,
        };

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iRealAmount"></param>
        /// <param name="iActivityList"></param>
        /// <param name="iCouponIDList"></param>
        /// <param name="iSalesPrice"></param>
        /// <returns></returns>
        private ReturnEntity<Order_OrderDC> Order_Create_0929(Guid iUserID, int iSite, Channel iChannel,
            IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
            decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            decimal iRealAmount, IDictionary<string, decimal> iActivityList,
            int? iUserCouponID, string iUserRemark, bool iSalesPrice, string iCouponCode,
            DateTime? iExpectTime, int? iSendType, string iInviteCode)
        {
            #region 检查
            if (iGetAddress == null)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "取货地址错误");
            }
            if (!Internal_CheckAddress(iGetAddress.Address))
            {
                return new ReturnEntity<Order_OrderDC>(-1, "您附近的收衣站点正在筹建中,目前暂时无法为您提供服务");
            }

            if (string.IsNullOrWhiteSpace(iGetAddress.Mpno))
            {
                return new ReturnEntity<Order_OrderDC>(-1, "取货手机错误");
            }
            if (iSendAddress == null)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "送货地址错误");
            }
            if (string.IsNullOrWhiteSpace(iSendAddress.Mpno))
            {
                return new ReturnEntity<Order_OrderDC>(-1, "送货手机错误");
            }
            if (iProductIDList == null || iProductIDList.Count == 0)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "未选择产品");
            }
            var user = orderDAL.User_Info_SELECT_Entity(iUserID);
            if (user == null)
            {
                return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
            }
            if (user.AccountStatus != 1)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "用户已冻结");
            }
            if (user.UserStatus != 1)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "用户已锁定");
            }
            if (user.Money < iUserMoney)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "用户余额不足");
            }

            //绑定优惠券
            if (!string.IsNullOrWhiteSpace(iCouponCode))
            {
                int bindid = orderDAL.User_Coupon_Bind(user.ID, iCouponCode);
                if (bindid > 0)
                {
                    iUserCouponID = bindid;
                }
                else
                {
                    return new ReturnEntity<Order_OrderDC>(-1, "优惠券编号不存在");
                }
            }

            if (iUserCouponID == 0)
            {
                iUserCouponID = null;
            }

            #endregion

            //跳转第三方支付的金额
            //var couponPayAmount = 0m;
            //var cardPayAmount = 0m;
            var expressPayAmount = 0m;
            var activityPayAmount = 0m;


            if (iSendType != 1 && iSendType != 2)
            {
                iSendType = 1;
            }

            var order = new Order_OrderDC()
            {
                OrderClass = (int)OrderClass.Normal,
                OrderType = (int)OrderType.Normal,
                UserID = user.ID,
                OrderStatus = (int)OrderStatus.Create,
                Site = iSite,
                Channel = (int)iChannel,

                Title = "",
                TotalAmount = 0,
                PayAmount = 0,
                PayType = iUserMoney <= 0 ? 0 : (int)PayMoneyType.Balance,

                UserRemark = iUserRemark,
                ExpectTime = iExpectTime,

                SendType = iSendType.Value,
                InviteCode = iInviteCode,
            };

            order.ProductList = new List<Order_ProductDC>();

            #region 产品

            decimal RealProductAmount = 0;

            foreach (var item in iProductIDList)
            {
                //时效类产品,分开送货
                if (order.SendType != 2 && slow_item.Contains(item))
                {
                    order.SendType = 2;
                }

                order.ProductList.Add(new Order_ProductDC() { ProductID = item, Type = 1 });
            }

            var checkProduct = Order_Create_Check_Product(order.ID, order.ProductList, true, out RealProductAmount, -1);
            if (!checkProduct.Success)
            {
                return new ReturnEntity<Order_OrderDC>(checkProduct.Code, checkProduct.Message);
            }

            //decimal Money_99 = 0;
            //decimal Money_99_Sum = 0;
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
            //        {
            //            ProductID = entity.ID,
            //            Name = entity.Name,
            //            Type = 1,

            //            CategoryName = entity.CategoryName,
            //            WebName = entity.WebName,
            //        };

            //        tmp_product.Price = iSalesPrice ? entity.SalesPrice : entity.MarketPrice;

            //        //if (tmp_product.Price == 6 || tmp_product.Price == 9)
            //        if (tmp_product.Price == 9.9m)
            //        {
            //            //if (user.Level > 0 && tmp_product.Price == 9)
            //            //{
            //            //    tmp_product.Price = 6;
            //            //}

            //            Money_99_Sum += tmp_product.Price;

            //            Money_99 = tmp_product.Price;

            //            //if (Money_69 < tmp_product.Price)
            //            //{
            //            //    Money_69 = tmp_product.Price;
            //            //}

            //        }

            //        RealProductAmount += tmp_product.Price;

            //        order.ProductList.Add(tmp_product);
            //    }
            //}
            #endregion

            if (order.ProductList.Count == 1)
            {
                order.Title = order.ProductList.First().Name;
            }
            else
            {
                order.Title = order.ProductList.First().Name + " 等(" + order.ProductList.Count + ")";
            }

            iGetAddress.Type = (int)ConsigneeAddressType.Get;
            order.GetAddress = iGetAddress;
            iSendAddress.Type = (int)ConsigneeAddressType.Send;
            order.SendAddress = iSendAddress;

            //IList<UserSystem.Contract.DataContract.User_CouponDC> useCouponList = new List<UserSystem.Contract.DataContract.User_CouponDC>();

            #region 优惠券检验

            var couponFaceValue = 0m;
            bool expressFeeFree = false;

            if (iUserCouponID != null)
            {

                var checkCoupon = Order_Create_Check_Coupon(order.UserID, iUserCouponID.Value, true, order.ProductList, iExpressFee,
                             out couponFaceValue, out expressFeeFree);
                if (!checkCoupon.Success)
                {
                    return new ReturnEntity<Order_OrderDC>(checkCoupon.Code, checkCoupon.Message);
                }

                //var coupon = orderDAL.User_Coupon_SELECT_Entity(user.ID, iUserCouponID.Value);
                //if (coupon == null)
                //{
                //    Log_Info("优惠券不存在" + user.ID + "_" + iUserCouponID);
                //    return new ReturnEntity<Order_OrderDC>(-1, "优惠券不存在");
                //}
                //else if (coupon.UseBeginDate >= DateTime.Now)
                //{
                //    Log_Info("优惠券未开始" + user.ID + "_" + iUserCouponID);
                //    return new ReturnEntity<Order_OrderDC>(-1, "优惠券未开始");
                //}
                //else if (coupon.UseEndDate <= DateTime.Now)
                //{
                //    Log_Info("优惠券已过期" + user.ID + "_" + iUserCouponID);
                //    return new ReturnEntity<Order_OrderDC>(-1, "优惠券已过期");
                //}

                ////18元券 只抵6元和9元
                ////19.8元券 只抵9.9元
                //if (coupon.CouponID == 2)
                //{
                //    if (Money_99_Sum > 19.8m)
                //    {
                //        coupon.FaceValue = 19.8m;
                //    }
                //    else if (Money_99_Sum > 0)
                //    {
                //        coupon.FaceValue = Money_99_Sum;
                //    }
                //    else
                //    {
                //        return new ReturnEntity<Order_OrderDC>(-1, "未选择可优惠产品");
                //    }

                //    //if (Money_69_Sum > 18)
                //    //{
                //    //    coupon.FaceValue = 18;
                //    //}
                //    //else if (Money_69_Sum > 0)
                //    //{
                //    //    coupon.FaceValue = Money_69_Sum;
                //    //}
                //    //else
                //    //{
                //    //    return new ReturnEntity<Order_OrderDC>(-1, "未选择可优惠产品");
                //    //}
                //}
                ////6选1单品试洗券 修改为 免6元或9元
                //else if (coupon.CouponID == 4 || coupon.CouponID == 5)
                //{
                //    if (Money_99 > 0)
                //    {
                //        coupon.FaceValue = Money_99;
                //    }
                //    else
                //    {
                //        return new ReturnEntity<Order_OrderDC>(-1, "未选择可优惠产品");
                //    }
                //}
                //else if (coupon.CouponID == 6)
                //{
                //    coupon.FaceValue = RealProductAmount;
                //}
                ////0:全类别使用 1:限品类
                //else if (coupon.UseClass == 1)
                //{
                //    if (coupon.CouponProductList == null)
                //    {
                //        return new ReturnEntity<Order_OrderDC>(-1, "优惠券限品类1");
                //    }
                //    foreach (var couponProduct in coupon.CouponProductList)
                //    {
                //        //购买产品中包含优惠券限制品类
                //        if (iProductIDList.Contains(couponProduct.ProductID))
                //        {
                //            //优惠券实际面值小于可用品类的金额
                //            if (coupon.FaceValue < couponProduct.SalesPrice)
                //            {
                //                coupon.FaceValue = couponProduct.SalesPrice;
                //            }
                //        }
                //    }
                //    if (coupon.FaceValue <= 0)
                //    {
                //        //购买产品中没有找到限制的品类
                //        return new ReturnEntity<Order_OrderDC>(-1, "优惠券限品类");
                //    }
                //}
                //else if (coupon.FaceValue > order.TotalAmount)
                //{
                //    coupon.FaceValue = order.TotalAmount;
                //}

                ////第三方支付的金额 优惠券抵扣
                //couponPayAmount += coupon.FaceValue;

                //useCouponList.Add(coupon);
            }

            #endregion

            order.AmountList = new List<Order_AmountDC>();

            #region 快递费
            if (iExpressFee != 0)
            {
                order.AmountList.Add(new Order_AmountDC()
                {
                    Money = Math.Abs(iExpressFee),
                    Type = (int)AmountType.ExpressFee,
                    Content = "快递费",
                });

                //第三方支付的金额 快递费
                expressPayAmount += Math.Abs(iExpressFee);

                if (iExpressFeeDiscount != 0)
                {
                    order.AmountList.Add(new Order_AmountDC()
                    {
                        Money = -Math.Abs(iExpressFeeDiscount),
                        Type = (int)AmountType.ExpressFeeReduce,
                        Content = "快递费抵扣",
                    });

                    //第三方支付的金额 快递费
                    expressPayAmount -= Math.Abs(iExpressFeeDiscount);
                }
            }
            #endregion

            #region 活动

            if (iActivityList != null)
            {
                foreach (var item in iActivityList)
                {
                    order.AmountList.Add(new Order_AmountDC()
                    {
                        Money = -Math.Abs(item.Value),
                        Type = (int)AmountType.Activity,
                        Content = item.Key,
                    });

                    //已支付金额是优惠掉的钱
                    //order.PayAmount += Math.Abs(item.Value);
                    //第三方支付的金额 减去优惠
                    activityPayAmount += Math.Abs(item.Value);
                }
            }

            #endregion

            #region 计算金额

            //减去用户余额抵扣
            //var realPayAmount = 0m;

            //产品金额
            order.TotalAmount += RealProductAmount;

            //测试产品
            if (order.TotalAmount == 0.1m)
            {
                expressPayAmount = 0;
            }

            order.TotalAmount += expressPayAmount;

            order.TotalAmount -= activityPayAmount;

            order.SourceAmount = order.ProductList.Sum(p => p.Price) + expressPayAmount;

            ////订单总金额-优惠券抵扣
            //realPayAmount = order.TotalAmount - couponPayAmount;
            //if (realPayAmount < 0)
            //{
            //    //优惠券抵扣完整个订单
            //    realPayAmount = 0;
            //}
            ////减活动
            ////realPayAmount -= activityPayAmount;
            ////减懒人卡
            //realPayAmount -= cardPayAmount;
            ////减用户余额
            //realPayAmount -= iUserMoney;
            //if (realPayAmount < 0)
            //{
            //    var err = string.Format("抵扣金额错误Total:{0} coupon:{1} activity:{2} UserMoney:{3} card:{4}",
            //                                    order.TotalAmount, couponPayAmount, activityPayAmount,
            //                                    iUserMoney, cardPayAmount);
            //    return new ReturnEntity<Order_OrderDC>(-2, err);
            //}

            //if (iRealAmount != realPayAmount)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-2, "总金额错误");
            //}

            #endregion

            #region 懒人卡检验
            if (iLazyCardList != null)
            {
                foreach (var item in iLazyCardList)
                {
                    if (!orderDAL.User_Card_CheckBalance(user.ID, item.Key, item.Value))
                    {
                        Log_Info("懒人卡余额不足" + user.ID + "_" + item.Key + "_" + item.Value);
                        return new ReturnEntity<Order_OrderDC>(-1, "懒人卡余额不足");
                    }
                    //else
                    //{
                    //    //第三方支付的金额 懒人卡抵扣
                    //    cardPayAmount += item.Value;
                    //}
                }
            }
            #endregion

            var orderid = orderDAL.Order_Order_ADD(order);

            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                Log_Fatal("订单完成失败" + order.OrderNumber);
                return new ReturnEntity<Order_OrderDC>(-1, "订单创建失败,订单号生成空");
            }

            //0元订单 可能是优惠或免单
            if (order.TotalAmount == 0)
            {
                var finishRtn = Order_Order_Finish(order.OrderNumber);
                if (!finishRtn.Success || finishRtn.OBJ == false)
                {
                    Log_Fatal("订单完成失败" + order.OrderNumber);
                    return new ReturnEntity<Order_OrderDC>(-1, "订单创建失败" + order.OrderNumber);
                }
            }
            else
            {
                var iPayMoneyChannel = PayMoneyChannel.Null;
                switch (iChannel)
                {
                    case Channel.Web:
                        iPayMoneyChannel = PayMoneyChannel.Web;
                        break;
                    case Channel.APP:
                        iPayMoneyChannel = PayMoneyChannel.APP;
                        break;
                    case Channel.Weixin:
                        iPayMoneyChannel = PayMoneyChannel.Weixin;
                        break;
                    default:
                        break;
                }

                //优惠券支付
                if (couponFaceValue > 0 && iUserCouponID.HasValue)
                {
                    //foreach (var item in useCouponList)
                    //{
                    var payRtn = Order_Order_Pay(order.OrderNumber, couponFaceValue, PayMoneyType.Coupon, iPayMoneyChannel, DateTime.Now, iUserCouponID.Value.ToString());
                    if (!payRtn.Success || payRtn.OBJ == false)
                    {
                        Log_Fatal("用户优惠券支付失败" + iUserCouponID + payRtn.Message);
                        return new ReturnEntity<Order_OrderDC>(-1, "用户优惠券支付失败" + iUserCouponID);
                    }
                    //}
                }
                //余额支付
                if (iUserMoney > 0)
                {
                    var payRtn = Order_Order_Pay(order.OrderNumber, iUserMoney, PayMoneyType.Balance, iPayMoneyChannel, DateTime.Now, null);
                    if (!payRtn.Success || payRtn.OBJ == false)
                    {
                        return new ReturnEntity<Order_OrderDC>(-1, payRtn.Message);
                    }
                }

                //懒人卡支付
                if (iLazyCardList != null)
                {
                    foreach (var item in iLazyCardList)
                    {
                        var payRtn = Order_Order_Pay(order.OrderNumber, item.Value, PayMoneyType.LazyCard, iPayMoneyChannel, DateTime.Now, item.Key.ToString());
                        if (!payRtn.Success || payRtn.OBJ == false)
                        {
                            Log_Fatal("用户卡支付失败 " + item.Key + "_" + item.Value + "_" + payRtn.Message);
                            return new ReturnEntity<Order_OrderDC>(-1, "用户卡支付失败" + item.Key);
                        }
                    }
                }
            }

            //完成订单
            Order_Order_Finish(order.OrderNumber);

            order = orderDAL.Order_Order_SELECT_Entity(orderid, false, true, false, false, false, false);

            return new ReturnEntity<Order_OrderDC>(order);
        }

        ///// <summary>
        ///// 创建订单
        ///// </summary>
        ///// <param name="iUserID"></param>
        ///// <param name="iSite"></param>
        ///// <param name="iChannel"></param>
        ///// <param name="iProductIDList"></param>
        ///// <param name="iExpressFee"></param>
        ///// <param name="iExpressFeeDiscount"></param>
        ///// <param name="iUserMoney"></param>
        ///// <param name="iLazyCardList"></param>
        ///// <param name="iGetAddress"></param>
        ///// <param name="iSendAddress"></param>
        ///// <param name="iRealAmount"></param>
        ///// <param name="iActivityList"></param>
        ///// <param name="iCouponIDList"></param>
        ///// <param name="iSalesPrice"></param>
        ///// <returns></returns>
        //private ReturnEntity<Order_OrderDC> Order_Create(Guid iUserID, int iSite, Channel iChannel,
        //    IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
        //    decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
        //    Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
        //    decimal iRealAmount, IDictionary<string, decimal> iActivityList,
        //    IList<int> iCouponIDList = null, bool iSalesPrice = true)
        //{
        //    if (iGetAddress == null)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-1, "取货地址错误");
        //    }
        //    if (iSendAddress == null)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-1, "送货地址错误");
        //    }
        //    if (iProductIDList == null || iProductIDList.Count == 0)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-1, "未选择产品");
        //    }

        //    var user = orderDAL.User_Info_SELECT_Entity(iUserID);
        //    if (user == null)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
        //    }
        //    if (user.AccountStatus != 1)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-1, "用户已冻结");
        //    }
        //    if (user.UserStatus != 1)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-1, "用户已锁定");
        //    }
        //    if (user.Money < iUserMoney)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-1, "用户余额不足");
        //    }

        //    //跳转第三方支付的金额
        //    var couponPayAmount = 0m;
        //    var cardPayAmount = 0m;
        //    var expressPayAmount = 0m;
        //    var activityPayAmount = 0m;
        //    //var productPayAmount = 0m;


        //    var ProductList = new List<Order_ProductDC>();

        //    #region 产品

        //    ////18元3件免洗0元
        //    //int Count_18 = 0;
        //    //decimal Money_18 = 0;
        //    decimal Money_69 = 0;
        //    decimal RealProductAmount = 0;

        //    foreach (var item in iProductIDList)
        //    {
        //        var entity = orderDAL.Wash_Product_SELECT_Entity(item);
        //        if (entity == null)
        //        {
        //            return new ReturnEntity<Order_OrderDC>(-1, "无效产品");
        //        }
        //        else
        //        {
        //            var tmp_product = new Order_ProductDC()
        //            {
        //                ProductID = entity.ID,
        //                Name = entity.Name,
        //                Type = 1,

        //                CategoryName = entity.CategoryName,
        //                WebName = entity.WebName,
        //            };

        //            tmp_product.Price = iSalesPrice ? entity.SalesPrice : entity.MarketPrice;
        //            RealProductAmount += tmp_product.Price;

        //            if (tmp_product.Price == 6 || tmp_product.Price == 9)
        //            {
        //                if (Money_69 < tmp_product.Price)
        //                {
        //                    Money_69 = tmp_product.Price;
        //                }
        //            }

        //            ////if (mianxi_18.Contains(tmp_product.ProductID) && Count_18 < 3)
        //            //if (mianxi_18.Contains(tmp_product.ProductID))
        //            //{
        //            //    tmp_product.Price = entity.MarketPrice;
        //            //    //18元快递费免洗3件
        //            //    Money_18 += tmp_product.Price;

        //            //    Count_18++;
        //            //}
        //            //else
        //            //{
        //            //    tmp_product.Price = iSalesPrice ? entity.SalesPrice : entity.MarketPrice;

        //            //    RealProductAmount += tmp_product.Price;
        //            //}

        //            ProductList.Add(tmp_product);
        //        }
        //    }
        //    #endregion


        //    IList<UserSystem.Contract.DataContract.User_CouponDC> useCouponList = new List<UserSystem.Contract.DataContract.User_CouponDC>();

        //    #region 优惠券检验
        //    if (iCouponIDList != null)
        //    {
        //        foreach (var item in iCouponIDList)
        //        {
        //            var coupon = orderDAL.User_Coupon_SELECT_Entity(user.ID, item);
        //            if (coupon == null)
        //            {
        //                Log_Info("优惠券不存在" + user.ID + "_" + item);
        //                return new ReturnEntity<Order_OrderDC>(-1, "优惠券不存在");
        //            }
        //            else if (coupon.UseBeginDate >= DateTime.Now)
        //            {
        //                Log_Info("优惠券未开始" + user.ID + "_" + item);
        //                return new ReturnEntity<Order_OrderDC>(-1, "优惠券未开始");
        //            }
        //            else if (coupon.UseEndDate <= DateTime.Now)
        //            {
        //                Log_Info("优惠券已过期" + user.ID + "_" + item);
        //                return new ReturnEntity<Order_OrderDC>(-1, "优惠券已过期");
        //            }

        //            //6选1单品试洗券 修改为 免6元或9元
        //            if (coupon.CouponID == 4)
        //            {
        //                if (Money_69 > 0)
        //                {
        //                    coupon.FaceValue = Money_69;
        //                }
        //                else
        //                {
        //                    return new ReturnEntity<Order_OrderDC>(-1, "未选择可优惠产品");
        //                }
        //                //if (Count_18 == 0)
        //                //{
        //                //    return new ReturnEntity<Order_OrderDC>(-1, "未选择可优惠产品");
        //                //}
        //                //else if (Count_18 <= 3 && RealProductAmount > 0)
        //                //{
        //                //    coupon.FaceValue = Count_18 * 6;
        //                //}
        //                //else
        //                //{
        //                //    coupon.FaceValue = 18;
        //                //}
        //            }
        //            //0:全类别使用 1:限品类
        //            else if (coupon.UseClass == 1)
        //            {
        //                if (coupon.CouponProductList == null)
        //                {
        //                    return new ReturnEntity<Order_OrderDC>(-1, "优惠券限品类1");
        //                }
        //                foreach (var couponProduct in coupon.CouponProductList)
        //                {
        //                    //购买产品中包含优惠券限制品类
        //                    if (iProductIDList.Contains(couponProduct.ProductID))
        //                    {
        //                        //优惠券实际面值小于可用品类的金额
        //                        if (coupon.FaceValue < couponProduct.SalesPrice)
        //                        {
        //                            coupon.FaceValue = couponProduct.SalesPrice;
        //                        }
        //                    }
        //                }
        //                if (coupon.FaceValue <= 0)
        //                {
        //                    //购买产品中没有找到限制的品类
        //                    return new ReturnEntity<Order_OrderDC>(-1, "优惠券限品类");
        //                }
        //            }
        //            //第三方支付的金额 优惠券抵扣
        //            couponPayAmount += coupon.FaceValue;

        //            useCouponList.Add(coupon);
        //        }
        //    }
        //    #endregion
        //    #region 懒人卡检验
        //    if (iLazyCardList != null)
        //    {
        //        foreach (var item in iLazyCardList)
        //        {
        //            if (!orderDAL.User_Card_CheckBalance(user.ID, item.Key, item.Value))
        //            {
        //                Log_Info("懒人卡余额不足" + user.ID + "_" + item.Key + "_" + item.Value);
        //                return new ReturnEntity<Order_OrderDC>(-1, "懒人卡余额不足");
        //            }
        //            else
        //            {
        //                //第三方支付的金额 懒人卡抵扣
        //                cardPayAmount += item.Value;
        //            }
        //        }
        //    }
        //    #endregion

        //    var order = new Order_OrderDC()
        //    {
        //        OrderClass = (int)OrderClass.Normal,
        //        OrderType = (int)OrderType.Normal,
        //        UserID = user.ID,
        //        OrderStatus = (int)OrderStatus.Create,
        //        Site = iSite,
        //        Channel = (int)iChannel,

        //        Title = "",
        //        TotalAmount = 0,
        //        PayAmount = 0,
        //    };

        //    order.ProductList = ProductList;
        //    order.Title = ProductList.Count == 1
        //            ? ProductList.First().CategoryName : ProductList.First().CategoryName + " 等(" + ProductList.Count + ")";

        //    iGetAddress.Type = (int)ConsigneeAddressType.Get;
        //    order.GetAddress = iGetAddress;
        //    iSendAddress.Type = (int)ConsigneeAddressType.Send;
        //    order.SendAddress = iSendAddress;

        //    //订单总金额 产品金额
        //    //productPayAmount = order.ProductList.Sum(p => p.Price);

        //    order.AmountList = new List<Order_AmountDC>();

        //    #region 快递费
        //    if (iExpressFee != 0)
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
        //    #endregion

        //    #region [免洗优惠]

        //    //if (Money_18 > 0)
        //    //{
        //    //    order.AmountList.Add(new Order_AmountDC()
        //    //    {
        //    //        Money = -Math.Abs(Money_18),
        //    //        Type = (int)AmountType.Activity,
        //    //        Content = "活动优惠",
        //    //    });
        //    //    //已支付金额是优惠掉的钱
        //    //    //order.PayAmount += Math.Abs(Money_18);
        //    //    //第三方支付的金额 减去优惠
        //    //    activityPayAmount += Math.Abs(Money_18);
        //    //}

        //    #endregion

        //    #region 活动

        //    if (iActivityList != null)
        //    {
        //        foreach (var item in iActivityList)
        //        {
        //            order.AmountList.Add(new Order_AmountDC()
        //            {
        //                Money = -Math.Abs(item.Value),
        //                Type = (int)AmountType.Activity,
        //                Content = item.Key,
        //            });

        //            //已支付金额是优惠掉的钱
        //            //order.PayAmount += Math.Abs(item.Value);
        //            //第三方支付的金额 减去优惠
        //            activityPayAmount += Math.Abs(item.Value);
        //        }
        //    }
        //    #endregion

        //    #region 计算金额
        //    //减去用户余额抵扣
        //    var realPayAmount = 0m;
        //    //产品金额
        //    order.TotalAmount += RealProductAmount;
        //    //快递费
        //    order.TotalAmount += expressPayAmount;

        //    order.SourceAmount = order.ProductList.Sum(p => p.Price) + expressPayAmount;

        //    //订单总金额-优惠券抵扣
        //    realPayAmount = order.TotalAmount - couponPayAmount;
        //    if (realPayAmount < 0)
        //    {
        //        //优惠券抵扣完整个订单
        //        realPayAmount = 0;
        //    }
        //    //减活动
        //    //realPayAmount -= activityPayAmount;
        //    //减懒人卡
        //    realPayAmount -= cardPayAmount;
        //    //减用户余额
        //    realPayAmount -= iUserMoney;
        //    if (realPayAmount < 0)
        //    {
        //        var err = string.Format("抵扣金额错误Total:{0} coupon:{1} activity:{2} UserMoney:{3} card:{4}",
        //                                        order.TotalAmount, couponPayAmount, activityPayAmount,
        //                                        iUserMoney, cardPayAmount);
        //        return new ReturnEntity<Order_OrderDC>(-2, err);
        //    }

        //    //if (iRealAmount != realPayAmount)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-2, "总金额错误");
        //    //}

        //    #endregion

        //    var orderid = orderDAL.Order_Order_ADD(order);

        //    if (string.IsNullOrEmpty(order.OrderNumber))
        //    {
        //        Log_Fatal("订单完成失败" + order.OrderNumber);
        //        return new ReturnEntity<Order_OrderDC>(-1, "订单创建失败,订单号生成空");
        //    }

        //    //order = orderDAL.Order_Order_SELECT_Entity(orderid, false, false, false, false, false, false);

        //    //0元订单 可能是优惠或免单
        //    if (order.TotalAmount == 0)
        //    {
        //        var finishRtn = Order_Order_Finish(order.OrderNumber);
        //        if (!finishRtn.Success || finishRtn.OBJ == false)
        //        {
        //            Log_Fatal("订单完成失败" + order.OrderNumber);
        //            return new ReturnEntity<Order_OrderDC>(-1, "订单创建失败" + order.OrderNumber);
        //        }
        //    }
        //    else
        //    {
        //        //优惠券支付
        //        if (useCouponList != null)
        //        {
        //            foreach (var item in useCouponList)
        //            {
        //                var payRtn = Order_Order_Pay(order.OrderNumber, item.FaceValue, PayMoneyType.Coupon, PayMoneyChannel.Weixin, DateTime.Now, item.ID.ToString());
        //                if (!payRtn.Success || payRtn.OBJ == false)
        //                {
        //                    Log_Fatal("用户优惠券支付失败" + item + payRtn.Message);
        //                    return new ReturnEntity<Order_OrderDC>(-1, "用户优惠券支付失败" + item);
        //                }
        //            }
        //        }
        //        //余额支付
        //        if (iUserMoney > 0)
        //        {
        //            var payRtn = Order_Order_Pay(order.OrderNumber, iUserMoney, PayMoneyType.Balance, PayMoneyChannel.Weixin, DateTime.Now, null);
        //            if (!payRtn.Success || payRtn.OBJ == false)
        //            {
        //                return new ReturnEntity<Order_OrderDC>(-1, payRtn.Message);
        //            }
        //        }

        //        //懒人卡支付
        //        if (iLazyCardList != null)
        //        {
        //            foreach (var item in iLazyCardList)
        //            {
        //                var payRtn = Order_Order_Pay(order.OrderNumber, item.Value, PayMoneyType.LazyCard, PayMoneyChannel.Weixin, DateTime.Now, item.Key.ToString());
        //                if (!payRtn.Success || payRtn.OBJ == false)
        //                {
        //                    Log_Fatal("用户卡支付失败 " + item.Key + "_" + item.Value + "_" + payRtn.Message);
        //                    return new ReturnEntity<Order_OrderDC>(-1, "用户卡支付失败" + item.Key);
        //                }
        //            }
        //        }
        //    }

        //    order = orderDAL.Order_Order_SELECT_Entity(orderid, false, true, false, false, false, false);

        //    return new ReturnEntity<Order_OrderDC>(order);
        //}

        #endregion

        /// <summary>
        /// 一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <param name="iUserCouponID"></param>
        /// <returns></returns>
        private ReturnEntity<Order_OrderDC> Order_Create_Onekey_0929(Guid iUserID, int iSite, Channel iChannel,
           Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, DateTime? iExpectTime,
           string iUserRemark, int? iUserCouponID, string iCouponCode, int? iSendType, string iInviteCode, int iUseMoney)
        {
            if (iChannel != Channel.CustomerService && iChannel != Channel.Express)
            {
                //var checkOrder = Order_Order_SELECT_List(null, iUserID, null, null, OrderClass.OneKey, OrderType.Normal, OrderStatus.Unaudited, null, null, null, null, null, null, 1, 1);
                //if (checkOrder.Success && checkOrder.OBJ != null && checkOrder.OBJ.RecordCount >= 1)
                //{
                //    return new ReturnEntity<Order_OrderDC>(-1, "您的订单正在审核中,请勿重复下单");
                //}
                if (!orderDAL.Order_Order_ContinueCheck(iUserID))
                {
                    return new ReturnEntity<Order_OrderDC>(-91, "小主，不要任性哦，5分钟内连续下单，懒懒会手忙脚乱滴");
                }
            }

            if (iGetAddress == null)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "取货地址错误");
            }
            if (iChannel != Channel.CustomerService && !Internal_CheckAddress(iGetAddress.Address))
            {
                return new ReturnEntity<Order_OrderDC>(-1, "您附近的收衣站点正在筹建中,目前暂时无法为您提供服务");
            }
            if (string.IsNullOrWhiteSpace(iGetAddress.Mpno))
            {
                return new ReturnEntity<Order_OrderDC>(-1, "取货手机错误");
            }
            if (iSendAddress == null)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "送货地址错误");
            }
            if (string.IsNullOrWhiteSpace(iSendAddress.Mpno))
            {
                return new ReturnEntity<Order_OrderDC>(-1, "送货手机错误");
            }

            var user = orderDAL.User_Info_SELECT_Entity(iUserID);
            if (user == null)
            {
                return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
            }

            if (string.IsNullOrWhiteSpace(iInviteCode))
            {
                iInviteCode = OrderClient.Cache_GetByKey("USER_INVITECODE_" + iUserID) as string;
                Console.WriteLine("订单读取邀请码:" + iUserID + "  " + iInviteCode);
            }

            if (iUserCouponID == 0)
            {
                iUserCouponID = null;
            }

            if (iSendType != 1 && iSendType != 2)
            {
                iSendType = 1;
            }

            Order_OrderDC order = new Order_OrderDC()
            {
                OrderClass = (int)OrderClass.OneKey,
                OrderType = (int)OrderType.Normal,
                UserID = iUserID,
                Title = "一键下单",
                TotalAmount = 0,
                PayAmount = 0,
                PayType = iUseMoney == 0 ? 0 : (int)PayMoneyType.Balance,
                ExpectTime = iExpectTime,
                //0:一键下单(未审核)
                //OrderStatus = 0,

                //一键下单不再审核 2014-12-03 guominjie
                OrderStatus = 2,
                Site = iSite,
                Channel = (int)iChannel,
                UserRemark = iUserRemark,
                InviteCode = iInviteCode,
                SendType = iSendType.Value,
            };

            iGetAddress.Type = (int)ConsigneeAddressType.Get;
            order.GetAddress = iGetAddress;
            iSendAddress.Type = (int)ConsigneeAddressType.Send;
            order.SendAddress = iSendAddress;

            var orderID = orderDAL.Order_Order_ADD(order);

            //一键下单使用券
            if (iUserCouponID.HasValue)
            {
                var useCouponRtn = orderDAL.User_Coupon_Use(order.UserID, iUserCouponID.Value, order.OrderNumber);
                if (useCouponRtn != 1)
                {
                    Log_Fatal("优惠券使用失败" + order.OrderNumber + "_" + iUserCouponID.Value);
                    return new ReturnEntity<Order_OrderDC>(-1, "优惠券使用失败" + order.OrderNumber + "_" + iUserCouponID.Value);
                }
                //添加支付信息
                orderDAL.Order_Payment_ADD(new Order_PaymentDC()
                {
                    OID = orderID,
                    PayMoney = 0,
                    PayMoneyType = (int)PayMoneyType.Coupon,
                    PayChannel = (int)iChannel,
                    PayDate = DateTime.Now,
                    RelationID = iUserCouponID.ToString(),
                });
            }

            var finishRtn = Order_Order_Finish(order.OrderNumber);
            if (!finishRtn.Success)
            {
                return new ReturnEntity<Order_OrderDC>(finishRtn.Code, finishRtn.Message);
            }

            order = orderDAL.Order_Order_SELECT_Entity(orderID, false, false, true, false, false, false);

            //if (iChannel != Channel.CustomerService && iChannel != Channel.Express)
            //{
            //    Config.NotifySend(order.OrderNumber, RoleDC.Role_CustomerService, 0, 0, "一键下单需审核", null,
            //       (int)LazyAtHome.WCF.Common.Contract.Enumerate.NotifyLevel.Notification3, false, false, 0);
            //}

            return new ReturnEntity<Order_OrderDC>(order);
        }

        ///// <summary>
        ///// 支付订单
        ///// </summary>
        ///// <param name="iOrderNumber"></param>
        ///// <param name="iUserCouponID"></param>
        ///// <param name="iPayMoney"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> Order_Order_PrePay(string iOrderNumber, int? iUserCouponID,
        //    decimal? iPayMoney, PayMoneyChannel iPayMoneyChannel)
        //{
        //    //用券支付
        //    if (iUserCouponID.HasValue)
        //    {
        //        var order = orderDAL.Order_Order_SELECT_Entity(iOrderNumber, true, false, false, false, true, false);
        //        if (order == null)
        //        {
        //            return new ReturnEntity<bool>(-1, "订单号错误");
        //        }

        //        //已有产品,计算优惠券抵扣金额
        //        if (order.ProductList.Count > 0)
        //        {
        //            var couponFaceValue = 0m;

        //            var checkCoupon = Order_Create_Check_Coupon(order.UserID, iUserCouponID.Value, true, order.ProductList, iExpressFee,
        //                  out couponFaceValue, out expressFeeFree);
        //            if (!checkCoupon.Success)
        //            {
        //                return new ReturnEntity<bool>(checkCoupon.Code, checkCoupon.Message);
        //            }

        //            var payRtn = Order_Order_Pay(order.OrderNumber, couponFaceValue, PayMoneyType.Coupon, iPayMoneyChannel, DateTime.Now, iUserCouponID.Value.ToString());
        //            if (!payRtn.Success || payRtn.OBJ == false)
        //            {
        //                Log_Fatal("用户优惠券支付失败" + iUserCouponID + payRtn.Message);
        //                return new ReturnEntity<bool>(-1, "用户优惠券支付失败" + iUserCouponID);
        //            }

        //        }
        //    }
        //}

        /// <summary>
        /// 支付订单(普通个人用户)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iPayType"></param>
        /// <param name="iPayChannel"></param>
        /// <param name="iPayRelationID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_Pay(string iOrderNumber, decimal iPayMoney, PayMoneyType iPayType,
            PayMoneyChannel iPayChannel, DateTime iPayDate, string iPayRelationID)
        {
            if (iPayMoney <= 0 && iPayType != PayMoneyType.Coupon)
            {
                return new ReturnEntity<bool>(-3, "支付金额错误");
            }

            //获取订单
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderNumber, false, false, false, false, true, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            ////未完成订单,订单类型错误
            //if (order.OrderType != (int)OrderType.Normal || order.OrderStatus != 1)
            //{
            //    return new ReturnEntity<bool>(-2, "订单状态错误");
            //}
            if (order.PayStatus == (int)PayStatus.Paid)
            {
                return new ReturnEntity<bool>(-70, "订单已支付完成");
            }
            //金额超限
            if (order.TotalAmount - order.PayAmount < iPayMoney)
            {
                return new ReturnEntity<bool>(-3, "支付金额错误,支付金额超过订单金额 " + order.TotalAmount + "_" + order.PayAmount + "_" + iPayMoney);
            }

            if (iPayType == PayMoneyType.Coupon)
            {
                #region 优惠券
                var userCouponID = 0;
                if (!int.TryParse(iPayRelationID, out userCouponID))
                {
                    return new ReturnEntity<bool>(-4, "用户优惠券ID错误" + iPayRelationID);
                }
                var coupon = orderDAL.User_Coupon_SELECT_Entity(order.UserID, userCouponID);
                if (coupon == null)
                {
                    Log_Info("优惠券不存在" + order.UserID + "_" + iPayRelationID);
                    return new ReturnEntity<bool>(-1, "优惠券不存在");
                }
                else if (coupon.UseBeginDate >= DateTime.Now)
                {
                    Log_Info("优惠券未开始" + order.UserID + "_" + iPayRelationID);
                    return new ReturnEntity<bool>(-1, "优惠券未开始");
                }
                else if (coupon.UseEndDate <= DateTime.Now)
                {
                    Log_Info("优惠券已过期" + order.UserID + "_" + iPayRelationID);
                    return new ReturnEntity<bool>(-1, "优惠券已过期");
                }
                else
                {
                    var useCouponRtn = orderDAL.User_Coupon_Use(order.UserID, userCouponID, order.OrderNumber);
                    if (useCouponRtn != 1)
                    {
                        Log_Fatal("优惠券使用失败" + order.OrderNumber + "_" + userCouponID);
                        return new ReturnEntity<bool>(-1, "优惠券使用失败" + order.OrderNumber + "_" + userCouponID);
                    }

                    //已由外部传入 2014-8-11
                    //else
                    //{
                    //    //计算支付金额
                    //    iPayMoney = coupon.FaceValue;
                    //}
                }

                #endregion
            }
            if (iPayType == PayMoneyType.Balance)
            {
                #region 余额支付
                var useRtn = orderDAL.User_Amount_Use(order.UserID, iPayMoney, "支付订单:" + iOrderNumber, iOrderNumber);
                if (useRtn != 1)
                {
                    switch (useRtn)
                    {
                        case -1:
                            return new ReturnEntity<bool>(-4, "用户不存在");
                        case -2:
                            return new ReturnEntity<bool>(-4, "余额不足");
                        default:
                            return new ReturnEntity<bool>(-4, "用户状态错误");
                    }
                }
                #endregion
            }
            else if (iPayType == PayMoneyType.LazyCard)
            {
                #region 懒人卡支付
                var userCardID = 0;
                if (!int.TryParse(iPayRelationID, out userCardID))
                {
                    return new ReturnEntity<bool>(-4, "用户卡ID错误" + iPayRelationID);
                }

                var useRtn = orderDAL.User_Card_Use(order.UserID, userCardID, iPayMoney, "支付订单:" + iOrderNumber, iOrderNumber);
                if (useRtn != 1)
                {
                    switch (useRtn)
                    {
                        case -1:
                            return new ReturnEntity<bool>(-4, "用户卡不存在");
                        case -2:
                            return new ReturnEntity<bool>(-4, "卡余额不足");
                        default:
                            return new ReturnEntity<bool>(-4, "卡状态错误");
                    }
                }
                #endregion
            }

            //添加支付信息
            orderDAL.Order_Payment_ADD(new Order_PaymentDC()
            {
                OID = order.ID,
                PayMoney = iPayMoney,
                PayMoneyType = (int)iPayType,
                PayChannel = (int)iPayChannel,
                PayDate = iPayDate,
                RelationID = iPayRelationID,
            });

            //已完成的订单,更新物流金额
            if (order.OrderStatus == (int)OrderStatus.Finish)
            {
                //更新物流收费金额
                Internal_ExpressOrderChargeFee(order.OrderNumber
                    , order.TotalAmount - order.PayAmount - iPayMoney);
            }
            else
            {
                //已完成支付
                if (order.PayAmount + iPayMoney == order.TotalAmount)
                {
                    return Order_Order_Finish(iOrderNumber);
                }
            }

            //计算金额,如果已经付完,完成订单
            //if ((order.OrderClass == (int)OrderClass.Normal
            //    || order.OrderClass == (int)OrderClass.OneKey
            //    || order.OrderClass == (int)OrderClass.Luxury
            //    || order.OrderClass == (int)OrderClass.Mall)
            //    && order.PayAmount + iPayMoney >= order.TotalAmount)
            //{
            //    orderDAL.Order_Order_UPDATE_PayStatus(order.ID, PayStatus.Paid);

            //    return Order_Order_Finish(iOrderNumber);
            //}

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 完成订单(普通个人用户)
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_Finish(string iOrderNumber)
        {
            //获取订单
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderNumber, true, false, true, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            ////订单金额错误
            //if (order.PayAmount < order.TotalAmount)
            //{
            //    return new ReturnEntity<bool>(-2, "订单金额错误");
            //}

            //if (!order.ExpectTime.HasValue)
            //{
            //    order.ExpectTime = DateTime.Now;

            //    //if (DateTime.Now.Hour >= 12)
            //    //{
            //    //    //12点以后 明天取件
            //    //    order.ExpectTime = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(1);
            //    //}
            //    //else
            //    //{
            //    //    order.ExpectTime = DateTime.Today.AddHours(15).AddMinutes(1);
            //    //}
            //}

            if (!order.ExpectTime.HasValue)
            {
                order.ExpectTime = DateTime.Now;
            }

            //更新订单状态
            if (!orderDAL.Order_Order_UPDATE_OrderStatus(order.ID, OrderStatus.Finish, order.ExpectTime))
            {
                return new ReturnEntity<bool>(-2, "更新订单状态失败");
            }
            //普通干洗
            if (order.OrderClass == (int)OrderClass.Normal || order.OrderClass == (int)OrderClass.OneKey)
            {
                //推送快递,不管返回值
                Order_Order_Finish_Express(order);
            }
            //奢侈品
            else if (order.OrderClass == (int)OrderClass.Luxury)
            {
                //快递推送
            }
            //商城
            else if (order.OrderClass == (int)OrderClass.Mall)
            {
                var rtn = Order_Order_FinishMallProduct(order.OrderNumber, order.ProductList);
                if (rtn == null || !rtn.Success || !rtn.OBJ)
                {
                    return rtn;
                }
            }
            else
            {

            }

            //移除邀请码
            OrderClient.Cache_Remove("USER_INVITECODE_" + order.UserID);

            Order_Step_GetPackage(order);

            orderDAL.Order_Finish_SendCoupon(order.ID);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 重新推送快递
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_InternalExpressOrder_ReCreate(string iOrderNumber)
        {
            //获取订单
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderNumber, true, false, true, false, false, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }

            return Order_Order_Finish_Express(order);

            //if (order.OrderStatus != (int)OrderStatus.Finish)
            //{
            //    return new ReturnEntity<bool>(-2, "订单未完成");
            //}
            //if (order.OrderClass != (int)OrderClass.Normal && order.OrderClass != (int)OrderClass.OneKey)
            //{
            //    return new ReturnEntity<bool>(-2, "订单类型无需推送快递");
            //}
            //if (order.ExpressStatus != 0)
            //{
            //    return new ReturnEntity<bool>(-2, "订单已推送快递");
            //}

            //string packageInfo = string.Empty;
            //if (order.OrderClass == (int)OrderClass.Normal && order.ProductList != null && order.ProductList.Count > 0)
            //{
            //    packageInfo = string.Join(" ", order.ProductList.Select(p => p.Name).ToArray());
            //}
            //else
            //{
            //    packageInfo = order.Title;
            //}

            //if (!order.ExpectTime.HasValue)
            //{
            //    order.ExpectTime = DateTime.Now;
            //}

            ////使用内部物流系统---------2014-09-04 edit guominjie
            //var expOrder = Internal_CreateExpressOrder(order.OrderNumber, order.GetAddress, order.SendAddress,
            //    order.ExpectTime.Value,
            //    packageInfo, order.ProductList.Count, 0, order.InviteCode
            //    );

            //if (expOrder != null && expOrder.Success && expOrder.OBJ)
            //{
            //    Order_Order_FinishExpress(order.ID);
            //}
            //else
            //{
            //    Log_Fatal("内部订单推送失败,订单:" + order.OrderNumber);
            //    return new ReturnEntity<bool>(-1, "内部订单推送失败");
            //}
            //return new ReturnEntity<bool>(true);
        }

        public ReturnEntity<bool> Order_Order_Finish_Express(Order_OrderDC iOrder)
        {
            if (iOrder == null)
            {
                return new ReturnEntity<bool>(-1, "订单号错误");
            }
            if (iOrder.OrderStatus != (int)OrderStatus.Finish && iOrder.OrderStatus != (int)OrderStatus.Create)
            {
                return new ReturnEntity<bool>(-2, "订单状态错误");
            }
            if (iOrder.OrderClass != (int)OrderClass.Normal && iOrder.OrderClass != (int)OrderClass.OneKey)
            {
                return new ReturnEntity<bool>(-2, "订单类型无需推送快递");
            }
            if (iOrder.ExpressStatus != 0)
            {
                return new ReturnEntity<bool>(-2, "订单已推送快递");
            }

            string packageInfo = string.Empty;
            if (iOrder.OrderClass == (int)OrderClass.Normal && iOrder.ProductList != null && iOrder.ProductList.Count > 0)
            {
                packageInfo = string.Join(" ", iOrder.ProductList.Select(p => p.Name).ToArray());
            }
            else
            {
                packageInfo = iOrder.Title;
            }

            //预约时间是半小时后,不再强推物流系统指定快递员
            if ((iOrder.ExpectTime.Value - DateTime.Now).TotalMinutes > 30)
            {
                iOrder.InviteCode = null;
            }

            //使用内部物流系统---------2014-09-04 edit guominjie
            var expOrder = Internal_CreateExpressOrder(iOrder.OrderNumber, iOrder.GetAddress, iOrder.SendAddress,
                iOrder.ExpectTime.Value,
                packageInfo, iOrder.ProductList.Count, iOrder.TotalAmount - iOrder.PayAmount, iOrder.InviteCode);

            if (expOrder != null && expOrder.Success && expOrder.OBJ)
            {
                Order_Order_FinishExpress(iOrder.ID);
            }
            else
            {
                Log_Fatal("内部订单推送失败,订单:" + iOrder.OrderNumber);
                return new ReturnEntity<bool>(-1, "内部订单推送失败");
            }

            return new ReturnEntity<bool>(true);

        }

        #region 查询订单

        /// <summary>
        /// 根据订单ID查订单
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity(int iID,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep, bool iGetFeedback = false)
        {
            return new ReturnEntity<Order_OrderDC>(orderDAL.Order_Order_SELECT_Entity(iID,
                iGetProduct, iGetAmount, iGetConsigneeAddress,
                iGetExpress, iGetPayment, iGetStep, iGetFeedback));
        }

        /// <summary>
        /// 根据订单号查订单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity(string iOrderNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            return new ReturnEntity<Order_OrderDC>(orderDAL.Order_Order_SELECT_Entity(iOrderNumber,
             iGetProduct, iGetAmount, iGetConsigneeAddress,
             iGetExpress, iGetPayment, iGetStep));
        }

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
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_Express(string iExpressNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            return new ReturnEntity<Order_OrderDC>(orderDAL.Order_Order_SELECT_Entity_Express(iExpressNumber,
             iGetProduct, iGetAmount, iGetConsigneeAddress,
             iGetExpress, iGetPayment, iGetStep));
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
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_FactoryCode(string iCodeNumber,
             bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
             bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            return new ReturnEntity<Order_OrderDC>(orderDAL.Order_Order_SELECT_Entity_FactoryCode(iCodeNumber,
                iGetProduct, iGetAmount, iGetConsigneeAddress,
                iGetExpress, iGetPayment, iGetStep));
        }

        /// <summary>
        /// 订单列表(微信)
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_Order_SELECT_List(
            string iOpenid, OrderStatus? iOrderStatus,
            int iPageIndex, int iPageSize)
        {
            var userID = orderDAL.User_Weixin_SELECT_UserID(iOpenid, OAuthType.Weixin);
            if (!userID.HasValue)
            {
                return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(-1, "用户未绑定");
            }

            var rtn = orderDAL.wx_Order_Order_SELECT_List(userID.Value, iOrderStatus, iPageIndex, iPageSize);
            if (rtn != null && rtn.ReturnList != null)
            {
                foreach (var item in rtn.ReturnList)
                {
                    item.ProductList = orderDAL.Order_Product_SELECT_List(item.ID, item.OrderClass);
                }
            }
            return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(rtn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_OrderDC>> app_Order_Order_SELECT_List(
         Guid iUserID, OrderStatus? iOrderStatus,
         int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.app_Order_Order_SELECT_List(iUserID, iOrderStatus, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(rtn);
        }

        #endregion

        /// <summary>
        /// 新增 Order_Feedback
        /// </summary>
        /// <param name="iOrder_FeedbackDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Order_Feedback_ADD(Order_FeedbackDC iOrder_FeedbackDC)
        {
            var rtn = orderDAL.Order_Feedback_ADD(iOrder_FeedbackDC);

            return new ReturnEntity<int>(rtn);
        }

    }
}
