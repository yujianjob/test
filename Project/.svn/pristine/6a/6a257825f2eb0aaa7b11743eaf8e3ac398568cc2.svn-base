using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.ProductAttribute;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class ProductAttributeController : BaseController
    {
        //
        // GET: /ProductAttribute/
        public ActionResult Index()
        {
            return View();
        }

        #region 产品属性

        /// <summary>
        /// 显示产品属性列表
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexAttributeFirst()
        {
            AttributeFirstListModel list = new AttributeFirstListModel();
            list.SearchInfo = new AttributeFirstSearchInfo();
            return View("IndexAttributeFirst", list);
        }


        /// <summary>
        /// 产品属性列表搜索
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchAttributeFirst(AttributeFirstSearchInfo search, int? pageNum)
        {
            //次查询不处理分页

            if (search == null)
                search = new AttributeFirstSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            AttributeFirstListModel list = new AttributeFirstListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息
            list.AttributeFirstList = ProductAttributeProxy.GetAttributeFirstList();

            if (list.AttributeFirstList != null)
            {

                if (list.AttributeFirstList.Count == 0)
                {
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Info, CodeSource.Common.ConstConfig.INFO_SearchMessage);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("IndexAttributeFirst", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ActionResult EditAttributeFirst(int aid)
        {
            Wash_AttributeDC entity = null;
            if (aid == 0)
            {
                //说明是添加，实例化对象
                entity = new Wash_AttributeDC();
                entity.Enable = 1;
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_AttributeDC> re = ProductAttributeProxy.GetProductAttributeBYID(aid);
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

            return View("EditAttributeFirst", entity);
        }

        /// <summary>
        /// 保存产品属性
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveAttributeFirst(Wash_AttributeDC entity)
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

                ReturnEntity<int> re = ProductAttributeProxy.AddProductAttribute(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                        logobj.LogContent = string.Format("[{0}]新增名称为[{1}]产品属性信息成功", item.Name, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]产品属性信息成功", entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "attributefirstList";
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
                ReturnEntity<bool> re = ProductAttributeProxy.UpdateProductAttribute(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                        logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]产品属性信息成功", item.Name, entity.ID, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]产品属性信息成功", entity.ID, entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "attributefirstList";
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

        #endregion

        #region 产品属性明细

        /// <summary>
        /// 显示产品属性明细列表
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexAttributeSecond()
        {
            AttributeSecondListModel list = new AttributeSecondListModel();
            list.SearchInfo = new AttributeSecondSearchInfo();
            return View("IndexAttributeSecond", list);
        }

        /// <summary>
        /// 产品属性明细列表搜索
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchAttributeSecond(AttributeSecondSearchInfo search, int id, string name, int? pageNum)
        {
            //此查询不处理分页

            if (search == null)
                search = new AttributeSecondSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            AttributeSecondListModel list = new AttributeSecondListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息
            list.PID = id;//保留父ID
            list.AttributeSecondList = ProductAttributeProxy.GetAttributeSecondList(id);

            if (list.AttributeSecondList != null)
            {

                if (list.AttributeSecondList.Count == 0)
                {
                    //ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Info, CodeSource.Common.ConstConfig.INFO_SearchMessage);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("IndexAttributeSecond", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult EditAttributeSecond(int aid, int pid)
        {
            Wash_AttributeDC entity = null;
            if (aid == 0)
            {
                //说明是添加，实例化对象
                entity = new Wash_AttributeDC();
                entity.Enable = 1;
                entity.ParentID = pid;
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_AttributeDC> re = ProductAttributeProxy.GetProductAttributeBYID(aid);
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

            return View("EditAttributeSecond", entity);
        }

        /// <summary>
        /// 保存产品属性明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveAttributeSecond(Wash_AttributeDC entity)
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

                ReturnEntity<int> re = ProductAttributeProxy.AddProductAttribute(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                        logobj.LogContent = string.Format("[{0}]新增名称为[{1}]产品属性明细信息成功", item.Name, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]产品属性明细信息成功", entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "attributesecondList";
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
                ReturnEntity<bool> re = ProductAttributeProxy.UpdateProductAttribute(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                        logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]产品属性明细信息成功", item.Name, entity.ID, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]产品属性明细信息成功", entity.ID, entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "attributesecondList";
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

        #endregion
	}
}