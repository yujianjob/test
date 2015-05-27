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
    public class OrderConfirmServiceResponse : EntityBase
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
        /// 备注 1 订单号与运单不匹配 2 成功 
        /// </summary>
        [DataMember]
        public int res_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool result
        {
            get
            {
                if (res_status == 2)
                    return true;
                else
                    return false;
            }
            set { }
        }

        public OrderConfirmServiceResponse(string xml)
            : base("OrderConfirmService", xml)
        {

        }

        public override bool AnalyzeBody()
        {
            var xmlResponse = XmlDoc.SelectSingleNode("/Response/Body/OrderConfirmResponse");

            this.orderid = xmlResponse.Attributes["orderid"].Value;

            if (xmlResponse.Attributes["mailno"] != null)
            {
                this.mailno = xmlResponse.Attributes["mailno"].Value;
            }

            if (xmlResponse.Attributes["res_status"] != null)
            {
                this.res_status = int.Parse(xmlResponse.Attributes["res_status"].Value);
            }

            return true;
        }
    }
}
