using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LazyAtHome.Web.SFSupport.Models.RegisterSourceConfig;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.Web.SFSupport.CodeSource.Proxy;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
//using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.SFSupport.Controllers
{
    public class RegisterSourceConfigController : Controller
    {
        //
        // GET: /RegisterSourceConfig/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccountIndex(string InternalKey)
        {
            AccountListModel list = new AccountListModel();
            list.SearchInfo = new AccountSearchInfo();
            //日期默认上周二到本周一
            list.SearchInfo.DateFrom = GetWeekUpOfDate(System.DateTime.Now, DayOfWeek.Monday, -1);
            list.SearchInfo.DateTo = GetWeekUpOfDate(System.DateTime.Now, DayOfWeek.Sunday, 0);
            if (!string.IsNullOrEmpty(InternalKey))
            {
                list.SearchInfo.InternalKey = InternalKey;
            }

            return View("Account", list);
        }

        /// <summary>
        /// 获取固定日期
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="weekday"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        private DateTime GetWeekUpOfDate(DateTime dt, DayOfWeek weekday, int Number)
        {
            int wd1 = (int)weekday;
            int wd2 = (int)dt.DayOfWeek;
            return wd2 == wd1 ? dt.AddDays(7 * Number) : dt.AddDays(7 * Number - wd2 + wd1);
        }

        public ActionResult SearchAccount(AccountSearchInfo search)
        {
            if (search == null)
                search = new AccountSearchInfo();


            //if (string.IsNullOrEmpty(search.InternalKey))
            //{
            //    //处理提示逻辑   
            //    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Warn, "请填写推广编号");
            //    return View("Account", new AccountListModel());
            //}

            //if (pageNum.HasValue)
            //    search.pageNum = pageNum.Value;//设置当前页

            AccountListModel list = new AccountListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            ReturnEntity<IList<User_WeixinAttentionLogStatDC>> re = RegisterSourceConfigProxy.GetWeixinAttentionLogStatList(search.InternalKey, search.DateFrom, search.DateTo.AddDays(1));
            IList<User_WeixinAttentionLogStatDC> statlist = null;
            //IList<User_WeixinAttentionLogStatDC> statlist = RegisterSourceConfigProxy.GetWeixinAttentionLogStatList(search.InternalKey, search.DateFrom, search.DateTo.AddDays(1));

            if (re != null)
            {
                if (re.Success)
                {
                    statlist = re.OBJ;

                    //处理获取到的数据

                    //判断是兼职还是顺丰
                    list.RegisterSourceType = statlist.First().RegisterSourceType;
                    if (list.RegisterSourceType == 1)
                    {
                        //兼职

                        //统计汇总
                        list.AccountJob = new AccountStatJob();
                        //推广编号
                        list.AccountJob.InternalKey = search.InternalKey;
                        //计算工作日
                        list.AccountJob.WorkDayCount = statlist.Count(p => p.AttentionCount != 0);
                        //日期
                        list.AccountJob.DateFrom = search.DateFrom;
                        list.AccountJob.DateTo = search.DateTo;
                        //关注数
                        list.AccountJob.AttentionCount = statlist.Sum(p => p.AttentionCount);
                        //取消关注数
                        list.AccountJob.RemoveAttentionCount = statlist.Sum(p => p.RemoveAttentionCount);
                        //注册数
                        list.AccountJob.RegisterCount = statlist.Sum(p => p.RegisterCount);



                        //统计明细
                        list.AccountJobDetailList = new List<AccountStatJob>();
                        foreach (User_WeixinAttentionLogStatDC item in statlist)
                        {
                            AccountStatJob detail = new AccountStatJob();
                            //推广编号
                            detail.InternalKey = search.InternalKey;

                            //计算工作日
                            detail.WorkDayCount = item.AttentionCount > 0 ? 1 : 0;

                            //日期
                            detail.Date = item.StatDate;
                            //关注数
                            detail.AttentionCount = item.AttentionCount;
                            //取消关注数
                            detail.RemoveAttentionCount = item.RemoveAttentionCount;
                            //注册数
                            detail.RegisterCount = item.RegisterCount;

                            list.AccountJobDetailList.Add(detail);

                        }

                    }
                    else
                    {
                        //顺丰
                        //统计汇总
                        list.AccountSF = new AccountStatSF();
                        //推广编号
                        list.AccountSF.InternalKey = search.InternalKey;

                        //日期
                        list.AccountSF.DateFrom = search.DateFrom;
                        list.AccountSF.DateTo = search.DateTo;
                        //关注数
                        list.AccountSF.AttentionCount = statlist.Sum(p => p.AttentionCount);
                        //取消关注数
                        list.AccountSF.RemoveAttentionCount = statlist.Sum(p => p.RemoveAttentionCount);
                        //注册数
                        list.AccountSF.RegisterCount = statlist.Sum(p => p.RegisterCount);

                        //统计明细
                        list.AccountSFDetailList = new List<AccountStatSF>();
                        foreach (User_WeixinAttentionLogStatDC item in statlist)
                        {
                            AccountStatSF detail = new AccountStatSF();
                            //推广编号
                            detail.InternalKey = search.InternalKey;
                            //日期
                            detail.Date = item.StatDate;
                            //关注数
                            detail.AttentionCount = item.AttentionCount;
                            //取消关注数
                            detail.RemoveAttentionCount = item.RemoveAttentionCount;
                            //注册数
                            detail.RegisterCount = item.RegisterCount;

                            list.AccountSFDetailList.Add(detail);

                        }
                    }
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
                }


            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("Account", list);
        }
	}
}