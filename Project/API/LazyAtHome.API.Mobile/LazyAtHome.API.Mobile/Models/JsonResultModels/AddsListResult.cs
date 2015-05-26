using LazyAtHome.API.Mobile.Models.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.JsonResultModels
{
    public class AddsListResult : BaseResult
    {
        public IList<UserAddsModel> addresslist { get; set; }
    }
}