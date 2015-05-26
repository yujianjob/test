﻿using LazyAtHome.API.Mobile.Models.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.JsonResultModels
{
    public class ServiceListResult : BaseResult
    {
        public IList<WashServiceModel> service { get; set; }
    }
}