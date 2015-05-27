using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.QFExpress.Business
{
    public class BusinessBase
    {
        public BusinessBase()
        {

        }

        protected string qfexpressService(string xml)
        {
            HttpWebRequest webRequest = null;
            Stream requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(Config.PostUrl) as HttpWebRequest;
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            webRequest.Method = "POST";
            byte[] postData = Encoding.UTF8.GetBytes(xml);
            webRequest.ContentLength = postData.Length;


            requestWriter = webRequest.GetRequestStream();
            try
            {
                requestWriter.Write(postData, 0, postData.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
                requestWriter = null;
            }

            responseData = WebResponseGet(webRequest);
            webRequest = null;
            return responseData;
        }

        ///<summary> 
        /// 获得请求返回值 
        ///</summary> 
        ///<param name="webRequest">请求</param>
        private static string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }






    }
}
