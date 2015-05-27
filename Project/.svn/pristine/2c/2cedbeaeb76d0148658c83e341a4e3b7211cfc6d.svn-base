using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Express;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using System.Text;



namespace LazyAtHome.Web.WebManage.Controllers
{
    public class ExpressController : BaseController
    {
        #region 物流

        public ActionResult NodeMap()
        {
            //string[,] aa = new string[2,3];
            ////aa[0] = {"121.4536865322", "31.3694940429", "莲花山路517弄"};
            //aa[0,0] = "121.4536865322";
            //aa[0,1] = "31.3694940429";
            //aa[0,2] = "莲花山路517弄";

            //aa[1,0] = "121.4403506779";
            //aa[1,1] = "31.3585278772";
            //aa[1,2] = "友邦大厦(东门)";

            //IList<ExpNodeMap> list = new List<ExpNodeMap>();
            //ExpNodeMap entity1 = new ExpNodeMap();
            //entity1.Longitude = "121.4536865322";
            //entity1.Latitude = "31.3694940429";
            //entity1.NodeName = "123莲花山路517弄";
            //list.Add(entity1);

            //ExpNodeMap entity2 = new ExpNodeMap();
            //entity2.Longitude = "121.4403506779";
            //entity2.Latitude = "31.3585278772";
            //entity2.NodeName = "123友邦大厦(东门)";
            //list.Add(entity2);

            //return View("NodeMap", list);
            ReturnEntity<IList<string>> re = ExpressProxy.GetNodeMap();

            IList<ExpNodeMap> list = new List<ExpNodeMap>();
            if (re != null && re.Success && re.OBJ != null)
            {
                foreach (string item in re.OBJ)
                {
                    ExpNodeMap entity = new ExpNodeMap();
                    string[] tmp = item.Split(',');
                    if (tmp.Length >= 4)
                    {
                        entity.Longitude = tmp[0];
                        entity.Latitude = tmp[1];
                        entity.Radius = tmp[2];
                        entity.NodeName = tmp[3];
                        entity.KeyWord = tmp[4];

                        list.Add(entity);
                    }
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }
            return View("NodeMap", list);
        }




        /// <summary>
        /// 物流订单列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpOrderIndex()
        {
            ExpOrderSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpOrder] as ExpOrderSearchInfo;
            if (search != null)
                return SearchExpOrder(search, search.pageNum);
            else
                return SearchExpOrder(null, null);

        }

        /// <summary>
        /// 物流订单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchExpOrder(ExpOrderSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ExpOrderSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpOrder] = search;
            //TempData[CodeSource.Common.ConstSearchVar.SearchExpOrder] = search;//缓存查询信息

            ExpOrderListModel list = new ExpOrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Step == -1)
            {
                search.Step = null;
            }
            if (search.TransportType == -1)
            {
                search.TransportType = null;
            }

            RecordCountEntity<Exp_OrderDC> rce = ExpressProxy.GetExpOrderList(search.ExpNumber, search.OutNumber, search.TransportType, search.Address, search.Contacts, search.Mpno, search.Step, search.Filter, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpOrderList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ExpOrderIndex", list);
        }


