using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Library.Partners.SFExpress.Entity
{
    /// <summary>
    ///  订单发货确认/消单
    /// </summary>
    public class OrderConfirmServiceBody : EntityBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { get; set; }

        /// <summary>
        /// 运单号(如果 dealtype=2，可选)
        /// </summary>
        public string mailno { get; set; }

        /// <summary>
        /// 订单操作标识:1-订单确认 2-消单 
        /// </summary>
        public int dealtype { get; set; }

        public OrderConfirmServiceBody()
            : base("OrderConfirmService")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        public override void CreateBody(XmlElement body)
        {
            var order = XmlDoc.CreateElement("OrderConfirm");

            order.SetAttribute("orderid", this.orderid);

            if (!string.IsNullOrWhiteSpace(mailno))
            {
                order.SetAttribute("mailno", this.mailno);
            }

            order.SetAttribute("dealtype", this.dealtype.ToString());

            body.AppendChild(order);

        }
    }
}
