using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Manager;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class ManagerController : BaseController
    {
        

        /// <summary>
        /// 显示管理员列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ManagerSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchManager] as ManagerSearchInfo;
            if (search != null)
                return SearchManager(search, null);
            else
                return SearchManager(null, null);

        }

        /// <summary>
        /// 管理员搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchManager(ManagerSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ManagerSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchManager] = search;

            ManagerListModel list = new ManagerListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Enable == -1)
            {
                search.Enable = null;
            }


            RecordCountEntity<OperatorDC> rce = OperatorProxy.GetManagerList(search.UserName, search.MPNo, string.Empty, search.Enable, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ManagerList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }
            
            return View("Index", list);
        }



        /// <summary>
        /// 展示管理员编辑页面
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult EditManager(int uid)
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

            //OperatorDC entity = null;
            ManagerModel entity = null;
            if (uid == 0)
            {
                //说明是添加，实例化对象
                //entity = new OperatorDC();

                entity = new ManagerModel();
                entity.ManagerInfo = new OperatorDC();
                entity.ManagerInfo.Enable = 1;
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<OperatorDC> re = OperatorProxy.GetManagerBYID(uid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //entity = re.OBJ;
                        ////密码是非明码的 防止页面上显示太长
                        //entity.LoginPwd = "1111111111";

                        entity = new ManagerModel();
                        entity.ManagerInfo = re.OBJ;
                        //entity.ManagerInfo.LoginPwd = "1111111111";
                        entity.ManagerInfo.LoginPwd = string.Empty;
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
                        entity = new ManagerModel();
                        entity.ManagerInfo = new OperatorDC();
                    }
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    entity = new ManagerModel();
                    entity.ManagerInfo = new OperatorDC();
                }
            }

            RoleListSet();

            return View("EditManager", entity);
            //return Index();
        }

        public JsonResult DeleteManager(string uid)
        {
            CodeSource.ReturnJsonEntity entity = new CodeSource.ReturnJsonEntity();
            entity.statusCode = CodeSource.StatusCode.Success;
            entity.message = "删除[" + uid + "]成功";
            //entity.callbackType = CodeSource.CallbackType.CloseCurrent;
            //entity.navTabId = "manageList";
            return Json(entity);
        }

        /// <summary>
        /// 管理员编辑
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditManager(ManagerModel entity)
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


            if (entity.ManagerInfo.RoleID == -1)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请选择职责类型";

                return Json(rjEntity);
            }


            //赋值 修改操作人     
            entity.ManagerInfo.Obj_Muser = item.ID;

            //默认开启
            //entity.ManagerInfo.Enable = 1;

            if (entity.ManagerInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.ManagerInfo.Obj_Cuser = item.ID;

                

                //新增
                ReturnEntity<int> re = OperatorProxy.AddManager(entity.ManagerInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Manager;
                        logobj.LogContent = string.Format("[{0}]新增账户[{1}]", item.Name, entity.ManagerInfo.LoginName);
                        OperatorProxy.OperateLog_Add(logobj);

                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = "新增帐号信息成功";
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "managerList";
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
                ReturnEntity<bool> re = OperatorProxy.UpdateManager(entity.ManagerInfo);
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
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Manager;
                            logobj.LogContent = string.Format("[{0}]更新账户[{1}]成功", item.Name, entity.ManagerInfo.LoginName);
                            OperatorProxy.OperateLog_Add(logobj);

                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = "更新帐号信息成功";
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "managerList";
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
            


            return Json(rjEntity);
        }



        /// <summary>
        /// 展示修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPassword()
        {
            return View();
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="currpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public JsonResult ResetPassword(string currpassword, string newpassword)
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

            //ResetPassword
            ReturnEntity<bool> re = OperatorProxy.ResetPassword(item.ID, currpassword, newpassword, item.ID);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Manager;
                    logobj.LogContent = string.Format("[{0}]进行修改密码成功", item.Name);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("密码修改成功！");
                    rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    //rjEntity.navTabId = "";

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