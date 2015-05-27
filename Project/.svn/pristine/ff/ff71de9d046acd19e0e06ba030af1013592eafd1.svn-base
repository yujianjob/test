using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyAtHome.Service.WorkQueue.Service
{
    public class TaskServer
    {

        private bool _Quit;
        private bool _Serving = false;
        private int _SleepTime = 1000;

        public TaskServer()
        {
        }

        public bool Serving
        {
            get
            {
                return _Serving;
            }
            set
            {
                _Serving = value;
            }
        }

        /// <summary>
        /// 执行间隔(毫秒)
        /// </summary>
        public int SleepTime
        {
            get { return _SleepTime; }
            set { _SleepTime = value; }
        }

        public bool TryQuit
        {
            get
            {
                return _Quit;
            }
            set
            {
                _Quit = value;
            }
        }


        protected virtual void Process(object oTask)
        {

        }

        protected virtual object GetTask()
        {
            object oTask = null;

            try
            {
                try
                {
                    //if (TaskList.Count == 0)
                    //{
                    //    oTask = null;
                    //}
                    //else
                    //{
                    //    oTask = TaskList[0];
                    //}
                }
                catch
                {
                    oTask = null;
                }
            }
            catch
            {
                //*TimeOut
                oTask = null;
            }

            if (oTask == null) return null;

            try
            {

                try
                {
                    //TaskList.RemoveAt(0);
                }
                catch
                {
                    oTask = null;
                }
            }
            catch
            {
                //*TimeOut
                oTask = null;
            }
            return oTask;

        }

        public void Serve()
        {
            while (true)
            {
                object oTask = GetTask();

                if (oTask == null)
                {
                    System.Threading.Thread.Sleep(SleepTime);
                }
                else
                {
                    Process(oTask);
                }

                if (TryQuit)
                {
                    break;
                }
            }
            TryQuit = false;
            _Serving = false;
        }
    }
}
