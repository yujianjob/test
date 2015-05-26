using LazyAtHome.API.Mobile.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.JsonResultModels
{
    public class BaseResult
    {
        public int code { get; set; }

        public string message { get; set; }

        public BaseResult()
        {
            message = string.Empty;
        }
        public BaseResult(int code, string message)
            : base()
        {
            this.code = code;
            this.message = message;
        }

        public static BaseResult BadResult(int code, string message)
        {
            PublicFun.LogAdd(code + "  " + message);

            return new BaseResult(code, message);
        }

        public static BaseResult SystemError(int code, string message)
        {
            PublicFun.LogAdd(message);

            return new BaseResult(code, message);
        }

        public static BaseResult SystemError(Exception ex)
        {
            PublicFun.LogAdd(ex.Message);

            return new BaseResult(2004, "System Error");
        }

        public static BaseResult EmptyResult
        {
            get
            {
                PublicFun.LogAdd("接口返回对象空");

                return new BaseResult(2004, "System Error");
            }
        }

        public static BaseResult MD5Error
        {
            get
            {
                return new BaseResult(2001, "MD5 Error");
            }
        }
        public static BaseResult ActidError
        {
            get
            {
                return new BaseResult(2002, "Actid Error");
            }
        }
        public static BaseResult ParametersError
        {
            get
            {
                return new BaseResult(2003, "Parameters Error");
            }
        }

        /// <summary>
        /// 成功对象
        /// </summary>
        public static BaseResult Success
        {
            get
            {
                return new BaseResult();
            }
        }

        public override string ToString()
        {
            Type type = this.GetType();

            PropertyInfo[] plist = type.GetProperties();

            var rtn = string.Empty;

            foreach (PropertyInfo info in plist)
            {
                try
                {
                    rtn += info.Name + "[" + info.GetValue(this) + "] ";
                }
                catch
                {
                }
            }

            return rtn;
        }
    }
}