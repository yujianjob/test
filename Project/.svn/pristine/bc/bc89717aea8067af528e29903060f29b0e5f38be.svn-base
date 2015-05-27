using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ISms
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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Base_SmsSendDC>> Base_SmsSend_SELECT_List(
            string iMPNo, DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iContent"></param>
        /// <param name="iRunTime"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Base_SmsSend_Create(string iMPNo, string iContent,
            DateTime? iRunTime = null,
            Sms_Priority iPriority = Sms_Priority.Normal,
            Sms_Type iType = Sms_Type.Normal,
            Sms_Channel iChannel = Sms_Channel.YM,
            Sms_Source iSource = Sms_Source.Null,
            string iSourceValue = null
            );
    }
}
