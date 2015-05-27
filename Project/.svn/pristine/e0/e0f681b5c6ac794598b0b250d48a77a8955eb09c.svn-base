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
        #region 干洗订单

        /// <summary>
        /// 网站下单
        /// </summary>
        public ReturnEntity<Order_OrderDC> Order_Web_Create(Guid iUserID, int iSite,
           IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
           decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
           Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
           decimal iRealAmount, IDictionary<string, decimal> iActivityList,
           IList<int> iCouponIDList = null, bool iSalesPrice = true, string iCouponCode = null,
            string iUserRemark = null, int? iSendType = 1, string iInviteCode = null)
        {
            int? iUserCouponID = null;

            if (iCouponIDList != null && iCouponIDList.Count > 0)
            {
                iUserCouponID = iCouponIDList.First();
            }

            return Order_Create_0929(iUserID, iSite, Channel.Web,
                  iProductIDList, iExpressFee, iExpressFeeDiscount,
                  iUserMoney, iLazyCardList, iGetAddress, iSendAddress,
                  iRealAmount, iActivityList, iUserCouponID, iUserRemark
                  , iSalesPrice, iCouponCode, null, iSendType, iInviteCode);
        }

        /// <summary>
        /// 网站下单(活动)
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="iActivityName"></param>
        /// <param name="ActivityMoney"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Web_Create_Activity(Guid iUserID, int iSite,
            IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
            decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            decimal iRealAmount, string iActivityName, decimal iActivityMoney)
        {
            if (iGetAddress == null)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "取货地址错误");
            }
            if (iSendAddress == null)
            {
                return new ReturnEntity<Order_OrderDC>(-1, "送货地址错误");
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



            if (iLazyCardList != null)
            {
                //过滤使用0元的卡
                iLazyCardList = iLazyCardList.Where(p => p.Value != 0).ToDictionary(p => p.Key, p => p.Value);

                foreach (var item in iLazyCardList)
                {
                    if (!orderDAL.User_Card_CheckBalance(user.ID, item.Key, item.Value))
                    {
                        Log_Info("懒人卡余额不足" + user.ID + " " + item.Key + " " + item.Value);
                        return new ReturnEntity<Order_OrderDC>(-1, "懒人卡余额不足");
                    }
                }
            }

            var order = new Order_OrderDC()
            {
                OrderClass = (int)OrderClass.Normal,
                OrderType = (int)OrderType.Normal,
                UserID = user.ID,
                OrderStatus = (int)OrderStatus.Create,
                Site = iSite,
                Channel = (int)Channel.Web,

                Title = "",
                TotalAmount = 0,
                PayAmount = 0,
            };
            var ProductList = new List<Order_ProductDC>();
            foreach (var item in iProductIDList)
            {
                var entity = orderDAL.Wash_Product_SELECT_Entity(item);
                if (entity == null)
                {
                    return new ReturnEntity<Order_OrderDC>(-1, "无效产品");
                }
                else
                {
                    ProductList.Add(new Order_ProductDC()
                    {
                        ProductID = entity.ID,
                        Name = entity.Name,
                        Type = 1,
                        Price = entity.MarketPrice,
                        CategoryName = entity.CategoryName,
                        WebName = entity.WebName,
                    });
                }
            }

            order.ProductList = ProductList;
            order.Title = iActivityName;

            iGetAddress.Type = (int)ConsigneeAddressType.Get;
            order.GetAddress = iGetAddress;
            iSendAddress.Type = (int)ConsigneeAddressType.Send;
            order.SendAddress = iSendAddress;

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

                if (iExpressFeeDiscount != 0)
                {
                    order.AmountList.Add(new Order_AmountDC()
                    {
                        Money = -Math.Abs(iExpressFeeDiscount),
                        Type = (int)AmountType.ExpressFeeReduce,
                        Content = "快递费抵扣",
                    });
                }
            }
            #endregion

            #region 活动
            if (iActivityMoney > 0)
            {
                order.AmountList.Add(new Order_AmountDC()
                {
                    Money = -Math.Abs(iActivityMoney),
                    Type = (int)AmountType.Activity,
                    Content = iActivityName,
                });
            }
            #endregion

            order.TotalAmount = order.ProductList.Sum(p => p.Price);
            order.TotalAmount += order.AmountList.Sum(p => p.Money);

            if (iRealAmount != order.TotalAmount)
            {
                return new ReturnEntity<Order_OrderDC>(-2, "总金额错误");
            }

            var orderid = orderDAL.Order_Order_ADD(order);

            order = orderDAL.Order_Order_SELECT_Entity(orderid, false, false, false, false, false, false);

            if (iUserMoney > 0)
            {
                var payRtn = Order_Order_Pay(order.OrderNumber, iUserMoney, PayMoneyType.Balance, PayMoneyChannel.Weixin, DateTime.Now, null);
                if (!payRtn.Success || payRtn.OBJ == false)
                {
                    return new ReturnEntity<Order_OrderDC>(-1, payRtn.Message);
                }
            }

            if (iLazyCardList != null)
            {
                foreach (var item in iLazyCardList)
                {
                    var payRtn = Order_Order_Pay(order.OrderNumber, item.Value, PayMoneyType.LazyCard, PayMoneyChannel.Weixin, DateTime.Now, item.Key.ToString());
                    if (!payRtn.Success || payRtn.OBJ == false)
                    {
                        Log_Fatal("用户卡支付失败" + item.Key + payRtn.Message);
                        return new ReturnEntity<Order_OrderDC>(-1, "用户卡支付失败" + item.Key);
                    }
                }
            }

            return new ReturnEntity<Order_OrderDC>(order);
        }

        #endregion

        #region 商城产品下单

        /// <summary>
        /// 商城产品下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iProductList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="TotalAmount"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Mall_Web_Create(Guid iUserID, int iSite,
               IDictionary<int, int> iProductList, decimal iExpressFee, decimal iExpressFeeDiscount,
               Order_ConsigneeAddressDC iSendAddress,
               decimal iRealAmount)
        {
            //if (iSendAddress == null)
            //{
            //    return new ReturnEntity<Order_OrderDC>(-1, "送货地址错误");
            //}
            if (iProductList == null || iProductList.Count == 0)
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

            var order = new Order_OrderDC()
            {
                OrderClass = (int)OrderClass.Mall,
                OrderType = (int)OrderType.Normal,
                UserID = user.ID,
                OrderStatus = (int)OrderStatus.Create,
                Site = iSite,
                Channel = (int)Channel.Web,

                Title = "",
                TotalAmount = 0,
                PayAmount = 0,
            };

            var ProductList = new List<Order_ProductDC>();
            foreach (var item in iProductList)
            {
                var entity = orderDAL.Mall_Product_SELECT_Entity(item.Key);
                if (entity == null)
                {
                    return new ReturnEntity<Order_OrderDC>(-1, "无效产品");
                }
                else if (entity.LeftCount != -1 && entity.LeftCount < item.Value)
                {
                    return new ReturnEntity<Order_OrderDC>(-1, "产品剩余数量不足");
                }
                else
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        ProductList.Add(new Order_ProductDC()
                        {
                            ProductID = entity.ID,
                            Name = entity.Name,
                            Type = entity.Type,
                            Price = entity.SalesPrice,
                            Attribute = entity.Class + "_" + entity.TypeValue,
                        });
                    }
                    if (entity.LeftCount != -1)
                    {
                        orderDAL.Mall_Product_UPDATE_LeftCount(entity.ID, item.Value);
                    }
                }
            }

            order.ProductList = ProductList;
            order.Title = ProductList.Count == 1
                    ? ProductList.First().Name : ProductList.First().Name + " 等(" + ProductList.Count + ")";
            order.TotalAmount = order.ProductList.Sum(p => p.Price);
            if (iRealAmount != order.TotalAmount)
            {
                return new ReturnEntity<Order_OrderDC>(-2, "总金额错误");
            }

            if (iSendAddress != null)
            {
                iSendAddress.Type = (int)ConsigneeAddressType.Send;
                order.SendAddress = iSendAddress;
            }

            if (iExpressFee != 0)
            {
                order.AmountList = new List<Order_AmountDC>();

                order.AmountList.Add(new Order_AmountDC()
                {
                    Money = Math.Abs(iExpressFee),
                    Type = (int)AmountType.ExpressFee,
                    Content = "快递费",
                });

                if (iExpressFeeDiscount != 0)
                {
                    order.AmountList.Add(new Order_AmountDC()
                    {
                        Money = -Math.Abs(iExpressFeeDiscount),
                        Type = (int)AmountType.ExpressFeeReduce,
                        Content = "快递费抵扣",
                    });
                }
            }

            var orderid = orderDAL.Order_Order_ADD(order);


            order = orderDAL.Order_Order_SELECT_Entity(orderid, false, false, false, false, false, false);

            return new ReturnEntity<Order_OrderDC>(order);
        }

        #endregion

        #region 一键下单

        /// <summary>
        /// 一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Onekey_Create(Guid iUserID, int iSite, Channel iChannel,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, DateTime? iExpectTime,
            string iUserRemark, int? iUserCouponID = null, string iCouponCode = null,
            int? iSendType = 1, string iInviteCode = null, int iUseMoney = 0)
        {
            return Order_Create_Onekey_0929(iUserID, iSite, Channel.Web, iGetAddress, iSendAddress,
                iExpectTime, iUserRemark, iUserCouponID, iCouponCode, iSendType, iInviteCode, iUseMoney);
            //Order_OrderDC order = new Order_OrderDC()
            //{
            //    OrderClass = (int)OrderClass.OneKey,
            //    OrderType = (int)OrderType.Normal,
            //    UserID = iUserID,
            //    Title = "一键下单",
            //    TotalAmount = 0,
            //    PayAmount = 0,
            //    ExpectTime = iExpectTime,
            //    //0:一键下单(未审核)
            //    OrderStatus = 0,
            //    Site = iSite,
            //    Channel = (int)iChannel,
            //    UserRemark = iUserRemark,
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

        /// <summary>
        /// 订单列表(网站)
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_Order_SELECT_List(
            Guid iUserID, OrderStatus? iOrderStatus,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Order_Order_SELECT_List(iUserID, iOrderStatus, iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(rtn);
        }

    }
}