        /// <summary>
        /// 待处理物流订单列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult UnAllocationExpOrderIndex()
        {
            UnAllocationExpOrderSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchUnAllocationExpOrder] as UnAllocationExpOrderSearchInfo;
            if (search != null)
                return SearchUnAllocationExpOrder(search, search.pageNum);
            else
                return SearchUnAllocationExpOrder(null, null);

        }


        /// <summary>
        /// 物流订单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchUnAllocationExpOrder(UnAllocationExpOrderSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new UnAllocationExpOrderSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchUnAllocationExpOrder] = search;
            //TempData[CodeSource.Common.ConstSearchVar.SearchUnAllocationExpOrder] = search;//缓存查询信息

            UnAllocationExpOrderListModel list = new UnAllocationExpOrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Step == -1)
            {
                search.Step = null;
            }
            if (search.TransportType == -1)
            {
                search.TransportType = null;
            }

            RecordCountEntity<Exp_OrderDC> rce = ExpressProxy.GetExpOrderListUnAllocation(search.ExpNumber, search.OutNumber, search.TransportType, search.Address, search.Contacts, search.Mpno, search.Step, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpOrderList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("UnAllocationExpOrderIndex", list);
        }



        /// <summary>
        /// 从消息通知详情页 跳转到物流订单列表页
        /// </summary>
        /// <param name="onumber"></param>
        /// <returns></returns>
        public ActionResult ExpOrderIndexByNotify(string onumber)
        {
            ExpOrderSearchInfo search = new ExpOrderSearchInfo();
            search.OutNumber = onumber;
            return SearchExpOrder(search, 1);
        }


        /// <summary>
        /// 展示订单页
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult EditExpOrder(int oid, int type)
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

            ExpOrderModel entity = null;
            if (oid == 0)
            {
                //说明是添加，实例化对象 暂时不会有这类情况
                entity = new ExpOrderModel();
                entity.ExpOrderInfo = new Exp_OrderDC();
                
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Exp_OrderDC> re = ExpressProxy.GetExpOrderBYID(oid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ExpOrderModel();
                        entity.ExpOrderInfo = re.OBJ;
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
            entity.Type = type;
            return View("EditExpOrder", entity);
        }






        /// <summary>
        /// 物流区域列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpAreaIndex()
        {
            //ExpAreaSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpArea] as ExpAreaSearchInfo;
            //if (search != null)
            //    return SearchExpArea(search, null);
            //else
            //    return SearchExpArea(null, null);
            return SearchExpArea();
        }

        public ActionResult AreaView()
        {
            ReturnEntity<IList<Exp_AreaDC>> re = ExpressProxy.GetExpAreaList();
            IList<Exp_AreaDC> list = null;
            if (re != null)
            {
                list = re.OBJ;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("SearchExpArea", list);
        }


        /// <summary>
        /// 展示区域列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchExpArea()
        {
            ReturnEntity<IList<Exp_AreaDC>> re = ExpressProxy.GetExpAreaList();
            IList<Exp_AreaDC> list = null;
            if (re != null)
            {
                list = re.OBJ;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ExpAreaIndex", list);
        }

        public ActionResult EditExpArea(int aid)
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

            ExpAreaModel entity = null;
            if (aid == 0)
            {
                //说明是添加，实例化对象
                entity = new ExpAreaModel();
                entity.ExpAreaInfo = new Exp_AreaDC();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Exp_AreaDC> re = ExpressProxy.GetExpAreaBYID(aid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ExpAreaModel();
                        entity.ExpAreaInfo = re.OBJ;
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

            return View("EditExpArea", entity);
        }

        /// <summary>
        /// 编辑区域
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveExpArea(ExpAreaModel entity)
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
            entity.ExpAreaInfo.Obj_Muser = item.ID;

            if (entity.ExpAreaInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.ExpAreaInfo.Obj_Cuser = item.ID;

                ReturnEntity<int> re = ExpressProxy.AddExpArea(entity.ExpAreaInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]区域信息成功", item.Name, entity.ExpAreaInfo.ID, entity.ExpAreaInfo.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]区域信息成功", entity.ExpAreaInfo.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "exparealist";
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
                ReturnEntity<bool> re = ExpressProxy.UpdateExpArea(entity.ExpAreaInfo);
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
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]区域信息成功", item.Name, entity.ExpAreaInfo.ID, entity.ExpAreaInfo.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]区域信息成功", entity.ExpAreaInfo.ID, entity.ExpAreaInfo.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "exparealist";
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
        /// 物流存衣点列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpNodeIndex()
        {
            ExpNodeSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpNode] as ExpNodeSearchInfo;
            if (search != null)
                return SearchExpNode(search, null);
            else
                return SearchExpNode(null, null);
        }

        /// <summary>
        /// 展示存衣点列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchExpNode(ExpNodeSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new ExpNodeSearchInfo();
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            //search.DistrictID = BDVIdL3;

            //TempData[CodeSource.Common.ConstSearchVar.SearchExpNode] = search;//缓存查询信息
            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpNode] = search;

            ExpNodeListModel list = new ExpNodeListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            //if (search.DistrictID == -1)
            //{
            //    search.DistrictID = null;
            //}
            if (search.AreaID == -1)
            {
                search.AreaID = null;
            }
            if (search.Type == -1)
            {
                search.Type = null;
            }

            RecordCountEntity<Exp_NodeDC> rce = ExpressProxy.GetExpNodeList(search.AreaID, search.Name, search.Type, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpNodeList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            //处理区域下拉框
            AreaListSet();

            return View("ExpNodeIndex", list);


            //IList<Exp_NodeDC> list = ExpressProxy.GetExpNodeList();

            //if (list == null)
            //{
            //    list = new List<Exp_NodeDC>();

            //    //处理报错逻辑   
            //    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            //}

            //return View("ExpNodeIndex", list);
        }

        /// <summary>
        /// 展示存衣点搜索页
        /// </summary>
        /// <returns></returns>
        public ActionResult NodeView(int nodetype, int source)
        {
            ExpNodeSearchInfo search = new ExpNodeSearchInfo();
            if (source == 1)
            {
                //说明是从 Operator 来的
                if (nodetype == 1 || nodetype == 3)
                {
                    search.Type = 1;
                }
                else if (nodetype == 2)
                {
                    search.Type = 3;
                }
                else if (nodetype == 4)
                {
                    search.Type = 2;
                }
            }
            else if (source == 2)
            {
                //说明是从 站点 来的
                search.Type = nodetype + 1;
            }
            else
            {
                search.Type = nodetype;
            }

            search.Source = source;

            return SearchExpNodeForOrder(search, null);
            //return SearchExpNodeForOrder(null, null, -1, -1, -1);
        }

        /// <summary>
        /// 搜索存衣点列表页
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <param name="BDVIdL1"></param>
        /// <param name="BDVIdL2"></param>
        /// <param name="BDVIdL3"></param>
        /// <returns></returns>
        public ActionResult SearchExpNodeForOrder(ExpNodeSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new ExpNodeSearchInfo();
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            //search.DistrictID = BDVIdL3;

            TempData[CodeSource.Common.ConstSearchVar.SearchExpNode] = search;//缓存查询信息

            ExpNodeListModel list = new ExpNodeListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.DistrictID == -1)
            {
                search.DistrictID = null;
            }
            if (search.AreaID == -1)
            {
                search.AreaID = null;
            }
            if (search.Type == -1)
            {
                search.Type = null;
            }

            RecordCountEntity<Exp_NodeDC> rce = ExpressProxy.GetExpNodeList(search.AreaID, search.Name, search.Type, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpNodeList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            //处理区域下拉框
            AreaListSet();

            return View("SearchExpNode", list);


        }


        /// <summary>
        /// 存衣点展示页
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public ActionResult EditExpNode(int nid)
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

            ExpNodeModel entity = null;
            if (nid == 0)
            {
                //说明是添加，实例化对象
                entity = new ExpNodeModel();
                entity.ExpNodeInfo = new Exp_NodeDC();
                entity.ExpNodeInfo.AlarmType = 1;
                entity.ExpOperatorList = new List<OperatorDC>();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Exp_NodeDC> re = ExpressProxy.GetExpNodeBYID(nid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ExpNodeModel();
                        entity.ExpNodeInfo = re.OBJ;

                        //获取人员列表
                        RecordCountEntity<OperatorDC> rce = OperatorProxy.GetManagerList(null, null, null, null, null, null, 1, 50, re.OBJ.ID);
                        if (rce != null)
                        {
                            entity.ExpOperatorList = rce.ReturnList;
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

            }


            return View("EditExpNode", entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="BDVIdL3"></param>
        /// <returns></returns>
        public JsonResult SaveExpNode(ExpNodeModel entity, int BDVIdL3)
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

            if (BDVIdL3 == -1)
            {
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请选择行政区划！";

                return Json(rjEntity);
            }

            if ((entity.ExpNodeInfo.Type == 1 || entity.ExpNodeInfo.Type == 2) && entity.ExpNodeInfo.ParentID == 0)
            {
                //站点或者干线 一定要选所属站点
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请选择上级站点！";

                return Json(rjEntity);
            }



            //默认联系人id与负责人id一致
            entity.ExpNodeInfo.LinkManID = entity.ExpNodeInfo.ManagerID;
            //行政区化
            entity.ExpNodeInfo.DistrictID = BDVIdL3;
            //修改人ID
            entity.ExpNodeInfo.Obj_Muser = item.ID;

            if (entity.ExpNodeInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.ExpNodeInfo.Obj_Cuser = item.ID;

                ReturnEntity<int> re = ExpressProxy.AddExpNode(entity.ExpNodeInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]存衣点信息成功", item.Name, entity.ExpNodeInfo.ID, entity.ExpNodeInfo.Name);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]存衣点信息成功", entity.ExpNodeInfo.Name);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "expnodelist";
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
                ReturnEntity<bool> re = ExpressProxy.UpdateExpNode(entity.ExpNodeInfo);
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
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]存衣点信息成功", item.Name, entity.ExpNodeInfo.ID, entity.ExpNodeInfo.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]存衣点信息成功", entity.ExpNodeInfo.ID, entity.ExpNodeInfo.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "expnodelist";
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




        ///// <summary>
        ///// 快递员列表页展示
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult ExpOperatorIndex()
        //{
        //    return SearchExpOperator(null, null);
        //}



        ///// <summary>
        ///// 快递员展示页
        ///// </summary>
        ///// <param name="nid"></param>
        ///// <returns></returns>
        //public ActionResult EditExpOperator(int oid)
        //{
        //    CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

        //    //获取登录信息
        //    OperatorDC item = CodeSource.SiteSession.OperatorInfo;
        //    //未登录返回登陆页
        //    if (item == null)
        //    {
        //        //界面返回信息
        //        rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
        //        rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

        //        return Json(rjEntity, JsonRequestBehavior.AllowGet);
        //    }

        //    ExpOperatorModel entity = null;
        //    if (oid == 0)
        //    {
        //        //说明是添加，实例化对象
        //        entity = new ExpOperatorModel();
        //        entity.ExpOperatorInfo = new Exp_OperatorDC();
        //    }
        //    else
        //    {
        //        //说明是编辑，根据ID获取实体
        //        ReturnEntity<Exp_OperatorDC> re = ExpressProxy.GetExpOperatorBYID(oid);
        //        if (re != null)
        //        {
        //            if (re.Success)
        //            {
        //                entity = new ExpOperatorModel();
        //                entity.ExpOperatorInfo = re.OBJ;
        //            }
        //            else
        //            {
        //                //处理报错逻辑   
        //                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
        //            }
        //        }
        //        else
        //        {
        //            //处理报错逻辑   
        //            ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
        //        }

        //    }


        //    return View("EditExpOperator", entity);
        //}


        //public JsonResult SaveExpOperator(ExpOperatorModel entity)
        //{
        //    CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

        //    //获取登录信息
        //    OperatorDC item = CodeSource.SiteSession.OperatorInfo;
        //    //未登录返回登陆页
        //    if (item == null)
        //    {
        //        //界面返回信息
        //        rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
        //        rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

        //        return Json(rjEntity);
        //    }

        //    //修改人ID
        //    entity.ExpOperatorInfo.Obj_Muser = item.ID;

        //    if (entity.ExpOperatorInfo.ID == 0)
        //    {
        //        //说明是添加

        //        //添加人ID
        //        entity.ExpOperatorInfo.Obj_Cuser = item.ID;

        //        ReturnEntity<int> re = ExpressProxy.AddExpOperator(entity.ExpOperatorInfo);
        //        if (re != null)
        //        {
        //            if (re.Success)
        //            {
        //                //成功 记录操作日志
        //                OperatorLogDC logobj = new OperatorLogDC();
        //                logobj.OperatorID = item.ID;
        //                logobj.OperatorName = item.Name;
        //                logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
        //                logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]快递员信息成功", item.Name, entity.ExpOperatorInfo.ID, entity.ExpOperatorInfo.Name);
        //                OperatorProxy.OperateLog_Add(logobj);

        //                //界面返回信息
        //                rjEntity.statusCode = CodeSource.StatusCode.Success;
        //                rjEntity.message = string.Format("新增名称为[{0}]存衣点信息成功", entity.ExpOperatorInfo.Name);
        //                rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
        //                rjEntity.navTabId = "expoperatorlist";
        //            }
        //            else
        //            {
        //                //失败
        //                rjEntity.statusCode = CodeSource.StatusCode.Faild;
        //                rjEntity.message = re.Message;
        //            }
        //        }
        //        else
        //        {
        //            //失败
        //            rjEntity.statusCode = CodeSource.StatusCode.Faild;
        //            rjEntity.message = CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage;
        //        }
        //    }
        //    else
        //    {
        //        entity.ExpOperatorInfo.Enable = 1;
        //        //说明是编辑
        //        ReturnEntity<bool> re = ExpressProxy.UpdateExpOperator(entity.ExpOperatorInfo);
        //        if (re != null)
        //        {
        //            if (re.Success)
        //            {
        //                if (re.OBJ)
        //                {
        //                    //成功 记录操作日志
        //                    OperatorLogDC logobj = new OperatorLogDC();
        //                    logobj.OperatorID = item.ID;
        //                    logobj.OperatorName = item.Name;
        //                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
        //                    logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]快递员信息成功", item.Name, entity.ExpOperatorInfo.ID, entity.ExpOperatorInfo.Name);
        //                    OperatorProxy.OperateLog_Add(logobj);

        //                    //界面返回信息
        //                    rjEntity.statusCode = CodeSource.StatusCode.Success;
        //                    rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]快递员信息成功", entity.ExpOperatorInfo.ID, entity.ExpOperatorInfo.Name);
        //                    rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
        //                    rjEntity.navTabId = "expoperatorlist";
        //                }
        //                else
        //                {
        //                    rjEntity.statusCode = CodeSource.StatusCode.Faild;
        //                    rjEntity.message = CodeSource.Common.ConstConfig.WRONG_UpdateErrorMessage;
        //                }
        //            }
        //            else
        //            {
        //                //失败
        //                rjEntity.statusCode = CodeSource.StatusCode.Faild;
        //                rjEntity.message = re.Message;
        //            }
        //        }
        //        else
        //        {
        //            //失败
        //            rjEntity.statusCode = CodeSource.StatusCode.Faild;
        //            rjEntity.message = CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage;
        //        }
        //    }

        //    //}


        //    return Json(rjEntity);

        //}



        ///// <summary>
        ///// 快递员列表页搜索
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="pageNum"></param>
        ///// <returns></returns>
        //public ActionResult SearchExpOperator(ExpOperatorSearchInfo search, int? pageNum)
        //{
        //    if (search == null)
        //    {
        //        search = new ExpOperatorSearchInfo();
        //    }

        //    if (pageNum.HasValue)
        //        search.pageNum = pageNum.Value;//设置当前页

        //    TempData[CodeSource.Common.ConstSearchVar.SearchExpOperator] = search;//缓存查询信息

        //    ExpOperatorListModel list = new ExpOperatorListModel();
        //    list.SearchInfo = search;//用于在页面上保留查询信息

        //    //页面与接口数据处理
        //    if (search.Type == -1)
        //    {
        //        search.Type = null;
        //    }

        //    RecordCountEntity<Exp_OperatorDC> rce = ExpressProxy.GetExpOperatorList(search.LoginName, search.Name, search.Type,search.MpNo,search.NodeName, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

        //    if (rce != null)
        //    {
        //        search.RecCount = rce.RecordCount;//设置查询总记录数
        //        list.ExpOperatorList = rce.ReturnList;
        //    }
        //    else
        //    {
        //        //处理报错逻辑   
        //        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
        //    }

        //    return View("ExpOperatorIndex", list);

        //}


        /// <summary>
        /// 展示快递员搜索页
        /// </summary>
        /// <returns></returns>
        public ActionResult OperatorView(int? nodeid, int source)
        {
            ExpOperatorSearchInfo search = new ExpOperatorSearchInfo();
            //search.NodeName = nodename;
            search.NodeID = nodeid;
            search.Source = source;
            return SearchExpOperatorForOrder(search, null);
        }

        /// <summary>
        /// 搜索快递员列表页
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchExpOperatorForOrder(ExpOperatorSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new ExpOperatorSearchInfo();
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            TempData[CodeSource.Common.ConstSearchVar.SearchExpOperator] = search;//缓存查询信息

            ExpOperatorListModel list = new ExpOperatorListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            //if (search.Type == -1)
            //{
            //    search.Type = null;
            //}
            search.Type = 1;

            //RecordCountEntity<Exp_OperatorDC> rce = ExpressProxy.GetExpOperatorList(search.LoginName, search.Name, search.Type, search.MpNo, search.NodeName, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);
            RecordCountEntity<OperatorDC> rce = OperatorProxy.GetManagerList(search.Name, search.MpNo, null, null, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage, search.NodeID);
            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpOperatorList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("SearchExpOperator", list);
        }







        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="transporttype"></param>
        /// <param name="operatorid"></param>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public JsonResult AllocationExpOrder(int oid, int transporttype, int operatorid, int nodeid)
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

            //if (nodeid == 0)
            //{
            //    //界面返回信息
            //    rjEntity.statusCode = CodeSource.StatusCode.Faild;
            //    rjEntity.message = "请选择存衣点！";

            //    return Json(rjEntity);
            //}
            //if (operatorid == 0)
            //{
            //    //界面返回信息
            //    rjEntity.statusCode = CodeSource.StatusCode.Faild;
            //    rjEntity.message = "请选择快递员！";

            //    return Json(rjEntity);
            //}

            int? OperatorID = null;
            string mess = string.Empty;
            if (transporttype == 1)
            {
                OperatorID = operatorid;
                mess = "取件";
            }
            else if (transporttype == 2)
            {
                OperatorID = null;
                mess = "送件";
            }

            ReturnEntity<bool> re = ExpressProxy.AllocationExpOrder(oid, OperatorID, nodeid, item.ID);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]分配订单ID为[{1}]" + mess + "信息成功", item.Name, oid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("分配收件信息成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "unallocationexporderlist";
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

            return Json(rjEntity);
            
        }

        /// <summary>
        /// 强制分配
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="transporttype"></param>
        /// <param name="operatorid"></param>
        /// <param name="nodeid"></param>
        /// <param name="csremark"></param>
        /// <returns></returns>
        public JsonResult AllocationForcedExpOrder(int oid, int transporttype, int operatorid, int nodeid, string csremark)
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

            //if (nodeid == 0)
            //{
            //    //界面返回信息
            //    rjEntity.statusCode = CodeSource.StatusCode.Faild;
            //    rjEntity.message = "请选择存衣点！";

            //    return Json(rjEntity);
            //}
            //if (operatorid == 0)
            //{
            //    //界面返回信息
            //    rjEntity.statusCode = CodeSource.StatusCode.Faild;
            //    rjEntity.message = "请选择快递员！";

            //    return Json(rjEntity);
            //}

            int? OperatorID = null;
            string mess = string.Empty;
            if (transporttype == 1)
            {
                if (operatorid <= 0)
                {
                    OperatorID = null;
                }
                else
                {
                    OperatorID = operatorid;
                }
                mess = "取件";
            }
            else if (transporttype == 2)
            {
                OperatorID = null;
                mess = "送件";
            }

            ReturnEntity<bool> re = ExpressProxy.AllocationForcedExpOrder(oid, OperatorID, nodeid, item.ID, csremark);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]强制分配订单ID为[{1}]" + mess + "信息成功", item.Name, oid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("强制分配收件信息成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "exporderlist";
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

            return Json(rjEntity);

        }

        /// <summary>
        /// 重新自动分配(取消分配)
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="transporttype"></param>
        /// <param name="csremark"></param>
        /// <returns></returns>
        public JsonResult AllocationCancelExpOrder(int oid, int transporttype, string csremark)
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

            string mess = string.Empty;
            if (transporttype == 1)
            {
                mess = "取件";
            }
            else if (transporttype == 2)
            {
                mess = "送件";
            }

            ReturnEntity<bool> re = ExpressProxy.AllocationForcedExpOrder(oid, null, null, item.ID, csremark);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]重新自动分配订单ID为[{1}]" + mess + "信息成功", item.Name, oid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("重新自动分配收件信息成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "exporderlist";
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

            return Json(rjEntity);
        }

        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="contacts"></param>
        /// <param name="mpno"></param>
        /// <param name="districtid"></param>
        /// <param name="address"></param>
        /// <param name="packageinfo"></param>
        /// <param name="packagecount"></param>
        /// <param name="chargefee"></param>
        /// <param name="exptime"></param>
        /// <returns></returns>
        public JsonResult SaveExpOrder(int oid, string contacts, string mpno, int districtid, string address, string packageinfo, int packagecount, decimal chargefee, DateTime exptime)
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

            Exp_OrderDC entity = new Exp_OrderDC();
            entity.ID = oid;
            entity.Contacts = contacts;
            entity.Mpno = mpno;
            entity.DistrictID = districtid;
            entity.Address = address;
            entity.PackageInfo = packageinfo;
            entity.PackageCount = packagecount;
            entity.ChargeFee = chargefee;
            entity.ExpTime = exptime;
            //entity.CSRemark = csremark;
            //entity.UserRemark = userremark;

            ReturnEntity<bool> re = ExpressProxy.UpdateExpOrder(entity);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]修改订单ID为[{1}]客户信息成功", item.Name, oid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("修改客户信息成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "unallocationexporderlist";
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

            return Json(rjEntity);
        }


        /// <summary>
        /// 设定订单状态
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="step"></param>
        /// <param name="stepremark"></param>
        /// <returns></returns>
        public JsonResult SetStepExpOrder(int oid, int step, string stepremark)
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

            ReturnEntity<bool> re = ExpressProxy.SetStepExpOrder(oid, step, stepremark, item.ID);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]修改订单ID为[{1}]状态[{2}]成功", item.Name, oid, step);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("修改订单状态成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "exporderlist";
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

            return Json(rjEntity);
        }

        #endregion

        #region 仓储

        /// <summary>
        /// 仓库列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpStorageIndex()
        {
            ExpStorageSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpStorage] as ExpStorageSearchInfo;
            if (search != null)
                return SearchExpStorage(search, search.pageNum);
            else
                return SearchExpStorage(null, null);

        }

        /// <summary>
        /// 仓库搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchExpStorage(ExpStorageSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ExpStorageSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpStorage] = search;

            ExpStorageListModel list = new ExpStorageListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Type == -1)
            {
                search.Type = null;
            }


            RecordCountEntity<Exp_StorageDC> rce = ExpressProxy.GetExpStorageList(search.Type, search.Name, null, null, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpStorageList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ExpStorageIndex", list);
        }



        public ActionResult StorageView()
        {
            return SearchExpStorageForItem(null, null);
        }

        public ActionResult SearchExpStorageForItem(ExpStorageForItemSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ExpStorageForItemSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页


            ExpStorageForItemListModel list = new ExpStorageForItemListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Type == -1)
            {
                search.Type = null;
            }


            RecordCountEntity<Exp_StorageDC> rce = ExpressProxy.GetExpStorageList(search.Type, search.Name, null, null, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpStorageList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("SearchExpStorage", list);
        }

        

        /// <summary>
        /// 仓库一栏页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpStorageItemIndex()
        {
            ExpStorageItemListModel list = new ExpStorageItemListModel();
            list.SearchInfo = new ExpStorageItemSearchInfo();
            return View("ExpStorageItemIndex", list);
            //return SearchExpStorageItem(null, null);
        }

        /// <summary>
        /// 仓库一栏页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchExpStorageItemByStorage(int sid, string sname)
        {
            ExpStorageItemSearchInfo search = new ExpStorageItemSearchInfo();
            search.StorageID = sid;
            search.StorageName = HttpUtility.UrlDecode(sname);//sname;
            //string aa = HttpUtility.UrlDecode(sname);
            return SearchExpStorageItem(search, 1);
        }

        /// <summary>
        /// 仓库一栏搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchExpStorageItem(ExpStorageItemSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ExpStorageItemSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页


            ExpStorageItemListModel list = new ExpStorageItemListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //list.SearchInfo.StorageID = sid;

            //页面与接口数据处理
            if (search.ItemType == -1)
            {
                search.ItemType = null;
            }
            if (search.TargetType == -1)
            {
                search.TargetType = null;
            }
            RecordCountEntity<Exp_StorageItemDC> rce = ExpressProxy.GetExpStorageItemList(search.StorageID, search.Number, search.OtherNumber, search.ItemType, search.TargetType, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpStorageItemList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ExpStorageItemIndex", list);
        }



        /// <summary>
        /// 出入库日志页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpStorageLogIndex()
        {
            return SearchExpStorageLog(null, null);
        }

        public ActionResult SearchExpStorageLog(ExpStorageLogSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ExpStorageLogSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页


            ExpStorageLogListModel list = new ExpStorageLogListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Type == -1)
            {
                search.Type = null;
            }

            RecordCountEntity<Exp_StorageLogDC> rce = ExpressProxy.GetExpStorageLogList(null, search.Type, search.Number, search.OtherNumber, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpStorageLogList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ExpStorageLogIndex", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="siid"></param>
        /// <returns></returns>
        public ActionResult EditExpStorageItem(int siid)
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

            ExpStorageItemModel entity = null;
            if (siid > 0)
            {
                //根据ID获取实体
                ReturnEntity<Exp_StorageItemDC> re = ExpressProxy.GetExpStorageItem(siid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ExpStorageItemModel();
                        entity.StorageItemInfo = re.OBJ;
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
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "物品ID不合法，请联系管理员！");
            }
            


            return View("EditExpStorageItem", entity);
        }

        /// <summary>
        /// 强制分配物品所在地
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="sourceid"></param>
        /// <param name="targetid"></param>
        /// <param name="targetidtype"></param>
        /// <param name="itemtype"></param>
        /// <returns></returns>
        public JsonResult AdjustStorageItem(int itemid, int sourceid, int targetid, int targetidtype, int itemtype)
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

            int TargeType = 0;
            //根据选择的仓库类型 处理TargeType
            if (targetidtype == 0)
            {
                //选择的仓库是系统库
                TargeType = 0;
            }
            else if (targetidtype == 1)
            {
                //选择的仓库是站点库
                if (itemtype == 1)
                {
                    //包裹 送干线
                    TargeType = 2;
                }
                else if (itemtype == 2)
                {
                    //衣物 送用户
                    TargeType = 8;
                }
            }
            else if (targetidtype == 2)
            {
                //选择的仓库是干线库
                if (itemtype == 1)
                {
                    //包裹 送工厂
                    TargeType = 3;
                }
                else if (itemtype == 2)
                {
                    //衣物 送站点
                    TargeType = 1;
                }
            }
            else if (targetidtype == 3)
            {
                //选择的仓库是工厂库
                if (itemtype == 1)
                {
                    //包裹 送待分拣
                    TargeType = 4;
                }
                else if (itemtype == 2)
                {
                    //衣物 送洗
                    TargeType = 5;
                }
                   
            }

            ReturnEntity<int> re = ExpressProxy.EditExpStorageItemIO(itemid, sourceid, targetid, TargeType, true, item.ID);
            if (re != null)
            {
                if (re.Success)
                {
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Storage; ;
                    logobj.LogContent = string.Format("[{0}]强制修改物品所在地，物品ID[{1}] 目标库ID[{2}] TargeType[{3}]", item.Name, itemid, targetid, TargeType);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("强制修改物品所在地成功！");
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "expstorageitemedit";
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

        #endregion


        #region 对账

        /// <summary>
        /// 佣金账单列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpCommissionBillIndex()
        {
            ExpCommissionBillSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpCommissionBill] as ExpCommissionBillSearchInfo;
            if (search != null)
                return SearchExpCommissionBill(search, search.pageNum);
            else
                return SearchExpCommissionBill(null, null);

        }

        /// <summary>
        /// 佣金账单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchExpCommissionBill(ExpCommissionBillSearchInfo search, int? pageNum)
        {
            //临时处理权限
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

            if (item.RoleID != 16 && item.RoleID != 999)
            {
                //跳到错误页
                return View("AuthorityError");
            }

            if (search == null)
            {
                search = new ExpCommissionBillSearchInfo();
                //search.Period = System.DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            }   

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpCommissionBill] = search;
            //TempData[CodeSource.Common.ConstSearchVar.SearchExpOrder] = search;//缓存查询信息

            ExpCommissionBillListModel list = new ExpCommissionBillListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.BillStatus == -1)
            {
                search.BillStatus = null;
            }

            RecordCountEntity<Exp_Preson_CommissionBillDC> rce = ExpressProxy.GetExpCommissionBillList(search.Period, search.OperatorName, search.BillStatus, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpCommissionBillList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ExpCommissionBillIndex", list);
        }

        /// <summary>
        /// 佣金账单详情
        /// </summary>
        /// <param name="ecbid"></param>
        /// <returns></returns>
        public ActionResult EditExpCommissionBill(int ecbid)
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

            ExpCommissionBillModel entity = null;
            if (ecbid == 0)
            {
                //说明是添加，实例化对象
                entity = new ExpCommissionBillModel();
                entity.ExpCommissionBillInfo = new Exp_Preson_CommissionBillDC();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Exp_Preson_CommissionBillDC> re = ExpressProxy.GetExpCommissionBillBYID(ecbid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ExpCommissionBillModel();
                        entity.ExpCommissionBillInfo = re.OBJ;
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


            return View("EditCommissionBill", entity);
        }

        /// <summary>
        /// 佣金账单结算
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveCommissionBill(ExpCommissionBillModel entity)
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



            ReturnEntity<bool> re = ExpressProxy.CloseExpCommissionBill(entity.ExpCommissionBillInfo.ID, entity.ExpCommissionBillInfo.CurrentCommission, item.ID);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]结算佣金账单[{1}]成功，结算金额[{2}]", item.Name, entity.ExpCommissionBillInfo.ID, entity.ExpCommissionBillInfo.CurrentCommission);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("结算佣金账单成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "expcommissionbilllist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "结算金额不合法";//re.Message;
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
        /// 收款账单列表页展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpPaymentBillIndex()
        {
            ExpPaymentBillSearchInfo search = HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpPaymentBill] as ExpPaymentBillSearchInfo;
            if (search != null)
                return SearchExpPaymentBill(search, search.pageNum);
            else
                return SearchExpPaymentBill(null, null);

        }

        /// <summary>
        /// 收款账单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchExpPaymentBill(ExpPaymentBillSearchInfo search, int? pageNum)
        {
            //临时处理权限
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

            if (item.RoleID != 16 && item.RoleID != 999)
            {
                //跳到错误页
                return View("AuthorityError");
            }


            if (search == null)
            {
                search = new ExpPaymentBillSearchInfo();
                //search.Period = System.DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HttpContext.Session[CodeSource.Common.ConstSearchVar.SearchExpPaymentBill] = search;
            //TempData[CodeSource.Common.ConstSearchVar.SearchExpOrder] = search;//缓存查询信息

            ExpPaymentBillListModel list = new ExpPaymentBillListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.BillStatus == -1)
            {
                search.BillStatus = null;
            }

            RecordCountEntity<Exp_Preson_PaymentBillDC> rce = ExpressProxy.GetExpPaymentBillList(search.Period, search.OperatorName, search.BillStatus, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ExpPaymentBillList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ExpPaymentBillIndex", list);
        }



        /// <summary>
        /// 佣金账单详情
        /// </summary>
        /// <param name="ecbid"></param>
        /// <returns></returns>
        public ActionResult EditExpPaymentBill(int epbid)
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

            ExpPaymentBillModel entity = null;
            if (epbid == 0)
            {
                //说明是添加，实例化对象
                entity = new ExpPaymentBillModel();
                entity.ExpPaymentBillInfo = new Exp_Preson_PaymentBillDC();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Exp_Preson_PaymentBillDC> re = ExpressProxy.GetExpPaymentBillBYID(epbid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ExpPaymentBillModel();
                        entity.ExpPaymentBillInfo = re.OBJ;
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


            return View("EditPaymentBill", entity);
        }


        /// <summary>
        /// 收款账单结算
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SavePaymentBill(ExpPaymentBillModel entity)
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



            ReturnEntity<bool> re = ExpressProxy.CloseExpPaymentBill(entity.ExpPaymentBillInfo.ID, entity.ExpPaymentBillInfo.CurrentPayment, item.ID);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Express; ;
                        logobj.LogContent = string.Format("[{0}]结算收款账单[{1}]成功，结算金额[{2}]", item.Name, entity.ExpPaymentBillInfo.ID, entity.ExpPaymentBillInfo.CurrentPayment);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("结算收款账单成功！");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "exppaymentbilllist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "结算金额不合法";//re.Message;
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







        public ActionResult ExportCommissionBill(DateTime PeriodDate)
        {
            DateTime begin = GetWeekFirstDayMon(PeriodDate);
            DateTime end = GetWeekLastDaySun(PeriodDate);

            //表格Title
            string ExTitle = string.Format("佣金清单{0}至{1}", begin.ToString("yyyyMMdd"), end.ToString("yyyyMMdd"));

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = 500;//CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;


            List<string[]> listall = new List<string[]>();
            IList<string[]> tmpList = new List<string[]>();

            do
            {
                tmpList = ExpressProxy.CommissionBillExport(end, pageinfo.pageNum, pageinfo.numPerPage);
                if (tmpList != null)
                {
                    listall.AddRange(tmpList);
                }
                else
                {
                    //处理报错逻辑   
                    //ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    //break;
                    return Json(new { success = false, message = string.Format(CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage) });

                }
                pageinfo.pageNum++;
            }
            while (tmpList.Count >= pageinfo.numPerPage);

            #region 数据排版

            if (listall.Count > 0)
            {
                StringBuilder strRecord = new StringBuilder();
                strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
                strRecord.Append("<tr><td colspan=\"" + listall[0].Length + "\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");


                for (int i = 0; i < listall.Count; i++)
                {
                    strRecord.Append("<tr>");
                    foreach (string item in listall[i])
                    {
                        if (i == 0)
                        {
                            strRecord.Append("<td align=\"center\"><strong>" + item + "</strong></td>");
                        }
                        else
                        {
                            strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item + "</td>");
                        }
                    }
                    strRecord.Append("</tr>");
                }

                strRecord.Append("</table>");

                LoadExlData downdata = new LoadExlData
                {
                    Title = ExTitle,
                    DataObject = strRecord.ToString()
                };
                if (downdata.OnLoadData())
                    return Json(new { success = true, message = "../Aspx/ToExcel.aspx" });
                else
                    return Json(new { success = false, message = string.Format("error") });
            }
            else
            {
                return Json(new { success = false, message = string.Format("记录不存在") });
            }

            #endregion



        }


        public ActionResult ExportPaymentBill(DateTime PeriodDate)
        {
            DateTime begin = GetWeekFirstDayMon(PeriodDate);
            DateTime end = GetWeekLastDaySun(PeriodDate);

            //表格Title
            string ExTitle = string.Format("应收货款清单{0}至{1}", begin.ToString("yyyyMMdd"), end.ToString("yyyyMMdd"));

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = 500;//CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;


            List<string[]> listall = new List<string[]>();
            IList<string[]> tmpList = new List<string[]>();

            do
            {
                tmpList = ExpressProxy.PaymentBillExport(end, pageinfo.pageNum, pageinfo.numPerPage);
                if (tmpList != null)
                {
                    listall.AddRange(tmpList);
                }
                else
                {
                    //处理报错逻辑   
                    //ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    //break;
                    return Json(new { success = false, message = string.Format(CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage) });

                }
                pageinfo.pageNum++;
            }
            while (tmpList.Count >= pageinfo.numPerPage);

            #region 数据排版

            if (listall.Count > 0)
            {
                StringBuilder strRecord = new StringBuilder();
                strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
                strRecord.Append("<tr><td colspan=\"" + listall[0].Length + "\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");


                for (int i = 0; i < listall.Count; i++)
                {
                    strRecord.Append("<tr>");
                    foreach (string item in listall[i])
                    {
                        if (i == 0)
                        {
                            strRecord.Append("<td align=\"center\"><strong>" + item + "</strong></td>");
                        }
                        else
                        {
                            strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item + "</td>");
                        }
                    }
                    strRecord.Append("</tr>");
                }

                strRecord.Append("</table>");

                LoadExlData downdata = new LoadExlData
                {
                    Title = ExTitle,
                    DataObject = strRecord.ToString()
                };
                if (downdata.OnLoadData())
                    return Json(new { success = true, message = "../Aspx/ToExcel.aspx" });
                else
                    return Json(new { success = false, message = string.Format("error") });
            }
            else
            {
                return Json(new { success = false, message = string.Format("记录不存在") });
            }

            #endregion



        }









        #endregion
    }
}