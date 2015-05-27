using LazyAtHome.Library.Partners.BaiduMap.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.BaiduMap
{
    public class Business
    {
        public void Test()
        {
            //var rt = ModelChange<GeocoderReturn>(
            //    "{\"status\":123,\"result\":{\"location\":{\"lng\":121.44,\"lat\":31.2},\"precise\":1,\"confidence\":70,\"level\":\"\"}}");
            //Console.WriteLine(rt);

            var str = geocoder("上海市", "莲花山路517弄");
            Console.WriteLine(str);
        }

        const string URL_Geocoding = "http://api.map.baidu.com/geocoder/v2/";

        const string ak = "CWzyX5xN9MzN63QuStTdw8rv";

        public GeocoderReturn geocoder(string city, string address)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(URL_Geocoding);

            sb.Append("?address=" + address);

            sb.Append("&city=" + city);

            sb.Append("&output=json");

            sb.Append("&ak=" + ak);

            var rtn = WebRequest(sb.ToString());

            if (string.IsNullOrWhiteSpace(rtn))
            {
                return null;
            }

            return ModelChange<GeocoderReturn>(rtn);
        }


        protected string WebRequest(string url)
        {
            HttpWebRequest webRequest = null;
            //Stream requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            webRequest.Method = "GET";
            //byte[] postData = Encoding.UTF8.GetBytes(xml);
            //webRequest.ContentLength = postData.Length;


            //requestWriter = webRequest.GetRequestStream();
            //try
            //{
            //    requestWriter.Write(postData, 0, postData.Length);
            //}
            //catch
            //{
            //    throw;
            //}
            //finally
            //{
            //    requestWriter.Close();
            //    requestWriter = null;
            //}

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

        private static T ModelChange<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                return (T)serializer.ReadObject(mStream);
            }
            catch (Exception ex)
            {
                throw new Exception("无法转换 " + jsonString + " 为:" + typeof(T));
            }
        }
    }
}
