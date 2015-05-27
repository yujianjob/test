using LazyAtHome.Core.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace LazyAtHome.Service.SMS
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public class Global
    {

        private static System.Timers.Timer _Timer_Day;

        public static IService YMsmsService;
        public static IService JZsmsService;
        public static IService YMsmsWebService;
        public static IService smsService;
        
        public static void Init()
        {
            //短信服务实例化
            YMsmsService = new YMService();
            //try
            //{
            //    YMsmsService.Service_UnRegister();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //YMsmsService.Service_Register();
            
            JZsmsService = new JZService();
            
            YMsmsWebService = new YMWebService();

            //YMsmsWebService.Service_Register();

            smsService = YMsmsWebService;

            #region 管理员短信

            _Timer_Day = new System.Timers.Timer();
            _Timer_Day.AutoReset = true;
            _Timer_Day.Interval = GetSetDateTimeTicks("09:00:00");

            _Timer_Day.Elapsed += _Timer_Day_Elapsed;

            var tmpAdminMPNo = System.Configuration.ConfigurationManager.AppSettings["AdminMPNo"];
            if (!string.IsNullOrWhiteSpace(tmpAdminMPNo))
            {
                foreach (var item in tmpAdminMPNo.Split(';'))
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        Global.AdminMPNo.Add(item);
                    }
                }
                if (Global.AdminMPNo.Count > 0)
                {
                    _Timer_Day.Start();
                    Console.WriteLine("开启服务状态定时推送:" + tmpAdminMPNo);
                }
            }
            else
            {
                Console.WriteLine("不开启服务状态定时推送.");
            }

            #endregion

            //黑名单 - 重启后生效
            BlackList = oDataAccess.Base_SmsSend_BlackList();

            _Timer_Day_Elapsed(null, null);
        }

        private static long GetSetDateTimeTicks(string settime)
        {
            var t = DateTime.Parse(DateTime.Now.Date.ToShortDateString() + " " + settime);
            if (t > DateTime.Now)
            {
                return Convert.ToInt64((t - DateTime.Now).TotalMilliseconds);
            }
            else if (t < DateTime.Now)
            {
                return Convert.ToInt64((t - DateTime.Now.AddDays(-1)).TotalMilliseconds);
            }
            else
            {
                return 1000 * 60 * 60 * 24;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _Timer_Day_Elapsed(object sender, ElapsedEventArgs e)
        {
            _Timer_Day.Stop();
            _Timer_Day.Interval = GetSetDateTimeTicks("09:00:00");
            try
            {
                string msg = "短信服务定时推送,";

                var balance = YMsmsWebService.Service_Balance();

                msg += "剩余金额:" + balance;

                //foreach (var item in Global.AdminMPNo)
                //{
                //    YMsmsService.Service_SendSMS(item, "短信服务定时推送,当前余量:" + balance, 5, 1);
                //}

                //balance = JZsmsService.Service_Balance();

                //msg += ",j:" + balance;

                //Console.WriteLine(msg);
                //return;

                foreach (var item in Global.AdminMPNo)
                {
                    smsService.Service_SendSMS(item, msg, 5, 1);
                }

            }
            catch (Exception ex)
            {
                Global.Log_Fatal(ex);
            }
            _Timer_Day.Start();
        }

        /// <summary>
        /// 管理员手机 多个手机号码用;分割
        /// </summary>
        //public const string AdminMPNo = "18616940990;13524622579;18601717193";
        public static IList<string> AdminMPNo = new List<string>();

        /// <summary>
        /// 签名
        /// </summary>
        //public const string Sign = "【懒到家】";
        public static string Sign = System.Configuration.ConfigurationManager.AppSettings["Sign"];

        /// <summary>
        /// 数据库链接
        /// </summary>
        public static DAL oDataAccess = new DAL();

        /// <summary>
        /// 黑名单
        /// </summary>
        public static HashSet<string> BlackList { get; set; }

        public static void Log_Fatal(Exception iException)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            //VEBS.Core.Helper.LogHelper.Log_Fatal("调用异常 " + iMethodName, 99, iException.Message + "\r\n" + GetFrameString(iException));
            var errMsg = iException.Message + "\r\n" + GetFrameString(iException);
            Console.WriteLine(errMsg);
            WriteToFile(DateTime.Today.ToString("yyyy-MM-dd"), errMsg);
            Console.ResetColor();
        }

        /// <summary>
        /// 获取异常堆栈
        /// </summary>
        /// <param name="iException"></param>
        /// <returns></returns>
        public static string GetFrameString(Exception iException)
        {
            bool flag = true;
            Hashtable rtn = new Hashtable();
            try
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(iException);
                foreach (System.Diagnostics.StackFrame frame in trace.GetFrames())
                {
                    if (flag)
                    {
                        flag = false;
                        continue;
                    }
                    if (frame.GetMethod().IsPublic)
                    {
                        //方法名
                        var methodName = frame.GetMethod().Name;

                        //类名
                        var tmpString = string.Empty;
                        if (frame.GetMethod().DeclaringType != null)
                        {
                            //方法名
                            tmpString = frame.GetMethod().DeclaringType.FullName + ".";
                            if (tmpString.StartsWith("System."))
                            {
                                //如果是系统级的，直接跳过
                                continue;
                            }
                        }
                        tmpString += methodName;
                        if (!rtn.ContainsKey(tmpString))
                            rtn.Add(tmpString, null);
                    }
                }
            }
            catch { }
            var rtnstring = "\n";
            foreach (var item in rtn.Keys)
            {
                rtnstring += item + "\n";
            }
            return rtnstring;
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
                rwl.ReleaseWriterLock();
            }
        }
    }
}
