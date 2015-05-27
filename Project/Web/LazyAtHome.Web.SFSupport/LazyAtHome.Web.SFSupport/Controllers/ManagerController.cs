using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using LazyAtHome.Web.SFSupport.Models.Manager;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.Web.SFSupport.CodeSource.Proxy;

namespace LazyAtHome.Web.SFSupport.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        //public ActionResult Index()
        //{
        //    return View();
        //}


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
            OperatorDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                return Json(rjEntity);
            }

            //ResetPassword
            ReturnEntity<bool> re = OperatorProxy.ResetPassword(item.ID, currpassword, newpassword, item.ID);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Manager;
                    logobj.LogContent = string.Format("[{0}]进行修改密码成功", item.Name);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("密码修改成功！");
                    rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    //rjEntity.navTabId = "";

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