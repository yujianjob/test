using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace LazyAtHome.API.TinyURL.App_Code
{
    public class UtilityFunc
    {
        public static ReaderWriterLock ReadWriteLock = new ReaderWriterLock();

        public static string LogPath = @"c:\AppLog\webapi\";

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
    }
}