using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.QFExpress.Entity
{
    public class RequestOrderBody : EntityBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string txLogisticID { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string mailNo { get; set; }

        /// <summary>
        /// 订单的flag标识，便于以后分拣和标识（暂时不用）
        /// </summary>
        private int flag = 1;

        /// <summary>
        /// 发货方
        /// </summary>
        public Address sender { get; set; }

        /// <summary>
        /// 收货方
        /// </summary>
        public Address receiver { get; set; }

        /// <summary>
        /// 物流上门取货时间段
        /// </summary>
        public DateTime? sendStartTime { get; set; }

        /// <summary>
        /// 物流上门取货时间段
        /// </summary>
        public DateTime? sendEndTime { get; set; }

        /// <summary>
        /// 货物信息
        /// </summary>
        public IList<Item> items { get; set; }

        /// <summary>
        /// 保值金额（暂时没有使用，默认为0.0）
        /// </summary>
        private double insuranceValue = 0;

        /// <summary>
        /// 是否打包（暂时没有使用，默认为false）
        /// </summary>
        private bool packageOrNot = true;

        /// <summary>
        /// 电商物流平台只区分文件和包裹，文件使用（0）标识，包裹使用（9）标识
        /// </summary>
        private int special = 9;

        private string remark = null;

        public class Address
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 邮编
            /// </summary>
            public string postCode { get; set; }

            /// <summary>
            /// 电话
            /// </summary>
            public string phone { get; set; }

            /// <summary>
            /// 用户移动电话[手机和电话两者必需提供一个]
            /// </summary>
            public string mobile { get; set; }

            /// <summary>
            /// 所在省
            /// </summary>
            public string prov { get; set; }

            /// <summary>
            /// 所在市县（区）
            /// </summary>
            public string city { get; set; }

            /// <summary>
            /// 地址
            /// </summary>
            public string address { get; set; }

        }

        public class Item
        {
            public string itemName { get; set; }
            public int Number { get; set; }
            public string remark { get; set; }
        }

        public override void CreateBody()
        {
            var order = XmlDoc.CreateElement("RequestOrder");

            order.SetAttribute("version", "2.0");

            //我们公司ID
            var tmpNode = XmlDoc.CreateElement("logisticProviderID");
            tmpNode.InnerText = this.logisticProviderID;
            order.AppendChild(tmpNode);

            //订单号
            tmpNode = XmlDoc.CreateElement("txLogisticID");
            tmpNode.InnerText = this.txLogisticID;
            order.AppendChild(tmpNode);

            //物流单号
            tmpNode = XmlDoc.CreateElement("mailNo");
            tmpNode.InnerText = this.mailNo;
            order.AppendChild(tmpNode);

            //
            tmpNode = XmlDoc.CreateElement("flag");
            tmpNode.InnerText = this.flag.ToString();
            order.AppendChild(tmpNode);

            var sender = XmlDoc.CreateElement("sender");

            tmpNode = XmlDoc.CreateElement("name");
            tmpNode.InnerText = this.sender.name;
            sender.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("postCode");
            tmpNode.InnerText = this.sender.postCode;
            sender.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("phone");
            tmpNode.InnerText = this.sender.phone;
            sender.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("mobile");
            tmpNode.InnerText = this.sender.mobile;
            sender.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("prov");
            tmpNode.InnerText = this.sender.prov;
            sender.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("city");
            tmpNode.InnerText = this.sender.city;
            sender.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("address");
            tmpNode.InnerText = this.sender.address;
            sender.AppendChild(tmpNode);

            order.AppendChild(sender);

            var receiver = XmlDoc.CreateElement("receiver");

            tmpNode = XmlDoc.CreateElement("name");
            tmpNode.InnerText = this.receiver.name;
            receiver.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("postCode");
            tmpNode.InnerText = this.receiver.postCode;
            receiver.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("phone");
            tmpNode.InnerText = this.receiver.phone;
            receiver.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("mobile");
            tmpNode.InnerText = this.receiver.mobile;
            receiver.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("prov");
            tmpNode.InnerText = this.receiver.prov;
            receiver.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("city");
            tmpNode.InnerText = this.receiver.city;
            receiver.AppendChild(tmpNode);
            tmpNode = XmlDoc.CreateElement("address");
            tmpNode.InnerText = this.receiver.address;
            receiver.AppendChild(tmpNode);

            order.AppendChild(receiver);

            if (this.sendStartTime.HasValue && this.sendEndTime.HasValue)
            {
                tmpNode = XmlDoc.CreateElement("sendStartTime");
                tmpNode.InnerText = this.sendStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss.f CST");
                order.AppendChild(tmpNode);
                tmpNode = XmlDoc.CreateElement("sendEndTime");
                tmpNode.InnerText = this.sendEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss.f CST");
                order.AppendChild(tmpNode);
            }

            var tmpItems = XmlDoc.CreateElement("items");

            foreach (var item in items)
            {
                var tmpItem = XmlDoc.CreateElement("item");

                tmpNode = XmlDoc.CreateElement("itemName");
                tmpNode.InnerText = item.itemName;
                tmpItem.AppendChild(tmpNode);

                tmpNode = XmlDoc.CreateElement("Number");
                tmpNode.InnerText = item.Number.ToString();
                tmpItem.AppendChild(tmpNode);


                tmpNode = XmlDoc.CreateElement("remark");
                tmpNode.InnerText = item.remark;
                tmpItem.AppendChild(tmpNode);

                tmpItems.AppendChild(tmpItem);

            }

            order.AppendChild(tmpItems);

            tmpNode = XmlDoc.CreateElement("insuranceValue");
            tmpNode.InnerText = this.insuranceValue.ToString("0.0");
            order.AppendChild(tmpNode);

            tmpNode = XmlDoc.CreateElement("packageOrNot");
            tmpNode.InnerText = this.packageOrNot.ToString().ToLower();
            order.AppendChild(tmpNode);

            tmpNode = XmlDoc.CreateElement("special");
            tmpNode.InnerText = this.special.ToString();
            order.AppendChild(tmpNode);


            tmpNode = XmlDoc.CreateElement("remark");
            tmpNode.InnerText = this.remark;
            order.AppendChild(tmpNode);

            XmlDoc.AppendChild(order);
        }
    }
}
