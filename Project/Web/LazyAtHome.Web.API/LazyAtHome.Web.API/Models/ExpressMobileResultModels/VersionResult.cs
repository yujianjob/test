using LazyAtHome.Web.API.Models.JsonResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.Models.ExpressMobileResultModels
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionResult : BaseResult
    {
        public string version { get; set; }
        public string minversion { get; set; }
        public int versioncode { get; set; }
        public string url { get; set; }
    }
}