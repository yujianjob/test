using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin3.Models
{
    public class RHModel
    {
        public Dictionary<int, int> ClassAList = new Dictionary<int, int>();
        public int ClassB = 0;

        public List<RHProductModel> ProductList = new List<RHProductModel>();

        public int TotalCount = 0;
        public decimal TotalPrice = 0;

        public string OpenID = "";
        public string CouponCode = "";
        public string Address = "";
        public int AreaID = 0;
        public string NickName = "";
        public string MPNo = "";
    }

    public class RHProductModel
    {
        public LazyAtHome.WCF.Wash.Contract.DataContract.web.web_Wash_ProductDC Product { get; set; }
        public int Count { get; set; }
        public int Type { get; set; }
    }
}