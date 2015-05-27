using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.SFExpress.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class OrderReverseServiceResponse : EntityBase
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

        ///// <summary>
        ///// 原寄地代码 非空
        ///// </summary>
        //[DataMember]
        //public string origincode { get; set; }

        ///// <summary>
        ///// 目的地代码 
        ///// </summary>
        //[DataMember]
        //public string destcode { get; set; }

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

        public OrderReverseServiceResponse(string xml)
            : base("OrderReverseService", xml)
        {

        }

        public override bool AnalyzeBody()
        {
            var xmlResponse = XmlDoc.SelectSingleNode("/Response/Body/OrderReverseResponse");

            this.orderid = xmlResponse.Attributes["orderid"].Value;

            if (xmlResponse.Attributes["mailno"] != null)
            {
                this.mailno = xmlResponse.Attributes["mailno"].Value;
            }
            //this.origincode = xmlResponse.Attributes["origincode"].Value;
            //this.destcode = xmlResponse.Attributes["destcode"].Value;

            if (xmlResponse.Attributes["filter_result"] != null)
            {
                this.filter_result = int.Parse(xmlResponse.Attributes["filter_result"].Value);
            }
            if (xmlResponse.Attributes["remark"] != null)
            {
                this.remark = xmlResponse.Attributes["remark"].Value;
            }
            return true;
        }

    }
}
