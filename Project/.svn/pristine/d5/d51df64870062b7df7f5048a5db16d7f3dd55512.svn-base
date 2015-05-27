using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Interface.IDAL
{
    public interface IAppExpressDAL
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="iLoginName"></param>
        ///// <param name="iLoginPassword"></param>
        ///// <returns></returns>
        //Exp_OperatorDC Exp_Operator_SELECT_Entity(string iLoginName, string iLoginPassword);

        ///// <summary>
        ///// 上岗下岗
        ///// </summary>
        ///// <param name="iOperatorID"></param>
        ///// <param name="iOnDuty"></param>
        ///// <returns></returns>
        //bool Exp_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty);

        /// <summary>
        /// 待接列表
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        IList<Exp_OrderDC> Exp_Order_SELECT_WaitAccept(int iOperatorID);


        //接单
        bool Exp_Order_Accept(int iOrderID, string iOutNumber, int iOperatorID, int iNodeID);

        //拒接单
        int Exp_Order_UnAccept(int iOrderID, int iOperatorID);

        //待收件列表
        IList<Exp_OrderDC> Exp_Order_SELECT_WaitTake(int iOperatorID);

        //收件完成
        bool Exp_Order_TakeComplete(int iOrderID, string iExpNumber, int iOperatorID);

        //收件失败
        bool Exp_Order_TakeFail(int iOrderID, int iOperatorID, string iFailReason);

        //更改取件时间
        //bool Exp_Order_Get_ExpectTime_Change(int iOrderID, DateTime iExpectTime);

        //扫描揽件
        bool Exp_Order_TakeSend(string iOutNumber, int iOperatorID);

        //待送件列表
        IList<Exp_OrderDC> Exp_Order_SELECT_WaitSend(int iOperatorID);

        //送件完成
        bool Exp_Order_SendComplete(int iOrderID, int iOperatorID);

        /// <summary>
        /// 电话联系用户
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStatus"></param>
        /// <param name="iCallTime"></param>
        /// <param name="iSecond"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        bool Exp_Order_UPDATE_CallUser(int iOrderID, int iStatus, DateTime iCallTime, int iSecond, int iMuser);

        /// <summary>
        /// 获取干线下的站点
        /// </summary>
        /// <param name="iLineID"></param>
        /// <returns></returns>
        IList<Exp_NodeDC> Exp_Node_LineSite(int iLineID);
    }
}
