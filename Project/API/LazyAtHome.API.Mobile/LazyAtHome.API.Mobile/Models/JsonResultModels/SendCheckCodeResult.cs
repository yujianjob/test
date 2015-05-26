using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.JsonResultModels
{
    public class SendCheckCodeResult : BaseResult
    {
        public string checkcode { get; set; }
    }
}