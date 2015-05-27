using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Category;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.DataContract.File;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class CategoryController : BaseController
    {
        /// <summary>
        /// 显示产品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //CategoryListModel list = new CategoryListModel();
            //list.SearchInfo = new CategorySearchInfo();
            //return View("Index", list);

            return SearchCategory(null, null);
        }

        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchCategory(CategorySearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new CategorySearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            CategoryListModel list = new CategoryListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Enable == -1)
            {
                search.Enable = null;
            }

            RecordCountEntity<Wash_CategoryDC> rce = CategoryProxy.GetCategoryList(search.CategoryName, search.CategoryCode, search.Enable, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.CategoryList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("Index", list);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult EditCategory(int cid)
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

            CategoryModel entity = null;
            if (cid == 0)
            {
                //说明是添加，实例化对象
                entity = new CategoryModel();
                entity.CategoryInfo = new Wash_CategoryDC();
                entity.CategoryInfo.Enable = 1;

                //产品移初备选属性
                //entity.AttributeList = GetCategroyAttribute(null);//添加时候传null
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_CategoryDC> re = CategoryProxy.GetCategoryBYID(cid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new CategoryModel();
                        entity.CategoryInfo = re.OBJ;

                        //产品移初备选属性
                        //entity.AttributeList = GetCategroyAttribute(entity.CategoryInfo.AttributeList);
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

            return View("EditCategory", entity);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCategory(CategoryModel entity, int BDVIdL1, int BDVIdL2, string strAttributeIDSelected, FormCollection coll)
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

            //产品移初备选属性
            //entity.strAttributeIDSelected = strAttributeIDSelected;
            
            
            entity.CategoryInfo.ClassID = BDVIdL2;


            //产品移初备选属性
            //判断是否有选中属性
            //if (string.IsNullOrEmpty(entity.strAttributeIDSelected))
            //{
            //    //没有选中，直接报错
            //    rjEntity.statusCode = CodeSource.StatusCode.Faild;
            //    rjEntity.message = "请选择产品属性";
            //}
            //else
            //{

                //修改人ID
                entity.CategoryInfo.Obj_Muser = item.ID;           

                if (entity.CategoryInfo.ID == 0)
                {
                    //说明是添加

                    //添加人ID
                    entity.CategoryInfo.Obj_Cuser = item.ID;

                    ReturnEntity<int> re = CategoryProxy.AddCategory(entity.CategoryInfo);
                    if (re != null)
                    {
                        if (re.Success)
                        {
                            //成功 记录操作日志
                            OperatorLogDC logobj = new OperatorLogDC();
                            logobj.OperatorID = item.ID;
                            logobj.OperatorName = item.Name;
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                            logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]产品信息成功", item.Name, entity.CategoryInfo.ID, entity.CategoryInfo.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("新增名称为[{0}]产品信息成功", entity.CategoryInfo.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "categorylist";
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
                    ReturnEntity<bool> re = CategoryProxy.UpdateCategory(entity.CategoryInfo);
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
                                logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]产品信息成功", item.Name, entity.CategoryInfo.ID, entity.CategoryInfo.Name);
                                OperatorProxy.OperateLog_Add(logobj);

                                //界面返回信息
                                rjEntity.statusCode = CodeSource.StatusCode.Success;
                                rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]产品信息成功", entity.CategoryInfo.ID, entity.CategoryInfo.Name);
                                rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                                rjEntity.navTabId = "categorylist";
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


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="imagetype"></param>
        /// <returns></returns>
        public JsonResult UploadImage(string imagetype)
        {
            //HttpPostedFileBase fileData = Request.Files["categoryimagel"];
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
                string FileName = CommonProxy.UpLoadFile((int)CodeSource.Common.ProjectType.Wash, fileInfoModel, "CategoryImgPath");
                if (!string.IsNullOrEmpty(FileName))
                {
                    return Json(new { status = 1, filename = FileName, type = imagetype,  message = "上传成功" });
                }
                else
                {
                    return Json(new { status = 0, filename = "", type = "", message = "上传失败" });
                }
            }

            else
                return Json(new { status = 0, filename = "", type = "", message = "上传失败" });
        }

        //产品移除备选属性
        ///// <summary>
        ///// 整理产品属性 用于界面展示
        ///// </summary>
        ///// <param name="SelectedAttribute">从数据库中获取的已经选择的产品属性 添加的时候为NULL</param>
        ///// <returns></returns>
        //private IList<CategroyAttribute> GetCategroyAttribute(IList<Wash_AttributeDC> SelectedAttribute)
        //{
        //    IList<CategroyAttribute> cas = null;

        //    //先获取所有备选属性大类
        //    IList<Wash_AttributeDC> p_list = ProductAttributeProxy.GetAttributeFirstList();
        //    if (p_list != null)
        //    {
        //        //实例化自定义备选属性List对象
        //        cas = new List<CategroyAttribute>();
        //        //循环数据库获取的p_list，来构建自定义备选属性
        //        foreach (var item in p_list)
        //        {
        //            //实例化自定义备选属性对象
        //            CategroyAttribute tmp = new CategroyAttribute();
        //            //自定义备选属性对象 大类赋值
        //            tmp.ParentAttribute = item;
        //            //自定义备选属性对象 小类List实例化赋值
        //            tmp.SubAttributeList = new List<WashAttribute>();
        //            //获取数据库小类List
        //            IList<Wash_AttributeDC> tmpsub = ProductAttributeProxy.GetAttributeSecondList(item.ID);
        //            //循环数据库小类List
        //            foreach (Wash_AttributeDC subitem in tmpsub)
        //            {
        //                //实例化自定义小类对象
        //                WashAttribute sub = new WashAttribute();
        //                //小类信息赋值
        //                sub.SubWashAttribute = subitem;

        //                if (SelectedAttribute == null)
        //                {
        //                    sub.isSelected = false;//添加时 默认为未选中
        //                }
        //                else
        //                {
        //                    sub.isSelected = false;
        //                    foreach (var tmp_item in SelectedAttribute)
        //                    {
        //                        if (tmp_item.ID == subitem.ID)
        //                        {
        //                            sub.isSelected = true;//已经选择的产品属性中存在 为选中
        //                            break;
        //                        }
        //                    }
        //                }
                        

        //                tmp.SubAttributeList.Add(sub);
        //            }

        //            cas.Add(tmp);
        //        }
        //    }
        //    return cas;
        //}
	}
}