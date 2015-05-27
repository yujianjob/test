using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Business.Business
{
    public partial class Order
    {
        public ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> Partner_Order_Express_SELECT_List(
            string iUserName, string iMPNo, int? iExpressStatus,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Partner_Order_Express_SELECT_List(iUserName, iMPNo, iExpressStatus,
                        iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>>(rtn);
        }

        public ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>> Partner_Order_Express_SELECT_List(
            string iUserName, string iMPNo, string iGetExpressNumber,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = orderDAL.Partner_Order_Express_SELECT_List(iUserName, iMPNo, iGetExpressNumber,
                         iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Partner_Order_ExpressDC>>(rtn);
        }

        public ReturnEntity<bool> Partner_Order_Express_UPDATE_ExpressStatus(int iOrderID, int iExpressStatus)
        {
            var rtn = orderDAL.Partner_Order_Express_UPDATE_ExpressStatus(iOrderID, iExpressStatus);

            return new ReturnEntity<bool>(rtn);
        }

        const string Partner_Ruohai_PushUrl = "http://www.campusmart.cn/weixin/freewash?ordernum={0}&state={1}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderNumber">订单号</param>
        /// <param name="iStatus">1.受理订单；2.取走衣服；3.干洗中；4.送还店面</param>
        /// <returns></returns>
        public void Partner_Ruohai_Order_ChangeStatus(string iOrderNumber, int iStatus)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                string pushUrl = string.Format(Partner_Ruohai_PushUrl, iOrderNumber, iStatus);

                try
                {
                    Log_Info("若海订单状态推送:" + iOrderNumber + "_" + iStatus);
                    HttpWebResponseUtility.CreateGetHttpResponse(pushUrl, null, null, null);
                }
                catch (Exception ex)
                {
                    Log_Fatal("若海订单状态推送失败:" + ex.Message + " " + pushUrl);
                }
            });
        }


         
    }
}
