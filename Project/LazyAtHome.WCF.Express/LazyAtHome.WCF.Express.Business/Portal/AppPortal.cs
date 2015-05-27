using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.Express.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Portal
{
    public partial class ExpressPortal : IAppExpress
    {
        protected LazyAtHome.WCF.Express.Business.Business.AppExpress AppExpressInstance = LazyAtHome.WCF.Express.Business.Business.AppExpress.Instance;

        /// <summary>
        /// 待接列表
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitAccept(int iOperatorID)
        {
            Log_DeBug("Exp_Order_SELECT_WaitAccept", iOperatorID);

            var list = AppExpressInstance.Exp_Order_SELECT_WaitAccept(iOperatorID);

            return list;
        }

        //接单
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Order_Accept(int iOrderID, int iOperatorID)
        {
            Log_DeBug("Exp_Order_Accept", iOrderID, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_Accept(iOrderID, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_Accept", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_Accept");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        //拒接单
        public ReturnEntity<bool> Exp_Order_UnAccept(int iOrderID, int iOperatorID)
        {
            Log_DeBug("Exp_Order_UnAccept", iOrderID, iOperatorID);

            var rtn = AppExpressInstance.Exp_Order_UnAccept(iOrderID, iOperatorID);

            return rtn;
        }

        //待收件列表
        public ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitTake(int iOperatorID)
        {
            Log_DeBug("Exp_Order_SELECT_WaitTake", iOperatorID);

            var list = AppExpressInstance.Exp_Order_SELECT_WaitTake(iOperatorID);

            return list;
        }

        //待收件详情
        public ReturnEntity<Exp_OrderDC> Exp_Order_SELECT_WaitSend_Detail(int iOrderID, int iOperatorID)
        {
            Log_DeBug("Exp_Order_SELECT_WaitSend_Detail", iOrderID, iOperatorID);

            var list = AppExpressInstance.Exp_Order_SELECT_WaitSend_Detail(iOrderID, iOperatorID);

            return list;
        }

        //收件完成
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Order_TakeComplete(int iOrderID, string iExpNumber, int iOperatorID)
        {
            Log_DeBug("Exp_Order_TakeComplete", iOrderID, iExpNumber, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_TakeComplete(iOrderID, iExpNumber, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_TakeComplete", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_TakeComplete");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        //收件失败
        public ReturnEntity<bool> Exp_Order_TakeFail(int iOrderID, int iOperatorID, string iFailReason, DateTime? iChangeTime)
        {
            Log_DeBug("Exp_Order_TakeFail", iOrderID, iOperatorID, iFailReason, iChangeTime);

            var rtn = AppExpressInstance.Exp_Order_TakeFail(iOrderID, iOperatorID, iFailReason, iChangeTime);

            return rtn;
        }

        //扫描揽件
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Order_TakeSend(IList<string> iExpNumberList, int iOperatorID)
        {
            Log_DeBug("Exp_Order_TakeSend", iExpNumberList, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_TakeSend(iExpNumberList, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_TakeSend", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_TakeSend");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        //待送件列表
        public ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitSend(int iOperatorID)
        {
            Log_DeBug("Exp_Order_SELECT_WaitSend", iOperatorID);

            var list = AppExpressInstance.Exp_Order_SELECT_WaitSend(iOperatorID);

            return list;
        }

        //送件完成
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Order_SendComplete(int iOrderID, int iOperatorID)
        {
            Log_DeBug("Exp_Order_SendComplete", iOrderID, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_SendComplete(iOrderID, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_SendComplete", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_SendComplete");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }




        //干线揽站点包裹数量
        public ReturnEntity<string> Exp_Storage_LineTakeSite_Count(int iSiteID, int iOperatorID)
        {
            Log_DeBug("Exp_Storage_LineTakeSite_Count", iSiteID, iOperatorID);

            ReturnEntity<string> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_LineTakeSite_Count(iSiteID, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_LineTakeSite_Count", ex);
                rtn = new ReturnEntity<string>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_LineTakeSite_Count");
            }

            return rtn;
        }

        //干线揽站点包裹
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Storage_LineTakeSite(string iExpnumber, int iSiteID, int iOperatorID)
        {
            Log_DeBug("Exp_Storage_LineTakeSite", iExpnumber, iSiteID, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_LineTakeSite(iExpnumber, iSiteID, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_LineTakeSite", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_LineTakeSite");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        //工厂揽站点数量
        public ReturnEntity<string> Exp_Storage_FactoryTakeLine_Count(int iOperatorID)
        {
            Log_DeBug("Exp_Storage_FactoryTakeLine_Count", iOperatorID);

            ReturnEntity<string> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_FactoryTakeLine_Count(iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_FactoryTakeLine_Count", ex);
                rtn = new ReturnEntity<string>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_FactoryTakeLine_Count");
            }

            return rtn;
        }

        //工厂揽站点完成
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete(int iOperatorID)
        {
            Log_DeBug("Exp_Storage_FactoryTakeLine_Complete", iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_FactoryTakeLine_Complete(iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_FactoryTakeLine_Complete", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_FactoryTakeLine_Complete");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        //干线揽工厂数量
        public ReturnEntity<string> Exp_Storage_LineTakeFactory_Count(int iOperatorID)
        {
            Log_DeBug("Exp_Storage_LineTakeFactory_Count", iOperatorID);

            ReturnEntity<string> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_LineTakeFactory_Count(iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_LineTakeFactory_Count", ex);
                rtn = new ReturnEntity<string>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_LineTakeFactory_Count");
            }

            return rtn;
        }

        //干线揽工厂衣服
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Storage_LineTakeFactory(string iCode, int iOperatorID)
        {
            Log_DeBug("Exp_Storage_LineTakeFactory", iCode, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_LineTakeFactory(iCode, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_LineTakeFactory", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_LineTakeFactory");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        //站点揽干线数量
        public ReturnEntity<string> Exp_Storage_SiteTakeLine_Count(int iOperatorID)
        {
            Log_DeBug("Exp_Storage_SiteTakeLine_Count", iOperatorID);

            ReturnEntity<string> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_SiteTakeLine_Count(iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_SiteTakeLine_Count", ex);
                rtn = new ReturnEntity<string>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_SiteTakeLine_Count");
            }

            return rtn;
        }

        //站点入库
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> Exp_Storage_SiteTakeLine(string iCode, int iOperatorID)
        {
            Log_DeBug("Exp_Storage_SiteTakeLine", iCode, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_SiteTakeLine(iCode, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_SiteTakeLine", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_SiteTakeLine");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }

            return rtn;
        }

        /// <summary>
        /// 电话联系用户
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStatus"></param>
        /// <param name="iCallTime"></param>
        /// <param name="iSecond"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_CallUser(int iOrderID, int iStatus, DateTime iCallTime, int iSecond, int iMuser)
        {
            Log_DeBug("Exp_Order_UPDATE_CallUser", iOrderID, iStatus, iCallTime, iSecond, iMuser);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_UPDATE_CallUser(iOrderID, iStatus, iCallTime, iSecond, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_UPDATE_CallUser", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_UPDATE_CallUser");
            }

            return rtn;
        }

        /// <summary>
        /// 获取干线下的站点
        /// </summary>
        /// <param name="iLineID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Exp_NodeDC>> Exp_Node_LineSite(int iLineID)
        {
            Log_DeBug("Exp_Node_LineSite", iLineID);

            ReturnEntity<IList<Exp_NodeDC>> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Node_LineSite(iLineID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Node_LineSite", ex);
                rtn = new ReturnEntity<IList<Exp_NodeDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Node_LineSite");
            }

            return rtn;
        }

        /// <summary>
        /// 站点入库异常
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_SiteTakeLine_FailNotify(int iOperatorID)
        {
            Log_DeBug("Exp_Storage_SiteTakeLine_FailNotify", iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_SiteTakeLine_FailNotify(iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_SiteTakeLine_FailNotify", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_SiteTakeLine_FailNotify");
            }

            return rtn;
        }

        /// <summary>
        /// 干线取站点件异常
        /// </summary>
        /// <param name="iLineID"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_LineTakeSite_FailNotify(int iSiteID, int iOperatorID)
        {
            Log_DeBug("Exp_Storage_LineTakeSite_FailNotify", iSiteID, iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_LineTakeSite_FailNotify(iSiteID, iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_LineTakeSite_FailNotify", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_LineTakeSite_FailNotify");
            }

            return rtn;
        }

        /// <summary>
        /// 干线取工厂件异常
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_LineTakeFactory_FailNotify(int iOperatorID)
        {
            Log_DeBug("Exp_Storage_LineTakeFactory_FailNotify", iOperatorID);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Storage_LineTakeFactory_FailNotify(iOperatorID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Storage_LineTakeFactory_FailNotify", ex);
                rtn = new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Storage_LineTakeFactory_FailNotify");
            }

            return rtn;
        }

        //站点收件日志
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_SiteTakeUser_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Order_SELECT_SiteTakeUser_Log", iOperatorID, iType, iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Exp_OrderDC>> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_SELECT_SiteTakeUser_Log(iOperatorID, iType, iStartDate, iEndDate, 
                    iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_SELECT_SiteTakeUser_Log", ex);
                rtn = new ReturnEntity<RecordCountEntity<Exp_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_SELECT_SiteTakeUser_Log");
            }

            return rtn;
        }

        //站点取干线日志 按入库时间搜
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_SiteTakeLine_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Order_SELECT_SiteTakeLine_Log", iOperatorID, iType, iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Exp_OrderDC>> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_SELECT_SiteTakeLine_Log(iOperatorID, iType, iStartDate, iEndDate,
                    iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_SELECT_SiteTakeLine_Log", ex);
                rtn = new ReturnEntity<RecordCountEntity<Exp_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_SELECT_SiteTakeLine_Log");
            }

            return rtn;
        }

        //干线取站点日志
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_LineTakeSite_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Order_SELECT_LineTakeSite_Log", iOperatorID, iType, iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Exp_OrderDC>> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_SELECT_LineTakeSite_Log(iOperatorID, iType, iStartDate, iEndDate,
                    iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_SELECT_LineTakeSite_Log", ex);
                rtn = new ReturnEntity<RecordCountEntity<Exp_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_SELECT_LineTakeSite_Log");
            }

            return rtn;
        }

        //干线取工厂日志
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_LineFactory_Log(int iOperatorID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Exp_Order_SELECT_LineFactory_Log", iOperatorID, iType, iStartDate, iEndDate, iPageIndex, iPageSize);

            ReturnEntity<RecordCountEntity<Exp_OrderDC>> rtn = null;

            try
            {
                rtn = AppExpressInstance.Exp_Order_SELECT_LineFactory_Log(iOperatorID, iType, iStartDate, iEndDate,
                    iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Exp_Order_SELECT_LineFactory_Log", ex);
                rtn = new ReturnEntity<RecordCountEntity<Exp_OrderDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Exp_Order_SELECT_LineFactory_Log");
            }

            return rtn;
        }
    }
}
