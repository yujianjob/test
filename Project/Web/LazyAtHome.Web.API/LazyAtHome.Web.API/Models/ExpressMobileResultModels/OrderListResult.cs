﻿using LazyAtHome.Web.API.Models.JsonResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.Models.ExpressMobileResultModels
{
    public class OrderListResult : BaseResult
    {
        public IList<OrderModel> list;
    }
}