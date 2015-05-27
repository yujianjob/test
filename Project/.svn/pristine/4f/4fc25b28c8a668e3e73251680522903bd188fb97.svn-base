using LazyAtHome.WCF.Common.Contract.DataContract.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace LazyAtHome.WCF.Common.DAL
{
    public class FileDAL :DALBase, LazyAtHome.WCF.Common.Interface.IDAL.IFileDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FileDAL()
        {
            APPModule = Core.Enumerate.ApplicationModule.Common;
        }

        /// <summary>
        /// 获取上传文件的配置
        /// </summary>
        /// <param name="iProjectType"></param>
        /// <returns></returns>
        public string File_GetModuleConfig(int iProjectType)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT SaveFilePath FROM File_UpLoadConfig ");
            sql.Append(" WHERE ProjectType = @ProjectType");
            sql.Append(" AND Obj_Status = @Obj_Status");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            db.AddInParameter(cmd, "@ProjectType", DbType.Int32, iProjectType);
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);

            var reader = db.ExecuteReader(cmd);
            if (reader == null)
            {
                return null;
            }
            else
            {
                try
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
                catch
                {

                    throw;
                }
                finally
                {
                    reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="iUpLoadFileLogDC"></param>
        /// <returns></returns>
        public void File_AddUpLoadLog(UpLoadLogDC iUpLoadFileLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO File_UpLoadLog(ProjectType, FileSignKey, FileCustomKey, SaveFilePath, FileMD5,");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(" ) VALUES (@ProjectType, @FileSignKey, @FileCustomKey, @SaveFilePath, @FileMD5,");
            //值拼接
            AddCommonInsertValues(sql);

            sql.Append(")");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ProjectType", DbType.Int32, iUpLoadFileLogDC.ProjectType);

            db.AddInParameter(cmd, "@FileSignKey", DbType.Guid, iUpLoadFileLogDC.FileSignKey);

            db.AddInParameter(cmd, "@FileCustomKey", DbType.String, iUpLoadFileLogDC.FileCustomKey);

            db.AddInParameter(cmd, "@SaveFilePath", DbType.String, iUpLoadFileLogDC.SaveFilePath);

            db.AddInParameter(cmd, "@FileMD5", DbType.String, iUpLoadFileLogDC.FileMD5);

            AddCommonInsertParameter(db, cmd, iUpLoadFileLogDC);

            db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 根据SignKey查询
        /// </summary>
        /// <param name="iSignKey"></param>
        /// <returns></returns>
        public UpLoadLogDC File_UpLoadLog_GetBySignKey(Guid iSignKey)
        {
            UpLoadLogDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM File_UpLoadLog WHERE FileSignKey = @FileSignKey AND Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@FileSignKey", DbType.Guid, iSignKey);
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = UpLoadLogDC.GetEntity(reader);
                }
            }
            return entity;
        }
    }
}
