using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;

namespace LazyAtHome.Web.Wash2.App_Code.Proxy
{
    public class WashProxy
    {
        public static ReturnEntity<RecordCountEntity<web_Mall_ProductDC>> Select_MallList(int? iType, int? iClass, string iKeyword, string iTypeValue, int? iSite, int? iOrderType, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<RecordCountEntity<web_Mall_ProductDC>>>(c => c.Proxy.web_Mall_Product_SELECT_List(iType, iClass, iKeyword, iTypeValue, iSite, iOrderType, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<web_Wash_ClassDC>> Select_Category_Search(string iKeyword, int iSite, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<RecordCountEntity<web_Wash_ClassDC>>>(c => c.Proxy.web_Wash_Category_SELECT_Search(iKeyword, iSite, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<web_Mall_ProductDC> Select_MallEntity(int id)
        {
            var rtn = WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<web_Mall_ProductDC>>(c => c.Proxy.web_Mall_Product_SELECT_Entity(id));
            return rtn;
        }

        public static ReturnEntity<IDictionary<int, web_Wash_ProductDC>> Select_ProductList(IList<int> iProductIDList)
        {
            var rtn = WCFInvokeHelper<web_ProductClient>.Invoke<ReturnEntity<IDictionary<int, web_Wash_ProductDC>>>(c => c.Proxy.web_Wash_Product_SELECT_Entity(iProductIDList));
            return rtn;
        }
    }
}