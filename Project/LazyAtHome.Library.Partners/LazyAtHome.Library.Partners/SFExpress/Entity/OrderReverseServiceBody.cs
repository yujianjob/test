using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Library.Partners.SFExpress.Entity
{
    /// <summary>
    ///  下退货订单
    /// </summary>
    public class OrderReverseServiceBody : EntityBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { get; set; }

        /// <summary>
        /// 快件产品类别(可根据需要定制扩展) 1 标准快递 2 顺丰特惠 3 电商特惠 
        /// </summary>
        public string express_type = "3";

        /// <summary>
        /// 寄件方公司名称
        /// </summary>
        public string j_company { get; set; }

        /// <summary>
        /// 寄件方联系人
        /// </summary>
        public string j_contact { get; set; }

        /// <summary>
        /// 寄系电话
        /// </summary>
        public string j_tel { get; set; }

        /// <summary>
        /// 寄件方地址
        /// </summary>
        public string j_address { get; set; }

        /// <summary>
        /// 寄件方省
        /// </summary>
        public string j_province { get; set; }

        /// <summary>
        /// 寄件方市
        /// </summary>
        public string j_city { get; set; }

        /// <summary>
        /// 到件方公司名称
        /// </summary>
        public string d_company { get; set; }

        /// <summary>
        /// 到件方联系人
        /// </summary>
        public string d_contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string d_tel { get; set; }

        /// <summary>
        /// 到件方地址
        /// </summary>
        public string d_address { get; set; }

        /// <summary>
        /// 到件方省
        /// </summary>
        public string d_province { get; set; }

        /// <summary>
        /// 到件方市
        /// </summary>
        public string d_city { get; set; }

        /// <summary>
        /// 要求取件上门时间
        /// </summary>
        public DateTime? sendstarttime { get; set; }

        /// <summary>
        /// 货款
        /// </summary>
        public decimal? Payment = null;

        public OrderReverseServiceBody()
            : base("OrderReverseService")
        {

        }

        public override void CreateBody(XmlElement body)
        {
            var order = XmlDoc.CreateElement("Order");

            order.SetAttribute("orderid", this.orderid);

            order.SetAttribute("express_type", this.express_type);

            //寄件信息
            order.SetAttribute("j_company", this.j_company);
            order.SetAttribute("j_contact", this.j_contact);
            order.SetAttribute("j_tel", this.j_tel);
            order.SetAttribute("j_address", this.j_address);
            order.SetAttribute("j_province", this.j_province);
            order.SetAttribute("j_city", this.j_city);

            //到件信息
            order.SetAttribute("d_company", this.d_company);
            order.SetAttribute("d_contact", this.d_contact);
            order.SetAttribute("d_tel", this.d_tel);
            order.SetAttribute("d_address", this.d_address);
            order.SetAttribute("d_province", this.d_province);
            order.SetAttribute("d_city", this.d_city);

            //第三方付
            order.SetAttribute("pay_method", "3");
            //月结卡号
            order.SetAttribute("custid", Config.Account.CardID);

            if (sendstarttime.HasValue)
            {
                order.SetAttribute("sendstarttime", sendstarttime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            //代收货款
            if (Payment.HasValue)
            {
                var addedService = XmlDoc.CreateElement("AddedService");

                addedService.SetAttribute("name", "COD");

                addedService.SetAttribute("value", Payment.Value.ToString("0.00"));

                addedService.SetAttribute("value1", Config.Account.CardID);

                order.AppendChild(addedService);
            }

            var cargo = XmlDoc.CreateElement("Cargo");

            cargo.SetAttribute("name", "衣物");
            cargo.SetAttribute("serl", "00001");
            cargo.SetAttribute("unit", "件");

            //cargo.SetAttribute("name", "手机一台");
            //cargo.SetAttribute("serl", "00001");
            //cargo.SetAttribute("unit", "台");

            order.AppendChild(cargo);


            body.AppendChild(order);


        }
    }
}
