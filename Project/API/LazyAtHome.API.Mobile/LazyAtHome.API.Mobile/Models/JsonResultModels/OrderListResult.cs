﻿using LazyAtHome.API.Mobile.Models.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.JsonResultModels
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderListResult : BaseResult
    {
        public IList<OrderModel> orderlist;
    }
}