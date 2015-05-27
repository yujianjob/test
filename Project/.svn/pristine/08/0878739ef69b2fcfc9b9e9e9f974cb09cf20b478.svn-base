using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Log;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using System.Web.UI.WebControls;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class LogController : BaseController
    {
        //
        // GET: /Log/
        public ActionResult Index()
        {
            return View();
        }

        #region 后台操作日志

        /// <summary>
        /// 显示后台操作日志列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult OperatorLogIndex()
        {
            OperatorLogListModel list = new OperatorLogListModel();
            list.SearchInfo = new OperatorLogSearchInfo();
            list.SearchInfo.DateFrom = System.DateTime.Now.Date.AddDays(-6);
            list.SearchInfo.DateTo = System.DateTime.Now.Date;

            return View("OperatorLogIndex", list);
        }


        /// <summary>
        /// 后台操作日志搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchOperatorLog(OperatorLogSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new OperatorLogSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OperatorLogListModel list = new OperatorLogListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Type == -1)
            {
                search.Type = null;
            }


            RecordCountEntity<OperatorLogDC> rce = LogProxy.GetOperatorLogList(search.Type, search.OperatorName, search.LogContent, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.OperatorLogList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("OperatorLogIndex", list);
        }

        #endregion


        #region B端操作日志

        /// <summary>
        /// 显示B端操作日志列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreOperatorLogIndex()
        {
            StoreOperatorLogListModel list = new StoreOperatorLogListModel();
            list.SearchInfo = new StoreOperatorLogSearchInfo();
            list.SearchInfo.DateFrom = System.DateTime.Now.Date.AddDays(-6);
            list.SearchInfo.DateTo = System.DateTime.Now.Date;
            list.StoreOperatorLogList = null;
            return View("StoreOperatorLogIndex", list);
        }


        /// <summary>
        /// B端操作日志搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchStoreOperatorLog(StoreOperatorLogSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new StoreOperatorLogSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            StoreOperatorLogListModel list = new StoreOperatorLogListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Type == -1)
            {
                search.Type = null;
            }


            RecordCountEntity<Wash_Store_OperatorLogDC> rce = LogProxy.GetStoreOperatorLogList(search.Type, search.OperatorName, search.LogContent, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.StoreOperatorLogList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("StoreOperatorLogIndex", list);
        }

        #endregion


        #region 系统日志

        /// <summary>
        /// 显示系统日志列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemLogIndex()
        {
            SystemLogListModel list = new SystemLogListModel();
            list.SearchInfo = new SystemLogSearchInfo();
            list.SearchInfo.DateFrom = System.DateTime.Now.Date.AddDays(-6);
            list.SearchInfo.DateTo = System.DateTime.Now.Date;


            IList<ListItem> appList = new List<ListItem>();
            foreach (var item in Enum.GetNames(typeof(LazyAtHome.Core.Enumerate.ApplicationModule)))
            {
                ListItem listitem = new ListItem(item, item);
                appList.Add(listitem);
            }
            appList.Insert(0, new ListItem() { Value = "all", Text = "全部" });
            ViewData["AppName"] = new SelectList(appList.AsEnumerable(), "Value", "Text");


            return View("SystemLogIndex", list);
        }


        /// <summary>
        /// 后台操作日志搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchSystemLog(SystemLogSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new SystemLogSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            SystemLogListModel list = new SystemLogListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.AppDomainName == "all")
            {
                search.AppDomainName = null;
            }


            RecordCountEntity<Base_LogDC> rce = LogProxy.GetBaseLogList(search.AppDomainName, search.Title, search.EventType, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.SystemLogList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }



            IList<ListItem> appList = new List<ListItem>();
            foreach (var item in Enum.GetNames(typeof(LazyAtHome.Core.Enumerate.ApplicationModule)))
            {
                ListItem listitem = new ListItem(item, item);
                appList.Add(listitem);
            }
            appList.Insert(0, new ListItem() { Value = "all", Text = "全部" });
            ViewData["AppName"] = new SelectList(appList.AsEnumerable(), "Value", "Text");




            return View("SystemLogIndex", list);
        }

        /// <summary>
        /// 显示日志详情
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult ViewSystemLog(string message)
        {

            Base_LogDC entity = new Base_LogDC();
            entity.Messages = message;
            return View("ViewSystemLog", entity);
        }

        #endregion
    }
}