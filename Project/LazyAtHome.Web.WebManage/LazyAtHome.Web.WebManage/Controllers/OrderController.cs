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
using LazyAtHome.Web.WebManage.Models.Order;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using System.Text;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.Web.WebManage.Models.User;
using LazyAtHome.Web.WebManage.Models.BussinessCustomer;
using LazyAtHome.Web.WebManage.Models.Product;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class OrderController : BaseController
    {
        /// <summary>
        /// 显示订单列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //OrderListModel list = new OrderListModel();
            //list.SearchInfo = new OrderSearchInfo();


            ////城市下拉框
            //SiteListSet();

            //return View("Index", list);

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
            {
                search = new OrderSearchInfo();
                //search.OrderStatus = WCF.OrderSystem.Contract.Enumerate.OrderStatus.CSProcess;
                search.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                search.DateTo = search.DateFrom.Value.AddMonths(2).AddDays(-1);

            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OrderListModel list = new OrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Channel == 0)
            {
                search.Channel = null;
            }
            if (search.Site == -1)
            {
                search.Site = null;
            }
            if (search.OrderClass == null || (int)search.OrderClass == -1)
            {
                search.OrderClass = null;
            }
            if (search.OrderType == null || (int)search.OrderType == -1)
            {
                search.OrderType = null;
            }
            if (search.OrderStatus == null || (int)search.OrderStatus == -1)
            {
                search.OrderStatus = null;
            }
            if (search.Step == -1)
            {
                search.Step = null;
            }
            if (search.GetExpressType == -1)
            {
                search.GetExpressType = null;
            }

            RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetOrderList(search.OrderNumber, null, search.MPNo, null, search.OrderClass, search.OrderType, search.OrderStatus, search.Site, search.Channel, search.TotalAmountMax, search.TotalAmountMin, search.DateFrom, search.DateTo, search.pageNum, search.numPerPage, search.Consignee, search.SortType, search.GetDateFrom, search.GetDateTo, search.Step, search.GetExpressType);

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

            //城市下拉框
            SiteListSet();

            return View("Index", list);
        }


        /// <summary>
        /// 显示逾期订单列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpireOrderIndex()
        {
            return SearchExpireOrder(null, null);
        }

        public ActionResult SearchExpireOrder(ExpireOrderSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new ExpireOrderSearchInfo();
                search.Deadline = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            ExpireOrderListModel list = new ExpireOrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetExpireOrderList(null, search.Deadline.AddDays( -1 * search.DateCount), search.pageNum, search.numPerPage);

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


            return View("ExpireOrderIndex", list);
        }




        /// <summary>
        /// 显示今日订单列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult TodayOrderIndex()
        {
            return SearchTodayOrder(null, null);
        }

        /// <summary>
        /// 订单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchTodayOrder(TodayOrderSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new TodayOrderSearchInfo();
                //search.OrderStatus = WCF.OrderSystem.Contract.Enumerate.OrderStatus.CSProcess;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OrderListModel list = new OrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.Channel == 0)
            {
                search.Channel = null;
            }
            if (search.Site == -1)
            {
                search.Site = null;
            }
            if (search.OrderClass == null || (int)search.OrderClass == -1)
            {
                search.OrderClass = null;
            }
            if (search.OrderType == null || (int)search.OrderType == -1)
            {
                search.OrderType = null;
            }
            if (search.OrderStatus == null || (int)search.OrderStatus == -1)
            {
                search.OrderStatus = null;
            }
            if (search.Step == -1)
            {
                search.Step = null;
            }
            if (search.GetExpressType == -1)
            {
                search.GetExpressType = null;
            }

            RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetOrderList(search.OrderNumber, null, search.MPNo, null, search.OrderClass, search.OrderType, search.OrderStatus, search.Site, search.Channel, search.TotalAmountMax, search.TotalAmountMin, null, null, search.pageNum, search.numPerPage, search.Consignee, search.SortType, System.DateTime.Now.Date, System.DateTime.Now.Date.AddDays(1), search.Step, search.GetExpressType);

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

            //城市下拉框
            SiteListSet();

            return View("TodayOrderIndex", list);
        }





        /// <summary>
        /// 展示订单详情页
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="type">1：为订单页进来的 2：为逾期订单页进来的</param>
        /// <returns></returns>
        public ActionResult EditOrder(int oid, int type)
        {
            OrderModel entity = null;

            if (oid == 0)
            {
                //说明是添加，实例化对象
                //后台没有添加

            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Order_OrderDC> re = OrderProxy.GetOrderBYID(oid);

                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new OrderModel();
                        entity.OrderInfo = re.OBJ;

                        //entity.OrderInfo.Feedback = new Order_FeedbackDC();
                        //entity.OrderInfo.Feedback.Score1 = 1;
                        //entity.OrderInfo.Feedback.Score2 = 2;
                        //entity.OrderInfo.Feedback.Score3 = 3;
                        //entity.OrderInfo.Feedback.Score4 = 4;
                        //entity.OrderInfo.Feedback.Content1 = "123";

                        //RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetOrderList(null, entity.OrderInfo.UserID, null, null, null, null, null, null, null, null, null, null, null, 1, 10);
                        //if (rce != null)
                        //{
                        //    entity.HistroyOrderList = rce.ReturnList;
                        //}
                        //else
                        //{
                        //    //处理报错逻辑   
                        //    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "获取历史订单发生错误！");
                        //}
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

            ViewBag.Type = type;

            return View("EditOrder", entity);
        }



        public ActionResult EditOrderByOrderNumber(string onumber)
        {
            OrderModel entity = null;

            if (string.IsNullOrEmpty(onumber))
            {
                //说明是添加，实例化对象
                //后台没有添加

            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Order_OrderDC> re = OrderProxy.GetOrderBYOrderNumber(onumber);

                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new OrderModel();
                        entity.OrderInfo = re.OBJ;

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

            ViewBag.Type = 1;

            return View("EditOrder", entity);
        }


        /// <summary>
        /// 客服一键下单展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateOrder()
        {
            return View();
        }


        /// <summary>
        /// 客服一键下单展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateBussinessOrder()
        {
            return View();
        }



        /// <summary>
        /// 一键下单审核
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public JsonResult AuditOrder(int oid, bool flag)
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

            ReturnEntity<bool> re = OrderProxy.AuditOrder(oid, flag, item.ID);
            if (re != null)
            {
                if (re.Success)
                {
                    string type = string.Empty;
                    if (flag)
                    {
                        type = "审核通过";
                    }
                    else
                    {
                        type = "审核拒绝";
                    }

                    //此操作比较特殊，只需要判re.Success即可
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行审核成功，审核结果：" + type, item.Name, oid);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对订单ID为[{0}]进行审核成功，审核结果：" + type, oid);
                    rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "orderlist";
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
        /// 期望时间修改
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="expecttime"></param>
        /// <returns></returns>
        public JsonResult EditExpectTime(int oid, string expecttime)
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

            DateTime Expect;
            try
            {
                Expect = Convert.ToDateTime(expecttime);
            }
            catch (Exception)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "时间格式不正确";

                return Json(rjEntity);
            }
            

            ReturnEntity<bool> re = OrderProxy.EditExpectTime(oid, Expect);
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
                        logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行期望收件时间修改[{2}]", item.Name, oid, expecttime);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单ID为[{0}]进行补填取件快递单号成功", oid);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "orderlist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "订单ID发生错误，请联系管理员！";
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
        /// 取件物流类型修改
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="getexpresstype"></param>
        /// <returns></returns>
        public JsonResult EditGetExpressType(int oid, int getexpresstype)
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



            ReturnEntity<bool> re = OrderProxy.EditGetExpressType(oid, getexpresstype);
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
                        logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行取件物流类型修改[{2}]", item.Name, oid, getexpresstype);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单ID为[{0}]进行取件物流类型修改成功", oid);
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        //rjEntity.navTabId = "orderlist";
                        rjEntity.navTabId = "orderedit";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "订单ID发生错误，请联系管理员！";
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
        /// 订单中收件短信推送
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="remark"></param>
        /// <param name="smstype"></param>
        /// <param name="mpno"></param>
        /// <returns></returns>
        public JsonResult SendOrderSms(int oid, string remark, int smstype, string mpno)
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

            //WCF.UserSystem.Contract.Enumerate.LoginType type = WCF.UserSystem.Contract.Enumerate.LoginType.MPNo;

            System.Text.RegularExpressions.Regex rMobile = new System.Text.RegularExpressions.Regex("^1\\d{10}$", System.Text.RegularExpressions.RegexOptions.Compiled);

            if (!rMobile.IsMatch(mpno))
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "取件手机格式不正确！";

                return Json(rjEntity);
            }



            string content = string.Empty;
            switch (smstype)
            {
                case 1:
                    content = "尊敬的用户您好~请尽快将衣物顺丰到付我司，7天后如未收衣服，我们会为您取消订单，感谢您对懒到家的支持，谢谢~";
                    break;
                default:
                    content = "";
                    break;
            }

            if (string.IsNullOrEmpty(content))
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请选择推送类型！";

                return Json(rjEntity);
            }
            ReturnEntity<bool> re = SmsProxy.AddUserSmsSend(mpno, content, null, WCF.Common.Contract.Enumerate.Sms_Priority.Normal, WCF.Common.Contract.Enumerate.Sms_Type.Normal, WCF.Common.Contract.Enumerate.Sms_Channel.YM, WCF.Common.Contract.Enumerate.Sms_Source.CustomerService, item.Name);
            if (re != null)
            {
                if (re.Success)
                {
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Sms;
                    logobj.LogContent = string.Format("[{0}]对用户[{1}]进行短信下行", item.Name, mpno);
                    OperatorProxy.OperateLog_Add(logobj);

                    remark += " 客服短信推送，时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //成功 改备注
                    ReturnEntity<bool> re2 = OrderProxy.EditCSRemark(oid, remark);
                    if (re2 != null)
                    {
                        if (re2.Success)
                        {
                            if (re2.OBJ)
                            {
                                //成功 记录操作日志
                                OperatorLogDC logobj2 = new OperatorLogDC();
                                logobj2.OperatorID = item.ID;
                                logobj2.OperatorName = item.Name;
                                logobj2.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                                logobj2.LogContent = string.Format("[{0}]对订单ID为[{1}]进行客服备注修改", item.Name, oid);
                                OperatorProxy.OperateLog_Add(logobj2);

                                //界面返回信息
                                rjEntity.statusCode = CodeSource.StatusCode.Success;
                                rjEntity.message = string.Format("对订单ID为[{0}]进行推送短信成功", oid);
                                //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                                //rjEntity.navTabId = "orderlist";
                                rjEntity.navTabId = "orderedit";
                            }
                            else
                            {
                                //失败
                                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                                rjEntity.message = "订单ID发生错误，请联系管理员！";
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
        /// 补填取件快递单号
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="getexpressnumber"></param>
        /// <returns></returns>
        public JsonResult EditGetExpressNumber(int oid, string getexpressnumber)
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

            ReturnEntity<bool> re = OrderProxy.FinishExpress(oid, getexpressnumber);
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
                        logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行补填取件快递单号[{2}]", item.Name, oid, getexpressnumber);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单ID为[{0}]进行补填取件快递单号成功", oid);
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        //rjEntity.navTabId = "orderlist";
                        rjEntity.navTabId = "orderedit";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "订单ID发生错误，请联系管理员！";
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
        /// 修改客服备注
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public JsonResult EditCSRemark(int oid, string remark)
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

            ReturnEntity<bool> re = OrderProxy.EditCSRemark(oid, remark);
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
                        logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行客服备注修改", item.Name, oid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单ID为[{0}]进行客服备注修改成功", oid);
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        //rjEntity.navTabId = "orderlist";
                        rjEntity.navTabId = "orderedit";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "订单ID发生错误，请联系管理员！";
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
        /// 修改用户订单中取收件信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="OID"></param>
        /// <param name="Consignee"></param>
        /// <param name="DistrictID"></param>
        /// <param name="Address"></param>
        /// <param name="Mpno"></param>
        /// <param name="Phone"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public JsonResult EditAddress(int ID, int OID, string Consignee, int DistrictID, string Address, string Mpno, string Phone, int Flag)
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

            Order_ConsigneeAddressDC entity = new Order_ConsigneeAddressDC();
            entity.OID = OID;
            entity.ID = ID;
            entity.Consignee = Consignee;
            entity.DistrictID = DistrictID;
            entity.Address = Address;
            entity.Mpno = Mpno;
            entity.Phone = Phone;
            entity.Type = Flag;

            ReturnEntity<bool> re = OrderProxy.EditAddress(entity);
            if (re != null)
            {
                if (re.Success)
                {
                    if (re.OBJ)
                    {
                        string type = string.Empty;
                        if (Flag == 1)
                        {
                            type = "取送地址";//type = "取件地址";
                        }
                        else if (Flag == 2)
                        {
                            type = "收件地址";
                        }

                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                        logobj.LogContent = string.Format("[{0}]对地址ID为[{1}]进行" + type + "修改", item.Name, ID);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format(type + "修改成功");
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        //rjEntity.navTabId = "orderlist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "地址ID发生错误，请联系管理员！";
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


        public JsonResult CheckAddress(string address)
        {
            ReturnEntity<bool> re = ExpressProxy.CheckAddress(address);
            if (re != null)
            {
                if (re.Success && re.OBJ)
                {
                    return Json(new { code = 1 });
                }
                else
                {
                    return Json(new { code = 0 });
                }
            }
            else
            {
                //失败
                return Json(new { code = -1 });
            }

        }



        /// <summary>
        /// 退单
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="omunber"></param>
        /// <returns></returns>
        public JsonResult RepayOrder(int oid, string omunber)
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

            //进行退单 不需要处理对错
            //ReturnEntity<SF_OrderInfoDC> resf = OrderProxy.CancelSFOrder(omunber);
            
            ReturnEntity<bool> re = OrderProxy.RepayOrder(oid);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可

                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行退单", item.Name, oid);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对订单ID为[{0}]进行退单成功", oid);
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    //rjEntity.navTabId = "orderlist";
                    rjEntity.navTabId = "orderedit";
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
        /// 取消订单
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public JsonResult CancelOrder(int oid)
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

            ReturnEntity<bool> re = OrderProxy.CancelOrder(oid);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可

                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行取消订单", item.Name, oid);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对订单ID为[{0}]进行取消订单成功", oid);
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    //rjEntity.navTabId = "orderlist";
                    rjEntity.navTabId = "orderedit";
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
        /// 反洗
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public JsonResult WashAgain(int oid, string pidlist)
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

            //判断是否有选中属性
            if (string.IsNullOrEmpty(pidlist))
            {
                //没有选中，直接报错
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "请选择产品";
            }
            else
            {
                string[] strpids = pidlist.Split(',');
                IList<int> pids = new List<int>();
                foreach (string id in strpids)
                {
                    pids.Add(Convert.ToInt32(id));

                }

                ReturnEntity<bool> re = OrderProxy.WashAgain(oid, pids);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //此操作比较特殊，只需要判re.Success即可
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                        logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]的产品[{2}]进行反洗", item.Name, oid, pidlist);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单ID为[{0}]的产品[{1}]进行反洗成功", oid, pidlist);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "orderlist";
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
        /// 快递推送
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public JsonResult ReCreateExpressOrder(string ordernumber)
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

            ReturnEntity<bool> re = OrderProxy.ReCreateInternalExpressOrder(ordernumber);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可

                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对订单号为[{1}]进行物流推送", item.Name, ordernumber);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对订单号为[{0}]进行物流推送", ordernumber);
                    rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "orderlist";
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



        public JsonResult EditOrderStep(int oid, int step)
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

            ReturnEntity<bool> re = OrderProxy.EditOrderStep(oid, step);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可

                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行进程修改[{2}]", item.Name, oid, step);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对订单ID为[{0}]进行进程修改成功", oid);
                    rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "expireorderlist";//expireorderlist
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
        /// 根据订单号 查询顺丰订单
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public ActionResult SearchSFOrder(string ordernumber)
        {
            SF_OrderInfoDC entity = null;

            ReturnEntity<SF_OrderInfoDC> re = OrderProxy.GetSFOrderBYOrderNum(ordernumber);
            if (re != null)
            {
                if (re.Success)
                {
                    if (re.OBJ != null)
                    {
                        entity = re.OBJ;
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

            return View("EditSFOrder", entity);
        }


        /// <summary>
        /// 顺丰订单重新下单
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public ActionResult CreateSFOrder(int orderid, string ordernumber)
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


            SF_OrderInfoDC entity = null;
            Order_ConsigneeAddressDC ConsigneeAddress = null;

            ReturnEntity<Order_OrderDC> reorder = OrderProxy.GetOrderConsigneeAddressBYOrderID(orderid);
            if (reorder != null)
            {
                if (reorder.Success)
                {
                    if (reorder.OBJ != null && reorder.OBJ.GetAddress != null)
                    {
                        ConsigneeAddress = reorder.OBJ.GetAddress;

                        //重下顺丰订单
                        ReturnEntity<SF_OrderInfoDC> re = OrderProxy.CreateSFOrder(ordernumber, ConsigneeAddress, reorder.OBJ.ExpectTime);
                        if (re != null)
                        {
                            if (re.Success)
                            {
                                //成功 记录操作日志
                                OperatorLogDC logobj = new OperatorLogDC();
                                logobj.OperatorID = item.ID;
                                logobj.OperatorName = item.Name;
                                logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                                logobj.LogContent = string.Format("[{0}]对订单号为[{1}]进行顺丰重新下单", item.Name, ordernumber);
                                OperatorProxy.OperateLog_Add(logobj);


                                if (re.OBJ != null)
                                {
                                    //顺丰下单成功 快递单号写入订单
                                    ReturnEntity<bool> retmp = OrderProxy.FinishExpress(orderid, re.OBJ.mailno);

                                    if (retmp != null)
                                    {
                                        if (retmp.Success)
                                        {
                                            //只需要判断Success
                                            //成功 记录操作日志
                                            logobj = new OperatorLogDC();
                                            logobj.OperatorID = item.ID;
                                            logobj.OperatorName = item.Name;
                                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                                            logobj.LogContent = string.Format("[{0}]对订单号为[{1}]进行顺丰快递单号[{2}]写入", item.Name, ordernumber, re.OBJ.mailno);
                                            OperatorProxy.OperateLog_Add(logobj);



                                            entity = re.OBJ;

                                            //成功信息
                                            ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Correct, "快递下单成功！");
                                        }
                                        else
                                        {
                                            //处理报错逻辑   
                                            ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, retmp.Message);
                                        }
                                    }
                                    else
                                    {
                                        //处理报错逻辑
                                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "快递下单成功，写入快递单号发生错误！");
                                    }


                                    //entity = re.OBJ;
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
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    }
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, reorder.Message);
                }
            }
            else
            {
                //处理报错逻辑
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "获取收件人地址信息发生错误！");
            }


            return View("EditSFOrder", entity);
        }


        /// <summary>
        /// 顺丰订单撤单
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public ActionResult CancelSFOrder(string ordernumber)
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

            SF_OrderInfoDC entity = null;

            ReturnEntity<SF_OrderInfoDC> re = OrderProxy.CancelSFOrder(ordernumber);
            if (re != null)
            {
                if (re.Success)
                {
                    if (re.OBJ != null)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                        logobj.LogContent = string.Format("[{0}]对订单号为[{1}]进行顺丰撤单", item.Name, ordernumber);
                        OperatorProxy.OperateLog_Add(logobj);

                        entity = re.OBJ;
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

            return View("EditSFOrder", entity);
        }



        public ActionResult Refresh(int oid)
        {
            return EditOrder(oid, 1);
        }

        /// <summary>
        /// 个人订单搜索
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="MPNo"></param>
        /// <returns></returns>
        public ActionResult PrivateOrderIndex(Guid UserID, string MPNo, int PrimaryOrderId)
        {
            PrivateOrderSearchInfo search = new PrivateOrderSearchInfo();
            search.UserID = UserID;
            search.MPNo = MPNo;
            //search.pageNum = 1;
            return SearchPrivateOrder(search, 1, PrimaryOrderId);

        }

        /// <summary>
        /// 个人订单搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchPrivateOrder(PrivateOrderSearchInfo search, int? pageNum, int primaryOrderId)
        {
            if (search == null)
                search = new PrivateOrderSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            PrivateOrderListModel list = new PrivateOrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息
            list.PrimaryOrderID = primaryOrderId;

            //页面与接口数据处理
            if (search.Channel == 0)
            {
                search.Channel = null;
            }
            if (search.Site == -1)
            {
                search.Site = null;
            }
            if (search.OrderClass == null || (int)search.OrderClass == -1)
            {
                search.OrderClass = null;
            }
            if (search.OrderType == null || (int)search.OrderType == -1)
            {
                search.OrderType = null;
            }
            if (search.OrderStatus == null || (int)search.OrderStatus == -1)
            {
                search.OrderStatus = null;
            }

            RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetOrderList(search.OrderNumber, search.UserID, search.MPNo, null, search.OrderClass, search.OrderType, search.OrderStatus, search.Site, search.Channel, null, null, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

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

            //城市下拉框
            //SiteListSet();

            return View("PrivateOrderIndex", list);
        }


        /// <summary>
        /// 展示订单合并页
        /// </summary>
        /// <param name="primaryoid">主订单ID</param>
        /// <param name="slaveoid">副订单ID</param>
        /// <returns></returns>
        public ActionResult MergerOrderView(int primaryoid, int slaveoid)
        {
            MergerOrderModel entity = new MergerOrderModel();

            //获取主订单信息
            ReturnEntity<Order_OrderDC> rePrimary = OrderProxy.GetOrderBYID(primaryoid);

            if (rePrimary != null)
            {
                if (rePrimary.Success)
                {
                    entity.PrimaryOrder = rePrimary.OBJ;
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, rePrimary.Message);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            //获取主订单信息
            ReturnEntity<Order_OrderDC> reSlave = OrderProxy.GetOrderBYID(slaveoid);

            if (reSlave != null)
            {
                if (reSlave.Success)
                {
                    entity.SlaveOrder = reSlave.OBJ;
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, reSlave.Message);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            return View("MergerOrderView", entity);
        }


        /// <summary>
        /// 订单合并
        /// </summary>
        /// <param name="primaryoid"></param>
        /// <param name="slaveoid"></param>
        /// <returns></returns>
        public JsonResult MergerOrder(int primaryoid, int slaveoid)
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

            if (primaryoid == slaveoid)
            {
                //相同订单不能合并
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "相同订单不能合并！";

                return Json(rjEntity);
            }

            ReturnEntity<bool> re = OrderProxy.MergerOrder(primaryoid, slaveoid);
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
                        logobj.LogContent = string.Format("[{0}]进行订单合并主订单ID为[{1}]，副订单ID为[{2}]", item.Name, primaryoid, slaveoid);
                        OperatorProxy.OperateLog_Add(logobj);


                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对订单进行合并成功");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "orderedit";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "合并发生错误，请联系管理员！";
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
        /// 用户查询
        /// </summary>
        /// <param name="usermpno"></param>
        /// <returns></returns>
        public ActionResult SearchUser(string usermpno)
        {
            UserModel oUserModel = new UserModel();
            if (string.IsNullOrEmpty(usermpno))
            {
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "请填写用户手机！");
                return View("OneKeySubmit", oUserModel);
            }

            ReturnEntity<User_InfoDC> reUserInfo = UserProxy.RegByCustomerService(usermpno);
            if (reUserInfo != null)
            {
                if (reUserInfo.Success)
                {
                    if (reUserInfo.OBJ != null)
                    {
                        oUserModel.UserInfo = reUserInfo.OBJ;
                    }
                    else
                    {
                        //处理报错
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "获取用户信息发生错误！");
                    }
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, reUserInfo.Message);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            if (oUserModel.UserInfo != null)
            {
                //获取地址列表
                ReturnEntity<IList<User_ConsigneeAddressDC>> re_UserConsigneeAddress = UserProxy.GetUserConsigneeAddressList(oUserModel.UserInfo.ID);

                if (re_UserConsigneeAddress != null)
                {
                    if (re_UserConsigneeAddress.Success)
                    {
                        if (re_UserConsigneeAddress.OBJ != null)
                        {
                            oUserModel.User_ConsigneeAddressList = re_UserConsigneeAddress.OBJ;
                        }
                        else
                        {
                            //处理报错
                            ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "获取地址列表发生错误");
                        }
                    }
                    else
                    {
                        //处理报错
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re_UserConsigneeAddress.Message);
                    }
                }
                else
                {
                    //处理报错
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                }



                //获取优惠券列表
                RecordCountEntity<User_CouponDC> re_UserCoupon = UserProxy.GetUserCouponList(oUserModel.UserInfo.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
                if (re_UserCoupon != null)
                {
                    oUserModel.User_CouponList = re_UserCoupon.ReturnList;
                }
                else
                {
                //处理报错
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                }
            }


            return View("OneKeySubmit", oUserModel);




            
        }




        /// <summary>
        /// 运价搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchProduct(ProductForOrderSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ProductForOrderSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            ProductForOrderListModel list = new ProductForOrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理


            RecordCountEntity<LazyAtHome.WCF.Wash.Contract.DataContract.Wash_ProductDC> rce = ProductProxy.GetProductList(search.ProductName, null, null, 1, null, null, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ProductList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ProductIndex", list);
        }


        /// <summary>
        /// 订单改产品
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="pidlist"></param>
        /// <param name="ordertype"></param>
        /// <returns></returns>
        public JsonResult AdjustOrderProduct(int oid, string pidlist, int ordertype)
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

            IList<Order_ProductDC> ProductList = null;

            if (ordertype == 1)
            {
                //普通下单
                //判断是否有选中属性
                if (string.IsNullOrEmpty(pidlist))
                {
                    //普通下单 一定要选产品 报错
                    rjEntity.statusCode = CodeSource.StatusCode.Faild;
                    rjEntity.message = "请选择要调整的衣物";

                    return Json(rjEntity);
                }
                else
                {
                    string[] strpids = pidlist.Split(',');

                    ProductList = new List<Order_ProductDC>();

                    //IList<int> pids = new List<int>();
                    foreach (string id in strpids)
                    {
                        Order_ProductDC tmp = new Order_ProductDC();
                        tmp.ProductID = Convert.ToInt32(id);
                        tmp.Type = 1;
                        ProductList.Add(tmp);
                    }
                }
            }
            else if (ordertype == 2)
            {
                //一键下单
                //重置产品为初始化
                ProductList = new List<Order_ProductDC>();
            }
            else
            {
                //参数错误 报错
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = "ordertype参数错误";

                return Json(rjEntity);
            }


            ReturnEntity<bool> re = OrderProxy.EditOrderProduct(oid, ProductList, item.ID);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]的产品[{2}]进行调整衣物", item.Name, oid, pidlist);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对订单ID为[{0}]的产品[{1}]进行调整衣物", oid, pidlist);
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "orderedit";

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
        /// 订单产品洗涤类型更改
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="pid"></param>
        /// <param name="washstatus"></param>
        /// <returns></returns>
        public JsonResult EditOrderProductWashStatus(int oid, int pid, int washstatus)
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

            ReturnEntity<bool> re = OrderProxy.EditOrderProductWashStatus(oid, pid, washstatus, item.ID);
            if (re != null)
            {
                if (re.Success)
                {

                    string tmp = string.Empty;
                    if (washstatus == 1)
                    {
                        tmp = "设为工厂可以洗涤";
                    }
                    else if (washstatus == 2)
                    {
                        tmp = "设为工厂不可洗涤";
                    }

                    //此操作比较特殊，只需要判re.Success即可
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]，产品ID为[{2}]进行设置洗涤类型，" + tmp, item.Name, oid, pid);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对订单ID为[{0}]的产品ID为[{1}]进行设置洗涤类型", oid, pid);
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "orderedit";

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
        /// 客服一键下单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="consignee"></param>
        /// <param name="address"></param>
        /// <param name="mpno"></param>
        /// <param name="districtid"></param>
        /// <param name="couponcode"></param>
        /// <param name="couponid"></param>
        /// <param name="expecttime"></param>
        /// <param name="userremark"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult OneKeySubmit(Guid userid, string consignee, string address, string mpno, int districtid, string couponcode, int couponid, string expecttime, string userremark, int type)
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


            //优惠券
            int? UserCouponID = null;
            bool tmp = false;  //check绑定优惠券是否成功

            //是否填优惠券码
            if (!string.IsNullOrEmpty(couponcode))
            {
                //填写的话 进行绑定优惠券
                couponcode = couponcode.Replace("-", "").ToUpper();
                ReturnEntity<User_CouponDC> re_Bind = UserProxy.BindUserCoupon(userid, couponcode);
                if (re_Bind != null)
                {
                    if (re_Bind.Success)
                    {
                        if (re_Bind.OBJ != null)
                        {
                            //绑券成功
                            UserCouponID = re_Bind.OBJ.ID;
                            tmp = true;
                        }
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = re_Bind.Message;
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
                //没有填写 优惠券码 也视为绑券成功
                tmp = true;

                UserCouponID = couponid;
            }
            
            if (tmp)
            {
                //绑券成功 进行一键下单

                //取件时间
                DateTime? ExpectTime = null;
                if (!string.IsNullOrEmpty(expecttime))
                {
                    ExpectTime = Convert.ToDateTime(expecttime);
                }


                //取件对象
                Order_ConsigneeAddressDC GetAddress = new Order_ConsigneeAddressDC();
                GetAddress.Consignee = consignee;
                GetAddress.Address = address;
                GetAddress.Mpno = mpno;
                GetAddress.DistrictID = districtid;
                Order_ConsigneeAddressDC SendAddress = GetAddress;


                if (UserCouponID == 0)
                {
                    UserCouponID = null;
                }

                ReturnEntity<Order_OrderDC> re = OrderProxy.OnekeyOrder(userid, GetAddress, SendAddress, ExpectTime, userremark, UserCouponID, item.ID);
                if (re != null)
                {
                    if (re.Success)
                    {
                        if (re.OBJ != null)
                        {
                            if (type == 2)
                            {
                                //绑定地址
                                User_ConsigneeAddressDC useraddress = new User_ConsigneeAddressDC();
                                useraddress.Consignee = consignee;
                                useraddress.Address = address;
                                useraddress.MPNo = mpno;
                                useraddress.DistrictID = districtid;
                                useraddress.UserID = userid;
                                ReturnEntity<int> reTmp = UserProxy.AddUserConsigneeAddress(useraddress);
                            }



                            //成功 记录操作日志
                            OperatorLogDC logobj = new OperatorLogDC();
                            logobj.OperatorID = item.ID;
                            logobj.OperatorName = item.Name;
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                            logobj.LogContent = string.Format("[{0}]进行一键下单订单号为[{1}]，使用优惠券ID[{2}]", item.Name, re.OBJ.OrderNumber, UserCouponID == null ? "无" : UserCouponID.ToString());
                            OperatorProxy.OperateLog_Add(logobj);


                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("一键下单成功");
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "orderlist";
                        }
                        else
                        {
                            //失败
                            rjEntity.statusCode = CodeSource.StatusCode.Faild;
                            rjEntity.message = "一键下单发生错误，请联系管理员！";
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

        public JsonResult GetBCInfo(int BCType)
        {
            string Address = string.Empty;
            string Consignee = string.Empty;
            string Mpno = string.Empty;
            string ProductName = string.Empty;
            decimal Price = 0;


            if (BCType == 1)
            {
                Address = "企业地址";
                Consignee = "企业联系人";
                Mpno = "企业联系电话";
                ProductName = "衣物";
                Price = 5.0m;
            }

            return Json(new { Address = Address, Consignee = Consignee, Mpno = Mpno, ProductName = ProductName, Price = Price });
        }

        public JsonResult SaveBCustomerOrder(BCustomer entity)
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

            //取件对象
            Order_ConsigneeAddressDC GetAddress = new Order_ConsigneeAddressDC();
            GetAddress.Consignee = entity.BCConsignee;
            GetAddress.Address = entity.BCAddress;
            GetAddress.Mpno = entity.BCMpno;
            GetAddress.DistrictID = entity.BCDistrictID;
            Order_ConsigneeAddressDC SendAddress = GetAddress;

            ReturnEntity<bool> re = OrderProxy.CreateBusinessOrder(entity.BCID, GetAddress, SendAddress, entity.BCProductName, entity.BCPrice, entity.BCCount, entity.BCExpectTime, entity.BCRemark, item.ID);
            if (re != null)
            {
                if (re.Success)
                {
                    if (re.OBJ != null)
                    {

                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                        logobj.LogContent = string.Format("[{0}]进行企业下单，企业ID为[{1}]", item.Name, entity.BCID.ToString());
                        OperatorProxy.OperateLog_Add(logobj);


                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("企业下单成功");
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "orderlist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "企业下单发生错误，请联系管理员！";
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
        /// 水洗条码导出
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public ActionResult ExportCode(string StartDate, string EndDate)
        {
            //处理时间
            DateTime begin = Convert.ToDateTime(StartDate);
            DateTime end = Convert.ToDateTime(EndDate);

            //表格Title
            string ExTitle = begin.ToString("yy年MM月dd日HH时") + "至" + end.ToString("yy年MM月dd日HH时") + "水洗条码清单";

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;

            List<Order_Order_StatDC> listall = new List<Order_Order_StatDC>();
            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetOrderCodeReportList(begin, end, pageinfo.pageNum, pageinfo.numPerPage);
            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                : ((totalcount - pagesize) / pagesize + 1);
            for (int i = 2; i <= loopcount + 1; i++)
            {
                pageinfo.pageNum = i;
                RecordCountEntity<Order_Order_StatDC> rce2 = OrderProxy.GetOrderCodeReportList(begin, end, pageinfo.pageNum, pageinfo.numPerPage);
                if (rce2 != null)
                {
                    //totalcount = rce2.RecordCount;//设置查询总记录数
                    listall.AddRange(rce2.ReturnList);
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                }
            }

            #region 数据排版

            StringBuilder strRecord = new StringBuilder();
            strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
            strRecord.Append("<tr><td colspan=\"5\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");
            strRecord.Append("<tr>");
            strRecord.Append("<td align=\"center\"><strong>序号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>水洗条码</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>前三位</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>六到八位</strong></td>");
            strRecord.Append("</tr>");

            int index = 0;
            foreach (Order_Order_StatDC item in listall)
            {
                index++;

                strRecord.Append("<tr>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + index + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");

                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Code + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + ((item.Code.Length >= 3) ? item.Code.Substring(0, 3) : "") + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + ((item.Code.Length >= 8) ? item.Code.Substring(5, 3) : "") + "</td>");

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

            #endregion

        }


        public ActionResult ExportOrderList(string ordernum, string mpno, int? orderclass, int? ordertype, int? orderstatus, decimal? totalamountmin, decimal? totalamountmax, DateTime? begindate, DateTime? enddate, int? orderchannel, int? orderstep, int sorttype, string consignee, DateTime? getbegindate, DateTime? getenddate, int? getexpresstype)
        {
            //表格Title
            string ExTitle = "订单清单";

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;

            //页面与接口数据处理
            if (orderchannel == 0)
            {
                orderchannel = null;
            }
            WCF.OrderSystem.Contract.Enumerate.OrderClass? oClass = null; 
            if (orderclass != -1)
            {
                oClass = (WCF.OrderSystem.Contract.Enumerate.OrderClass)orderclass;
            }
            WCF.OrderSystem.Contract.Enumerate.OrderType? oType = null;
            if (ordertype != -1)
            {
                oType = (WCF.OrderSystem.Contract.Enumerate.OrderType)ordertype;
            }
            WCF.OrderSystem.Contract.Enumerate.OrderStatus? oStatus = null;
            if (orderstatus != -1)
            {
                oStatus = (WCF.OrderSystem.Contract.Enumerate.OrderStatus)orderstatus;
            }
            if (orderstep == -1)
            {
                orderstep = null;
            }
            if (string.IsNullOrEmpty(consignee))
            {
                consignee = null;
            }
            if (getexpresstype == -1)
            {
                getexpresstype = null;
            }

            List<Order_OrderDC> listall = new List<Order_OrderDC>();
            RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetOrderList(ordernum, null, mpno, null, oClass, oType, oStatus, null, orderchannel, totalamountmax, totalamountmin, begindate, enddate, pageinfo.pageNum, pageinfo.numPerPage, consignee, sorttype, getbegindate, getenddate, orderstep, getexpresstype);
            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            if (totalcount > pagesize)
            {
                loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                    : ((totalcount - pagesize) / pagesize + 1);
                for (int i = 2; i <= loopcount + 1; i++)
                {
                    pageinfo.pageNum = i;
                    RecordCountEntity<Order_OrderDC> rce2 = OrderProxy.GetOrderList(ordernum, null, mpno, null, oClass, oType, oStatus, null, orderchannel, totalamountmax, totalamountmin, begindate, enddate, pageinfo.pageNum, pageinfo.numPerPage, consignee, sorttype, getbegindate, getenddate, orderstep, getexpresstype);
                    if (rce2 != null)
                    {
                        //totalcount = rce2.RecordCount;//设置查询总记录数
                        listall.AddRange(rce2.ReturnList);
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    }
                }
            }

            #region 数据排版

            StringBuilder strRecord = new StringBuilder();
            strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
            strRecord.Append("<tr><td colspan=\"16\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");
            strRecord.Append("<tr>");
            strRecord.Append("<td align=\"center\"><strong>序号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单分类</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>取件手机</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单金额</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>支付状态</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>已付金额</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单状态</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>下单渠道</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>取件时间</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>下单时间</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单进程</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>代操作提示</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>客服备注</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>用户备注</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>用户信息</strong></td>");
            strRecord.Append("</tr>");

            int index = 0;
            foreach (Order_OrderDC item in listall)
            {
                index++;

                strRecord.Append("<tr>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + index + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");

                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + CodeSource.Common.Func.FormatOrderClass(item.OrderClass) + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.MPNo + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0.00\">" + item.TotalAmount + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + CodeSource.Common.Func.FormatPayStatus(item.PayStatus) + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0.00\">" + item.PayAmount + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + CodeSource.Common.Func.FormatOrderStatus(item.OrderStatus) + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + CodeSource.Common.Func.FormatRegistSource(item.Channel) + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.ExpectTime + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.Obj_Cdate + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + CodeSource.Common.Func.FormatOrderStep(item.Step) + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.CSSuggest + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.CSRemark + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.UserRemark + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Address + "[" + item.Consignee + "]" + "</td>");

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

            #endregion
        }








        public ActionResult OrderExportIndex()
        {
            return View();
        }

        public ActionResult ExportOrderDetail(DateTime? CreateStartDate, DateTime? CreateEndDate, DateTime? FinishStartDate, DateTime? FinishEndDate)
        {
            //表格Title
            string ExTitle = "订单清单";

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = 500;//CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;


            List<string[]> listall = new List<string[]>();
            IList<string[]> tmpList = new List<string[]>();

            do
            {
                tmpList = OrderProxy.OrderExport(null, CreateStartDate, FormatDateTimeAddOneDay(CreateEndDate), FinishStartDate, FormatDateTimeAddOneDay(FinishEndDate), pageinfo.pageNum, pageinfo.numPerPage);
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











        /// <summary>
        /// Excel导出(顺丰未处理订单)
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportOrder()
        {

            int pagesize = 20;//分页系数
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = pagesize;


            #region 数据排版
            StringBuilder strRecord = new StringBuilder();
            strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
            strRecord.Append("<tr><td colspan=\"2\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">顺丰未处理订单</td></tr>");
            strRecord.Append("<tr>");
            strRecord.Append("<td align=\"center\"><strong>订单号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>顺丰单号</strong></td>");
            strRecord.Append("</tr>");

            List<Order_OrderDC> listall = new List<Order_OrderDC>();
            RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetOrderList(null, null, null, null, null, null, null, null, null, null, null, null, null, pageinfo.pageNum, pageinfo.numPerPage);

            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }
            //List<ORD_OrderDC> listall = new List<ORD_OrderDC>();

            //listall = UserProxy.ORD_Order_GetList(null, MPNO, Status, StartDate, EndDate, pageinfo.PageSize,
            //        pageinfo.PageIndex, ref totalcount).ToList();

            loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                : ((totalcount - pagesize) / pagesize + 1);
            for (int i = 2; i <= loopcount + 1; i++)
            {
                pageinfo.pageNum = i;
                RecordCountEntity<Order_OrderDC> rce2 = OrderProxy.GetOrderList(null, null, null, null, null, null, null, null, null, null, null, null, null, pageinfo.pageNum, pageinfo.numPerPage);
                if (rce2 != null)
                {
                    //totalcount = rce2.RecordCount;//设置查询总记录数
                    listall.AddRange(rce2.ReturnList);
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                }
                
                
                //if (list != null && list.Count > 0)
                //{
                //    listall.AddRange(list);
                //}
            }

            foreach (var item in listall)
            {
                strRecord.Append("<tr>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.GetExpressNumber + "</td>");
                //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;\">" + item.TicketTitle + "</td>");
                //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;\">" + GetOrderStatus(item.OrderStatus) + "</td>");
                //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;\">" + (roleFlag ? item.SourceUserMobile : VeloUnion.WebManageMVC.CodeSource.Common.WebUtility.ChangeMobile(item.SourceUserMobile)) + "</td>");
                //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;\">" + (roleFlag ? item.TargetUserMobile : VeloUnion.WebManageMVC.CodeSource.Common.WebUtility.ChangeMobile(item.TargetUserMobile)) + "</td>");
                strRecord.Append("</tr>");
            }

            strRecord.Append("</table>");

            #endregion


            LoadExlData downdata = new LoadExlData
            {
                Title = "顺丰未处理订单",
                DataObject = strRecord.ToString()
            };
            if (downdata.OnLoadData())
                return Json(new { success = true, message = "/Aspx/ToExcel.aspx" });
            else
                return Json(new { success = false, message = string.Format("error") });

            //return View();
        }


        public ActionResult SFNumberImportIndex()
        {
            return View();
        }
	}
}