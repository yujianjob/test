using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyAtHome.Core.Web.Base
{
    public class SearchBase
    {
        public SearchBase()
        {
            RawUrl = Helper.HttpContextHelper.RequestRawUrl;
        }
        public string RawUrl { get; set; }
    }
}
