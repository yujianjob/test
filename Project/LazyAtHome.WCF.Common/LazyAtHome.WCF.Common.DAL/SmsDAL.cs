using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Sms;
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
    public class SmsDAL : DALBase, LazyAtHome.WCF.Common.Interface.IDAL.ISmsDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SmsDAL()
        {
            APPModule = Core.Enumerate.ApplicationModule.Common;
        }

        /// <summary>
        /// 获取短信列表
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Base_SmsSendDC> Base_SmsSend_SELECT_List(
              string iMPNo, DateTime iStartDate, DateTime iEndDate,
              int iPageIndex, int iPageSize)
        {
            List<Base_SmsSendDC> list = new List<Base_SmsSendDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@"Obj_Status = 1 ");
            sqlorder.Append(@" ID DESC ");

            //用户手机
            if (!string.IsNullOrWhiteSpace(iMPNo))
                sqlwhere.Append("AND Base_SmsSend.Mobile = '" + iMPNo + "' ");

            //创建时间开始
            sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Base_SmsSendDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Base_SmsSend", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Base_SmsSend", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Base_SmsSendDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iContent"></param>
        /// <param name="iSendTime"></param>
        /// <returns></returns>
        public bool Base_SmsSend_Create(Base_SmsSendDC iBase_SmsSendDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Base_SmsSend(Mobile, Content, RunTime, Priority, Type, Channel, Source, SourceValue, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Mobile, @Content,  @RunTime, @Priority, @Type, @Channel, @Source, @SourceValue, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //手机号
            db.AddInParameter(cmd, "@Mobile", DbType.String, iBase_SmsSendDC.Mobile);
            //短信内容
            db.AddInParameter(cmd, "@Content", DbType.String, iBase_SmsSendDC.Content);
            //定时发送时间
            db.AddInParameter(cmd, "@RunTime", DbType.DateTime, iBase_SmsSendDC.RunTime);

            db.AddInParameter(cmd, "@Priority", DbType.Int32, iBase_SmsSendDC.Priority);
            db.AddInParameter(cmd, "@Type", DbType.Int32, iBase_SmsSendDC.Type);
            db.AddInParameter(cmd, "@Channel", DbType.Int32, iBase_SmsSendDC.Channel);
            db.AddInParameter(cmd, "@Source", DbType.Int32, iBase_SmsSendDC.Source);
            db.AddInParameter(cmd, "@SourceValue", DbType.String, iBase_SmsSendDC.SourceValue);

            //添加共通参数
            AddCommonInsertParameter(db, cmd, iBase_SmsSendDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #region Base_Push

        /// <summary>
        /// 新增 Base_Push
        /// </summary>
        /// <param name="iBase_PushDC"></param>
        /// <returns></returns>
        public int Base_Push_ADD(Base_PushDC iBase_PushDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Base_Push(Title, Content, SendStatus, SendTime, RunTime, Type, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Title, @Content, @SendStatus, @SendTime, @RunTime, @Type, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iBase_PushDC.Title);
            //内容
            db.AddInParameter(cmd, "@Content", DbType.String, iBase_PushDC.Content);
            //发送状态
            db.AddInParameter(cmd, "@SendStatus", DbType.Int32, iBase_PushDC.SendStatus);
            //发送时间
            db.AddInParameter(cmd, "@SendTime", DbType.DateTime, iBase_PushDC.SendTime);
            //服务轮询到时间
            db.AddInParameter(cmd, "@RunTime", DbType.DateTime, iBase_PushDC.RunTime);
            //推送类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iBase_PushDC.Type);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iBase_PushDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Base_Push
        /// </summary>
        /// <param name="iBase_PushDC"></param>
        /// <returns></returns>
        public bool Base_Push_UPDATE(Base_PushDC iBase_PushDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Push SET Title = @Title, Content = @Content, SendStatus = @SendStatus, SendTime = @SendTime, RunTime = @RunTime, Type = @Type, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iBase_PushDC.ID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iBase_PushDC.Title);
            //内容
            db.AddInParameter(cmd, "@Content", DbType.String, iBase_PushDC.Content);
            //发送状态
            db.AddInParameter(cmd, "@SendStatus", DbType.Int32, iBase_PushDC.SendStatus);
            //发送时间
            db.AddInParameter(cmd, "@SendTime", DbType.DateTime, iBase_PushDC.SendTime);
            //服务轮询到时间
            db.AddInParameter(cmd, "@RunTime", DbType.DateTime, iBase_PushDC.RunTime);
            //推送类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iBase_PushDC.Type);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iBase_PushDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Base_Push
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Base_Push_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Push SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询Base_Push
        /// </summary>
        /// <returns></returns>
        public IList<Base_PushDC> Base_Push_SELECT_List()
        {
            List<Base_PushDC> list = new List<Base_PushDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT TOP 200 * FROM Base_Push WHERE SendStatus = 0 AND RunTime <= GETDATE() AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Base_PushDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 更新发送状态
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iSendStatus"></param>
        /// <returns></returns>
        public bool Base_Push_UPDATE(int iID, int iSendStatus)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Base_Push SET SendStatus = @SendStatus, SendTime = getdate() WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //状态
            db.AddInParameter(cmd, "@SendStatus", DbType.Int32, iSendStatus);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 根据ID查询Base_Push
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Base_PushDC Base_Push_SELECT_Entity(int iID)
        {
            Base_PushDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Base_Push WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Base_PushDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion
    }
}
