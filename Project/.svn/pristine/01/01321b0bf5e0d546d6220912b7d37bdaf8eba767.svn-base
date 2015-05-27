using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.TinyURL.App_Code.Proxy
{
    public class CommonProxy
    {
        public static ReturnEntity<string> Tinyurl_CreateNew(string iUrl)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<string>>(
                c => c.Proxy.Tinyurl_CreateNew(iUrl));

            return rtn;
        }

        public static ReturnEntity<string> Tinyurl_Get(string iCode)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<string>>(
                c => c.Proxy.Tinyurl_Get(iCode));

            return rtn;
        }
    }
}