using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Models.WebAttribute
{
    /// <summary>
    /// 页面属性列表页绑定实体
    /// </summary>
    [Serializable]
    public class WebAttributeListModel
    {
        public WebAttributeSearchInfo SearchInfo;
        public IList<Base_WebAttributeDC> WebAttributeList;
    }
}