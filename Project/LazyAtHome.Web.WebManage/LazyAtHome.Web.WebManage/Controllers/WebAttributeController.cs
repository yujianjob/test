using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.WebAttribute;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;


namespace LazyAtHome.Web.WebManage.Controllers
{
    public class WebAttributeController : BaseController
    {
        /// <summary>
        /// 页面属性展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return SearchWebAttribute(null, null);
        }


        /// <summary>
        /// 页面属性搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchWebAttribute(WebAttributeSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new WebAttributeSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            WebAttributeListModel list = new WebAttributeListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理


            RecordCountEntity<Base_WebAttributeDC> rce = WebAttributeProxy.GetWebAttributeList(search.WebAttributeName, search.WebAttributePage, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.WebAttributeList = rce.ReturnList;
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
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult EditWebAttribute(int wid)
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

            Base_WebAttributeDC entity = null;
            if (wid == 0)
            {
                //说明是添加，实例化对象
                entity = new Base_WebAttributeDC();
                
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Base_WebAttributeDC> re = WebAttributeProxy.GetWebAttributeBYID(wid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = re.OBJ;
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

            return View("EditWebAttribute", entity);
        }


        /// <summary>
        /// 编辑页面属性
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveWebAttribute(Base_WebAttributeDC entity)
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
            entity.Obj_Muser = item.ID;

            if (entity.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.Obj_Cuser = item.ID;

                ReturnEntity<int> re = WebAttributeProxy.AddWebAttribute(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web; ;
                        logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]页面属性信息成功", item.Name, entity.ID, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]页面属性信息成功", entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "webattributelist";
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
                ReturnEntity<bool> re = WebAttributeProxy.UpdateWebAttribute(entity);
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
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]页面属性信息成功", item.Name, entity.ID, entity.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]页面属性信息成功", entity.ID, entity.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "webattributelist";
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



        //DeleteWebAttribute
        public JsonResult DeleteWebAttribute(int wid)
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
            //entity.Obj_Muser = item.ID;

            ReturnEntity<bool> re = WebAttributeProxy.DeleteWebAttribute(wid);
            if (re != null)
            {
                if (re.Success)
                {
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web;
                    logobj.LogContent = string.Format("[{0}]删除ID为[{1}]页面属性信息成功", item.Name, wid);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("删除ID为[{0}]页面属性信息成功", wid);
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "webattributelist";
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


            return Json(rjEntity);
        }
	}
}