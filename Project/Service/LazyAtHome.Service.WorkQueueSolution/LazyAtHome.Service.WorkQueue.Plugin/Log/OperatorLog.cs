using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using LazyAtHome.Core.Infrastructure.WorkQueue;
using LazyAtHome.Service.WorkQueue.Contract.DataContract.Log;

namespace LazyAtHome.Service.WorkQueue.Plugin.Log
{
    public class OperatorLog : PluginBase
    {
        public OperatorLog()
        {
            TypeName = "Log_Operator";
        }

        public override void Execute(IQueueItem item)
        {
            base.Execute(item);
            OperatorLogItem logitem = (OperatorLogItem)item;
            InsertData(logitem);
        }


        private const string PARAM_OperatorLog_OperatorID = "@OperatorID";
        private const string PARAM_OperatorLog_OperatorName = "@OperatorName";
        private const string PARAM_OperatorLog_Type = "@Type";
        private const string PARAM_OperatorLog_LogContent = "@LogContent";

        private void InsertData(OperatorLogItem logitem)
        {
            string strSql = "Insert Into PR_OperatorLog(OperatorID, OperatorName, Type, LogContent) VALUES (@OperatorID, @OperatorName, @Type, @LogContent)";

            DbCommand cmd = new SqlCommand();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;

            SqlParameter[] paras = {
                        new SqlParameter(PARAM_OperatorLog_OperatorID, SqlDbType.Int),
                        new SqlParameter(PARAM_OperatorLog_OperatorName, SqlDbType.NVarChar, 20),
                        new SqlParameter(PARAM_OperatorLog_Type, SqlDbType.Int),
                        new SqlParameter(PARAM_OperatorLog_LogContent, SqlDbType.NVarChar, 1000)};

            paras[0].Value = logitem.OperatorID;
            paras[1].Value = logitem.OperatorName;
            paras[2].Value = logitem.Type;
            paras[3].Value = logitem.LogContent;

            cmd.Parameters.AddRange(paras);

            _SqlCommandOperation_NonQuery(TypeName, cmd);
        }
    }
}
