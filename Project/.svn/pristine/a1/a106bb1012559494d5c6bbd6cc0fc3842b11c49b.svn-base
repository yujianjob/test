using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Library.Partners.SFExpress.Entity
{
    /// <summary>
    /// 实体类底层
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// 生成对象,根据字段生成xml
        /// </summary>
        /// <param name="iServiceName"></param>
        /// <param name="iOperatingType">Request:发送请求中包含用户名密码 Response:响应对方请求,回答中只有OK</param>
        public EntityBase(string iServiceName)
        {
            this._serviceName = iServiceName;
            //this._operatingType = iOperatingType;
        }

        /// <summary>
        /// 传入xml,解析后生成对象
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="iServiceName"></param>
        /// <param name="iOperatingType"></param>
        public EntityBase(string iServiceName, string xml)
            : this(iServiceName)
        {
            this._xmlString = xml;
        }

        //public EntityBase(string iServiceName, OperatingType iOperatingType)
        //    : this(iServiceName,iOperatingType)
        //{
        //    this._serviceName = iServiceName;
        //}

        private const string _lang = "zh-CN";

        /// <summary>
        /// 客户卡号
        /// </summary>
        private static string j_custid = Config.Account.CustomerID;

        /// <summary>
        /// 校验码
        /// </summary>
        private static string checkwork = Config.Account.Check;

        /// <summary>
        /// 调用服务名称
        /// </summary>
        private string _serviceName;

        ///// <summary>
        ///// 协议操作类型 
        ///// Request:发送请求中包含用户名密码
        ///// Response:响应对方请求,回答中只有OK
        ///// </summary>
        //protected OperatingType _operatingType = OperatingType.Request;

        ///// <summary>
        ///// 序列化后的文本
        ///// </summary>
        //protected StringBuilder SerializerString { get; set; }

        /// <summary>
        /// 返回的xml文本
        /// </summary>
        private string _xmlString;

        /// <summary>
        /// xml对象
        /// </summary>
        protected XmlDocument XmlDoc;

        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrCode { get; protected set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrMessage { get; protected set; }

        /// <summary>
        /// 序列化为xml
        /// </summary>
        /// <returns></returns>
        public string Serializer()
        {
            return Serializer(true);
        }

        /// <summary>
        /// 序列化为xml(是否包含识别号)
        /// </summary>
        /// <returns></returns>
        public string Serializer(bool iRequest)
        {
            XmlDoc = new XmlDocument();

            if (iRequest)
            {
                XmlDoc.LoadXml("<Request service=\"" + this._serviceName + "\" lang=\"zh-CN\"></Request>");
            }
            else
            {
                XmlDoc.LoadXml("<Response service=\"" + this._serviceName + "\" ></Response>");
            }

            CreateHead(iRequest);

            var body = XmlDoc.CreateElement("Body");

            if (this.ErrCode != 0)
            {
                var error = XmlDoc.CreateElement("ERROR");

                error.SetAttribute("code", this.ErrCode.ToString());

                error.InnerText = this.ErrMessage;

                body.AppendChild(error);
            }
            else
            {
                CreateBody(body);
            }

            XmlDoc.DocumentElement.AppendChild(body);

            return XmlDoc.InnerXml;
        }

        /// <summary>
        /// 反序列化,生成字段
        /// </summary>
        public bool Deserialize()
        {
            return Deserialize(true);
        }

        /// <summary>
        /// 反序列化,生成字段(报文是否包含头部)
        /// </summary>
        public bool Deserialize(bool iResponse)
        {
            XmlDoc = new XmlDocument();

            try
            {
                XmlDoc.LoadXml(this._xmlString);
            }
            catch (Exception ex)
            {
                this.ErrCode = -999;
                this.ErrMessage = ex.Message;
                return false;
            }

            //由我方发起的请求,回应的数据需要解析头部
            if (iResponse)
            {
                if (!AnalyzeHead())
                    return false;
            }

            try
            {
                return AnalyzeBody();
            }
            catch (Exception ex)
            {
                this.ErrCode = -999;
                this.ErrMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 解析xml头部
        /// </summary>
        /// <returns></returns>
        private bool AnalyzeHead()
        {
            var Response = XmlDoc.SelectSingleNode("/Response/Head").InnerText;

            if (Response != "OK")
            {
                try
                {
                    this.ErrCode = int.Parse(XmlDoc.SelectSingleNode("/Response/ERROR").Attributes["code"].Value);
                }
                catch
                {
                    this.ErrCode = -1;
                }
                this.ErrMessage = XmlDoc.SelectSingleNode("/Response/ERROR").InnerText;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 解析xml内容数据
        /// </summary>
        /// <returns></returns>
        public virtual bool AnalyzeBody() { return false; }

        /// <summary>
        /// 创建头部(是否包含识别号)
        /// </summary>
        /// <param name="iRequest"></param>
        private void CreateHead(bool iRequest)
        {
            var head = this.XmlDoc.CreateElement("Head");

            //我方发起的请求,头部需要识别号
            if (iRequest)
            {
                head.InnerText = j_custid + "," + checkwork;
            }
            else
            {
                if (this.ErrCode == 0)
                {
                    head.InnerText = "OK";
                }
                else
                {
                    head.InnerText = "ERR";
                }
            }
            XmlDoc.DocumentElement.AppendChild(head);
        }

        public virtual void CreateBody(XmlElement body) { }
    }
}
