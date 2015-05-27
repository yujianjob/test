using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace LazyAtHome.API.Mobile.App_Code
{
    public static class PublicFun
    {

        public static Random rd = new Random();

        public const string LogPath = @"c:\AppLog\Mobile\";
        private static ReaderWriterLock ReadWriteLock = new ReaderWriterLock();

        /// <summary>
        /// 将信息写入日志文件中
        /// </summary>
        /// <param name="strMessage"></param>
        public static void LogAdd(string strMessage)
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
        }

        /// <summary>
        /// 获取登录名的类型
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static LazyAtHome.WCF.UserSystem.Contract.Enumerate.LoginType GetLoginNameType(string loginName)
        {
            if (string.IsNullOrWhiteSpace(loginName))
            {
                return WCF.UserSystem.Contract.Enumerate.LoginType.LoginName;
            }
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (Regex.IsMatch(loginName, expression, RegexOptions.Compiled))
            {
                return WCF.UserSystem.Contract.Enumerate.LoginType.Email;
            }
            expression = @"^1[3458]\d{9}$";
            if (Regex.IsMatch(loginName, expression, RegexOptions.Compiled))
            {
                return WCF.UserSystem.Contract.Enumerate.LoginType.MPNo;
            }

            return WCF.UserSystem.Contract.Enumerate.LoginType.LoginName;
        }

        #region ECB模式
        /// <summary>
        /// DES3 ECB模式加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">明文的byte数组</param>
        /// <returns>密文的byte数组</returns>
        public static string Des3EncodeECB(string data)
        {
            try
            {
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                DES.Key = APPConfig.Des_key;
                DES.Mode = CipherMode.ECB;
                DES.Padding = PaddingMode.PKCS7;
                ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                var t_data = System.Text.Encoding.UTF8.GetBytes(data);
                var rtn = DESEncrypt.TransformFinalBlock(t_data, 0, t_data.Length);
                return Convert.ToBase64String(rtn);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("错误的转换: {0}", e.Message);
                return null;
            }
        }


        /// <summary>
        /// DES3 ECB模式解密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">密文的byte数组</param>
        /// <returns>明文的byte数组</returns>
        public static string Des3DecodeECB(string data)
        {
            try
            {
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                DES.Key = APPConfig.Des_key;
                DES.Mode = CipherMode.ECB;
                DES.Padding = PaddingMode.PKCS7;
                ICryptoTransform DESEncrypt = DES.CreateDecryptor();
                var t_data = Convert.FromBase64String(data);
                var rtn = DESEncrypt.TransformFinalBlock(t_data, 0, t_data.Length);
                return System.Text.Encoding.UTF8.GetString(rtn);
            }
            catch (CryptographicException e)
            {
                LogAdd("Des解密失败:[" + data + "]" + e.Message);
                return null;
            }
        }

        #endregion

        public static string CreateAlipayPayParam(string out_trade_no, string subject, string body, decimal total_fee)
        {
            if (total_fee <= 0)
            {
                return string.Empty;
            }

            LogAdd(string.Format("CreateAlipayPayParam:{0} {1} {2} {3}", out_trade_no, subject, body, total_fee));

            try
            {
                var rtn = LazyAtHome.Library.Pay.MobileEmbed.Alipay.AlipayEmbed.CreateParamV12(out_trade_no, subject, body, total_fee);

                LogAdd(string.Format("CreateAlipayPayParam Result:{0}", rtn));

                return rtn;

            }
            catch (Exception ex)
            {
                LogAdd(string.Format("CreateAlipayPayParam Error:{0}", ex.Message));
                return string.Empty;
            }
        }
    }
}