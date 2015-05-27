using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.LazyCard
{
    public static class FileController
    {
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
                string Path = System.Environment.CurrentDirectory + "\\";

                //写入文件
                Path += FileName + ".txt";

                rwl.AcquireWriterLock(Timeout.Infinite);

                using (StreamWriter sw = new StreamWriter(Path, true, Encoding.Default))
                {
                    sw.WriteLine(Content);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }

    }
}
