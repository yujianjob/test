using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Service.SMS.DataContract;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.SMS
{
    public class DAL : DALBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DAL()
        {
            APPModule = ApplicationModule.Common;
        }

        const string SQL_SELECT_Base_SmsSend =
            @"SELECT TOP 500 
                Base_SmsSend.ID
                ,Base_SmsSend.Mobile
                ,Base_SmsSend.Content
                ,Base_SmsSend.Type
                ,Base_SmsSend.Channel
                ,ISNULL(Base_SmsSend_SendCount.Count,0) AS Count
            FROM Base_SmsSend LEFT JOIN Base_SmsSend_SendCount
            ON Base_SmsSend.Mobile = Base_SmsSend_SendCount.Mobile AND Base_SmsSend_SendCount.SendTime = CONVERT(CHAR(10),GETDATE(),120)
            WHERE SendStatus = 0 AND RunTime <= GETDATE() AND Obj_Status = 1";

        /// <summary>
        /// 查询短信列表
        /// </summary>
        /// <returns></returns>
        public IList<Base_SmsSendDC> Base_SmsSend_SELECT_List()
        {
            List<Base_SmsSendDC> list = new List<Base_SmsSendDC>();
            try
            {
                Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

                DbCommand cmd = db.GetSqlStringCommand(SQL_SELECT_Base_SmsSend);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        list.Add(Base_SmsSendDC.GetEntity(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Log_Fatal(ex);
            }

            return list;
        }

        /// <summary>
        /// 更新发送状态
        /// </summary>
        /// <param name="iID">ID</param>
        /// <param name="iSendStatus">状态</param>
        /// <returns></returns>
        public bool Base_SmsSend_UPDATE(int iID, string iMobile, int iSendStatus, int iCount)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            //发送成功 
            if (iSendStatus > 0)
            {
                    sql.Append(@"
UPDATE Base_SmsSend SET SendStatus = @SendStatus, SendTime = getdate() WHERE ID = @ID
IF((SELECT COUNT(0) FROM Base_SmsSend_SendCount WHERE Mobile = @Mobile AND SendTime = CONVERT(CHAR(10),GETDATE(),120)) = 0)
BEGIN
    INSERT INTO Base_SmsSend_SendCount(Mobile,SendTime,Count) VALUES (@Mobile,@SendTime,1)
END
ELSE
BEGIN
    UPDATE Base_SmsSend_SendCount SET Count = Count + 1 WHERE Mobile = @Mobile AND SendTime = @SendTime
END
");
            }
            else
            {
                sql.Append("UPDATE Base_SmsSend SET ReTry = ReTry + 1 WHERE ID = @ID");
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //状态
            db.AddInParameter(cmd, "@SendStatus", DbType.Int32, iSendStatus);

            db.AddInParameter(cmd, "@Mobile", DbType.String, iMobile);

            db.AddInParameter(cmd, "@SendTime", DbType.String, DateTime.Today.ToString("yyyy-MM-dd"));

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新重试超限
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool Base_SmsSend_UPDATE_ReTry(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Base_SmsSend SET SendStatus = -1001, ReTry = ReTry + 1 WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新数量超限
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool Base_SmsSend_UPDATE_Count(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Base_SmsSend SET SendStatus = -1002 WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        /// <summary>
        /// 更新黑名单
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool Base_SmsSend_UPDATE_Black(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Base_SmsSend SET SendStatus = -1003 WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HashSet<string> Base_SmsSend_BlackList()
        {
            var list = new HashSet<string>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Base_SmsSend_BlackList");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
            }
            return list;
        }
    }
}
