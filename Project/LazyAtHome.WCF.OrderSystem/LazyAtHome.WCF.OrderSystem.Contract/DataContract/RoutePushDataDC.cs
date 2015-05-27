using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class RoutePushDataDC
    {
        /// <summary>
        /// 路由编号
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [DataMember]
        public string ExpressNumber { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [DataMember]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 路由发生时间
        /// </summary>
        [DataMember]
        public DateTime AcceptTime { get; set; }

        /// <summary>
        /// 路由内容
        /// </summary>
        [DataMember]
        public string RouteInfo { get; set; }

        /// <summary>
        /// 路由附带信息
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// 操作码
        /// </summary>
        [DataMember]
        public string OpCode { get; set; }
    }
}
