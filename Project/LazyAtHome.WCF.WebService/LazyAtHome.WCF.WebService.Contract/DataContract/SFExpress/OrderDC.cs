using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LazyAtHome.WCF.WebService.Contract.DataContract.SFExpress
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class OrderDC : EntityBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [DataMember]
        public string orderid { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [DataMember]
        public string mailno { get; set; }

        /// <summary>
        /// 原寄地代码
        /// </summary>
        [DataMember]
        public string origincode { get; set; }

        /// <summary>
        /// 目的地代码
        /// </summary>
        [DataMember]
        public string destcode { get; set; }

        /// <summary>
        /// 筛单结果：1-人工确认，2-可收派 3-不可 以收派 
        /// </summary>
        [DataMember]
        public int filter_result { get; set; }

        /// <summary>
        /// 1-收方超范围，2-派方超范围，3-其他原因
        /// </summary>
        [DataMember]
        public string remark { get; set; }
    }
}
