using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.Web.SFSupport.Models.Order;
using LazyAtHome.Web.SFSupport.CodeSource.Proxy;

namespace LazyAtHome.Web.SFSupport.Controllers
{
    public class OrderController : BaseController
    {
        


        /// <summary>
        /// 订单信息展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return SearchOrder(null, null);
        }



        /// <summary>
        /// 订单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchOrder(OrderSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new OrderSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OrderListModel list = new OrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            if (search.ExpressStatus == -1)
            {
                search.ExpressStatus = null;
            }


            RecordCountEntity<Partner_Order_ExpressDC> rce = OrderProxy.GetOrderList(search.UserName, search.MPNo, search.ExpressStatus, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.OrderList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            return View("Index", list);
        }




        /// <summary>
        /// 历史订单信息展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult HisOrderIndex()
        {
            return SearchHisOrder(null, null);
        }

        /// <summary>
        /// 订单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchHisOrder(HisOrderSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new HisOrderSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            HisOrderListModel list = new HisOrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            DateTime begin = System.DateTime.Now.Date.AddDays(-6);
            DateTime end = System.DateTime.Now.Date.AddDays(1);

            RecordCountEntity<Partner_Order_ExpressDC> rce = OrderProxy.GetOrderList(search.UserName, search.MPNo, search.GetExpressNumber,begin, end, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.OrderList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            return View("HisOrderIndex", list);
        }


        /// <summary>
        /// 填写顺丰单号
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="sfnumber"></param>
        /// <returns></returns>
        public JsonResult UpdateSFNumber(int oid, string sfnumber)
        {
            CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

            //获取登录信息
            OperatorDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                item = new OperatorDC();
                item.ID = -1;
                item.Name = "虚拟顺丰操作员";

                ////界面返回信息
                //rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                //rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                //return Json(rjEntity);
            }

            if (string.IsNullOrEmpty(sfnumber))
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请填写快递单号";

                return Json(rjEntity);
            }

            ReturnEntity<bool> re = OrderProxy.FinishExpress(oid, sfnumber);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                        logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]填写取件快递单号[{2}]", item.Name, oid, sfnumber);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单ID为[{0}]填写取件快递单号成功", oid);
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "orderlist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "订单ID错误，请联系管理员！";
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
        /// 通知顺丰快递人员
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public JsonResult CallSF(int oid)
        {
            CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

            //获取登录信息
            OperatorDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                item = new OperatorDC();
                item.ID = -1;
                item.Name = "虚拟顺丰操作员";

                ////界面返回信息
                //rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                //rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                //return Json(rjEntity);
            }

            ReturnEntity<bool> re = OrderProxy.UPDATEExpressStatus(oid, 1);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                        logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行快递通知", item.Name, oid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单ID为[{0}]进行快递通知成功", oid);
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "orderlist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "订单ID错误，请联系管理员！";
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
	}
}