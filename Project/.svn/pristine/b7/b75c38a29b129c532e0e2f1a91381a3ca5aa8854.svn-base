using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.QFExpress.Entity
{
    public class RequestCancelBody : EntityBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string txLogisticID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void CreateBody()
        {
            var order = XmlDoc.CreateElement("RequestCancel");

            var tmpNode = XmlDoc.CreateElement("txLogisticID");
            tmpNode.InnerText = this.txLogisticID;
            order.AppendChild(tmpNode);

            tmpNode = XmlDoc.CreateElement("logisticProviderID");
            tmpNode.InnerText = this.logisticProviderID;
            order.AppendChild(tmpNode);

            tmpNode = XmlDoc.CreateElement("remark");
            tmpNode.InnerText = this.remark;
            order.AppendChild(tmpNode);

            XmlDoc.AppendChild(order);
        }
    }
}
