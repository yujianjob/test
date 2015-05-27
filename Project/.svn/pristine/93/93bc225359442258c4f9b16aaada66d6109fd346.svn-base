using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.Web.WebManage.Models.Common;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class CommonController :BaseController
    {
        //
        // GET: /Common/
        public ActionResult Index()
        {
            return View();
        }


        #region 行政区划控件

        /// <summary>
        /// 行政区划控件
        /// </summary>
        /// <param name="id">行政区划Id</param>
        /// <returns>行政区划控件</returns>
        [ChildActionOnly]
        public ActionResult Divisions(int id, string bindName, string DivisionsL1Name = "ddlDivisionL1", string DivisionsL2Name = "ddlDivisionL2", string DivisionsL3Name = "ddlDivisionL3")
        {
            ViewBag.BindName = bindName;
            var model = new DivisionsModel();
            model.Data = new Base_DistrictDC() { ID = id };


            model.DivisionsL1Name = DivisionsL1Name;
            model.DivisionsL2Name = DivisionsL2Name;
            model.DivisionsL3Name = DivisionsL3Name;

            //获取一级行政区列表
            IList<Base_DistrictDC> divisionsL1 = BaseInfoProxy.Base_GetAllDistrict().Where(p => p.Level == 1).ToList();
            divisionsL1.Insert(0, new Base_DistrictDC() { ID = -1, Name = "请选择" });
            model.DivisionsL1 = divisionsL1;


            string code1 = string.Empty;
            if (id != 0)
            {
                //截取ID前2位
                code1 = id.ToString().Substring(0, 2);
            }
            //获取二级行政区列表
            IList<Base_DistrictDC> divisionsL2 = BaseInfoProxy.Base_GetDistrict_L2(code1);
            divisionsL2.Insert(0, new Base_DistrictDC() { ID = -1, Name = "请选择" });
            model.DivisionsL2 = divisionsL2;

            string code2 = string.Empty;
            if (id != 0)
            {
                //截取ID中间2位
                code2 = id.ToString().Substring(2, 2);
            }
            
            //获取三级行政区列表
            IList<Base_DistrictDC> divisionsL3 = BaseInfoProxy.Base_GetDistrict_L3(code1, code2);
            divisionsL3.Insert(0, new Base_DistrictDC() { ID = -1, Name = "请选择" });
            model.DivisionsL3 = divisionsL3;
            return PartialView("Divisions", model);
        }

        /// <summary>
        /// 行政区划三级联动
        /// </summary>
        /// <param name="level">行政区划级别</param>
        /// <param name="id">行政区划Id</param>
        /// <returns>行政区划列表Json</returns>
        [HttpPost]
        public ActionResult DivisionsGet(int level, int id)
        {
            IList<Base_DistrictDC> divis = null;

            //截取ID前2位
            string code1 = id.ToString().Substring(0, 2);
            //截取ID中间2位
            string code2 = id.ToString().Substring(2, 2);

            //获取一级行政区列表
            if (level == 1)
            {
                divis = BaseInfoProxy.Base_GetAllDistrict().Where(p => p.Level == 1).ToList();
            }
            else if (level == 2)//获取二级行政区列表
            {
                divis = BaseInfoProxy.Base_GetDistrict_L2(code1);
            }
            else//获取三级行政区列表
            {
                divis = BaseInfoProxy.Base_GetDistrict_L3(code1, code2);
            }

            return Json(divis);
        }

        #endregion


        #region 分类 品类 二级联动控件

        /// <summary>
        /// 分类 品类 二级联动控件
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="id"></param>
        /// <param name="bindName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult WashClass(int pid, int id, string bindName)
        {
            ViewBag.BindName = bindName;
            WashClassModel model = new WashClassModel();
            model.ID = id;
            model.PID = pid;

            //获取产品分类列表
            IList<Wash_ClassDC> WashClassL1 = ProductCategoryProxy.GetCategoryFirstList();
            WashClassL1.Insert(0, new Wash_ClassDC() { ID = -1, Name = "请选择" });
            model.WashClassL1 = WashClassL1;

            //获取产品品类列表
            IList<Wash_ClassDC> WashClassL2 = ProductCategoryProxy.GetCategorySecondList(pid);
            WashClassL2.Insert(0, new Wash_ClassDC() { ID = -1, Name = "请选择" });
            model.WashClassL2 = WashClassL2;


            return PartialView("WashClass", model);
        }

        /// <summary>
        /// 分类 品类 二级联动
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="id">Id</param>
        /// <returns>Json</returns>
        [HttpPost]
        public ActionResult WashClassGet(int level, int id)
        {
            IList<Wash_ClassDC> washclass = null;

            //获取分类列表
            if (level == 1)
            {
                washclass = ProductCategoryProxy.GetCategoryFirstList();
            }
            else if (level == 2)//获取品类列表
            {
                washclass = ProductCategoryProxy.GetCategorySecondList(id);
            }

            return Json(washclass);
        }

        #endregion


        #region 分类 品类 产品 三级联动控件

        /// <summary>
        /// 分类 品类 产品 三级联动控件
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="id"></param>
        /// <param name="sid"></param>
        /// <param name="bindName"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult CategoryCombox(int pid, int id, int sid, bool enable, string bindName)
        {
            ViewBag.BindName = bindName;
            CategoryComboxModel model = new CategoryComboxModel();
            model.ID = id;
            model.PID = pid;
            model.SID = sid;
            model.Enable = enable;

            //获取产品分类列表
            IList<Wash_ClassDC> WashClassL1 = ProductCategoryProxy.GetCategoryFirstList();
            WashClassL1.Insert(0, new Wash_ClassDC() { ID = -1, Name = "请选择" });
            model.WashClassL1 = WashClassL1;

            //获取产品品类列表
            IList<Wash_ClassDC> WashClassL2 = ProductCategoryProxy.GetCategorySecondList(pid);
            WashClassL2.Insert(0, new Wash_ClassDC() { ID = -1, Name = "请选择" });
            model.WashClassL2 = WashClassL2;

            //获取产品列表
            IList<Wash_CategoryDC> WashCategory = CategoryProxy.GetCategoryListBYClassID(id);

            IList<Wash_CategoryDC> WashClassL3 = new List<Wash_CategoryDC>();
            foreach (var item in WashCategory)
            {
                WashClassL3.Add(item);
            }

            WashClassL3.Insert(0, new Wash_CategoryDC() { ID = -1, Name = "请选择" });
            model.WashClassL3 = WashClassL3;

            return PartialView("CategoryCombox", model);
        }

        /// <summary>
        /// 分类 品类 产品 三级联动
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="id">Id</param>
        /// <returns>Json</returns>
        [HttpPost]
        public ActionResult CategoryComboxGet(int level, int id)
        {
            //获取一级行政区列表
            if (level == 1)
            {
                IList<Wash_ClassDC> washclass = ProductCategoryProxy.GetCategoryFirstList();
                return Json(washclass);
            }
            else if (level == 2)//获取二级行政区列表
            {
                IList<Wash_ClassDC> washclass = ProductCategoryProxy.GetCategorySecondList(id);
                return Json(washclass);
            }
            else
            {
                IList<Wash_CategoryDC> category = CategoryProxy.GetCategoryListBYClassID(id);
                return Json(category);
            }

            //return Json(washclass);
        }

        #endregion



        #region 消息通知

        /// <summary>
        /// 物流订单列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult NotifyIndex()
        {
            NotifySearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchNotify] as NotifySearchInfo;
            if (search != null)
            {
                search.NotifyStatus = 100;
                //search.Level = 4;
                return SearchNotify(search, search.pageNum);
            }
                
            else
                return SearchNotify(null, null);

        }

        /// <summary>
        /// 消息搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchNotify(NotifySearchInfo search, int? pageNum)
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


            if (search == null)
            {
                search = new NotifySearchInfo();
                search.NotifyStatus = 100;
                //search.Level = 4;
            }
                
            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchNotify] = search;

            NotifyListModel list = new NotifyListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Level == -1)
            {
                search.Level = null;
            }
            if (search.NotifyStatus == -1)
            {
                search.NotifyStatus = null;
            }

            RecordCountEntity<Base_NotifyDC> rce = CommonProxy.GetNotifyList(search.EventNumber, search.OrderNumber, item.RoleID, item.ID, search.Title, search.Level, search.NotifyStatus, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.NotifyList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("NotifyIndex", list);
        }

        /// <summary>
        /// 消息通知 展示页
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public ActionResult EditNotify(int nid)
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

            ViewBag.RoleID = item.RoleID;
            ViewBag.OperatorID = item.ID;

            NotifyModel entity = null;
            if (nid == 0)
            {
                //说明是添加，实例化对象
                entity = new NotifyModel();
                entity.NotifyInfo = new Base_NotifyDC();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Base_NotifyDC> re = CommonProxy.GetNotifyBYID(nid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new NotifyModel();
                        entity.NotifyInfo = re.OBJ;
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

            return View("EditNotify", entity);
        }


        /// <summary>
        /// 领取 消息处理
        /// </summary>
        /// <param name="notifyid"></param>
        /// <returns></returns>
        public JsonResult DealNotify(int notifyid)
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

            ReturnEntity<bool> re = CommonProxy.UPDATENotifyGet(notifyid, item.ID);
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
                        logobj.LogContent = string.Format("[{0}]领取通知消息[{1}]成功", item.Name, notifyid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("领取通知消息成功！");
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "notifyedit"; 
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
                    rjEntity.message = CodeSource.Common.ConstConfig.WRONG_UpdateErrorMessage;
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

        public ActionResult DealNotifyByIndex(int notifyid)
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

            ReturnEntity<bool> re = CommonProxy.UPDATENotifyGet(notifyid, item.ID);
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
                        logobj.LogContent = string.Format("[{0}]领取通知消息[{1}]成功", item.Name, notifyid);
                        OperatorProxy.OperateLog_Add(logobj);

                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
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

            return EditNotify(notifyid);
        }

        /// <summary>
        /// 更新 通知消息 处理进程
        /// </summary>
        /// <param name="notifyid"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public JsonResult ResultNotify(int notifyid, string result)
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

            ReturnEntity<bool> re = CommonProxy.UPDATENotifyResult(notifyid, result, item.ID);
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
                        logobj.LogContent = string.Format("[{0}]更新通知消息处理进程成功！", item.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("更新通知消息处理进程成功！");
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "notifyedit";
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
                    rjEntity.message = CodeSource.Common.ConstConfig.WRONG_UpdateErrorMessage;
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



        /// <summary>
        /// 消息处理 完成/关闭
        /// </summary>
        /// <param name="notifyid"></param>
        /// <param name="result"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JsonResult FinishNotify(int notifyid, string result, int status)
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

            ReturnEntity<bool> re = CommonProxy.UPDATENotifyFinish(notifyid, result, status, item.ID);
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
                        logobj.LogContent = string.Format("[{0}]完成通知消息成功！处理状态[{1}]", item.Name, status);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("完成通知消息成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "notifylist";
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
                    rjEntity.message = CodeSource.Common.ConstConfig.WRONG_UpdateErrorMessage;
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

        #endregion



    }
}