using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Business
{
    public static class Common
    {
        static string LogPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Content"></param>
        public static void WriteToFile(string FileName, string Content)
        {
            ReaderWriterLock rwl = new ReaderWriterLock();
            try
            {
                if (!System.IO.Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }

                rwl.AcquireWriterLock(Timeout.Infinite);

                using (StreamWriter sw = new StreamWriter(LogPath + FileName + ".txt", true, Encoding.Default))
                {
                    sw.WriteLine(DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss") + Content);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (rwl.IsReaderLockHeld)
                    rwl.ReleaseWriterLock();
            }
        }
    }
}
