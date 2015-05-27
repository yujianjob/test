using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Express.Contract.ClientProxy;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.App_Code.Proxy
{
    public class AppExpressProxy
    {
        //public static ReturnEntity<Exp_OperatorDC> Exp_Operator_Login(string iLoginName, string iLoginPassword)
        //{
        //    var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<Exp_OperatorDC>>(
        //        c => c.Proxy.Exp_Operator_Login(iLoginName, iLoginPassword));

        //    return rtn;
        //}

        //public static ReturnEntity<bool> Exp_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty)
        //{
        //    var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
        //        c => c.Proxy.Exp_Operator_UPDATE_OnDuty(iOperatorID, iOnDuty));

        //    return rtn;
        //}

        public static ReturnEntity<OperatorDC> PR_Operator_Login(string iLoginName, string iLoginPassword)
        {
            var rtn = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<OperatorDC>>(
                c => c.Proxy.PR_Operator_Login(iLoginName, iLoginPassword, WCF.Common.Contract.Enumerate.OperatorType.ALL));

            return rtn;
        }
        public static ReturnEntity<bool> PR_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty)
        {
            var rtn = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.PR_Operator_UPDATE_OnDuty(iOperatorID, iOnDuty));

            return rtn;
        }

        public static ReturnEntity<OperatorDC> PR_Operator_SELECT_BYID(int iID)
        {
            var rtn = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<OperatorDC>>(
                c => c.Proxy.PR_Operator_SELECT_BYID(iID));

            return rtn;
        }

        public static ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitAccept(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<IList<Exp_OrderDC>>>(
                c => c.Proxy.Exp_Order_SELECT_WaitAccept(iOperatorID));

            return rtn;
        }


        //接单
        public static ReturnEntity<bool> Exp_Order_Accept(int iOrderID, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Order_Accept(iOrderID, iOperatorID));

            return rtn;
        }

        //拒接单
        public static ReturnEntity<bool> Exp_Order_UnAccept(int iOrderID, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Order_UnAccept(iOrderID, iOperatorID));

            return rtn;
        }

        //待收件列表
        public static ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitTake(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<IList<Exp_OrderDC>>>(
                c => c.Proxy.Exp_Order_SELECT_WaitTake(iOperatorID));

            return rtn;
        }

        //收件完成
        public static ReturnEntity<bool> Exp_Order_TakeComplete(int iOrderID, string iExpNumber, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Order_TakeComplete(iOrderID, iExpNumber, iOperatorID));

            return rtn;
        }

        //收件失败
        public static ReturnEntity<bool> Exp_Order_TakeFail(int iOrderID, int iOperatorID, string iFailReason, DateTime? iChangeTime)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Order_TakeFail(iOrderID, iOperatorID, iFailReason, iChangeTime));

            return rtn;
        }
        //扫描揽件
        public static ReturnEntity<bool> Exp_Order_TakeSend(IList<string> iExpNumberList, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Order_TakeSend(iExpNumberList, iOperatorID));

            return rtn;
        }

        //待送件列表
        public static ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitSend(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<IList<Exp_OrderDC>>>(
                c => c.Proxy.Exp_Order_SELECT_WaitSend(iOperatorID));

            return rtn;
        }

        //待收件详情
        public static ReturnEntity<Exp_OrderDC> Exp_Order_SELECT_WaitSend_Detail(int iOrderID, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<Exp_OrderDC>>(
                c => c.Proxy.Exp_Order_SELECT_WaitSend_Detail(iOrderID, iOperatorID));

            return rtn;
        }


        //送件完成
        public static ReturnEntity<bool> Exp_Order_SendComplete(int iOrderID, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Order_SendComplete(iOrderID, iOperatorID));

            return rtn;
        }



        //干线揽站点包裹数量
        public static ReturnEntity<string> Exp_Storage_LineTakeSite_Count(int iSiteID, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<string>>(
                 c => c.Proxy.Exp_Storage_LineTakeSite_Count(iSiteID, iOperatorID));

            return rtn;
        }

        //干线揽站点包裹
        public static ReturnEntity<bool> Exp_Storage_LineTakeSite(string iExpnumber, int iSiteID, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Storage_LineTakeSite(iExpnumber, iSiteID, iOperatorID));

            return rtn;
        }

        //工厂揽站点数量
        public static ReturnEntity<string> Exp_Storage_FactoryTakeLine_Count(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<string>>(
                c => c.Proxy.Exp_Storage_FactoryTakeLine_Count(iOperatorID));

            return rtn;
        }

        //工厂揽站点完成
        public static ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Storage_FactoryTakeLine_Complete(iOperatorID));

            return rtn;
        }

        //干线揽工厂数量
        public static ReturnEntity<string> Exp_Storage_LineTakeFactory_Count(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<string>>(
               c => c.Proxy.Exp_Storage_LineTakeFactory_Count(iOperatorID));

            return rtn;
        }

        //干线揽工厂衣服
        public static ReturnEntity<bool> Exp_Storage_LineTakeFactory(string iCode, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Storage_LineTakeFactory(iCode, iOperatorID));

            return rtn;
        }

        //站点揽干线数量
        public static ReturnEntity<string> Exp_Storage_SiteTakeLine_Count(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<string>>(
                c => c.Proxy.Exp_Storage_SiteTakeLine_Count(iOperatorID));

            return rtn;
        }

        //站点揽干线衣服
        public static ReturnEntity<bool> Exp_Storage_SiteTakeLine(string iCode, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Storage_SiteTakeLine(iCode, iOperatorID));

            return rtn;
        }

        //电话联系用户
        public static ReturnEntity<bool> Exp_Order_UPDATE_CallUser(int iOrderID, int iStatus, DateTime iCallTime, int iSecond, int iMuser)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Order_UPDATE_CallUser(iOrderID, iStatus, iCallTime, iSecond, iMuser));

            return rtn;
        }

        /// <summary>
        /// 获取干线下的站点
        /// </summary>
        /// <param name="iLineID"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<Exp_NodeDC>> Exp_Node_LineSite(int iLineID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<IList<Exp_NodeDC>>>(
               c => c.Proxy.Exp_Node_LineSite(iLineID));

            return rtn;
        }

        //
        public static ReturnEntity<Exp_NodeDC> Exp_Node_SELECT_Entity(int iNodeID)
        {
            var rtn = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_NodeDC>>(
               c => c.Proxy.Exp_Node_SELECT_Entity(iNodeID));

            return rtn;
        }

        /// <summary>
        /// 站点入库异常
        /// </summary>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Exp_Storage_SiteTakeLine_FailNotify(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Storage_SiteTakeLine_FailNotify(iOperatorID));

            return rtn;
        }

        /// <summary>
        /// 干线取站点件异常
        /// </summary>
        /// <param name="iLineID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Exp_Storage_LineTakeSite_FailNotify(int iSiteID, int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Storage_LineTakeSite_FailNotify(iSiteID, iOperatorID));

            return rtn;
        }

        /// <summary>
        /// 干线取工厂件异常
        /// </summary>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Exp_Storage_LineTakeFactory_FailNotify(int iOperatorID)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.Exp_Storage_LineTakeFactory_FailNotify(iOperatorID));

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public static ReturnEntity<Exp_Preson_AccountDC> Exp_Preson_Account_SELECT_Entity_UserID(int iUserID)
        {
            var rtn = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_Preson_AccountDC>>(
                c => c.Proxy.Exp_Preson_Account_SELECT_Entity_UserID(iUserID));

            return rtn;
        }

        /// <summary>
        /// 查询全部Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static ReturnEntity<RecordCountEntity<Exp_Preson_CommissionLogDC>> Exp_Preson_CommissionLog_SELECT_List(
            int? iUserID, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_Preson_CommissionLogDC>>>(
             c => c.Proxy.Exp_Preson_CommissionLog_SELECT_List(iUserID, iPageIndex, iPageSize));

            return rtn;
        }

        public static ReturnEntity<Exp_OrderDC> Exp_Order_SELECT_Entity(int iOrderID)
        {
            var rtn = WCFInvokeHelper<ExpressClient>.Invoke<ReturnEntity<Exp_OrderDC>>(
                c => c.Proxy.Exp_Order_SELECT_Entity(iOrderID));

            return rtn;
        }

        /// <summary>
        /// 快递当面下单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Order_Onekey_Create_Express(string iOrderNumber, string iInviteCode)
        {
            var rtn = WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(
             c => c.Proxy.Order_Onekey_Create_Express(iOrderNumber, iInviteCode));

            return rtn;
        }

        //站点收件日志
        public static ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_SiteTakeUser_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_OrderDC>>>(
                c => c.Proxy.Exp_Order_SELECT_SiteTakeUser_Log(iOperatorID, iType,
                    iStartDate, iEndDate, iPageIndex, iPageSize));

            return rtn;
        }

        //站点取干线日志 按入库时间搜
        public static ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_SiteTakeLine_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_OrderDC>>>(
                c => c.Proxy.Exp_Order_SELECT_SiteTakeLine_Log(iOperatorID, iType,
                    iStartDate, iEndDate, iPageIndex, iPageSize));

            return rtn;
        }

        //干线取站点日志
        public static ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_LineTakeSite_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_OrderDC>>>(
                c => c.Proxy.Exp_Order_SELECT_LineTakeSite_Log(iOperatorID, iType,
                    iStartDate, iEndDate, iPageIndex, iPageSize));

            return rtn;
        }

        //干线取工厂日志
        public static ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_LineFactory_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<AppExpressClient>.Invoke<ReturnEntity<RecordCountEntity<Exp_OrderDC>>>(
                c => c.Proxy.Exp_Order_SELECT_LineFactory_Log(iOperatorID, iType,
                    iStartDate, iEndDate, iPageIndex, iPageSize));

            return rtn;
        }

    }
}