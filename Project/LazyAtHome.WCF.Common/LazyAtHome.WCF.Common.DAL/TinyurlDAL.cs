using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.DAL
{
    public class TinyurlDAL : DALBase, LazyAtHome.WCF.Common.Interface.IDAL.ITinyurlDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TinyurlDAL()
        {
            APPModule = Core.Enumerate.ApplicationModule.Common;
        }

        public string Tinyurl_CreateNew(string iUrl)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Tinyurl_CreateCode");

            db.AddInParameter(cmd, "@Url", DbType.String, iUrl);

            db.AddOutParameter(cmd, "@Code", DbType.String, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return db.GetParameterValue(cmd, "@Code").ToString();
        }

        public string Tinyurl_Get(string iCode)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT Url FROM Base_Tinyurl WHERE Code = @Code");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Code", DbType.String, iCode);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return string.Empty;
        }
    }
}
