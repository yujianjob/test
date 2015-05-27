using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin3.Models
{
    public class CartModel
    {
        IList<CartProductItemModel> _ProductList = new List<CartProductItemModel>();
        Dictionary<int, decimal> _UseCard = new Dictionary<int, decimal>();
        Dictionary<int, decimal> _UseCoupon = new Dictionary<int, decimal>();

        /// <summary>
        /// 产品列表
        /// </summary>
        public IList<CartProductItemModel> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; }
        }

        /// <summary>
        /// 总计金额
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 非正价产品总价
        /// </summary>
        public decimal TotalMinPrice { get; set; }

        /// <summary>
        /// 总计数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 非正价产品最高金额
        /// </summary>
        public decimal MaxFreeMoney { get; set; }

        /// <summary>
        /// 启用余额支付
        /// </summary>
        public bool UseBalanceEnable { get; set; }
        /// <summary>
        /// 使用余额支付
        /// </summary>
        public decimal UseBalance { get; set; }

        /// <summary>
        /// 使用的懒人卡列表
        /// </summary>
        public Dictionary<int, decimal> UseCard
        {
            get { return _UseCard; }
            set { _UseCard = value; }
        }

        /// <summary>
        /// 懒人卡抵扣金额
        /// </summary>
        public decimal UseCardPrice { get; set; }

        /// <summary>
        /// 使用优惠券列表
        /// </summary>
        public Dictionary<int, decimal> UseCoupon
        {
            get { return _UseCoupon; }
            set { _UseCoupon = value; }
        }

        /// <summary>
        /// 优惠券抵扣金额
        /// </summary>
        public decimal UseCouponPrice { get; set; }

        private decimal _PayPrice = 0;
        public bool ExpressEnable = true;

        /// <summary>
        /// 运费
        /// </summary>
        public decimal ExpressMoney { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创始会员优惠
        /// </summary>
        public decimal CharterSale { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal PayPrice
        {
            get { return _PayPrice; }
            set { _PayPrice = value; }
        }

        public decimal NomalPrice { get; set; }

        public int UserLevel { get; set; }

        public void Clear(bool clearProduct = true)
        {
            if (clearProduct)
            {
                ProductList.Clear();
                TotalPrice = 0;
                TotalCount = 0;
                MaxFreeMoney = 0;
                ExpressMoney = 0;
                NomalPrice = 0;
                PayPrice = 0;
                SalePrice = 0;
                CharterSale = 0;
                TotalMinPrice = 0;
            }
            UseBalanceEnable = false;
            UseBalance = 0;
            UseCardPrice = 0;
            UseCard.Clear();            
            UseCoupon.Clear();
            UseCouponPrice = 0;
        }

        public void TotalProductInfo()
        {
            TotalCount = 0;
            TotalPrice = 0;
            MaxFreeMoney = 0;
            ExpressMoney = 0;
            NomalPrice = 0;
            SalePrice = 0;
            TotalMinPrice=0;

            foreach (var item in _ProductList)
            {
                TotalCount += item.Count;
                TotalPrice += item.ProductInfo.MarketPrice * item.Count;

                decimal tmpPrice = item.ProductInfo.SalesPrice;
                
                //if (UserLevel > 0 && tmpPrice == 9.9m)
                //    tmpPrice = 6;

                NomalPrice += tmpPrice * item.Count;

                if (tmpPrice == 9.9m)
                {
                    TotalMinPrice += tmpPrice * item.Count;
                    if (tmpPrice > MaxFreeMoney)
                        MaxFreeMoney = tmpPrice;
                }
            }

            if (TotalCount == 0)
                PayPrice = 0;
            else
            {
                SalePrice = TotalPrice - NomalPrice;
                if (ExpressEnable && NomalPrice < 18)
                    ExpressMoney = 18 - NomalPrice;
                PayPrice = NomalPrice + ExpressMoney;
            }
        }
    }
}