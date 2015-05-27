using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LazyAtHome.Core.Infrastructure.WCF
{
    public abstract class DALBase
    {
        protected Core.Enumerate.ApplicationModule APPModule = Enumerate.ApplicationModule.None;

        #region 辅助增删改方法

        protected bool Exists(Database db, Action<StringBuilder> sbSelectAction, Action<DbCommand> cmdAction)
        {
            StringBuilder sb = new StringBuilder();
            sbSelectAction(sb);

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            cmdAction(cmd);

            return (int)db.ExecuteScalar(cmd) > 0;
        }

        protected void Select(Database db, Action<StringBuilder> sbSelectAction, Action<DbCommand> cmdAction, Action<IDataReader> readerAction)
        {
            StringBuilder sb = new StringBuilder();
            sbSelectAction(sb);

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            cmdAction(cmd);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                readerAction(reader);
            }
        }

        protected bool Insert(Database db, Action<StringBuilder> sbInsertAction, Action<DbCommand> cmdAction)
        {
            return Insert(db, sbInsertAction, cmdAction, false) > 0;
        }

        protected int Insert(Database db, Action<StringBuilder> sbInsertAction, Action<DbCommand> cmdAction, bool needReturnId)
        {
            StringBuilder sb = new StringBuilder();

            sbInsertAction(sb);


            if (needReturnId)
            {
                sb.Append(";Select @@IDENTITY");
            }

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            cmdAction(cmd);


            if (needReturnId)
            {
                return (int)db.ExecuteScalar(cmd);
            }
            return db.ExecuteNonQuery(cmd);
        }

        protected bool Update(Database db, Action<StringBuilder> sbUpdateAction, Action<DbCommand> cmdAction)
        {
            StringBuilder sb = new StringBuilder();


            sbUpdateAction(sb);


            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            cmdAction(cmd);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        protected bool Delete(Database db, Action<StringBuilder> sbDeleteAction, Action<DbCommand> cmdAction)
        {
            StringBuilder sb = new StringBuilder();

            sbDeleteAction(sb);

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            cmdAction(cmd);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #endregion

        #region 公共部分参数处理方法

        /// <summary>
        /// 添加插入的公共部分的SQL
        /// </summary>
        /// <param name="sb"></param>
        protected void AddCommonInsertSql(StringBuilder sb)
        {
            sb.Append(@"[Obj_Remark],
                        [Obj_Status],
                        [Obj_Cuser],
                        [Obj_Muser]");
        }

        /// <summary>
        /// 添加插入的公共部分的SQL(Values)
        /// </summary>
        /// <param name="sb"></param>
        protected void AddCommonInsertValues(StringBuilder sb)
        {
            sb.Append(@"@Obj_Remark,
                        @Obj_Status,
                        @Obj_Cuser,
                        @Obj_Muser");
        }

        /// <summary>
        /// 添加插入的公共部分的参数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cmd"></param>
        /// <param name="model"></param>
        protected void AddCommonInsertParameter(Database db, DbCommand cmd, EntityBase model)
        {
            db.AddInParameter(cmd, "@Obj_Remark", DbType.String, model.Obj_Remark);
            db.AddInParameter(cmd, "@Obj_Status", DbType.Int32, model.Obj_Status);
            //db.AddInParameter(cmd, "@Obj_Cdate", DbType.DateTime, model.Obj_Cdate);
            db.AddInParameter(cmd, "@Obj_Cuser", DbType.Int32, model.Obj_Cuser);
            //db.AddInParameter(cmd, "@Obj_Mdate", DbType.DateTime, model.Obj_Mdate);
            db.AddInParameter(cmd, "@Obj_Muser", DbType.Int32, model.Obj_Muser);
        }

        /// <summary>
        /// 添加更新的公共部分的SQL
        /// </summary>
        /// <param name="sb"></param>
        protected void AddCommonUpdateSql(StringBuilder sb)
        {
            sb.Append("Obj_Remark = @Obj_Remark,");
            sb.Append("Obj_Status = @Obj_Status,");
            sb.Append("Obj_Mdate = Getdate(),");
            sb.Append("Obj_Muser = @Obj_Muser");
        }

        /// <summary>
        /// 添加更新的公共部分的参数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cmd"></param>
        /// <param name="model"></param>
        protected void AddCommonUpdateParameter(Database db, DbCommand cmd, EntityBase model)
        {
            db.AddInParameter(cmd, "@Obj_Remark", DbType.String, model.Obj_Remark);
            db.AddInParameter(cmd, "@Obj_Status", DbType.Int32, model.Obj_Status);
            //db.AddInParameter(cmd, "@Obj_Mdate", DbType.DateTime, model.Obj_Mdate);
            db.AddInParameter(cmd, "@Obj_Muser", DbType.Int32, model.Obj_Muser);
        }

        protected bool Update(Database db, string tableName, IList<UpdateParamEntity> param)
        {
            //如果无更新内容，不需要执行更新方法
            if (param.Count < 2)
                return false;

            StringBuilder sb = new StringBuilder();
            string PrimeKey = null;
            bool isHasMdate = false;
            sb.AppendFormat("UPDATE {0} SET ", tableName);
            foreach (var item in param)
            {
                if (item.IsPrimaryKey)
                {
                    PrimeKey = item.Name;
                    continue;
                }
                switch (item.Name)
                {
                    case "Obj_Cdate":
                    case "Obj_Mdate":
                        isHasMdate = true;
                        item.NewValue = DateTime.Now;//赋值系统时间
                        break;
                }
                sb.AppendFormat("{0} = @{0},", item.Name);
            }
            //没有赋值系统时间，自动赋值服务器数据库时间
            if (!isHasMdate)
                sb.Append("Obj_Mdate = getdate(),");

            if (string.IsNullOrEmpty(PrimeKey))
                return false;

            RemoveLastDot(sb);
            sb.AppendFormat(" where {0} = @{0}", PrimeKey);
            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            foreach (var item in param)
            {
                //switch (item.Name)
                //{
                //    case "Obj_Cdate":                        
                //    case "Obj_Mdate":
                //        item.NewValue = DateTime.Now;//赋值系统时间
                //        break;
                //}
                db.AddInParameter(cmd, "@" + item.Name, item.DbType, item.NewValue);
            }

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #endregion

        #region 辅助方法

        protected void RemoveLastDot(StringBuilder sb)
        {
            while (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == ' ')
            {
                sb.Length -= 1;
            }
        }

        protected string GetPropertyName(LambdaExpression property)
        {
            return property.Body.ToString().Split('.').Last();
        }

        protected DBMetadata GetModelMetadata(Type type)
        {
            var modelMetadata = new DBMetadata();
            var attr1 = type.GetCustomAttributes(typeof(DBAttribute), true);
            if (attr1.Length == 1)
            {
                var dbColumnAttr = attr1[0] as DBAttribute;
                modelMetadata.TableName = dbColumnAttr.TableName;
            }
            PropertyInfo[] properties = type.GetProperties();
            foreach (var item in properties)
            {
                var attr2 = item.GetCustomAttributes(typeof(DBAttribute), true);
                if (attr2.Length == 1)
                {
                    var dbColumnAttr = attr2[0] as DBAttribute;
                    if (dbColumnAttr != null)
                    {
                        modelMetadata.Add(dbColumnAttr.ColumnName, new ColumnMetadata()
                        {
                            DbType = dbColumnAttr.ColumnType,
                            IsPrimeKey = dbColumnAttr.IsPrimeKey,
                            IsIdentity = dbColumnAttr.IsIdentity,
                            IsCommon = dbColumnAttr.IsCommon,
                            Property = item
                        });
                    }
                }
            }
            return modelMetadata;
        }

        ///// <summary>
        ///// 数据分页查询
        ///// </summary>
        ///// <param name="iTables">表名称,视图(支持多张,不支持重命名)</param>
        ///// <param name="iPrimaryKey">主键</param>
        ///// <param name="iFields">抽出字段</param>
        ///// <param name="iFilter">过滤语句，不带Where</param>
        ///// <param name="iSort">排序语句，不带Order By 比如：NewsID Desc,OrderRows Asc</param>
        ///// <param name="iGroup">Group语句,不带Group By</param>
        ///// <param name="iCurrentIndex">当前下标</param>
        ///// <param name="iPageSize">分页尺寸</param>
        ///// <param name="iModule">项目</param>
        ///// <returns></returns>
        //protected IDataReader ExecuteReaderPagination(string iTables, string iPrimaryKey, string iFields, string iFilter, string iSort, string iGroup, int iCurrentIndex, int iPageSize, ApplicationModule iModule, DataAccessPatterns iDataAccess = DataAccessPatterns.Query)
        //{
        //    //sql注入检查
        //    if (string.IsNullOrEmpty(iTables))
        //        return null;
        //    if (string.IsNullOrEmpty(iPrimaryKey))
        //        return null;
        //    if (!DBHelper.CheckParmsForTables(iTables))
        //        return null;
        //    if (!DBHelper.CheckParms(iPrimaryKey + " " + (string.IsNullOrEmpty(iFields) ? string.Empty : iFields) + " " + (string.IsNullOrEmpty(iFilter) ? string.Empty : iFilter) + " " + (string.IsNullOrEmpty(iSort) ? string.Empty : iSort) + " " + (string.IsNullOrEmpty(iGroup) ? string.Empty : iGroup)))
        //        return null;

        //    Database db = DBHelper.CreateDataBase(iModule, iDataAccess);
        //    DbCommand cmd = db.GetStoredProcCommand("SP_Pagination");

        //    db.AddInParameter(cmd, "@Tables", DbType.String, iTables);
        //    db.AddInParameter(cmd, "@PrimaryKey", DbType.String, iPrimaryKey);
        //    db.AddInParameter(cmd, "@Fields", DbType.String, iFields);
        //    if (!string.IsNullOrEmpty(iFilter))
        //        db.AddInParameter(cmd, "@Filter", DbType.String, iFilter);
        //    if (!string.IsNullOrEmpty(iSort))
        //        db.AddInParameter(cmd, "@Sort", DbType.String, iSort);
        //    if (!string.IsNullOrEmpty(iGroup))
        //        db.AddInParameter(cmd, "@Group", DbType.String, iGroup);
        //    db.AddInParameter(cmd, "@CurrentIndex", DbType.Int32, iCurrentIndex);
        //    db.AddInParameter(cmd, "@PageSize", DbType.Int32, iPageSize);
        //    return db.ExecuteReader(cmd);
        //}

        /// <summary>
        /// 数据分页查询
        /// </summary>
        /// <param name="iTables">表名称,视图(支持多张,不支持重命名)</param>
        /// <param name="iPrimaryKey">主键</param>
        /// <param name="iFields">抽出字段</param>
        /// <param name="iFilter">过滤语句，不带Where</param>
        /// <param name="iSort">排序语句，不带Order By 比如：NewsID Desc,OrderRows Asc</param>
        /// <param name="iGroup">Group语句,不带Group By</param>
        /// <param name="iCurrentPage">当前页码</param>
        /// <param name="iPageSize">分页尺寸</param>
        /// <param name="iModule">项目</param>
        /// <returns></returns>
        protected IDataReader ExecuteReaderPaginationByPageIndex(string iTables, string iPrimaryKey, string iFields, string iFilter, string iSort, string iGroup, int iCurrentPage, int iPageSize, ApplicationModule iModule, DataAccessPatterns iDataAccess = DataAccessPatterns.Query)
        {
            //sql注入检查
            if (string.IsNullOrEmpty(iTables))
                return null;
            if (string.IsNullOrEmpty(iPrimaryKey))
                return null;
            if (!DBHelper.CheckParmsForTables(iTables))
                return null;
            if (!DBHelper.CheckParms(iPrimaryKey + " " + (string.IsNullOrEmpty(iFields) ? string.Empty : iFields) + " " + (string.IsNullOrEmpty(iFilter) ? string.Empty : iFilter) + " " + (string.IsNullOrEmpty(iSort) ? string.Empty : iSort) + " " + (string.IsNullOrEmpty(iGroup) ? string.Empty : iGroup)))
                return null;

            if (string.IsNullOrWhiteSpace(iPrimaryKey))
            {
                throw new Exception("主键不能为空.");
            }

            if (string.IsNullOrWhiteSpace(iFields))
            {
                iFields = "*";
            }

            if (!string.IsNullOrWhiteSpace(iFilter))
            {
                iFilter = " WHERE " + iFilter.Trim();
            }

            if (!string.IsNullOrWhiteSpace(iSort))
            {
                //主键不在orderby中,加入主键
                if (iSort.IndexOf(iPrimaryKey) < 0)
                {
                    iSort = " Order By " + iSort.Trim() + "," + iPrimaryKey.Trim();
                }
                else
                {
                    iSort = " Order By " + iSort.Trim();
                }
            }
            else
            {
                iSort = " Order By " + iPrimaryKey.Trim();
            }

            if (!string.IsNullOrWhiteSpace(iGroup))
            {
                if (iFields == "*")
                {
                    throw new Exception("GroupBy语句中不允许查询字段为空.");
                }
                iGroup = " Group By " + iGroup.Trim();
            }

            Database db = DBHelper.CreateDataBase(iModule, iDataAccess);
            StringBuilder sql = new StringBuilder();

            int startIndex = (iCurrentPage - 1) * iPageSize + 1;
            int endIndex = iCurrentPage * iPageSize;
            sql.Append(@"SELECT * FROM ( ");
            sql.Append(@" SELECT " + iFields);
            sql.Append(@" ,ROW_NUMBER() OVER (" + iSort + ") as ROWOA6DFN");
            sql.Append(@" FROM " + iTables);
            sql.Append(iFilter);
            sql.Append(iGroup);
            sql.Append(") as TempB4D6KU");
            sql.Append(" WHERE ROWOA6DFN BETWEEN " + startIndex + " AND " + endIndex);

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            return db.ExecuteReader(cmd);
        }


        ///// <summary>
        ///// 数据分页查询
        ///// </summary>
        ///// <param name="iTables">表名称,视图(支持多张,不支持重命名)</param>
        ///// <param name="iPrimaryKey">主键</param>
        ///// <param name="iFields">抽出字段</param>
        ///// <param name="iFilter">过滤语句，不带Where</param>
        ///// <param name="iSort">排序语句，不带Order By 比如：NewsID Desc,OrderRows Asc</param>
        ///// <param name="iGroup">Group语句,不带Group By</param>
        ///// <param name="iCurrentIndex">当前下标</param>
        ///// <param name="iPageSize">分页尺寸</param>
        ///// <param name="iModule">项目</param>
        ///// <returns>错误:-1</returns>
        //protected int ExecuteReaderRecordCount(string iTables, string iPrimaryKey, string iFields, string iFilter, string iSort, string iGroup, int iCurrentIndex, int iPageSize, ApplicationModule iModule, DataAccessPatterns iDataAccess = DataAccessPatterns.Query)
        //{
        //    int rtn = -1;
        //    //sql注入检查
        //    if (string.IsNullOrEmpty(iTables))
        //        return rtn;
        //    if (string.IsNullOrEmpty(iPrimaryKey))
        //        return rtn;
        //    if (!DBHelper.CheckParmsForTables(iTables))
        //        return rtn;
        //    if (!DBHelper.CheckParms(iPrimaryKey + " " + (string.IsNullOrEmpty(iFields) ? string.Empty : iFields) + " " + (string.IsNullOrEmpty(iFilter) ? string.Empty : iFilter) + " " + (string.IsNullOrEmpty(iSort) ? string.Empty : iSort) + " " + (string.IsNullOrEmpty(iGroup) ? string.Empty : iGroup)))
        //        return rtn;

        //    Database db = DBHelper.CreateDataBase(iModule, iDataAccess);
        //    StringBuilder sql = new StringBuilder();
        //    if (string.IsNullOrEmpty(iGroup))
        //        sql.Append("SELECT COUNT(0) FROM " + iTables);
        //    else
        //        sql.Append("SELECT COUNT(0) FROM (SELECT " + iFields + " FROM " + iTables);
        //    if (!string.IsNullOrEmpty(iFilter))
        //        sql.Append(" WHERE " + iFilter);
        //    if (!string.IsNullOrEmpty(iGroup))
        //    {
        //        sql.Append(" GROUP BY " + iGroup);
        //        sql.Append(") AS TMP");
        //    }

        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    using (IDataReader reader = db.ExecuteReader(cmd))
        //    {
        //        if (reader.Read())
        //            rtn = Convert.ToInt32(reader[0]);
        //    }
        //    return rtn;
        //}

        /// <summary>
        /// 数据分页查询
        /// </summary>
        /// <param name="iTables">表名称,视图(支持多张,不支持重命名)</param>
        /// <param name="iPrimaryKey">主键</param>
        /// <param name="iFields">抽出字段</param>
        /// <param name="iFilter">过滤语句，不带Where</param>
        /// <param name="iSort">排序语句，不带Order By 比如：NewsID Desc,OrderRows Asc</param>
        /// <param name="iGroup">Group语句,不带Group By</param>
        /// <param name="iCurrentPage">当前页码</param>
        /// <param name="iPageSize">分页尺寸</param>
        /// <param name="iModule">项目</param>
        /// <returns>错误:-1</returns>
        protected int ExecuteReaderRecordCountByPageIndex(string iTables, string iPrimaryKey, string iFields, string iFilter, string iSort, string iGroup, int iCurrentPage, int iPageSize, ApplicationModule iModule, DataAccessPatterns iDataAccess = DataAccessPatterns.Query)
        {
            int rtn = -1;
            //sql注入检查
            if (string.IsNullOrEmpty(iTables))
                return rtn;
            if (string.IsNullOrEmpty(iPrimaryKey))
                return rtn;
            if (!DBHelper.CheckParmsForTables(iTables))
                return rtn;
            if (!DBHelper.CheckParms(iPrimaryKey + " " + (string.IsNullOrEmpty(iFields) ? string.Empty : iFields) + " " + (string.IsNullOrEmpty(iFilter) ? string.Empty : iFilter) + " " + (string.IsNullOrEmpty(iSort) ? string.Empty : iSort) + " " + (string.IsNullOrEmpty(iGroup) ? string.Empty : iGroup)))
                return rtn;

            Database db = DBHelper.CreateDataBase(iModule, iDataAccess);
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(iGroup))
                sql.Append("SELECT COUNT(0) FROM " + iTables);
            else
                sql.Append("SELECT COUNT(0) FROM (SELECT " + iFields + " FROM " + iTables);
            if (!string.IsNullOrEmpty(iFilter))
                sql.Append(" WHERE " + iFilter);
            if (!string.IsNullOrEmpty(iGroup))
            {
                sql.Append(" GROUP BY " + iGroup);
                sql.Append(") AS TMP");
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                    rtn = Convert.ToInt32(reader[0]);
            }
            return rtn;
        }

        #endregion
    }
}
