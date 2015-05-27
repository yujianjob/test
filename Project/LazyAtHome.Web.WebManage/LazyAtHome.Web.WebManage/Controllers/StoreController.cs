using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Store;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
//using LazyAtHome.WCF.Common.Contract.DataContract.File;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class StoreController : BaseController
    {
        /// <summary>
        /// 显示合作门店列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //StoreListModel list = new StoreListModel();
            //list.SearchInfo = new StoreSearchInfo();

            ////城市下拉框
            //SiteListSet();

            //return View("Index", list);

            return SearchStore(null, null);
        }

        /// <summary>
        /// 合作门店搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchStore(StoreSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new StoreSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            StoreListModel list = new StoreListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Site == -1)
            {
                search.Site = null;
            }

            RecordCountEntity<Wash_StoreDC> rce = StoreProxy.GetStoreList(search.StoreName, search.StoreCode, search.Site, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.StoreList = rce.ReturnList;
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
        /// 显示门店页面
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public ActionResult EditStore(int sid)
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

            StoreModel entity = null;
            if (sid == 0)
            {
                //说明是添加，实例化对象
                entity = new StoreModel();
                entity.StoreInfo = new Wash_StoreDC();
                
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_StoreDC> re = StoreProxy.GetStoreBYID(sid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new StoreModel();
                        entity.StoreInfo = re.OBJ;
                        entity.StoreOperatorList = StoreProxy.GetStoreOperatorListBYStoreID(sid);
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
            SiteListSet();

            return View("EditStore", entity);
        }

        public ActionResult EditStoreByGuid(Guid sid)
        {
            StoreModel entity = null;
            if (sid == Guid.Empty)
            {
                //说明是添加，实例化对象
                entity = new StoreModel();
                entity.StoreInfo = new Wash_StoreDC();

            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_StoreDC> re = StoreProxy.GetStoreBYID(sid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new StoreModel();
                        entity.StoreInfo = re.OBJ;
                        entity.StoreOperatorList = StoreProxy.GetStoreOperatorListBYStoreID(entity.StoreInfo.ID);
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
            SiteListSet();

            return View("EditStore", entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveStore(StoreModel entity, int BDVIdL3, FormCollection coll)
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


            entity.StoreInfo.DistrictID = BDVIdL3;


            //修改人ID
            entity.StoreInfo.Obj_Muser = item.ID;

            if (entity.StoreInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.StoreInfo.Obj_Cuser = item.ID;

                ReturnEntity<bool> re = StoreProxy.AddStore(entity.StoreInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Store; ;
                        logobj.LogContent = string.Format("[{0}]新增名称为[{1}]合作门店信息成功", item.Name, entity.StoreInfo.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]合作门店信息成功", entity.StoreInfo.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "storelist";
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
                ReturnEntity<bool> re = StoreProxy.UpdateStore(entity.StoreInfo);
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
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Store; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]产品信息成功", item.Name, entity.StoreInfo.ID, entity.StoreInfo.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]产品信息成功", entity.StoreInfo.ID, entity.StoreInfo.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "storelist";
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