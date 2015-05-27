using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WashSite.Controllers
{
    public class LazyArtifactController : BaseController
    {
        //
        // GET: /LazyArtifact/
        public ActionResult Index(Models.Home.ArtifactViewModel search)
        {
            IList<WCF.Wash.Contract.DataContract.web.web_Mall_ProductDC> xList = null;
            var rtn = App_Code.Proxy.WashProxy.Select_MallList(search.Type, search.Class, search.Keyword, search.TypeValue, AppConfig.SiteID, search.OrderType, 1, 20);
            if (rtn.Success)
            {
                xList = rtn.OBJ.ReturnList;
            }
            if (xList == null)
                xList = new List<WCF.Wash.Contract.DataContract.web.web_Mall_ProductDC>();

            ViewBag.List = xList;

            return View(search);
        }

        public ActionResult Show(int id=1)
        {
            var rtn = App_Code.Proxy.WashProxy.Select_MallEntity(id);
            return View(rtn.OBJ);
        }

        [Authorize]
        public ActionResult Trade(int id, int buycount = 1)
        {
            var user = GetUserInfo();

            var rtn = App_Code.Proxy.WashProxy.Select_MallEntity(id);
            decimal payMoney = rtn.OBJ.SalesPrice * buycount;

            Dictionary<int, int> pList = new Dictionary<int, int>();
            pList.Add(id, buycount);

            var orderRtn = App_Code.Proxy.OrderProxy.Order_MallCreate(user.User.ID, AppConfig.SiteID, pList, 8m, 8m, null, payMoney);
            if (orderRtn.Success)
            {
                return RedirectToAction("Alipay", "Cart", new { OrderNum = orderRtn.OBJ.OrderNumber });
            }
            throw new Exception("异常的交易");
        }
    }
}