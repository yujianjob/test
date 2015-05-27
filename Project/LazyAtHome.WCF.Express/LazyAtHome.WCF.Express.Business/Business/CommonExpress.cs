using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Business
{
    public class CommonExpress
    {
        //private static LazyAtHome.WCF.Express.Interface.IDAL.IExpressDAL expressDAL;

        private static System.Timers.Timer _Timer_RoutePush;

        private static System.Timers.Timer _Timer_AutoAllocation;

        private static System.Timers.Timer _Timer_GetAlarm;

        public static void Init()
        {
            //if (expressDAL == null)
            //    expressDAL = new LazyAtHome.WCF.Express.DAL.ExpressDAL();

            Console.WriteLine("RoutePush线程开启...");

            //路由推送
            _Timer_RoutePush = new System.Timers.Timer();
            _Timer_RoutePush.AutoReset = true;
            _Timer_RoutePush.Interval = 1000 * 10;

            _Timer_RoutePush.Elapsed += _Timer_RoutePush_Elapsed;

            _Timer_RoutePush.Start();

            Console.WriteLine("自动分配线程开启...");

            //自动分配
            _Timer_AutoAllocation = new System.Timers.Timer();
            _Timer_AutoAllocation.AutoReset = true;
            _Timer_AutoAllocation.Interval = 1000 * 10;

            _Timer_AutoAllocation.Elapsed += _Timer_AutoAllocation_Elapsed;

            _Timer_AutoAllocation.Start();

            Console.WriteLine("无人接单预警线程开启...");

            //无人接单预警
            _Timer_GetAlarm = new System.Timers.Timer();
            _Timer_GetAlarm.AutoReset = true;
            _Timer_GetAlarm.Interval = 1000 * 60 * 1;
            //_Timer_GetAlarm.Interval = 1000 * 60 * 5;//正式版应设置5分钟

            _Timer_GetAlarm.Elapsed += _Timer_GetAlarm_Elapsed;
            
            _Timer_GetAlarm.Start();

        }

        //
        static void _Timer_GetAlarm_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _Timer_GetAlarm.Stop();
            try
            {
                GetAlarm.Instance.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine("无人接单预警错误:" + ex.Message);
            }
            finally
            {
                _Timer_GetAlarm.Start();
            }
        }

        static void _Timer_AutoAllocation_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _Timer_AutoAllocation.Stop();
            try
            {
                Allocation.Instance.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine("自动分配错误:" + ex.Message);
            }
            finally
            {
                _Timer_AutoAllocation.Start();
            }
        }

        static void _Timer_RoutePush_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _Timer_RoutePush.Stop();

            try
            {
                RoutePush.Instance.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine("信息推送错误:" + ex.Message);
            }
            finally
            {
                _Timer_RoutePush.Start();
            }
        }

        static Random rd = new Random(new object().GetHashCode());

        static LazyAtHome.Core.Helper.QueueHelper queue = new Core.Helper.QueueHelper();

        public static void NotifySend(string iOrderNumber, int iRoleID, int iPersonnelID, int iClass,
            string iTitle, string iContent, int iLevel, bool iIsSms, bool iIsEmail, int iStatus)
        {
            LazyAtHome.Service.WorkQueue.Contract.DataContract.Notify.NotifyMessageItem item = new Service.WorkQueue.Contract.DataContract.Notify.NotifyMessageItem();

            item.EventNumber = "EX" + DateTime.Now.ToString("yyMMddHH") + (rd.Next(0, 10000) % 10000).ToString().PadLeft(4, '0');
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
                Console.WriteLine("NotifySend:" + ex.Message);
            }
        }

    }
}
