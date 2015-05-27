﻿using LazyAtHome.WCF.Express.Contract.Enumerate;
using LazyAtHome.Web.API.App_Code;
using LazyAtHome.Web.API.App_Code.Proxy;
using LazyAtHome.Web.API.Models.ExpressMobileResultModels;
using LazyAtHome.Web.API.Models.JsonResultModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.API.Controllers
{
    public class ExpressMobileController : Controller
    {
        #region base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            System.Collections.Specialized.NameValueCollection _Params = null;
            if (filterContext.RequestContext.HttpContext.Request.RequestType == "GET")
                _Params = filterContext.RequestContext.HttpContext.Request.QueryString;
            else
                _Params = filterContext.RequestContext.HttpContext.Request.Form;

            //记录日志
            ParamsLog(filterContext.RequestContext.HttpContext.Request.Path + " " + filterContext.RequestContext.HttpContext.Request.RequestType, _Params.ToString());

            if (filterContext.RequestContext.HttpContext.Request.Path.Contains("/ExpressMobile/Version"))
            {
                return;
            }

            //检查参数
            if (!CheckParams(_Params))
            {
                filterContext.Result = MyJson(Models.JsonResultModels.BaseResult.ParametersError);
                return;
            }

            //检查vkey
            //if (!CheckSign(_Params))
            //{
            //    filterContext.Result = MyJson(Models.JsonResultModels.BaseResult.MD5Error);
            //    return;
            //}
        }

        //记录日志
        private void ParamsLog(string path, string logContent)
        {
            UtilityFunc.Add(path + ": " + logContent);
        }

        //检查参数
        private bool CheckParams(System.Collections.Specialized.NameValueCollection paramList)
        {
            var paramsKeys = paramList.AllKeys;
            if (!paramsKeys.Contains("vkey"))
            {
                return false;
            }

            return true;
        }

        //检查vkey
        private bool CheckSign(System.Collections.Specialized.NameValueCollection paramList)
        {
            var paramsKeys = paramList.AllKeys;

            var paramsvalue = string.Empty;
            var vkey = string.Empty;
            foreach (var item in paramsKeys)
            {
                if (item == null) continue;

                if (item.Contains("vkey"))
                {
                    vkey = paramList[item];
                    continue;
                }
                if (item.Contains("reason"))
                {
                    continue;
                }
                paramsvalue += paramList[item];
            }
            //第一步转大写
            paramsvalue = paramsvalue.ToUpper();
            //MD5
            paramsvalue = LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(paramsvalue).ToUpper();
            paramsvalue += WebApiConfig.Key;

            paramsvalue = LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(paramsvalue).ToUpper();

            if (paramsvalue != vkey)
            {
                UtilityFunc.Add("vkey计算失败:" + vkey + " 服务器计算值:" + paramsvalue);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected JsonResult MyJson(object obj)
        {
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //登录
        public JsonResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return MyJson(BaseResult.BadResult(-1, "登录名不能为空"));
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                return MyJson(BaseResult.BadResult(-1, "密码不能为空"));
            }

            try
            {
                //获取用户
                var userRtn = AppExpressProxy.PR_Operator_Login(username, password);
                if (userRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (userRtn.Success == false)
                {
                    if (userRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(userRtn.Code, userRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(userRtn.Code, userRtn.Message));
                    }
                }
                else if (userRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    var nodeRtn = AppExpressProxy.Exp_Node_SELECT_Entity(userRtn.OBJ.NodeID);
                    if (nodeRtn == null || nodeRtn.OBJ == null)
                    {
                        return MyJson(BaseResult.BadResult(-1, "帐号未设置归属站点."));
                    }

                    var user = new LoginResult.UserModel()
                    {
                        id = userRtn.OBJ.ID,
                        name = userRtn.OBJ.Name,
                        //type = userRtn.OBJ.Type == 3 ? 1 : 2,
                        mpno = userRtn.OBJ.MpNo,
                        nodeid = userRtn.OBJ.NodeID,
                        addr = nodeRtn.OBJ.Address,
                        nodename = userRtn.OBJ.NodeName,
                        nodeno = nodeRtn.OBJ.No,
                        reccode = userRtn.OBJ.Code,
                        type = 0,
                    };

                    if (WebApiConfig.ExpressMobile_Test)
                    {
                        user.name = "本地测试" + user.name;
                    }

                    if (userRtn.OBJ.NodeType == (int)LazyAtHome.WCF.Express.Contract.Enumerate.NodeType.Site)
                    {
                        user.type = 1;
                    }
                    else if (userRtn.OBJ.NodeType == (int)LazyAtHome.WCF.Express.Contract.Enumerate.NodeType.Line)
                    {
                        user.type = 2;

                        user.sitelist = new List<LazyAtHome.Web.API.Models.ExpressMobileResultModels.LoginResult.SiteModel>();

                        var nodeListRtn = AppExpressProxy.Exp_Node_LineSite(userRtn.OBJ.NodeID);
                        if (nodeListRtn != null && nodeListRtn.OBJ != null)
                        {
                            foreach (var item in nodeListRtn.OBJ)
                            {
                                user.sitelist.Add(new LoginResult.SiteModel()
                                {
                                    id = item.ID,
                                    name = item.Name,
                                });
                            }
                        }
                    }
                    else
                    {
                        return MyJson(BaseResult.BadResult(-1, "帐号未设置归属站点" + userRtn.OBJ.NodeType));
                    }

                    //生成对象
                    var rtn = new LoginResult()
                    {
                        user = user,
                    };

                    //返回
                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //上岗
        public JsonResult OperatorOnLine(int operatorid)
        {
            try
            {
                var dutyRtn = AppExpressProxy.PR_Operator_UPDATE_OnDuty(operatorid, 1);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //离岗
        public JsonResult OperatorOffLine(int operatorid)
        {
            try
            {
                var dutyRtn = AppExpressProxy.PR_Operator_UPDATE_OnDuty(operatorid, 0);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //待确认列表
        public JsonResult OrderWaitAccept(int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                OrderListResult rtn = new OrderListResult()
                {
                    list = new List<OrderModel>(),
                };
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "泰和路2038号",
                    mpno = "18221530985",
                    name = "徐文阳" + DateTime.Now.ToString(" MM-dd") + "14:00:00",
                    count = 0,
                });
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "西藏中路268号",
                    mpno = "13524622579",
                    name = "金魁" + DateTime.Now.ToString(" MM-dd") + "14:12:00",
                    count = 2,
                });
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "新会路379号",
                    mpno = "18612980478",
                    name = "蔡宇" + DateTime.Now.ToString(" MM-dd") + "16:02:00",
                    count = 5,
                });

                return MyJson(rtn);
            }

            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_SELECT_WaitAccept(operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else if (dutyRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    OrderListResult rtn = new OrderListResult()
                    {
                        list = new List<OrderModel>(),
                    };

                    foreach (var item in dutyRtn.OBJ)
                    {
                        rtn.list.Add(new OrderModel()
                        {
                            id = item.ID,
                            address = item.Address,
                            mpno = item.Mpno,
                            name = item.Contacts + item.ExpTime.ToString(" MM-dd HH:mm:ss"),
                            count = item.PackageCount,
                        });
                    }

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //接单
        public JsonResult OrderAccept(int orderid, int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                if (orderid == -1)
                {
                    return MyJson(BaseResult.Success);
                }
            }
            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_Accept(orderid, operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //拒接单
        public JsonResult OrderUnAccept(int orderid, int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                if (orderid == -1)
                {
                    return MyJson(BaseResult.Success);
                }
            }

            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_UnAccept(orderid, operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //待收件列表
        public JsonResult OrderWaitTakeList(int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                OrderListResult rtn = new OrderListResult()
                {
                    list = new List<OrderModel>(),
                };
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "泰和路2038号",
                    mpno = "18221530985",
                    name = "徐文阳" + DateTime.Now.ToString(" MM-dd") + "14:00:00",
                    count = 0,
                    linkstatus = 0,
                    remark = "",
                    wait = 0,
                });
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "西藏中路268号",
                    mpno = "13524622579",
                    name = "金魁" + DateTime.Now.ToString(" MM-dd") + "14:12:00",
                    count = 2,
                    linkstatus = 1,
                    remark = "",
                    wait = 0,
                });
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "新会路379号",
                    mpno = "18612980478",
                    name = "蔡宇" + DateTime.Now.ToString(" MM-dd") + "16:02:00",
                    count = 5,
                    linkstatus = 1,
                    remark = "",
                    wait = 1,
                });

                return MyJson(rtn);
            }

            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_SELECT_WaitTake(operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else if (dutyRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    OrderListResult rtn = new OrderListResult()
                    {
                        list = new List<OrderModel>(),
                    };

                    foreach (var item in dutyRtn.OBJ)
                    {
                        rtn.list.Add(new OrderModel()
                        {
                            id = item.ID,
                            address = item.Address,
                            mpno = item.Mpno,
                            name = item.Contacts + item.ExpTime.ToString(" MM-dd HH:mm:ss"),
                            count = item.PackageCount,
                            linkstatus = item.CallUserStatus,
                            remark = item.CSRemark,
                            wait = item.WaitProcess,
                        });
                    }

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //收件完成
        public JsonResult OrderTakeComplete(int orderid, string expnumber, int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                if (orderid == -1)
                {
                    return MyJson(BaseResult.Success);
                }
            }

            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_TakeComplete(orderid, expnumber, operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //收件失败
        public JsonResult OrderTakeFail(int orderid, int operatorid, string reason, string time)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                if (orderid == -1)
                {
                    return MyJson(BaseResult.Success);
                }
            }

            try
            {
                DateTime? dt = null;

                if (!string.IsNullOrWhiteSpace(time))
                {
                    dt = DateTime.ParseExact(time, "yyyyMMddHHmmss", null);
                }

                var dutyRtn = AppExpressProxy.Exp_Order_TakeFail(orderid, operatorid, reason, dt);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //扫描揽件
        public JsonResult OrderTakeSend(string[] expnumberlist, int operatorid)
        {
            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_TakeSend(expnumberlist, operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //待送件列表
        public JsonResult OrderWaitSendList(int operatorid)
        {

            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                OrderListResult rtn = new OrderListResult()
                {
                    list = new List<OrderModel>(),
                };
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "西藏南路555弄5号1702室",
                    mpno = "18221530985",
                    name = "沈忱" + DateTime.Now.ToString(" MM-dd") + "16:00:00",
                    count = 1,
                    linkstatus = 0,
                    remark = "",
                    wait = 0,
                    money = "18.00",
                    sendcount = 1,
                });
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "绥化路228弄3号401室",
                    mpno = "13524622579",
                    name = "张波" + DateTime.Now.ToString(" MM-dd") + "18:20:00",
                    count = 5,
                    linkstatus = 1,
                    remark = "",
                    wait = 0,
                    money = "0.00",
                    sendcount = 2,
                });
                rtn.list.Add(new OrderModel()
                {
                    id = -1,
                    address = "新会路379号",
                    mpno = "18612980478",
                    name = "蔡宇" + DateTime.Now.ToString(" MM-dd") + "20:30:00",
                    count = 5,
                    linkstatus = 1,
                    remark = "",
                    wait = 0,
                    money = "49.50",
                    sendcount = 5,
                });

                return MyJson(rtn);
            }


            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_SELECT_WaitSend(operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else if (dutyRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    OrderListResult rtn = new OrderListResult()
                    {
                        list = new List<OrderModel>(),
                    };

                    foreach (var item in dutyRtn.OBJ)
                    {
                        var tmpOrder = new OrderModel()
                         {
                             id = item.ID,
                             address = item.Address,
                             mpno = item.Mpno,
                             name = item.Contacts,
                             count = item.PackageCount,
                             expnumber = item.ExpNumber,

                             money = item.ChargeFee.ToString("0.00"),
                             linkstatus = 1,
                             remark = "",
                             sendcount = item.SendCount,
                         };

                        rtn.list.Add(tmpOrder);
                    }

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //待送件详情
        public JsonResult OrderWaitSendDetail(int orderid, int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                OrderDetailResult rtn = new OrderDetailResult()
                {
                    order = new OrderModel()
                    {
                        id = -1,
                        address = "新会路379号",
                        mpno = "18612980478",
                        name = "蔡宇" + DateTime.Now.ToString(" MM-dd") + "20:30:00",
                        count = 5,
                        expnumber = "0000000001",

                        money = "49.50",
                        linkstatus = 1,
                        remark = "",
                        sendcount = 5,
                    }
                };
                rtn.order.itemlist = new List<ProductModel>();

                rtn.order.itemlist.Add(new ProductModel()
                {
                    id = -1,
                    count = 1,
                    name = "羽绒服",
                });

                rtn.order.itemlist.Add(new ProductModel()
                {
                    id = -1,
                    count = 1,
                    name = "马甲",
                });
                rtn.order.itemlist.Add(new ProductModel()
                {
                    id = -1,
                    count = 1,
                    name = "衬衫",
                });
                rtn.order.itemlist.Add(new ProductModel()
                {
                    id = -1,
                    count = 1,
                    name = "棉衣",
                });
                rtn.order.itemlist.Add(new ProductModel()
                {
                    id = -1,
                    count = 1,
                    name = "大衣",
                });

                return MyJson(rtn);
            }

            try
            {
                var orderRtn = AppExpressProxy.Exp_Order_SELECT_WaitSend_Detail(orderid, operatorid);
                if (orderRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (orderRtn.Success == false)
                {
                    if (orderRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(orderRtn.Code, orderRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(orderRtn.Code, orderRtn.Message));
                    }
                }
                else if (orderRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    OrderDetailResult rtn = new OrderDetailResult()
                    {
                        order = new OrderModel()
                        {
                            id = orderRtn.OBJ.ID,
                            address = orderRtn.OBJ.Address,
                            mpno = orderRtn.OBJ.Mpno,
                            name = orderRtn.OBJ.Contacts,
                            count = orderRtn.OBJ.PackageCount,
                            expnumber = orderRtn.OBJ.ExpNumber,

                            money = orderRtn.OBJ.ChargeFee.ToString("0.00"),
                            linkstatus = orderRtn.OBJ.CallUserStatus,
                            remark = orderRtn.OBJ.CSRemark,
                            sendcount = orderRtn.OBJ.SendCount,
                        }
                    };

                    rtn.order.itemlist = new List<ProductModel>();

                    if (orderRtn.OBJ.StorageItemList != null)
                    {
                        foreach (var item in orderRtn.OBJ.StorageItemList)
                        {
                            rtn.order.itemlist.Add(new ProductModel()
                            {
                                id = item.ID,
                                count = 1,
                                name = item.ItemName,
                            });
                        }
                    }
                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //送件完成
        public JsonResult OrderSendComplete(int orderid, int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                if (orderid == -1)
                {
                    return MyJson(BaseResult.Success);
                }
            }

            try
            {
                var dutyRtn = AppExpressProxy.Exp_Order_SendComplete(orderid, operatorid);
                if (dutyRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (dutyRtn.Success == false)
                {
                    if (dutyRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(dutyRtn.Code, dutyRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(dutyRtn.Code, dutyRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //版本号
        public JsonResult Version(string version)
        {
            //return Content(@"[{"version":"1.0.1","minversion":"1.0.1","versioncode":2,"url":"http://newsyue.8866.org:85/base/Exp_android_download","code":0,"message":""}]");

            VersionResult rtn = new VersionResult();

            rtn.version = ConfigurationManager.AppSettings["ExpressMobile:Version"];

            rtn.minversion = ConfigurationManager.AppSettings["ExpressMobile:Version"];

            rtn.versioncode = int.Parse(ConfigurationManager.AppSettings["ExpressMobile:VersionCode"]);

            rtn.url = ConfigurationManager.AppSettings["ExpressMobile:DownloadUrl"];

            return MyJson(rtn);
        }

        #region 站点新增

        //站点入库信息
        public JsonResult GetLinePackageInfo(int operatorid)
        {
            try
            {
                var infoRtn = AppExpressProxy.Exp_Storage_SiteTakeLine_Count(operatorid);
                if (infoRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (infoRtn.Success == false)
                {
                    if (infoRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(infoRtn.Code, infoRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(infoRtn.Code, infoRtn.Message));
                    }
                }
                else if (string.IsNullOrWhiteSpace(infoRtn.OBJ))
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    PackageInfoResult rtn = new PackageInfoResult()
                    {
                        name = infoRtn.OBJ.Split('_')[0],
                        count = Convert.ToInt32(infoRtn.OBJ.Split('_')[1]),
                    };

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //站点入库
        public JsonResult GetLinePackage(string expnumber, int operatorid)
        {
            try
            {
                var getRtn = AppExpressProxy.Exp_Storage_SiteTakeLine(expnumber, operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.21	通话信息上传
        public JsonResult CallUserDetail(int orderid, string starttime, int second, int operatorid)
        {
            try
            {
                //if (second <= 5) return MyJson(BaseResult.Success);

                DateTime time = DateTime.ParseExact(starttime, "yyyy-MM-dd HH:mm:ss", null);

                AppExpressProxy.Exp_Order_UPDATE_CallUser(orderid, 1, time, second, operatorid);

                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.22	佣金信息
        public JsonResult OperMoney(int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                MoneyInfoResult rtn = new MoneyInfoResult()
                {
                    brokerage = "20.00",
                    payment = "150.00",
                };

                return MyJson(rtn);
            }

            try
            {
                var getRtn = AppExpressProxy.Exp_Preson_Account_SELECT_Entity_UserID(operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    MoneyInfoResult rtn = new MoneyInfoResult()
                    {
                        brokerage = "佣金已最终发放为准",
                        payment = "0",
                    };
                    if (getRtn.OBJ != null)
                    {
                        //rtn.brokerage = getRtn.OBJ.Commission.ToString("0.00");
                        rtn.payment = getRtn.OBJ.Payment.ToString("0.00");
                    }

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.22	佣金信息
        public JsonResult BrokerageList(int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                BrokerageListResult rtn = new BrokerageListResult()
                {
                    list = new List<LazyAtHome.Web.API.Models.ExpressMobileResultModels.BrokerageListResult.BrokerageModel>(),
                };

                var tmp = new BrokerageListResult.BrokerageModel()
                {
                    id = -1,
                    name = "响应速度奖励",//5分钟联系用户
                    money = "+￥" + " 1.00",
                    time = "11-21 13:55",//"11-21 13:55",
                };

                rtn.list.Add(tmp);

                var tmp1 = new BrokerageListResult.BrokerageModel()
               {
                   id = -1,
                   name = "收件小于10分钟",//5分钟联系用户
                   money = "+￥" + " 7.00",
                   time = "11-21 15:00",//"11-21 13:55",
               };

                rtn.list.Add(tmp1);

                var tmp2 = new BrokerageListResult.BrokerageModel()
               {
                   id = -1,
                   name = "新用户开发奖励",//5分钟联系用户
                   money = "+￥" + " 4.00",
                   time = "11-21 16:52",//"11-21 13:55",
               };

                rtn.list.Add(tmp2);

                var tmp3 = new BrokerageListResult.BrokerageModel()
               {
                   id = -1,
                   name = "当面下单提成",
                   money = "+￥" + " 8.00",
                   time = "11-21 13:50",//"11-21 13:55",
               };

                rtn.list.Add(tmp3);

                return MyJson(rtn);
            }

            try
            {
                var getRtn = AppExpressProxy.Exp_Preson_CommissionLog_SELECT_List(operatorid, 1, 20);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {

                    BrokerageListResult rtn = new BrokerageListResult()
                    {
                        list = new List<LazyAtHome.Web.API.Models.ExpressMobileResultModels.BrokerageListResult.BrokerageModel>(),
                    };

                    if (getRtn.OBJ != null && getRtn.OBJ.ReturnList != null)
                    {
                        foreach (var item in getRtn.OBJ.ReturnList)
                        {
                            var tmp = new BrokerageListResult.BrokerageModel()
                            {
                                id = item.ID,
                                name = "抢单",
                                money = "+￥2.00",
                                time = item.Obj_Cdate.Value.ToString("MM-dd HH:mm"),//"11-21 13:55",
                            };

                            switch (item.ChangeType)
                            {
                                case 1:
                                    tmp.name = "响应速度奖励";//5分钟联系用户
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 2://10分钟内取件
                                    tmp.name = "收件小于10分钟";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 3://30分钟内取件
                                    tmp.name = "收件小于30分钟";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 4://60分钟内取件
                                    tmp.name = "收件小于60分钟";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 5://新用户提成
                                    tmp.name = "新用户开发奖励";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 6://继续下单提成
                                    tmp.name = "当面下单奖励";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 7://收发订单
                                    tmp.name = "收发订单奖励";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 12:
                                case 13:
                                case 14:
                                    tmp.name = "收件速度提成";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 15:
                                    tmp.name = "新用户开发提成";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 16:
                                    tmp.name = "当面下单提成";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 17:
                                    tmp.name = "收发订单提成";
                                    tmp.money = "+￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                case 100:
                                    tmp.name = "账单结算";
                                    tmp.money = "-￥" + item.ChangeValue.ToString("0.00");
                                    break;
                                default:
                                    continue;
                            }
                            rtn.list.Add(tmp);
                        }
                    }
                    return MyJson(rtn);
                }

                //var getRtn = AppExpressProxy.Exp_Storage_SiteTakeLine(expnumber, operatorid);
                //if (getRtn == null)
                //{
                //    return MyJson(BaseResult.EmptyResult);
                //}
                //else if (getRtn.Success == false)
                //{
                //    if (getRtn.Code != -999)
                //    {
                //        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                //    }
                //    else
                //    {
                //        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                //    }
                //}
                //else
                //{
                //    return MyJson(BaseResult.Success);
                //}
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.24	继续下单
        public JsonResult ReCreateOrder(int orderid, int operatorid)
        {
            //测试环境单
            if (WebApiConfig.ExpressMobile_Test)
            {
                if (orderid == -1)
                {
                    return MyJson(BaseResult.Success);
                }
            }

            try
            {
                var order = AppExpressProxy.Exp_Order_SELECT_Entity(orderid);
                if (order == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "订单错误"));
                }

                var oper = AppExpressProxy.PR_Operator_SELECT_BYID(operatorid);
                if (oper == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "用户错误"));
                }

                var createRtn = AppExpressProxy.Order_Onekey_Create_Express(order.OBJ.OutNumber, oper.OBJ.Code);
                if (createRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (createRtn.Success == false)
                {
                    if (createRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(createRtn.Code, createRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(createRtn.Code, createRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        #endregion

        #region 干线用

        //4.12	站点送干线件信息
        public JsonResult Line_GetSitePackageInfo(int siteid, int operatorid)
        {
            try
            {
                var infoRtn = AppExpressProxy.Exp_Storage_LineTakeSite_Count(siteid, operatorid);
                if (infoRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (infoRtn.Success == false)
                {
                    if (infoRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(infoRtn.Code, infoRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(infoRtn.Code, infoRtn.Message));
                    }
                }
                else if (string.IsNullOrWhiteSpace(infoRtn.OBJ))
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    PackageInfoResult rtn = new PackageInfoResult()
                    {
                        name = infoRtn.OBJ.Split('_')[0],
                        count = Convert.ToInt32(infoRtn.OBJ.Split('_')[1]),
                    };

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.13	干线揽站点件
        public JsonResult Line_GetSitePackage(string expnumber, int siteid, int operatorid)
        {
            try
            {
                var getRtn = AppExpressProxy.Exp_Storage_LineTakeSite(expnumber, siteid, operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.14	干线入工厂库信息
        public JsonResult Line_SendFactoryPackageInfo(int operatorid)
        {
            try
            {
                var infoRtn = AppExpressProxy.Exp_Storage_FactoryTakeLine_Count(operatorid);
                if (infoRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (infoRtn.Success == false)
                {
                    if (infoRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(infoRtn.Code, infoRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(infoRtn.Code, infoRtn.Message));
                    }
                }
                else if (string.IsNullOrWhiteSpace(infoRtn.OBJ))
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    PackageInfoResult rtn = new PackageInfoResult()
                    {
                        name = infoRtn.OBJ.Split('_')[0],
                        count = Convert.ToInt32(infoRtn.OBJ.Split('_')[1]),
                    };

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.15	干线入工厂库完成
        public JsonResult Line_SendFactoryPackageComplete(int operatorid)
        {
            try
            {
                var getRtn = AppExpressProxy.Exp_Storage_FactoryTakeLine_Complete(operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.16	干线取工厂件信息
        public JsonResult Line_GetFactoryPackageInfo(int operatorid)
        {
            try
            {
                var infoRtn = AppExpressProxy.Exp_Storage_LineTakeFactory_Count(operatorid);
                if (infoRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (infoRtn.Success == false)
                {
                    if (infoRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(infoRtn.Code, infoRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(infoRtn.Code, infoRtn.Message));
                    }
                }
                else if (string.IsNullOrWhiteSpace(infoRtn.OBJ))
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else
                {
                    PackageInfoResult rtn = new PackageInfoResult()
                    {
                        name = infoRtn.OBJ.Split('_')[0],
                        count = Convert.ToInt32(infoRtn.OBJ.Split('_')[1]),
                    };

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //4.17	干线取工厂信息
        public JsonResult Line_GetFactoryPackage(string expnumber, int operatorid)
        {
            try
            {
                var getRtn = AppExpressProxy.Exp_Storage_LineTakeFactory(expnumber, operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        #endregion

        #region 失败消息

        //站点入库异常
        public JsonResult GetLinePackage_FailNotify(int operatorid)
        {
            try
            {
                var getRtn = AppExpressProxy.Exp_Storage_SiteTakeLine_FailNotify(operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //干线取站点件异常
        public JsonResult Line_GetSitePackage_FailNotify(int siteid, int operatorid)
        {
            try
            {
                var getRtn = AppExpressProxy.Exp_Storage_LineTakeSite_FailNotify(siteid, operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //干线取工厂件异常
        public JsonResult Line_GetFactoryPackage_FailNotify(int operatorid)
        {
            try
            {
                var getRtn = AppExpressProxy.Exp_Storage_LineTakeFactory_FailNotify(operatorid);
                if (getRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (getRtn.Success == false)
                {
                    if (getRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(getRtn.Code, getRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(getRtn.Code, getRtn.Message));
                    }
                }
                else
                {
                    return MyJson(BaseResult.Success);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        #endregion

        #region 日志记录

        const int pageSize = 50;

        //站点收件日志
        public JsonResult OrderSiteTakeUserLog(int operatorid, int type, string startdate, string enddate, int page)
        {
            if (string.IsNullOrWhiteSpace(startdate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (string.IsNullOrWhiteSpace(enddate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (page <= 0)
            {
                return MyJson(BaseResult.BadResult(-1, "页数错误"));
            }
            try
            {
                DateTime dstart = DateTime.Now;
                DateTime dend = DateTime.Now;

                dstart = DateTime.ParseExact(startdate, "yyyyMMddHHmmss", null);
                dend = DateTime.ParseExact(enddate, "yyyyMMddHHmmss", null).AddDays(1);

                var logRtn = AppExpressProxy.Exp_Order_SELECT_SiteTakeUser_Log(operatorid, type, dstart, dend, page, pageSize);
                if (logRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (logRtn.Success == false)
                {
                    if (logRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(logRtn.Code, logRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(logRtn.Code, logRtn.Message));
                    }
                }
                else
                {
                    OrderLogListResult rtn = new OrderLogListResult()
                    {
                        list = new List<OrderModel>(),
                        page = page,
                        totalcount = logRtn.OBJ.RecordCount,
                    };
                    if (rtn.totalcount <= 0)
                    {
                        rtn.totalpage = 1;
                    }
                    else if (rtn.totalcount % pageSize == 0)
                    {
                        rtn.totalpage = rtn.totalcount / pageSize;
                    }
                    else
                    {
                        rtn.totalpage = rtn.totalcount / pageSize + 1;
                    }

                    foreach (var item in logRtn.OBJ.ReturnList)
                    {
                        if (item == null) continue;

                        var orderTmp = new OrderModel()
                        {
                            id = item.ID,
                            address = item.Address,
                            mpno = item.Mpno,
                            name = item.Contacts, // + item.ExpTime.ToString("MM-dd HH:mm:ss"),
                            count = item.PackageCount,
                            expnumber = item.ExpNumber,
                            money = "",
                            linkstatus = 0,
                            remark = "",
                            sendcount = 0,
                            status = "",//状态
                        };

                        if (item.StorageItemList != null)
                        {
                            foreach (var storageItem in item.StorageItemList)
                            {
                                if (storageItem.TargetType == (int)StorageTargetType.ToLine)
                                {
                                    orderTmp.status = "在库中";
                                }
                                else
                                {
                                    orderTmp.status = "已送干线";
                                }
                                break;
                            }
                        }

                        rtn.list.Add(orderTmp);
                    }

                    //rtn.totalpage = 5;
                    //rtn.totalcount = 500;
                    //rtn.list.Add(new OrderModel()
                    //{
                    //    id = -1,
                    //    address = "新会路379号",
                    //    mpno = "18612980478",
                    //    name = "蔡宇" + DateTime.Now.ToString("MM-dd ") + "20:30:00",
                    //    count = 5,
                    //    expnumber = "0000000001",

                    //    money = "49.50",
                    //    linkstatus = 1,
                    //    remark = "",
                    //    sendcount = 5,
                    //    status = "状态",

                    //});

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //站点取干线日志
        public JsonResult OrderSiteTakeLineLog(int operatorid, int type, string startdate, string enddate, int page)
        {
            if (string.IsNullOrWhiteSpace(startdate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (string.IsNullOrWhiteSpace(enddate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (page <= 0)
            {
                return MyJson(BaseResult.BadResult(-1, "页数错误"));
            }
            try
            {
                DateTime dstart = DateTime.Now;
                DateTime dend = DateTime.Now;

                dstart = DateTime.ParseExact(startdate, "yyyyMMddHHmmss", null);
                dend = DateTime.ParseExact(enddate, "yyyyMMddHHmmss", null).AddDays(1);

                var logRtn = AppExpressProxy.Exp_Order_SELECT_SiteTakeLine_Log(operatorid, type, dstart, dend, page, pageSize);
                if (logRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (logRtn.Success == false)
                {
                    if (logRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(logRtn.Code, logRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(logRtn.Code, logRtn.Message));
                    }
                }
                else
                {
                    OrderLogListResult rtn = new OrderLogListResult()
                    {
                        list = new List<OrderModel>(),
                        page = page,
                        totalcount = logRtn.OBJ.RecordCount,
                        // totalpage = logRtn.OBJ.
                    };
                    if (rtn.totalcount <= 0)
                    {
                        rtn.totalpage = 1;
                    }
                    else if (rtn.totalcount % pageSize == 0)
                    {
                        rtn.totalpage = rtn.totalcount / pageSize;
                    }
                    else
                    {
                        rtn.totalpage = rtn.totalcount / pageSize + 1;
                    }

                    foreach (var item in logRtn.OBJ.ReturnList)
                    {
                        if (item == null) continue;


                        var orderTmp = new OrderModel()
                        {
                            id = item.ID,
                            address = item.Address,
                            mpno = item.Mpno,
                            name = item.Contacts, //      item.ExpTime.ToString("MM-dd HH:mm:ss"),
                            count = item.PackageCount,
                            expnumber = item.ExpNumber,
                            money = item.ChargeFee.ToString("0.00"),
                            linkstatus = 0,
                            remark = "",
                            sendcount = 0,
                            status = "",//状态
                        };

                        if (item.StorageItemList != null)
                        {
                            foreach (var storageItem in item.StorageItemList)
                            {
                                if (storageItem.ItemType != (int)StorageItemType.Clothing) continue;

                                ////送用户时间
                                //if (storageItem.TargetTime8.HasValue)
                                //{
                                //    orderTmp.name += storageItem.TargetTime8.Value.ToString(" MM-dd HH:mm:ss");
                                //}

                                if (storageItem.TargetTime7.HasValue
                                    && storageItem.TargetTime7 >= dstart
                                    && storageItem.TargetTime7 <= dend)
                                {
                                    //站点入库时间
                                    //orderTmp.name += storageItem.TargetTime7.Value.ToString(" MM-dd HH:mm:ss");

                                    if (storageItem.TargetType == (int)StorageTargetType.ToUser)
                                    {
                                        orderTmp.status = "在库中";
                                    }
                                    else
                                    {
                                        orderTmp.status = "已送用户";
                                    }
                                    break;
                                }
                            }
                        }

                        rtn.list.Add(orderTmp);
                    }

                    //rtn.totalpage = 5;
                    //rtn.totalcount = 500;
                    //rtn.list.Add(new OrderModel()
                    //{
                    //    id = -1,
                    //    address = "新会路379号",
                    //    mpno = "18612980478",
                    //    name = "蔡宇" + DateTime.Now.ToString(" MM-dd") + "20:30:00",
                    //    count = 5,
                    //    expnumber = "0000000001",

                    //    money = "49.50",
                    //    linkstatus = 1,
                    //    remark = "",
                    //    sendcount = 5,
                    //    status = "状态",
                    //});

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //干线取站点日志
        public JsonResult OrderLineTakeSiteLog(int operatorid, int type, string startdate, string enddate, int page)
        {
            if (string.IsNullOrWhiteSpace(startdate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (string.IsNullOrWhiteSpace(enddate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (page <= 0)
            {
                return MyJson(BaseResult.BadResult(-1, "页数错误"));
            }
            try
            {
                DateTime dstart = DateTime.Now;
                DateTime dend = DateTime.Now;

                dstart = DateTime.ParseExact(startdate, "yyyyMMddHHmmss", null);
                dend = DateTime.ParseExact(enddate, "yyyyMMddHHmmss", null).AddDays(1);

                var logRtn = AppExpressProxy.Exp_Order_SELECT_LineTakeSite_Log(operatorid, type, dstart, dend, page, pageSize);
                if (logRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (logRtn.Success == false)
                {
                    if (logRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(logRtn.Code, logRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(logRtn.Code, logRtn.Message));
                    }
                }
                else
                {
                    OrderLogListResult rtn = new OrderLogListResult()
                    {
                        list = new List<OrderModel>(),
                        page = page,
                        totalcount = logRtn.OBJ.RecordCount,
                        // totalpage = logRtn.OBJ.
                    };
                    if (rtn.totalcount <= 0)
                    {
                        rtn.totalpage = 1;
                    }
                    else if (rtn.totalcount % pageSize == 0)
                    {
                        rtn.totalpage = rtn.totalcount / pageSize;
                    }
                    else
                    {
                        rtn.totalpage = rtn.totalcount / pageSize + 1;
                    }

                    foreach (var item in logRtn.OBJ.ReturnList)
                    {
                        if (item == null) continue;

                        var orderTmp = new OrderModel()
                        {
                            id = item.ID,
                            address = item.Address,
                            mpno = item.Mpno,
                            name = item.Contacts, // + item.ExpTime.ToString("MM-dd HH:mm:ss"),
                            count = item.PackageCount,
                            expnumber = item.ExpNumber,
                            money = item.ChargeFee.ToString("0.00"),
                            linkstatus = 0,
                            remark = "",
                            sendcount = 0,
                            status = "",//状态
                        };

                        if (item.StorageItemList != null)
                        {
                            foreach (var storageItem in item.StorageItemList)
                            {
                                if (storageItem.TargetType == (int)StorageTargetType.ToFactory)
                                {
                                    orderTmp.status = "在库中";
                                }
                                else
                                {
                                    orderTmp.status = "已送工厂";
                                }
                                break;
                            }
                        }

                        rtn.list.Add(orderTmp);
                    }
                    //rtn.totalpage = 5;
                    //rtn.totalcount = 500;
                    //rtn.list.Add(new OrderModel()
                    //{
                    //    id = -1,
                    //    address = "新会路379号",
                    //    mpno = "18612980478",
                    //    name = "蔡宇" + DateTime.Now.ToString(" MM-dd") + "20:30:00",
                    //    count = 5,
                    //    expnumber = "0000000001",

                    //    money = "49.50",
                    //    linkstatus = 1,
                    //    remark = "",
                    //    sendcount = 5,
                    //    status = "状态",
                    //});

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        //干线取工厂日志
        public JsonResult OrderLineFactoryLog(int operatorid, int type, string startdate, string enddate, int page)
        {
            if (string.IsNullOrWhiteSpace(startdate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (string.IsNullOrWhiteSpace(enddate))
            {
                return MyJson(BaseResult.BadResult(-1, "时间不能为空"));
            }
            if (page <= 0)
            {
                return MyJson(BaseResult.BadResult(-1, "页数错误"));
            }
            try
            {
                DateTime dstart = DateTime.Now;
                DateTime dend = DateTime.Now;

                dstart = DateTime.ParseExact(startdate, "yyyyMMddHHmmss", null);
                dend = DateTime.ParseExact(enddate, "yyyyMMddHHmmss", null).AddDays(1);

                var logRtn = AppExpressProxy.Exp_Order_SELECT_LineFactory_Log(operatorid, type, dstart, dend, page, pageSize);
                if (logRtn == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                else if (logRtn.Success == false)
                {
                    if (logRtn.Code != -999)
                    {
                        return MyJson(BaseResult.BadResult(logRtn.Code, logRtn.Message));
                    }
                    else
                    {
                        return MyJson(BaseResult.SystemError(logRtn.Code, logRtn.Message));
                    }
                }
                else
                {
                    OrderLogListResult rtn = new OrderLogListResult()
                    {
                        list = new List<OrderModel>(),
                        page = page,
                        totalcount = logRtn.OBJ.RecordCount,
                        // totalpage = logRtn.OBJ.
                    };
                    if (rtn.totalcount <= 0)
                    {
                        rtn.totalpage = 1;
                    }
                    else if (rtn.totalcount % pageSize == 0)
                    {
                        rtn.totalpage = rtn.totalcount / pageSize;
                    }
                    else
                    {
                        rtn.totalpage = rtn.totalcount / pageSize + 1;
                    }

                    foreach (var item in logRtn.OBJ.ReturnList)
                    {
                        if (item == null) continue;

                        var orderTmp = new OrderModel()
                        {
                            id = item.ID,
                            address = item.Address,
                            mpno = item.Mpno,
                            name = item.Contacts, // + item.ExpTime.ToString("MM-dd HH:mm:ss"),
                            count = item.PackageCount,
                            expnumber = item.ExpNumber,
                            money = item.ChargeFee.ToString("0.00"),
                            linkstatus = 0,
                            remark = "",
                            sendcount = 0,
                            status = "",//状态
                        };

                        if (item.StorageItemList != null)
                        {
                            foreach (var storageItem in item.StorageItemList)
                            {
                                if (storageItem.ItemType != (int)StorageItemType.Clothing) continue;

                                if (storageItem.TargetTime6.HasValue
                                    && storageItem.TargetTime6 >= dstart
                                    && storageItem.TargetTime6 <= dend)
                                {
                                    //orderTmp.name += storageItem.TargetTime6.Value.ToString(" MM-dd HH:mm:ss");
                                   
                                    if (storageItem.TargetType == (int)StorageTargetType.ToSite)
                                    {
                                        orderTmp.status = "在库中";
                                    }
                                    else
                                    {
                                        orderTmp.status = "已送站点";
                                    }
                                    break;
                                }
                            }
                        }

                        rtn.list.Add(orderTmp);
                    }
                    //rtn.totalpage = 5;
                    //rtn.totalcount = 500;
                    //rtn.list.Add(new OrderModel()
                    //{
                    //    id = -1,
                    //    address = "新会路379号",
                    //    mpno = "18612980478",
                    //    name = "蔡宇" + DateTime.Now.ToString(" MM-dd") + "20:30:00",
                    //    count = 5,
                    //    expnumber = "0000000001",

                    //    money = "49.50",
                    //    linkstatus = 1,
                    //    remark = "",
                    //    sendcount = 5,
                    //    status = "状态",
                    //});

                    return MyJson(rtn);
                }
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        #endregion

    }
}