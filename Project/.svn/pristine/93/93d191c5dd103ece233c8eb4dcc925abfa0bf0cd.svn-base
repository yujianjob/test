using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.App_Code.Proxy
{
    public class WashProxy
    {


        //4.14	获取服务费列表与广告	11


        //4.15	获取洗涤服务列表	11    
        public static ReturnEntity<IList<web_Wash_ClassDC>> web_Wash_Class_SELECT_List_Second_Detail(int iSite)
        {
            var rtn = WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IList<web_Wash_ClassDC>>>(
                c => c.Proxy.web_Wash_Class_SELECT_List_Second_Detail(iSite));

            return rtn;
        }

        public static ReturnEntity<IDictionary<int, LazyAtHome.WCF.Wash.Contract.DataContract.web.web_Wash_ProductDC>> web_Wash_Product_SELECT_Entity(IList<int> iProductIDList)
        {
            var rtn = WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IDictionary<int, LazyAtHome.WCF.Wash.Contract.DataContract.web.web_Wash_ProductDC>>>(
                c => c.Proxy.web_Wash_Product_SELECT_Entity(iProductIDList));

            return rtn;
        }

    }
}