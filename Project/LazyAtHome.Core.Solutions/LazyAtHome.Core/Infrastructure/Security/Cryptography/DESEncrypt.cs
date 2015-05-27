using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using LazyAtHome.Core.Enumerate;

namespace LazyAtHome.Core.Infrastructure.Security.Cryptography
{
    public class DESEncrypt
    {
        private byte[] bKey = new byte[] { 0x16, 0x83, 0x0A, 0x8D, 0x27, 0x25, 0x43, 0xBE };

        private byte[] Key
        {
            get
            {
                if (bKey == null)
                    bKey = GenerateKey();
                return bKey;
            }
        }

        private byte[] GenerateKey()
        {
            return null;
        }

        // 加密字符串   
        public string EncryptString(string sInputString, CryptoTypeEnum type = CryptoTypeEnum.BaseDES)
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Key;
            DES.IV = Key;
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result).Replace("-", "");
        }

        // 解密字符串   
        public string DecryptString(string sInputString, CryptoTypeEnum type = CryptoTypeEnum.BaseDES)
        {
            //string[] sInput = sInputString.Split("-".ToCharArray());
            //byte[] data = new byte[sInput.Length];
            //for (int i = 0; i < sInput.Length; i++)
            //{
            //    data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            //}
            byte[] data = ToHexByte(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Key;
            DES.IV = Key;
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }

        public byte[] ToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
    }
}
