using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.ProductCategory;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.File;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class ProductCategoryController : BaseController
    {
        //
        // GET: /ProductCategory/
        public ActionResult Index()
        {
            return View();
        }

        #region 产品分类

        /// <summary>
        /// 显示产品分类列表
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexCategoryFirst()
        {
            //CategoryFirstListModel list = new CategoryFirstListModel();
            //list.SearchInfo = new CategoryFirstSearchInfo();
            //return View("IndexCategoryFirst", list);

            return SearchCategoryFirst(null, null);
        }


        /// <summary>
        /// 产品分类列表搜索
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchCategoryFirst(CategoryFirstSearchInfo search, int? pageNum)
        {
            //此查询不处理分页

            if (search == null)
                search = new CategoryFirstSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            CategoryFirstListModel list = new CategoryFirstListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息
            list.CategoryFirstList = ProductCategoryProxy.GetCategoryFirstList();

            if (list.CategoryFirstList != null)
            {

                if (list.CategoryFirstList.Count == 0)
                {
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Info, CodeSource.Common.ConstConfig.INFO_SearchMessage);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("IndexCategoryFirst", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult EditCategoryFirst(int cid)
        {
            Wash_ClassDC entity = null;
            if (cid == 0)
            {
                //说明是添加，实例化对象
                entity = new Wash_ClassDC();
                entity.Enable = 1;
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_ClassDC> re = ProductCategoryProxy.GetProductCategoryBYID(cid);
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

            return View("EditCategoryFirst", entity);
        }

        /// <summary>
        /// 保存产品分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCategoryFirst(Wash_ClassDC entity)
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

                ReturnEntity<int> re = ProductCategoryProxy.AddProductCategory(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                        logobj.LogContent = string.Format("[{0}]新增名称为[{1}]产品分类信息成功", item.Name, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]产品分类信息成功", entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "categoryfirstlist";
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
                ReturnEntity<bool> re = ProductCategoryProxy.UpdateProductCategory(entity);
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
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]产品分类信息成功", item.Name, entity.ID, entity.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]产品分类信息成功", entity.ID, entity.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "categoryfirstlist";
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

        #endregion

        #region 产品品类

        /// <summary>
        /// 显示产品品类列表
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexCategorySecond()
        {
            CategorySecondListModel list = new CategorySecondListModel();
            list.SearchInfo = new CategorySecondSearchInfo();
            return View("IndexCategorySecond", list); 
        }

        /// <summary>
        /// 产品品类列表搜索
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchCategorySecond(CategorySecondSearchInfo search, int id, int? pageNum)
        {
            //次查询不处理分页

            if (search == null)
                search = new CategorySecondSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            CategorySecondListModel list = new CategorySecondListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息
            list.PID = id;//保留父ID
            list.CategorySecondList = ProductCategoryProxy.GetCategorySecondList(id);

            if (list.CategorySecondList != null)
            {

                if (list.CategorySecondList.Count == 0)
                {
                    //ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Info, CodeSource.Common.ConstConfig.INFO_SearchMessage);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("IndexCategorySecond", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult EditCategorySecond(int cid, int pid)
        {
            Wash_ClassDC entity = null;
            if (cid == 0)
            {
                //说明是添加，实例化对象
                entity = new Wash_ClassDC();
                entity.Enable = 1;
                entity.ParentID = pid;
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_ClassDC> re = ProductCategoryProxy.GetProductCategoryBYID(cid);
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

            return View("EditCategorySecond", entity);
        }

        /// <summary>
        /// 保存产品品类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCategorySecond(Wash_ClassDC entity)
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

                ReturnEntity<int> re = ProductCategoryProxy.AddProductCategory(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                        logobj.LogContent = string.Format("[{0}]新增名称为[{1}]产品品类信息成功", item.Name, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]产品品类信息成功", entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "categorysecondlist";
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
                ReturnEntity<bool> re = ProductCategoryProxy.UpdateProductCategory(entity);
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
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]产品品类信息成功", item.Name, entity.ID, entity.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]产品品类信息成功", entity.ID, entity.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "categorysecondlist";
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

        #endregion

        #region 图片上传

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="imagetype"></param>
        /// <returns></returns>
        public JsonResult UploadImage(string imagetype)
        {
            HttpPostedFileBase fileData = Request.Files[imagetype];
            if (fileData != null && fileData.ContentLength > 0)
            {
                //获取图片 并调用文件服务器上传图片
                UploadInfoDC fileInfoModel = new UploadInfoDC();
                //begin
                long fileSize = fileData.InputStream.Length;
                fileInfoModel.PostArray = new byte[fileSize];
                fileData.InputStream.Read(fileInfoModel.PostArray, 0, (int)fileSize);
                fileInfoModel.FileName = fileData.FileName;

                //图片上传
                string FileName = CommonProxy.UpLoadFile((int)CodeSource.Common.ProjectType.Wash, fileInfoModel, "ClassImgPath");
                if (!string.IsNullOrEmpty(FileName))
                {
                    return Json(new { status = 1, filename = FileName, type = imagetype, message = "上传成功" });
                }
                else
                {
                    return Json(new { status = 0, filename = "", type = "", message = "上传失败" });
                }
            }

            else
                return Json(new { status = 0, filename = "", type = "", message = "上传失败" });
        }

        #endregion
    }
}