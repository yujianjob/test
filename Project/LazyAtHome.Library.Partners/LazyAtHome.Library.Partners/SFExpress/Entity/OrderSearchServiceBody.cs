using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Library.Partners.SFExpress.Entity
{
    /// <summary>
    /// 订单结果查询
    /// </summary>
    public class OrderSearchServiceBody : EntityBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { get; set; }

        public OrderSearchServiceBody()
            : base("OrderSearchService")
        {

        }

        public override void CreateBody(XmlElement body)
        {
            var order = XmlDoc.CreateElement("OrderSearch");

            order.SetAttribute("orderid", this.orderid);

            body.AppendChild(order);

            //SerializerString.Append("<Order orderid=\"" + this.orderid + "\"/>");
        }
    }
}
