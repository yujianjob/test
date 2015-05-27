using LazyAtHome.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LazyAtHome.Service.SMS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = System.Configuration.ConfigurationManager.AppSettings["Title"] + " - SMSService";

            try
            {
                Global.Init();
            }
            catch (Exception ex)
            {
                Console.WriteLine("遇到严重错误,按回车结束程序...");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            System.Threading.WaitCallback Callback_SmsSend = new WaitCallback(SMSSendThreadWork);
            ThreadPool.QueueUserWorkItem(Callback_SmsSend);
            Console.ReadLine();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Global.Log_Fatal(e.ExceptionObject as Exception);
        }

        static Regex rMobile = new System.Text.RegularExpressions.Regex("^1\\d{10}$", System.Text.RegularExpressions.RegexOptions.Compiled);



        static void SMSSendThreadWork(object state)
        {
            Console.WriteLine("短信发送线程开启...");
            Global.WriteToFile(DateTime.Today.ToString("yyyy-MM-dd"), "短信发送线程开启...");
            DAL oDataAccess = Global.oDataAccess;
            while (true)
            {
                System.Threading.Thread.Sleep(5000);

                try
                {

                    var xList = oDataAccess.Base_SmsSend_SELECT_List();
                    if (xList.Count == 0)
                    {
                        continue;
                    }
                    foreach (var item in xList)
                    {
                        //手机号验证 1开头
                        if (!rMobile.IsMatch(item.Mobile))
                        {
                            oDataAccess.Base_SmsSend_UPDATE(item.ID, item.Mobile, -999, item.Count);
                            continue;
                        }
                        //黑名单
                        if (Global.BlackList.Contains(item.Mobile))
                        {
                            oDataAccess.Base_SmsSend_UPDATE_Black(item.ID);
                            continue;
                        }
                        //超过3次重试失败
                        if (item.ReTry > 3)
                        {
                            oDataAccess.Base_SmsSend_UPDATE_ReTry(item.ID);
                            continue;
                        }
                        //超过10条
                        if (item.Count >= 10)
                        {
                            oDataAccess.Base_SmsSend_UPDATE_Count(item.ID);
                            continue;
                        }

                        var rtn = -9999;

                        try
                        {
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + item.Mobile + "  " + item.Content);
                            rtn = Global.smsService.Service_SendSMS(item.Mobile, item.Content, item.Priority, item.Type);

                            ////亿美软通
                            //if (item.Channel == 2 || item.Channel == 0)
                            //{
                            //    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + item.Mobile + "  " + item.Content + "\tYM");
                            //    rtn = Global.smsService.Service_SendSMS(item.Mobile, item.Content, item.Priority, item.Type);
                            //}
                            ////建周短信
                            //else
                            //{
                            //    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + item.Mobile + "  " + item.Content + "\tJZ");
                            //    rtn = Global.smsService.Service_SendSMS(item.Mobile, item.Content, item.Priority, item.Type);
                            //}
                        }
                        catch (Exception ex)
                        {
                            Global.Log_Fatal(ex);
                        }

                        if (rtn > 0)
                        {
                            oDataAccess.Base_SmsSend_UPDATE(item.ID, item.Mobile, 2, item.Count);
                        }
                        else
                        {
                            oDataAccess.Base_SmsSend_UPDATE(item.ID, item.Mobile, rtn, item.Count);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Global.Log_Fatal(ex);
                }
            }
        }
    }
}
