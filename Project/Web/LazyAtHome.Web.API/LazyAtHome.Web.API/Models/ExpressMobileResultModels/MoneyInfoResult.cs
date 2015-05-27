using LazyAtHome.Web.API.Models.JsonResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.Models.ExpressMobileResultModels
{
    public class MoneyInfoResult : BaseResult
    {
        /// <summary>
        /// 佣金
        /// </summary>
        public string brokerage;

        /// <summary>
        /// 货款
        /// </summary>
        public string payment;
    }
}