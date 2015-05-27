using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.Winform.FactoryPortal.Common;
using LazyAtHome.Winform.FactoryPortal.WashOrderService;
using LazyAtHome.Winform.FactoryPortal.SFExpressService;
using LazyAtHome.Winform.FactoryPortal.QFExpressService;
using LazyAtHome.Winform.FactoryPortal.InternalExpressService;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;


namespace LazyAtHome.Winform.FactoryPortal.Business
{
    public class FormWashOrderClient : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<IWashOrder>
    {
        public FormWashOrderClient()
            : base("WSHttpBinding_IWashOrder")
        { }
    }

    public class FormSFExpressClient : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<ISFExpress>
    {
        public FormSFExpressClient()
            : base("WSHttpBinding_ISFExpress")
        { }
    }

    public class FormQFExpressClient : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<IQFExpress>
    {
        public FormQFExpressClient()
            : base("WSHttpBinding_IQFExpress")
        { }
    }



    public class FormInternalExpressClient : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<IInternalExpress>
    {
        public FormInternalExpressClient()
            : base("WSHttpBinding_IInternalExpress")
        { }
    }

    /// <summary>
    /// 订单业务逻辑类
    /// </summary>
    public class WashOrder : Base
    {
        //public IWashOrder WSWashOrderService = new WashOrderClient();
        public FormWashOrderClient WSWashOrderService = new FormWashOrderClient();

        //public ISFExpress WSSFExpressService = new SFExpressClient();
        public FormSFExpressClient WSSFExpressService = new FormSFExpressClient();

        //public IQFExpress WSQFExpressService = new QFExpressClient();
        public FormQFExpressClient WSQFExpressService = new FormQFExpressClient();

        public FormInternalExpressClient WSInternalExpressService = new FormInternalExpressClient();

        #region 订单



        /// <summary>
        /// 根据快递单号获取订单信息
        /// </summary>
        /// <param name="iExpressNumber"></param>
        public Order_OrderDC GetOrder(string iExpressNumber)
        {
            Order_OrderDC entity = null;

            //ReturnEntityOfOrder_OrderDCP7q220_SG re = null;
            ReturnEntity<Order_OrderDC> re = null;

            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                //re = WSWashOrderService.Proxy.Order_Order_SELECT_Entity_Express(login, iExpressNumber, true, false, true, true, false, false);
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                    (client => client.Proxy.Order_Order_SELECT_Entity_Express(login, iExpressNumber, true, false, true, true, false, false));

                //re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                //    (client => client.Proxy.Order_Order_SELECT_Entity_FactoryCode(login, iExpressNumber, true, false, true, true, false, false));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder GetOrder|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    entity = re.OBJ;
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return entity;
        }


        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="iOrderNumber"></param>
        public Order_OrderDC GetOrderByOrderNumber(string iOrderNumber)
        {
            Order_OrderDC entity = null;

            //ReturnEntityOfOrder_OrderDCP7q220_SG re = null;
            ReturnEntity<Order_OrderDC> re = null;

            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                //re = WSWashOrderService.Proxy.Order_Order_SELECT_Entity_Express(login, iExpressNumber, true, false, true, true, false, false);
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                    (client => client.Proxy.Order_Order_SELECT_Entity_OrderNumber(login, iOrderNumber, false, false, true, false, false, false));

                //re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                //    (client => client.Proxy.Order_Order_SELECT_Entity_FactoryCode(login, iExpressNumber, true, false, true, true, false, false));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder GetOrder|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    entity = re.OBJ;
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return entity;
        }


        /// <summary>
        /// 根据洗涤条码获取订单信息
        /// </summary>
        /// <param name="iFactoryCode"></param>
        public Order_OrderDC GetOrderByFactoryCode(string iFactoryCode)
        {
            Order_OrderDC entity = null;

            //ReturnEntityOfOrder_OrderDCP7q220_SG re = null;
            ReturnEntity<Order_OrderDC> re = null;

            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                //re = WSWashOrderService.Proxy.Order_Order_SELECT_Entity_Express(login, iExpressNumber, true, false, true, true, false, false);
                //re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                //    (client => client.Proxy.Order_Order_SELECT_Entity_Express(login, iExpressNumber, true, false, true, true, false, false));

                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                    (client => client.Proxy.Order_Order_SELECT_Entity_FactoryCode(login, iFactoryCode, true, false, true, true, false, false));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder GetOrderByCode|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    entity = re.OBJ;
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return entity;
        }



