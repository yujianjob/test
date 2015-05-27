using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
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
    /// <summary>
    /// 
    /// </summary>
    public class PRDAL : DALBase, LazyAtHome.WCF.Common.Interface.IDAL.IPRDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PRDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        Random rnd = new Random(new object().GetHashCode());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOperatorDC"></param>
        /// <returns></returns>
        public int PR_Operator_ADD(OperatorDC iOperatorDC)
        {
            //iOperatorDC.Code = "E" + rnd.Next(0, 1000000).ToString().PadLeft(6, '0');
            //while (PR_Operator_Code_CheckExist(iOperatorDC.Code))
            //{
            //    iOperatorDC.Code = "E" + rnd.Next(0, 1000000).ToString().PadLeft(6, '0');
            //}

            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);



            sql.Append("INSERT INTO PR_Operator(LoginName, LoginPwd, Type, Name, MpNo, EMail, Enable, ParentID, Code, RoleID, NodeID, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@LoginName, @LoginPwd, @Type, @Name, @MpNo, @EMail, @Enable, @ParentID, @Code, @RoleID, @NodeID,  ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //登录名
            db.AddInParameter(cmd, "@LoginName", DbType.String, iOperatorDC.LoginName);
            //登录密码
            db.AddInParameter(cmd, "@LoginPwd", DbType.String, iOperatorDC.LoginPwd);
            //
            db.AddInParameter(cmd, "@Type", DbType.Int32, iOperatorDC.Type);
            //用户名
            db.AddInParameter(cmd, "@Name", DbType.String, iOperatorDC.Name);
            //用户手机
            db.AddInParameter(cmd, "@MpNo", DbType.String, iOperatorDC.MpNo);
            //用户邮箱
            db.AddInParameter(cmd, "@EMail", DbType.String, iOperatorDC.EMail);
            //是否启用（1启用0不启用）
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iOperatorDC.Enable);
            //上级ID
            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iOperatorDC.ParentID);
            //编号
            db.AddInParameter(cmd, "@Code", DbType.String, iOperatorDC.Code);
            //职位
            db.AddInParameter(cmd, "@RoleID", DbType.Int32, iOperatorDC.RoleID);
            //点位ID
            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iOperatorDC.NodeID);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOperatorDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            Exp_Preson_Account_ADD(id);

            PR_Operator_UPDATE_Code(id);

            return id;
        }

        private bool Exp_Preson_Account_ADD(int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("INSERT INTO Exp_Preson_Account(UserID,Payment) VALUES (@UserID,0)");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //用户名
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iOperatorID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        public bool PR_Operator_UPDATE_Code(int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE PR_Operator SET Code = @Code WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOperatorID);

            db.AddInParameter(cmd, "@Code", DbType.String, "E" + iOperatorID.ToString().PadLeft(4, '0'));

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        private bool PR_Operator_Code_CheckExist(string iCode)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(0) FROM PR_Operator WHERE Code = @Code ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Code", DbType.String, iCode);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iPR_OperatorDC"></param>
        /// <returns></returns>
        public bool PR_Operator_UPDATE(OperatorDC iOperatorDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE PR_Operator SET LoginName = @LoginName, Type = @Type, Name = @Name, MpNo = @MpNo, EMail = @EMail, Enable = @Enable, ParentID = @ParentID, RoleID = @RoleID, NodeID = @NodeID, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOperatorDC.ID);
            //登录名
            db.AddInParameter(cmd, "@LoginName", DbType.String, iOperatorDC.LoginName);
            ////登录密码
            //db.AddInParameter(cmd, "@LoginPwd", DbType.String, iPR_OperatorDC.LoginPwd);
            //
            db.AddInParameter(cmd, "@Type", DbType.Int32, iOperatorDC.Type);
            //用户名
            db.AddInParameter(cmd, "@Name", DbType.String, iOperatorDC.Name);
            //用户手机
            db.AddInParameter(cmd, "@MpNo", DbType.String, iOperatorDC.MpNo);
            //用户邮箱
            db.AddInParameter(cmd, "@EMail", DbType.String, iOperatorDC.EMail);
            //是否启用（1启用0不启用）
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iOperatorDC.Enable);
            //上级ID
            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iOperatorDC.ParentID);
            //职位
            db.AddInParameter(cmd, "@RoleID", DbType.Int32, iOperatorDC.RoleID);
            //点位ID
            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iOperatorDC.NodeID);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iOperatorDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 上岗下岗
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOnDuty"></param>
        /// <returns></returns>
        public bool PR_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
            INSERT INTO Exp_Operator_DutyLog(OperatorID,DutyStatus) VALUES(@ID,@OnDuty)
            UPDATE PR_Operator SET OnDuty = @OnDuty WHERE ID = @ID
                        ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOperatorID);
            //
            db.AddInParameter(cmd, "@OnDuty", DbType.Int32, iOnDuty);
            //添加共通参数
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iOperatorDC"></param>
        /// <returns></returns>
        public bool PR_Operator_UPDATE_Password(int iOperatorID, string iNewPassword, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE PR_Operator SET LoginPwd = @LoginPwd, Obj_Muser = @Obj_Muser, Obj_Mdate = GETDATE() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOperatorID);
            //登录名
            db.AddInParameter(cmd, "@LoginPwd", DbType.String, iNewPassword);
            //登录密码
            db.AddInParameter(cmd, "@Obj_Muser", DbType.Int32, iMuser);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public bool PR_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE PR_Operator SET LoginPwd = @NewLoginPwd, Obj_Mdate = GETDATE()
                 WHERE ID = @ID AND LoginPwd = @LoginPwd");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOperatorID);
            //登录密码
            db.AddInParameter(cmd, "@LoginPwd", DbType.String, iOldPassword);
            //登录密码
            db.AddInParameter(cmd, "@NewLoginPwd", DbType.String, iNewPassword);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public OperatorDC PR_Operator_SELECT_BYID(int iID)
        {
            OperatorDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT PR_Operator.*,Exp_Node.Name AS NodeName,Exp_Node.Type AS NodeType,Exp_Node.StorageID,Parent.Name AS ParentName
FROM PR_Operator LEFT JOIN Exp_Node ON PR_Operator.NodeID = Exp_Node.ID
LEFT JOIN PR_Operator Parent ON PR_Operator.ParentID = Parent.ID
WHERE PR_Operator.ID = @ID 
AND PR_Operator.Obj_Status = 1");
            //sql.Append("SELECT * FROM PR_Operator WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = OperatorDC.GetEntity(reader);
                }
            }
            return entity;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iNodeID"></param>
        /// <returns></returns>
        public OperatorDC PR_Operator_SELECT_BYNodeManagerID(int iNodeID)
        {
            OperatorDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT PR_Operator.*,Exp_Node.Name AS NodeName,Exp_Node.Type AS NodeType,Exp_Node.StorageID,Parent.Name AS ParentName
FROM PR_Operator 
INNER JOIN Exp_Node ON PR_Operator.ID = Exp_Node.ManagerID
LEFT JOIN PR_Operator Parent ON PR_Operator.ParentID = Parent.ID
WHERE Exp_Node.ID = @NodeID 
AND PR_Operator.Obj_Status = 1");
            //sql.Append("SELECT * FROM PR_Operator WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = OperatorDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据ID查询HRM人员信息表
        /// </summary>
        /// <param name="iLoginName">登录名</param>								
        /// <param name="iPassword">密码</param>
        /// <returns></returns>
        public OperatorDC PR_Operator_SELECT_Entity(string iLoginName, string iPassword, OperatorType iOperatorType)
        {
            OperatorDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PR_Operator.*,Exp_Node.Name AS NodeName,Exp_Node.Type AS NodeType,Exp_Node.StorageID
                        FROM PR_Operator LEFT JOIN Exp_Node ON PR_Operator.NodeID = Exp_Node.ID AND Exp_Node.Obj_Status = 1
                            WHERE LoginName = @LoginName
                            AND LoginPwd = @LoginPwd
                            AND (@Type = -1 OR PR_Operator.Type = @Type)
                            AND PR_Operator.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //登录名
            db.AddInParameter(cmd, "@LoginName", DbType.String, iLoginName);
            //密码
            db.AddInParameter(cmd, "@LoginPwd", DbType.String, iPassword);
            //
            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iOperatorType);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = OperatorDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iMpNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iEnableType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<OperatorDC> PR_Operator_SELECT_List(string iName, string iMpNo, string iEmail,
            int? iEnableType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize,
            int? iNodeID, string iKeyword)
        {
            List<OperatorDC> list = new List<OperatorDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();

            sqlfields.Append(" PR_Operator.*,Exp_Node.Name AS NodeName,Exp_Node.Type AS NodeType,Exp_Node.StorageID ");

            sqlorder.Append(@" PR_Operator.ID ");

            sqlwhere.Append(@" PR_Operator.Obj_Status = 1 ");


            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append("AND PR_Operator.Name LIKE '%" + iName + "%' ");
            //手机
            if (!string.IsNullOrEmpty(iMpNo))
                sqlwhere.Append("AND PR_Operator.MpNo = '" + iMpNo + "' ");
            //邮箱
            if (!string.IsNullOrEmpty(iEmail))
                sqlwhere.Append("AND PR_Operator.EMail = '" + iEmail + "' ");
            //状态
            if (iEnableType.HasValue)
                sqlwhere.Append("AND PR_Operator.Enable = '" + iEnableType + "' ");
            //注册时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND PR_Operator,Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //注册时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND PR_Operator.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            //状态
            if (iNodeID.HasValue)
                sqlwhere.Append("AND PR_Operator.NodeID = '" + iNodeID + "' ");

            if (!string.IsNullOrWhiteSpace(iKeyword))
                sqlwhere.Append("AND PR_Operator.MpNo = '" + iMpNo + "' ");

            var rtn = new RecordCountEntity<OperatorDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("PR_Operator LEFT JOIN Exp_Node ON PR_Operator.NodeID = Exp_Node.ID", "PR_Operator.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("PR_Operator LEFT JOIN Exp_Node ON PR_Operator.NodeID = Exp_Node.ID", "PR_Operator.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(OperatorDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OperatorDC> PR_Operator_SELECT_List_ALL()
        {
            List<OperatorDC> list = new List<OperatorDC>();
            OperatorDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM PR_Operator WHERE Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = OperatorDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 检查登录名是否重复
        /// </summary>
        /// <param name="iLoginName">登录名</param>
        /// <param name="iID">用户ID</param>
        /// <returns></returns>	
        public bool PR_Operator_HasLoginName(int iID, string iLoginName)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(0) FROM PR_Operator WHERE LoginName = @LoginName AND Obj_Status = 1 AND ID != @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //登录名
            db.AddInParameter(cmd, "@LoginName", DbType.String, iLoginName);
            //用户ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iPR_OperatorLogDC"></param>
        /// <returns></returns>
        public int PR_OperatorLog_ADD(OperatorLogDC iPR_OperatorLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO PR_OperatorLog(OperatorID, OperatorName, Type, LogContent, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OperatorID, @OperatorName, @Type, @LogContent, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //操作人
            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iPR_OperatorLogDC.OperatorID);
            //操作人名字
            db.AddInParameter(cmd, "@OperatorName", DbType.String, iPR_OperatorLogDC.OperatorName);
            //日志类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iPR_OperatorLogDC.Type);
            //日志内容
            db.AddInParameter(cmd, "@LogContent", DbType.String, iPR_OperatorLogDC.LogContent);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iPR_OperatorLogDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 查询日志列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<OperatorLogDC> PR_OperatorLog_SELECT_List(int? iType, string iOperatorName,
            string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            List<OperatorLogDC> list = new List<OperatorLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");
            sqlorder.Append(@" ID DESC ");

            //名称
            if (!string.IsNullOrEmpty(iOperatorName))
                sqlwhere.Append("AND OperatorName LIKE '%" + iOperatorName + "%' ");
            //内容
            if (!string.IsNullOrEmpty(iLogContent))
                sqlwhere.Append("AND LogContent LIKE '%" + iLogContent + "%' ");
            //类型
            if (iType.HasValue)
                sqlwhere.Append("AND Type = '" + iType + "' ");
            //时间开始
            sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<OperatorLogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("PR_OperatorLog", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("PR_OperatorLog", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(OperatorLogDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }


        //        /// <summary>
        //        /// 检查角色编号是否重复				
        //        /// </summary>								
        //        /// <param name="iID">角色ID</param>	
        //        /// <param name="iCode">角色编号</param>
        //        /// <returns></returns>	
        //        public bool VELOUnion_Role_HasRoleCode(int iID, string iCode)
        //        {
        //            bool rtn = true;
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append("SELECT COUNT(0) AS CNT FROM Common_Role WHERE COR_Code = @COR_Code AND Obj_Status = @Obj_Status AND COR_ID != @COR_ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色编号
        //            db.AddInParameter(cmd, "@COR_Code", DbType.String, iCode);
        //            //有效数据
        //            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_ID", DbType.Int32, iID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                if (reader.Read())
        //                {
        //                    if (Convert.ToInt32(reader["CNT"]) == 0)
        //                    {
        //                        rtn = false;
        //                    }
        //                }
        //            }
        //            return rtn;
        //        }

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="iRoleDC"></param>
        ///// <returns></returns>
        //public int VELOUnion_Role_ADD(RoleDC iRoleDC)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("INSERT INTO Common_Role(COR_Code, COR_Name, ");
        //    //字段拼接
        //    AddCommonInsertSql(sql);
        //    sql.Append(") VALUES (@COR_Code, @COR_Name, ");
        //    //值拼接
        //    AddCommonInsertValues(sql);
        //    sql.Append(");SELECT @@IDENTITY;");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //角色编号
        //    db.AddInParameter(cmd, "@COR_Code", DbType.String, iRoleDC.COR_Code);
        //    //角色名称
        //    db.AddInParameter(cmd, "@COR_Name", DbType.String, iRoleDC.COR_Name);
        //    //添加共通参数
        //    AddCommonInsertParameter(db, cmd, iRoleDC);
        //    int id = Convert.ToInt32(db.ExecuteScalar(cmd));
        //    return id;
        //}

        //        /// <summary>
        //        /// 根据ID查询角色表
        //        /// </summary>
        //        /// <param name="iPRR_ID">角色ID</param>
        //        /// <returns></returns>
        //        public RoleDC VELOUnion_Role_SELECT_BYCOR_ID(int iCOR_ID)
        //        {
        //            RoleDC entity = null;
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append("SELECT * FROM Common_Role WHERE COR_ID = @COR_ID AND Obj_Status = @Obj_Status");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_ID", DbType.Int32, iCOR_ID);
        //            //有效数据
        //            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                if (reader.Read())
        //                {
        //                    entity = RoleDC.GetEntity(reader);
        //                }
        //            }
        //            return entity;
        //        }

        //        /// <summary>
        //        /// 共通更新方法角色表
        //        /// </summary>
        //        /// <param name="iParam">待更新数据</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_Role_Update_Common(IList<UpdateParamEntity> iParam)
        //        {
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            return Update(db, "Common_Role", iParam);
        //        }

        //        /// <summary>
        //        /// 删除角色表
        //        /// </summary>
        //        /// <param name="iPRR_ID">角色ID</param>
        //        /// <param name="iMuser">操作人</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_Role_DELETE(int iCOR_ID, int iMuser)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("UPDATE Common_Role SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE COR_ID = @COR_ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_ID", DbType.Int32, iCOR_ID);
        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>								
        //        /// 通过员工ID获取角色关联人员列表								
        //        /// </summary>								
        //        /// <param name="iPersonnelID">员工ID</param>			
        //        /// <returns></returns>								
        //        public IList<RolePersonnelDC> VELOUnion_RolePersonnel_SELECT_List_ByPersonnelID(int iPersonnelID)
        //        {
        //            List<RolePersonnelDC> list = new List<RolePersonnelDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT a.* FROM Common_RolePersonnel a, Common_Role b WHERE a.COP_PersonnelID = @PersonnelID 
        //                            AND a.Obj_Status = 1 
        //                            AND a.COR_RoleID = b.COR_ID
        //                            AND b.Obj_Status = 1 ");
        //            sql.Append("ORDER BY a.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //员工ID
        //            db.AddInParameter(cmd, "@PersonnelID", DbType.Int32, iPersonnelID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(RolePersonnelDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 删除角色关联人员
        //        /// </summary>
        //        /// <param name="iPRRP_ID">ID</param>
        //        /// <param name="iMuser">操作人</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_RolePersonnel_DELETE(int iRoleID, int iPersonnelID, int iMuser)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("UPDATE Common_RolePersonnel SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE COR_RoleID = @COR_RoleID AND COP_PersonnelID = @COP_PersonnelID AND Obj_Status = '1'");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //ID
        //            db.AddInParameter(cmd, "@COR_RoleID", DbType.Int32, iRoleID);
        //            db.AddInParameter(cmd, "@COP_PersonnelID", DbType.Int32, iPersonnelID);
        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>
        //        /// 创建角色关联人员
        //        /// </summary>
        //        /// <param name="iRolePersonnelDC">角色关联人员</param>
        //        /// <returns></returns>
        //        public int VELOUnion_RolePersonnel_ADD(RolePersonnelDC iRolePersonnelDC)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("INSERT INTO Common_RolePersonnel(COR_RoleID, COP_PersonnelID, ");
        //            //字段拼接
        //            AddCommonInsertSql(sql);
        //            sql.Append(") VALUES (@COR_RoleID, @COP_PersonnelID, ");
        //            //值拼接
        //            AddCommonInsertValues(sql);
        //            sql.Append(");SELECT @@IDENTITY;");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_RoleID", DbType.Int32, iRolePersonnelDC.COR_RoleID);
        //            //人员ID
        //            db.AddInParameter(cmd, "@COP_PersonnelID", DbType.Int32, iRolePersonnelDC.COP_PersonnelID);
        //            //添加共通参数
        //            AddCommonInsertParameter(db, cmd, iRolePersonnelDC);
        //            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
        //            return id;
        //        }

        //        /// <summary>								
        //        /// 通过角色ID获取角色关联人员列表								
        //        /// </summary>								
        //        /// <param name="iRoleID">角色ID</param>			
        //        /// <returns></returns>								
        //        public IList<RolePersonnelDC> VELOUnion_RolePersonnel_SELECT_List_ByRoleID(int iRoleID)
        //        {
        //            List<RolePersonnelDC> list = new List<RolePersonnelDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT a.* FROM Common_RolePersonnel a, Common_Role b WHERE a.COR_RoleID = @COR_RoleID 
        //                            AND a.Obj_Status = 1 
        //                            AND a.COR_RoleID = b.COR_ID
        //                            AND b.Obj_Status = 1 ");
        //            sql.Append("ORDER BY a.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_RoleID", DbType.Int32, iRoleID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(RolePersonnelDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 通过角色ID获取角色关联功能列表								
        //        /// </summary>								
        //        /// <param name="iRoleID">角色ID</param>								
        //        /// <returns></returns>								
        //        public IList<RoleFunctionDC> VELOUnion_RoleFunction_SELECT_List_ByRoleID(int iRoleID)
        //        {
        //            List<RoleFunctionDC> list = new List<RoleFunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT a.* FROM Common_RoleFunction a, Common_Function b WHERE a.COR_RoleID = @COR_RoleID 
        //                            AND a.Obj_Status = 1 
        //                            AND a.COF_FunctionID = b.COF_ID
        //                            AND b.Obj_Status = 1 
        //                            ORDER BY a.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_RoleID", DbType.Int32, iRoleID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(RoleFunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 删除角色关联界面/功能
        //        /// </summary>
        //        /// <param name="iPRRF_ID">ID</param>
        //        /// <param name="iMuser">操作人</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_RoleFunction_DELETE(int iCORF_ID, int iMuser)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("UPDATE Common_RoleFunction SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE CORF_ID = @CORF_ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //ID
        //            db.AddInParameter(cmd, "@CORF_ID", DbType.Int32, iCORF_ID);
        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>
        //        /// 新增角色关联界面/功能
        //        /// </summary>
        //        /// <param name="iRoleFunctionDC">角色关联界面/功能</param>
        //        /// <returns></returns>
        //        public int VELOUnion_RoleFunction_ADD(RoleFunctionDC iRoleFunctionDC)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("INSERT INTO Common_RoleFunction(COR_RoleID, COF_FunctionID, ");
        //            //字段拼接
        //            AddCommonInsertSql(sql);
        //            sql.Append(") VALUES (@COR_RoleID, @COF_FunctionID, ");
        //            //值拼接
        //            AddCommonInsertValues(sql);
        //            sql.Append(");SELECT @@IDENTITY;");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_RoleID", DbType.Int32, iRoleFunctionDC.COR_RoleID);
        //            //关联ID
        //            db.AddInParameter(cmd, "@COF_FunctionID", DbType.Int32, iRoleFunctionDC.COF_FunctionID);
        //            //添加共通参数
        //            AddCommonInsertParameter(db, cmd, iRoleFunctionDC);
        //            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
        //            return id;
        //        }

        //        /// <summary>								
        //        /// 根据员工ID获取角色列表						
        //        /// </summary>								
        //        /// <param name="iPersonnelID">员工ID</param>								
        //        /// <returns></returns>								
        //        public IList<RoleDC> VELOUnion_Role_SELECT_List_ByPersonnelID(int iPersonnelID)
        //        {
        //            List<RoleDC> list = new List<RoleDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT b.* FROM Common_RolePersonnel a, Common_Role b WHERE a.COP_PersonnelID = @COP_PersonnelID 
        //                            AND a.Obj_Status = 1 
        //                            AND a.COR_RoleID = b.COR_ID 
        //                            AND b.Obj_Status = 1 
        //                            ORDER BY b.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //员工ID
        //            db.AddInParameter(cmd, "@COP_PersonnelID", DbType.Int32, iPersonnelID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(RoleDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 根据员工ID获取角色列表						
        //        /// </summary>								
        //        /// <param name="iPersonnelID">员工ID</param>								
        //        /// <returns></returns>								
        //        public IList<RoleDC> VELOUnion_Role_SELECT_List_ByPersonnelID(int iPersonnelID, int iEnable)
        //        {
        //            List<RoleDC> list = new List<RoleDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT b.* FROM Common_RolePersonnel a, Common_Role b WHERE a.COP_PersonnelID = @COP_PersonnelID 
        //                            AND a.Obj_Status = 1 
        //                            AND a.COR_RoleID = b.COR_ID 
        //                            AND b.COR_Enable = @COR_Enable
        //                            AND b.Obj_Status = 1 
        //                            ORDER BY b.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //员工ID
        //            db.AddInParameter(cmd, "@COP_PersonnelID", DbType.Int32, iPersonnelID);
        //            db.AddInParameter(cmd, "@COR_Enable", DbType.Int32, iEnable);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(RoleDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 通过角色ID获取角色关联人员列表								
        //        /// </summary>								
        //        /// <param name="iRoleID">角色ID</param>								
        //        /// <returns></returns>								
        //        public IList<LoginDC> VELOUnion_Login_SELECT_List_ByRoleID(int iRoleID)
        //        {
        //            List<LoginDC> list = new List<LoginDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT b.* FROM Common_RolePersonnel a, Common_Login b
        //                            WHERE a.COR_RoleID = @COR_RoleID 
        //                            AND b.COL_ID = a.COP_PersonnelID 
        //                            AND a.Obj_Status = 1 
        //                            ORDER BY b.COL_ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_RoleID", DbType.Int32, iRoleID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(LoginDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 根据角色ID查询角色关联功能权限列表						
        //        /// </summary>								
        //        /// <param name="iRoleID">角色ID</param>								
        //        /// <returns></returns>								
        //        public IList<FunctionDC> VELOUnion_Function_SELECT_List_ByRoleID(int iRoleID)
        //        {
        //            List<FunctionDC> list = new List<FunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT b.* FROM Common_RoleFunction a, Common_Function b WHERE a.COR_RoleID = @COR_RoleID 
        //                            AND a.Obj_Status = 1 
        //                            AND a.COF_FunctionID = b.COF_ID 
        //                            AND b.COF_Enable = 1
        //                            AND b.Obj_Status = 1 
        //                            ORDER BY b.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COR_RoleID", DbType.Int32, iRoleID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(FunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 检查权限编号是否重复				
        //        /// </summary>								
        //        /// <param name="iCOF_ID">权限ID</param>	
        //        /// <param name="iCOF_Code">权限编号</param>
        //        /// <returns></returns>	
        //        public bool VELOUnion_Function_HasFunctionCode(int iCOF_ID, string iCOF_Code)
        //        {
        //            bool rtn = true;
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append("SELECT COUNT(0) AS CNT FROM Common_Function WHERE COF_Code = @COF_Code AND Obj_Status = @Obj_Status AND COF_ID != @COF_ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //权限编号
        //            db.AddInParameter(cmd, "@COF_Code", DbType.String, iCOF_Code);
        //            //有效数据
        //            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
        //            //权限ID
        //            db.AddInParameter(cmd, "@COF_ID", DbType.Int32, iCOF_ID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                if (reader.Read())
        //                {
        //                    if (Convert.ToInt32(reader["CNT"]) == 0)
        //                    {
        //                        rtn = false;
        //                    }
        //                }
        //            }
        //            return rtn;
        //        }

        //        /// <summary>
        //        /// 新增菜单/界面/功能信息表
        //        /// </summary>
        //        /// <param name="iFunctionDC">菜单/界面/功能信息表</param>
        //        /// <returns></returns>
        //        public int VELOUnion_Function_ADD(FunctionDC iFunctionDC)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("INSERT INTO Common_Function(COF_ParentID, COF_Code, COF_Name, COF_Level, COF_DataType, COF_OperateType, COF_URL, COF_Enable, COF_OrderIndex, COF_IdentifierNo, ");
        //            //字段拼接
        //            AddCommonInsertSql(sql);
        //            sql.Append(") VALUES (@COF_ParentID, @COF_Code, @COF_Name, @COF_Level, @COF_DataType, @COF_OperateType, @COF_URL, @COF_Enable, @COF_OrderIndex, @COF_IdentifierNo, ");
        //            //值拼接
        //            AddCommonInsertValues(sql);
        //            sql.Append(");UPDATE Common_Function SET COF_IdentifierNo = CONVERT(VARCHAR, COF_ParentID) + '_' + CONVERT(VARCHAR, @@IDENTITY) WHERE COF_ID = @@IDENTITY;SELECT @@IDENTITY;");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //父节点ID
        //            db.AddInParameter(cmd, "@COF_ParentID", DbType.Int32, iFunctionDC.COF_ParentID);
        //            //编号
        //            db.AddInParameter(cmd, "@COF_Code", DbType.String, iFunctionDC.COF_Code);
        //            //名称
        //            db.AddInParameter(cmd, "@COF_Name", DbType.String, iFunctionDC.COF_Name);
        //            //界面等级
        //            db.AddInParameter(cmd, "@COF_Level", DbType.Int32, iFunctionDC.COF_Level);
        //            //数据类型（1项目 2菜单 3页面 4操作）
        //            db.AddInParameter(cmd, "@COF_DataType", DbType.Byte, iFunctionDC.COF_DataType);
        //            //操作类型(1添加 2编辑 3删除）
        //            db.AddInParameter(cmd, "@COF_OperateType", DbType.Byte, iFunctionDC.COF_OperateType);
        //            //URL
        //            db.AddInParameter(cmd, "@COF_URL", DbType.String, iFunctionDC.COF_URL);
        //            //状态（1启用0未启用）
        //            db.AddInParameter(cmd, "@COF_Enable", DbType.Int32, iFunctionDC.COF_Enable);
        //            //排序
        //            db.AddInParameter(cmd, "@COF_OrderIndex", DbType.Int32, iFunctionDC.COF_OrderIndex);
        //            //业务自动编号
        //            db.AddInParameter(cmd, "@COF_IdentifierNo", DbType.String, iFunctionDC.COF_IdentifierNo);
        //            //添加共通参数
        //            AddCommonInsertParameter(db, cmd, iFunctionDC);
        //            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
        //            return id;
        //        }

        //        /// <summary>
        //        /// 根据ID查询菜单/界面/功能信息表
        //        /// </summary>
        //        /// <param name="iCOF_ID">ID</param>
        //        /// <returns></returns>
        //        public FunctionDC VELOUnion_Function_SELECT_BYCOF_ID(int iCOF_ID)
        //        {
        //            FunctionDC entity = null;
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append("SELECT * FROM Common_Function WHERE COF_ID = @COF_ID AND Obj_Status = @Obj_Status");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //ID
        //            db.AddInParameter(cmd, "@COF_ID", DbType.Int32, iCOF_ID);
        //            //有效数据
        //            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                if (reader.Read())
        //                {
        //                    entity = FunctionDC.GetEntity(reader);
        //                }
        //            }
        //            return entity;
        //        }

        //        /// <summary>
        //        /// 共通更新方法菜单/界面/功能信息表
        //        /// </summary>
        //        /// <param name="iParam">待更新数据</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_Function_Update_Common(IList<UpdateParamEntity> iParam)
        //        {
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            return Update(db, "Common_Function", iParam);
        //        }

        //        /// <summary>								
        //        /// 根据角色ID查询角色关联功能权限列表						
        //        /// </summary>								
        //        /// <param name="iRoleID">角色ID</param>								
        //        /// <returns></returns>								
        //        public IList<FunctionDC> VELOUnion_Function_SELECT_List_ByCOF_ID(int iCOF_ID)
        //        {
        //            List<FunctionDC> list = new List<FunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"WITH pr
        //                         AS 
        //                         (
        //                          SELECT * FROM Common_Function WHERE COF_ID = @COF_ID
        //                          UNION ALL 
        //                          SELECT t.* FROM Common_Function AS t 
        //                          INNER JOIN pr AS c ON t.COF_ParentID = c.COF_ID AND t.Obj_Status = 1
        //                         ) 
        //                         SELECT * FROM pr");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //角色ID
        //            db.AddInParameter(cmd, "@COF_ID", DbType.Int32, iCOF_ID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(FunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 删除菜单/界面/功能信息表
        //        /// </summary>
        //        /// <param name="iCOF_ID">ID</param>
        //        /// <param name="iMuser">操作人</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_Function_DELETE(int iCOF_ID, int iMuser)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append(@"WITH pr
        //                         AS 
        //                         (
        //                          SELECT * FROM Common_Function WHERE COF_ID = @COF_ID
        //                          UNION ALL 
        //                          SELECT t.* FROM Common_Function AS t 
        //                          INNER JOIN pr AS c ON t.COF_ParentID = c.COF_ID AND t.Obj_Status = 1
        //                         ) 
        //                         UPDATE Common_Function SET Obj_Status = '6', Obj_Muser = 1, Obj_Mdate = getdate() 
        //                         FROM pr
        //                         WHERE Common_Function.COF_ID = pr.COF_ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //ID
        //            db.AddInParameter(cmd, "@COF_ID", DbType.Int32, iCOF_ID);
        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>								
        //        /// 根据父节点ID获取功能权限信息列表						
        //        /// </summary>								
        //        /// <param name="iParentID">父节点ID</param>	
        //        /// <param name="iDataType">数据类型</param>
        //        /// <param name="iEnableType">状态(null全部true启用false不启用)</param>
        //        /// <param name="iCode">编号</param>	
        //        /// <param name="iName">名称</param>
        //        /// <param name="iOperateType">操作类型(0全部 1添加 2编辑 3删除）</param>
        //        /// <param name="iInfo">分页信息</param>
        //        /// <returns></returns>								
        //        public IList<FunctionDC> VELOUnion_Function_SELECT_List_ByParentIDANDDataTypeANDEnableType(int iParentID, VeloUnion_FunctionDataTypeEnum iDataType, bool? iEnableType, string iCode, string iName, VeloUnion_FunctionOperateTypeEnum iOperateType, ref PageInfo iInfo)
        //        {
        //            List<FunctionDC> list = new List<FunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sqlwhere = new StringBuilder();
        //            StringBuilder sqlorder = new StringBuilder();
        //            sqlwhere.Append("COF_ParentID = '" + iParentID + "' AND Obj_Status = 1 ");
        //            //数据类型
        //            if (iDataType != VeloUnion_FunctionDataTypeEnum.ALL)
        //                sqlwhere.Append("AND COF_DataType = '" + (int)iDataType + "' ");
        //            //状态
        //            if (iEnableType != null)
        //                sqlwhere.Append("AND COF_Enable = '" + (iEnableType == true ? "1" : "0") + "' ");
        //            //编号
        //            if (!string.IsNullOrEmpty(iCode))
        //                sqlwhere.Append("AND COF_Code = '" + iCode + "' ");
        //            //名称
        //            if (!string.IsNullOrEmpty(iName))
        //                sqlwhere.Append("AND COF_Name LIKE '%" + iName + "%' ");
        //            //数据类型
        //            if (iOperateType != VeloUnion_FunctionOperateTypeEnum.ALL)
        //                sqlwhere.Append("AND COF_OperateType = '" + (int)iOperateType + "' ");
        //            //排序
        //            if (!string.IsNullOrEmpty(iInfo.OrderByCol))
        //            {
        //                switch (iInfo.OrderByCol)
        //                {
        //                    case "code":
        //                        sqlorder.Append("COF_Code");
        //                        break;
        //                    case "name":
        //                        sqlorder.Append("COF_Name");
        //                        break;
        //                    case "type":
        //                        sqlorder.Append("COF_DataType");
        //                        break;
        //                    case "order":
        //                        sqlorder.Append("COF_OrderIndex");
        //                        break;
        //                    case "date":
        //                        sqlorder.Append("Obj_Mdate");
        //                        break;
        //                    case "status":
        //                        sqlorder.Append("COF_Enable");
        //                        break;
        //                    case "operate":
        //                        sqlorder.Append("COF_OperateType");
        //                        break;
        //                    default:
        //                        break;
        //                }

        //                //排序方式
        //                if (iInfo.OrderByType == OrderByType.ASC)
        //                    sqlorder.Append(" ASC");
        //                else if (iInfo.OrderByType == OrderByType.DESC)
        //                    sqlorder.Append(" DESC");
        //            }
        //            else
        //                sqlorder.Append("Obj_Mdate DESC");

        //            //取得总数量
        //            iInfo.TotalCount = ExecuteReaderRecordCountByPageIndex("Common_Function", "COF_ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iInfo.PageIndex, iInfo.PageSize, ApplicationModule.VeloUnion);
        //            //取得功能权限信息列表
        //            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Common_Function", "COF_ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iInfo.PageIndex, iInfo.PageSize, ApplicationModule.VeloUnion))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(FunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 查询权限树桩结构
        //        /// </summary>
        //        /// <param name="iDataType">操作类型(0全部 1添加 2编辑 3删除）</param>
        //        /// <returns></returns>
        //        public IList<FunctionDC> VELOUnion_Function_SELECT_List_ByDataType(IList<int> iDataType)
        //        {
        //            List<FunctionDC> list = new List<FunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT a.COF_ID, a.COF_ParentID, a.COF_Name FROM Common_Function a, Common_Function b WHERE a.Obj_Status = 1 
        //                            AND ((a.COF_ParentID = 0 AND a.COF_ID = b.COF_ID) OR a.COF_ParentID = b.COF_ID)
        //                            AND b.Obj_Status = 1 ");

        //            if (iDataType != null && iDataType.Count > 0)
        //            {
        //                sql.Append("AND (a.COF_DataType = @COF_DataType0 ");
        //                for (int i = 1; i < iDataType.Count; i++)
        //                {
        //                    sql.Append("OR a.COF_DataType = @COF_DataType" + i + " ");
        //                }
        //                sql.Append(") ");
        //            }
        //            sql.Append("ORDER BY a.COF_ID");


        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

        //            if (iDataType != null)
        //            {
        //                for (int i = 0; i < iDataType.Count; i++)
        //                {
        //                    //数据类型
        //                    db.AddInParameter(cmd, "@COF_DataType" + i, DbType.Int32, iDataType[i]);
        //                }
        //            }

        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(FunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 查询全部HRM人员信息表
        //        /// </summary>
        //        /// <returns></returns>
        //        public IList<LoginDC> VELOUnion_Login_SELECT_List_ALL()
        //        {
        //            List<LoginDC> list = new List<LoginDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT * FROM Common_Login");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(LoginDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 根据组织机构编号获取角色列表							
        //        /// </summary>								
        //        /// <param name="iRoleCode">角色关键字</param>
        //        /// <param name="iRoleName">角色名称</param>
        //        /// <param name="iEnableType">角色状态(null全部true启用false不启用)</param>
        //        /// <param name="iSearchType">是否递规获取</param>
        //        /// <param name="iInfo">数据分页信息</param>
        //        /// <returns></returns>								
        //        public IList<RoleDC> VELOUnion_Role_SELECT_List_ByRoleCodeANDEnableType(string iRoleCode, string iRoleName, bool? iEnableType, int iSearchType, ref PageInfo iInfo)
        //        {
        //            List<RoleDC> list = new List<RoleDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sqlwhere = new StringBuilder();
        //            StringBuilder sqlorder = new StringBuilder();
        //            sqlwhere.Append("Obj_Status = 1 ");
        //            //角色关键字
        //            if (!string.IsNullOrEmpty(iRoleCode))
        //                sqlwhere.Append("AND COR_Code = '" + iRoleCode + "' ");
        //            //名称
        //            if (!string.IsNullOrEmpty(iRoleName))
        //                sqlwhere.Append("AND COR_Name LIKE '%" + iRoleName + "%' ");
        //            //状态
        //            if (iEnableType != null)
        //                sqlwhere.Append("AND COR_Enable = '" + (iEnableType == true ? "1" : "0") + "' ");
        //            //排序
        //            if (!string.IsNullOrEmpty(iInfo.OrderByCol))
        //            {
        //                switch (iInfo.OrderByCol)
        //                {
        //                    case "code":
        //                        sqlorder.Append("COR_Code");
        //                        break;
        //                    case "name":
        //                        sqlorder.Append("COR_Name");
        //                        break;
        //                    case "enable":
        //                        sqlorder.Append("COR_Enable");
        //                        break;
        //                    default:
        //                        break;
        //                }

        //                //排序方式
        //                if (iInfo.OrderByType == OrderByType.ASC)
        //                    sqlorder.Append(" ASC");
        //                else if (iInfo.OrderByType == OrderByType.DESC)
        //                    sqlorder.Append(" DESC");
        //            }
        //            else
        //                sqlorder.Append("Obj_Mdate DESC");

        //            //取得总数量
        //            iInfo.TotalCount = ExecuteReaderRecordCountByPageIndex("Common_Role", "COR_ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iInfo.PageIndex, iInfo.PageSize, ApplicationModule.VeloUnion);
        //            //取得功能权限信息列表
        //            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Common_Role", "COR_ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iInfo.PageIndex, iInfo.PageSize, ApplicationModule.VeloUnion))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(RoleDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 查询全部HRM人员信息表
        //        /// </summary>
        //        /// <returns></returns>
        //        public IList<RoleDC> VELOUnion_Role_SELECT_List()
        //        {
        //            List<RoleDC> list = new List<RoleDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT * FROM Common_Role");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(RoleDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 通过员工ID获取权限关联列表								
        //        /// </summary>								
        //        /// <param name="iPersonnelID">员工ID</param>								
        //        /// <returns></returns>								
        //        public IList<FunctionDC> VELOUnion_PersonnelFunction_SELECT_List_ByPersonnelIDForFunction(int iPersonnelID)
        //        {
        //            List<FunctionDC> list = new List<FunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT b.* FROM Common_PersonnelFunction a, Common_Function b WHERE a.COP_PersonnelID = @PersonnelID 
        //                        AND a.Obj_Status = 1
        //                        AND a.COF_FunctionID = b.COF_ID
        //                        AND b.Obj_Status = 1
        //                        ORDER BY b.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //员工ID
        //            db.AddInParameter(cmd, "@PersonnelID", DbType.Int32, iPersonnelID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(FunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 查询HRM人员关联权限
        //        /// </summary>
        //        /// <param name="iPersonnelID">员工ID</param>
        //        /// <returns></returns>
        //        public IList<FunctionDC> VELOUnion_Function_SELECT_List_BYPersonnelID(int iPersonnelID)
        //        {
        //            List<FunctionDC> list = new List<FunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT d.* FROM (
        //                            SELECT COF_FunctionID as FunctionID FROM Common_PersonnelFunction WHERE COP_PersonnelID = @PersonnelID 
        //                            AND (COPF_BeginDate IS NULL OR COPF_BeginDate <= getdate()) 
        //                            AND (COPF_EndDate IS NULL OR COPF_EndDate >= getdate())  AND Obj_Status = 1
        //                            UNION 
        //                            SELECT b.COF_FunctionID FROM Common_RolePersonnel a, Common_RoleFunction b, Common_Role e WHERE a.COP_PersonnelID = @PersonnelID AND a.COR_RoleID = b.COR_RoleID AND e.COR_ID = a.COR_RoleID AND e.COR_Enable = 1 AND a.Obj_Status = 1 AND b.Obj_Status = 1) c, Common_Function d
        //                            WHERE d.COF_ID = c.FunctionID
        //                            AND d.COF_Enable = 1
        //                            AND d.Obj_Status = 1
        //                            ORDER BY d.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //员工ID
        //            db.AddInParameter(cmd, "@PersonnelID", DbType.Int32, iPersonnelID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(FunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>								
        //        /// 通过员工ID获取权限关联人员列表								
        //        /// </summary>								
        //        /// <param name="iPersonnelID">员工ID</param>								
        //        /// <returns></returns>								
        //        public IList<PersonnelFunctionDC> VELOUnion_PersonnelFunction_SELECT_List_ByPersonnelID(int iPersonnelID)
        //        {
        //            List<PersonnelFunctionDC> list = new List<PersonnelFunctionDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT a.* FROM Common_PersonnelFunction a, Common_Function b WHERE a.COP_PersonnelID = @COP_PersonnelID 
        //                            AND a.Obj_Status = 1 
        //                            AND a.COF_FunctionID = b.COF_ID
        //                            AND b.Obj_Status = 1
        //                            ORDER BY a.Obj_Mdate DESC");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //组织机构编号
        //            db.AddInParameter(cmd, "@COP_PersonnelID", DbType.Int32, iPersonnelID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(PersonnelFunctionDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 共通更新方法权限关联
        //        /// </summary>
        //        /// <param name="iParam">待更新数据</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_PersonnelFunction_Update_Common(IList<UpdateParamEntity> iParam)
        //        {
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            return Update(db, "Common_PersonnelFunction", iParam);
        //        }

        //        /// <summary>
        //        /// 删除人员关联界面/功能
        //        /// </summary>
        //        /// <param name="iPRPF_ID">ID</param>
        //        /// <param name="iMuser">操作人</param>
        //        /// <returns></returns>
        //        public bool VELOUnion_PersonnelFunction_DELETE(int iCOPF_ID, int iMuser)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("UPDATE Common_PersonnelFunction SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE COPF_ID = @COPF_ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //ID
        //            db.AddInParameter(cmd, "@COPF_ID", DbType.Int32, iCOPF_ID);
        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>
        //        /// 创建人员单独关联权限
        //        /// </summary>
        //        /// <param name="iPersonnelFunctionDC">人员关联界面/功能</param>
        //        /// <returns></returns>
        //        public int VELOUnion_PersonnelFunction_ADD(PersonnelFunctionDC iPersonnelFunctionDC)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("INSERT INTO Common_PersonnelFunction(COP_PersonnelID, COF_FunctionID, COPF_BeginDate, COPF_EndDate, ");
        //            //字段拼接
        //            AddCommonInsertSql(sql);
        //            sql.Append(") VALUES (@COP_PersonnelID, @COF_FunctionID, @COPF_BeginDate, @COPF_EndDate, ");
        //            //值拼接
        //            AddCommonInsertValues(sql);
        //            sql.Append(");SELECT @@IDENTITY;");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //人员ID
        //            db.AddInParameter(cmd, "@COP_PersonnelID", DbType.Int32, iPersonnelFunctionDC.COP_PersonnelID);
        //            //关联ID
        //            db.AddInParameter(cmd, "@COF_FunctionID", DbType.Int32, iPersonnelFunctionDC.COF_FunctionID);
        //            //开始时间
        //            db.AddInParameter(cmd, "@COPF_BeginDate", DbType.DateTime, iPersonnelFunctionDC.COPF_BeginDate);
        //            //结束时间
        //            db.AddInParameter(cmd, "@COPF_EndDate", DbType.DateTime, iPersonnelFunctionDC.COPF_EndDate);
        //            //添加共通参数
        //            AddCommonInsertParameter(db, cmd, iPersonnelFunctionDC);
        //            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
        //            return id;
        //        }
        //    }
    }
}
