using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public class CacheTimer
    {
        public static TimeSpan GetTimeSpanOneHour
        {
            get
            {
                return new TimeSpan(0, (DateTime.Now.Minute >= 5 ? (65 - DateTime.Now.Minute) : (5 - DateTime.Now.Minute)), 0);
            }
        }

        public static TimeSpan GetTimeSpanOneDay
        {
            get
            {
                return new TimeSpan((25 - DateTime.Now.Hour), 5, 0);
            }
        }

        private static System.Timers.Timer Timer;

        private static System.Timers.Timer _Timer_JPush;

        public static void Init()
        {
            //服务器工作方式
            Timer = new System.Timers.Timer();
            Timer.AutoReset = true;
            Timer.Interval = 1000 * 60 * 60 * 4;//6小时
            //Timer.Interval = 1000 * 60 * 3;//3分钟
            Timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            Timer.Start();
            timer_Elapsed(null, null);


            Console.WriteLine("手机推送服务线程开启...");

            //手机推送服务
            _Timer_JPush = new System.Timers.Timer();
            _Timer_JPush.AutoReset = true;
            _Timer_JPush.Interval = 1000 * 5;

            _Timer_JPush.Elapsed += _Timer_JPush_Elapsed;

            _Timer_JPush.Start();
        }

        static void _Timer_JPush_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _Timer_JPush.Stop();
            try
            {
                JPush.Instance.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine("手机推送服务线程错误:" + ex.Message);
            }
            finally
            {
                _Timer_JPush.Start();
            }
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("*****************定时更新缓存**************");
            PR.Instance.SetCache();
            Base.Instance.SetCache();
            Console.WriteLine("*****************定时更新缓存结束**************");
        }

    }
}
