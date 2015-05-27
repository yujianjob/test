using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.Common.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Portal
{
    public partial class CommonPortal : ISms
    {
        protected LazyAtHome.WCF.Common.Business.Business.Sms SmsInstance = LazyAtHome.WCF.Common.Business.Business.Sms.Instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_SmsSendDC>> Base_SmsSend_SELECT_List(
           string iMPNo, DateTime iStartDate, DateTime iEndDate,
           int iPageIndex, int iPageSize)
        {
            Log_DeBug("Base_SmsSend_SELECT_List", iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return SmsInstance.Base_SmsSend_SELECT_List(iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_SmsSend_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Base_SmsSendDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_SmsSend_SELECT_List");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iContent"></param>
        /// <param name="iRunTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_SmsSend_Create(string iMPNo, string iContent,
            DateTime? iRunTime = null,
            Sms_Priority iPriority = Sms_Priority.Normal,
            Sms_Type iType = Sms_Type.Normal,
            Sms_Channel iChannel = Sms_Channel.YM,
            Sms_Source iSource = Sms_Source.Null,
            string iSourceValue = null
            )
        {
            Log_DeBug("Base_SmsSend_Create", iMPNo, iContent,
                    iRunTime, iPriority, iType,
                    iSource, iChannel, iSourceValue);
            try
            {
                return SmsInstance.Base_SmsSend_Create(iMPNo, iContent,
                    iRunTime, iPriority, iType,
                    iChannel, iSource, iSourceValue);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_SmsSend_Create", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_SmsSend_Create");
            }

        }
    }
}
