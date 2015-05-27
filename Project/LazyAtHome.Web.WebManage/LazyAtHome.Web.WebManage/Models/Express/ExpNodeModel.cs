using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 存衣点页面展示实体类
    /// </summary>
    [Serializable]
    public class ExpNodeModel
    {
        /// <summary>
        /// 存衣点信息
        /// </summary>
        public Exp_NodeDC ExpNodeInfo { get; set; }


        /// <summary>
        /// 站点人员列表
        /// </summary>
        public IList<OperatorDC> ExpOperatorList { get; set; }
    }
}