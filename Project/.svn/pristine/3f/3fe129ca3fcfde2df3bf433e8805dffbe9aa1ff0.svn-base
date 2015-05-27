using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.User;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// 显示用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //UserListModel list = new UserListModel();
            //list.SearchInfo = new UserSearchInfo();
            //return View("Index", list);
            return SearchUser(null, null);
        }

        /// <summary>
        /// 用户列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchUser(UserSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new UserSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            UserListModel list = new UserListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息



            RecordCountEntity<User_InfoDC> rce = UserProxy.GetUserList(search.LoginName, search.MPNo, search.Email, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.UserList = rce.ReturnList;
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
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult EditUser(Guid uid)
        {
            UserModel oUserModel = new UserModel();

            //获取UserInfo
            ReturnEntity<User_InfoDC> re_UserInfo = UserProxy.GetUserInfo(uid);
            if (re_UserInfo != null)
            {
                if (re_UserInfo.Success)
                {
                    oUserModel.UserInfo = re_UserInfo.OBJ;
                }
                else
                {
                    //处理报错
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re_UserInfo.Message);
                }
            }
            else
            {
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }
            

            //获取UserDetail
            ReturnEntity<User_DetailDC> re_UserDetail = UserProxy.GetUserDetail(uid);
            if (re_UserDetail != null)
            {
                if (re_UserDetail.Success)
                {
                    oUserModel.UserDetailInfo = re_UserDetail.OBJ;
                }
                else
                {
                    //处理报错
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re_UserDetail.Message);
                }
            }
            else
            {
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            //获取UserConsigneeAddress
            ReturnEntity<IList<User_ConsigneeAddressDC>> re_UserConsigneeAddress = UserProxy.GetUserConsigneeAddressList(uid);
            if (re_UserConsigneeAddress != null)
            {
                if (re_UserConsigneeAddress.Success)
                {
                    oUserModel.User_ConsigneeAddressList = re_UserConsigneeAddress.OBJ;
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
            
            
            IList<Base_DistrictDC> District = BaseInfoProxy.Base_GetAllDistrict();
            if (District != null)
            {
                oUserModel.AllDistrictList = District;
            }
            else
            {
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            //城市下拉框
            SiteListSet();

            return View(oUserModel);
        }




        /// <summary>
        /// 显示用户积分日志列表
        /// </summary>
        /// <param name="MPNO"></param>
        /// <returns></returns>
        public ActionResult ScoreLogIndex(string MPNO)
        {
            UserScoreLogListModel list = new UserScoreLogListModel();
            list.SearchInfo = new UserScoreLogSearchInfo();
            list.SearchInfo.MPNo = MPNO;
            list.SearchInfo.DateFrom = System.DateTime.Now.Date.AddDays(-6);
            list.SearchInfo.DateTo = System.DateTime.Now.Date;
            return View("ScoreLogIndex", list);
        }

        /// <summary>
        /// 用户积分日志列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchUserScoreLog(UserScoreLogSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new UserScoreLogSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }             

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            UserScoreLogListModel list = new UserScoreLogListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息



            RecordCountEntity<User_ScoreLogDC> rce = UserProxy.GetUserScoreLogList(search.UserID, search.MPNo, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.UserScoreLogList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑  
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("ScoreLogIndex", list);
        }


        /// <summary>
        /// 显示用户余额日志列表
        /// </summary>
        /// <param name="MPNO"></param>
        /// <returns></returns>
        public ActionResult AmountLogIndex(string MPNO)
        {
            UserAmountLogListModel list = new UserAmountLogListModel();
            list.SearchInfo = new UserAmountLogSearchInfo();
            list.SearchInfo.MPNo = MPNO;
            list.SearchInfo.DateFrom = System.DateTime.Now.Date.AddDays(-6);
            list.SearchInfo.DateTo = System.DateTime.Now.Date;
            return View("AmountLogIndex", list);
        }

        /// <summary>
        /// 用户余额日志列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchUserAmountLog(UserAmountLogSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new UserAmountLogSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }
                

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            UserAmountLogListModel list = new UserAmountLogListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息



            RecordCountEntity<User_AmountLogDC> rce = UserProxy.GetUserAmountLogList(search.UserID, search.MPNo, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.UserScoreLogList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑  
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("AmountLogIndex", list);
        }

        /// <summary>
        /// 展示用优惠券列表页
        /// </summary>
        /// <param name="MPNo"></param>
        /// <returns></returns>
        public ActionResult CouponIndex(string MPNo)
        {
            UserCouponListModel list = new UserCouponListModel();
            list.SearchInfo = new UserCouponSearchInfo();
            list.SearchInfo.MPNo = MPNo;

            return View("UserCouponIndex", list);
        }

        /// <summary>
        /// 用户优惠券搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchUserCoupon(UserCouponSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new UserCouponSearchInfo();


            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            UserCouponListModel list = new UserCouponListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            

            RecordCountEntity<User_CouponDC> rce = UserProxy.GetUserCouponList(search.UserID, search.MPNo,search.CouponName, search.CouponStatus, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.UserCouponList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑  
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("UserCouponIndex", list);
        }




        /// <summary>
        /// 显示用户余额日志列表
        /// </summary>
        /// <param name="MPNO"></param>
        /// <returns></returns>
        public ActionResult SmsSendIndex(string MPNO)
        {
            UserSmsSendListModel list = new UserSmsSendListModel();
            list.SearchInfo = new UserSmsSendSearchInfo();
            list.SearchInfo.MPNo = MPNO;
            list.SearchInfo.DateFrom = System.DateTime.Now.Date.AddDays(-6);
            list.SearchInfo.DateTo = System.DateTime.Now.Date;
            return View("SmsSendIndex", list);
        }

        /// <summary>
        /// 用户短信下行列表查询
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchUserSmsSend(UserSmsSendSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new UserSmsSendSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }


            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            UserSmsSendListModel list = new UserSmsSendListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息



            RecordCountEntity<Base_SmsSendDC> rce = SmsProxy.GetUserSmsSendList(search.MPNo, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.UserSmsSendList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑  
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("SmsSendIndex", list);
        }

        /// <summary>
        /// 展示短信下行 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSmsSend(string mpno)
        {
            Base_SmsSendDC entity = new Base_SmsSendDC();
            if (!string.IsNullOrEmpty(mpno))
            {
                entity.Mobile = mpno;
            }      

            return View(entity);
        }

        /// <summary>
        /// 短信下行
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveSmsSend(Base_SmsSendDC entity)
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

            //赋值 修改操作人     
            entity.Obj_Muser = item.ID;

            if (entity.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.Obj_Cuser = item.ID;


                if (entity.RunTime <= System.DateTime.Now)
                {
                    entity.RunTime = System.DateTime.Now;
                }

                //新增
                ReturnEntity<bool> re = SmsProxy.AddUserSmsSend(entity.Mobile, entity.Content, entity.RunTime, WCF.Common.Contract.Enumerate.Sms_Priority.Normal, WCF.Common.Contract.Enumerate.Sms_Type.Normal, WCF.Common.Contract.Enumerate.Sms_Channel.YM, WCF.Common.Contract.Enumerate.Sms_Source.CustomerService, item.Name);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Sms;
                        logobj.LogContent = string.Format("[{0}]对用户[{1}]进行短信下行", item.Name, entity.Mobile);
                        OperatorProxy.OperateLog_Add(logobj);

                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = "短信下行成功";
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "usersmssendlist";
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





        public JsonResult EditUserLevel(Guid uid, int level, string user)
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

            User_InfoDC entity = new User_InfoDC();
            entity.ID = uid;
            entity.Level = level;

            ReturnEntity<bool> re = UserProxy.EditUserLevel(entity);
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
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.User;
                        logobj.LogContent = string.Format("[{0}]对用户[{1}]进行会员等级[{2}]修改", item.Name, user, level.ToString());
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("对用户[{0}]进行会员等级修改成功", user);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "userlist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = "用户ID发生错误，请联系管理员！";
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




        //public JsonResult CitySelect(string ID)
        //{
        //    //截取ID前2位
        //    string code1 = ID.Substring(0, 2);
        //    //获取二级数据
        //    IList<Base_DistrictDC> list = BaseInfoProxy.Base_GetDistrict_L2(code1);

        //    string[][] data = new string[list.Count][];
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        data[i] = new string[2]; 
        //        data[i][0] = list[i].ID.ToString();
        //        data[i][1] = list[i].Name;
        //    }
        //    return Json(data);

        //}

        //public ActionResult AreaSelect(string ID)
        //{
        //    //截取ID前2位
        //    string code1 = ID.Substring(0, 2);
        //    string code2 = ID.Substring(2, 2);

        //    //获取三级数据
        //    IList<Base_DistrictDC> list = BaseInfoProxy.Base_GetDistrict_L3(code1, code2);

        //    string[][] data = new string[list.Count][];
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        data[i] = new string[2];
        //        data[i][0] = list[i].ID.ToString();
        //        data[i][1] = list[i].Name;
        //    }
        //    return Json(data);

        //}


	}
}