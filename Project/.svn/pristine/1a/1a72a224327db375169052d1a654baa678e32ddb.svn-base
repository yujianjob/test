using LazyAtHome.Core.Infrastructure.WCF.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Portal
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class CommonPortal : ServiceBase
    {

        protected void Log_Fatal(string iMethodName, Exception iException)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            //VEBS.Core.Helper.LogHelper.Log_Fatal("调用异常 " + iMethodName, 99, iException.Message + "\r\n" + GetFrameString(iException));
            Console.WriteLine("调用异常 " + iMethodName + 99 + iException.Message + "\r\n" + GetFrameString(iException));
            Console.ResetColor();
        }

        protected void Log_Warn(string iMethodName, string iMessage)
        {
            //VEBS.Core.Helper.LogHelper.Log_Info(iMethodName, 99, iMessage);
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

        /// <summary>
        /// 调用日志
        /// </summary>
        /// <param name="iMethodName"></param>
        /// <param name="iParams"></param>
        protected void Log_DeBug(string iMethodName, params object[] iParams)
        {
            for (int i = 0; i < iParams.Length; i++)
            {
                if (iParams[i] == null)
                {
                    iParams[i] = string.Empty;
                }
            }

            Console.WriteLine(string.Format("{0}\t{1}", iMethodName, String.Join("_", iParams)));
            //LazyAtHome.Core.Helper..Log_Debug("调用日志", 99
            //        , string.Format("{0}\t{1}"
            //        , iMethodName
            //        , String.Join("_", iParams)
            //        )
            //    );
        }

    }
}
