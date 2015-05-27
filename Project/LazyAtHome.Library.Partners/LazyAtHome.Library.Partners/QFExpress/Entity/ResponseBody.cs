using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Library.Partners.QFExpress.Entity
{
    public class ResponseBody : EntityBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string txLogisticID { get; set; }

        public bool Success { get; set; }

        public ResponseBody(string xml)
            : base(xml)
        {

        }

        public ResponseBody(string iOrderNumber, bool iSuccess, string iErrorMessage)
        {
            this.txLogisticID = iOrderNumber;
            this.Success = iSuccess;
            this.ErrCode = -1;
            this.ErrMessage = iErrorMessage;
        }

        public override bool AnalyzeBody()
        {
            var xmlResponseNode = XmlDoc.SelectSingleNode("/Response/success");

            if (xmlResponseNode != null)
            {
                if (xmlResponseNode.InnerText == "true")
                {
                    this.Success = true;
                    return true;
                }
                var errNode = XmlDoc.SelectSingleNode("/Response/reason");
                if (errNode != null)
                {
                    this.ErrCode = -1;
                    this.ErrMessage = errNode.InnerText;
                }
                else
                {
                    this.ErrCode = -2;
                    this.ErrMessage = "服务返回错误,错误内容为空";
                }
            }
            else
            {
                this.ErrCode = -2;
                this.ErrMessage = "返回信息解析失败";
            }

            this.Success = false;
            return true;
        }

        public new string Serializer()
        {
            XmlDoc = new XmlDocument();

            var response = XmlDoc.CreateElement("Response");

            var tmpNode = XmlDoc.CreateElement("txLogisticID");
            tmpNode.InnerText = this.txLogisticID;
            response.AppendChild(tmpNode);

            tmpNode = XmlDoc.CreateElement("logisticProviderID");
            tmpNode.InnerText = this.logisticProviderID;
            response.AppendChild(tmpNode);

            tmpNode = XmlDoc.CreateElement("success");
            tmpNode.InnerText = this.Success.ToString().ToLower();
            response.AppendChild(tmpNode);

            if (!this.Success)
            {
                tmpNode = XmlDoc.CreateElement("reason");
                this.ErrCode = -1;
                tmpNode.InnerText = this.ErrMessage;
                response.AppendChild(tmpNode);
            }

            XmlDoc.AppendChild(response);

            return XmlDoc.InnerXml;
        }
    }
}
