﻿using LazyAtHome.Core.Infrastructure.WCF;
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
        #region 普通订单

        ///// <summary>
        ///// 微信普通订单
        ///// </summary>
        ///// <param name="iOpenid"></param>
        ///// <param name="iSite"></param>
        ///// <param name="iProductIDList"></param>
        ///// <param name="iExpressFee"></param>
        ///// <param name="iExpressFeeDiscount"></param>
        ///// <param name="iUserMoney"></param>
        ///// <param name="iLazyCardList"></param>
        ///// <param name="iGetAddress"></param>
        ///// <param name="iSendAddress"></param>
        ///// <returns></returns>
        //public ReturnEntity<Order_OrderDC> Order_Weixin_Create(string iOpenid, int iSite,
        //    IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
        //    decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
        //    Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
        //    decimal iRealAmount, string iActivityName = null, decimal? iActivityMoney = null,
        //    IList<int> iCouponIDList = null)
        //{
        //    var user = orderDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
        //    if (user == null)
        //    {
        //        return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
        //    }

        //    IDictionary<string, decimal> activityList = new Dictionary<string, decimal>();
        //    if (iActivityMoney.HasValue)
        //    {
        //        activityList.Add(iActivityName, iActivityMoney.Value);
        //    }
        //    return Order_Create(user.ID, iSite, Channel.Weixin,
        //        iProductIDList, iExpressFee, iExpressFeeDiscount,
        //        iUserMoney, iLazyCardList,
        //        iGetAddress, iSendAddress,
        //        iRealAmount, activityList, iCouponIDList);

        //    //if (iGetAddress == null)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-1, "取货地址错误");
        //    //}
        //    //if (iSendAddress == null)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-1, "送货地址错误");
        //    //}
        //    //if (iProductIDList == null || iProductIDList.Count == 0)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-1, "未选择产品");
        //    //}

        //    //var user = orderDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
        //    //if (user == null)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
        //    //}
        //    //if (user.AccountStatus != 1)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-1, "用户已冻结");
        //    //}
        //    //if (user.UserStatus != 1)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-1, "用户已锁定");
        //    //}
        //    //if (user.Money < iUserMoney)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-1, "用户余额不足");
        //    //}
        //    //if (iLazyCardList != null)
        //    //{
        //    //    foreach (var item in iLazyCardList)
        //    //    {
        //    //        if (!orderDAL.User_Card_CheckBalance(user.ID, item.Key, item.Value))
        //    //        {
        //    //            return new ReturnEntity<Order_OrderDC>(-1, "懒人卡余额不足");
        //    //        }
        //    //    }
        //    //}

        //    //var order = new Order_OrderDC()
        //    //{
        //    //    OrderClass = (int)OrderClass.Normal,
        //    //    OrderType = (int)OrderType.Normal,
        //    //    UserID = user.ID,
        //    //    OrderStatus = (int)OrderStatus.Create,
        //    //    Site = iSite,
        //    //    Channel = (int)Channel.Weixin,

        //    //    Title = "",
        //    //    TotalAmount = 0,
        //    //    PayAmount = 0,
        //    //};

        //    //var ProductList = new List<Order_ProductDC>();
        //    //foreach (var item in iProductIDList)
        //    //{
        //    //    var entity = orderDAL.Wash_Product_SELECT_Entity(item);
        //    //    if (entity == null)
        //    //    {
        //    //        return new ReturnEntity<Order_OrderDC>(-1, "无效产品");
        //    //    }
        //    //    else
        //    //    {
        //    //        ProductList.Add(new Order_ProductDC()
        //    //        {
        //    //            ProductID = entity.ID,
        //    //            Name = entity.Name,
        //    //            Type = 1,
        //    //            Price = entity.SalesPrice,
        //    //            CategoryName = entity.CategoryName,
        //    //            WebName = entity.WebName,
        //    //        });
        //    //    }
        //    //}

        //    //order.ProductList = ProductList;
        //    //order.Title = ProductList.Count == 1
        //    //        ? ProductList.First().CategoryName : ProductList.First().CategoryName + " 等(" + ProductList.Count + ")";

        //    //iGetAddress.Type = (int)ConsigneeAddressType.Get;
        //    //order.GetAddress = iGetAddress;
        //    //iSendAddress.Type = (int)ConsigneeAddressType.Send;
        //    //order.SendAddress = iSendAddress;

        //    //order.AmountList = new List<Order_AmountDC>();

        //    //#region 快递费
        //    //if (iExpressFee != 0)
        //    //{
        //    //    order.AmountList.Add(new Order_AmountDC()
        //    //    {
        //    //        Money = Math.Abs(iExpressFee),
        //    //        Type = (int)AmountType.ExpressFee,
        //    //        Content = "快递费",
        //    //    });

        //    //    if (iExpressFeeDiscount != 0)
        //    //    {
        //    //        order.AmountList.Add(new Order_AmountDC()
        //    //        {
        //    //            Money = -Math.Abs(iExpressFeeDiscount),
        //    //            Type = (int)AmountType.ExpressFeeReduce,
        //    //            Content = "快递费抵扣",
        //    //        });
        //    //    }
        //    //}
        //    //#endregion

        //    //#region 活动
        //    //if (iActivityMoney.HasValue)
        //    //{
        //    //    order.AmountList.Add(new Order_AmountDC()
        //    //    {
        //    //        Money = -Math.Abs(iActivityMoney.Value),
        //    //        Type = (int)AmountType.Activity,
        //    //        Content = iActivityName,
        //    //    });
        //    //}
        //    //#endregion

        //    //order.TotalAmount = order.ProductList.Sum(p => p.Price);
        //    //order.TotalAmount += order.AmountList.Sum(p => p.Money);

        //    //if (TotalAmount != order.TotalAmount)
        //    //{
        //    //    return new ReturnEntity<Order_OrderDC>(-2, "总金额错误");
        //    //}


        //    //var orderid = orderDAL.Order_Order_ADD(order);
        //    //order = orderDAL.Order_Order_SELECT_Entity(orderid, false, false, false, false, false, false);

        //    //if (iUserMoney > 0)
        //    //{
        //    //    var payRtn = Order_Order_Pay(order.OrderNumber, iUserMoney, PayMoneyType.Balance, PayMoneyChannel.Weixin, DateTime.Now, null);
        //    //    if (!payRtn.Success || payRtn.OBJ == false)
        //    //    {
        //    //        return new ReturnEntity<Order_OrderDC>(-1, payRtn.Message);
        //    //    }
        //    //}

        //    //if (iLazyCardList != null)
        //    //{
        //    //    foreach (var item in iLazyCardList)
        //    //    {
        //    //        var payRtn = Order_Order_Pay(order.OrderNumber, item.Value, PayMoneyType.LazyCard, PayMoneyChannel.Weixin, DateTime.Now, item.Key.ToString());
        //    //        if (!payRtn.Success || payRtn.OBJ == false)
        //    //        {
        //    //            Log_Fatal("用户卡支付失败" + item.Key + payRtn.Message);
        //    //            return new ReturnEntity<Order_OrderDC>(-1, "用户卡支付失败" + item.Key);
        //    //        }
        //    //    }
        //    //}

        //    //return new ReturnEntity<Order_OrderDC>(order);
        //}

        /// <summary>
        /// 微信普通订单
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iSite"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="iActivityList"></param>
        /// <param name="iCouponIDList"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Weixin_Create(string iOpenid, int iSite,
            IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
            decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            decimal iRealAmount, IDictionary<string, decimal> iActivityList,
            IList<int> iCouponIDList = null, bool iSalesPrice = true, string iUserRemark = null,
            string iCouponCode = null, Channel iChannel = Channel.Weixin, DateTime? iExpectTime = null
            , int? iSendType = 1, string iInviteCode = null)
        {
            var user = orderDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            if (user == null)
            {
                return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
            }

            int? iUserCouponID = null;

            if (iCouponIDList != null && iCouponIDList.Count > 0)
            {
                iUserCouponID = iCouponIDList.First();
            }

            //若海无时间,设置时间为当前
            if (iChannel == Channel.Partner_Ruohai && !iExpectTime.HasValue)
            {
                iExpectTime = DateTime.Today.AddHours(DateTime.Now.Hour).AddHours(4);
                //10点前设置为10点
                if (iExpectTime.Value.Hour < 10)
                {
                    iExpectTime = iExpectTime.Value.AddHours(10 - iExpectTime.Value.Hour);
                }
            }

            return Order_Create_0929(user.ID, iSite, iChannel,
                iProductIDList, iExpressFee, iExpressFeeDiscount,
                iUserMoney, iLazyCardList,
                iGetAddress, iSendAddress,
                iRealAmount, iActivityList,
                iUserCouponID, iUserRemark
                , iSalesPrice, iCouponCode, iExpectTime
                , iSendType, iInviteCode);
        }

        #endregion

        #region 一键下单

        /// <summary>
        /// 一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="ExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Onekey_Create(string iOpenid, int iSite, Channel iChannel,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, DateTime? iExpectTime,
            string iUserRemark, int? iUserCouponID = null, string iCouponCode = null,
            int? iSendType = 1, string iInviteCode = null, int iUseMoney = 0)
        {
            var user = orderDAL.User_Info_SELECT_Entity(iOpenid, OAuthType.Weixin);
            if (user == null)
            {
                return new ReturnEntity<Order_OrderDC>(-2, "用户不存在");
            }

            return Order_Create_Onekey_0929(user.ID, iSite, Channel.Weixin, iGetAddress, iSendAddress,
                iExpectTime, iUserRemark, iUserCouponID, iCouponCode, iSendType, iInviteCode, iUseMoney);
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
            //    Channel = (int)iChannel,
            //};

            //iGetAddress.Type = (int)ConsigneeAddressType.Get;
            //order.GetAddress = iGetAddress;
            //iSendAddress.Type = (int)ConsigneeAddressType.Send;
            //order.SendAddress = iSendAddress;

            ////order.GetAddress = new Order_ConsigneeAddressDC()
            ////{
            ////    Type = 1,
            ////    Consignee = iGetAddress.Consignee,
            ////    DistrictID = iGetAddress.DistrictID,
            ////    Address = iGetAddress.Address,
            ////    Email = iGetAddress.Email,
            ////    Mpno = iGetAddress.Mpno,
            ////    Phone = iGetAddress.Phone,
            ////};

            ////order.SendAddress = new Order_ConsigneeAddressDC()
            ////{
            ////    Type = 2,
            ////    Consignee = iSendAddress.Consignee,
            ////    DistrictID = iSendAddress.DistrictID,
            ////    Address = iSendAddress.Address,
            ////    Email = iSendAddress.Email,
            ////    Mpno = iSendAddress.Mpno,
            ////    Phone = iSendAddress.Phone,
            ////};

            //var orderID = orderDAL.Order_Order_ADD(order);


            //order = orderDAL.Order_Order_SELECT_Entity(orderID, false, false, true, false, false, false);


            //return new ReturnEntity<Order_OrderDC>(order);
        }

        #endregion

        #region 微信支付回调通知

        /// <summary>
        /// 微信支付回调通知
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_WeiXinPay_Notice(string iOrderNumber)
        {
            var order = orderDAL.Order_Order_SELECT_Entity(iOrderNumber, false, false, false, false, true, false);
            if (order == null)
            {
                return new ReturnEntity<bool>(false);
            }
            var dt = orderDAL.Order_PayRecord_Select(iOrderNumber, 10, null);
            if (dt.Rows.Count > 0)
            {
                if (!orderDAL.Order_PayRecord_Update(iOrderNumber, 10, 1))
                {
                    return new ReturnEntity<bool>(false);
                }
            }

            dt = orderDAL.Order_PayRecord_Select(iOrderNumber, null, 1);

            if (dt.Rows.Count > 0)
            {
                decimal sum = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    sum += Convert.ToDecimal(dr["payAmount"]);
                }
                if (sum >= order.TotalAmount)
                {
                    if (!orderDAL.Order_Order_UPDATE_PayStatus(order.ID,PayStatus.Paid)
                        //&& !orderDAL.Order_PayAmount_Update(order.ID, sum)
                        )
                    {
                        return new ReturnEntity<bool>(false);
                    }
                }
            }
            return new ReturnEntity<bool>(true);
        }

        #endregion

    }
}