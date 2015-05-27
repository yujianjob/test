using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Business
{
    public class RoutePush
    {

        private LazyAtHome.WCF.Express.Interface.IDAL.IExpressDAL expressDAL;

        public RoutePush()
        {
            if (expressDAL == null)
                expressDAL = new LazyAtHome.WCF.Express.DAL.ExpressDAL();
        }

        static RoutePush _RoutePush;

        public static RoutePush Instance
        {
            get
            {
                if (_RoutePush == null)
                {
                    _RoutePush = new RoutePush();
                }
                return _RoutePush;
            }
        }

        public static string RoutePush_Url = System.Configuration.ConfigurationManager.AppSettings["RoutePush_Url"];

        public void Process()
        {
            RoutePushModel model = new RoutePushModel() { message = string.Empty, list = new List<Exp_RoutePushDC>() };

            var list = expressDAL.Exp_RoutePush_SELECT_List();

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    model.list.Add(item);
                    Console.WriteLine("RoutePush:" + item.OutNumber + " " + item.RouteInfo);
                }

                var data = "content=" + Stringify(model);

                var rtn = HttpRequestPost(RoutePush_Url, data);
                if (rtn == "SUCCESS")
                {
                    expressDAL.Exp_RoutePush_UPDATE_PushStatus(list.Select(p => p.ID).ToArray());
                }
            }


        }


        public static T ParseJson<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string Stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        private static string HttpRequestPost(string url, string data)
        {
            HttpWebRequest webRequest = null;
            Stream requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            webRequest.Method = "POST";
            byte[] postData = Encoding.UTF8.GetBytes(data);
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
                return string.Empty;
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
