using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Specialized;
using System.Web;
using System.Net;
using System.IO;

namespace LazyAtHome.Library.Pay.MobileEmbed.WXPayment
{
    public class WXPay
    {
        private const string Key = "ASCAmEwg5nth6DgJdAgEAAoGBAKIfHcE";
        private const string Charset = "utf-8";
        private const string Url_UnifiedOrder = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        private SortedList<string, string> _Parameters = new SortedList<string, string>();

        public WXPay()
        {

        }

        public SortedList<string, string> Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue"></param>
        public void SetParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (_Parameters.ContainsKey(parameter))
                    _Parameters.Remove(parameter);

                _Parameters.Add(parameter, parameterValue);
            }
        }

        /// <summary>
        /// 生成Sign
        /// </summary>
        public string CreateSign()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> item in _Parameters)
            {
                if (item.Value != null && item.Value != "" && item.Key != "sign")
                    sb.Append(item.Key + "=" + item.Value + "&");
            }

            sb.Append("key=" + Key);
            string sign = MD5Util.GetMD5(sb.ToString(), Charset).ToUpper();
            return sign;
        }

        /// <summary>
        /// 根据参数生成XML
        /// </summary>
        /// <returns></returns>
        public string ConvertToXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (KeyValuePair<string, string> item in _Parameters)
            {
                if (Regex.IsMatch(item.Value, @"^[0-9.]$"))
                    sb.Append("<" + item.Key + ">" + item.Value + "</" + item.Key + ">");
                else
                    sb.Append("<" + item.Key + "><![CDATA[" + item.Value + "]]></" + item.Key + ">");
            }
            sb.Append("</xml>");
            return sb.ToString();
        }

        /// <summary>
        /// 解析XML
        /// </summary>
        public void ParseXML(string responseXML)
        {
            _Parameters.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseXML);
            XmlNode root = xmlDoc.SelectSingleNode("xml");
            XmlNodeList xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                SetParameter(xnf.Name, xnf.InnerText);
            }
        }

        public string BuildRequest(string para)
        {
            //把数组转换成流中所需字节数组类型
            byte[] bytesRequestData = UTF8Encoding.UTF8.GetBytes(para);

            //请求远程HTTP
            string strResult = "";
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(Url_UnifiedOrder);
                myReq.Method = "POST";
                myReq.ContentType = "text/xml";

                //填充POST数据
                myReq.ContentLength = bytesRequestData.Length;
                Stream requestStream = myReq.GetRequestStream();
                requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                requestStream.Close();

                //发送POST数据请求服务器
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();

                //获取服务器返回信息
                StreamReader reader = new StreamReader(myStream, Encoding.UTF8);
                StringBuilder responseData = new StringBuilder();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }

                //释放
                myStream.Close();

                strResult = responseData.ToString();
            }
            catch (Exception exp)
            {
                strResult = "报错：" + exp.Message;
            }

            return strResult;
        }

        public string GetResponseXML(HttpContextBase httpContext)
        {
            string xml = "";

            if (httpContext.Request.InputStream.Length > 0)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(httpContext.Request.InputStream);
                XmlNode root = xmlDoc.SelectSingleNode("xml");                
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    SetParameter(xnf.Name, xnf.InnerText);
                    xml += xnf.Name + ":" + xnf.InnerText + " ";
                }
            }

            return xml;
        }

        public bool Verify()
        {
            string sign = CreateSign();
            if (sign == _Parameters["sign"])
                return true;
            else
                return false;
        }
    }
}