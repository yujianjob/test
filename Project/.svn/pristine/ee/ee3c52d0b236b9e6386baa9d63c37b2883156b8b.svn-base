using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    public class CacheProxy
    {
        public static string RemoveCache(string cachename)
        {
            string rtn = "成功";
            try
            {
                LazyAtHome.Core.Helper.CacheHelper.Remove(cachename);
            }
            catch (Exception ex)
            {

                rtn = ex.Message;
            }
            return rtn;
        }
    }
}