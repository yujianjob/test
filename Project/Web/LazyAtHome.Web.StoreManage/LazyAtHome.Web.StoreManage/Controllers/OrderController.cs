using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.StoreManage.CodeSource.Proxy;
using LazyAtHome.Web.StoreManage.Models.Order;
using LazyAtHome.Web.StoreManage.Models.ProductCategory;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.Web.StoreManage.Controllers
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
                search = new OrderSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-6);
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OrderListModel list = new OrderListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息


            RecordCountEntity<Order_OrderDC> rce = OrderProxy.GetOrderList(item.StoreID, search.OrderStatus, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.OrderList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_SearchMessage);
            }

            return View("Index", list);
        }

        /// <summary>
        /// 获取合作门店订单情况
        /// 有未打包的订单显示该订单
        /// 无未打包的订单提示添加
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreOrderIndex()
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

            Order_OrderDC entity = null;

            //获取当前门店 未打包的订单
            ReturnEntity<Order_OrderDC> re = OrderProxy.GetStoreOrderByStoreID(item.StoreID);
            if (re != null && re.Success)
            {
                if (re.OBJ != null)
                {
                    entity = re.OBJ;
                }
                else
                {
                    //说明没有未打包的订单 需要创建
                    entity = new Order_OrderDC();
                }


            }
            else
            {
                entity = new Order_OrderDC();
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_SearchMessage);
            }
            return View("CreateOrder", entity);
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrder()
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

            Order_ConsigneeAddressDC address = CodeSource.Common.ConstConfig.GetConsigneeAddress(item);

            Order_OrderDC entity = null;
            ReturnEntity<Order_OrderDC> re = OrderProxy.AddOrder(item.StoreID, address);
            if (re != null && re.Success && re.OBJ != null)
            {
                entity = re.OBJ;

                //成功 记录操作日志
                //Wash_Store_OperatorLogDC logobj = new Wash_Store_OperatorLogDC();
                //logobj.OperatorID = item.OperatorID;
                //logobj.OperatorName = item.OperatorName;
                //logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.StoreOrder;
                //logobj.LogContent = string.Format("[{0}]进行订单创建，订单id为[{1}]", item.OperatorName, entity.ID);
                //OperatorProxy.OperateLog_Add(logobj);
            }
            else
            {
                entity = new Order_OrderDC();
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_SearchMessage);
            }

            return View("CreateOrder", entity);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <returns></returns>
        public ActionResult GetStoreOrder(string ordernumber)
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

            //地址信息
            Order_ConsigneeAddressDC address = CodeSource.Common.ConstConfig.GetConsigneeAddress(item);

            Order_OrderDC entity = null;
            ReturnEntity<Order_OrderDC> re = OrderProxy.GetStoreOrderByNum(ordernumber);


            //if (string.IsNullOrEmpty(ordernumber))
            //{
            //    //如果订单号为空 获取当前未打包的订单 没有则创建
            //    re = OrderProxy.GetStoreOrder(item.StoreID, address);
                
            //}
            //else
            //{
            //    //如果有订单号
            //    re = OrderProxy.GetStoreOrderByNum(ordernumber);
            //}
            if (re!= null && re.Success)
            {
                //entity = re.OBJ;

                if (re.OBJ != null)
                {
                    entity = re.OBJ;
                }
                else
                {
                    entity = new Order_OrderDC();
                }
                //CodeSource.SiteSession.CurrOrderNumber = entity.OrderNumber;
            }
            else
            {
                entity = new Order_OrderDC();
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_SearchMessage);
            }

            return View("CreateOrder", entity);
            
        }

        /// <summary>
        /// 点击收衣按钮 跳转到衣物选择页面 获取产品信息
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public ActionResult AddProduct(int oid)
        {
            //获取分类与品类 以便选择页面展示
            IList<Wash_ClassDC> subList = ProductCategoryProxy.GetCategorySecondList();

            IList<Category> list = null;

            //CategoryListModel entity = new CategoryListModel();

            if (subList != null)
            {
                list = new List<Category>();
                foreach (var item in subList)
                {
                    Category category = new Category(item);
                    category.SubList = ProductCategoryProxy.GetCategoryListBYClassID(item.ID);//subList.Where(p => p.ParentID == item.ID).ToList();
                    list.Add(category);
                }
            }
            else
            {
                list = new List<Category>();
                //处理报错
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_SearchMessage);
            }
            //把订单号传到页面上 用于下单
            ViewData["OrderID"] = oid;

            //entity.CategoryList = list;
            //entity.UserMpno = string.Empty;
            //entity.UserName = string.Empty;

            return View("CategorySelect", list);
            //return View("CategorySelect", entity);
        }

        /// <summary>
        /// 点击左边的产品树 右边显示运价列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult SearchProduct(int cid, string pname, int oid, string usermpno, string username)
        { 
            //根据产品ID 获取 运价
            IList<LazyAtHome.WCF.Wash.Contract.DataContract.Wash_ProductDC> list = ProductCategoryProxy.GetProductListBYCategoryID(cid);

            IList<Product> clientList = null;

            //把图片地址传到页面上 用于显示
            ViewData["CategroyImg"] = CodeSource.Common.ConstConfig.IMAGE_PATH + CodeSource.Common.ConstConfig.CATEGORY_IMG_PATH + pname;
            //把订单号传到页面上 用于下单
            ViewData["OrderID"] = oid;

            if (usermpno == null)
            {
                usermpno = string.Empty;
            }
            if (username == null)
            {
                username = string.Empty;
            }
            ViewData["UserMpno"] = usermpno;
            ViewData["UserName"] = username;

            if (list != null)
            {
                clientList = new List<Product>();
                foreach (var item in list)
                {
                    Product product = new Product(item);
                    product.IsSelect = false;
                    product.SelectCount = 1;
                    clientList.Add(product);
                }
            }




            return View("ProductSelect", clientList);
        }

        /// <summary>
        /// 添加选中的衣物到订单
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="username"></param>
        /// <param name="usermpno"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public JsonResult AddProductToOrder(int oid, string username, string usermpno, Product[] product)
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

            IList<Order_ProductDC> list = null;
            if (product != null)
            {
                list = new List<Order_ProductDC>();
                //循环衣物列表
                foreach (var item_p in product)
                {
                    if (item_p.IsSelect)
                    {
                        //筛选被选中的
                        for (int i = 0; i < item_p.SelectCount; i++)
                        {
                            Order_ProductDC entity = new Order_ProductDC();
                            entity.ProductID = item_p.ID;
                            entity.UserName = username;
                            entity.UserMPNo = usermpno;
                            list.Add(entity);
                        }
                    }
                }

                if (list.Count == 0)
                {
                    //提示需要选择产品
                    rjEntity.statusCode = CodeSource.StatusCode.Faild;
                    rjEntity.message = "请选择衣物！";
                }
                else
                {

                    ReturnEntity<bool> re = OrderProxy.AddProductToOrder(oid, list);

                    if (re != null && re.Success)
                    {
                        if ((bool)re.OBJ)
                        {
                            //成功 记录操作日志
                            Wash_Store_OperatorLogDC logobj = new Wash_Store_OperatorLogDC();
                            logobj.OperatorID = item.OperatorID;
                            logobj.OperatorName = item.OperatorName;
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.StoreOrder;
                            logobj.LogContent = string.Format("[{0}]进行衣物添加至订单，订单id为[{1}]", item.OperatorName, oid);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("放入收衣篮成功！");
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
                }
            }
              

            return Json(rjEntity);
        }


        /// <summary>
        /// 删除已经选中的衣物
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult DeleteProduct(int oid, int pid)
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

            ReturnEntity<bool> re = OrderProxy.DeleteProductFromOrder(oid, pid);

            if (re != null && re.Success)
            {
                if ((bool)re.OBJ)
                {
                    //成功 记录操作日志
                    Wash_Store_OperatorLogDC logobj = new Wash_Store_OperatorLogDC();
                    logobj.OperatorID = item.OperatorID;
                    logobj.OperatorName = item.OperatorName;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.StoreOrder;
                    logobj.LogContent = string.Format("[{0}]进行衣物从订单中删除，订单id为[{1}]，订单衣物id为[{2}]", item.OperatorName, oid, pid);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("删除成功！");
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "createorder";
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
        

        /// <summary>
        /// 弹出物流单号对话框
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public ActionResult ExpressNumDialog(int oid)
        {
            return View("ExpressNumDialog", oid);
        }


        /// <summary>
        /// 订单打包
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="expressnum"></param>
        /// <returns></returns>
        public JsonResult PackageOrder(int oid, string expressnum)
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

            ReturnEntity<bool> re = OrderProxy.PackageOrder(oid, expressnum);

            if (re != null && re.Success)
            {
                if ((bool)re.OBJ)
                {
                    //成功 记录操作日志
                    Wash_Store_OperatorLogDC logobj = new Wash_Store_OperatorLogDC();
                    logobj.OperatorID = item.OperatorID;
                    logobj.OperatorName = item.OperatorName;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.StoreOrder;
                    logobj.LogContent = string.Format("[{0}]对订单id为[{1}]进行打包，快递单号为[{2}]", item.OperatorName, oid, expressnum);
                    OperatorProxy.OperateLog_Add(logobj);



                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("打包成功！");
                    rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    rjEntity.navTabId = "createorder";
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