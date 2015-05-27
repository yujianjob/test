using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.WebManage.Models.Log
{
    /// <summary>
    /// 操作日志列表页绑定实体
    /// </summary>
    [Serializable]
    public class OperatorLogListModel
    {
        public OperatorLogSearchInfo SearchInfo;
        public IList<OperatorLogDC> OperatorLogList;
    }


}