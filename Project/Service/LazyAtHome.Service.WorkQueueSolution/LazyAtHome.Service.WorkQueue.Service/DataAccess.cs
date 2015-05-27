using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;
using LazyAtHome.Core.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LazyAtHome.Service.WorkQueue.Service
{
    public class DataAccess
    {
        public static void SqlCommandOperation_NonQuery(string pluginType, DbCommand cmd, LazyAtHome.Core.Enumerate.DataAccessPatterns dataAccess = LazyAtHome.Core.Enumerate.DataAccessPatterns.Write)
        {
            Database db = DBHelper.CreateDataBase(LazyAtHome.Core.Enumerate.ApplicationModule.WorkQueue, dataAccess);
            //DbCommand CurrCMD = db.GetSqlStringCommand(cmd.CommandText);
            //CurrCMD.CommandType = cmd.CommandType;
            //foreach (DbParameter param in cmd.Parameters)
            //    CurrCMD.Parameters.Add(param.);

            db.ExecuteNonQuery(cmd);
        }

        public static object SqlCommandOperation_Scalar(string pluginType, DbCommand cmd, LazyAtHome.Core.Enumerate.DataAccessPatterns dataAccess = LazyAtHome.Core.Enumerate.DataAccessPatterns.Query)
        {
            Database db = DBHelper.CreateDataBase(LazyAtHome.Core.Enumerate.ApplicationModule.WorkQueue, dataAccess);
            DbCommand CurrCMD = db.GetSqlStringCommand(cmd.CommandText);
            CurrCMD.CommandType = cmd.CommandType;
            foreach (DbParameter param in cmd.Parameters)
                CurrCMD.Parameters.Add(param);

            return db.ExecuteScalar(CurrCMD);
        }

        public static DataTable SqlCommandOperation_DataTable(string pluginType, DbCommand cmd, LazyAtHome.Core.Enumerate.DataAccessPatterns dataAccess = LazyAtHome.Core.Enumerate.DataAccessPatterns.Query)
        {
            Database db = DBHelper.CreateDataBase(LazyAtHome.Core.Enumerate.ApplicationModule.WorkQueue, dataAccess);
            //DbCommand CurrCMD = db.GetSqlStringCommand(cmd.CommandText);
            //CurrCMD.CommandType = cmd.CommandType;
            //foreach (DbParameter param in cmd.Parameters)
            //    CurrCMD.Parameters.Add(param);

            //DbCommand cmd = db.GetSqlStringCommand(strSql);
            cmd.Connection = db.CreateConnection();

            DbDataAdapter da = db.GetDataAdapter();
            da.SelectCommand = cmd;

            DataTable rtnList = new DataTable();
            da.Fill(rtnList);
            cmd.Dispose();
            da.Dispose();
            return rtnList;
        }

        public static bool SqlCommandOperation_Transaction(string pluginType, IList arrSqlList, LazyAtHome.Core.Enumerate.DataAccessPatterns dataAccess = LazyAtHome.Core.Enumerate.DataAccessPatterns.Query)
        {
            Database db = DBHelper.CreateDataBase(LazyAtHome.Core.Enumerate.ApplicationModule.WorkQueue, dataAccess);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();

                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (string strSql in arrSqlList)
                    {
                        db.ExecuteNonQuery(transaction, CommandType.Text, strSql);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }

                connection.Close();
            }

            return true;
        }
    }
}
