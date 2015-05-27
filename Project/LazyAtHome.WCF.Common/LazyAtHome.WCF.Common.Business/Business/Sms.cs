using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public class Sms
    {
        private LazyAtHome.WCF.Common.Interface.IDAL.ISmsDAL smsDAL;


        public Sms()
        {
            if (smsDAL == null)
                smsDAL = new LazyAtHome.WCF.Common.DAL.SmsDAL();
        }

        static Sms _Sms;
        public static Sms Instance
        {
            get
            {
                if (_Sms == null)
                {
                    _Sms = new Sms();
                }
                return _Sms;
            }
        }



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
            return new ReturnEntity<RecordCountEntity<Base_SmsSendDC>>(smsDAL.Base_SmsSend_SELECT_List(
                iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize));
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
            var entity = new Base_SmsSendDC()
            {
                Mobile = iMPNo,
                Content = iContent,
                RunTime = iRunTime.HasValue ? iRunTime.Value : DateTime.Now,
                Priority = (int)iPriority,
                Type = (int)iType,
                Channel = (int)iChannel,
                Source = (int)iSource,
                SourceValue = iSourceValue,
            };

            if (entity.Channel == 0)
                entity.Channel = 2;

            return new ReturnEntity<bool>(smsDAL.Base_SmsSend_Create(entity));
        }
    }
}
