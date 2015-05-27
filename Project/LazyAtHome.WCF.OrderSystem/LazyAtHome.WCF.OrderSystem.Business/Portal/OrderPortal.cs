using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Service;
//using LazyAtHome.Library.Partners.SFExpress.Entity;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Business.Portal
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
    public class OrderPortal : ServiceBase, LazyAtHome.WCF.OrderSystem.Contract.InterfaceContract.IOrder
    {
        protected LazyAtHome.WCF.OrderSystem.Business.Business.Order OrderInstance = LazyAtHome.WCF.OrderSystem.Business.Business.Order.Instance;

        protected void Log_Fatal(string iMethodName, Exception iException)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            //VEBS.Core.Helper.LogHelper.Log_Fatal("调用异常 " + iMethodName, 99, iException.Message + "\r\n" + GetFrameString(iException));
            Console.WriteLine("调用异常 " + iMethodName + 99 + iException.Message + "\r\n" + GetFrameString(iException));
            Console.ResetColor();
        }

        protected void Log_Warn(string iMethodName, string iMessage)
        {
            //VEBS.Core.Helper.LogHelper.Log_Info(iMethodName, 99, iMessage);
        }

        /// <summary>
        /// 获取异常堆栈
        /// </summary>
        /// <param name="iException"></param>
        /// <returns></returns>
        public static string GetFrameString(Exception iException)
        {
            bool flag = true;
            Hashtable rtn = new Hashtable();
            try
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(iException);
                foreach (System.Diagnostics.StackFrame frame in trace.GetFrames())
                {
                    if (flag)
                    {
                        flag = false;
                        continue;
                    }
                    if (frame.GetMethod().IsPublic)
                    {
                        //方法名
                        var methodName = frame.GetMethod().Name;

                        //类名
                        var tmpString = string.Empty;
                        if (frame.GetMethod().DeclaringType != null)
                        {
                            //方法名
                            tmpString = frame.GetMethod().DeclaringType.FullName + ".";
                            if (tmpString.StartsWith("System."))
                            {
                                //如果是系统级的，直接跳过
                                continue;
                            }
                        }
                        tmpString += methodName;
                        if (!rtn.ContainsKey(tmpString))
                            rtn.Add(tmpString, null);
                    }
                }
            }
            catch { }
            var rtnstring = "\n";
            foreach (var item in rtn.Keys)
            {
                rtnstring += item + "\n";
            }
            return rtnstring;
        }

        /// <summary>
        /// 调用日志
        /// </summary>
        /// <param name="iMethodName"></param>
        /// <param name="iParams"></param>
        protected void Log_DeBug(string iMethodName, params object[] iParams)
        {
            for (int i = 0; i < iParams.Length; i++)
            {
                if (iParams[i] == null)
                {
                    iParams[i] = string.Empty;
                }
            }

            Console.WriteLine(string.Format("{0}\t{1}", iMethodName, String.Join("_", iParams)));
            //LazyAtHome.Core.Helper..Log_Debug("调用日志", 99
            //        , string.Format("{0}\t{1}"
            //        , iMethodName
            //        , String.Join("_", iParams)
            //        )
            //    );
        }

        public ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_Order_SELECT_List(
            string iOrderNumber, Guid? iUserID,
            string iMPNo, string iLoginName,
            OrderClass? OrderClass,
            OrderType? iOrderType, OrderStatus? iOrderStatus,
            int? iSite, int? iChannel,
            decimal? iTotalAmountMax, decimal? iTotalAmountMin,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize,
            string iConsignee = null,
            int iSortType = 0,
            DateTime? iExpStartDate = null, DateTime? iExpEndDate = null,
            int? iStep = null, int? iGetExpressType = null)
        {
            Log_DeBug("Order_Order_SELECT_List", iOrderNumber, iUserID,
                iMPNo, iLoginName, iOrderType, iOrderStatus, iSite,
                iChannel, iTotalAmountMax, iTotalAmountMin,
                iStartDate, iEndDate, iPageIndex, iPageSize,
                iConsignee, iSortType,
                iExpStartDate, iExpEndDate, iStep, iGetExpressType);
            try
            {
                return OrderInstance.Order_Order_SELECT_List(iOrderNumber, iUserID,
                    iMPNo, iLoginName, OrderClass, iOrderType, iOrderStatus,
                    iSite, iChannel, iTotalAmountMax, iTotalAmountMin,
                    iStartDate, iEndDate, iPageIndex, iPageSize,
                    iConsignee, iSortType,
                    iExpStartDate, iExpEndDate, iStep, iGetExpressType);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_List");
            }
        }

        /// <summary>
        /// 订单列表(网站)
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_Order_SELECT_List(
            Guid iUserID, OrderStatus? iOrderStatus,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_SELECT_List", iUserID, iOrderStatus, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return OrderInstance.Order_Order_SELECT_List(iUserID, iOrderStatus, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_List");
            }

        }

        /// <summary>
        /// 订单列表(微信)
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_OrderDC>> Order_Order_SELECT_List(
            string iOpenid, OrderStatus? iOrderStatus,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_SELECT_List", iOpenid, iOrderStatus, iPageIndex, iPageSize);
            try
            {
                return OrderInstance.Order_Order_SELECT_List(iOpenid, iOrderStatus, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_List");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_OrderDC>> app_Order_Order_SELECT_List(
            Guid iUserID, OrderStatus? iOrderStatus,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("app_Order_Order_SELECT_List", iUserID, iOrderStatus, iPageIndex, iPageSize);
            try
            {
                return OrderInstance.app_Order_Order_SELECT_List(iUserID, iOrderStatus, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("app_Order_Order_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Order_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("app_Order_Order_SELECT_List");
            }
        }

        /// <summary>
        /// 退单
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_Cancel(int iID, OrderStatus iOrderStatus)
        {
            Log_DeBug("Order_Order_Cancel", iID, iOrderStatus);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Cancel(iID, iOrderStatus);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Cancel", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Cancel");
            }

            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        /// <summary>
        /// 退单
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iChannel"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_ChargeBack(int iID, Channel iChannel)
        {
            Log_DeBug("Order_Order_ChargeBack", iID, iChannel);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_ChargeBack(iID, iChannel);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_ChargeBack", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_ChargeBack");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 返洗
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iChannel"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_Return(int iID, IList<int> iPidList)
        {
            Log_DeBug("Order_Order_Return", iID, iPidList);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Return(iID, iPidList);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Return", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Return");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity(
            int iID, bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep, bool iGetFeedback = false)
        {
            Log_DeBug("Order_Order_SELECT_Entity", iID, iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep, iGetFeedback);
            try
            {
                return OrderInstance.Order_Order_SELECT_Entity(iID, iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep, iGetFeedback);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_Entity", ex);
                return new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_Entity");
            }
        }

        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity(
            string iOrderNumber, bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            Log_DeBug("Order_Order_SELECT_Entity", iOrderNumber, iGetProduct, iGetAmount, iGetConsigneeAddress,
               iGetExpress, iGetPayment, iGetStep);
            try
            {
                return OrderInstance.Order_Order_SELECT_Entity(iOrderNumber, iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_Entity", ex);
                return new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_Entity");
            }
        }

        /// <summary>
        /// 根据物流订单号查订单
        /// </summary>
        /// <param name="iExpressNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_Express(string iExpressNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            Log_DeBug("Order_Order_SELECT_Entity_Express", iExpressNumber, iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
            try
            {
                return OrderInstance.Order_Order_SELECT_Entity_Express(iExpressNumber, iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_Entity_Express", ex);
                return new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_Entity_Express");
            }
        }

        /// <summary>
        /// 根据工厂条形码查订单
        /// </summary>
        /// <param name="iCodeNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity_FactoryCode(string iCodeNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            Log_DeBug("Order_Order_SELECT_Entity_Code", iCodeNumber, iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
            try
            {
                return OrderInstance.Order_Order_SELECT_Entity_FactoryCode(iCodeNumber, iGetProduct, iGetAmount, iGetConsigneeAddress,
              iGetExpress, iGetPayment, iGetStep);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_Entity_Code", ex);
                return new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_Entity_Code");
            }
        }

        ///// <summary>
        ///// 工厂出库添加快递单号
        ///// </summary>
        ///// <param name="iOrderID"></param>
        ///// <param name="iExpressNumber"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> Order_Order_CreateSendExpress(int iOrderID, string iExpressNumber)
        //{
        //    Log_DeBug("Order_Order_CreateSendExpress", iOrderID, iExpressNumber);
        //    try
        //    {
        //        return OrderInstance.Order_Order_CreateSendExpress(iOrderID, iExpressNumber);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_Fatal("Order_Order_CreateSendExpress", ex);
        //        return new ReturnEntity<bool>(-999, ex.Message);
        //    }
        //    finally
        //    {
        //        Log_DeBug("Order_Order_CreateSendExpress");
        //    }
        //}

        ///// <summary>
        ///// 网站下单
        ///// </summary>
        ///// <param name="iUserID"></param>
        ///// <param name="iSite"></param>
        ///// <param name="iProductIDList"></param>
        ///// <param name="iExpressFee"></param>
        ///// <param name="iExpressFeeDiscount"></param>
        ///// <param name="iUserMoney"></param>
        ///// <param name="iLazyCardList"></param>
        ///// <param name="iGetAddress"></param>
        ///// <param name="iSendAddress"></param>
        ///// <param name="TotalAmount"></param>
        ///// <returns></returns>
        //[OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        //public ReturnEntity<Order_OrderDC> Order_Web_Create(Guid iUserID, int iSite,
        //    IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
        //    decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
        //    Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
        //    decimal iRealAmount, string iActivityName = null, decimal? iActivityMoney = null,
        //    IList<int> iCouponIDList = null)
        //{
        //    Log_DeBug("Order_Web_Create", iUserID, iSite,
        //       iProductIDList, iExpressFee, iExpressFeeDiscount,
        //       iUserMoney, iLazyCardList,
        //       iGetAddress, iSendAddress, iRealAmount, iActivityName, iActivityMoney, iCouponIDList);

        //    ReturnEntity<Order_OrderDC> rtn = null;

        //    try
        //    {
        //        rtn = OrderInstance.Order_Web_Create(iUserID, iSite,
        //                iProductIDList, iExpressFee, iExpressFeeDiscount,
        //                iUserMoney, iLazyCardList,
        //                iGetAddress, iSendAddress, iRealAmount, iActivityName, iActivityMoney, iCouponIDList);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_Fatal("Order_Web_Create", ex);
        //        rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
        //    }
        //    finally
        //    {
        //        Log_DeBug("Order_Web_Create");
        //    }
        //    if (rtn != null && rtn.Success)
        //    {
        //        OperationContext.Current.SetTransactionComplete();
        //    }
        //    return rtn;
        //}

        /// <summary>
        /// 网站下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="iActivityList"></param>
        /// <param name="iCouponIDList"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_Web_Create(Guid iUserID, int iSite,
            IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
            decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            decimal iRealAmount, IDictionary<string, decimal> iActivityList,
            IList<int> iCouponIDList = null, bool iSalesPrice = true, string iUserRemark = null,
            int? iSendType = 1, string iInviteCode = null)
        {
            Log_DeBug("Order_Web_Create", iUserID, iSite,
               iProductIDList, iExpressFee, iExpressFeeDiscount,
               iUserMoney, iLazyCardList,
               iGetAddress, iSendAddress, iRealAmount, iActivityList, iCouponIDList, iSalesPrice
               , iUserRemark, iSendType, iInviteCode);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Web_Create(iUserID, iSite,
                        iProductIDList, iExpressFee, iExpressFeeDiscount,
                        iUserMoney, iLazyCardList,
                        iGetAddress, iSendAddress, iRealAmount, iActivityList, iCouponIDList, iSalesPrice
                        , null, iUserRemark, iSendType, iInviteCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Web_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Web_Create");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 网站下单(活动)
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="iActivityName"></param>
        /// <param name="ActivityMoney"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_Web_Create_Activity(Guid iUserID, int iSite,
         IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
         decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
         Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
         decimal iRealAmount, string iActivityName, decimal iActivityMoney)
        {
            Log_DeBug("Order_Web_Create_Activity", iUserID, iSite,
               iProductIDList, iExpressFee, iExpressFeeDiscount,
               iUserMoney, iLazyCardList,
               iGetAddress, iSendAddress, iRealAmount, iActivityName, iActivityMoney);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Web_Create_Activity(iUserID, iSite,
                        iProductIDList, iExpressFee, iExpressFeeDiscount,
                        iUserMoney, iLazyCardList,
                        iGetAddress, iSendAddress, iRealAmount, iActivityName, iActivityMoney);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Web_Create_Activity", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Web_Create_Activity");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        ///// <summary>
        ///// 微信普通订单
        ///// </summary>
        ///// <param name="iOpenid"></param>
        ///// <param name="iSite"></param>
        ///// <param name="iProductIDList"></param>
        ///// <param name="iExpressFee"></param>
        ///// <param name="iExpressFeeDiscount"></param>
        ///// <param name="iUserMoney"></param>
        ///// <param name="iLazyCardList"></param>
        ///// <param name="iGetAddress"></param>
        ///// <param name="iSendAddress"></param>
        ///// <returns></returns>
        //[OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        //public ReturnEntity<Order_OrderDC> Order_Weixin_Create(string iOpenid, int iSite,
        //    IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
        //    decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
        //    Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
        //    decimal iRealAmount, string iActivityName = null, decimal? iActivityMoney = null,
        //    IList<int> iCouponIDList = null)
        //{
        //    Log_DeBug("Order_Weixin_Create", iOpenid, iSite,
        //        iProductIDList, iExpressFee, iExpressFeeDiscount,
        //        iUserMoney, iLazyCardList,
        //        iGetAddress, iSendAddress, iRealAmount, iActivityName, iActivityMoney, iCouponIDList);

        //    ReturnEntity<Order_OrderDC> rtn = null;

        //    try
        //    {
        //        rtn = OrderInstance.Order_Weixin_Create(iOpenid, iSite,
        //                iProductIDList, iExpressFee, iExpressFeeDiscount,
        //                iUserMoney, iLazyCardList,
        //                iGetAddress, iSendAddress, iRealAmount, iActivityName, iActivityMoney, iCouponIDList);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_Fatal("Order_Weixin_Create", ex);
        //        rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
        //    }
        //    finally
        //    {
        //        Log_DeBug("Order_Weixin_Create");
        //    }
        //    if (rtn != null && rtn.Success)
        //    {
        //        OperationContext.Current.SetTransactionComplete();
        //    }
        //    return rtn;
        //}

        /// <summary>
        /// 微信普通订单
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iSite"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="iActivityList"></param>
        /// <param name="iCouponIDList"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_Weixin_Create(string iOpenid, int iSite,
            IList<int> iProductIDList, decimal iExpressFee, decimal iExpressFeeDiscount,
            decimal iUserMoney, IDictionary<int, decimal> iLazyCardList,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            decimal iRealAmount, IDictionary<string, decimal> iActivityList,
            IList<int> iCouponIDList = null, bool iSalesPrice = true, string iUserRemark = null,
            string iCouponCode = null, Channel iChannel = Channel.Weixin, DateTime? iExpectTime = null,
            int? iSendType = 1, string iInviteCode = null)
        {
            Log_DeBug("Order_Weixin_Create", iOpenid, iSite,
               iProductIDList, iExpressFee, iExpressFeeDiscount,
               iUserMoney, iLazyCardList,
               iGetAddress, iSendAddress, iRealAmount, iActivityList,
               iCouponIDList, iSalesPrice, iUserRemark, iCouponCode, iChannel, iExpectTime,
               iSendType, iInviteCode);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Weixin_Create(iOpenid, iSite,
                        iProductIDList, iExpressFee, iExpressFeeDiscount,
                        iUserMoney, iLazyCardList,
                        iGetAddress, iSendAddress, iRealAmount, iActivityList,
                        iCouponIDList, iSalesPrice, iUserRemark, iCouponCode,
                        iChannel, iExpectTime, iSendType, iInviteCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Weixin_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Weixin_Create");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="ExpectTime"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_Onekey_Create(string iOpenid, int iSite, Channel iChannel,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, DateTime? iExpectTime,
            string iUserRemark = null, int? iUserCouponID = null, string iCouponCode = null, int? iSendType = 1,
            string iInviteCode = null, int iUseMoney = 0)
        {
            Log_DeBug("Order_Onekey_Create", iOpenid, iSite, iChannel, iGetAddress, iSendAddress, iExpectTime,
                iUserRemark, iUserCouponID, iCouponCode, iSendType, iInviteCode, iUseMoney);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Onekey_Create(iOpenid, iSite, iChannel, iGetAddress, iSendAddress,
                    iExpectTime, iUserRemark, iUserCouponID, iCouponCode, iSendType, iInviteCode, iUseMoney);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Onekey_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Onekey_Create");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 为一键下单订单添加产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Factory_AddProduct(int iOrderID, IList<Order_ProductDC> iProductList, int iMuser)
        {
            Log_DeBug("Order_Factory_AddProduct", iOrderID, iProductList, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Factory_AddProduct(iOrderID, iProductList, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Factory_AddProduct", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Factory_AddProduct");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Express_ADD_Factory(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            Log_DeBug("Order_Express_ADD_Factory", iOrderID, iExpressNumber, iRelationID, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Express_ADD_Factory(iOrderID, iExpressNumber, iRelationID, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Express_ADD_Factory", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Express_ADD_Factory");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 添加快递信息
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iRelationID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Express_ADD_Factory_Re(int iOrderID, string iExpressNumber, string iRelationID, int iMuser)
        {
            Log_DeBug("Order_Express_ADD_Factory_Re", iOrderID, iExpressNumber, iRelationID, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Express_ADD_Factory_Re(iOrderID, iExpressNumber, iRelationID, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Express_ADD_Factory_Re", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Express_ADD_Factory_Re");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 更新客服备注
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iCSRemark"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_UPDATE_CSRemark(int iOrderID, string iCSRemark)
        {
            Log_DeBug("Order_Order_UPDATE_CSRemark", iOrderID, iCSRemark);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_UPDATE_CSRemark(iOrderID, iCSRemark);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_UPDATE_CSRemark", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_UPDATE_CSRemark");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 更新订单收发货地址
        /// </summary>
        /// <param name="iOrder_ConsigneeAddress"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_ConsigneeAddress_UPDATE(Order_ConsigneeAddressDC iOrder_ConsigneeAddress)
        {
            Log_DeBug("Order_ConsigneeAddress_UPDATE", iOrder_ConsigneeAddress);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_ConsigneeAddress_UPDATE(iOrder_ConsigneeAddress);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_ConsigneeAddress_UPDATE", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_ConsigneeAddress_UPDATE");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 订单审核
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iAuditStatus"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_Audit(int iOrderID, bool iAuditStatus, int iMuser)
        {
            Log_DeBug("Order_Order_Audit", iOrderID, iAuditStatus, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Audit(iOrderID, iAuditStatus, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Audit", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Audit");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        #region 合作门店

        /// <summary>
        /// 门店订单生成
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <param name="iGetAddress"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_Order_Create_Store(Guid iStoreID, Order_ConsigneeAddressDC iGetAddress)
        {
            Log_DeBug("Order_Order_Create_Store", iStoreID, iGetAddress);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Create_Store(iStoreID, iGetAddress);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Create_Store", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Create_Store");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 根据门店ID查询订单主表
        /// </summary>
        /// <param name="iStoreID">门店ID</param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Order_SELECT_Entity(Guid iStoreID)
        {
            Log_DeBug("Order_Order_SELECT_Entity", iStoreID);
            try
            {
                return OrderInstance.Order_Order_SELECT_Entity(iStoreID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_SELECT_Entity", ex);
                return new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_SELECT_Entity");
            }
        }


        /// <summary>
        /// 门店订单添加产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Product_ADD_Store(int iOrderID, IList<Order_ProductDC> iProductList)
        {
            Log_DeBug("Order_Product_ADD_Store", iOrderID, iProductList);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_ADD_Store(iOrderID, iProductList);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_ADD_Store", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_ADD_Store");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 门店订单删除产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Product_DELETE_Store(int iOrderID, int iOrderProductID)
        {
            Log_DeBug("Order_Product_DELETE_Store", iOrderID, iOrderProductID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_DELETE_Store(iOrderID, iOrderProductID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_DELETE_Store", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_DELETE_Store");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 门店订单提交
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_Submit_Store(int iOrderID, string iExpressNumber)
        {
            Log_DeBug("Order_Order_Submit_Store", iOrderID, iExpressNumber);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Submit_Store(iOrderID, iExpressNumber);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Submit_Store", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Submit_Store");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 门店签收用户订单产品
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iUserName"></param>
        /// <param name="iUserSignType"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Product_UPDATE_UserSignType(int iOrderProductID, UserSignType iUserSignType)
        {
            Log_DeBug("Order_Product_UPDATE_UserSignType", iOrderProductID, iUserSignType);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_UPDATE_UserSignType(iOrderProductID, iUserSignType);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_UPDATE_UserSignType", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_UPDATE_UserSignType");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 门店查询用户未签收订单产品
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iUserName"></param>
        /// <param name="iUserSignType"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Order_ProductDC>> Order_Product_SELECT_List_Store(Guid iStoreID, string iUserMPNo,
            string iUserName, UserSignType? iUserSignType, DateTime iStartDate, DateTime iEndDate)
        {
            Log_DeBug("Order_Product_SELECT_List_Store", iStoreID, iUserMPNo,
                iUserName, iUserSignType, iStartDate, iEndDate);
            try
            {
                return OrderInstance.Order_Product_SELECT_List_Store(iStoreID, iUserMPNo,
                    iUserName, iUserSignType, iStartDate, iEndDate);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_SELECT_List_Store", ex);
                return new ReturnEntity<IList<Order_ProductDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_SELECT_List_Store");
            }
        }

        #endregion

        /// <summary>
        /// 顺丰客户下单
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <param name="j_Company"></param>
        /// <param name="j_Contact"></param>
        /// <param name="j_Tel"></param>
        /// <param name="j_Address"></param>
        /// <param name="j_City"></param>
        /// <param name="j_Province"></param>
        /// <param name="d_Company"></param>
        /// <param name="d_Contact"></param>
        /// <param name="d_Tel"></param>
        /// <param name="d_Address"></param>
        /// <param name="d_City"></param>
        /// <param name="d_Province"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<SF_OrderInfoDC> SF_CreateReverseOrder(string iOrderNumber,
            Order_ConsigneeAddressDC iGetAddress,
            DateTime? iExpectTime)
        {
            Log_DeBug("SF_CreateReverseOrder", iOrderNumber, iGetAddress, iExpectTime);
            try
            {
                return OrderInstance.SF_CreateReverseOrder(iOrderNumber, iGetAddress, iExpectTime);
            }
            catch (Exception ex)
            {
                Log_Fatal("SF_CreateReverseOrder", ex);
                return new ReturnEntity<SF_OrderInfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("SF_CreateReverseOrder");
            }
        }

        /// <summary>
        /// 订单取消
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<SF_OrderInfoDC> SF_CancelOrder(string iOrderid)
        {
            Log_DeBug("SF_CancelOrder", iOrderid);

            ReturnEntity<SF_OrderInfoDC> rtn = null;

            try
            {
                rtn = OrderInstance.SF_CancelOrder(iOrderid);
            }
            catch (Exception ex)
            {
                Log_Fatal("SF_CancelOrder", ex);
                rtn = new ReturnEntity<SF_OrderInfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("SF_CancelOrder");
            }
            return rtn;
        }

        /// <summary>
        /// 订单结果查询
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<SF_OrderInfoDC> SF_OrderSearch(string iOrderid)
        {
            Log_DeBug("SF_OrderSearch", iOrderid);
            try
            {
                return OrderInstance.SF_OrderSearch(iOrderid);
            }
            catch (Exception ex)
            {
                Log_Fatal("SF_OrderSearch", ex);
                return new ReturnEntity<SF_OrderInfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("SF_OrderSearch");
            }
        }

        /// <summary>
        /// 订单补快递单号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_FinishExpress(int iOrderID, string iExpressNumber)
        {
            Log_DeBug("Order_Order_FinishExpress", iOrderID, iExpressNumber);
            try
            {
                return OrderInstance.Order_Order_FinishExpress(iOrderID, iExpressNumber);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_FinishExpress", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_FinishExpress");
            }
        }

        /// <summary>
        /// 顺丰物流推送
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iDatetime"></param>
        /// <param name="iOpCode"></param>
        /// <param name="iRemark"></param>
        public ReturnEntity<bool> SF_Express_RoutePush(IList<RoutePushDataDC> iRouteList)
        {
            Log_DeBug("SF_Express_RoutePush", iRouteList);
            try
            {
                return OrderInstance.SF_Express_RoutePush(iRouteList);
            }
            catch (Exception ex)
            {
                Log_Fatal("SF_Express_RoutePush", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("SF_Express_RoutePush");
            }
        }

        /// <summary>
        /// 内部物流推送
        /// </summary>
        /// <param name="iRouteList"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Internal_ExpressRoutePush(IList<RoutePushDataDC> iRouteList)
        {
            Log_DeBug("Internal_ExpressRoutePush", iRouteList);
            try
            {
                return OrderInstance.Internal_ExpressRoutePush(iRouteList);
            }
            catch (Exception ex)
            {
                Log_Fatal("Internal_ExpressRoutePush", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Internal_ExpressRoutePush");
            }
        }

        /// <summary>
        /// 商城产品下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iProductList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="TotalAmount"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_Mall_Web_Create(Guid iUserID, int iSite,
               IDictionary<int, int> iProductList, decimal iExpressFee, decimal iExpressFeeDiscount,
               Order_ConsigneeAddressDC iSendAddress,
               decimal iRealAmount)
        {
            Log_DeBug("Order_Mall_Web_Create", iUserID, iSite, iProductList, iExpressFee, iExpressFeeDiscount, iSendAddress, iRealAmount);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Mall_Web_Create(iUserID, iSite, iProductList, iExpressFee, iExpressFeeDiscount, iSendAddress, iRealAmount);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Mall_Web_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Mall_Web_Create");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 支付订单(普通个人用户)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iPayType"></param>
        /// <param name="iPayChannel"></param>
        /// <param name="iPayRelationID"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_Pay(string iOrderNumber, decimal iPayMoney, PayMoneyType iPayType,
            PayMoneyChannel iPayChannel, DateTime iPayDate, string iPayRelationID)
        {
            Log_DeBug("Order_Order_Pay", iOrderNumber, iPayMoney, iPayType,
              iPayChannel, iPayDate, iPayRelationID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Pay(iOrderNumber, iPayMoney, iPayType,
                    iPayChannel, iPayDate, iPayRelationID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Pay", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Pay");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 合并订单
        /// </summary>
        /// <param name="iPrimaryOrderID"></param>
        /// <param name="iSlaveOrderID"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_Merger(int iPrimaryOrderID, int iSlaveOrderID)
        {
            Log_DeBug("Order_Order_Merger", iPrimaryOrderID, iSlaveOrderID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Merger(iPrimaryOrderID, iSlaveOrderID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Merger", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Merger");
            }
            if (rtn != null && rtn.Success && rtn.OBJ)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 快递合作商查询订单
        /// </summary>
        /// <param name="iUserName"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iExpressStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> Partner_Order_Express_SELECT_List(
            string iUserName, string iMPNo, int? iExpressStatus,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Partner_Order_Express_SELECT_List", iUserName, iMPNo, iExpressStatus, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Partner_Order_Express_SELECT_List(iUserName, iMPNo, iExpressStatus, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Partner_Order_Express_SELECT_List", ex);
                rtn = new ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Partner_Order_Express_SELECT_List");
            }

            return rtn;
        }

        /// <summary>
        /// 快递合作商查询订单
        /// </summary>
        /// <param name="iUserName"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iGetExpressNumber"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> Partner_Order_Express_SELECT_List(
            string iUserName, string iMPNo, string iGetExpressNumber,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Partner_Order_Express_SELECT_List", iUserName, iMPNo, iGetExpressNumber,
                iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Partner_Order_Express_SELECT_List(iUserName, iMPNo, iGetExpressNumber,
                    iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Partner_Order_Express_SELECT_List", ex);
                rtn = new ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Partner_Order_Express_SELECT_List");
            }

            return rtn;
        }

        /// <summary>
        /// 快递合作商更新快递状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpressStatus"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Partner_Order_Express_UPDATE_ExpressStatus(int iOrderID, int iExpressStatus)
        {
            Log_DeBug("Partner_Order_Express_UPDATE_ExpressStatus", iOrderID, iExpressStatus);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Partner_Order_Express_UPDATE_ExpressStatus(iOrderID, iExpressStatus);
            }
            catch (Exception ex)
            {
                Log_Fatal("Partner_Order_Express_UPDATE_ExpressStatus", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Partner_Order_Express_UPDATE_ExpressStatus");
            }

            return rtn;
        }

        /// <summary>
        /// 一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_App_Onekey_Create(Guid iUserID, int iSite,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            DateTime? iExpectTime, string iUserRemark, string iCouponCode = null,
            int? iSendType = 1, string iInviteCode = null, int iUseMoney = 0)
        {
            Log_DeBug("Order_App_Onekey_Create", iUserID, iSite, iGetAddress, iSendAddress, iExpectTime, iUserRemark,
                  iCouponCode, iSendType, iInviteCode, iUseMoney);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_App_Onekey_Create(iUserID, iSite, iGetAddress, iSendAddress,
                    iExpectTime, iUserRemark, iCouponCode, iSendType, iInviteCode, iUseMoney);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_App_Onekey_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_App_Onekey_Create");
            }
            if (rtn != null && rtn.Success && rtn.OBJ != null)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iProductIDList"></param>
        /// <param name="iExpressFee"></param>
        /// <param name="iExpressFeeDiscount"></param>
        /// <param name="iUserMoney"></param>
        /// <param name="iLazyCardList"></param>
        /// <param name="iCouponIDList"></param>
        /// <param name="iActivityList"></param>
        /// <param name="iServiceList"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_App_Create(Guid iUserID, int iSite,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            IList<int> iProductIDList,
            decimal iExpressFee, decimal iExpressFeeDiscount,
            decimal iUserMoney, IDictionary<int, decimal> iLazyCardList, IList<int> iCouponIDList,
            IDictionary<string, decimal> iActivityList,
            IDictionary<string, decimal> iServiceList,
            DateTime? iExpectTime, string iUserRemark,
            int? iSendType = 1, string iInviteCode = null)
        {
            Log_DeBug("Order_App_Create", iUserID, iSite,
                iGetAddress, iSendAddress,
                iProductIDList,
                iExpressFee, iExpressFeeDiscount,
                iUserMoney, iLazyCardList, iCouponIDList,
                iActivityList,
                iServiceList,
                iExpectTime, iUserRemark,
                iSendType, iInviteCode);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_App_Create(iUserID, iSite,
                    iGetAddress, iSendAddress,
                    iProductIDList,
                    iExpressFee, iExpressFeeDiscount,
                    iUserMoney, iLazyCardList, iCouponIDList,
                    iActivityList,
                    iServiceList,
                    iExpectTime, iUserRemark,
                    iSendType, iInviteCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_App_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_App_Create");
            }
            if (rtn != null && rtn.Success && rtn.OBJ != null)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 新增 Order_Feedback
        /// </summary>
        /// <param name="iOrder_FeedbackDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Order_Feedback_ADD(Order_FeedbackDC iOrder_FeedbackDC)
        {
            Log_DeBug("Order_Feedback_ADD", iOrder_FeedbackDC);

            ReturnEntity<int> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Feedback_ADD(iOrder_FeedbackDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Feedback_ADD", ex);
                rtn = new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Feedback_ADD");
            }
            return rtn;
        }

        /// <summary>
        /// 一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<Order_OrderDC> Order_Onekey_Create(Guid iUserID, int iSite, Channel iChannel,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress, DateTime? iExpectTime,
            string iUserRemark = null, int? iUserCouponID = null, string iCouponCode = null,
            int? iSendType = 1, string iInviteCode = null, int iUseMoney = 0)
        {
            Log_DeBug("Order_Onekey_Create", iUserID, iSite, iChannel, iGetAddress, iSendAddress,
                iExpectTime, iUserRemark, iUserCouponID, iCouponCode, iSendType, iInviteCode, iUseMoney);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Onekey_Create(iUserID, iSite, iChannel, iGetAddress, iSendAddress,
                    iExpectTime, iUserRemark, iUserCouponID, iCouponCode, iSendType, iInviteCode, iUseMoney);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Onekey_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Onekey_Create");
            }
            return rtn;
        }

        /// <summary>
        /// 重新推送快递
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_InternalExpressOrder_ReCreate(string iOrderNumber)
        {
            Log_DeBug("Order_Order_InternalExpressOrder_ReCreate", iOrderNumber);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_InternalExpressOrder_ReCreate(iOrderNumber);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_InternalExpressOrder_ReCreate", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_InternalExpressOrder_ReCreate");
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Step_Change(int iOrderID, WashStepType iStep)
        {
            Log_DeBug("Order_Step_Change", iOrderID, iStep);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Step_Change(iOrderID, iStep);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Step_Change", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Step_Change");
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_ExpectTime_Change(int iOrderID, DateTime iExpectTime)
        {
            Log_DeBug("Order_ExpectTime_Change", iOrderID, iExpectTime);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_ExpectTime_Change(iOrderID, iExpectTime);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_ExpectTime_Change", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_ExpectTime_Change");
            }
            return rtn;
        }

        /// <summary>
        /// 客服一键下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <param name="iUserCouponID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<Order_OrderDC> Order_CustomerService_Onekey_Create(Guid iUserID,
               Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            DateTime? iExpectTime, string iUserRemark, int? iUserCouponID, int iMuser,
            string iCouponCode = null, int? iSendType = 1, string iInviteCode = null, int iUseMoney = 0)
        {
            Log_DeBug("Order_CustomerService_Onekey_Create",
                iUserID, iGetAddress, iSendAddress, iExpectTime, iUserRemark, iUserCouponID, iMuser,
                iCouponCode, iSendType, iInviteCode, iUseMoney);

            ReturnEntity<Order_OrderDC> rtn = null;

            try
            {
                rtn = OrderInstance.Order_CustomerService_Onekey_Create(
                    iUserID, iGetAddress, iSendAddress, iExpectTime, iUserRemark, iUserCouponID, iMuser,
                    iCouponCode, iSendType, iInviteCode, iUseMoney);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_CustomerService_Onekey_Create", ex);
                rtn = new ReturnEntity<Order_OrderDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_CustomerService_Onekey_Create");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 修改产品步骤
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_Step(int iOrderID, int iOrderProductID, int iStep)
        {
            Log_DeBug("Order_Product_UPDATE_Step", iOrderID, iOrderProductID, iStep);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_UPDATE_Step(iOrderID, iOrderProductID, iStep);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_UPDATE_Step", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_UPDATE_Step");
            }
            return rtn;
        }

        /// <summary>
        /// 工厂出库
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<int> Order_Product_OutFactory(int iOrderID, int iOrderProductID, string iExpressNumber, int iMuser)
        {
            Log_DeBug("Order_Product_OutFactory", iOrderID, iOrderProductID, iExpressNumber, iMuser);

            ReturnEntity<int> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_OutFactory(iOrderID, iOrderProductID, iExpressNumber, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_OutFactory", ex);
                rtn = new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_OutFactory");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 工厂洗涤条码导出
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Product_WashStep_Code_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Product_WashStep_Code_Stat", iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_WashStep_Code_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_WashStep_Code_Stat", ex);
                rtn = new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_WashStep_Code_Stat");
            }
            return rtn;
        }

        /// <summary>
        /// 订单步骤时间统计
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_StepTime_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_StepTime_Stat", iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_StepTime_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_StepTime_Stat", ex);
                rtn = new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_StepTime_Stat");
            }
            return rtn;
        }


        /// <summary>
        /// 订单出库查询
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_OutFactory_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_OutFactory_Stat", iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_OutFactory_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_OutFactory_Stat", ex);
                rtn = new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_OutFactory_Stat");
            }
            return rtn;
        }

        /// <summary>
        /// 订单入库查询
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_InFactory_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_InFactory_Stat", iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_InFactory_Stat(iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_InFactory_Stat", ex);
                rtn = new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_InFactory_Stat");
            }
            return rtn;
        }

        /// <summary>
        /// 修改物流类型
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Order_UPDATE_GetExpressType(int iOrderID, int iType)
        {
            Log_DeBug("Order_Order_UPDATE_GetExpressType", iOrderID, iType);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_UPDATE_GetExpressType(iOrderID, iType);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_UPDATE_GetExpressType", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_UPDATE_GetExpressType");
            }
            return rtn;
        }

        /// <summary>
        /// 订单步骤时间预警
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_Alarm_Stat(
            bool iGetPackage, bool iWash, bool iSendPackage, bool iAll,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_Alarm_Stat", iGetPackage, iWash, iSendPackage, iAll,
                iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Alarm_Stat(iGetPackage, iWash, iSendPackage, iAll,
              iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Alarm_Stat", ex);
                rtn = new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Alarm_Stat");
            }
            return rtn;
        }

        /// <summary>
        /// 订单步骤时间预警
        /// </summary>
        /// <param name="iGetPackage"></param>
        /// <param name="iWash"></param>
        /// <param name="iSendPackage"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Order_Order_StatDC>> Order_Order_Warning_Stat(
            bool iGetPackage, bool iWash, bool iSendPackage,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_Warning_Stat", iGetPackage, iWash, iSendPackage,
                iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Order_Order_StatDC>> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Warning_Stat(iGetPackage, iWash, iSendPackage,
              iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Warning_Stat", ex);
                rtn = new ReturnEntity<RecordCountEntity<Order_Order_StatDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Warning_Stat");
            }
            return rtn;
        }

        /// <summary>
        /// 更新外部编号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iOtherCode"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_Product_UPDATE_OtherCode(int iOrderID, int iOrderProductID, string iOtherCode)
        {
            Log_DeBug("Order_Product_UPDATE_OtherCode", iOrderID, iOrderProductID, iOtherCode);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_UPDATE_OtherCode(iOrderID, iOrderProductID, iOtherCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_UPDATE_OtherCode", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_UPDATE_OtherCode");
            }
            return rtn;
        }

        /// <summary>
        /// 客服商务订单下单
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iProductName"></param>
        /// <param name="iPrice"></param>
        /// <param name="iCount"></param>
        /// <param name="iExpectTime"></param>
        /// <param name="iUserRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_CustomerService_BusinessOrder_Create(Guid iUserID,
            Order_ConsigneeAddressDC iGetAddress, Order_ConsigneeAddressDC iSendAddress,
            string iProductName, decimal iPrice, int iCount,
            DateTime? iExpectTime, string iUserRemark, int iMuser)
        {
            Log_DeBug("Order_CustomerService_BusinessOrder_Create", iUserID, iGetAddress, iSendAddress,
                iProductName, iPrice, iCount, iExpectTime, iUserRemark, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_CustomerService_BusinessOrder_Create(iUserID, iGetAddress, iSendAddress,
                    iProductName, iPrice, iCount, iExpectTime, iUserRemark, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_CustomerService_BusinessOrder_Create", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_CustomerService_BusinessOrder_Create");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 修改订单产品内容(客服权限)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Order_EditProduct(int iOrderID, IList<Order_ProductDC> iProductList, int iMuser)
        {
            Log_DeBug("Order_Order_EditProduct", iOrderID, iProductList, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_EditProduct(iOrderID, iProductList, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_EditProduct", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_EditProduct");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 修改产品是否可洗涤(客服权限)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iProductID"></param>
        /// <param name="iWashStatus">1:可以洗,2:无法洗</param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Product_WashStatus(int iOrderID, int iProductID, int iWashStatus, int iMuser)
        {
            Log_DeBug("Order_Product_WashStatus", iOrderID, iProductID, iWashStatus, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Product_WashStatus(iOrderID, iProductID, iWashStatus, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Product_WashStatus", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Product_WashStatus");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 快递当面下单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Order_Onekey_Create_Express(string iOrderNumber, string iInviteCode)
        {
            Log_DeBug("Order_Onekey_Create_Express", iOrderNumber, iInviteCode);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Onekey_Create_Express(iOrderNumber, iInviteCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Onekey_Create_Express", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Onekey_Create_Express");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 订单导出
        /// </summary>
        /// <param name="iKeyword"></param>
        /// <param name="iCreateStartDate"></param>
        /// <param name="iCreateEndDate"></param>
        /// <param name="iFinishStartDate"></param>
        /// <param name="iFinishEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<IList<string[]>> Order_Order_Export(
            string iKeyword,
            DateTime? iCreateStartDate, DateTime? iCreateEndDate,
            DateTime? iFinishStartDate, DateTime? iFinishEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Order_Order_Export",
                        iKeyword,
                        iCreateStartDate, iCreateEndDate,
                        iFinishStartDate, iFinishEndDate,
                        iPageIndex, iPageSize);

            ReturnEntity<IList<string[]>> rtn = null;

            try
            {
                rtn = OrderInstance.Order_Order_Export(
                        iKeyword,
                        iCreateStartDate, iCreateEndDate,
                        iFinishStartDate, iFinishEndDate,
                        iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_Order_Export", ex);
                rtn = new ReturnEntity<IList<string[]>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_Order_Export");
            }
            return rtn;
        }
        /// <summary>
        /// 微信支付回调通知,更新支付记录,订单状态
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Order_WeiXinPay_Notice(string iOrderNumber)
        {
            Log_DeBug("Order_WeiXinPay_Notice", iOrderNumber);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = OrderInstance.Order_WeiXinPay_Notice(iOrderNumber);
            }
            catch (Exception ex)
            {
                Log_Fatal("Order_WeiXinPay_Notice", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Order_WeiXinPay_Notice");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }
    }
}
