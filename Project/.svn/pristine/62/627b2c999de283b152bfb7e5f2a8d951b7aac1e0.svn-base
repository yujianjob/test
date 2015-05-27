using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Interface.IDAL
{
    public interface ISmsDAL
    {
        /// <summary>
        /// 获取短信列表
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<Base_SmsSendDC> Base_SmsSend_SELECT_List(
            string iMPNo, DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="iBase_SmsSendDC"></param>
        /// <returns></returns>
        bool Base_SmsSend_Create(Base_SmsSendDC iBase_SmsSendDC);

        /// <summary>
        /// 查询Base_Push
        /// </summary>
        /// <returns></returns>
        IList<Base_PushDC> Base_Push_SELECT_List();

        /// <summary>
        /// 更新发送状态
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iSendStatus"></param>
        /// <returns></returns>
        bool Base_Push_UPDATE(int iID, int iSendStatus);
    }
}
