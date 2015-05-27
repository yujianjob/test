using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Luxury;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;

namespace LazyAtHome.Web.WebManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LuxuryController : BaseController
    {
        /// <summary>
        /// 显示奢侈品列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 奢侈品搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchLuxury(LuxurySearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new LuxurySearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            LuxuryListModel list = new LuxuryListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理



            RecordCountEntity<Luxury_ProductDC> rce = LuxuryProxy.GetLuxuryList(search.LuxuryName, search.CommitStatus, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.LuxuryList = rce.ReturnList;
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