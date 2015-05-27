using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Contract.DataContract
{
    [DataContract]
    [Serializable]
    public class SF_OrderInfoDC
    {
        /// <summary>
        /// 订单号 
        /// </summary>
        [DataMember]
        public string orderid { get; set; }

        /// <summary>
        /// 运单号，可多个单号，如子母件，以逗号分隔
        /// </summary>
        [DataMember]
        public string mailno { get; set; }

        /// <summary>
        /// 原寄地代码 非空
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

        /// <summary>
        /// 备注 1 订单号与运单不匹配 2 成功 
        /// </summary>
        [DataMember]
        public int res_status { get; set; }
    }
}