        /// <summary>
        /// 更新衣物步骤
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public bool OrderProductStepUPDATE(int iOrderID, int iOrderProductID, int iStep)
        {
            bool rtn = false;
            ReturnEntity<bool> re = null;
            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Product_UPDATE_Step(iOrderID, iOrderProductID, iStep));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderProductStepUPDATE|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对衣物ID为[{1}]更新步骤[{2}]", logobj.OperatorName, iOrderProductID, iStep);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }



        /// <summary>
        /// 更新第三方条码
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iOtherCode"></param>
        /// <returns></returns>
        public bool OrderProductOtherCodeUPDATE(int iOrderID, int iOrderProductID, string iOtherCode)
        {
            bool rtn = false;
            ReturnEntity<bool> re = null;
            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Product_UPDATE_OtherCode(login, iOrderID, iOrderProductID, iOtherCode));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderProductOtherCodeUPDATE|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对衣物ID为[{1}]更新第三方条码[{2}]", logobj.OperatorName, iOrderProductID, iOtherCode);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }












        /// <summary>
        /// 出库操作
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public int OrderProductOutFactory(int iOrderID, int iOrderProductID, string iExpressNumber, int iMuser)
        {
            int rtn = -1;
            ReturnEntity<int> re = null;
            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<int>>
                    (client => client.Proxy.Order_Product_OutFactory(login, iOrderID, iOrderProductID, iExpressNumber, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderProductOutFactory|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对衣物ID为[{1}]进行出库操作", logobj.OperatorName, iOrderProductID);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }



        #region 上报异常

        /// <summary>
        /// 上报异常 物流条码无效
        /// </summary>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool OrderInFactory_FailNotify_NotFound(string iExpressNumber, int iMuser)
        {
            bool rtn = false;
            ReturnEntity<bool> re = null;
            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Order_InFactory_FailNotify_NotFound(login, iExpressNumber, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderInFactory_FailNotify_NotFound|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]在入库时上报物流条码[{1}]无效", logobj.OperatorName, iExpressNumber);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }


        /// <summary>
        /// 上报异常 入库订单物品异常
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iContent"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool OrderInFactory_FailNotify_ItemError(string iOrderNumber, string iContent, int iMuser)
        {
            bool rtn = false;
            ReturnEntity<bool> re = null;
            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Order_InFactory_FailNotify_ItemError(login, iOrderNumber, iContent, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderInFactory_FailNotify_ItemError|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]在入库时上报订单[{1}]物品异常，[{2}]", logobj.OperatorName, iOrderNumber, iContent);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }



        /// <summary>
        /// 上报异常 水洗条码无效
        /// </summary>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool OrderOutFactory_FailNotify_NotFound(string iCode, int iMuser)
        {
            bool rtn = false;
            ReturnEntity<bool> re = null;
            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Order_OutFactory_FailNotify_NotFound(login, iCode, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderOutFactory_FailNotify_NotFound|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]在出库时上报水洗条码[{1}]无效", logobj.OperatorName, iCode);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }


        /// <summary>
        /// 上报异常 出库订单物品异常
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iContent"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool OrderOutFactory_FailNotify_ItemBad(string iOrderNumber, string iContent, int iMuser)
        {
            bool rtn = false;
            ReturnEntity<bool> re = null;
            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Order_OutFactory_FailNotify_ItemBad(login, iOrderNumber, iContent, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderOutFactory_FailNotify_ItemBad|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]在入库时上报订单[{1}]物品异常，[{2}]", logobj.OperatorName, iOrderNumber, iContent);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }

        #endregion




        /// <summary>
        /// 订单增加快递物流单号 送衣
        /// </summary>
        /// <param name="iOrderID">订单号</param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iOrderIDNew">新订单号</param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool OrderExpressADD(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            bool rtn = false;

            //WashOrderService.ReturnEntityOfboolean re = null;
            ReturnEntity<bool> re = null;

            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                //re = WSWashOrderService.Proxy.Order_Express_ADD_Factory(iOrderID, iExpressNumber, iRelationID, iMuser);

                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Express_ADD_Factory(iOrderID, iExpressNumber, iRelationID, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderExpressADD|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]写入送衣单号[{2}]", logobj.OperatorName, iOrderID, iExpressNumber);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }


        /// <summary>
        /// 订单更新快递物流单号 送衣
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iOrderIDNew">新订单号</param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool OrderExpressUpdate(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            bool rtn = false;
            
            //WashOrderService.ReturnEntityOfboolean re = null;
            ReturnEntity<bool> re = null;

            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {
                //re = WSWashOrderService.Proxy.Order_Express_ADD_Factory_Re(iOrderID, iExpressNumber,iRelationID, iMuser);

                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Express_ADD_Factory_Re(iOrderID, iExpressNumber, iRelationID, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderExpressADD|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;


                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]更新送衣单号[{2}]", logobj.OperatorName, iOrderID, iExpressNumber);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }




        /// <summary>
        /// 订单更新快递物流单号 收衣
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public bool OrderExpressFinish(int iOrderID, string iExpressNumber)
        {
            bool rtn = false;

            ReturnEntity<bool> re = null;

            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {

                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Order_FinishExpress(iOrderID, iExpressNumber));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder OrderExpressFinish|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;


                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]更新收衣单号[{2}]", logobj.OperatorName, iOrderID, iExpressNumber);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }


        /// <summary>
        /// 补充一键下单的产品信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="pList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Factory_AddProduct(int iOrderID, IList<Order_ProductDC> pList, int iMuser)
        {
            bool rtn = false;

            ReturnEntity<bool> re = null;

            WashOrderService.LoginCredentials login = Common.ConstConfig.GetWashOrderLogin();

            try
            {

                re = WCFInvokeHelper<FormWashOrderClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Order_Factory_AddProduct(login, iOrderID, pList.ToArray(), iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder Onekey_AddProduct|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;
                    string pid = string.Empty;
                    if (pList != null)
                    {
                        foreach (var item in pList)
                        {
                            pid += item.ProductID.ToString() + ",";
                        }
                    }
                    pid = pid.Substring(0, pid.Length - 1);
                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]补充一键下单的产品信息,产品ID为[{2}]", logobj.OperatorName, iOrderID, pid);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }





        #endregion

        #region 顺丰

        /// <summary>
        /// 顺丰下单 若已经下过单 返回已经下单的信息
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <param name="iCompany"></param>
        /// <param name="iContact"></param>
        /// <param name="iTel"></param>
        /// <param name="iAddress"></param>
        /// <param name="iCity"></param>
        /// <param name="iProvince"></param>
        /// <returns></returns>
        public OrderDC SFExpressOrderCreate(string iOrderid, string iCompany, string iContact, string iTel, string iAddress, string iCity, string iProvince, decimal? iPayment)
        {
            //return new OrderDC();
            OrderDC entity = null;

            //ReturnEntityOfOrderDCVyyWkXyZ re = null;
            ReturnEntity<OrderDC> re = null;

            SFExpressService.LoginCredentials login = Common.ConstConfig.GetSFExpressLogin();

            try
            {
                //re = WSSFExpressService.Proxy.CreateOrder(login, iOrderid, Common.ConstConfig.S_Company, Common.ConstConfig.S_Contact, Common.ConstConfig.S_Phone, Common.ConstConfig.S_Address, Common.ConstConfig.S_City, Common.ConstConfig.S_Province, iCompany, iContact, iTel, iAddress, iCity, iProvince, iPayment);

                re = WCFInvokeHelper<FormSFExpressClient>.Invoke<ReturnEntity<OrderDC>>
                    (client => client.Proxy.CreateOrder(login, iOrderid, Common.ConstConfig.S_Company, Common.ConstConfig.S_Contact, Common.ConstConfig.S_Phone, Common.ConstConfig.S_Address, Common.ConstConfig.S_City, Common.ConstConfig.S_Province, iCompany, iContact, iTel, iAddress, iCity, iProvince, iPayment));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder SFExpressOrderCreate|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    entity = re.OBJ;


                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行顺丰下单", logobj.OperatorName, iOrderid);
                    business.OperateLog_Add(logobj);
                }
                else if (re.Code == 8016)
                {
                    //说明已经下过单 调用查询接口
                    entity = SFExpressOrderSearch(iOrderid);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return entity;
        }


        /// <summary>
        /// 顺丰订单查询
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public OrderDC SFExpressOrderSearch(string orderid)
        {
            OrderDC entity = null;

            //ReturnEntityOfOrderDCVyyWkXyZ re = null;
            ReturnEntity<OrderDC> re = null;

            SFExpressService.LoginCredentials login = Common.ConstConfig.GetSFExpressLogin();

            try
            {
                //re = WSSFExpressService.Proxy.OrderSearch(login, orderid);
                re = WCFInvokeHelper<FormSFExpressClient>.Invoke<ReturnEntity<OrderDC>>
                    (client => client.Proxy.OrderSearch(login, orderid));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder SFExpressOrderSearch|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    entity = re.OBJ;
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return entity;
        }


        #endregion

        #region 全峰

        /// <summary>
        /// 全峰下单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="d_Name"></param>
        /// <param name="d_Phone"></param>
        /// <param name="d_Mobile"></param>
        /// <param name="d_Province"></param>
        /// <param name="d_City"></param>
        /// <param name="d_Address"></param>
        /// <param name="iItemList"></param>
        /// <param name="iExpectStartTime"></param>
        /// <param name="iExpectEndTime"></param>
        /// <returns></returns>
        public bool QFExpressOrderCreate(string iOrderNumber, string iExpressNumber,
          //string j_Name, string j_PostCode, string j_Phone, string j_Mobile, string j_Province, string j_City, string j_Address,
          string d_Name, string d_Phone, string d_Mobile, string d_Province, string d_City, string d_Address,
          Dictionary<string, int> iItemList, DateTime? iExpectStartTime = null, DateTime? iExpectEndTime = null)
        {
            bool rtn = false;

            //QFExpressService.ReturnEntityOfboolean re = null;
            ReturnEntity<bool> re = null;

            QFExpressService.LoginCredentials login = Common.ConstConfig.GetQFExpressLogin();
            try
            {
                //re = WSQFExpressService.Proxy.CreateOrder(login, iOrderNumber, iExpressNumber,
                //    Common.ConstConfig.S_Company, "10000", null, Common.ConstConfig.S_Phone, Common.ConstConfig.S_Province, Common.ConstConfig.S_City, Common.ConstConfig.S_Address,
                //    d_Name, "10000", d_Phone, d_Mobile, d_Province, d_City, d_Address, iItemList, iExpectStartTime, iExpectEndTime);

                re = WCFInvokeHelper<FormQFExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.CreateOrder(login, iOrderNumber, iExpressNumber,
                    Common.ConstConfig.S_Company, "10000", null, Common.ConstConfig.S_Phone, Common.ConstConfig.S_Province, Common.ConstConfig.S_City, Common.ConstConfig.S_Address,
                    d_Name, "10000", d_Phone, d_Mobile, d_Province, d_City, d_Address, iItemList, iExpectStartTime, iExpectEndTime));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder QFExpressOrderCreate|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;


                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行全峰下单", logobj.OperatorName, iOrderNumber);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }

        /// <summary>
        /// 全峰订单撤销
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public bool QFExpressOrderCancel(string iOrderNumber)
        {
            bool rtn = false;

            //QFExpressService.ReturnEntityOfboolean re = null;
            ReturnEntity<bool> re = null;

            QFExpressService.LoginCredentials login = Common.ConstConfig.GetQFExpressLogin();

            try
            {
                //re = WSQFExpressService.Proxy.CancelOrder(login, iOrderNumber, "系统撤销");

                re = WCFInvokeHelper<FormQFExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.CancelOrder(login, iOrderNumber, "系统撤销"));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder QFExpressOrderCreate|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单ID为[{1}]进行全峰撤单", logobj.OperatorName, iOrderNumber);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }

        #endregion

        #region 自主物流

        /// <summary>
        /// 送件下单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public Exp_OrderDC ExpOrderCreateSend(string iOutNumber)
        {
            Exp_OrderDC entity = null;

            ReturnEntity<Exp_OrderDC> re = null;

            InternalExpressService.LoginCredentials login = Common.ConstConfig.GetInternalExpressLogin();

            try
            {
                re = WCFInvokeHelper<FormInternalExpressClient>.Invoke<ReturnEntity<Exp_OrderDC>>
                            (client => client.Proxy.Exp_Order_Create_SendNumber(login, iOutNumber));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder ExpOrderCreate|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    entity = re.OBJ;


                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对订单编号为[{1}]进行物流下单", logobj.OperatorName, iOutNumber);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return entity;
        }

        /// <summary>
        /// 获取该工厂下所有干线
        /// </summary>
        /// <returns></returns>
        public IList<Exp_NodeDC> GetLineList()
        {
            IList<Exp_NodeDC> list = null;
            ReturnEntity<RecordCountEntity<Exp_NodeDC>> rce = null;
            InternalExpressService.LoginCredentials login = Common.ConstConfig.GetInternalExpressLogin();
            try
            {
                rce = WCFInvokeHelper<FormInternalExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_NodeDC>>>
                    (client => client.Proxy.Exp_Node_SELECT_List(login, null, null, 2, null, null, 1, 20, Common.ConstConfig.currOperator.NodeID));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder GetLineList|" + ex.Message);
                LastError = ex.Message;
            }
            if (rce != null)
            {
                if (rce.Success)
                {
                    list = rce.OBJ.ReturnList;
                }
                else
                {
                    //处理报错信息
                    LastError = rce.Message;
                }

            }
            return list;
        }

        /// <summary>
        /// 获取干线仓库库存
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        private RecordCountEntity<Exp_StorageItemDC> GetLineStorageItem(int iStorageID, int iPageIndex, int iPageSize)
        {
            //IList<Exp_StorageItemDC> list = null;

            RecordCountEntity<Exp_StorageItemDC> rtn = null;
            ReturnEntity<RecordCountEntity<Exp_StorageItemDC>> rce = null;
            InternalExpressService.LoginCredentials login = Common.ConstConfig.GetInternalExpressLogin();
            try
            {
                rce = WCFInvokeHelper<FormInternalExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_StorageItemDC>>>
                    (client => client.Proxy.Exp_StorageItem_SELECT_List(login, iStorageID, null, null, 1, null, null, null, null, iPageIndex, iPageSize));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder GetLineStorageItem|" + ex.Message);
                LastError = ex.Message;
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            else
            {
                //处理报错信息
                LastError = rce.Message;
            }
            return rtn;

        }


        /// <summary>
        /// 获取干线仓库库存
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <returns></returns>
        public IList<Exp_StorageItemDC> GetLineStorageItem(int iStorageID)
        {
            //设置分页参数
            int pageNum = 1;
            int pagesize = 50;
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            List<Exp_StorageItemDC> listall = new List<Exp_StorageItemDC>();
            RecordCountEntity<Exp_StorageItemDC> rce = this.GetLineStorageItem(iStorageID, pageNum, pagesize);

            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑
                return null;
            }

            loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                : ((totalcount - pagesize) / pagesize + 1);
            for (int i = 2; i <= loopcount + 1; i++)
            {
                pageNum = i;
                RecordCountEntity<Exp_StorageItemDC> rce2 = this.GetLineStorageItem(iStorageID, pageNum, pagesize);
                if (rce2 != null)
                {
                    //totalcount = rce2.RecordCount;//设置查询总记录数
                    listall.AddRange(rce2.ReturnList);
                }
                else
                {
                    //处理报错逻辑   
                    return null;
                }
            }

            return listall;
        }


        /// <summary>
        /// 收干线件 入库
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Factory_TakeLine_Complete(int iStorageID, string[] iOrderNumberList, int iMuser)
        {
            bool rtn = false;

            ReturnEntity<bool> re = null;

            InternalExpressService.LoginCredentials login = Common.ConstConfig.GetInternalExpressLogin();

            try
            {

                re = WCFInvokeHelper<FormInternalExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Exp_Storage_FactoryTakeLine_Complete(login, iStorageID, iOrderNumberList, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder Factory_TakeLine_Complete|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;
                    
                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对干线仓库ID为[{1}]进行收件入库", logobj.OperatorName, iStorageID);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }




        /// <summary>
        /// 收干线件 错件上报
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Factory_TakeLine_ErrorNumber(int iStorageID, string[] iOrderNumberList, int iMuser)
        {
            bool rtn = false;

            ReturnEntity<bool> re = null;

            InternalExpressService.LoginCredentials login = Common.ConstConfig.GetInternalExpressLogin();

            try
            {

                re = WCFInvokeHelper<FormInternalExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Exp_Storage_FactoryTakeLine_ErrorNumber(login, iStorageID, iOrderNumberList, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder Factory_TakeLine_ErrorNumber|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对干线仓库ID为[{1}]进行错件上报", logobj.OperatorName, iStorageID);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }



        /// <summary>
        /// 收干线件 差件上报
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Factory_TakeLine_ErrorCount(int iStorageID, int iMuser)
        {
            bool rtn = false;

            ReturnEntity<bool> re = null;

            InternalExpressService.LoginCredentials login = Common.ConstConfig.GetInternalExpressLogin();

            try
            {

                re = WCFInvokeHelper<FormInternalExpressClient>.Invoke<ReturnEntity<bool>>
                    (client => client.Proxy.Exp_Storage_FactoryTakeLine_ErrorCount(login, iStorageID, iMuser));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashOrder Factory_TakeLine_ErrorCount|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    rtn = re.OBJ;

                    //成功 记录操作日志
                    Business.Operator business = new Operator();
                    Model.OperatorLog logobj = new Model.OperatorLog();
                    logobj.OperatorID = Common.ConstConfig.currOperator.ID;
                    logobj.OperatorName = Common.ConstConfig.currOperator.Name;
                    logobj.Type = 2003;
                    logobj.LogContent = string.Format("[{0}]对干线仓库ID为[{1}]进行差件上报", logobj.OperatorName, iStorageID);
                    business.OperateLog_Add(logobj);
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return rtn;
        }

        #endregion
    }
}
