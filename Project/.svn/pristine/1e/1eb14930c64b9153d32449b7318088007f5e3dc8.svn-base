using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WashSite.App_Code.Proxy
{
    public class CommonProxy
    {
        public static ReturnEntity<Survey_MainDC> Survey_Answer_SELECT_Entity(int id)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<Survey_MainDC>>(c => c.Proxy.Survey_Main_SELECT_Entity(id));
            return rtn;
        }

        public static ReturnEntity<int> Survey_Answer_ADD(Survey_AnswerDC answer)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<int>>(c => c.Proxy.Survey_Answer_ADD(answer));
            return rtn;
        }
    }
}