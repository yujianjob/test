using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.SFSupport.CodeSource
{
    public enum AlertMsgType
    {
        /// <summary>
        /// 成功信息
        /// </summary>
        Correct,
        /// <summary>
        /// 提示信息
        /// </summary>
        Info,
        /// <summary>
        /// 警告信息
        /// </summary>
        Warn,
        /// <summary>
        /// 错误信息
        /// </summary>
        Error
    }
    public class WebUtility
    {
        public static string AlertMsg(AlertMsgType type, string msg)
        {
            string strType = null;
            switch (type)
            {
                case AlertMsgType.Info:
                    strType = "info";
                    break;
                case AlertMsgType.Correct:
                    strType = "correct";
                    break;
                case AlertMsgType.Warn:
                    strType = "warn";
                    break;
                case AlertMsgType.Error:
                    strType = "error";
                    break;
            }

            string str = "<script type=\"text/javascript\">alertMsg.{0}('{1}')</script>";
            return string.Format(str, strType, msg);
        }
    }
}