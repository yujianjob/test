using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Activity;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.DataContract.File;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class ActivityController : BaseController
    {
        /// <summary>
        /// 活动展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return SearchActivity(null, null);
        }



        /// <summary>
        /// 活动搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchActivity(ActivitySearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ActivitySearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            ActivityListModel list = new ActivityListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Site == -1)
            {
                search.Site = null;
            }
            if (search.Channel == -1)
            {
                search.Channel = null;
            }
            if (search.CommitStatus == -1)
            {
                search.CommitStatus = null;
            }

            RecordCountEntity<Wash_ActivityDC> rce = ActivityProxy.GetActivityList(search.ActivityName, search.Site, search.Channel, search.CommitStatus, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ActivityList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            //城市下拉框
            SiteListSet();

            return View("Index", list);
        }



        /// <summary>
        /// 展示活动编辑页
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult EditActivity(int aid)
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


            ActivityModel entity = null;
            if (aid == 0)
            {
                //说明是添加，实例化对象
                entity = new ActivityModel();
                entity.ActivityInfo = new Wash_ActivityDC();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_ActivityDC> re = ActivityProxy.GetActivityBYID(aid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ActivityModel();
                        entity.ActivityInfo = re.OBJ;
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


            //城市下拉框
            SiteListSet("City");

            return View("EditActivity", entity);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveActivity(ActivityModel entity,  FormCollection coll)
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

            if (entity.ActivityInfo.Site == -1)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请选择站点！";

                return Json(rjEntity);
            }
            if (entity.ActivityInfo.Channel == -1)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请选择发布渠道！";

                return Json(rjEntity);
            }
            



            entity.ActivityInfo.BeginDate = Convert.ToDateTime(coll["ActivityInfo_BeginDate"].ToString());
            entity.ActivityInfo.EndDate = Convert.ToDateTime(coll["ActivityInfo_EndDate"].ToString()).AddHours(23).AddMinutes(59).AddSeconds(59);



            //修改人ID
            entity.ActivityInfo.Obj_Muser = item.ID;

            if (entity.ActivityInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.ActivityInfo.Obj_Cuser = item.ID;

                ReturnEntity<int> re = ActivityProxy.AddActivity(entity.ActivityInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Activity; ;
                        logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]活动信息成功", item.Name, entity.ActivityInfo.ID, entity.ActivityInfo.Title);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]活动信息成功", entity.ActivityInfo.Title);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "activitylist";
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
            else
            {
                //说明是编辑
                ReturnEntity<bool> re = ActivityProxy.UpdateActivity(entity.ActivityInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        if (re.OBJ)
                        {
                            //成功 记录操作日志
                            OperatorLogDC logobj = new OperatorLogDC();
                            logobj.OperatorID = item.ID;
                            logobj.OperatorName = item.Name;
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Activity; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]活动信息成功", item.Name, entity.ActivityInfo.ID, entity.ActivityInfo.Title);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]活动信息成功", entity.ActivityInfo.ID, entity.ActivityInfo.Title);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "activitylist";
                        }
                        else
                        {
                            rjEntity.statusCode = CodeSource.StatusCode.Faild;
                            rjEntity.message = CodeSource.Common.ConstConfig.WRONG_UpdateErrorMessage;
                        }
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

            //}


            return Json(rjEntity);
        }
	}
}