using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Business.Business
{
    public static class Config
    {

        public static class FactoryAddress
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public static string Contact = System.Configuration.ConfigurationManager.AppSettings["Contact"];

            /// <summary>
            /// 公司
            /// </summary>
            public static string Company = System.Configuration.ConfigurationManager.AppSettings["Company"];

            /// <summary>
            /// 邮编
            /// </summary>
            public static string ZipCode = System.Configuration.ConfigurationManager.AppSettings["ZipCode"];

            /// <summary>
            /// 电话
            /// </summary>
            public static string Tel = System.Configuration.ConfigurationManager.AppSettings["Tel"];

            /// <summary>
            /// 用户移动电话[手机和电话两者必需提供一个]
            /// </summary>
            public static string Phone = System.Configuration.ConfigurationManager.AppSettings["Phone"];

            /// <summary>
            /// 所在省
            /// </summary>
            public static string Province = System.Configuration.ConfigurationManager.AppSettings["Province"];

            /// <summary>
            /// 所在市县（区）
            /// </summary>
            public static string City = System.Configuration.ConfigurationManager.AppSettings["City"];

            /// <summary>
            /// 地址
            /// </summary>
            public static string Address = System.Configuration.ConfigurationManager.AppSettings["Address"];

        }

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
            catch (Exception ex)
            {
                Console.WriteLine("记录日志错误:" + ex.Message);
            }
            finally
            {
                if (rwl.IsReaderLockHeld)
                    rwl.ReleaseWriterLock();
            }
        }

        static Random rd = new Random(new object().GetHashCode());
        static LazyAtHome.Core.Helper.QueueHelper queue = new Core.Helper.QueueHelper();

        public static void NotifySend(string iOrderNumber, int iRoleID, int iPersonnelID, int iClass,
            string iTitle, string iContent, int iLevel, bool iIsSms, bool iIsEmail, int iStatus)
        {
            LazyAtHome.Service.WorkQueue.Contract.DataContract.Notify.NotifyMessageItem item = new Service.WorkQueue.Contract.DataContract.Notify.NotifyMessageItem();

            item.EventNumber = "OD" + DateTime.Now.ToString("yyMMddHH") + (rd.Next(0, 10000) % 10000).ToString().PadLeft(4, '0');
            item.OrderNumber = iOrderNumber;

            item.RoleID = iRoleID;
            item.PersonnelID = iPersonnelID;

            item.Class = iClass;
            item.Level = iLevel;

            item.Title = iTitle;
            item.Content = iContent;

            item.IsSms = iIsSms;
            item.IsEmail = iIsEmail;

            item.Status = iStatus;

            //IList<int> nUserList = new List<int>();
            //nUserList.Add(101);
            //nUserList.Add(32);
            //item.NotifyUserList = nUserList;

            try
            {
                queue.Send(item);
                Console.WriteLine("NotifySend 发送完成 " + item.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
