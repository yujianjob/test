using LazyAtHome.API.Mobile.Models.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.JsonResultModels
{
    public class OrderCreateCalcResult : BaseResult
    {
        public UserAddsModel address { get; set; }

        public int money { get; set; }

        public IList<UserCardModel> card { get; set; }

        public IList<UserCouponModel> coupon { get; set; }

        public IList<WashProductModel> productlist { get; set; }

        public IList<OrderAmountModel> amountlist { get; set; }

        public int amount { get; set; }
    }
}