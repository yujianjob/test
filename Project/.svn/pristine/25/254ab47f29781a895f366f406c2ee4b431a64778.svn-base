using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LazyAtHome.Core.Enumerate;

namespace LazyAtHome.Core.Helper
{
    /// <summary>
    /// 数据库服务
    /// </summary>
    public static class DBHelper
    {
        /// <summary>
        /// 获取数据库对象
        /// </summary>
        /// <param name="name">数据库实例名(默认name为空,调用默认数据库实例)</param>
        /// <returns>数据库对象</returns>
        public static Database CreateDataBase(ApplicationModule module = ApplicationModule.None, DataAccessPatterns patterns = DataAccessPatterns.Query)
        {
            string databaseName = "";

            switch (patterns)
            {
                case Enumerate.DataAccessPatterns.Query:
                    databaseName = "QueryConn";
                    break;
                case Enumerate.DataAccessPatterns.Write:
                    databaseName = "WriteConn";
                    break;
            }

            //return DatabaseFactory.CreateDatabase(name);
            return EnterpriseLibraryContainer.Current.GetInstance<Database>(databaseName);
        }

        /// <summary>
        /// 参数检验(True--参数合法 False--含有非法参数)
        /// </summary>
        /// <param name="self"></param>
        /// <returns>True--参数合法 False--含有非法参数</returns>
        public static bool CheckParms(string self)
        {
            string word = "exec|insert|delete|update|chr|master|truncate|char|declare|join|drop";
            if (self == null)
                return true;
            foreach (string i in word.Split('|'))
            {
                if ((self.ToLower().IndexOf(i + " ") > -1) || (self.ToLower().IndexOf(" " + i) > -1))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 参数检验(True--参数合法 False--含有非法参数)
        /// </summary>
        /// <param name="self"></param>
        /// <returns>True--参数合法 False--含有非法参数</returns>
        public static bool CheckParmsForTables(string self)
        {
            string word = "exec|insert|select|delete|update|chr|master|truncate|char|declare|drop";
            if (self == null)
                return true;
            foreach (string i in word.Split('|'))
            {
                if ((self.ToLower().IndexOf(i + " ") > -1) || (self.ToLower().IndexOf(" " + i) > -1))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// SQL替换'特殊字符
        /// </summary>
        /// <param name="iParam"></param>
        /// <returns></returns>
        public static string SqlParamReplace(string iParam)
        {
            string rtn = iParam.Replace("'", "''");

            return rtn;
        }

        /// <summary>
        /// SQL替换特殊字符
        /// </summary>
        /// <param name="iParam"></param>
        /// <returns></returns>
        public static string SqlLikeReplace(string iParam)
        {
            string rtn = string.Empty;
            for (int i = 0; i < iParam.Length; i++)
            {
                if (iParam[i].Equals('[') || iParam[i].Equals(']'))
                    rtn = rtn + "[" + iParam[i] + "]";
                else
                    rtn = rtn + iParam[i];
            }
            rtn = iParam.Replace("'", "''").Replace("%", "[%]").Replace("_", "[_]");
            return " '%" + rtn + "%' ";
        }

        /// <summary>
        /// SQL替换特殊字符
        /// </summary>
        /// <param name="iParam"></param>
        /// <returns></returns>
        public static string SqlLikeReplaceForParam(string iParam)
        {
            string rtn = string.Empty;
            for (int i = 0; i < iParam.Length; i++)
            {
                if (iParam[i].Equals('[') || iParam[i].Equals(']'))
                    rtn = rtn + "[" + iParam[i] + "]";
                else
                    rtn = rtn + iParam[i];
            }
            rtn = iParam.Replace("'", "''").Replace("%", "[%]").Replace("_", "[_]");
            return rtn;
        }
    }
}
