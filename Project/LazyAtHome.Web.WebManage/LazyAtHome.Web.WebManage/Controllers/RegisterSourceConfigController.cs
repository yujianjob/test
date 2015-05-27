using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.RegisterSourceConfig;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
//using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class RegisterSourceConfigController : BaseController
    {
        /// <summary>
        /// 用户推广配置展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //return View();
            return SearchRegisterSourceConfig(null, null);
        }


        /// <summary>
        /// 用户推广配置搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchRegisterSourceConfig(RegisterSourceConfigSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new RegisterSourceConfigSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            RegisterSourceConfigListModel list = new RegisterSourceConfigListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Type == -1)
            {
                search.Type = null;
            }

            RecordCountEntity<User_RegisterSourceDC> rce = RegisterSourceConfigProxy.GetRegisterSourceList(search.Type, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.RegisterSourceConfigList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            return View("Index", list);
        }



        /// <summary>
        /// 展示活动编辑页
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public ActionResult EditRegisterSourceConfig(int rid)
        {
            CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

            //获取登录信息
            OperatorDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                return Json(rjEntity, JsonRequestBehavior.AllowGet);
            }


            RegisterSourceConfigModel entity = null;
            if (rid == 0)
            {
                //说明是添加，实例化对象
                entity = new RegisterSourceConfigModel();
                entity.RegisterSourceConfigInfo = new User_RegisterSourceDC();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<User_RegisterSourceDC> re = RegisterSourceConfigProxy.GetRegisterSourceBYID(rid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new RegisterSourceConfigModel();
                        entity.RegisterSourceConfigInfo = re.OBJ;
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

            }


            return View("EditRegisterSourceConfig", entity);
        }


        /// <summary>
        /// 编辑推广配置
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveRegisterSourceConfig(RegisterSourceConfigModel entity)
        {
            CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

            //获取登录信息
            OperatorDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                return Json(rjEntity);
            }

           

            //修改人ID
            entity.RegisterSourceConfigInfo.Obj_Muser = item.ID;

            if (entity.RegisterSourceConfigInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.RegisterSourceConfigInfo.Obj_Cuser = item.ID;

                ReturnEntity<int> re = RegisterSourceConfigProxy.AddRegisterSource(entity.RegisterSourceConfigInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web; ;
                        logobj.LogContent = string.Format("[{0}]新增ID为[{1}]推广编号为[{2}]推广配置成功", item.Name, re.OBJ, entity.RegisterSourceConfigInfo.InternalKey);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增推广编号为[{0}]推广配置成功", entity.RegisterSourceConfigInfo.InternalKey);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "registersourceconfiglist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = re.Message;
                    }
                }
                else
                {
                    //失败
                    rjEntity.statusCode = CodeSource.StatusCode.Faild;
                    rjEntity.message = CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage;
                }
            }
            


            return Json(rjEntity);
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