using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Coupon;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;


namespace LazyAtHome.Web.WebManage.Controllers
{
    public class CouponController : BaseController
    {
        /// <summary>
        /// 优惠券列表展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return SearchCoupon(null, null);
        }

        /// <summary>
        /// 优惠券搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchCoupon(CouponSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new CouponSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            CouponListModel list = new CouponListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.UseClass == -1)
            {
                search.UseClass = null;
            }
            if (search.UseType == -1)
            {
                search.UseType = null;
            }
            if (search.CommitStatus == -1)
            {
                search.CommitStatus = null;
            }

            RecordCountEntity<Base_CouponDC> rce = CouponProxy.GetCouponList(search.CouponName, search.UseClass, search.UseType, search.CommitStatus, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.CouponList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("Index", list);
        }
	}
}