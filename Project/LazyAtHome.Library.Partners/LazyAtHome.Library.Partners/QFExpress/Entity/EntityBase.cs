using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Library.Partners.QFExpress.Entity
{
    /// <summary>
    /// 实体类底层
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public string logisticProviderID = "QF";

        /// <summary>
        /// 生成对象,根据字段生成xml
        /// </summary>
        /// <param name="iServiceName"></param>
        /// <param name="iOperatingType">Request:发送请求 Response:响应对方请求</param>
        public EntityBase()
        {
            //this._serviceName = iServiceName;
            //this._operatingType = iOperatingType;
        }

        /// <summary>
        /// 传入xml,解析后生成对象
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="iServiceName"></param>
        /// <param name="iOperatingType"></param>
        public EntityBase(string xml)
            : this()
        {
            this._xmlString = xml;
        }

        /// <summary>
        /// 返回的xml文本
        /// </summary>
        protected string _xmlString;

        /// <summary>
        /// xml对象
        /// </summary>
        protected XmlDocument XmlDoc;

        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrMessage { get; set; }

        /// <summary>
        /// 序列化为xml
        /// </summary>
        /// <returns></returns>
        public string Serializer()
        {
            XmlDoc = new XmlDocument();

            CreateBody();

            var bizData = XmlDoc.InnerXml;

            var digest = XmlDoc.InnerXml + logisticProviderID;

            digest = Convert.ToBase64String(Common.ToHexByte(LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(digest)));

            digest = digest.Replace('+', '@');

            digest = System.Web.HttpUtility.UrlEncode(digest);

            bizData = System.Web.HttpUtility.UrlEncode(bizData);

            return "bizData=" + bizData + "&digest=" + digest;
        }

        /// <summary>
        /// 反序列化,生成字段
        /// </summary>
        public bool Deserialize()
        {
            XmlDoc = new XmlDocument();

            if (this.ErrCode != 0)
            {
                return false;
            }

            try
            {
                XmlDoc.LoadXml(this._xmlString);
            }
            catch (Exception ex)
            {
                this.ErrCode = -1;
                this.ErrMessage = "无法识别的xml文档";
                return false;
            }

            try
            {
                return AnalyzeBody();
            }
            catch (Exception ex)
            {
                this.ErrCode = -998;
                this.ErrMessage = ex.Message;
                return false;
            }

        }

        public virtual bool AnalyzeBody() { return false; }


        public virtual void CreateBody() { }

    }
}
