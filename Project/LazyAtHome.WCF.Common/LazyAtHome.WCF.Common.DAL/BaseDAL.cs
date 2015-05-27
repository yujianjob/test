using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
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
    public class BaseDAL : DALBase, LazyAtHome.WCF.Common.Interface.IDAL.IBaseDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseDAL()
        {
            APPModule = Core.Enumerate.ApplicationModule.Common;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Base_DistrictDC> Base_District_SELECT_List_ALL()
        {
            List<Base_DistrictDC> list = new List<Base_DistrictDC>();
            Base_DistrictDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Base_District WHERE Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Base_DistrictDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IList<Base_SiteDC> Base_Site_SELECT_List_ALL()
        {
            List<Base_SiteDC> list = new List<Base_SiteDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Base_Site WHERE Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Base_SiteDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Base_LogDC> Base_Log_SELECT_List(string iAppDomainName, string iTitle,
            int? iEventType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<Base_LogDC> list = new List<Base_LogDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" 1 = 1 ");
            sqlorder.Append(@" LogID DESC");

            //程序名称
            if (!string.IsNullOrEmpty(iAppDomainName))
                sqlwhere.Append("AND AppDomainName LIKE '%" + iAppDomainName + "%' ");
            //标题
            if (!string.IsNullOrEmpty(iTitle))
                sqlwhere.Append("AND Title LIKE '%" + iTitle + "%' ");
            //事件类型
            if (iEventType.HasValue)
                sqlwhere.Append("AND EventType =" + iEventType + " ");

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND LogTime >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND LogTime <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Base_LogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Base_Log", "LogID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Base_Log", "LogID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Base_LogDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;

            return rtn;
        }

        #region 优惠券

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iBase_CouponDC"></param>
        /// <returns></returns>
        public int Base_Coupon_ADD(Base_CouponDC iBase_CouponDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Base_Coupon(Name, UseClass, UseType, GetCount, FaceValue, MinMoney, MaxMoney, TotalCount, SendCount, UseBeginDate, UseEndDate, ValidDay, CommitStatus, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @UseClass, @UseType, @GetCount, @FaceValue, @MinMoney, @MaxMoney, @TotalCount, @SendCount, @UseBeginDate, @UseEndDate, @ValidDay, @CommitStatus, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iBase_CouponDC.Name);
            //使用类别
            db.AddInParameter(cmd, "@UseClass", DbType.Int32, iBase_CouponDC.UseClass);
            //使用类型
            db.AddInParameter(cmd, "@UseType", DbType.Int32, iBase_CouponDC.UseType);

            db.AddInParameter(cmd, "@GetCount", DbType.Int32, iBase_CouponDC.GetCount);
            //面值
            db.AddInParameter(cmd, "@FaceValue", DbType.Decimal, iBase_CouponDC.FaceValue);
            //最低消费额
            db.AddInParameter(cmd, "@MinMoney", DbType.Decimal, iBase_CouponDC.MinMoney);
            //最高消费额
            db.AddInParameter(cmd, "@MaxMoney", DbType.Decimal, iBase_CouponDC.MaxMoney);
            //总数量
            db.AddInParameter(cmd, "@TotalCount", DbType.Int32, iBase_CouponDC.TotalCount);
            //已领取数量
            db.AddInParameter(cmd, "@SendCount", DbType.Int32, iBase_CouponDC.SendCount);
            //有效期开始
            db.AddInParameter(cmd, "@UseBeginDate", DbType.DateTime, iBase_CouponDC.UseBeginDate);
            //有效期结束
            db.AddInParameter(cmd, "@UseEndDate", DbType.DateTime, iBase_CouponDC.UseEndDate);
            //领取后有效天
            db.AddInParameter(cmd, "@ValidDay", DbType.Int32, iBase_CouponDC.ValidDay);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iBase_CouponDC.CommitStatus);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iBase_CouponDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iBase_CouponDC">主键</param>
        /// <returns></returns>
        public bool Base_Coupon_UPDATE(Base_CouponDC iBase_CouponDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Coupon SET Name = @Name, UseClass = @UseClass, UseType = @UseType, GetCount = @GetCount, FaceValue = @FaceValue, MinMoney = @MinMoney, MaxMoney = @MaxMoney, TotalCount = @TotalCount, SendCount = @SendCount, UseBeginDate = @UseBeginDate, UseEndDate = @UseEndDate, ValidDay = @ValidDay, CommitStatus = @CommitStatus, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iBase_CouponDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iBase_CouponDC.Name);
            //使用类别
            db.AddInParameter(cmd, "@UseClass", DbType.Int32, iBase_CouponDC.UseClass);
            //使用类型
            db.AddInParameter(cmd, "@UseType", DbType.Int32, iBase_CouponDC.UseType);

            db.AddInParameter(cmd, "@GetCount", DbType.Int32, iBase_CouponDC.GetCount);
            //面值
            db.AddInParameter(cmd, "@FaceValue", DbType.Decimal, iBase_CouponDC.FaceValue);
            //最低消费额
            db.AddInParameter(cmd, "@MinMoney", DbType.Decimal, iBase_CouponDC.MinMoney);
            //最高消费额
            db.AddInParameter(cmd, "@MaxMoney", DbType.Decimal, iBase_CouponDC.MaxMoney);
            //总数量
            db.AddInParameter(cmd, "@TotalCount", DbType.Int32, iBase_CouponDC.TotalCount);
            //已领取数量
            db.AddInParameter(cmd, "@SendCount", DbType.Int32, iBase_CouponDC.SendCount);
            //有效期开始
            db.AddInParameter(cmd, "@UseBeginDate", DbType.DateTime, iBase_CouponDC.UseBeginDate);
            //有效期结束
            db.AddInParameter(cmd, "@UseEndDate", DbType.DateTime, iBase_CouponDC.UseEndDate);
            //领取后有效天
            db.AddInParameter(cmd, "@ValidDay", DbType.Int32, iBase_CouponDC.ValidDay);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iBase_CouponDC.CommitStatus);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iBase_CouponDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询优惠券列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iUseClass"></param>
        /// <param name="iUseType"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Base_CouponDC> Base_Coupon_SELECT_List(string iName, int? iUseClass,
           int? iUseType, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Base_CouponDC> list = new List<Base_CouponDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");
            sqlorder.Append(@" ID DESC ");

            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append("AND Name LIKE '%" + iName + "%' ");

            //使用类别
            if (iUseClass.HasValue)
                sqlwhere.Append("AND UseClass =" + iUseClass + " ");

            //使用类别
            if (iUseType.HasValue)
                sqlwhere.Append("AND UseType =" + iUseType + " ");

            //确认状态
            if (iCommitStatus.HasValue)
                sqlwhere.Append("AND CommitStatus =" + iCommitStatus + " ");

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Base_CouponDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Base_Coupon", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Base_Coupon", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Base_CouponDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Base_CouponDC Base_Coupon_SELECT_Entity(int iID)
        {
            Base_CouponDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Base_Coupon WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Base_CouponDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        #region 网站SEO

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        public int Base_WebAttribute_ADD(Base_WebAttributeDC iBase_WebAttributeDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Base_WebAttribute(Name, Page, Title, Keywords, Description, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @Page, @Title, @Keywords, @Description, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //页面名称
            db.AddInParameter(cmd, "@Name", DbType.String, iBase_WebAttributeDC.Name);
            //页面链接
            db.AddInParameter(cmd, "@Page", DbType.String, iBase_WebAttributeDC.Page);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iBase_WebAttributeDC.Title);
            //关键字
            db.AddInParameter(cmd, "@Keywords", DbType.String, iBase_WebAttributeDC.Keywords);
            //说明
            db.AddInParameter(cmd, "@Description", DbType.String, iBase_WebAttributeDC.Description);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iBase_WebAttributeDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        public bool Base_WebAttribute_UPDATE(Base_WebAttributeDC iBase_WebAttributeDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_WebAttribute SET Name = @Name, Page = @Page, Title = @Title, Keywords = @Keywords, Description = @Description, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iBase_WebAttributeDC.ID);
            //页面名称
            db.AddInParameter(cmd, "@Name", DbType.String, iBase_WebAttributeDC.Name);
            //页面链接
            db.AddInParameter(cmd, "@Page", DbType.String, iBase_WebAttributeDC.Page);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iBase_WebAttributeDC.Title);
            //关键字
            db.AddInParameter(cmd, "@Keywords", DbType.String, iBase_WebAttributeDC.Keywords);
            //说明
            db.AddInParameter(cmd, "@Description", DbType.String, iBase_WebAttributeDC.Description);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iBase_WebAttributeDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Base_WebAttribute_DELETE(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_WebAttribute SET Obj_Status = 6, Obj_Mdate = getdate() WHERE ID = @ID ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Base_WebAttributeDC> Base_WebAttribute_SELECT_List(string iName, string iPage,
            int iPageIndex, int iPageSize)
        {
            List<Base_WebAttributeDC> list = new List<Base_WebAttributeDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");
            sqlorder.Append(@" ID ");

            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append(" AND Name LIKE '%" + iName + "%' ");

            //名称
            if (!string.IsNullOrEmpty(iPage))
                sqlwhere.Append(" AND Page LIKE '%" + iPage + "%' ");

            var rtn = new RecordCountEntity<Base_WebAttributeDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Base_WebAttribute", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Base_WebAttribute", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Base_WebAttributeDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Base_WebAttributeDC Base_WebAttribute_SELECT_Entity(int iID)
        {
            Base_WebAttributeDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Base_WebAttribute WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Base_WebAttributeDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据Page查询
        /// </summary>
        /// <param name="iPage"></param>
        /// <returns></returns>
        public Base_WebAttributeDC web_Base_WebAttribute_SELECT_Entity(string iPage)
        {
            Base_WebAttributeDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Base_WebAttribute WHERE Page = @Page AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@Page", DbType.String, iPage);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Base_WebAttributeDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        #region 问卷

        /// <summary>
        /// 新增问卷
        /// </summary>
        /// <param name="iSurvey_MainDC"></param>
        /// <returns></returns>
        public int Survey_Main_ADD(Survey_MainDC iSurvey_MainDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Survey_Main(Title, Content, BeginDate, EndDate, CommitStatus, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Title, @Content, @BeginDate, @EndDate, @CommitStatus, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iSurvey_MainDC.Title);
            //
            db.AddInParameter(cmd, "@Content", DbType.String, iSurvey_MainDC.Content);
            //有效期开始
            db.AddInParameter(cmd, "@BeginDate", DbType.DateTime, iSurvey_MainDC.BeginDate);
            //有效期结束
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, iSurvey_MainDC.EndDate);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iSurvey_MainDC.CommitStatus);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iSurvey_MainDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新问卷
        /// </summary>
        /// <param name="iSurvey_MainDC">主键</param>
        /// <returns></returns>
        public bool Survey_Main_UPDATE(Survey_MainDC iSurvey_MainDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Survey_Main SET Title = @Title, Content = @Content, BeginDate = @BeginDate, EndDate = @EndDate, CommitStatus = @CommitStatus, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iSurvey_MainDC.ID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iSurvey_MainDC.Title);

            db.AddInParameter(cmd, "@Content", DbType.String, iSurvey_MainDC.Content);
            //有效期开始
            db.AddInParameter(cmd, "@BeginDate", DbType.DateTime, iSurvey_MainDC.BeginDate);
            //有效期结束
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, iSurvey_MainDC.EndDate);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iSurvey_MainDC.CommitStatus);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iSurvey_MainDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Survey_Main_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Survey_Main SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部问卷
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Survey_MainDC> Survey_Main_SELECT_List(
            string iTitle, int iPageIndex, int iPageSize)
        {
            List<Survey_MainDC> list = new List<Survey_MainDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");
            sqlorder.Append(@" ID ");

            //名称
            if (!string.IsNullOrEmpty(iTitle))
                sqlwhere.Append(" AND Title LIKE '%" + iTitle + "%' ");

            var rtn = new RecordCountEntity<Survey_MainDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Survey_Main", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Survey_Main", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Survey_MainDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 根据ID查询问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Survey_MainDC Survey_Main_SELECT_Entity(int iID)
        {
            Survey_MainDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Survey_Main WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Survey_MainDC.GetEntity(reader);
                    if (entity != null)
                    {
                        entity.QuestionList = Survey_Question_SELECT_List(entity.ID, true);
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC"></param>
        /// <returns></returns>
        public int Survey_Question_ADD(Survey_QuestionDC iSurvey_QuestionDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Survey_Question(SurID, Title, Type, Seq, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@SurID, @Title, @Type, @Seq, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //问卷表ID
            db.AddInParameter(cmd, "@SurID", DbType.Int32, iSurvey_QuestionDC.SurID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iSurvey_QuestionDC.Title);
            //问题类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iSurvey_QuestionDC.Type);
            //序号
            db.AddInParameter(cmd, "@Seq", DbType.Int32, iSurvey_QuestionDC.Seq);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iSurvey_QuestionDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            if (iSurvey_QuestionDC.OptionsList != null)
            {
                for (int i = 0; i < iSurvey_QuestionDC.OptionsList.Count; i++)
                {
                    iSurvey_QuestionDC.OptionsList[i].SurID = iSurvey_QuestionDC.SurID;
                    iSurvey_QuestionDC.OptionsList[i].QuID = id;
                    iSurvey_QuestionDC.OptionsList[i].Seq = Convert.ToInt32(Math.Pow(2, i));
                    Survey_Options_ADD(iSurvey_QuestionDC.OptionsList[i]);
                }
            }
            return id;
        }

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC">主键</param>
        /// <returns></returns>
        public bool Survey_Question_UPDATE(Survey_QuestionDC iSurvey_QuestionDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Survey_Question SET Title = @Title, Type = @Type, Seq = @Seq, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iSurvey_QuestionDC.ID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iSurvey_QuestionDC.Title);
            //问题类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iSurvey_QuestionDC.Type);
            //序号
            db.AddInParameter(cmd, "@Seq", DbType.Int32, iSurvey_QuestionDC.Seq);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iSurvey_QuestionDC);

            Survey_Options_DELETE(iSurvey_QuestionDC.ID);

            if (iSurvey_QuestionDC.OptionsList != null)
            {
                for (int i = 0; i < iSurvey_QuestionDC.OptionsList.Count; i++)
                {
                    iSurvey_QuestionDC.OptionsList[i].SurID = iSurvey_QuestionDC.SurID;
                    iSurvey_QuestionDC.OptionsList[i].QuID = iSurvey_QuestionDC.ID;
                    iSurvey_QuestionDC.OptionsList[i].Seq = Convert.ToInt32(Math.Pow(2, i));
                    Survey_Options_ADD(iSurvey_QuestionDC.OptionsList[i]);
                }
            }

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool Survey_Question_DELETE(int iID, int iMuser)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Survey_Question SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //主键
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //    //操作人
        //    db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        /// <summary>
        /// 查询全部问题
        /// </summary>
        /// <returns></returns>
        public IList<Survey_QuestionDC> Survey_Question_SELECT_List(int iSurID, bool iGetOptions = false)
        {
            List<Survey_QuestionDC> list = new List<Survey_QuestionDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Survey_Question WHERE SurID = @SurID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //有效数据
            db.AddInParameter(cmd, "@SurID", DbType.Int32, iSurID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var entity = Survey_QuestionDC.GetEntity(reader);
                    if (entity != null && iGetOptions)
                    {
                        entity.OptionsList = Survey_Options_SELECT_List(entity.ID);
                    }
                    list.Add(entity);

                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询问题
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Survey_QuestionDC Survey_Question_SELECT_Entity(int iID)
        {
            Survey_QuestionDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Survey_Question WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Survey_QuestionDC.GetEntity(reader);
                    if (entity != null)
                    {
                        entity.OptionsList = Survey_Options_SELECT_List(entity.ID);
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增选项
        /// </summary>
        /// <param name="iSurvey_OptionsDC"></param>
        /// <returns></returns>
        private int Survey_Options_ADD(Survey_OptionsDC iSurvey_OptionsDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Survey_Options(SurID, QuID, Name, Title, Type, Seq, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@SurID, @QuID, @Name, @Title, @Type, @Seq, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //问卷表ID
            db.AddInParameter(cmd, "@SurID", DbType.Int32, iSurvey_OptionsDC.SurID);
            //问题表ID
            db.AddInParameter(cmd, "@QuID", DbType.Int32, iSurvey_OptionsDC.QuID);
            //选项名
            db.AddInParameter(cmd, "@Name", DbType.String, iSurvey_OptionsDC.Name);
            //选项文字
            db.AddInParameter(cmd, "@Title", DbType.String, iSurvey_OptionsDC.Title);
            //选项类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iSurvey_OptionsDC.Type);
            //序号
            db.AddInParameter(cmd, "@Seq", DbType.Int32, iSurvey_OptionsDC.Seq);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iSurvey_OptionsDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="iSurvey_OptionsDC">主键</param>
        ///// <returns></returns>
        //public bool Survey_Options_UPDATE(Survey_OptionsDC iSurvey_OptionsDC)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Survey_Options SET SurID = @SurID, QuID = @QuID, Name = @Name, Title = @Title, Type = @Type, Seq = @Seq, SelPct = @SelPct, ");
        //    //字段拼接
        //    AddCommonUpdateSql(sql);
        //    sql.Append(" WHERE ID = @ID");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //主键
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iSurvey_OptionsDC.ID);
        //    //问卷表ID
        //    db.AddInParameter(cmd, "@SurID", DbType.Int32, iSurvey_OptionsDC.SurID);
        //    //问题表ID
        //    db.AddInParameter(cmd, "@QuID", DbType.Int32, iSurvey_OptionsDC.QuID);
        //    //选项名
        //    db.AddInParameter(cmd, "@Name", DbType.String, iSurvey_OptionsDC.Name);
        //    //选项文字
        //    db.AddInParameter(cmd, "@Title", DbType.String, iSurvey_OptionsDC.Title);
        //    //选项类型
        //    db.AddInParameter(cmd, "@Type", DbType.Int32, iSurvey_OptionsDC.Type);
        //    //序号
        //    db.AddInParameter(cmd, "@Seq", DbType.Int32, iSurvey_OptionsDC.Seq);
        //    //选择率
        //    db.AddInParameter(cmd, "@SelPct", DbType.Decimal, iSurvey_OptionsDC.SelPct);
        //    //添加共通参数
        //    AddCommonUpdateParameter(db, cmd, iSurvey_OptionsDC);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Survey_Options_DELETE(int iQuID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Survey_Options SET Obj_Status = 6, Obj_Mdate = getdate() WHERE QuID = @QuID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@QuID", DbType.Int32, iQuID);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool Survey_Options_DELETE(int iID, int iMuser)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Survey_Options SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //主键
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //    //操作人
        //    db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        /// <summary>
        /// 查询全部选项
        /// </summary>
        /// <returns></returns>
        private IList<Survey_OptionsDC> Survey_Options_SELECT_List(int iQuID)
        {
            List<Survey_OptionsDC> list = new List<Survey_OptionsDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Survey_Options WHERE QuID = @QuID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //有效数据
            db.AddInParameter(cmd, "@QuID", DbType.Int32, iQuID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Survey_OptionsDC.GetEntity(reader));
                }
            }
            return list;
        }

        ///// <summary>
        ///// 根据ID查询
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <returns></returns>
        //public Survey_OptionsDC Survey_Options_SELECT_BYID(int iID)
        //{
        //    Survey_OptionsDC entity = null;
        //    Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Query);
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("SELECT * FROM Survey_Options WHERE ID = @ID AND Obj_Status = @Obj_Status");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //主键
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //    //有效数据
        //    db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
        //    using (IDataReader reader = db.ExecuteReader(cmd))
        //    {
        //        if (reader.Read())
        //        {
        //            entity = Survey_OptionsDC.GetEntity(reader);
        //        }
        //    }
        //    return entity;
        //}

        /// <summary>
        /// 新增回答
        /// </summary>
        /// <param name="iSurvey_AnswerDC"></param>
        /// <returns></returns>
        public int Survey_Answer_ADD(Survey_AnswerDC iSurvey_AnswerDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Survey_Main SET UserCount = UserCount + 1 WHERE ID = @SurID;");
            sql.Append("INSERT INTO Survey_Answer(SurID, UserID, UserSource, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@SurID, @UserID, @UserSource, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //问卷表ID
            db.AddInParameter(cmd, "@SurID", DbType.Int32, iSurvey_AnswerDC.SurID);
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iSurvey_AnswerDC.UserID);
            //用户来源
            db.AddInParameter(cmd, "@UserSource", DbType.String, iSurvey_AnswerDC.UserSource);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iSurvey_AnswerDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            if (iSurvey_AnswerDC.DetailList != null)
            {
                foreach (var item in iSurvey_AnswerDC.DetailList)
                {
                    item.AnsID = id;
                    Survey_Answer_ADD(item);
                }
            }



            return id;
        }

        /// <summary>
        /// 新增回答
        /// </summary>
        /// <param name="iSurvey_AnswerDetailDC"></param>
        /// <returns></returns>
        private int Survey_Answer_ADD(Survey_AnswerDetailDC iSurvey_AnswerDetailDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Survey_AnswerDetail(AnsID, QuID, AnsValue, AnsContent, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@AnsID, @QuID, @AnsValue, @AnsContent, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //问卷表ID
            db.AddInParameter(cmd, "@AnsID", DbType.Int32, iSurvey_AnswerDetailDC.AnsID);
            //问题表ID
            db.AddInParameter(cmd, "@QuID", DbType.Int32, iSurvey_AnswerDetailDC.QuID);
            //答案值
            db.AddInParameter(cmd, "@AnsValue", DbType.Int32, iSurvey_AnswerDetailDC.AnsValue);
            //回答内容
            db.AddInParameter(cmd, "@AnsContent", DbType.String, iSurvey_AnswerDetailDC.AnsContent);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iSurvey_AnswerDetailDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="iSurvey_AnswerDC">主键</param>
        ///// <returns></returns>
        //public bool Survey_Answer_UPDATE(Survey_AnswerDC iSurvey_AnswerDC)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Survey_Answer SET SurID = @SurID, QuID = @QuID, AnsValue = @AnsValue, AnsContent = @AnsContent, UserID = @UserID, UserSource = @UserSource, ");
        //    //字段拼接
        //    AddCommonUpdateSql(sql);
        //    sql.Append(" WHERE ID = @ID");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //主键
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iSurvey_AnswerDC.ID);
        //    //问卷表ID
        //    db.AddInParameter(cmd, "@SurID", DbType.Int32, iSurvey_AnswerDC.SurID);
        //    //问题表ID
        //    db.AddInParameter(cmd, "@QuID", DbType.Int32, iSurvey_AnswerDC.QuID);
        //    //答案值
        //    db.AddInParameter(cmd, "@AnsValue", DbType.Int32, iSurvey_AnswerDC.AnsValue);
        //    //回答内容
        //    db.AddInParameter(cmd, "@AnsContent", DbType.String, iSurvey_AnswerDC.AnsContent);
        //    //用户ID
        //    db.AddInParameter(cmd, "@UserID", DbType.Guid, iSurvey_AnswerDC.UserID);
        //    //用户来源
        //    db.AddInParameter(cmd, "@UserSource", DbType.String, iSurvey_AnswerDC.UserSource);
        //    //添加共通参数
        //    AddCommonUpdateParameter(db, cmd, iSurvey_AnswerDC);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool Survey_Answer_DELETE(int iID, int iMuser)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Survey_Answer SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //主键
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //    //操作人
        //    db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        /// <summary>
        /// 查询一个问卷的全部用户
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Survey_AnswerDC> Survey_Answer_SELECT_List(int? iSurID,
            string iUserSource, int iPageIndex, int iPageSize)
        {
            List<Survey_AnswerDC> list = new List<Survey_AnswerDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Survey_Answer.Obj_Status = 1 ");
            sqlwhere.Append(@" AND  Survey_Answer.SurID = Survey_Main.ID ");
            sqlorder.Append(@" Survey_Answer.ID DESC");

            //问卷ID
            if (iSurID.HasValue)
            {
                sqlwhere.Append(" AND Survey_Answer.SurID = " + iSurID + " ");
            }
            //用户
            if (!string.IsNullOrEmpty(iUserSource))
                sqlwhere.Append(" AND Survey_Answer.UserSource LIKE '%" + iUserSource + "%' ");

            var rtn = new RecordCountEntity<Survey_AnswerDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Survey_Answer,Survey_Main", "Survey_Answer.ID", "Survey_Answer.*,Survey_Main.Title", sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Survey_Answer,Survey_Main", "Survey_Answer.ID", "Survey_Answer.*,Survey_Main.Title", sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Survey_AnswerDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 查询一个问卷一个用户全部回答
        /// </summary>
        /// <returns></returns>
        private IList<Survey_AnswerDetailDC> Survey_AnswerDetail_SELECT_List(int iAnsID)
        {
            List<Survey_AnswerDetailDC> list = new List<Survey_AnswerDetailDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM Survey_AnswerDetail WHERE AnsID = @AnsID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@AnsID", DbType.Int32, iAnsID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Survey_AnswerDetailDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Survey_AnswerDC Survey_Answer_SELECT_Entity(int iID)
        {
            Survey_AnswerDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Survey_Answer WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Survey_AnswerDC.GetEntity(reader);
                }
            }

            if (entity != null)
            {
                entity.DetailList = Survey_AnswerDetail_SELECT_List(entity.ID);
            }

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSurveyID"></param>
        /// <returns></returns>
        public bool Survey_Stat(int iSurveyID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Survey_Stat");

            db.AddInParameter(cmd, "@SurveyID", DbType.Int32, iSurveyID);

            db.ExecuteNonQuery(cmd);

            return true;
        }

        #endregion

        #region Base_Notify

        /// <summary>
        /// 新增 Base_Notify
        /// </summary>
        /// <param name="iBase_NotifyDC"></param>
        /// <returns></returns>
        public int Base_Notify_ADD(Base_NotifyDC iBase_NotifyDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Base_Notify(EventNumber, OrderNumber, RoleID, PersonnelID, Class, Title, Content, Level, Status, Result, NotifyUserList, IsSms, IsEmail, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@EventNumber, @OrderNumber, @RoleID, @PersonnelID, @Class, @Title, @Content, @Level, @Status, @Result, @NotifyUserList, @IsSms, @IsEmail, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@EventNumber", DbType.String, iBase_NotifyDC.EventNumber);
            //
            db.AddInParameter(cmd, "@OrderNumber", DbType.String, iBase_NotifyDC.OrderNumber);
            //
            db.AddInParameter(cmd, "@RoleID", DbType.Int32, iBase_NotifyDC.RoleID);
            //
            db.AddInParameter(cmd, "@PersonnelID", DbType.Int32, iBase_NotifyDC.PersonnelID);
            //
            db.AddInParameter(cmd, "@Class", DbType.Int32, iBase_NotifyDC.Class);
            //
            db.AddInParameter(cmd, "@Title", DbType.String, iBase_NotifyDC.Title);
            //
            db.AddInParameter(cmd, "@Content", DbType.String, iBase_NotifyDC.Content);
            //
            db.AddInParameter(cmd, "@Level", DbType.Int32, iBase_NotifyDC.Level);
            //
            db.AddInParameter(cmd, "@Status", DbType.Int32, iBase_NotifyDC.Status);
            //
            db.AddInParameter(cmd, "@Result", DbType.String, iBase_NotifyDC.Result);
            //
            db.AddInParameter(cmd, "@NotifyUserList", DbType.String, iBase_NotifyDC.NotifyUserList);
            //
            db.AddInParameter(cmd, "@IsSms", DbType.Boolean, iBase_NotifyDC.IsSms);
            //
            db.AddInParameter(cmd, "@IsEmail", DbType.Boolean, iBase_NotifyDC.IsEmail);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iBase_NotifyDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新Base_Notify
        /// </summary>
        /// <param name="iBase_NotifyDC"></param>
        /// <returns></returns>
        public bool Base_Notify_UPDATE(Base_NotifyDC iBase_NotifyDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Notify SET EventNumber = @EventNumber, OrderNumber = @OrderNumber, RoleID = @RoleID, PersonnelID = @PersonnelID, Class = @Class, Title = @Title, Content = @Content, Level = @Level, Status = @Status, Result = @Result, NotifyUserList = @NotifyUserList, IsSms = @IsSms, IsEmail = @IsEmail, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iBase_NotifyDC.ID);
            //
            db.AddInParameter(cmd, "@EventNumber", DbType.String, iBase_NotifyDC.EventNumber);
            //
            db.AddInParameter(cmd, "@OrderNumber", DbType.String, iBase_NotifyDC.OrderNumber);
            //
            db.AddInParameter(cmd, "@RoleID", DbType.Int32, iBase_NotifyDC.RoleID);
            //
            db.AddInParameter(cmd, "@PersonnelID", DbType.Int32, iBase_NotifyDC.PersonnelID);
            //
            db.AddInParameter(cmd, "@Class", DbType.Int32, iBase_NotifyDC.Class);
            //
            db.AddInParameter(cmd, "@Title", DbType.String, iBase_NotifyDC.Title);
            //
            db.AddInParameter(cmd, "@Content", DbType.String, iBase_NotifyDC.Content);
            //
            db.AddInParameter(cmd, "@Level", DbType.Int32, iBase_NotifyDC.Level);
            //
            db.AddInParameter(cmd, "@Status", DbType.Int32, iBase_NotifyDC.Status);
            //
            db.AddInParameter(cmd, "@Result", DbType.String, iBase_NotifyDC.Result);
            //
            db.AddInParameter(cmd, "@NotifyUserList", DbType.String, iBase_NotifyDC.NotifyUserList);
            //
            db.AddInParameter(cmd, "@IsSms", DbType.Boolean, iBase_NotifyDC.IsSms);
            //
            db.AddInParameter(cmd, "@IsEmail", DbType.Boolean, iBase_NotifyDC.IsEmail);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iBase_NotifyDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除Base_Notify
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Base_Notify_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Notify SET Obj_Status = 6, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询Base_Notify
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Base_NotifyDC> Base_Notify_SELECT_List(string iEventNumber, string iOrderNumber,
            int? iRoleID, int? iPersonnelID, string iTitle, int? iLevel, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            List<Base_NotifyDC> list = new List<Base_NotifyDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            StringBuilder sqlfields = new StringBuilder();

            sqlfields.Append(" Base_Notify.*,PR_Operator.Name AS PersonnelName ");

            sqlorder.Append(@" Base_Notify.ID DESC ");

            sqlwhere.Append(@" Base_Notify.Obj_Status = 1 ");

            //事件编号
            if (!string.IsNullOrEmpty(iEventNumber))
                sqlwhere.Append(" AND Base_Notify.EventNumber = '" + iEventNumber + "' ");
            //相关订单号
            if (!string.IsNullOrEmpty(iOrderNumber))
                sqlwhere.Append(" AND Base_Notify.OrderNumber = '" + iOrderNumber + "' ");
            //职位
            if (iRoleID.HasValue && iRoleID.Value != RoleDC.Role_Admin)
                sqlwhere.Append(" AND Base_Notify.RoleID = " + iRoleID + " ");
            //处理人
            if (iPersonnelID.HasValue && iRoleID.Value != RoleDC.Role_Admin && iRoleID.Value != RoleDC.Role_CustomerService)
            {
                sqlwhere.Append(" AND Base_Notify.PersonnelID = " + iPersonnelID + " ");
            }
            //标题
            if (!string.IsNullOrEmpty(iTitle))
                //sqlwhere.Append(" AND Base_Notify.Title LIKE '%" + DBHelper.SqlLikeReplace(iTitle) + "%' ");
                sqlwhere.Append(" AND Base_Notify.Title LIKE '%" + iTitle + "%' ");
            //事件等级
            if (iLevel.HasValue)
                sqlwhere.Append(" AND Base_Notify.Level = " + iLevel + " ");
            //处理状态
            if (iStatus.HasValue)
            {
                if (iStatus == 100)
                {
                    sqlwhere.Append(" AND Base_Notify.Status IN (0,1)");
                }
                else
                {
                    sqlwhere.Append(" AND Base_Notify.Status = " + iStatus + " ");
                }
            }
            var rtn = new RecordCountEntity<Base_NotifyDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Base_Notify LEFT JOIN PR_Operator ON Base_Notify.PersonnelID = PR_Operator.ID ", "Base_Notify.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Base_Notify LEFT JOIN PR_Operator ON Base_Notify.PersonnelID = PR_Operator.ID ", "Base_Notify.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Base_NotifyDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 根据ID查询Base_Notify
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Base_NotifyDC Base_Notify_SELECT_Entity(int iID)
        {
            Base_NotifyDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Base_Notify.*,PR_Operator.Name AS PersonnelName 
FROM Base_Notify LEFT JOIN PR_Operator ON Base_Notify.PersonnelID = PR_Operator.ID WHERE Base_Notify.ID = @ID AND Base_Notify.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Base_NotifyDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Base_Notify_UPDATE_Get(int iNotifyID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Notify SET Status = 1, PersonnelID = @Muser, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND Status IN(0,1)");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iNotifyID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新备注
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Base_Notify_UPDATE_Result(int iNotifyID, string iResult, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Notify SET Result = @Result, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iNotifyID);
            //
            db.AddInParameter(cmd, "@Result", DbType.String, iResult);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 完成处理
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iStatus"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Base_Notify_UPDATE_Finish(int iNotifyID, string iResult, int iStatus, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Base_Notify SET Status = @Status, Result = @Result, PersonnelID = @Muser, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND Status IN(0,1)");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iNotifyID);

            db.AddInParameter(cmd, "@Status", DbType.Int32, iStatus);

            db.AddInParameter(cmd, "@Result", DbType.String, iResult);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #endregion

    }
}
