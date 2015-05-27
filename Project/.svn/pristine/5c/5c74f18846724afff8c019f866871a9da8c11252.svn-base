using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.StoreManage.Models.User;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.Web.StoreManage.CodeSource.Proxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;

namespace LazyAtHome.Web.StoreManage.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户衣物列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserProductIndex()
        {

            UserProductListModel list = new UserProductListModel();
            list.SearchInfo = new UserProductSearchInfo();
            list.SearchInfo.DateFrom = System.DateTime.Now.Date.AddDays(-6);
            list.SearchInfo.DateTo = System.DateTime.Now.Date;

            return View("UserProductIndex", list);
        }

        /// <summary>
        /// 用户衣物搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchUserProduct(UserProductSearchInfo search, int? pageNum)
        {
            //获取登录信息
            Wash_StoreLoginInfoDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                return Json(rjEntity, JsonRequestBehavior.AllowGet);

            }

            if (search == null)
            {
                search = new UserProductSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            UserProductListModel list = new UserProductListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if ((int)search.UserSignType == -1)
            {
                search.UserSignType = null;
            }


            ReturnEntity<IList<Order_ProductDC>> re = UserProxy.GetProductListByUser(item.StoreID, search.UserMPNo, search.UserName, search.UserSignType, search.DateFrom, search.DateTo.AddDays(1));

            if (re != null && re.Success)
            {
                //search.RecCount = rce.RecordCount;//设置查询总记录数
                list.UserProductList = re.OBJ;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_SearchMessage);
            }

            return View("UserProductIndex", list);
        }

        /// <summary>
        /// 签收/撤销签收
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="signtype"></param>
        /// <returns></returns>
        public JsonResult ProductSign(int pid, int signtype)
        {
            CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

            //获取登录信息
            Wash_StoreLoginInfoDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                return Json(rjEntity);

            }

            UserSignType usersigntype = (UserSignType)(signtype ^ 1);
            //UpdateUserSignType
            ReturnEntity<bool> re = UserProxy.UpdateUserSignType(pid, usersigntype);
            if (re != null && re.Success)
            {
                if ((bool)re.OBJ)
                {
                    string message = string.Empty;
                    if (usersigntype == UserSignType.Sign)
                    {
                        message = "签收";
                    }
                    else if (usersigntype == UserSignType.UnSign)
                    {
                        message = "撤销签收";
                    }


                    //成功 记录操作日志
                    Wash_Store_OperatorLogDC logobj = new Wash_Store_OperatorLogDC();
                    logobj.OperatorID = item.OperatorID;
                    logobj.OperatorName = item.OperatorName;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.StoreUser;
                    logobj.LogContent = string.Format("[{0}]进行衣物" + message + "，衣物id为[{1}]", item.OperatorName, pid);
                    OperatorProxy.OperateLog_Add(logobj);




                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format(message + "成功！");
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    //rjEntity.navTabId = "createorder";
                }
                else
                {
                    //失败
                    rjEntity.statusCode = CodeSource.StatusCode.Faild;
                    rjEntity.message = "内部错误!";//re.Message;
                }
            }
            else
            {
                //失败
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = re.Message;
            }

            return Json(rjEntity);
        }
	}
}