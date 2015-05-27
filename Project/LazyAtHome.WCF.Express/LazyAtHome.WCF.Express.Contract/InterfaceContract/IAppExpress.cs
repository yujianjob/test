using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAppExpress
    {
        //[OperationContract]
        //ReturnEntity<Exp_OperatorDC> Exp_Operator_Login(string iLoginName, string iLoginPassword);

        ///// <summary>
        ///// 上岗下岗
        ///// </summary>
        ///// <param name="iOperatorID"></param>
        ///// <param name="iOnDuty"></param>
        ///// <returns></returns>
        //[OperationContract]
        //ReturnEntity<bool> Exp_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty);

        /// <summary>
        /// 待接列表
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitAccept(int iOperatorID);

        //接单
        [OperationContract]
        ReturnEntity<bool> Exp_Order_Accept(int iOrderID, int iOperatorID);

        //拒接单
        [OperationContract]
        ReturnEntity<bool> Exp_Order_UnAccept(int iOrderID, int iOperatorID);

        //待收件列表
        [OperationContract]
        ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitTake(int iOperatorID);

        //待收件详情
        [OperationContract]
        ReturnEntity<Exp_OrderDC> Exp_Order_SELECT_WaitSend_Detail(int iOrderID, int iOperatorID);

        //收件完成
        [OperationContract]
        ReturnEntity<bool> Exp_Order_TakeComplete(int iOrderID, string iExpNumber, int iOperatorID);

        //收件失败
        [OperationContract]
        ReturnEntity<bool> Exp_Order_TakeFail(int iOrderID, int iOperatorID, string iFailReason, DateTime? iChangeTime);

        //扫描揽件
        [OperationContract]
        ReturnEntity<bool> Exp_Order_TakeSend(IList<string> iExpNumberList, int iOperatorID);

        //待送件列表
        [OperationContract]
        ReturnEntity<IList<Exp_OrderDC>> Exp_Order_SELECT_WaitSend(int iOperatorID);

        //送件完成
        [OperationContract]
        ReturnEntity<bool> Exp_Order_SendComplete(int iOrderID, int iOperatorID);

        //干线揽站点包裹数量
        [OperationContract]
        ReturnEntity<string> Exp_Storage_LineTakeSite_Count(int iSiteID, int iOperatorID);

        //干线揽站点包裹
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_LineTakeSite(string iExpnumber, int iSiteID, int iOperatorID);

        //工厂揽站点数量
        [OperationContract]
        ReturnEntity<string> Exp_Storage_FactoryTakeLine_Count(int iOperatorID);

        //工厂揽站点完成
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete(int iOperatorID);

        //干线揽工厂数量
        [OperationContract]
        ReturnEntity<string> Exp_Storage_LineTakeFactory_Count(int iOperatorID);

        //干线揽工厂衣服
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_LineTakeFactory(string iCode, int iOperatorID);

        //站点揽干线数量
        [OperationContract]
        ReturnEntity<string> Exp_Storage_SiteTakeLine_Count(int iOperatorID);

        //站点入库
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_SiteTakeLine(string iCode, int iOperatorID);

        /// <summary>
        /// 电话联系用户
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStatus"></param>
        /// <param name="iCallTime"></param>
        /// <param name="iSecond"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Order_UPDATE_CallUser(int iOrderID, int iStatus, DateTime iCallTime, int iSecond, int iMuser);

        /// <summary>
        /// 获取干线下的站点
        /// </summary>
        /// <param name="iLineID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Exp_NodeDC>> Exp_Node_LineSite(int iLineID);

        /// <summary>
        /// 站点入库异常
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_SiteTakeLine_FailNotify(int iOperatorID);

        /// <summary>
        /// 干线取站点件异常
        /// </summary>
        /// <param name="iSiteID"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_LineTakeSite_FailNotify(int iSiteID, int iOperatorID);

        /// <summary>
        /// 干线取工厂件异常
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Exp_Storage_LineTakeFactory_FailNotify(int iOperatorID);

        //站点收件日志
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_SiteTakeUser_Log(int iOperatorID,
          int? iType,
          DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);

        //站点取干线日志 按入库时间搜
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_SiteTakeLine_Log(int iOperatorID,
          int? iType,
          DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);

        //干线取站点日志
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_LineTakeSite_Log(int iOperatorID,
          int? iType,
          DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);

        //干线取工厂日志
        [OperationContract]
        ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_LineFactory_Log(int iOperatorID,
          int? iType,
          DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);
    }
}
