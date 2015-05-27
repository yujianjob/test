using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Business
{
    public class GetAlarm
    {
        private LazyAtHome.WCF.Express.Interface.IDAL.IExpressDAL expressDAL;

        public GetAlarm()
        {
            if (expressDAL == null)
                expressDAL = new LazyAtHome.WCF.Express.DAL.ExpressDAL();
        }

        static GetAlarm _GetAlarm;

        public static GetAlarm Instance
        {
            get
            {
                if (_GetAlarm == null)
                {
                    _GetAlarm = new GetAlarm();
                }
                return _GetAlarm;
            }
        }

        public void Process()
        {
            #region 15分钟使用推荐码未取件

            var alarmUnTakeCompleteList_Invite = expressDAL.Exp_Order_Alarm_UnTakeComplete_Invite();
            if (alarmUnTakeCompleteList_Invite != null)
            {
                foreach (var item in alarmUnTakeCompleteList_Invite)
                {
                    Console.WriteLine("15分钟使用推荐码未取件 站点:" + item.NodeName + "  " + item.OperatorName);

                    //清空数据,重新分配
                    expressDAL.Exp_Order_Clear(item.ID, item.OutNumber);
                }
            }

            #endregion

            #region 5分钟无人接单

            //当前时间-预计时间大于5分钟 无人接单 当前时间-预计时间小于8分钟
            var alarmUnAcceptList = expressDAL.Exp_Order_Alarm_UnAccept();
            if (alarmUnAcceptList != null)
            {
                foreach (var item in alarmUnAcceptList)
                {
                    //0物流单ID,1订单编号,2站点名,3负责人ID,4负责人职位,5负责人名,6取件人ID,7取件人名
                    var tmp = item.Split('_');

                    //负责人职位 空 分配客服
                    CommonExpress.NotifySend(tmp[1], RoleDC.Role_CustomerService, 0, 0, "5分钟无人接单",
                       "站点:" + tmp[2] + " 负责人: " + tmp[5],
                       (int)NotifyLevel.Warning4, false, false, 0);

                    //if (string.IsNullOrEmpty(tmp[4]) || tmp[4] == "0")
                    //{
                    //    //负责人职位 空 分配客服
                    //    CommonExpress.NotifySend(tmp[1], RoleDC.Role_CustomerService, 0, 0, "5分钟无人接单",
                    //       "站点:" + tmp[2] + " 负责人: " + tmp[5],
                    //       (int)NotifyLevel.Warning4, false, false, 0);
                    //}
                    //else
                    //{
                    //    //推送站点负责人
                    //    CommonExpress.NotifySend(tmp[1], int.Parse(tmp[4]), int.Parse(tmp[3]), 0, "5分钟无人接单",
                    //        "站点:" + tmp[2] + " 负责人:" + tmp[5],
                    //       (int)NotifyLevel.Notification3, true, false, 0);
                    //}

                    Console.WriteLine("5分钟无人接单 站点:" + tmp[2] + "  " + tmp[1]);

                    //将订单设置为失败
                    expressDAL.Exp_Order_Alarm_UnAccept_Close(int.Parse(tmp[0]));
                }
            }

            #endregion

            #region 60分钟接单未取件完成

            //获取60分钟接单未取件完成  直接分配客服 当前时间-预计时间>60分钟
            var alarmUnTakeCompleteList = expressDAL.Exp_Order_Alarm_UnTakeComplete();
            if (alarmUnAcceptList != null)
            {
                foreach (var item in alarmUnTakeCompleteList)
                {
                    //物流单ID,订单编号,站点名,3负责人ID,4负责人职位,5负责人名,6取件人ID,7取件人名
                    var tmp = item.Split('_');

                    //推送客服
                    CommonExpress.NotifySend(tmp[1], RoleDC.Role_CustomerService, 0, 0, "60分钟接单未取件",
                       "站点:" + tmp[2] + " 负责人:" + tmp[5] + " 取件人:" + tmp[7],
                       (int)NotifyLevel.Warning4, false, false, 0);

                    Console.WriteLine("60分钟接单未取件 站点:" + tmp[2] + "  " + tmp[1]);

                    //将订单设置为失败
                    expressDAL.Exp_Order_Alarm_UnTakeComplete_Close(int.Parse(tmp[0]));
                }
            }

            #endregion

        }

    }
}
