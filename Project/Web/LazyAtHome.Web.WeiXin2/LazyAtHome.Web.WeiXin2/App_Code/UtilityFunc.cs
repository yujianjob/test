﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Threading;
using System.Net;
using System.IO;
using System.Text;

namespace LazyAtHome.Web.WeiXin2.App_Code
{
    public class UtilityFunc
    {
        public static ReaderWriterLock ReadWriteLock = new ReaderWriterLock();
        public static string WebRequest(string url, string postDataStr)
        {
            HttpWebRequest webRequest = null;
            Stream requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] postData = Encoding.UTF8.GetBytes(postDataStr);
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
        public static string WebResponseGet(HttpWebRequest webRequest)
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

        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key != "sign" && temp.Value != "" && temp.Value != null)
                    dicArray.Add(temp.Key, temp.Value);
                else if (temp.Key == "msg_sys_sn" || temp.Key == "coupon_no")
                    dicArray.Add(temp.Key, temp.Value);
            }

            return dicArray;
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }



        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value, code) + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);
            return prestr.ToString();
        }

        public static string LogPath = @"c:\AppLog\WeiXin2\";

        /// <summary>
        /// 将信息写入日志文件中
        /// </summary>
        /// <param name="strMessage"></param>
        public static void Add(string strMessage)
        {
            string LogFileName = LogPath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            try
            {
                ReadWriteLock.AcquireWriterLock(2000);
                strMessage = strMessage + " " + DateTime.Now.ToString() + "\r\n";
                using (StreamWriter sw = new StreamWriter(LogFileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(strMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ReadWriteLock.ReleaseWriterLock();
            }
            Console.WriteLine(strMessage);
        }

        /// <summary>
        /// 将信息写入日志文件中
        /// </summary>
        /// <param name="strMessage"></param>
        public static void Add(string title, string strMessage)
        {
            string LogFileName = LogPath + DateTime.Now.ToString("yyyy-MM-dd") + title + ".txt";

            try
            {
                ReadWriteLock.AcquireWriterLock(2000);
                strMessage = strMessage + " " + DateTime.Now.ToString() + "\r\n";
                using (StreamWriter sw = new StreamWriter(LogFileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(strMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ReadWriteLock.ReleaseWriterLock();
            }
            Console.WriteLine(strMessage);
        }

        public static string GetMD5(string str)
        {
            string md5 = "";
            MD5 md5Hasher = MD5.Create();
            byte[] sdataBytes = Encoding.Default.GetBytes(str);
            byte[] data = md5Hasher.ComputeHash(sdataBytes);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("X2"));
            md5 = sBuilder.ToString();
            return md5;
        }
    }
}