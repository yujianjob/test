using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Infrastructure.Security.Cryptography;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace LazyAtHome.Core.Helper
{
    /// <summary>
    /// 加密服务
    /// </summary>
    public static class CryptoHelper
    {
        const string SymmetricKey = "DESCryptoServiceProvider";


        public static string MD5Encrypt(string val, string _input_charset = "utf-8")
        {
            StringBuilder sb = new StringBuilder(32);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(val));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        public static bool MD5Verify(string val, string sign, string _input_charset = "utf-8")
        {
            string mysign = MD5Encrypt(val, _input_charset);
            if (mysign == sign)
                return true;
            else
                return false;
        }

        public static string DESEncryptString(string val, CryptoTypeEnum type = CryptoTypeEnum.BaseDES)
        {
            DESEncrypt des = new DESEncrypt();
            return des.EncryptString(val, type);
        }

        public static string DESDecryptString(string val, CryptoTypeEnum type = CryptoTypeEnum.BaseDES)
        {
            DESEncrypt des = new DESEncrypt();
            return des.DecryptString(val, type);
        }

        /// <summary>
        /// 离散加密
        /// </summary>
        /// <param name="val"></param>
        /// <param name="cm"></param>
        /// <returns></returns>
        public static string Encrypt(string val, Enumerate.CryptoMode cm = Enumerate.CryptoMode.MD5)
        {
            string rtn = null;
            switch (cm)
            {
                case Enumerate.CryptoMode.MD5:
                    rtn = Cryptographer.CreateHash("MD5Cng", val);
                    break;
                case Enumerate.CryptoMode.SHA1:
                    rtn = Cryptographer.CreateHash("SHA1Cng", val);
                    break;
            }
            return rtn;
        }

        /// <summary>
        /// 验证离散加密
        /// </summary>
        /// <param name="val">明文</param>
        /// <param name="cngval">密文</param>
        /// <param name="cm">加密模式</param>
        /// <returns></returns>
        public static bool CompareEncrypt(string val, string cngval, Enumerate.CryptoMode cm = Enumerate.CryptoMode.MD5)
        {
            switch (cm)
            {
                case Enumerate.CryptoMode.MD5:
                    return Cryptographer.CompareHash("MD5Cng", val, cngval);
                case Enumerate.CryptoMode.SHA1:
                    return Cryptographer.CompareHash("SHA1Cng", val, cngval);
                default:
                    return false;
            }
        }

        /// <summary>
        /// 对称加密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string EncryptSymmetric(string val)
        {
            string rtn = null;
            rtn = Cryptographer.EncryptSymmetric(SymmetricKey, val);
            return rtn;
        }

        /// <summary>
        /// 对称解密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string DecryptSymmetric(string val)
        {
            string rtn = null;
            rtn = Cryptographer.DecryptSymmetric(SymmetricKey, val);
            return rtn;
        }
    }
}
