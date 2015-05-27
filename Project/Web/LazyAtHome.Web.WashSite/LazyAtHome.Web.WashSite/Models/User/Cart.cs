using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WashSite.Models.User
{
    public class Cart
    {
        IList<CartProductItem> _ProductList = new List<CartProductItem>();
        Dictionary<int, decimal> _UseCard = new Dictionary<int, decimal>();
        Dictionary<int, decimal> _UseCoupon = new Dictionary<int, decimal>();

        /// <summary>
        /// 产品列表
        /// </summary>
        public IList<CartProductItem> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; }
        }

        /// <summary>
        /// 总计金额
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 总计数量
        /// </summary>
        public int TotalCount { get; set; }

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
        public bool ExpressEnable = false;

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
            get
            {                
                if (!ExpressEnable)
                {
                    if (TotalPrice < 25)
                        _PayPrice += 8;
                    ExpressEnable = true;
                }
                return _PayPrice;
            }
            set { _PayPrice = value; }
        }

        public void Clear(bool clearProduct = true)
        {
            if (clearProduct)
            {
                ProductList.Clear();
                TotalPrice = 0;
                TotalCount = 0;
            }
            UseBalanceEnable = false;
            UseBalance = 0;
            UseCardPrice = 0;
            UseCard.Clear();
            PayPrice = 0;
            SalePrice = 0;
            UseCoupon.Clear();
            UseCouponPrice = 0;
            CharterSale = 0;

        }
    }
}