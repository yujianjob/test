using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Web.StoreManage.CodeSource.Proxy;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.StoreManage.Controllers
{
    public class OperatorController : BaseController
    {
        //
        // GET: /Operator/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 展示修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="currpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public JsonResult ResetPassword(string currpassword, string newpassword)
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

            //ResetPassword
            ReturnEntity<bool> re = OperatorProxy.ResetPassword(item.OperatorID, currpassword, newpassword, item.OperatorID);
            if (re.Success)
            {
                //此操作比较特殊，只需要判re.Success即可
                //成功 记录操作日志
                Wash_Store_OperatorLogDC logobj = new Wash_Store_OperatorLogDC();
                logobj.OperatorID = item.OperatorID;
                logobj.OperatorName = item.OperatorName;
                logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.StoreManager;
                logobj.LogContent = string.Format("[{0}]进行修改密码成功", item.OperatorName);
                OperatorProxy.OperateLog_Add(logobj);

                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.Success;
                rjEntity.message = string.Format("密码修改成功！");
                rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                //rjEntity.navTabId = "";

                CodeSource.SiteSession.InitPassword = false;

                //return View("Index","Home");
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