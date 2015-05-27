using LazyAtHome.Web.API.Models.JsonResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.Models.ExpressMobileResultModels
{
    public class BrokerageListResult : BaseResult
    {
        public IList<BrokerageModel> list;

        public class BrokerageModel
        {
            public int id;
            public string name;
            public string money;
            public string time;

        }
    }
}