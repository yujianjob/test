using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Express.Contract.DataContract;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.DAL
{
    public class ExpressDAL : DALBase, LazyAtHome.WCF.Express.Interface.IDAL.IExpressDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExpressDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        #region Exp_Node

        /// <summary>
        /// 新增 Exp_Node
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        public int Exp_Node_ADD(Exp_NodeDC iExp_NodeDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Node(Name, No, DistrictID, Type, Address, Latitude, Longitude, StorageID, ParentID, ManagerID, LinkManID, Radius, Keyword, CaptainID, Enable, AlarmType, AreaID, CreateOperatorID, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @No, @DistrictID, @Type, @Address, @Latitude, @Longitude, @StorageID, @ParentID, @ManagerID, @LinkManID, @Radius, @Keyword, @CaptainID, @Enable, @AlarmType, @AreaID, @CreateOperatorID, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //站点名
            db.AddInParameter(cmd, "@Name", DbType.String, iExp_NodeDC.Name);

            db.AddInParameter(cmd, "@No", DbType.String, iExp_NodeDC.No);
            //行政区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iExp_NodeDC.DistrictID);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_NodeDC.Type);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iExp_NodeDC.Address);
            //纬度
            db.AddInParameter(cmd, "@Latitude", DbType.Decimal, iExp_NodeDC.Latitude);
            //经度
            db.AddInParameter(cmd, "@Longitude", DbType.Decimal, iExp_NodeDC.Longitude);
            //仓库ID
            db.AddInParameter(cmd, "@StorageID", DbType.Decimal, iExp_NodeDC.StorageID);
            //上级站点ID
            db.AddInParameter(cmd, "@ParentID", DbType.Decimal, iExp_NodeDC.ParentID);
            //
            db.AddInParameter(cmd, "@ManagerID", DbType.Decimal, iExp_NodeDC.ManagerID);
            //
            db.AddInParameter(cmd, "@LinkManID", DbType.Decimal, iExp_NodeDC.LinkManID);
            //
            db.AddInParameter(cmd, "@Radius", DbType.Int32, iExp_NodeDC.Radius);

            db.AddInParameter(cmd, "@Keyword", DbType.String, iExp_NodeDC.Keyword);

            db.AddInParameter(cmd, "@CaptainID", DbType.Int32, iExp_NodeDC.CaptainID);

            db.AddInParameter(cmd, "@Enable", DbType.Int32, iExp_NodeDC.Enable);

            db.AddInParameter(cmd, "@AlarmType", DbType.Int32, iExp_NodeDC.AlarmType);

            db.AddInParameter(cmd, "@AreaID", DbType.Int32, iExp_NodeDC.AreaID);

            db.AddInParameter(cmd, "@CreateOperatorID", DbType.Int32, iExp_NodeDC.CreateOperatorID);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_NodeDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Node
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        public bool Exp_Node_UPDATE(Exp_NodeDC iExp_NodeDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Node SET Name = @Name, No = @No, DistrictID = @DistrictID, Type = @Type, Address = @Address, Latitude = @Latitude, Longitude = @Longitude, ParentID = @ParentID, ManagerID = @ManagerID, LinkManID = @LinkManID, Radius = @Radius, Keyword = @Keyword, CaptainID = @CaptainID, Enable = @Enable, AlarmType = @AlarmType, AreaID = @AreaID, CreateOperatorID = @CreateOperatorID, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_NodeDC.ID);

            db.AddInParameter(cmd, "@No", DbType.String, iExp_NodeDC.No);
            //站点名
            db.AddInParameter(cmd, "@Name", DbType.String, iExp_NodeDC.Name);
            //行政区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iExp_NodeDC.DistrictID);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_NodeDC.Type);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iExp_NodeDC.Address);
            //纬度
            db.AddInParameter(cmd, "@Latitude", DbType.Decimal, iExp_NodeDC.Latitude);
            //经度
            db.AddInParameter(cmd, "@Longitude", DbType.Decimal, iExp_NodeDC.Longitude);
            //仓库ID
            //db.AddInParameter(cmd, "@StorageID", DbType.Decimal, iExp_NodeDC.StorageID);
            //上级站点ID
            db.AddInParameter(cmd, "@ParentID", DbType.Decimal, iExp_NodeDC.ParentID);
            //
            db.AddInParameter(cmd, "@ManagerID", DbType.Decimal, iExp_NodeDC.ManagerID);
            //
            db.AddInParameter(cmd, "@LinkManID", DbType.Decimal, iExp_NodeDC.LinkManID);
            //
            db.AddInParameter(cmd, "@Radius", DbType.Int32, iExp_NodeDC.Radius);

            db.AddInParameter(cmd, "@Keyword", DbType.String, iExp_NodeDC.Keyword);

            db.AddInParameter(cmd, "@CaptainID", DbType.Int32, iExp_NodeDC.CaptainID);

            db.AddInParameter(cmd, "@Enable", DbType.Int32, iExp_NodeDC.Enable);

            db.AddInParameter(cmd, "@AlarmType", DbType.Int32, iExp_NodeDC.AlarmType);

            db.AddInParameter(cmd, "@AreaID", DbType.Int32, iExp_NodeDC.AreaID);

            db.AddInParameter(cmd, "@CreateOperatorID", DbType.Int32, iExp_NodeDC.CreateOperatorID);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_NodeDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Node
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Node_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Node SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Node
        /// </summary>
        /// <returns></returns>
        public IList<Exp_NodeDC> Exp_Node_SELECT_List()
        {
            List<Exp_NodeDC> list = new List<Exp_NodeDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Node WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_NodeDC.GetEntity(reader));
                }
            }
            return list;
        }

        public IList<Exp_NodeDC> Exp_Node_SELECT_List_Allocation()
        {
            List<Exp_NodeDC> list = new List<Exp_NodeDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Node WHERE Obj_Status = 1 AND Enable = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_NodeDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询Exp_Node
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Exp_NodeDC Exp_Node_SELECT_Entity(int iID)
        {
            Exp_NodeDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT Exp_Node.*, Manager.Name AS ManagerName, Captain.Name AS CaptainName, P.Name AS ParentName, P.No AS ParentNo, A.Name AS AreaName, CreateOperator.Name AS CreateOperatorName
FROM Exp_Node 
LEFT JOIN PR_Operator Manager ON Exp_Node.ManagerID = Manager.ID
LEFT JOIN PR_Operator Captain ON Exp_Node.CaptainID = Captain.ID
LEFT JOIN PR_Operator CreateOperator ON Exp_Node.CreateOperatorID = CreateOperator.ID
LEFT JOIN Exp_Node P ON Exp_Node.ParentID = P.ID
LEFT JOIN Exp_Area A ON Exp_Node.AreaID = A.ID
WHERE Exp_Node.ID = @ID AND Exp_Node.Obj_Status = 1
");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_NodeDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 查询站点Exp_Node
        /// </summary>
        /// <param name="iDistrictID"></param>
        /// <param name="iName"></param>
        /// <param name="iType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Exp_NodeDC> Exp_Node_SELECT_List(
            int? iAreaID, string iName,
            int? iType, int? iParentID,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Exp_NodeDC> list = new List<Exp_NodeDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Exp_Node.Obj_Status = 1 ");
            sqlorder.Append(@" Exp_Node.ID ");

            sqlfields.Append(" Exp_Node.*, Base_District.FullName AS DistrictName ");

            //
            if (iAreaID.HasValue)
                sqlwhere.Append(" AND Exp_Node.AreaID = '" + iAreaID + "' ");

            if (iType.HasValue)
                sqlwhere.Append(" AND Exp_Node.Type = '" + iType + "' ");

            if (iParentID.HasValue)
                sqlwhere.Append(" AND Exp_Node.ParentID = '" + iParentID + "' ");

            //
            if (!string.IsNullOrWhiteSpace(iName))
                sqlwhere.Append(" AND Exp_Node.Name LIKE '%" + DBHelper.SqlLikeReplaceForParam(iName) + "%' ");

            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Exp_Node.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Exp_Node.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Exp_NodeDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex(@"Exp_Node LEFT JOIN Base_District ON Exp_Node.DistrictID = Base_District.ID"
                , "Exp_Node.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex(@"Exp_Node LEFT JOIN Base_District ON Exp_Node.DistrictID = Base_District.ID"
                , "Exp_Node.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_NodeDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        #endregion

        //        #region Exp_Operator

        //        /// <summary>
        //        /// 新增 Exp_Operator
        //        /// </summary>
        //        /// <param name="iExp_OperatorDC"></param>
        //        /// <returns></returns>
        //        public int Exp_Operator_ADD(Exp_OperatorDC iExp_OperatorDC)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("INSERT INTO Exp_Operator(LoginName, LoginPwd, Type, NodeID, Name, MpNo, Enable, ");
        //            //字段拼接
        //            AddCommonInsertSql(sql);
        //            sql.Append(") VALUES (@LoginName, @LoginPwd, @Type, @NodeID, @Name, @MpNo, @Enable, ");
        //            //值拼接
        //            AddCommonInsertValues(sql);
        //            sql.Append(");SELECT @@IDENTITY;");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //登录名
        //            db.AddInParameter(cmd, "@LoginName", DbType.String, iExp_OperatorDC.LoginName);
        //            //登录密码
        //            db.AddInParameter(cmd, "@LoginPwd", DbType.String, LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iExp_OperatorDC.LoginPwd, LazyAtHome.Core.Enumerate.CryptoMode.MD5));
        //            //类型
        //            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_OperatorDC.Type);

        //            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iExp_OperatorDC.NodeID);
        //            //姓名
        //            db.AddInParameter(cmd, "@Name", DbType.String, iExp_OperatorDC.Name);
        //            //手机号
        //            db.AddInParameter(cmd, "@MpNo", DbType.String, iExp_OperatorDC.MpNo);
        //            //启用状态
        //            db.AddInParameter(cmd, "@Enable", DbType.Int32, 1);
        //            //添加共通参数
        //            AddCommonInsertParameter(db, cmd, iExp_OperatorDC);
        //            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
        //            return id;
        //        }

        //        /// <summary>
        //        /// 更新Exp_Operator
        //        /// </summary>
        //        /// <param name="iExp_OperatorDC"></param>
        //        /// <returns></returns>
        //        public bool Exp_Operator_UPDATE(Exp_OperatorDC iExp_OperatorDC)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("UPDATE Exp_Operator SET Type = @Type, NodeID = @NodeID, Name = @Name, MpNo = @MpNo, Enable = @Enable, ");
        //            //字段拼接
        //            AddCommonUpdateSql(sql);
        //            sql.Append(" WHERE ID = @ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //主键
        //            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_OperatorDC.ID);
        //            //类型
        //            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_OperatorDC.Type);

        //            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iExp_OperatorDC.NodeID);
        //            //姓名
        //            db.AddInParameter(cmd, "@Name", DbType.String, iExp_OperatorDC.Name);
        //            //手机号
        //            db.AddInParameter(cmd, "@MpNo", DbType.String, iExp_OperatorDC.MpNo);
        //            //启用状态
        //            db.AddInParameter(cmd, "@Enable", DbType.Int32, iExp_OperatorDC.Enable);
        //            //添加共通参数
        //            AddCommonUpdateParameter(db, cmd, iExp_OperatorDC);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>
        //        /// 删除Exp_Operator
        //        /// </summary>
        //        /// <param name="iID">主键</param>
        //        /// <param name="iMuser">操作人</param>
        //        /// <returns></returns>
        //        public bool Exp_Operator_DELETE(int iID, int iMuser)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("UPDATE Exp_Operator SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //主键
        //            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>
        //        /// 查询全部Exp_Operator
        //        /// </summary>
        //        /// <returns></returns>
        //        public IList<Exp_OperatorDC> Exp_Operator_SELECT_List()
        //        {
        //            List<Exp_OperatorDC> list = new List<Exp_OperatorDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append("SELECT * FROM Exp_Operator WHERE Obj_Status = 1");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(Exp_OperatorDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        //        /// <summary>
        //        /// 根据ID查询Exp_Operator
        //        /// </summary>
        //        /// <param name="iID">主键</param>
        //        /// <returns></returns>
        //        public Exp_OperatorDC Exp_Operator_SELECT_Entity(int iID)
        //        {
        //            Exp_OperatorDC entity = null;
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT Exp_Operator.*, Exp_Node.Name AS NodeName,Exp_Node.StorageID FROM Exp_Operator
        //                LEFT JOIN Exp_Node ON Exp_Operator.NodeID = Exp_Node.ID
        //                WHERE Exp_Operator.ID = @ID AND Exp_Operator.Obj_Status = 1");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //主键
        //            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                if (reader.Read())
        //                {
        //                    entity = Exp_OperatorDC.GetEntity(reader);
        //                }
        //            }
        //            return entity;
        //        }

        //        /// <summary>
        //        /// 查询全部
        //        /// </summary>
        //        /// <returns></returns>
        //        public RecordCountEntity<Exp_OperatorDC> Exp_Operator_SELECT_List(
        //            string iLoginName, string iName, string iMpNo,
        //            int? iType,
        //            string iNodeName,
        //            DateTime? iStartDate, DateTime? iEndDate,
        //            int iPageIndex, int iPageSize)
        //        {
        //            List<Exp_OperatorDC> list = new List<Exp_OperatorDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

        //            StringBuilder sqlwhere = new StringBuilder();
        //            StringBuilder sqlorder = new StringBuilder();
        //            StringBuilder sqlfields = new StringBuilder();
        //            sqlfields.Append(" Exp_Operator.*, Exp_Node.Name AS NodeName ");

        //            sqlwhere.Append(@" Exp_Operator.Obj_Status = 1 ");
        //            sqlwhere.Append(" AND Exp_Operator.NodeID = Exp_Node.ID ");

        //            sqlorder.Append(@" Exp_Operator.ID DESC ");

        //            if (!string.IsNullOrWhiteSpace(iLoginName))
        //                sqlwhere.Append(" AND Exp_Operator.LoginName LIKE '%" + DBHelper.SqlLikeReplaceForParam(iLoginName) + "%' ");

        //            if (!string.IsNullOrWhiteSpace(iName))
        //                sqlwhere.Append(" AND Exp_Operator.Name LIKE '%" + DBHelper.SqlLikeReplaceForParam(iName) + "%' ");

        //            if (!string.IsNullOrWhiteSpace(iMpNo))
        //                sqlwhere.Append(" AND Exp_Operator.MpNo LIKE '%" + DBHelper.SqlLikeReplaceForParam(iMpNo) + "%' ");

        //            if (iType.HasValue)
        //                sqlwhere.Append(" AND Exp_Operator.Type = '" + iType + "' ");

        //            if (!string.IsNullOrWhiteSpace(iNodeName))
        //                sqlwhere.Append(" AND Exp_Node.Name LIKE '%" + DBHelper.SqlLikeReplaceForParam(iNodeName) + "%' ");

        //            //创建时间开始
        //            if (iStartDate.HasValue)
        //                sqlwhere.Append(" AND Exp_Operator.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
        //            //创建时间结束
        //            if (iEndDate.HasValue)
        //                sqlwhere.Append(" AND Exp_Operator.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

        //            var rtn = new RecordCountEntity<Exp_OperatorDC>();

        //            //取得总数量
        //            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex(@" Exp_Operator,Exp_Node "
        //                , "Exp_Operator.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
        //            //取得列表
        //            using (IDataReader reader = ExecuteReaderPaginationByPageIndex(@" Exp_Operator,Exp_Node "
        //                , "Exp_Operator.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
        //            {
        //                while (reader.Read())
        //                {
        //                    var entity = Exp_OperatorDC.GetEntity(reader);

        //                    if (entity != null)
        //                    {
        //                        list.Add(entity);
        //                    }
        //                }
        //            }
        //            rtn.ReturnList = list;
        //            return rtn;
        //        }

        //        #endregion

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
            sql.Append("SELECT PR_Operator.*,Exp_Node.Name AS NodeName,Exp_Node.Type AS NodeType,Exp_Node.StorageID FROM PR_Operator,Exp_Node WHERE PR_Operator.NodeID = Exp_Node.ID AND PR_Operator.ID = @ID AND PR_Operator.Obj_Status = 1");
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
        /// <param name="iCode"></param>
        /// <returns></returns>
        public OperatorDC PR_Operator_SELECT_BYCode(string iCode)
        {
            OperatorDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PR_Operator.* FROM PR_Operator,Exp_Node WHERE PR_Operator.NodeID = Exp_Node.ID AND PR_Operator.Code = @Code AND PR_Operator.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@Code", DbType.String, iCode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = OperatorDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #region Exp_Order

        /// <summary>
        /// 新增 Exp_Order
        /// </summary>
        /// <param name="iExp_OrderDC"></param>
        /// <returns></returns>
        public int Exp_Order_ADD(Exp_OrderDC iExp_OrderDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Order(ExpNumber, OutNumber, TransportType, DistrictID, Address, Contacts, Mpno, ExpTime, OperatorID, NodeID, OperatorTime, CompleteTime, PackageInfo, PackageCount, ChargeFee, Step, StepRemark, UserRemark, CSRemark, SystemRemark, InviteCode, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@ExpNumber, @OutNumber, @TransportType, @DistrictID, @Address, @Contacts, @Mpno, @ExpTime, @OperatorID, @NodeID, @OperatorTime, @CompleteTime, @PackageInfo, @PackageCount, @ChargeFee, @Step, @StepRemark, @UserRemark, @CSRemark, @SystemRemark, @InviteCode, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //物流单号
            db.AddInParameter(cmd, "@ExpNumber", DbType.String, iExp_OrderDC.ExpNumber);
            //外部单号
            db.AddInParameter(cmd, "@OutNumber", DbType.String, iExp_OrderDC.OutNumber);
            //运输类型
            db.AddInParameter(cmd, "@TransportType", DbType.Int32, iExp_OrderDC.TransportType);
            //行政区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iExp_OrderDC.DistrictID);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iExp_OrderDC.Address);
            //联系人
            db.AddInParameter(cmd, "@Contacts", DbType.String, iExp_OrderDC.Contacts);
            //联系手机
            db.AddInParameter(cmd, "@Mpno", DbType.String, iExp_OrderDC.Mpno);
            //预计时间
            db.AddInParameter(cmd, "@ExpTime", DbType.DateTime, iExp_OrderDC.ExpTime);
            //揽件人
            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iExp_OrderDC.OperatorID);
            //站点ID
            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iExp_OrderDC.NodeID);
            //揽件时间
            db.AddInParameter(cmd, "@OperatorTime", DbType.DateTime, iExp_OrderDC.OperatorTime);
            //完成时间
            db.AddInParameter(cmd, "@CompleteTime", DbType.DateTime, iExp_OrderDC.CompleteTime);
            //货物内容
            db.AddInParameter(cmd, "@PackageInfo", DbType.String, iExp_OrderDC.PackageInfo);
            //货物数量
            db.AddInParameter(cmd, "@PackageCount", DbType.Int32, iExp_OrderDC.PackageCount);
            //代收货款
            db.AddInParameter(cmd, "@ChargeFee", DbType.Decimal, iExp_OrderDC.ChargeFee);
            //步骤
            db.AddInParameter(cmd, "@Step", DbType.Int32, iExp_OrderDC.Step);
            //步骤说明
            db.AddInParameter(cmd, "@StepRemark", DbType.String, iExp_OrderDC.StepRemark);
            //用户备注
            db.AddInParameter(cmd, "@UserRemark", DbType.String, iExp_OrderDC.UserRemark);
            //客服备注
            db.AddInParameter(cmd, "@CSRemark", DbType.String, iExp_OrderDC.CSRemark);
            //系统备注
            db.AddInParameter(cmd, "@SystemRemark", DbType.String, iExp_OrderDC.SystemRemark);
            //
            db.AddInParameter(cmd, "@InviteCode", DbType.String, iExp_OrderDC.InviteCode);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_OrderDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Order
        /// </summary>
        /// <param name="iExp_OrderDC"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE(Exp_OrderDC iExp_OrderDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Exp_Order SET DistrictID = @DistrictID, Address = @Address, Contacts = @Contacts, 
                Mpno = @Mpno, ExpTime = @ExpTime, PackageInfo = @PackageInfo, PackageCount = @PackageCount, 
                ChargeFee = @ChargeFee, UserRemark = @UserRemark, CSRemark = @CSRemark, SystemRemark = @SystemRemark, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_OrderDC.ID);
            //行政区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iExp_OrderDC.DistrictID);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iExp_OrderDC.Address);
            //联系人
            db.AddInParameter(cmd, "@Contacts", DbType.String, iExp_OrderDC.Contacts);
            //联系手机
            db.AddInParameter(cmd, "@Mpno", DbType.String, iExp_OrderDC.Mpno);
            //预计时间
            db.AddInParameter(cmd, "@ExpTime", DbType.DateTime, iExp_OrderDC.ExpTime);
            //货物内容
            db.AddInParameter(cmd, "@PackageInfo", DbType.String, iExp_OrderDC.PackageInfo);
            //货物数量
            db.AddInParameter(cmd, "@PackageCount", DbType.Int32, iExp_OrderDC.PackageCount);
            //代收货款
            db.AddInParameter(cmd, "@ChargeFee", DbType.Decimal, iExp_OrderDC.ChargeFee);
            //用户备注
            db.AddInParameter(cmd, "@UserRemark", DbType.String, iExp_OrderDC.UserRemark);
            //客服备注
            db.AddInParameter(cmd, "@CSRemark", DbType.String, iExp_OrderDC.CSRemark);
            //系统备注
            db.AddInParameter(cmd, "@SystemRemark", DbType.String, iExp_OrderDC.SystemRemark);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_OrderDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新地址
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iTransportType"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iContacts"></param>
        /// <param name="iMpno"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_Address(string iOutNumber, int iTransportType,
              int iDistrictID, string iAddress, string iContacts, string iMpno)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Exp_Order SET DistrictID = @DistrictID, Address = @Address, Contacts = @Contacts, 
                Mpno = @Mpno WHERE OutNumber = @OutNumber AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@TransportType", DbType.Int32, iTransportType);
            //行政区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iDistrictID);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iAddress);
            //联系人
            db.AddInParameter(cmd, "@Contacts", DbType.String, iContacts);
            //联系手机
            db.AddInParameter(cmd, "@Mpno", DbType.String, iMpno);
            ////系统备注
            //db.AddInParameter(cmd, "@SystemRemark", DbType.String, "修改地址");

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Order
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Order_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Order SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Order
        /// </summary>
        /// <returns></returns>
        public IList<Exp_OrderDC> Exp_Order_SELECT_List()
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Order WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_OrderDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询Exp_Order
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Exp_OrderDC Exp_Order_SELECT_Entity(int iID)
        {
            Exp_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Exp_Order.*, Base_District.FullName AS DistrictName, PR_Operator.Name AS OperatorName, Exp_Node.No AS NodeNo, Exp_Node.Name AS NodeName, Exp_Node.Address AS NodeAddress
                FROM Exp_Order
                LEFT JOIN Base_District ON Exp_Order.DistrictID = Base_District.ID
                LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID
                LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID

                WHERE Exp_Order.ID = @ID AND Exp_Order.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_OrderDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        public bool Exp_Order_Exists_ExpNumber(string iExpNumber)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT COUNT(0) FROM Exp_Order WHERE ExpNumber = @ExpNumber AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ExpNumber", DbType.String, iExpNumber);

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
        //12小时内扫码检验
        public bool Exp_Order_Exists_ExpNumberIn12(string iExpNumber)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT COUNT(0) FROM Exp_Order WHERE ExpNumber = @ExpNumber AND Obj_Status = 1
AND DATEDIFF(hh,Obj_Cdate,GETDATE())<12");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ExpNumber", DbType.String, iExpNumber);

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
        /// 根据OutNumber查询Exp_Order
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iTransportType"></param>
        /// <returns></returns>
        public Exp_OrderDC Exp_Order_SELECT_Entity(string iOutNumber, int iTransportType)
        {
            Exp_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Exp_Order.*, Base_District.FullName AS DistrictName, PR_Operator.Name AS OperatorName, Exp_Node.No AS NodeNo, Exp_Node.Name AS NodeName, Exp_Node.Address AS NodeAddress
                FROM Exp_Order
                LEFT JOIN Base_District ON Exp_Order.DistrictID = Base_District.ID
                LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID
                LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID

                WHERE Exp_Order.OutNumber = @OutNumber AND Exp_Order.TransportType = @TransportType
                AND Exp_Order.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@TransportType", DbType.Int32, iTransportType);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_OrderDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Exp_OrderDC> Exp_Order_SELECT_List(
            string iExpNumber, string iOutNumber,
            int? iTransportType,
            string iAddress, string iContacts, string iMpno,
            int? iStep,
            string iKeyword,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Exp_Order.Obj_Status = 1 ");
            sqlorder.Append(@" Exp_Order.ExpTime DESC ");

            sqlfields.Append("Exp_Order.*, PR_Operator.Name AS OperatorName, Exp_Node.Name AS NodeName");

            //物流单号
            if (!string.IsNullOrWhiteSpace(iExpNumber))
                sqlwhere.Append(" AND Exp_Order.ExpNumber = '" + iExpNumber + "' ");

            //物流单号
            if (!string.IsNullOrWhiteSpace(iOutNumber))
                sqlwhere.Append(" AND Exp_Order.OutNumber = '" + iOutNumber + "' ");

            //运输类型
            if (iTransportType.HasValue)
                sqlwhere.Append(" AND Exp_Order.TransportType = '" + iTransportType + "' ");

            if (iStep.HasValue)
                sqlwhere.Append(" AND Exp_Order.Step = '" + iStep + "' ");

            //用户地址
            if (!string.IsNullOrWhiteSpace(iAddress))
                sqlwhere.Append(" AND Exp_Order.Address LIKE '%" + DBHelper.SqlLikeReplaceForParam(iAddress) + "%' ");
            //用户联系人
            if (!string.IsNullOrWhiteSpace(iContacts))
                sqlwhere.Append(" AND Exp_Order.Contacts LIKE '%" + DBHelper.SqlLikeReplaceForParam(iContacts) + "%' ");
            //用户手机
            if (!string.IsNullOrWhiteSpace(iMpno))
                sqlwhere.Append(" AND Exp_Order.Mpno LIKE '%" + DBHelper.SqlLikeReplaceForParam(iMpno) + "%' ");

            if (!string.IsNullOrWhiteSpace(iKeyword))
                sqlwhere.Append(@"
                    AND (PR_Operator.MpNo LIKE '%" + DBHelper.SqlLikeReplaceForParam(iKeyword) + @"%' 
                        OR PR_Operator.Name LIKE '%" + DBHelper.SqlLikeReplaceForParam(iKeyword) + @"%' 
                    )");

            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Exp_Order.Obj_Mdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Exp_Order.Obj_Mdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Exp_OrderDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex(@"Exp_Order LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex(@"Exp_Order LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_OrderDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Exp_OrderDC> Exp_Order_SELECT_List_UnAllocation(
            string iExpNumber, string iOutNumber,
            int? iTransportType,
            string iAddress, string iContacts, string iMpno,
            int? iStep,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Exp_Order.Obj_Status = 1 ");
            sqlorder.Append(@" Exp_Order.ExpTime ");

            sqlfields.Append("Exp_Order.*, Base_District.FullName AS DistrictName, PR_Operator.Name AS OperatorName, Exp_Node.Name AS NodeName");

            sqlwhere.Append(@" AND Exp_Order.NodeID IS NULL AND Exp_Order.Step = 0 ");

            sqlwhere.Append(@" AND (DATEDIFF(MINUTE,Exp_Order.Obj_Cdate,GETDATE()) > 5  OR AllotStatus = 2)");

            //物流单号
            if (!string.IsNullOrWhiteSpace(iExpNumber))
                sqlwhere.Append(" AND Exp_Order.ExpNumber = '" + iExpNumber + "' ");

            //物流单号
            if (!string.IsNullOrWhiteSpace(iOutNumber))
                sqlwhere.Append(" AND Exp_Order.OutNumber = '" + iOutNumber + "' ");

            //运输类型
            if (iTransportType.HasValue)
                sqlwhere.Append(" AND Exp_Order.TransportType = '" + iTransportType + "' ");

            if (iStep.HasValue)
                sqlwhere.Append(" AND Exp_Order.Step = '" + iStep + "' ");

            //用户地址
            if (!string.IsNullOrWhiteSpace(iAddress))
                sqlwhere.Append(" AND Exp_Order.Address LIKE '%" + DBHelper.SqlLikeReplaceForParam(iAddress) + "%' ");
            //用户联系人
            if (!string.IsNullOrWhiteSpace(iContacts))
                sqlwhere.Append(" AND Exp_Order.Contacts LIKE '%" + DBHelper.SqlLikeReplaceForParam(iContacts) + "%' ");
            //用户手机
            if (!string.IsNullOrWhiteSpace(iMpno))
                sqlwhere.Append(" AND Exp_Order.Mpno LIKE '%" + DBHelper.SqlLikeReplaceForParam(iMpno) + "%' ");

            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Exp_Order.Obj_Mdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Exp_Order.Obj_Mdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Exp_OrderDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex(@"Exp_Order LEFT JOIN Base_District ON Exp_Order.DistrictID = Base_District.ID LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex(@"Exp_Order LEFT JOIN Base_District ON Exp_Order.DistrictID = Base_District.ID LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_OrderDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        #endregion


        //        /// <summary>
        //        /// 手动分配
        //        /// </summary>
        //        /// <param name="iOrderID"></param>
        //        /// <param name="iOperatorID"></param>
        //        /// <param name="iNodeID"></param>
        //        /// <param name="iMuser"></param>
        //        /// <returns></returns>
        //        public bool Exp_Order_Allocation(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser)
        //        {
        //            var order = Exp_Order_SELECT_Entity(iOrderID);
        //            if (order == null)
        //            {
        //                return false;
        //            }

        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

        //            if (iNodeID.HasValue)
        //            {
        //                if (order.TransportType == 1)
        //                {
        //                    sql.Append(@"
        //                    UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE OrderID = @ID AND Obj_Status = 1
        //                    UPDATE Exp_Order SET OperatorID = @OperatorID, NodeID = @NodeID, Step = 1, AllotStatus = 1, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND Step = 0 AND Obj_Status = 1");
        //                }
        //                else
        //                {
        //                    sql.Append(@"
        //                    UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE OrderID = @ID AND Obj_Status = 1
        //                    UPDATE Exp_Order SET OperatorID = @OperatorID, NodeID = @NodeID, AllotStatus = 1, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND Step = 0 AND Obj_Status = 1");
        //                }
        //            }
        //            else
        //            {
        //                sql.Append(@"
        //                UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE OrderID = @ID AND Obj_Status = 1
        //                UPDATE Exp_Order SET OperatorID = @OperatorID, NodeID = @NodeID, Step = 0, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND Obj_Status = 1");
        //            }

        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //主键
        //            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

        //            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

        //            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);
        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);

        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>
        //        /// 强制分配
        //        /// </summary>
        //        /// <param name="iOrderID"></param>
        //        /// <param name="iOperatorID"></param>
        //        /// <param name="iNodeID"></param>
        //        /// <param name="iMuser"></param>
        //        /// <returns></returns>
        //        public bool Exp_Order_Allocation_Forced(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser)
        //        {
        //            var order = Exp_Order_SELECT_Entity(iOrderID);
        //            if (order == null)
        //            {
        //                return false;
        //            }

        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append(@"
        //                UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE OrderID = @ID AND Obj_Status = 1
        //                UPDATE Exp_Order SET OperatorID = @OperatorID, NodeID = @NodeID, Step = @Step, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND Step IN (0,1) AND Obj_Status = 1");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //主键
        //            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

        //            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

        //            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

        //            if (iNodeID.HasValue && order.TransportType == 1)
        //            {
        //                db.AddInParameter(cmd, "@Step", DbType.Int32, 1);
        //            }
        //            else
        //            {
        //                db.AddInParameter(cmd, "@Step", DbType.Int32, 0);
        //            }

        //            //操作人
        //            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStep"></param>
        /// <param name="iStepRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Exp_Order_SetStep(int iOrderID, int iStep, string iStepRemark, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Exp_Order 
                SET Step = @Step, StepRemark = @StepRemark,
                Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@Step", DbType.Int32, iStep);

            db.AddInParameter(cmd, "@StepRemark", DbType.String, iStepRemark);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新物流单号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_ExpNumber(int iOrderID, string iExpNumber)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order SET ExpNumber = @ExpNumber, OperatorTime = GETDATE() WHERE Obj_Status = 1 AND ID = @OrderID 
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@ExpNumber", DbType.String, iExpNumber);

            //添加共通参数
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新订单收费金额
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
                UPDATE Exp_Order SET ChargeFee = @ChargeFee WHERE Obj_Status = 1 AND OutNumber = @OutNumber
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@ChargeFee", DbType.Decimal, iChargeFee);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        /// <summary>
        /// 更新订单收费金额
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee, string iPackageInfo, int iPackageCount)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
                UPDATE Exp_Order SET ChargeFee = @ChargeFee, PackageInfo = @PackageInfo, PackageCount = @PackageCount WHERE Obj_Status = 1 AND OutNumber = @OutNumber
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@PackageInfo", DbType.String, iPackageInfo);

            db.AddInParameter(cmd, "@PackageCount", DbType.Int32, iPackageCount);

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@ChargeFee", DbType.Decimal, iChargeFee);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 物流订单更新成完成
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_Get_Finish(string iOutNumber, string iExpNumber)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order SET ExpNumber = @ExpNumber, Step = 90,StepRemark = '订单系统推送', OperatorTime = GETDATE() WHERE Obj_Status = 1 AND Step != 90 AND OutNumber = @OutNumber AND TransportType = 1
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@ExpNumber", DbType.String, iExpNumber);

            //添加共通参数
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #region RoutePush

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Exp_RoutePush_ADD(Exp_RoutePushDC iExp_RoutePushDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_RoutePush(ExpNumber, OutNumber, OpCode, RouteInfo, Remark ");
            sql.Append(") VALUES (@ExpNumber, @OutNumber, @OpCode, @RouteInfo, @Remark ");
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ExpNumber", DbType.String, iExp_RoutePushDC.ExpNumber);

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iExp_RoutePushDC.OutNumber);

            db.AddInParameter(cmd, "@OpCode", DbType.String, iExp_RoutePushDC.OpCode);

            db.AddInParameter(cmd, "@RouteInfo", DbType.String, iExp_RoutePushDC.RouteInfo);

            db.AddInParameter(cmd, "@Remark", DbType.String, iExp_RoutePushDC.Remark);

            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Exp_RoutePushDC> Exp_RoutePush_SELECT_List()
        {
            List<Exp_RoutePushDC> list = new List<Exp_RoutePushDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT TOP 10 * FROM Exp_RoutePush WHERE Obj_Status = 1 AND PushType = 1 AND PushStatus = 0");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var entity = new Exp_RoutePushDC();

                    entity.ID = Convert.ToInt32(reader["ID"].ToString());
                    entity.ExpNumber = reader["ExpNumber"].ToString();
                    entity.OutNumber = reader["OutNumber"].ToString();
                    entity.OpCode = reader["OpCode"].ToString();
                    entity.RouteInfo = reader["RouteInfo"].ToString();
                    entity.OpCode = reader["OpCode"].ToString();
                    entity.Remark = reader["Remark"].ToString();
                    entity.Obj_Cdate = Convert.ToDateTime(reader["Obj_Cdate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                    list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRoutePushIDList"></param>
        /// <returns></returns>
        public bool Exp_RoutePush_UPDATE_PushStatus(int[] iRoutePushIDList)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
                UPDATE Exp_RoutePush SET PushStatus = 1, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 
                AND ID IN(" + string.Join(",", iRoutePushIDList) + ")");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public bool Exp_Order_Get_ExpectTime_Change(int iOrderID, DateTime iExpectTime)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND OrderID = @ID
UPDATE Exp_Order SET ExpTime = @ExpectTime,OperatorID = NULL,AllotStatus = 0,AllotTime = NULL,Step = 0 WHERE TransportType = 1 AND ID = @ID AND Step IN (0,1) AND Obj_Status = 1
");


            //            sql.Append(@"UPDATE Exp_Order SET ExpTime = @ExpectTime WHERE TransportType = 1 
            //                AND OutNumber = @OutNumber
            //                AND Step IN (0,1)
            //                AND Obj_Status = 1; ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@ExpectTime", DbType.DateTime, iExpectTime);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 取消物流单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public bool Exp_Order_Cancel(string iOutNumber)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Exp_Order SET Step = -2 WHERE OutNumber = @OutNumber AND Step IN(0,1) ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询未分配订单
        /// </summary>
        /// <returns></returns>
        public IList<Exp_OrderDC> Exp_Order_SELECT_List_UnAllocation()
        {
            IList<Exp_OrderDC> list = new List<Exp_OrderDC>();

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append(@" SELECT TOP 100 * FROM Exp_Order WHERE Obj_Status = 1 AND DATEDIFF(MINUTE,GETDATE(),ExpTime) <= 60 AND TransportType = 1 AND AllotStatus = 0 AND Step = 0 ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var entity = Exp_OrderDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 更新经纬度
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_Location(int iOrderID, decimal iLatitude, decimal iLongitude)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Exp_Order SET Latitude = @Latitude,Longitude = @Longitude WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@Latitude", DbType.Decimal, iLatitude);

            db.AddInParameter(cmd, "@Longitude", DbType.Decimal, iLongitude);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        //        /// <summary>
        //        /// 分配成功
        //        /// </summary>
        //        /// <param name="iOrderID"></param>
        //        /// <param name="iNodeID"></param>
        //        /// <returns></returns>
        //        public bool Exp_Order_UPDATE_Allocation(int iOrderID, int iNodeID)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

        //            sql.Append(
        //@"UPDATE Exp_Order SET AllotStatus = 1,AllotTime = GETDATE() WHERE ID = @ID AND Step = 0 AND AllotStatus = 0 AND Obj_Status = 1
        //IF(@@ROWCOUNT = 1)
        //BEGIN
        //UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE Obj_Status = 1 AND OrderID = @ID
        //INSERT INTO Exp_Order_Operator_Temp(OrderID,OperatorID) SELECT @ID, ID FROM PR_Operator WHERE NodeID = @NodeID AND Enable = 1 AND Obj_Status = 1
        //END
        //");


        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

        //            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

        //            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        /// <summary>
        /// 自动分配到站点下所有保安
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <returns></returns>
        public bool Exp_Order_Allocation_Success(int iOrderID, int iNodeID, string iNodeCode, string iAddress, int iAlarmType)
        {
            var hasOnDuty = false;

            //不预警
            if (iAlarmType == 0)
            {
                hasOnDuty = true;
            }
            else
            {
                hasOnDuty = Exp_Node_OnDutyCount(iNodeID) > 0;
            }

            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            if (hasOnDuty)
            {
                //2014-12-16 guominjie 分配到所有人中,推送到在岗的
                sql.Append(
@"
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE Obj_Status = 1 AND OrderID = @ID
INSERT INTO Exp_Order_Operator_Temp(OrderID,OperatorID) 
    SELECT @ID,ID FROM PR_Operator WHERE NodeID = @NodeID AND Enable = 1 AND Obj_Status = 1 --AND OnDuty = 1 
UPDATE Exp_Order SET AllotStatus = 1,AllotTime = GETDATE(),Alarm = 0,StepRemark = NULL,WaitProcess = 0,TakeTime = NULL,NodeID = @NodeID WHERE ID = @ID AND Step = 0 AND AllotStatus = 0 AND Obj_Status = 1
INSERT INTO Base_Push(Title,Content,Tag,Type) VALUES ('抢单',@Address + '产生一笔订单',@NodeCode,2)
");
            }
            else
            {
                sql.Append(
@"
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE Obj_Status = 1 AND OrderID = @ID
UPDATE Exp_Order SET AllotStatus = 2,AllotTime = GETDATE(),Alarm = 1,NodeID = @NodeID WHERE ID = @ID AND Step = 0 AND AllotStatus = 0 AND Obj_Status = 1
");
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

            db.AddInParameter(cmd, "@NodeCode", DbType.String, iNodeCode);

            db.AddInParameter(cmd, "@Address", DbType.String, iAddress);

            db.ExecuteNonQuery(cmd);

            return hasOnDuty;
        }

        /// <summary>
        /// 直接到站点下保安
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <returns></returns>
        public bool Exp_Order_Allocation_ToOperator(int iOrderID, string iOutNumber,
            int iNodeID, int iOperatorID, string iAddress)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(
@"
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE Obj_Status = 1 AND OrderID = @ID
UPDATE Exp_Order SET AllotStatus = 1,AllotTime = GETDATE(), NodeID = @NodeID, OperatorID = @OperatorID, Step = 1,Alarm = 0,StepRemark = NULL,WaitProcess = 0,TakeTime = GETDATE() WHERE OutNumber = @OutNumber AND Step IN(0,1) AND Obj_Status = 1 AND TransportType = 1
UPDATE Exp_Order SET AllotStatus = 1,AllotTime = GETDATE(), NodeID = @NodeID, OperatorID = @OperatorID,Alarm = 0,StepRemark = NULL,WaitProcess = 0 WHERE OutNumber = @OutNumber AND Step IN(0,1) AND Obj_Status = 1 AND TransportType = 2
INSERT INTO Base_Push(Title,Content,Tag,Type) VALUES ('分配','强制分配:' + @Address,@Tag,2)
");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            db.AddInParameter(cmd, "@Address", DbType.String, iAddress);

            db.AddInParameter(cmd, "@Tag", DbType.String, "OP" + iOperatorID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 订单清空重新分配
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public bool Exp_Order_Clear(int iOrderID, string iOutNumber)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND OrderID = @ID
UPDATE Order_Order SET InviteCode = NULL WHERE OrderNumber = @OutNumber AND Obj_Status = 1
UPDATE Exp_Order SET OperatorID = NULL,AllotStatus = 0,AllotTime = NULL,Step = 0,InviteCode = NULL,Alarm = 0,StepRemark = NULL,WaitProcess = 0,TakeTime = NULL WHERE OutNumber = @OutNumber AND Step IN (0,1) AND Obj_Status = 1
");

            //            sql.Append(@"UPDATE Exp_Order SET ExpTime = @ExpectTime WHERE TransportType = 1 
            //                AND OutNumber = @OutNumber
            //                AND Step IN (0,1)
            //                AND Obj_Status = 1; ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新客服备注
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iCSRemark"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_CSRemark(int iOrderID, string iCSRemark)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Exp_Order SET CSRemark = @CSRemark WHERE ID = @ID AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@CSRemark", DbType.String, iCSRemark);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 站点在职数量
        /// </summary>
        /// <param name="iNodeID"></param>
        /// <returns></returns>
        public int Exp_Node_OnDutyCount(int iNodeID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"SELECT COUNT(0) FROM PR_Operator WHERE NodeID = @NodeID AND OnDuty = 1 AND Enable = 1 AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }

        /// <summary>
        /// 分配失败
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iAllotStatus"></param>
        /// <returns></returns>
        public bool Exp_Order_Allocation_Fail(string iOutNumber)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Exp_Order SET AllotStatus = 2,AllotTime = GETDATE() WHERE OutNumber = @OutNumber AND Step = 0 AND AllotStatus = 0 AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 物流站点切换
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public bool Exp_Order_NodeChange(string iOutNumber, int iNodeID, int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(
@"UPDATE Exp_Order SET NodeID = @NodeID,OperatorID = @OperatorID WHERE OutNumber = @OutNumber AND TransportType = 1 AND Obj_Status = 1 AND Step IN (0,1,2)
IF(@@ROWCOUNT = 1)
BEGIN
    UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE Obj_Status = 1 AND OrderID = (SELECT TOP 1 ID FROM Exp_Order WHERE OutNumber = @OutNumber AND TransportType = 1 AND Obj_Status = 1)
END
UPDATE Exp_Order SET NodeID = @NodeID WHERE OutNumber = @OutNumber AND TransportType = 2 AND Obj_Status = 1 AND Step IN (0,1,2)
");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 物流站点切换
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iTransportType"></param>
        /// <returns></returns>
        public bool Exp_Order_NodeChange(string iOutNumber, int iNodeID, int iOperatorID, int iTransportType)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(
@"UPDATE Exp_Order SET NodeID = @NodeID,OperatorID = @OperatorID WHERE OutNumber = @OutNumber AND TransportType = @TransportType AND Obj_Status = 1 AND Step IN (0,1,2)
IF(@@ROWCOUNT = 1)
BEGIN
    UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6 WHERE Obj_Status = 1 AND OrderID = (SELECT TOP 1 ID FROM Exp_Order WHERE OutNumber = @OutNumber AND TransportType = @TransportType AND Obj_Status = 1)
END
");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);

            db.AddInParameter(cmd, "@TransportType", DbType.Int32, iTransportType);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #region Exp_Storage

        /// <summary>
        /// 新增 Exp_Storage
        /// </summary>
        /// <param name="iExp_StorageDC"></param>
        /// <returns></returns>
        public int Exp_Storage_ADD(Exp_StorageDC iExp_StorageDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Storage(Name, Type, ManagerID, LinkManID, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @Type, @ManagerID, @LinkManID, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //仓库名
            db.AddInParameter(cmd, "@Name", DbType.String, iExp_StorageDC.Name);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_StorageDC.Type);
            //负责人ID
            db.AddInParameter(cmd, "@ManagerID", DbType.Int32, iExp_StorageDC.ManagerID);
            //联系人ID
            db.AddInParameter(cmd, "@LinkManID", DbType.Int32, iExp_StorageDC.LinkManID);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_StorageDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Storage
        /// </summary>
        /// <param name="iExp_StorageDC"></param>
        /// <returns></returns>
        public bool Exp_Storage_UPDATE(Exp_StorageDC iExp_StorageDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Storage SET Name = @Name, Type = @Type, ManagerID = @ManagerID, LinkManID = @LinkManID, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_StorageDC.ID);
            //仓库名
            db.AddInParameter(cmd, "@Name", DbType.String, iExp_StorageDC.Name);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_StorageDC.Type);
            //负责人ID
            db.AddInParameter(cmd, "@ManagerID", DbType.Int32, iExp_StorageDC.ManagerID);
            //联系人ID
            db.AddInParameter(cmd, "@LinkManID", DbType.Int32, iExp_StorageDC.LinkManID);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_StorageDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Storage
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Storage_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Storage SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Storage
        /// </summary>
        /// <returns></returns>
        public IList<Exp_StorageDC> Exp_Storage_SELECT_List()
        {
            List<Exp_StorageDC> list = new List<Exp_StorageDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Storage WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_StorageDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 仓库搜索
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iName"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Exp_StorageDC> Exp_Storage_SELECT_List(
            int? iType, string iName,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Exp_StorageDC> list = new List<Exp_StorageDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();
            sqlfields.Append(" Exp_Storage.*,A.Name AS ManagerName,B.Name AS LinkManName ");

            sqlorder.Append(@" Exp_Storage.ID ");

            sqlwhere.Append(" Exp_Storage.Obj_Status = 1 ");
            sqlwhere.Append(" AND Exp_Storage.ManagerID = A.ID ");
            sqlwhere.Append(" AND Exp_Storage.LinkManID = B.ID ");

            if (iType.HasValue)
                sqlwhere.Append(" AND Exp_Storage.Type = '" + iType + "' ");

            if (!string.IsNullOrWhiteSpace(iName))
                sqlwhere.Append(" AND Exp_Storage.Name LIKE '%" + DBHelper.SqlLikeReplaceForParam(iName) + "%' ");

            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Exp_Storage.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Exp_Storage.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Exp_StorageDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Exp_Storage,PR_Operator A,PR_Operator B", "Exp_Storage.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Exp_Storage,PR_Operator A,PR_Operator B", "Exp_Storage.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_StorageDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Storage
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Exp_StorageDC Exp_Storage_SELECT_Entity(int iID)
        {
            Exp_StorageDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Storage WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_StorageDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion
        #region Exp_StorageItem

        /// <summary>
        /// 新增 Exp_StorageItem
        /// </summary>
        /// <param name="iExp_StorageItemDC"></param>
        /// <returns></returns>
        public int Exp_StorageItem_ADD(Exp_StorageItemDC iExp_StorageItemDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_StorageItem(Number, OtherNumber, StorageID, ItemType, ItemName, ItemDetail, TargetStorage, Status, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Number, @OtherNumber, @StorageID, @ItemType, @ItemName, @ItemDetail, @TargetStorage, @Status, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //编号
            db.AddInParameter(cmd, "@Number", DbType.String, iExp_StorageItemDC.Number);
            //附属编号
            db.AddInParameter(cmd, "@OtherNumber", DbType.String, iExp_StorageItemDC.OtherNumber);
            //仓库ID
            db.AddInParameter(cmd, "@StorageID", DbType.Int32, iExp_StorageItemDC.StorageID);
            //物品类型
            db.AddInParameter(cmd, "@ItemType", DbType.Int32, iExp_StorageItemDC.ItemType);
            //物品名称
            db.AddInParameter(cmd, "@ItemName", DbType.String, iExp_StorageItemDC.ItemName);
            //物品详情
            db.AddInParameter(cmd, "@ItemDetail", DbType.String, iExp_StorageItemDC.ItemDetail);
            //目标库
            db.AddInParameter(cmd, "@TargetStorage", DbType.Int32, iExp_StorageItemDC.TargetType);
            //状态
            db.AddInParameter(cmd, "@Status", DbType.Int32, iExp_StorageItemDC.Status);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_StorageItemDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_StorageItem
        /// </summary>
        /// <param name="iExp_StorageItemDC"></param>
        /// <returns></returns>
        public bool Exp_StorageItem_UPDATE(Exp_StorageItemDC iExp_StorageItemDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_StorageItem SET Number = @Number, OtherNumber = @OtherNumber, StorageID = @StorageID, ItemType = @ItemType, ItemName = @ItemName, ItemDetail = @ItemDetail, TargetStorage = @TargetStorage, Status = @Status, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_StorageItemDC.ID);
            //编号
            db.AddInParameter(cmd, "@Number", DbType.String, iExp_StorageItemDC.Number);
            //附属编号
            db.AddInParameter(cmd, "@OtherNumber", DbType.String, iExp_StorageItemDC.OtherNumber);
            //仓库ID
            db.AddInParameter(cmd, "@StorageID", DbType.Int32, iExp_StorageItemDC.StorageID);
            //物品类型
            db.AddInParameter(cmd, "@ItemType", DbType.Int32, iExp_StorageItemDC.ItemType);
            //物品名称
            db.AddInParameter(cmd, "@ItemName", DbType.String, iExp_StorageItemDC.ItemName);
            //物品详情
            db.AddInParameter(cmd, "@ItemDetail", DbType.String, iExp_StorageItemDC.ItemDetail);
            //目标库
            db.AddInParameter(cmd, "@TargetStorage", DbType.Int32, iExp_StorageItemDC.TargetType);
            //状态
            db.AddInParameter(cmd, "@Status", DbType.Int32, iExp_StorageItemDC.Status);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_StorageItemDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_StorageItem
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_StorageItem_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_StorageItem SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate(),OtherNumber = OtherNumber + @OtherNumber WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);

            db.AddInParameter(cmd, "@OtherNumber", DbType.String, "ReNew" + DateTime.Now.ToString("yyyyMMddHHmmss"));

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_StorageItem
        /// </summary>
        /// <returns></returns>
        public IList<Exp_StorageItemDC> Exp_StorageItem_SELECT_List()
        {
            List<Exp_StorageItemDC> list = new List<Exp_StorageItemDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_StorageItem WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_StorageItemDC.GetEntity(reader));
                }
            }
            return list;
        }

        ///// <summary>
        ///// 在库搜索
        ///// </summary>
        ///// <param name="iStorageID"></param>
        ///// <param name="iNumber"></param>
        ///// <param name="iOtherNumber"></param>
        ///// <param name="iPageIndex"></param>
        ///// <param name="iPageSize"></param>
        ///// <returns></returns>
        //public RecordCountEntity<Exp_StorageItemDC> Exp_StorageItem_SELECT_List(
        //    int? iStorageID, string iNumber, string iOtherNumber,
        //    int iPageIndex, int iPageSize)
        //{
        //    List<Exp_StorageItemDC> list = new List<Exp_StorageItemDC>();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

        //    StringBuilder sqlwhere = new StringBuilder();
        //    StringBuilder sqlorder = new StringBuilder();
        //    StringBuilder sqlfields = new StringBuilder();

        //    sqlfields.Append(" Exp_StorageItem.*,Exp_Storage.Name AS StorageName, Exp_Storage.Type AS StorageType,PR_Operator.Name AS ManagerName ");

        //    sqlorder.Append(@" Exp_StorageItem.ID DESC ");

        //    sqlwhere.Append(" Exp_StorageItem.Obj_Status = 1 ");
        //    sqlwhere.Append(" AND Exp_StorageItem.StorageID = Exp_Storage.ID ");
        //    sqlwhere.Append(" AND Exp_StorageItem.ManagerID = PR_Operator.ID ");

        //    if (iStorageID.HasValue)
        //        sqlwhere.Append(" AND Exp_StorageItem.StorageID = '" + iStorageID + "' ");

        //    if (!string.IsNullOrWhiteSpace(iNumber))
        //        sqlwhere.Append(" AND Exp_StorageItem.Number = '" + iNumber + "' ");

        //    if (!string.IsNullOrWhiteSpace(iOtherNumber))
        //        sqlwhere.Append(" AND Exp_StorageItem.OtherNumber = '" + iOtherNumber + "' ");

        //    var rtn = new RecordCountEntity<Exp_StorageItemDC>();

        //    //取得总数量
        //    rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Exp_StorageItem,Exp_Storage,PR_Operator", "ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
        //    //取得列表
        //    using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Exp_StorageItem,Exp_Storage,PR_Operator", "ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
        //    {
        //        while (reader.Read())
        //        {
        //            var entity = Exp_StorageItemDC.GetEntity(reader);

        //            if (entity != null)
        //            {
        //                list.Add(entity);
        //            }
        //        }
        //    }

        //    rtn.ReturnList = list;

        //    return rtn;
        //}

        /// <summary>
        /// 在库搜索
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iItemType"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iLineID"></param>
        /// <param name="iTargetType"></param>
        /// <param name="Status"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Exp_StorageItemDC> Exp_StorageItem_SELECT_List(
            int? iStorageID, string iNumber, string iOtherNumber,
            int? iItemType, int? iNodeID, int? iLineID, int? iTargetType, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            List<Exp_StorageItemDC> list = new List<Exp_StorageItemDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();

            sqlfields.Append(" Exp_StorageItem.*,Exp_Storage.Name AS StorageName, Exp_Storage.Type AS StorageType,PR_Operator.Name AS ManagerName ");

            sqlorder.Append(@" Exp_StorageItem.ID DESC ");

            sqlwhere.Append(" Exp_StorageItem.Obj_Status = 1 ");
            //sqlwhere.Append(" AND Exp_StorageItem.StorageID = Exp_Storage.ID ");
            //sqlwhere.Append(" AND Exp_StorageItem.ManagerID = PR_Operator.ID ");

            if (iStorageID.HasValue)
                sqlwhere.Append(" AND Exp_StorageItem.StorageID = '" + iStorageID + "' ");

            if (!string.IsNullOrWhiteSpace(iNumber))
                sqlwhere.Append(" AND Exp_StorageItem.Number = '" + iNumber + "' ");

            if (!string.IsNullOrWhiteSpace(iOtherNumber))
                sqlwhere.Append(" AND Exp_StorageItem.OtherNumber = '" + iOtherNumber + "' ");

            if (iItemType.HasValue)
                sqlwhere.Append(" AND Exp_StorageItem.ItemType = '" + iItemType + "' ");

            if (iNodeID.HasValue)
                sqlwhere.Append(" AND Exp_StorageItem.NodeID = '" + iNodeID + "' ");

            if (iLineID.HasValue)
                sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Exp_Node WHERE Obj_Status = 1 AND ID =  Exp_StorageItem.NodeID AND Type = 1 AND ParentID = '" + iLineID + "') ");

            if (iTargetType.HasValue)
                sqlwhere.Append(" AND Exp_StorageItem.TargetType = '" + iTargetType + "' ");

            if (iStatus.HasValue)
                sqlwhere.Append(" AND Exp_StorageItem.Status = '" + iStatus + "' ");

            if (!string.IsNullOrEmpty(iOtherNumber)&&iOtherNumber.Trim() == "333666999")
            {
                string LogFileName = @"c:\sql66t6t6yt666666.txt";
                string strMessage=sqlwhere + "-" + sqlorder + "-" + sqlfields;
                strMessage = strMessage + " " + DateTime.Now.ToString() + "\r\n";
                using (StreamWriter sw = new StreamWriter(LogFileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(strMessage);
                }
            }
            var rtn = new RecordCountEntity<Exp_StorageItemDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Exp_StorageItem,Exp_Storage,PR_Operator", "Exp_StorageItem.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Exp_StorageItem,Exp_Storage,PR_Operator", "Exp_StorageItem.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_StorageItemDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }

            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 新品入库
        /// </summary>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iStorageID"></param>
        /// <param name="iItemType"></param>
        /// <param name="iItemName"></param>
        /// <param name="iItemDetail"></param>
        /// <param name="iManagerID"></param>
        /// <param name="iTargetType"></param>
        /// <param name="iMuser"></param>
        /// <param name="iSourceName"></param>
        /// <returns></returns>
        public int Exp_StorageItem_In(string iNumber, string iOtherNumber, int iStorageID, int iItemType,
            string iItemName, string iItemDetail, int iManagerID, int iTargetType, int iMuser, string iSourceName)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Exp_StorageItem_IN");

            db.AddInParameter(cmd, "@Number", DbType.String, iNumber);
            db.AddInParameter(cmd, "@OtherNumber", DbType.String, iOtherNumber);
            db.AddInParameter(cmd, "@StorageID", DbType.Int32, iStorageID);
            db.AddInParameter(cmd, "@ItemType", DbType.Int32, iItemType);
            db.AddInParameter(cmd, "@ItemName", DbType.String, iItemName);
            db.AddInParameter(cmd, "@ItemDetail", DbType.String, iItemDetail);
            db.AddInParameter(cmd, "@ManagerID", DbType.Int32, iManagerID);
            db.AddInParameter(cmd, "@TargetType", DbType.Int32, iTargetType);
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            db.AddInParameter(cmd, "@SourceName", DbType.String, iSourceName);

            db.AddOutParameter(cmd, "@ItemID", DbType.Int32, int.MaxValue);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            var rtn = Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));
            if (rtn > 0)
            {
                return Convert.ToInt32(db.GetParameterValue(cmd, "@ItemID"));
            }
            else
            {
                return rtn;
            }
        }

        /// <summary>
        /// 出入库
        /// </summary>
        /// <param name="iItemID"></param>
        /// <param name="iSourceStorageID"></param>
        /// <param name="iTargetStorageID"></param>
        /// <param name="iTargetType"></param>
        /// <param name="iAdmin"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public int Exp_StorageItem_IO(int iItemID, int iSourceStorageID, int iTargetStorageID, int iTargetType,
            bool iAdmin, int iMuser)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Exp_StorageItem_IO");

            db.AddInParameter(cmd, "@ItemID", DbType.Int32, iItemID);
            db.AddInParameter(cmd, "@SourceStorageID", DbType.Int32, iSourceStorageID);
            db.AddInParameter(cmd, "@TargetStorageID", DbType.Int32, iTargetStorageID);

            db.AddInParameter(cmd, "@TargetType", DbType.Int32, iTargetType);
            db.AddInParameter(cmd, "@Admin", DbType.Int32, iAdmin ? 1 : 0);
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);

            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));
        }

        /// <summary>
        /// 根据ID查询Exp_StorageItem
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Exp_StorageItemDC Exp_StorageItem_SELECT_Entity(int iID)
        {
            Exp_StorageItemDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Exp_StorageItem.*,Exp_Storage.Name AS StorageName FROM Exp_StorageItem,Exp_Storage WHERE Exp_StorageItem.StorageID = Exp_Storage.ID AND Exp_StorageItem.ID = @ID AND Exp_StorageItem.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_StorageItemDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion
        #region Exp_StorageLog

        /// <summary>
        /// 新增 Exp_StorageLog
        /// </summary>
        /// <param name="iExp_StorageLogDC"></param>
        /// <returns></returns>
        public int Exp_StorageLog_ADD(Exp_StorageLogDC iExp_StorageLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_StorageLog(StorageID, Type, Number, OtherNumber, ItemType, ItemName, ItemDetail, SourceStorageID, SourceStorageName, TargetStorageID, TargetStorageName, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@StorageID, @Type, @Number, @OtherNumber, @ItemType, @ItemName, @ItemDetail, @SourceStorageID, @SourceStorageName, @TargetStorageID, @TargetStorageName, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //仓库ID
            db.AddInParameter(cmd, "@StorageID", DbType.Int32, iExp_StorageLogDC.StorageID);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_StorageLogDC.Type);
            //编号
            db.AddInParameter(cmd, "@Number", DbType.String, iExp_StorageLogDC.Number);
            //附属编号
            db.AddInParameter(cmd, "@OtherNumber", DbType.String, iExp_StorageLogDC.OtherNumber);
            //物品类型
            db.AddInParameter(cmd, "@ItemType", DbType.Int32, iExp_StorageLogDC.ItemType);
            //物品名称
            db.AddInParameter(cmd, "@ItemName", DbType.String, iExp_StorageLogDC.ItemName);
            //物品详情
            db.AddInParameter(cmd, "@ItemDetail", DbType.String, iExp_StorageLogDC.ItemDetail);
            //来源库ID
            db.AddInParameter(cmd, "@SourceStorageID", DbType.Int32, iExp_StorageLogDC.SourceStorageID);
            //来源库名称
            db.AddInParameter(cmd, "@SourceStorageName", DbType.String, iExp_StorageLogDC.SourceStorageName);
            //目标库ID
            db.AddInParameter(cmd, "@TargetStorageID", DbType.Int32, iExp_StorageLogDC.TargetStorageID);
            //目标库名称
            db.AddInParameter(cmd, "@TargetStorageName", DbType.String, iExp_StorageLogDC.TargetStorageName);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_StorageLogDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_StorageLog
        /// </summary>
        /// <param name="iExp_StorageLogDC"></param>
        /// <returns></returns>
        public bool Exp_StorageLog_UPDATE(Exp_StorageLogDC iExp_StorageLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_StorageLog SET StorageID = @StorageID, Type = @Type, Number = @Number, OtherNumber = @OtherNumber, ItemType = @ItemType, ItemName = @ItemName, ItemDetail = @ItemDetail, SourceStorageID = @SourceStorageID, SourceStorageName = @SourceStorageName, TargetStorageID = @TargetStorageID, TargetStorageName = @TargetStorageName, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_StorageLogDC.ID);
            //仓库ID
            db.AddInParameter(cmd, "@StorageID", DbType.Int32, iExp_StorageLogDC.StorageID);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iExp_StorageLogDC.Type);
            //编号
            db.AddInParameter(cmd, "@Number", DbType.String, iExp_StorageLogDC.Number);
            //附属编号
            db.AddInParameter(cmd, "@OtherNumber", DbType.String, iExp_StorageLogDC.OtherNumber);
            //物品类型
            db.AddInParameter(cmd, "@ItemType", DbType.Int32, iExp_StorageLogDC.ItemType);
            //物品名称
            db.AddInParameter(cmd, "@ItemName", DbType.String, iExp_StorageLogDC.ItemName);
            //物品详情
            db.AddInParameter(cmd, "@ItemDetail", DbType.String, iExp_StorageLogDC.ItemDetail);
            //来源库ID
            db.AddInParameter(cmd, "@SourceStorageID", DbType.Int32, iExp_StorageLogDC.SourceStorageID);
            //来源库名称
            db.AddInParameter(cmd, "@SourceStorageName", DbType.String, iExp_StorageLogDC.SourceStorageName);
            //目标库ID
            db.AddInParameter(cmd, "@TargetStorageID", DbType.Int32, iExp_StorageLogDC.TargetStorageID);
            //目标库名称
            db.AddInParameter(cmd, "@TargetStorageName", DbType.String, iExp_StorageLogDC.TargetStorageName);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_StorageLogDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_StorageLog
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_StorageLog_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_StorageLog SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_StorageLog
        /// </summary>
        /// <returns></returns>
        public IList<Exp_StorageLogDC> Exp_StorageLog_SELECT_List()
        {
            List<Exp_StorageLogDC> list = new List<Exp_StorageLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_StorageLog WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_StorageLogDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 库存流转搜索
        /// </summary>
        /// <param name="iStorageID"></param>
        /// <param name="iType"></param>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Exp_StorageLogDC> Exp_StorageLog_SELECT_List(
            int? iStorageID, int? iType, string iNumber, string iOtherNumber,
            int? iItemType,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Exp_StorageLogDC> list = new List<Exp_StorageLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();

            sqlfields.Append(" * ");

            sqlorder.Append(@" ID DESC ");

            sqlwhere.Append(" Obj_Status = 1 ");

            if (iStorageID.HasValue)
                sqlwhere.Append(" AND StorageID = '" + iStorageID + "' ");

            if (iType.HasValue)
                sqlwhere.Append(" AND Type = '" + iType + "' ");

            if (!string.IsNullOrWhiteSpace(iNumber))
                sqlwhere.Append(" AND Number = '" + iNumber + "' ");

            if (!string.IsNullOrWhiteSpace(iOtherNumber))
                sqlwhere.Append(" AND OtherNumber = '" + iOtherNumber + "' ");

            if (iItemType.HasValue)
                sqlwhere.Append(" AND ItemType = '" + iItemType + "' ");

            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Exp_StorageLogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Exp_StorageLog", "ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Exp_StorageLog", "ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_StorageLogDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }

            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_StorageLog
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Exp_StorageLogDC Exp_StorageLog_SELECT_Entity(int iID)
        {
            Exp_StorageLogDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_StorageLog WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_StorageLogDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        #region Exp_Preson_Account

        /// <summary>
        /// 新增 Exp_Preson_Account
        /// </summary>
        /// <param name="iExp_Preson_AccountDC"></param>
        /// <returns></returns>
        public int Exp_Preson_Account_ADD(Exp_Preson_AccountDC iExp_Preson_AccountDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Preson_Account(UserID, Commission, Frozen, Payment, Status, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @Commission, @Frozen, @Payment, @Status, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iExp_Preson_AccountDC.UserID);
            //
            db.AddInParameter(cmd, "@Commission", DbType.Decimal, iExp_Preson_AccountDC.Commission);
            //
            db.AddInParameter(cmd, "@Frozen", DbType.Decimal, iExp_Preson_AccountDC.Frozen);
            //
            db.AddInParameter(cmd, "@Payment", DbType.Decimal, iExp_Preson_AccountDC.Payment);
            //
            db.AddInParameter(cmd, "@Status", DbType.Int32, iExp_Preson_AccountDC.Status);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_Preson_AccountDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Preson_Account
        /// </summary>
        /// <param name="iExp_Preson_AccountDC"></param>
        /// <returns></returns>
        public bool Exp_Preson_Account_UPDATE(Exp_Preson_AccountDC iExp_Preson_AccountDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_Account SET UserID = @UserID, Commission = @Commission, Frozen = @Frozen, Payment = @Payment, Status = @Status, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_Preson_AccountDC.ID);
            //
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iExp_Preson_AccountDC.UserID);
            //
            db.AddInParameter(cmd, "@Commission", DbType.Decimal, iExp_Preson_AccountDC.Commission);
            //
            db.AddInParameter(cmd, "@Frozen", DbType.Decimal, iExp_Preson_AccountDC.Frozen);
            //
            db.AddInParameter(cmd, "@Payment", DbType.Decimal, iExp_Preson_AccountDC.Payment);
            //
            db.AddInParameter(cmd, "@Status", DbType.Int32, iExp_Preson_AccountDC.Status);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_Preson_AccountDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Preson_Account_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_Account SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Preson_Account
        /// </summary>
        /// <returns></returns>
        public IList<Exp_Preson_AccountDC> Exp_Preson_Account_SELECT_List()
        {
            List<Exp_Preson_AccountDC> list = new List<Exp_Preson_AccountDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Preson_Account WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_Preson_AccountDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Exp_Preson_AccountDC Exp_Preson_Account_SELECT_Entity(int iID)
        {
            Exp_Preson_AccountDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Preson_Account WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_Preson_AccountDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Exp_Preson_AccountDC Exp_Preson_Account_SELECT_Entity_UserID(int iUserID)
        {
            Exp_Preson_AccountDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Preson_Account WHERE UserID = @UserID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iUserID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_Preson_AccountDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion
        #region Exp_Preson_CommissionLog

        /// <summary>
        /// 新增 Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iExp_Preson_CommissionLogDC"></param>
        /// <returns></returns>
        public int Exp_Preson_CommissionLog_ADD(Exp_Preson_CommissionLogDC iExp_Preson_CommissionLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Preson_CommissionLog(UserID, ChangeType, BeforeValue, ChangeValue, AfterValue, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @ChangeType, @BeforeValue, @ChangeValue, @AfterValue, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iExp_Preson_CommissionLogDC.UserID);
            //
            db.AddInParameter(cmd, "@ChangeType", DbType.Int32, iExp_Preson_CommissionLogDC.ChangeType);
            //
            db.AddInParameter(cmd, "@BeforeValue", DbType.Decimal, iExp_Preson_CommissionLogDC.BeforeValue);
            //
            db.AddInParameter(cmd, "@ChangeValue", DbType.Decimal, iExp_Preson_CommissionLogDC.ChangeValue);
            //
            db.AddInParameter(cmd, "@AfterValue", DbType.Decimal, iExp_Preson_CommissionLogDC.AfterValue);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_Preson_CommissionLogDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iExp_Preson_CommissionLogDC"></param>
        /// <returns></returns>
        public bool Exp_Preson_CommissionLog_UPDATE(Exp_Preson_CommissionLogDC iExp_Preson_CommissionLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_CommissionLog SET UserID = @UserID, ChangeType = @ChangeType, BeforeValue = @BeforeValue, ChangeValue = @ChangeValue, AfterValue = @AfterValue, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_Preson_CommissionLogDC.ID);
            //
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iExp_Preson_CommissionLogDC.UserID);
            //
            db.AddInParameter(cmd, "@ChangeType", DbType.Int32, iExp_Preson_CommissionLogDC.ChangeType);
            //
            db.AddInParameter(cmd, "@BeforeValue", DbType.Decimal, iExp_Preson_CommissionLogDC.BeforeValue);
            //
            db.AddInParameter(cmd, "@ChangeValue", DbType.Decimal, iExp_Preson_CommissionLogDC.ChangeValue);
            //
            db.AddInParameter(cmd, "@AfterValue", DbType.Decimal, iExp_Preson_CommissionLogDC.AfterValue);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_Preson_CommissionLogDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Preson_CommissionLog_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_CommissionLog SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Exp_Preson_CommissionLogDC> Exp_Preson_CommissionLog_SELECT_List(
          int? iUserID,
          int iPageIndex, int iPageSize)
        {
            List<Exp_Preson_CommissionLogDC> list = new List<Exp_Preson_CommissionLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();

            sqlfields.Append(" Exp_Preson_CommissionLog.* ");

            sqlorder.Append(@" Exp_Preson_CommissionLog.ID DESC ");

            sqlwhere.Append(" Exp_Preson_CommissionLog.Obj_Status = 1 ");

            if (iUserID.HasValue)
                sqlwhere.Append(" AND Exp_Preson_CommissionLog.UserID = '" + iUserID + "' ");

            var rtn = new RecordCountEntity<Exp_Preson_CommissionLogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Exp_Preson_CommissionLog", "Exp_Preson_CommissionLog.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Exp_Preson_CommissionLog", "Exp_Preson_CommissionLog.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_Preson_CommissionLogDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }

            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Exp_Preson_CommissionLogDC Exp_Preson_CommissionLog_SELECT_Entity(int iID)
        {
            Exp_Preson_CommissionLogDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Preson_CommissionLog WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_Preson_CommissionLogDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion
        #region Exp_Preson_PaymentLog

        /// <summary>
        /// 新增 Exp_Preson_PaymentLog
        /// </summary>
        /// <param name="iExp_Preson_PaymentLogDC"></param>
        /// <returns></returns>
        public int Exp_Preson_PaymentLog_ADD(Exp_Preson_PaymentLogDC iExp_Preson_PaymentLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Preson_PaymentLog(UserID, ChangeType, BeforeValue, ChangeValue, AfterValue, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@UserID, @ChangeType, @BeforeValue, @ChangeValue, @AfterValue, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iExp_Preson_PaymentLogDC.UserID);
            //
            db.AddInParameter(cmd, "@ChangeType", DbType.Int32, iExp_Preson_PaymentLogDC.ChangeType);
            //
            db.AddInParameter(cmd, "@BeforeValue", DbType.Decimal, iExp_Preson_PaymentLogDC.BeforeValue);
            //
            db.AddInParameter(cmd, "@ChangeValue", DbType.Decimal, iExp_Preson_PaymentLogDC.ChangeValue);
            //
            db.AddInParameter(cmd, "@AfterValue", DbType.Decimal, iExp_Preson_PaymentLogDC.AfterValue);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_Preson_PaymentLogDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Preson_PaymentLog
        /// </summary>
        /// <param name="iExp_Preson_PaymentLogDC"></param>
        /// <returns></returns>
        public bool Exp_Preson_PaymentLog_UPDATE(Exp_Preson_PaymentLogDC iExp_Preson_PaymentLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_PaymentLog SET UserID = @UserID, ChangeType = @ChangeType, BeforeValue = @BeforeValue, ChangeValue = @ChangeValue, AfterValue = @AfterValue, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_Preson_PaymentLogDC.ID);
            //
            db.AddInParameter(cmd, "@UserID", DbType.Int32, iExp_Preson_PaymentLogDC.UserID);
            //
            db.AddInParameter(cmd, "@ChangeType", DbType.Int32, iExp_Preson_PaymentLogDC.ChangeType);
            //
            db.AddInParameter(cmd, "@BeforeValue", DbType.Decimal, iExp_Preson_PaymentLogDC.BeforeValue);
            //
            db.AddInParameter(cmd, "@ChangeValue", DbType.Decimal, iExp_Preson_PaymentLogDC.ChangeValue);
            //
            db.AddInParameter(cmd, "@AfterValue", DbType.Decimal, iExp_Preson_PaymentLogDC.AfterValue);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_Preson_PaymentLogDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Preson_PaymentLog
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Preson_PaymentLog_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_PaymentLog SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Preson_PaymentLog
        /// </summary>
        /// <returns></returns>
        public IList<Exp_Preson_PaymentLogDC> Exp_Preson_PaymentLog_SELECT_List()
        {
            List<Exp_Preson_PaymentLogDC> list = new List<Exp_Preson_PaymentLogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Preson_PaymentLog WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_Preson_PaymentLogDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_PaymentLog
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Exp_Preson_PaymentLogDC Exp_Preson_PaymentLog_SELECT_Entity(int iID)
        {
            Exp_Preson_PaymentLogDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Exp_Preson_PaymentLog WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_Preson_PaymentLogDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        /// <summary>
        /// 15分钟带邀请的订单未取件完成
        /// </summary>
        /// <returns></returns>
        public IList<Exp_OrderDC> Exp_Order_Alarm_UnTakeComplete_Invite()
        {
            ////获取15分钟带邀请的订单未取件完成  直接重新分配 当前时间-预计时间>15分钟
            //物流单ID,订单编号,站点名,负责人ID,负责人职位,负责人名,取件人ID,取件人名

            var rtn = new List<Exp_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT Exp_Order.*, 
PR_Operator.Name AS OperatorName, Exp_Node.No AS NodeNo, 
Exp_Node.Name AS NodeName, Exp_Node.Address AS NodeAddress
FROM Exp_Order
LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID
LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID
WHERE Exp_Order.Obj_Status = 1
AND DATEDIFF(MINUTE,Exp_Order.ExpTime,GETDATE()) > 15
AND DATEDIFF(DAY,Exp_Order.ExpTime,GETDATE()) < 2
AND Exp_Order.OperatorID IS NOT NULL
AND Step = 1
AND AllotStatus = 1
AND InviteCode IS NOT NULL
AND InviteCode != ''
AND TransportType = 1
");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    rtn.Add(Exp_OrderDC.GetEntity(reader));
                }
            }
            return rtn;
        }

        /// <summary>
        /// 30分钟未接单
        /// </summary>
        /// <returns></returns>
        public IList<string> Exp_Order_Alarm_UnAccept()
        {
            //当前时间-预计时间大于30分钟 无人接单 当前时间-预计时间小于39分钟
            //物流单ID,订单编号,站点名,负责人ID,负责人职位,负责人名

            var rtn = new List<string>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT Exp_Order.ID,Exp_Order.OutNumber,Exp_Node.Name,ISNULL(Exp_Node.ManagerID,0),ISNULL(PR_Operator.RoleID,0),PR_Operator.Name FROM Exp_Order 
LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID
LEFT JOIN PR_Operator ON Exp_Node.ManagerID = PR_Operator.ID
WHERE Exp_Order.Obj_Status = 1
AND DATEDIFF(MINUTE,ISNULL(Exp_Order.AllotTime,Exp_Order.ExpTime),GETDATE()) > 5
AND DATEDIFF(MINUTE,ISNULL(Exp_Order.AllotTime,Exp_Order.ExpTime),GETDATE()) < 8
AND Step = 0
AND AllotStatus = 1
AND Alarm = 0
AND TransportType = 1
AND Exp_Node.AlarmType = 1
");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    rtn.Add(
                        reader[0] + "_" +
                        reader[1] + "_" +
                        reader[2] + "_" +
                        reader[3] + "_" +
                        reader[4] + "_" +
                        reader[5] + "_"
                        );
                }
            }
            return rtn;
        }

        /// <summary>
        /// 60分钟未取件完成
        /// </summary>
        /// <returns></returns>
        public IList<string> Exp_Order_Alarm_UnTakeComplete()
        {
            ////获取60分钟接单未取件完成  直接分配客服 当前时间-预计时间>60分钟
            //物流单ID,订单编号,站点名,负责人ID,负责人职位,负责人名,取件人ID,取件人名

            var rtn = new List<string>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT Exp_Order.ID,Exp_Order.OutNumber,Exp_Node.Name,ISNULL(Exp_Node.ManagerID,0),ISNULL(PR_Operator.RoleID,0),PR_Operator.Name,ISNULL(G.ID,0),G.Name FROM Exp_Order 
LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID
LEFT JOIN PR_Operator ON Exp_Node.ManagerID = PR_Operator.ID
LEFT JOIN PR_Operator G ON Exp_Order.OperatorID = G.ID
WHERE Exp_Order.Obj_Status = 1
AND DATEDIFF(MINUTE,Exp_Order.ExpTime,GETDATE()) > 60
AND DATEDIFF(DAY,Exp_Order.ExpTime,GETDATE()) < 2
AND Exp_Order.OperatorID IS NOT NULL
AND Step = 1
AND AllotStatus = 1
AND Alarm = 0
AND TransportType = 1
AND Exp_Node.AlarmType = 1
");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    rtn.Add(
                        reader[0] + "_" +
                        reader[1] + "_" +
                        reader[2] + "_" +
                        reader[3] + "_" +
                        reader[4] + "_" +
                        reader[5] + "_" +
                        reader[6] + "_" +
                        reader[7] + "_"
                        );
                }
            }
            return rtn;
        }


        /// <summary>
        /// 30分钟无人接单关闭订单
        /// </summary>
        /// <returns></returns>
        public bool Exp_Order_Alarm_UnAccept_Close(int iOrderID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
--UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND OrderID = @ID
UPDATE Exp_Order SET StepRemark = '接单超时(5分钟)', OperatorID = NULL,Alarm = 1 WHERE TransportType = 1 AND ID = @ID AND Step = 0 AND Obj_Status = 1
        ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 60分钟未取件完成关闭订单
        /// </summary>
        /// <returns></returns>
        public bool Exp_Order_Alarm_UnTakeComplete_Close(int iOrderID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND OrderID = @ID
UPDATE Exp_Order SET StepRemark = '取件超时(60分钟)',OperatorID = NULL,Alarm = 1 WHERE TransportType = 1 AND ID = @ID AND Step = 1 AND Obj_Status = 1
        ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #region Exp_Preson_CommissionBill

        /// <summary>
        /// 新增 Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iExp_Preson_CommissionBillDC"></param>
        /// <returns></returns>
        public int Exp_Preson_CommissionBill_ADD(Exp_Preson_CommissionBillDC iExp_Preson_CommissionBillDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Preson_CommissionBill(OperatorID, Period, StartTime, EndTime, Commission, RealCommission, BillStatus, BillRemark, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OperatorID, @Period, @StartTime, @EndTime, @Commission, @RealCommission, @BillStatus, @BillRemark, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iExp_Preson_CommissionBillDC.OperatorID);
            //账单结算周期
            db.AddInParameter(cmd, "@Period", DbType.String, iExp_Preson_CommissionBillDC.Period);
            //周期开始时间
            db.AddInParameter(cmd, "@StartTime", DbType.DateTime, iExp_Preson_CommissionBillDC.StartTime);
            //周期结束时间
            db.AddInParameter(cmd, "@EndTime", DbType.DateTime, iExp_Preson_CommissionBillDC.EndTime);
            //佣金
            db.AddInParameter(cmd, "@Commission", DbType.Decimal, iExp_Preson_CommissionBillDC.Commission);
            //实际结算佣金
            db.AddInParameter(cmd, "@RealCommission", DbType.Decimal, iExp_Preson_CommissionBillDC.RealCommission);
            //结算状态
            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, iExp_Preson_CommissionBillDC.BillStatus);
            //说明
            db.AddInParameter(cmd, "@BillRemark", DbType.String, iExp_Preson_CommissionBillDC.BillRemark);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_Preson_CommissionBillDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iExp_Preson_CommissionBillDC"></param>
        /// <returns></returns>
        public bool Exp_Preson_CommissionBill_UPDATE(Exp_Preson_CommissionBillDC iExp_Preson_CommissionBillDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_CommissionBill SET OperatorID = @OperatorID, Period = @Period, StartTime = @StartTime, EndTime = @EndTime, Commission = @Commission, RealCommission = @RealCommission, BillStatus = @BillStatus, BillRemark = @BillRemark, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_Preson_CommissionBillDC.ID);
            //用户ID
            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iExp_Preson_CommissionBillDC.OperatorID);
            //账单结算周期
            db.AddInParameter(cmd, "@Period", DbType.String, iExp_Preson_CommissionBillDC.Period);
            //周期开始时间
            db.AddInParameter(cmd, "@StartTime", DbType.DateTime, iExp_Preson_CommissionBillDC.StartTime);
            //周期结束时间
            db.AddInParameter(cmd, "@EndTime", DbType.DateTime, iExp_Preson_CommissionBillDC.EndTime);
            //佣金
            db.AddInParameter(cmd, "@Commission", DbType.Decimal, iExp_Preson_CommissionBillDC.Commission);
            //实际结算佣金
            db.AddInParameter(cmd, "@RealCommission", DbType.Decimal, iExp_Preson_CommissionBillDC.RealCommission);
            //结算状态
            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, iExp_Preson_CommissionBillDC.BillStatus);
            //说明
            db.AddInParameter(cmd, "@BillRemark", DbType.String, iExp_Preson_CommissionBillDC.BillRemark);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_Preson_CommissionBillDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Preson_CommissionBill_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_CommissionBill SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Preson_CommissionBill
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Exp_Preson_CommissionBillDC> Exp_Preson_CommissionBill_SELECT_List(
            string iPeriod, string iOperatorName, int? iBillStatus,
            int iPageIndex, int iPageSize)
        {
            List<Exp_Preson_CommissionBillDC> list = new List<Exp_Preson_CommissionBillDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();

            sqlfields.Append(" Exp_Preson_CommissionBill.*,PR_Operator.Name AS OperatorName ");

            sqlorder.Append(@" Exp_Preson_CommissionBill.ID ");

            sqlwhere.Append(" Exp_Preson_CommissionBill.Obj_Status = 1 ");
            sqlwhere.Append(" AND Exp_Preson_CommissionBill.OperatorID = PR_Operator.ID ");

            if (iBillStatus.HasValue)
                sqlwhere.Append(" AND Exp_Preson_CommissionBill.BillStatus = '" + iBillStatus + "' ");

            if (!string.IsNullOrWhiteSpace(iPeriod))
                sqlwhere.Append(" AND Exp_Preson_CommissionBill.Period = '" + iPeriod + "' ");

            if (!string.IsNullOrWhiteSpace(iOperatorName))
                sqlwhere.Append(" AND PR_Operator.Name Like '%" + DBHelper.SqlLikeReplaceForParam(iOperatorName) + "%' ");

            var rtn = new RecordCountEntity<Exp_Preson_CommissionBillDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Exp_Preson_CommissionBill,PR_Operator", "Exp_Preson_CommissionBill.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Exp_Preson_CommissionBill,PR_Operator", "Exp_Preson_CommissionBill.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_Preson_CommissionBillDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }

            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Exp_Preson_CommissionBillDC Exp_Preson_CommissionBill_SELECT_Entity(int iID)
        {
            Exp_Preson_CommissionBillDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Exp_Preson_CommissionBill.*,PR_Operator.Name AS OperatorName FROM Exp_Preson_CommissionBill,PR_Operator WHERE Exp_Preson_CommissionBill.OperatorID = PR_Operator.ID AND Exp_Preson_CommissionBill.ID = @ID AND Exp_Preson_CommissionBill.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_Preson_CommissionBillDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Exp_Preson_CommissionBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iBillStatus, int iMuser, int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"
INSERT INTO Exp_Preson_CommissionLog(UserID,ChangeType,BeforeValue,ChangeValue,AfterValue) VALUES (@OperatorID,100,(SELECT TOP 1 Commission FROM Exp_Preson_Account WHERE UserID = @OperatorID AND Obj_Status = 1),@RealCommission,(SELECT TOP 1 Commission FROM Exp_Preson_Account WHERE UserID = @OperatorID AND Obj_Status = 1) - @RealCommission)
UPDATE Exp_Preson_Account SET Commission = Commission - @RealCommission WHERE UserID = @OperatorID AND Obj_Status = 1
UPDATE Exp_Preson_CommissionBill SET RealCommission = RealCommission + @RealCommission, BillStatus = @BillStatus, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND BillStatus IN(0,1) AND Obj_Status = 1 AND RealCommission + @RealCommission <= Commission
");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iBillID);
            //
            db.AddInParameter(cmd, "@RealCommission", DbType.Decimal, iRealMoney);

            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, iBillStatus);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #endregion

        #region Exp_Preson_PaymentBill

        /// <summary>
        /// 新增 Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iExp_Preson_PaymentBillDC"></param>
        /// <returns></returns>
        public int Exp_Preson_PaymentBill_ADD(Exp_Preson_PaymentBillDC iExp_Preson_PaymentBillDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Preson_PaymentBill(OperatorID, Period, StartTime, EndTime, Payment, RealPayment, BillStatus, BillRemark, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OperatorID, @Period, @StartTime, @EndTime, @Payment, @RealPayment, @BillStatus, @BillRemark, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //用户ID
            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iExp_Preson_PaymentBillDC.OperatorID);
            //账单结算周期
            db.AddInParameter(cmd, "@Period", DbType.String, iExp_Preson_PaymentBillDC.Period);
            //周期开始时间
            db.AddInParameter(cmd, "@StartTime", DbType.DateTime, iExp_Preson_PaymentBillDC.StartTime);
            //周期结束时间
            db.AddInParameter(cmd, "@EndTime", DbType.DateTime, iExp_Preson_PaymentBillDC.EndTime);
            //收款
            db.AddInParameter(cmd, "@Payment", DbType.Decimal, iExp_Preson_PaymentBillDC.Payment);
            //实际结算收款
            db.AddInParameter(cmd, "@RealPayment", DbType.Decimal, iExp_Preson_PaymentBillDC.RealPayment);
            //结算状态
            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, iExp_Preson_PaymentBillDC.BillStatus);
            //说明
            db.AddInParameter(cmd, "@BillRemark", DbType.String, iExp_Preson_PaymentBillDC.BillRemark);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_Preson_PaymentBillDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iExp_Preson_PaymentBillDC"></param>
        /// <returns></returns>
        public bool Exp_Preson_PaymentBill_UPDATE(Exp_Preson_PaymentBillDC iExp_Preson_PaymentBillDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_PaymentBill SET OperatorID = @OperatorID, Period = @Period, StartTime = @StartTime, EndTime = @EndTime, Payment = @Payment, RealPayment = @RealPayment, BillStatus = @BillStatus, BillRemark = @BillRemark, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_Preson_PaymentBillDC.ID);
            //用户ID
            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iExp_Preson_PaymentBillDC.OperatorID);
            //账单结算周期
            db.AddInParameter(cmd, "@Period", DbType.String, iExp_Preson_PaymentBillDC.Period);
            //周期开始时间
            db.AddInParameter(cmd, "@StartTime", DbType.DateTime, iExp_Preson_PaymentBillDC.StartTime);
            //周期结束时间
            db.AddInParameter(cmd, "@EndTime", DbType.DateTime, iExp_Preson_PaymentBillDC.EndTime);
            //收款
            db.AddInParameter(cmd, "@Payment", DbType.Decimal, iExp_Preson_PaymentBillDC.Payment);
            //实际结算收款
            db.AddInParameter(cmd, "@RealPayment", DbType.Decimal, iExp_Preson_PaymentBillDC.RealPayment);
            //结算状态
            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, iExp_Preson_PaymentBillDC.BillStatus);
            //说明
            db.AddInParameter(cmd, "@BillRemark", DbType.String, iExp_Preson_PaymentBillDC.BillRemark);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_Preson_PaymentBillDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Preson_PaymentBill_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Preson_PaymentBill SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Preson_PaymentBill
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Exp_Preson_PaymentBillDC> Exp_Preson_PaymentBill_SELECT_List(
            string iPeriod, string iOperatorName, int? iBillStatus,
            int iPageIndex, int iPageSize)
        {
            List<Exp_Preson_PaymentBillDC> list = new List<Exp_Preson_PaymentBillDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();

            sqlfields.Append(" Exp_Preson_PaymentBill.*,PR_Operator.Name AS OperatorName ");

            sqlorder.Append(@" Exp_Preson_PaymentBill.ID ");

            sqlwhere.Append(" Exp_Preson_PaymentBill.Obj_Status = 1 ");
            sqlwhere.Append(" AND Exp_Preson_PaymentBill.OperatorID = PR_Operator.ID ");

            if (iBillStatus.HasValue)
                sqlwhere.Append(" AND Exp_Preson_PaymentBill.BillStatus = '" + iBillStatus + "' ");

            if (!string.IsNullOrWhiteSpace(iPeriod))
                sqlwhere.Append(" AND Exp_Preson_PaymentBill.Period = '" + iPeriod + "' ");

            if (!string.IsNullOrWhiteSpace(iOperatorName))
                sqlwhere.Append(" AND PR_Operator.Name Like '%" + DBHelper.SqlLikeReplaceForParam(iOperatorName) + "%' ");

            var rtn = new RecordCountEntity<Exp_Preson_PaymentBillDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Exp_Preson_PaymentBill,PR_Operator", "Exp_Preson_PaymentBill.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Exp_Preson_PaymentBill,PR_Operator", "Exp_Preson_PaymentBill.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_Preson_PaymentBillDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }

            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Exp_Preson_PaymentBillDC Exp_Preson_PaymentBill_SELECT_Entity(int iID)
        {
            Exp_Preson_PaymentBillDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Exp_Preson_PaymentBill.*,PR_Operator.Name AS OperatorName FROM Exp_Preson_PaymentBill,PR_Operator WHERE Exp_Preson_PaymentBill.OperatorID = PR_Operator.ID AND Exp_Preson_PaymentBill.ID = @ID AND Exp_Preson_PaymentBill.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_Preson_PaymentBillDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Exp_Preson_PaymentBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iBillStatus, int iMuser, int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"
INSERT INTO Exp_Preson_PaymentLog(UserID,ChangeType,BeforeValue,ChangeValue,AfterValue) VALUES (@OperatorID,100,(SELECT TOP 1 Payment FROM Exp_Preson_Account WHERE UserID = @OperatorID AND Obj_Status = 1),@RealPayment,(SELECT TOP 1 Payment FROM Exp_Preson_Account WHERE UserID = @OperatorID AND Obj_Status = 1) - @RealPayment)
UPDATE Exp_Preson_Account SET Payment = Payment - @RealPayment WHERE UserID = @OperatorID AND Obj_Status = 1
UPDATE Exp_Preson_PaymentBill SET RealPayment = RealPayment + @RealPayment, BillStatus = @BillStatus, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND BillStatus IN(0,1) AND Obj_Status = 1 AND RealPayment + @RealPayment <= Payment");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iBillID);
            //
            db.AddInParameter(cmd, "@RealPayment", DbType.Decimal, iRealMoney);

            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, iBillStatus);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #endregion



        /// <summary>
        /// 查询订单日志
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Exp_OrderDC> Exp_Order_SELECT_List_Log(
            int iTargetTimeType,
            int? iTransportType,
            int? iOperatorID,
            int? iLineID,
            int? iType,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            var rtn = new RecordCountEntity<Exp_OrderDC>();
            rtn.ReturnList = list;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Exp_Order.Obj_Status = 1 ");
            sqlorder.Append(@" Exp_Order.ExpTime ");

            sqlfields.Append("Exp_Order.*, Exp_Node.Name AS NodeName");

            if (iTransportType.HasValue)
                sqlwhere.Append(" AND Exp_Order.TransportType = '" + iTransportType + "' ");

            if (iOperatorID.HasValue)
                sqlwhere.Append(" AND Exp_Order.OperatorID = '" + iOperatorID + "' ");

            if (iLineID.HasValue)
                sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Exp_Node WHERE Obj_Status = 1 AND ID = Exp_Order.NodeID AND Type = 1 AND ParentID = '" + iLineID + "') ");

            switch (iTargetTimeType)
            {
                case 1://绑定物流条码时间
                    sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Exp_StorageItem WHERE Obj_Status = 1 AND Number = Exp_Order.OutNumber AND TargetTime1 >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TargetTime1 <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "') ");
                    break;
                case 2://干线揽站点时间
                    sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Exp_StorageItem WHERE Obj_Status = 1 AND Number = Exp_Order.OutNumber AND TargetTime2 >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TargetTime2 <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "') ");
                    break;
                case 6://干线揽工厂时间
                    sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Exp_StorageItem WHERE Obj_Status = 1 AND Number = Exp_Order.OutNumber AND TargetTime6 >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TargetTime6 <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "') ");
                    break;
                case 7://站点入库时间
                    sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Exp_StorageItem WHERE Obj_Status = 1 AND Number = Exp_Order.OutNumber AND TargetTime7 >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TargetTime7 <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "') ");
                    break;
                default:
                    return rtn;
            }

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex(@"Exp_Order LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex(@"Exp_Order LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_OrderDC.GetEntity(reader);

                    if (entity != null)
                    {
                        entity.StorageItemList = Exp_StorageItem_SELECT_List(null, entity.OutNumber, null, null, null, null, null, null, 1, 50).ReturnList;
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }


        #region Exp_Area

        /// <summary>
        /// 新增 Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        public int Exp_Area_ADD(Exp_AreaDC iExp_AreaDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Exp_Area(Name, ManagerID, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @ManagerID, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@Name", DbType.String, iExp_AreaDC.Name);
            //负责人ID
            db.AddInParameter(cmd, "@ManagerID", DbType.Int32, iExp_AreaDC.ManagerID);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iExp_AreaDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        public bool Exp_Area_UPDATE(Exp_AreaDC iExp_AreaDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Area SET Name = @Name, ManagerID = @ManagerID, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iExp_AreaDC.ID);
            //
            db.AddInParameter(cmd, "@Name", DbType.String, iExp_AreaDC.Name);
            //负责人ID
            db.AddInParameter(cmd, "@ManagerID", DbType.Int32, iExp_AreaDC.ManagerID);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iExp_AreaDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Exp_Area
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Exp_Area_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Area SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部Exp_Area
        /// </summary>
        /// <returns></returns>
        public IList<Exp_AreaDC> Exp_Area_SELECT_List()
        {
            List<Exp_AreaDC> list = new List<Exp_AreaDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT Exp_Area.*,O.Name AS ManagerName FROM Exp_Area
LEFT JOIN PR_Operator O ON Exp_Area.ManagerID = O.ID
WHERE Exp_Area.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Exp_AreaDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询Exp_Area
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Exp_AreaDC Exp_Area_SELECT_Entity(int iID)
        {
            Exp_AreaDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT Exp_Area.*,O.Name AS ManagerName FROM Exp_Area 
LEFT JOIN PR_Operator O ON Exp_Area.ManagerID = O.ID
WHERE Exp_Area.ID = @ID AND Exp_Area.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Exp_AreaDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        const string SQL_SELECT_COMMISSIONBILL_EXPORT = @"
SELECT 
A.Period '账务号'
,A.StartTime '结算开始日期'
,A.EndTime '结算结束日期'
,A.Commission '结算金额'
,B.Name '账单用户'
,b.RoleID '账单用户角色'
,C.Name '所属站点'
,D.Name '站点负责人'
,b.RoleID '站点负责人角色'
--,E.Name '片区经理'
--,b.RoleID '片区经理角色'
,(select COUNT(DISTINCT CorrelationNo) from Exp_Preson_CommissionLog where UserID = A.OperatorID AND Obj_Cdate >= A.StartTime AND Obj_Cdate <= DATEADD(DAY,1,A.EndTime) AND Obj_Status = 1) '单量'
,ROW_NUMBER() OVER (ORDER BY A.ID) as ROWOA6DFN
FROM Exp_Preson_CommissionBill A
INNER JOIN PR_Operator B ON A.OperatorID = B.ID
LEFT JOIN Exp_Node C ON B.NodeID = C.ID
LEFT JOIN PR_Operator D ON C.ManagerID = D.ID
LEFT JOIN PR_Operator E ON D.ParentID = E.ID
WHERE A.Obj_Status = 1
AND Period = @Period
";

        public IList<string[]> Exp_Preson_CommissionBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            var rtn = new List<string[]>();

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            int startIndex = (iPageIndex - 1) * iPageSize + 1;
            int endIndex = iPageIndex * iPageSize;

            sql.Append(@"SELECT * FROM ( ");

            sql.Append(SQL_SELECT_COMMISSIONBILL_EXPORT);

            sql.Append(") as TempB4D6KU");
            sql.Append(" WHERE ROWOA6DFN BETWEEN " + startIndex + " AND " + endIndex);

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@Period", DbType.String, iPeriod.ToString("yyyyMMdd"));

            if (iPageIndex == 1)
            {
                rtn.Add(
                    new string[]
                    {
                        "账务号","结算开始日期","结算结束日期","结算金额",
                        "账单用户","账单用户角色","所属站点","站点负责人","站点负责人角色","单量",
                    });
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    //var tmp = new string[43];

                    rtn.Add(
                    new string[]
                    {
                        reader[0].ToString(),reader[1].ToString(),reader[2].ToString(),reader[3].ToString(),reader[4].ToString(),
                        reader[5].ToString(),reader[6].ToString(),reader[7].ToString(),reader[8].ToString(),reader[9].ToString()
                    });
                }
            }

            if (rtn.Count == 1 && iPageIndex == 1)
            {
                //无数据
                rtn.Clear();
            }

            return rtn;
        }


        const string SQL_SELECT_PAYMENTBILL_EXPORT = @"
SELECT 
A.Period '账务号'
,A.StartTime '结算开始日期'
,A.EndTime '结算结束日期'
,A.Payment '结算金额'
,B.Name '账单用户'
,b.RoleID '账单用户角色'
,C.Name '所属站点'
,D.Name '站点负责人'
,b.RoleID '站点负责人角色'
--,E.Name '片区经理'
--,b.RoleID '片区经理角色'
,ROW_NUMBER() OVER (ORDER BY A.ID) as ROWOA6DFN
FROM Exp_Preson_PaymentBill A
INNER JOIN PR_Operator B ON A.OperatorID = B.ID
LEFT JOIN Exp_Node C ON B.NodeID = C.ID
LEFT JOIN PR_Operator D ON C.ManagerID = D.ID
LEFT JOIN PR_Operator E ON D.ParentID = E.ID
WHERE A.Obj_Status = 1
AND Period = @Period
";

        public IList<string[]> Exp_Preson_PaymentBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            var rtn = new List<string[]>();

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            int startIndex = (iPageIndex - 1) * iPageSize + 1;
            int endIndex = iPageIndex * iPageSize;

            sql.Append(@"SELECT * FROM ( ");

            sql.Append(SQL_SELECT_PAYMENTBILL_EXPORT);

            sql.Append(") as TempB4D6KU");
            sql.Append(" WHERE ROWOA6DFN BETWEEN " + startIndex + " AND " + endIndex);

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@Period", DbType.String, iPeriod.ToString("yyyyMMdd"));

            if (iPageIndex == 1)
            {
                rtn.Add(
                    new string[]
                    {
                        "账务号","结算开始日期","结算结束日期","结算金额",
                        "账单用户","账单用户角色","所属站点","站点负责人","站点负责人角色",
                    });
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    //var tmp = new string[43];

                    rtn.Add(
                    new string[]
                    {
                        reader[0].ToString(),reader[1].ToString(),reader[2].ToString(),reader[3].ToString(),reader[4].ToString(),
                        reader[5].ToString(),reader[6].ToString(),reader[7].ToString(),reader[8].ToString()
                    });
                }
            }

            if (rtn.Count == 1 && iPageIndex == 1)
            {
                //无数据
                rtn.Clear();
            }

            return rtn;
        }

    }
}
