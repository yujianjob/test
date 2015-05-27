using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Gift;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.File;


namespace LazyAtHome.Web.WebManage.Controllers
{
    public class GiftController : BaseController
    {
        /// <summary>
        /// 显示商城礼品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //return View();
            return SearchGift(null, null);
        }

        /// <summary>
        /// 商城礼品搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchGift(GiftSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new GiftSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            GiftListModel list = new GiftListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Type == -1)
            {
                search.Type = null;
            }
            if (search.Class == -1)
            {
                search.Class = null;
            }
            if (search.Site == -1)
            {
                search.Site = null;
            }

            RecordCountEntity<Mall_ProductDC> rce = GiftProxy.GetGiftList(search.Type, search.Class, search.GiftName, search.Site, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);
            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.GiftList = rce.ReturnList;
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
        /// 类型选择 展示类别
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public JsonResult TypeSelect(int typeid)
        {
            string[][] data = null;
            if (typeid == -1)
            {
                //是全部
                data = new string[1][];
                data[0] = new string[2];
                data[0][0] = "-1";
                data[0][1] = "全部";
            }
            else if (typeid == 1)
            {
                //是懒人卡
                data = new string[3][];             
                data[0] = new string[2];
                data[0][0] = "-1";
                data[0][1] = "全部";
                data[1] = new string[2];
                data[1][0] = "1";
                data[1][1] = "实物卡";
                data[2] = new string[2];
                data[2][0] = "2";
                data[2][1] = "电子卡";
            }
            else if (typeid == 99)
            {
                //暂定为其他
                data = new string[2][];            
                data[0] = new string[2];
                data[0][0] = "-1";
                data[0][1] = "全部";
                data[1] = new string[2];
                data[1][0] = "99";
                data[1][1] = "其他礼品";

            }

            return Json(data);
        }



        /// <summary>
        /// 展示商城礼品编辑页面
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult EditGift(int gid)
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


            Mall_ProductDC entity = null;
            if (gid == 0)
            {
                //说明是添加，实例化对象
                entity = new Mall_ProductDC();
                entity.SellBeginDate = System.DateTime.Now.Date;
                entity.SellEndDate = System.DateTime.Now.Date.AddYears(10);
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Mall_ProductDC> re = GiftProxy.GetGiftBYID(gid);
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

            //城市下拉框
            SiteListSet("City");

            return View("EditGift", entity);

        }

        /// <summary>
        /// 礼品保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveGift(Mall_ProductDC entity)
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

                ReturnEntity<bool> re = GiftProxy.AddGift(entity);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                        logobj.LogContent = string.Format("[{0}]新增名称为[{1}]礼品信息成功", item.Name, entity.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]礼品信息成功", entity.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "giftlist";
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
                ReturnEntity<bool> re = GiftProxy.UpdateGift(entity);
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
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]礼品信息成功", item.Name, entity.ID, entity.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]礼品信息成功", entity.ID, entity.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "giftlist";
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
                string FileName = CommonProxy.UpLoadFile((int)CodeSource.Common.ProjectType.Wash, fileInfoModel, "GiftImgPath");
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
	}
}