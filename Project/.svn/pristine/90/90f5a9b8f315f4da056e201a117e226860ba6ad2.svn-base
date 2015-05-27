using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners
{
    public delegate void de_WriteToFile(string FileName, string Content);

    public static class Common
    {
        public static byte[] ToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }

        public static event de_WriteToFile WriteToFileEvent;

        public static void WriteToFile(string FileName, string Content)
        {
            if (WriteToFileEvent != null)
            {
                WriteToFileEvent(FileName, Content);
            }
        }

        //static string LogPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="FileName"></param>
        ///// <param name="Content"></param>
        //public static void WriteToFile(string FileName, string Content)
        //{
        //    ReaderWriterLock rwl = new ReaderWriterLock();
        //    try
        //    {
        //        if (!System.IO.Directory.Exists(LogPath))
        //        {
        //            Directory.CreateDirectory(LogPath);
        //        }

        //        //写入文件
        //        LogPath += FileName + ".txt";

        //        rwl.AcquireWriterLock(Timeout.Infinite);

        //        using (StreamWriter sw = new StreamWriter(LogPath, true, Encoding.Default))
        //        {
        //            sw.WriteLine(DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + Content);
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        rwl.ReleaseWriterLock();
        //    }
        //}
    }
}
