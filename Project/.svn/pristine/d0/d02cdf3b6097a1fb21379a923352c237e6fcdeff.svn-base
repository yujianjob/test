using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.DAL
{
    public class UserDAL : DALBase, LazyAtHome.WCF.UserSystem.Interface.IDAL.IUserDAL
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public bool User_LoginPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET LoginPassword = @NewPassword, Obj_Mdate = GETDATE()");

            sql.Append(" WHERE ID = @ID AND LoginPassword = @OldPassword");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            //支付密码
            db.AddInParameter(cmd, "@OldPassword", DbType.String, iOldPassword);

            //支付密码
            db.AddInParameter(cmd, "@NewPassword", DbType.String, iNewPassword);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public bool User_LoginPassword_Reset(Guid iUserID, string iNewPassword)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET LoginPassword = @NewPassword, Obj_Mdate = GETDATE()");

            sql.Append(" WHERE ID = @ID ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            //登录密码
            db.AddInParameter(cmd, "@NewPassword", DbType.String, iNewPassword);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改支付密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public bool User_PayPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET PayPassword = @NewPassword, Obj_Mdate = GETDATE()");

            sql.Append(" WHERE ID = @ID AND PayPassword = @OldPassword");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            //支付密码
            db.AddInParameter(cmd, "@OldPassword", DbType.String, iOldPassword);

            //支付密码
            db.AddInParameter(cmd, "@NewPassword", DbType.String, iNewPassword);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public bool User_PayPassword_Reset(Guid iUserID, string iNewPassword)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET PayPassword = @NewPassword, Obj_Mdate = GETDATE()");

            sql.Append(" WHERE ID = @ID ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            //登录密码
            db.AddInParameter(cmd, "@NewPassword", DbType.String, iNewPassword);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改登录名
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLoginName"></param>
        /// <returns></returns>
        public bool User_LoginName_Change(Guid iUserID, string iLoginName)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET LoginName = @LoginName, Obj_Mdate = GETDATE()");

            sql.Append(" WHERE ID = @ID ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            //登录密码
            db.AddInParameter(cmd, "@LoginName", DbType.String, iLoginName);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public bool User_MPNo_Change(Guid iUserID, string iMPNo)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_MPNo_Change");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@MPNo", DbType.String, iMPNo);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            var rtn = Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));

            if (rtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            //StringBuilder sql = new StringBuilder();
            //Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            //sql.Append("UPDATE User_Info SET MPNo = @MPNo, Obj_Mdate = GETDATE()");

            //sql.Append(" WHERE ID = @ID ");
            //DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            ////
            //db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            ////登录密码
            //db.AddInParameter(cmd, "@MPNo", DbType.String, iMPNo);

            //return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public bool User_Email_Change(Guid iUserID, string iEmail)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET Email = @Email, Obj_Mdate = GETDATE()");

            sql.Append(" WHERE ID = @ID ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            //登录密码
            db.AddInParameter(cmd, "@Email", DbType.String, iEmail);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 用户存在查询
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public Guid? User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ID FROM User_Info WHERE 1 = 1 ");

            switch (iType)
            {
                case LoginType.LoginName://用户名
                    sql.Append(" AND LoginName = @Parameter ");
                    break;
                case LoginType.MPNo://手机号
                    sql.Append(" AND MPNo = @Parameter ");
                    break;
                case LoginType.Email://邮箱
                    sql.Append(" AND Email = @Parameter ");
                    break;
                case LoginType.RecommendedCode:
                    sql.Append(" AND RecommendedCode = @Parameter ");
                    break;
                case LoginType.Weixin:
                    sql.Append(@" AND EXISTS (SELECT 1 FROM User_OAuth 
                                                WHERE User_OAuth.UserID = User_Info.ID
                                                AND User_OAuth.Type = 1 AND OpenID = @Parameter 
                                                AND User_OAuth.Obj_Status = 1 ) ");
                    break;
                default:
                    throw new Exception("查询类型错误");
            }

            if (iUserID.HasValue)
            {
                sql.Append(" AND ID != @ID ");
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Parameter", DbType.String, iParameter);

            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.GetGuid(0);
                }
            }
            return null;
        }

        /// <summary>
        /// 昵称重名验证
        /// </summary>
        /// <param name="iNickName"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public bool User_NickName_Exist(string iNickName, Guid? iUserID = null)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(0) FROM User_Detail WHERE NickName = @NickName ");

            if (iUserID.HasValue)
            {
                sql.Append("AND ID != @ID");
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@NickName", DbType.String, iNickName);

            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <param name="iUser_DetailDC"></param>
        /// <param name="iUser_RegisterLogDC"></param>
        /// <returns></returns>
        public bool User_Regist(User_InfoDC iUser_InfoDC, User_DetailDC iUser_DetailDC, User_RegisterLogDC iUser_RegisterLogDC)
        {
            var _SeedID = User_Info_ADD(iUser_InfoDC);

            iUser_DetailDC.ID = iUser_InfoDC.ID;
            iUser_DetailDC.SeedID = _SeedID;

            if (!User_Detail_ADD(iUser_DetailDC))
            {
                return false;
            }

            //获取邀请用户
            Guid? _InviteUserID = User_Exist_Check(iUser_RegisterLogDC.InviteCode, LoginType.RecommendedCode, null);

            iUser_RegisterLogDC.UserID = iUser_InfoDC.ID;
            iUser_RegisterLogDC.InviteUserID = _InviteUserID;

            if (iUser_RegisterLogDC.Channel == 4)
            {
                iUser_RegisterLogDC.SourceID = User_WeixinAttentionLog_SELECT_Source(iUser_RegisterLogDC.SourceID);
            }

            if (!User_RegisterLog_ADD(iUser_RegisterLogDC))
            {
                return false;
            }

            //注册后处理
            User_Reg_Popularize(iUser_InfoDC.ID);

            return true;
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <returns></returns>
        private int User_Info_ADD(User_InfoDC iUser_InfoDC)
        {
            iUser_InfoDC.RecommendedCode = Guid.NewGuid().ToString().Substring(0, 8);
            while (User_Exist_Check(iUser_InfoDC.RecommendedCode, LoginType.RecommendedCode).HasValue)
            {
                iUser_InfoDC.RecommendedCode = Guid.NewGuid().ToString().Substring(0, 8);
            }

            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_Info(ID, LoginName, MPNo, Email, LoginPassword, PayPassword, Type, Site, LastLoginTime, RegistSource, RecommendedCode, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@ID, @LoginName, @MPNo, @Email, @LoginPassword, @PayPassword, @Type, @Site, @LastLoginTime, @RegistSource, @RecommendedCode, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            if (iUser_InfoDC.ID == Guid.Empty)
            {
                iUser_InfoDC.ID = Guid.NewGuid();
            }
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUser_InfoDC.ID);
            //登录名
            db.AddInParameter(cmd, "@LoginName", DbType.String, iUser_InfoDC.LoginName);
            //手机号
            db.AddInParameter(cmd, "@MPNo", DbType.String, iUser_InfoDC.MPNo);
            //邮箱
            db.AddInParameter(cmd, "@Email", DbType.String, iUser_InfoDC.Email);
            //登录密码
            db.AddInParameter(cmd, "@LoginPassword", DbType.String, iUser_InfoDC.LoginPassword);
            //支付密码
            db.AddInParameter(cmd, "@PayPassword", DbType.String, iUser_InfoDC.PayPassword);
            //类型(1:个人用户)
            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_InfoDC.Type);
            //站点(注册时的城市,可修改)
            db.AddInParameter(cmd, "@Site", DbType.Int32, iUser_InfoDC.Site);
            //最后登录时间
            db.AddInParameter(cmd, "@LastLoginTime", DbType.DateTime, iUser_InfoDC.LastLoginTime);
            //注册来源(1:网站)
            db.AddInParameter(cmd, "@RegistSource", DbType.Int32, iUser_InfoDC.RegistSource);
            //推荐码
            db.AddInParameter(cmd, "@RecommendedCode", DbType.String, iUser_InfoDC.RecommendedCode);

            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_InfoDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            User_Stat_ADD(iUser_InfoDC.ID);

            return id;
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <returns></returns>
        public bool User_Info_UPDATE(User_InfoDC iUser_InfoDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET Level = @Level WHERE ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Guid, iUser_InfoDC.ID);
            //会员等级
            db.AddInParameter(cmd, "@Level", DbType.String, iUser_InfoDC.Level);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 用户注册后操作
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        private void User_Reg_Popularize(Guid iUserID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_UserReg_Popularize");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 新增用户详情
        /// </summary>
        /// <param name="iUser_DetailDC"></param>
        /// <returns></returns>
        private bool User_Detail_ADD(User_DetailDC iUser_DetailDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_Detail(ID, SeedID, NickName, Birthday, Sex, IDCard, RealName, DistrictID, Location, MaritalStatus, Salary, Hobbies, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@ID, @SeedID, @NickName, @Birthday, @Sex, @IDCard, @RealName, @DistrictID, @Location, @MaritalStatus, @Salary, @Hobbies, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUser_DetailDC.ID);
            //
            db.AddInParameter(cmd, "@SeedID", DbType.Int32, iUser_DetailDC.SeedID);
            //昵称
            db.AddInParameter(cmd, "@NickName", DbType.String, iUser_DetailDC.NickName);
            //生日
            db.AddInParameter(cmd, "@Birthday", DbType.DateTime, iUser_DetailDC.Birthday);
            //性别(1:男 0:女 2:保密)
            db.AddInParameter(cmd, "@Sex", DbType.Int32, iUser_DetailDC.Sex);
            //身份证
            db.AddInParameter(cmd, "@IDCard", DbType.String, iUser_DetailDC.IDCard);
            //真实名称
            db.AddInParameter(cmd, "@RealName", DbType.String, iUser_DetailDC.RealName);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iUser_DetailDC.DistrictID);
            //所在地地址
            db.AddInParameter(cmd, "@Location", DbType.String, iUser_DetailDC.Location);
            //婚姻状况(1:未婚 2:已婚 3:离异)
            db.AddInParameter(cmd, "@MaritalStatus", DbType.Int32, iUser_DetailDC.MaritalStatus);
            //月收入(1:0-1999 2:2000-4999)
            db.AddInParameter(cmd, "@Salary", DbType.Int32, iUser_DetailDC.Salary);
            //兴趣爱好(空格分隔)
            db.AddInParameter(cmd, "@Hobbies", DbType.String, iUser_DetailDC.Hobbies);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_DetailDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 新增用户统计表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        private bool User_Stat_ADD(Guid iUserID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_Stat(ID, Obj_Cuser, Obj_Muser) VALUES (@ID, -1, -1)");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改用户详情
        /// </summary>
        /// <param name="iPR_OperatorDC"></param>
        /// <returns></returns>
        public bool User_Detail_UPDATE(User_DetailDC iUser_DetailDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE User_Detail SET Birthday = @Birthday, Sex = @Sex, IDCard = @IDCard, RealName = @RealName, 
                DistrictID = @DistrictID, Location = @Location, MaritalStatus = @MaritalStatus, Salary = @Salary,
                Hobbies = @Hobbies, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUser_DetailDC.ID);
            //生日 1:男 0:女 2:保密
            db.AddInParameter(cmd, "@Birthday", DbType.DateTime, iUser_DetailDC.Birthday);
            //性别
            db.AddInParameter(cmd, "@Sex", DbType.Int32, iUser_DetailDC.Sex);
            //身份证
            db.AddInParameter(cmd, "@IDCard", DbType.String, iUser_DetailDC.IDCard);
            //用户邮箱
            db.AddInParameter(cmd, "@RealName", DbType.String, iUser_DetailDC.RealName);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iUser_DetailDC.DistrictID);
            //所在地地址
            db.AddInParameter(cmd, "@Location", DbType.String, iUser_DetailDC.Location);
            //婚姻状况
            db.AddInParameter(cmd, "@MaritalStatus", DbType.Int32, iUser_DetailDC.MaritalStatus);
            //月收入
            db.AddInParameter(cmd, "@Salary", DbType.Int32, iUser_DetailDC.Salary);
            //兴趣爱好
            db.AddInParameter(cmd, "@Hobbies", DbType.String, iUser_DetailDC.Hobbies);

            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iUser_DetailDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iDate"></param>
        private void User_LastLoginTime_UPDATE(Guid iUserID, DateTime iDate)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Info SET LastLoginTime = @LastLoginTime WHERE ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            db.AddInParameter(cmd, "@LastLoginTime", DbType.DateTime, iDate);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 用户信息查询列表
        /// </summary>
        /// <param name="iLoginName"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<User_InfoDC> User_Info_SELECT_List(string iLoginName, string iMPNo,
                string iEmail, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<User_InfoDC> list = new List<User_InfoDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@"Obj_Status = 1 ");

            sqlorder.Append(" Obj_Cdate DESC ");

            //名称
            if (!string.IsNullOrEmpty(iLoginName))
                sqlwhere.Append("AND LoginName = '" + iLoginName + "' ");
            //手机
            if (!string.IsNullOrEmpty(iMPNo))
                sqlwhere.Append("AND MPNo = '" + iMPNo + "' ");
            //邮箱
            if (!string.IsNullOrEmpty(iEmail))
                sqlwhere.Append("AND EMail = '" + iEmail + "' ");

            //注册时间开始
            if (iStartDate != null)
                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //注册时间结束
            if (iEndDate != null)
                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<User_InfoDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_Info", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_Info", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(User_InfoDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public User_InfoDC User_Info_SELECT_Entity(Guid iID)
        {
            User_InfoDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_Info WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Guid, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_InfoDC.GetEntity(reader);
                }
            }
            return entity;
        }


        /// <summary>
        /// 用户获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        public User_InfoDC User_Info_SELECT_Entity(string iOpenid, OAuthType iOAuthType)
        {
            User_InfoDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM User_Info 
                WHERE ID = (SELECT TOP 1 UserID 
                                FROM User_OAuth WHERE OpenID = @OpenID AND Type = @Type AND Obj_Status = 1)");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iOAuthType);

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_InfoDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public User_InfoDC User_Info_SELECT_Entity(string iMPNo)
        {
            User_InfoDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_Info WHERE Obj_Status = 1 ");
            sql.Append("AND MPNo = @MPNo ");

            DbCommand cmd = cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@MPNo", DbType.String, iMPNo);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_InfoDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <returns></returns>
        public User_InfoDC User_Info_SELECT_Entity(string iParameter, LoginType iType, string iLoginPassword)
        {
            User_InfoDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_Info WHERE Obj_Status = 1 ");
            sql.Append("AND LoginPassword = @LoginPassword ");

            switch (iType)
            {
                case LoginType.LoginName://用户名
                    sql.Append("AND LoginName = @Parameter ");
                    break;
                case LoginType.MPNo://手机号
                    sql.Append("AND MPNo = @Parameter ");
                    break;
                case LoginType.Email://邮箱
                    sql.Append("AND Email = @Parameter ");
                    break;
                default:
                    throw new Exception("查询类型错误");
            }

            DbCommand cmd = cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@LoginPassword", DbType.String, iLoginPassword);

            db.AddInParameter(cmd, "@Parameter", DbType.String, iParameter);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_InfoDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public User_DetailDC User_Detail_SELECT_Entity(Guid iID)
        {
            User_DetailDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_Detail WHERE ID = @ID AND Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@ID", DbType.Guid, iID);
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_DetailDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 注册日志
        /// </summary>
        /// <param name="iUser_RegisterLogDC"></param>
        /// <returns></returns>
        public bool User_RegisterLog_ADD(User_RegisterLogDC iUser_RegisterLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_RegisterLog(UserID, Channel, SourceID, InviteUserID, RegisterIP, InviteCode, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @Channel, @SourceID, @InviteUserID, @RegisterIP, @InviteCode, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_RegisterLogDC.UserID);
            //渠道(0：通用注册 1：门店注册 2：APP注册 3：新浪微博注册 4：微信注册 5: WEB注册 6:短信注册)
            db.AddInParameter(cmd, "@Channel", DbType.Int32, iUser_RegisterLogDC.Channel);
            //来源ID(web时记城市)
            db.AddInParameter(cmd, "@SourceID", DbType.String, iUser_RegisterLogDC.SourceID);
            //邀请人ID
            db.AddInParameter(cmd, "@InviteUserID", DbType.Guid, iUser_RegisterLogDC.InviteUserID);
            //注册
            db.AddInParameter(cmd, "@InviteCode", DbType.String, iUser_RegisterLogDC.InviteCode);

            db.AddInParameter(cmd, "@RegisterIP", DbType.String, iUser_RegisterLogDC.RegisterIP);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_RegisterLogDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="iUser_LoginLogDC"></param>
        /// <returns></returns>
        public bool User_LoginLog_ADD(User_LoginLogDC iUser_LoginLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_LoginLog(UserID, Channel, LogType, LoginIP, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @Channel, @LogType, @LoginIP, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_LoginLogDC.UserID);
            //渠道(0：通用注册 1：门店注册 2：APP注册 3：新浪微博注册 4：微信注册 5: WEB注册 6:短信注册)
            db.AddInParameter(cmd, "@Channel", DbType.Int32, iUser_LoginLogDC.Channel);
            //登录类型(1:登录 2:唤醒)
            db.AddInParameter(cmd, "@LogType", DbType.Int32, iUser_LoginLogDC.LogType);
            //登录IP
            db.AddInParameter(cmd, "@LoginIP", DbType.String, iUser_LoginLogDC.LoginIP);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_LoginLogDC);
            var rtn = db.ExecuteNonQuery(cmd) > 0 ? true : false;

            User_LastLoginTime_UPDATE(iUser_LoginLogDC.UserID, DateTime.Now);

            return rtn;
        }

        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        public bool User_NickName_Change(Guid iUserID, string iNickName)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Detail SET NickName = @NickName, Obj_Mdate = GETDATE() WHERE ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            //登录密码
            db.AddInParameter(cmd, "@NickName", DbType.String, iNickName);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 用户邀请数量
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public int User_Invite_Count(Guid iUserID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT COUNT(0) FROM User_RegisterLog,User_Info
                    WHERE User_RegisterLog.UserID = User_Info.ID
                    AND User_RegisterLog.Obj_Status = 1 AND InviteUserID = @ID
                    AND User_Info.MPNo IS NOT NULL");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public IList<User_ConsigneeAddressDC> User_ConsigneeAddress_SELECT_List(Guid iUserID)
        {
            List<User_ConsigneeAddressDC> list = new List<User_ConsigneeAddressDC>();
            User_ConsigneeAddressDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT User_ConsigneeAddress.*,C.FullName AS DistrictName,
                B.Name AS CityName,
                A.Name AS ProvinceName
                FROM User_ConsigneeAddress 
                LEFT JOIN Base_District C ON User_ConsigneeAddress.DistrictID = C.ID AND C.Level = 3 AND C.Obj_Status = 1
                LEFT JOIN Base_District B ON C.Code1 = B.Code1 AND C.Code2 = B.Code2 AND B.Code3 = '00' AND B.Level = 2 AND B.Obj_Status = 1
                LEFT JOIN Base_District A ON B.Code1 = A.Code1 AND A.Code2 = '00' AND A.Code3 = '00'  AND A.Level = 1 AND A.Obj_Status = 1
                WHERE UserID = @UserID AND User_ConsigneeAddress.Obj_Status = 1
            
            ORDER BY User_ConsigneeAddress.IsDefault DESC,User_ConsigneeAddress.ID DESC
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //登录名
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = User_ConsigneeAddressDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public User_ConsigneeAddressDC User_ConsigneeAddress_SELECT_Entity(int iID)
        {
            User_ConsigneeAddressDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT User_ConsigneeAddress.*,C.FullName AS DistrictName,
                B.Name AS CityName,
                A.Name AS ProvinceName
                FROM User_ConsigneeAddress 
                LEFT JOIN Base_District C ON User_ConsigneeAddress.DistrictID = C.ID AND C.Level = 3 AND C.Obj_Status = 1
                LEFT JOIN Base_District B ON C.Code1 = B.Code1 AND C.Code2 = B.Code2 AND B.Code3 = '00' AND B.Level = 2 AND B.Obj_Status = 1
                LEFT JOIN Base_District A ON B.Code1 = A.Code1 AND A.Code2 = '00' AND A.Code3 = '00'  AND A.Level = 1 AND A.Obj_Status = 1
                WHERE User_ConsigneeAddress.ID = @ID 
            ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_ConsigneeAddressDC.GetEntity(reader);
                }
            }
            return entity;
        }


        /// <summary>
        /// 收货地址
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public int User_ConsigneeAddress_ADD(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"
                IF(@IsDefault = 1)
                BEGIN
                    UPDATE User_ConsigneeAddress SET IsDefault = 0 WHERE UserID = @UserID AND Obj_Status = 1
                END
            ");
            sql.Append("INSERT INTO User_ConsigneeAddress(UserID, Consignee, DistrictID, Address, MPNo, Phone, IsDefault, ZipCode, Email, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @Consignee, @DistrictID, @Address, @MPNo, @Phone, @IsDefault, @ZipCode, @Email, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_ConsigneeAddressDC.UserID);
            //收货人
            db.AddInParameter(cmd, "@Consignee", DbType.String, iUser_ConsigneeAddressDC.Consignee);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iUser_ConsigneeAddressDC.DistrictID);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iUser_ConsigneeAddressDC.Address);
            //手机号
            db.AddInParameter(cmd, "@MPNo", DbType.String, iUser_ConsigneeAddressDC.MPNo);
            //电话号码
            db.AddInParameter(cmd, "@Phone", DbType.String, iUser_ConsigneeAddressDC.Phone);
            //默认地址
            db.AddInParameter(cmd, "@IsDefault", DbType.Int32, iUser_ConsigneeAddressDC.IsDefault);
            //邮编
            db.AddInParameter(cmd, "@ZipCode", DbType.String, iUser_ConsigneeAddressDC.ZipCode);
            //邮箱
            db.AddInParameter(cmd, "@Email", DbType.String, iUser_ConsigneeAddressDC.Email);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_ConsigneeAddressDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            if (iUser_ConsigneeAddressDC.IsDefault == 1)
            {
                User_ConsigneeAddress_SetDefault(id);
            }
            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public bool User_ConsigneeAddress_UPDATE(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_ConsigneeAddress SET UserID = @UserID, Consignee = @Consignee, DistrictID = @DistrictID, Address = @Address, MPNo = @MPNo, Phone = @Phone, ZipCode = @ZipCode, Email = @Email, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iUser_ConsigneeAddressDC.ID);
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_ConsigneeAddressDC.UserID);
            //收货人
            db.AddInParameter(cmd, "@Consignee", DbType.String, iUser_ConsigneeAddressDC.Consignee);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iUser_ConsigneeAddressDC.DistrictID);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iUser_ConsigneeAddressDC.Address);
            //手机号
            db.AddInParameter(cmd, "@MPNo", DbType.String, iUser_ConsigneeAddressDC.MPNo);
            //电话号码
            db.AddInParameter(cmd, "@Phone", DbType.String, iUser_ConsigneeAddressDC.Phone);
            //邮编
            db.AddInParameter(cmd, "@ZipCode", DbType.String, iUser_ConsigneeAddressDC.ZipCode);
            //邮箱
            db.AddInParameter(cmd, "@Email", DbType.String, iUser_ConsigneeAddressDC.Email);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iUser_ConsigneeAddressDC);
            var rtn = db.ExecuteNonQuery(cmd) > 0 ? true : false;
            if (iUser_ConsigneeAddressDC.IsDefault == 1)
            {
                User_ConsigneeAddress_SetDefault(iUser_ConsigneeAddressDC.ID);
            }
            return rtn;
        }

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="iConsigneeAddressID"></param>
        /// <returns></returns>
        public bool User_ConsigneeAddress_SetDefault(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE User_ConsigneeAddress SET IsDefault = 0 WHERE UserID =(SELECT UserID FROM User_ConsigneeAddress WHERE ID = @ID) AND Obj_Status = 1 AND IsDefault = 1");

            sql.Append("UPDATE User_ConsigneeAddress SET IsDefault = 1,Obj_Mdate = GETDATE() WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool User_ConsigneeAddress_DELETE(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_ConsigneeAddress SET Obj_Status = 6, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 邮件订阅
        /// </summary>
        /// <param name="iUser_MailSubscriptionDC"></param>
        /// <returns></returns>
        public int User_MailSubscription_ADD(User_MailSubscriptionDC iUser_MailSubscriptionDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_MailSubscription(UserID, ProjectType, GroupID, Type, Site, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @ProjectType, @GroupID, @Type, @Site, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_MailSubscriptionDC.UserID);
            //订阅类别(1:干洗)
            db.AddInParameter(cmd, "@ProjectType", DbType.Int32, iUser_MailSubscriptionDC.ProjectType);
            //分组ID(用户分组)
            db.AddInParameter(cmd, "@GroupID", DbType.Int32, iUser_MailSubscriptionDC.GroupID);
            //类型(1:帐号变动 2:账户变动)
            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_MailSubscriptionDC.Type);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iUser_MailSubscriptionDC.Site);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_MailSubscriptionDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 投诉
        /// </summary>
        /// <param name="iUser_ComplaintsDC"></param>
        /// <returns></returns>
        public int User_Complaints_ADD(User_ComplaintsDC iUser_ComplaintsDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_Complaints(UserID, No, MPNo, Name, Type, Content, HandleStatus, HandleEmployeeID, HandleDate, HandleContent, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @No, @MPNo, @Name, @Type, @Content, @HandleStatus, @HandleEmployeeID, @HandleDate, @HandleContent, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_ComplaintsDC.UserID);
            //编号
            db.AddInParameter(cmd, "@No", DbType.String, iUser_ComplaintsDC.No);
            //手机号
            db.AddInParameter(cmd, "@MPNo", DbType.String, iUser_ComplaintsDC.MPNo);
            //投诉人
            db.AddInParameter(cmd, "@Name", DbType.String, iUser_ComplaintsDC.Name);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_ComplaintsDC.Type);
            //内容
            db.AddInParameter(cmd, "@Content", DbType.String, iUser_ComplaintsDC.Content);
            //处理状态(0：未处理  1：已处理)
            db.AddInParameter(cmd, "@HandleStatus", DbType.Int32, iUser_ComplaintsDC.HandleStatus);
            //处理人ID
            db.AddInParameter(cmd, "@HandleEmployeeID", DbType.Int32, iUser_ComplaintsDC.HandleEmployeeID);
            //处理时间
            db.AddInParameter(cmd, "@HandleDate", DbType.DateTime, iUser_ComplaintsDC.HandleDate);
            //处理内容
            db.AddInParameter(cmd, "@HandleContent", DbType.String, iUser_ComplaintsDC.HandleContent);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_ComplaintsDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<User_AmountLogDC> User_AmountLog_SELECT_List(
            Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<User_AmountLogDC> list = new List<User_AmountLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" User_AmountLog.Obj_Status = 1 ");
            sqlwhere.Append(@" AND User_AmountLog.UserID = User_Info.ID ");

            sqlorder.Append(@" User_AmountLog.ID DESC ");

            sqlfields.Append(" User_AmountLog.*,User_Info.MPNo ");

            //用户ID
            if (iUserID.HasValue)
                sqlwhere.Append("AND User_AmountLog.UserID = '" + iUserID + "' ");
            //用户手机
            if (!string.IsNullOrWhiteSpace(iMPNo))
                sqlwhere.Append("AND User_Info.MPNo = '" + iMPNo + "' ");

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND User_AmountLog.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND User_AmountLog.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<User_AmountLogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_AmountLog,User_Info", "User_AmountLog.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_AmountLog,User_Info", "User_AmountLog.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(User_AmountLogDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<User_ScoreLogDC> User_ScoreLog_SELECT_List(
         Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<User_ScoreLogDC> list = new List<User_ScoreLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@"User_ScoreLog.Obj_Status = 1 ");
            sqlorder.Append(@" User_ScoreLog.ID DESC ");

            sqlfields.Append(" User_ScoreLog.*,User_Info.MPNo ");

            //用户ID
            if (iUserID.HasValue)
                sqlwhere.Append("AND User_ScoreLog.UserID = '" + iUserID + "' ");
            //用户手机
            if (!string.IsNullOrWhiteSpace(iMPNo))
                sqlwhere.Append("AND User_Info.MPNo = '" + iMPNo + "' ");

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND User_ScoreLog.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND User_ScoreLog.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<User_ScoreLogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_ScoreLog,User_Info", "User_ScoreLog.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_ScoreLog,User_Info", "User_ScoreLog.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(User_ScoreLogDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="iUser_WeixinDC"></param>
        ///// <returns></returns>
        //public int User_Weixin_ADD(User_WeixinDC iUser_WeixinDC)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("INSERT INTO User_Weixin(openid, nickname, sex, province, city, country, headimgurl, ");
        //    //字段拼接
        //    AddCommonInsertSql(sql);
        //    sql.Append(") VALUES (@openid, @nickname, @sex, @province, @city, @country, @headimgurl, ");
        //    //值拼接
        //    AddCommonInsertValues(sql);
        //    sql.Append(");SELECT @@IDENTITY;");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //
        //    db.AddInParameter(cmd, "@openid", DbType.String, iUser_WeixinDC.openid);
        //    //
        //    db.AddInParameter(cmd, "@nickname", DbType.String, iUser_WeixinDC.nickname);
        //    //
        //    db.AddInParameter(cmd, "@sex", DbType.Int32, iUser_WeixinDC.sex);
        //    //
        //    db.AddInParameter(cmd, "@province", DbType.String, iUser_WeixinDC.province);
        //    //
        //    db.AddInParameter(cmd, "@city", DbType.String, iUser_WeixinDC.city);
        //    //
        //    db.AddInParameter(cmd, "@country", DbType.String, iUser_WeixinDC.country);
        //    //
        //    db.AddInParameter(cmd, "@headimgurl", DbType.String, iUser_WeixinDC.headimgurl);
        //    //添加共通参数
        //    AddCommonInsertParameter(db, cmd, iUser_WeixinDC);
        //    int id = Convert.ToInt32(db.ExecuteScalar(cmd));
        //    return id;
        //}

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="iUser_WeixinDC"></param>
        ///// <returns></returns>
        //public bool User_Weixin_UPDATE(User_WeixinDC iUser_WeixinDC)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE User_Weixin SET openid = @openid, nickname = @nickname, sex = @sex, province = @province, city = @city, country = @country, headimgurl = @headimgurl, ");
        //    //字段拼接
        //    AddCommonUpdateSql(sql);
        //    sql.Append(" WHERE ID = @ID");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iUser_WeixinDC.ID);
        //    //
        //    db.AddInParameter(cmd, "@openid", DbType.String, iUser_WeixinDC.openid);
        //    //
        //    db.AddInParameter(cmd, "@nickname", DbType.String, iUser_WeixinDC.nickname);
        //    //
        //    db.AddInParameter(cmd, "@sex", DbType.Int32, iUser_WeixinDC.sex);
        //    //
        //    db.AddInParameter(cmd, "@province", DbType.String, iUser_WeixinDC.province);
        //    //
        //    db.AddInParameter(cmd, "@city", DbType.String, iUser_WeixinDC.city);
        //    //
        //    db.AddInParameter(cmd, "@country", DbType.String, iUser_WeixinDC.country);
        //    //
        //    db.AddInParameter(cmd, "@headimgurl", DbType.String, iUser_WeixinDC.headimgurl);
        //    //添加共通参数
        //    AddCommonUpdateParameter(db, cmd, iUser_WeixinDC);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        ///// <summary>
        ///// 更新用户ID
        ///// </summary>
        ///// <param name="iID"></param>
        ///// <param name="iUserID"></param>
        ///// <returns></returns>
        //public bool User_Weixin_UPDATE_UserID(int iID, Guid iUserID)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

        //    sql.Append("UPDATE User_Weixin SET UserID = @UserID WHERE ID = @ID");

        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //    //
        //    db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        ///// <summary>
        ///// 根据ID查询
        ///// </summary>
        ///// <param name="iOpenid"></param>
        ///// <returns></returns>
        //public User_WeixinDC User_Weixin_SELECT_BYID(string iOpenid)
        //{
        //    User_WeixinDC entity = null;
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("SELECT * FROM User_Weixin WHERE openid = @openid AND Obj_Status = 1");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //
        //    db.AddInParameter(cmd, "@openid", DbType.String, iOpenid);
        //    using (IDataReader reader = db.ExecuteReader(cmd))
        //    {
        //        if (reader.Read())
        //        {
        //            entity = User_WeixinDC.GetEntity(reader);
        //        }
        //    }
        //    return entity;
        //}

        /// <summary>
        /// 用户绑卡
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        public int User_Card_Bind(Guid iUserID, UserCardType iUserCardType, string iCardPassword)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Card_Bind");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iUserCardType);
            db.AddInParameter(cmd, "@CardPassword", DbType.String, LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(iCardPassword));
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));
        }

        /// <summary>
        /// 卡绑定前检查
        /// </summary>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        public int User_Card_Bind_Check(UserCardType iUserCardType, string iCardPassword)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT  Used,ExpiryDate
                FROM Base_Card 
                WHERE Type = @Type AND Password = @CardPassword AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //登录名
            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iUserCardType);
            db.AddInParameter(cmd, "@CardPassword", DbType.String, LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(iCardPassword));

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) != 0)
                    {
                        //卡已被绑定
                        return -2;
                    }
                    else if (reader.GetDateTime(1) < DateTime.Today.AddDays(1))
                    {
                        //卡已过期
                        return -2;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            //卡密码无效
            return -1;
        }

        /// <summary>
        /// 获取用户卡列表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public IList<User_CardDC> User_Card_SELECT_List(Guid iUserID)
        {
            List<User_CardDC> list = new List<User_CardDC>();
            User_CardDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT User_Card.ID, Base_Card.Type,Base_Card.Number,Base_Card.Password,Base_Card.Money,
                    Base_Card.Balance,Base_Card.ExpiryDate,Base_Card.CityCode,Base_Card.Batch,
                    Base_Card.Enable,Base_Card.Used,User_Card.CardID,User_Card.UserID
                FROM Base_Card,User_Card
                WHERE Base_Card.ID = User_Card.CardID
                AND User_Card.UserID = @UserID
                AND Base_Card.Obj_Status = 1
                AND User_Card.Obj_Status = 1
                AND Base_Card.Enable = 1
                AND User_Card.RelationType = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //登录名
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = User_CardDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取用户卡
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardID"></param>
        /// <returns></returns>
        public User_CardDC User_Card_SELECT_Entity(Guid iUserID, int iUserCardID)
        {
            User_CardDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT User_Card.ID, Base_Card.Type,Base_Card.Number,Base_Card.Password,Base_Card.Money,
                    Base_Card.Balance,Base_Card.ExpiryDate,Base_Card.CityCode,Base_Card.Batch,
                    Base_Card.Enable,Base_Card.Used,User_Card.CardID,User_Card.UserID
                FROM Base_Card,User_Card
                WHERE Base_Card.ID = User_Card.CardID
                AND User_Card.UserID = @UserID
                AND User_Card.ID = @ID
                AND Base_Card.Obj_Status = 1
                AND User_Card.Obj_Status = 1
                AND Base_Card.Enable = 1
                --AND User_Card.RelationType = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@ID", DbType.Int32, iUserCardID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_CardDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 查询卡消费日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCardID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Base_CardLogDC> Base_CardLog_SELECT_List(Guid? iUserID, int? iCardID,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<Base_CardLogDC> list = new List<Base_CardLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 AND Type IN(3,4) ");
            sqlorder.Append(@" ID DESC");

            if (iUserID.HasValue)
                sqlwhere.Append("AND UserID = '" + iUserID + "' ");
            if (iCardID.HasValue)
                sqlwhere.Append("AND CardID = '" + iCardID + "' ");

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Base_CardLogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Base_CardLog", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Base_CardLog", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Base_CardLogDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        ///  微信ID获取用户ID
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        public Guid? User_Weixin_SELECT_UserID(string iOpenid)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT ID FROM User_Info WHERE ID = (SELECT UserID 
                                FROM User_OAuth WHERE OpenID = @OpenID AND Type = @Type AND Obj_Status = 1)");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)OAuthType.Weixin);

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.GetGuid(0);
                }
            }
            return null;
        }

        /// <summary>
        /// 微信ID已绑定用户
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        public bool wx_User_Weixin_CheckIsBind(string iOpenid)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT COUNT(0) FROM User_Info WHERE ID = (SELECT TOP 1 UserID 
                                FROM User_OAuth WHERE OpenID = @OpenID AND Type = @Type AND Obj_Status = 1)");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)OAuthType.Weixin);

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_OAuthDC"></param>
        /// <returns></returns>
        public int User_OAuth_ADD(User_OAuthDC iUser_OAuthDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_OAuth SET Obj_Status = 6, Obj_Mdate = getdate() WHERE OpenID = @OpenID AND Obj_Status = 1; ");
            sql.Append("INSERT INTO User_OAuth(UserID, Type, OpenID, AccessToken, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @Type, @OpenID, @AccessToken, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_OAuthDC.UserID);
            //第三方合作类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_OAuthDC.Type);
            //第三方ID
            db.AddInParameter(cmd, "@OpenID", DbType.String, iUser_OAuthDC.OpenID);
            //临时令牌
            db.AddInParameter(cmd, "@AccessToken", DbType.String, iUser_OAuthDC.AccessToken);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_OAuthDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool User_OAuth_DELETE(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_OAuth SET Obj_Status = 6, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除授权
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        public bool User_OAuth_DELETE(Guid iUserID, string iOpenid, OAuthType iOAuthType)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_OAuth SET Obj_Status = 6, Obj_Mdate = getdate() WHERE UserID = @UserID AND Type = @Type AND OpenID = @OpenID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iOAuthType);
            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenid);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IList<User_OAuthDC> User_OAuth_SELECT_UserID(Guid iUserID)
        {
            List<User_OAuthDC> list = new List<User_OAuthDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_OAuth WHERE UserID = @UserID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //有效数据
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(User_OAuthDC.GetEntity(reader));
                }
            }
            return list;
        }

        #region 用户消息

        /// <summary>
        /// 查询用户消息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCardID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<User_Message_PrivateDC> User_Message_Private_SELECT_List(
            Guid iUserID, int iPageIndex, int iPageSize)
        {
            List<User_Message_PrivateDC> list = new List<User_Message_PrivateDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");
            sqlorder.Append(@" ID DESC");

            sqlwhere.Append(" AND UserID = '" + iUserID + "' ");

            var rtn = new RecordCountEntity<User_Message_PrivateDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_Message_Private", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_Message_Private", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(User_Message_PrivateDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }


        /// <summary>
        /// 消息置已读
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool User_Message_Private_Read(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Message_Private SET ReadStatus = 1, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            db.ExecuteNonQuery(cmd);
            return true;
        }

        /// <summary>
        /// 消息删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool User_Message_Private_DELETE(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_Message_Private SET Obj_Status = 6, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            db.ExecuteNonQuery(cmd);
            return true;
        }

        /// <summary>
        /// 根据ID查询消息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public User_Message_PrivateDC User_Message_Private_SELECT_Entity(int iID)
        {
            User_Message_PrivateDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_Message_Private WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_Message_PrivateDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        #region 优惠券

        /// <summary>
        /// 查询优惠券列表
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iCouponName"></param>
        /// <param name="iUseClass"></param>
        /// <param name="iUseType"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<User_CouponDC> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo,
            string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<User_CouponDC> list = new List<User_CouponDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfield = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" User_Coupon.Obj_Status = 1 AND Base_Coupon.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Base_Coupon.CommitStatus = 1 ");
            sqlwhere.Append(@" AND User_Coupon.CouponID = Base_Coupon.ID ");
            sqlwhere.Append(@" AND User_Coupon.CouponStatus IN (1,2) ");
            sqlwhere.Append(@" AND User_Coupon.UserID = User_Info.ID ");

            sqlorder.Append(@" User_Coupon.ID ");

            sqlfield.Append(@" User_Coupon.*,Base_Coupon.Name,Base_Coupon.UseClass,Base_Coupon.UseType,
                    Base_Coupon.FaceValue,Base_Coupon.MinMoney,Base_Coupon.MaxMoney,Base_Coupon.TotalCount,
                    Base_Coupon.SendCount,Base_Coupon.ValidDay,User_Info.MPNo ");

            if (iUserID.HasValue)
                sqlwhere.Append(" AND User_Coupon.UserID = '" + iUserID + "' ");

            //手机号
            if (!string.IsNullOrEmpty(iMPNo))
                sqlwhere.Append(" AND User_Info.MPNo = '" + iMPNo + "' ");

            //名称
            if (!string.IsNullOrEmpty(iCouponName))
                sqlwhere.Append(" AND User_Info.Name LIKE '%" + iCouponName + "%' ");

            //使用类别
            if (iCouponStatus.HasValue)
            {
                switch (iCouponStatus.Value)
                {
                    case CouponStatus.Expired:
                        sqlwhere.Append(" AND User_Coupon.UseEndDate <= GETDATE() AND CouponStatus = 1 ");
                        break;
                    case CouponStatus.Normal:
                        sqlwhere.Append(" AND User_Coupon.UseEndDate > GETDATE() AND CouponStatus = 1 ");
                        break;
                    default:
                        sqlwhere.Append(" AND User_Coupon.CouponStatus =" + (int)iCouponStatus + " ");
                        break;
                }
            }

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND User_Coupon.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND User_Coupon.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<User_CouponDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Base_Coupon,User_Coupon,User_Info", "User_Coupon.ID", sqlfield.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Base_Coupon,User_Coupon,User_Info", "User_Coupon.ID", sqlfield.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = User_CouponDC.GetEntity(reader);

                    //0:全类别使用 1:限品类
                    if (entity.UseClass == 1)
                    {
                        entity.CouponProductList = Base_CouponProduct_SELECT_List(entity.CouponID);
                    }

                    list.Add(entity);
                }
            }
            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 获取优惠券限制品类
        /// </summary>
        /// <param name="iCouponID"></param>
        /// <returns></returns>
        private IList<Base_CouponProductDC> Base_CouponProduct_SELECT_List(int iCouponID)
        {
            List<Base_CouponProductDC> list = new List<Base_CouponProductDC>();
            Base_CouponProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Base_CouponProduct.*
                ,Wash_Product.Name
                ,Wash_Product.SalesPrice
                FROM Base_CouponProduct,Wash_Product 
            WHERE CouponID = @CouponID 
            AND Base_CouponProduct.ProductID = Wash_Product.ID
            AND Base_CouponProduct.Obj_Status = 1
            AND Wash_Product.Obj_Status = 1
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@CouponID", DbType.Int32, iCouponID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Base_CouponProductDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserCouponID"></param>
        /// <returns></returns>
        public User_CouponDC User_Coupon_SELECT_Entity(int iUserCouponID)
        {
            User_CouponDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();

            sqlwhere.Append(@"SELECT 
                    User_Coupon.*,Base_Coupon.Name,Base_Coupon.UseClass,Base_Coupon.UseType,
                    Base_Coupon.FaceValue,Base_Coupon.MinMoney,Base_Coupon.MaxMoney,Base_Coupon.TotalCount,
                    Base_Coupon.SendCount,Base_Coupon.ValidDay,User_Info.MPNo
                FROM Base_Coupon,User_Coupon,User_Info WHERE User_Coupon.Obj_Status = 1 AND Base_Coupon.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Base_Coupon.CommitStatus = 1 ");
            sqlwhere.Append(@" AND User_Coupon.CouponID = Base_Coupon.ID ");
            sqlwhere.Append(@" AND User_Coupon.CouponStatus IN (1,2) ");
            sqlwhere.Append(@" AND User_Coupon.UserID = User_Info.ID ");

            sqlwhere.Append(@" AND User_Coupon.ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sqlwhere.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iUserCouponID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_CouponDC.GetEntity(reader);
                }
            }

            return entity;
        }

        /// <summary>
        /// 用户绑定券检查
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public int User_Coupon_Bind_Check(Guid iUserID, string iCouponCode)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Coupon_Bind_Check");

            iCouponCode = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(iCouponCode);

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@CouponCode", DbType.String, iCouponCode);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            var rtn = Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public int User_Coupon_Bind(Guid iUserID, string iCouponCode)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Coupon_Bind");

            iCouponCode = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(iCouponCode);

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@CouponCode", DbType.String, iCouponCode);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            var rtn = Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));

            return rtn;
        }

        #endregion

        #region 微信关注日志

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_WeixinAttentionLogDC"></param>
        /// <returns></returns>
        public bool User_WeixinAttentionLog_ADD(User_WeixinAttentionLogDC iUser_WeixinAttentionLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_WeixinAttentionLog(OpenID, AttentionTime, Source, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OpenID, @AttentionTime, @Source, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //第三方ID
            db.AddInParameter(cmd, "@OpenID", DbType.String, iUser_WeixinAttentionLogDC.OpenID);
            //关注时间
            db.AddInParameter(cmd, "@AttentionTime", DbType.DateTime, iUser_WeixinAttentionLogDC.AttentionTime);
            //来源
            db.AddInParameter(cmd, "@Source", DbType.String, iUser_WeixinAttentionLogDC.Source);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_WeixinAttentionLogDC);
            db.ExecuteScalar(cmd);
            return true;
        }

        /// <summary>
        /// 删除关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        public bool User_WeixinAttentionLog_DELETE(string iOpenID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_WeixinAttentionLog SET Obj_Status = 6, Obj_Remark = '取消关注', Obj_Mdate = getdate() WHERE OpenID = @OpenID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenID);

            db.ExecuteNonQuery(cmd);
            return true;
        }

        /// <summary>
        /// 获取最新关注来源
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        public string User_WeixinAttentionLog_SELECT_Source(string iOpenID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT TOP 1 Source FROM User_WeixinAttentionLog WHERE Obj_Status = 1 AND OpenID = @OpenID ORDER BY ID DESC");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return null;
        }

        /// <summary>
        /// 用户是否关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        public bool User_WeixinAttention_Check(string iOpenID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT COUNT(0) FROM User_WeixinAttentionLog WHERE Obj_Status = 1 AND OpenID = @OpenID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_AppInfoDC"></param>
        /// <returns></returns>
        public int User_AppInfo_ADD(User_AppInfoDC iUser_AppInfoDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_AppInfo(UserID, Type, Flag, Version, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @Type, @Flag, @Version, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUser_AppInfoDC.UserID);
            //手机类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_AppInfoDC.Type);
            //手机标识
            db.AddInParameter(cmd, "@Flag", DbType.String, iUser_AppInfoDC.Flag);
            //客户端版本
            db.AddInParameter(cmd, "@Version", DbType.String, iUser_AppInfoDC.Version);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_AppInfoDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <returns></returns>
        public bool User_LoginOut(Guid iUserID, int iMobileType)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE User_AppInfo SET Obj_Status = 6,Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND UserID = @UserID AND Type = @MobileType ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            db.AddInParameter(cmd, "@MobileType", DbType.Int32, iMobileType);

            db.ExecuteNonQuery(cmd);

            return false;
        }

        /// <summary>
        /// 微信关注统计
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <returns></returns>
        public IList<User_WeixinAttentionLogStatDC> User_WeixinAttentionLog_Stat(string iInternalKey, DateTime iStartDate, DateTime iEndDate)
        {
            var regSource = User_RegisterSource_SELECT_Entity_InternalKey(iInternalKey);
            if (regSource == null)
            {
                return null;
            }
            var rtnList = new Dictionary<DateTime, User_WeixinAttentionLogStatDC>();

            if ((iEndDate - iStartDate).Days > 32)
            {
                //查询周期太长
                return null;
            }

            var tmpStartDate = iStartDate;

            while (tmpStartDate <= iEndDate)
            {
                rtnList.Add(tmpStartDate.Date, new User_WeixinAttentionLogStatDC()
                {
                    StatDate = tmpStartDate.Date,
                    RegisterSourceType = regSource.Type,
                });
                tmpStartDate = tmpStartDate.AddDays(1);
            }

            //查询关注数
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT CONVERT(varchar(10),Obj_Cdate,120),COUNT(0) 
                FROM User_WeixinAttentionLog WHERE Source = @Source 
                AND Obj_Status IN(1,6)
                AND Obj_Cdate >= @StartDate
                AND Obj_Cdate < @EndDate
                GROUP BY CONVERT(varchar(10),Obj_Cdate,120)");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Source", DbType.String, iInternalKey);

            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, iStartDate.Date);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, iEndDate.Date);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var date = DateTime.Parse(reader.GetString(0));
                    if (rtnList.ContainsKey(date))
                    {
                        rtnList[date].AttentionCount = reader.GetInt32(1);
                    }
                }
            }

            //查询取消关注数
            sql.Clear();
            sql.Append(@"SELECT CONVERT(varchar(10),Obj_Mdate,120),COUNT(0) 
                FROM User_WeixinAttentionLog WHERE Source = @Source 
                AND Obj_Status = 6
                AND Obj_Mdate >= @StartDate
                AND Obj_Mdate < @EndDate
                GROUP BY CONVERT(varchar(10),Obj_Mdate,120)");

            cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Source", DbType.String, iInternalKey);

            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, iStartDate.Date);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, iEndDate.Date);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var date = DateTime.Parse(reader.GetString(0));
                    if (rtnList.ContainsKey(date))
                    {
                        rtnList[date].RemoveAttentionCount = reader.GetInt32(1);
                    }
                }
            }

            //查询注册数
            sql.Clear();
            sql.Append(@"SELECT CONVERT(varchar(10),Obj_Cdate,120),COUNT(0) 
                FROM User_RegisterLog WHERE SourceID = @Source 
                AND Obj_Status = 1
                AND Channel = 4
                AND Obj_Cdate >= @StartDate
                AND Obj_Cdate < @EndDate
                GROUP BY CONVERT(varchar(10),Obj_Cdate,120)");

            cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Source", DbType.String, iInternalKey);

            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, iStartDate.Date);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, iEndDate.Date);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var date = DateTime.Parse(reader.GetString(0));
                    if (rtnList.ContainsKey(date))
                    {
                        rtnList[date].RegisterCount = reader.GetInt32(1);
                    }
                }
            }

            return rtnList.Values.ToList();
        }

        #region User_RegisterSource

        /// <summary>
        /// 新增 User_RegisterSource
        /// </summary>
        /// <param name="iUser_RegisterSourceDC"></param>
        /// <returns></returns>
        public int User_RegisterSource_ADD(User_RegisterSourceDC iUser_RegisterSourceDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO User_RegisterSource(InternalKey, Type, SourceID, Content, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@InternalKey, @Type, @SourceID, @Content, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //内部ID
            db.AddInParameter(cmd, "@InternalKey", DbType.String, iUser_RegisterSourceDC.InternalKey);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_RegisterSourceDC.Type);
            //来源ID
            db.AddInParameter(cmd, "@SourceID", DbType.String, iUser_RegisterSourceDC.SourceID);
            //来源内容
            db.AddInParameter(cmd, "@Content", DbType.String, iUser_RegisterSourceDC.Content);

            //添加共通参数
            AddCommonInsertParameter(db, cmd, iUser_RegisterSourceDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <returns></returns>
        public bool User_RegisterSource_Exist_Check_InternalKey(string iInternalKey)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(0) FROM User_RegisterSource WHERE Obj_Status = 1 AND InternalKey = @InternalKey");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@InternalKey", DbType.String, iInternalKey);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSourceID"></param>
        /// <returns></returns>
        public bool User_RegisterSource_Exist_Check_SourceID(string iSourceID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(0) FROM User_RegisterSource WHERE Obj_Status = 1 AND SourceID = @SourceID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@SourceID", DbType.String, iSourceID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 更新User_RegisterSource
        /// </summary>
        /// <param name="iUser_RegisterSourceDC"></param>
        /// <returns></returns>
        public bool User_RegisterSource_UPDATE(User_RegisterSourceDC iUser_RegisterSourceDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_RegisterSource SET InternalKey = @InternalKey, Type = @Type, SourceID = @SourceID, Content = @Content,  ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iUser_RegisterSourceDC.ID);
            //内部ID
            db.AddInParameter(cmd, "@InternalKey", DbType.String, iUser_RegisterSourceDC.InternalKey);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iUser_RegisterSourceDC.Type);
            //来源ID
            db.AddInParameter(cmd, "@SourceID", DbType.String, iUser_RegisterSourceDC.SourceID);
            //来源内容
            db.AddInParameter(cmd, "@Content", DbType.String, iUser_RegisterSourceDC.Content);

            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iUser_RegisterSourceDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool User_RegisterSource_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE User_RegisterSource SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部User_RegisterSource
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<User_RegisterSourceDC> User_RegisterSource_SELECT_List(int? iType, int iPageIndex, int iPageSize)
        {
            List<User_RegisterSourceDC> list = new List<User_RegisterSourceDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");

            sqlorder.Append(@" InternalKey ");

            if (iType.HasValue)
                sqlwhere.Append(" AND Type = '" + iType + "' ");

            var rtn = new RecordCountEntity<User_RegisterSourceDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("User_RegisterSource", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("User_RegisterSource", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(User_RegisterSourceDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public User_RegisterSourceDC User_RegisterSource_SELECT_Entity(int iID)
        {
            User_RegisterSourceDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_RegisterSource WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_RegisterSourceDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public string User_RegisterSource_SELECT_InternalKey(string iSourceID)
        {
            if (string.IsNullOrWhiteSpace(iSourceID))
            {
                return iSourceID;
            }

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT InternalKey FROM User_RegisterSource WHERE SourceID = @SourceID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@SourceID", DbType.String, iSourceID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return iSourceID;
        }

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <returns></returns>
        public User_RegisterSourceDC User_RegisterSource_SELECT_Entity_InternalKey(string iInternalKey)
        {
            User_RegisterSourceDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM User_RegisterSource WHERE InternalKey = @InternalKey AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@InternalKey", DbType.String, iInternalKey);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_RegisterSourceDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        /// <summary>
        /// 操作员邀请码验证
        /// </summary>
        /// <param name="iNickName"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public bool PR_Operator_Exist(string iCode)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT COUNT(0) FROM PR_Operator WHERE Code = @Code AND Obj_Status = 1 AND Enable = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Code", DbType.String, iCode);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        public bool User_Info_UPDATE_Location(Guid iUserID, int? iNodeID, decimal iLatitude, decimal iLongitude)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE User_Info SET NodeID = @NodeID, Latitude = @Latitude,Longitude = @Longitude WHERE ID = @ID AND Longitude IS NULL AND Latitude IS NULL ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

            db.AddInParameter(cmd, "@Latitude", DbType.Decimal, iLatitude);

            db.AddInParameter(cmd, "@Longitude", DbType.Decimal, iLongitude);

            // return db.ExecuteNonQuery(cmd) > 0 ? true : false;

            db.ExecuteNonQuery(cmd);

            return true;
        }
    }
}
