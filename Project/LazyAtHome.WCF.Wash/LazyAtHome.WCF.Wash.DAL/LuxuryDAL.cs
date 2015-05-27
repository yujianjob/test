using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.DAL
{
    public class LuxuryDAL : DALBase, LazyAtHome.WCF.Wash.Interface.IDAL.ILuxuryDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LuxuryDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iLuxury_ClassDC"></param>
        /// <returns></returns>
        public int Luxury_Class_ADD(Luxury_ClassDC iLuxury_ClassDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Luxury_Class(Name, ParentID, Code, Sort, Enable, PictureL, PictureM, PictureS, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @ParentID, @Code, @Sort, @Enable, @PictureL, @PictureM, @PictureS, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iLuxury_ClassDC.Name);
            //上级ID
            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iLuxury_ClassDC.ParentID);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iLuxury_ClassDC.Code);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iLuxury_ClassDC.Sort);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iLuxury_ClassDC.Enable);
            //图片
            db.AddInParameter(cmd, "@PictureL", DbType.String, iLuxury_ClassDC.PictureL);
            //图片
            db.AddInParameter(cmd, "@PictureM", DbType.String, iLuxury_ClassDC.PictureM);
            //图片
            db.AddInParameter(cmd, "@PictureS", DbType.String, iLuxury_ClassDC.PictureS);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iLuxury_ClassDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iLuxury_ClassDC">主键</param>
        /// <returns></returns>
        public bool Luxury_Class_UPDATE(Luxury_ClassDC iLuxury_ClassDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Luxury_Class SET Name = @Name, ParentID = @ParentID, Code = @Code, Sort = @Sort, Enable = @Enable, PictureL = @PictureL, PictureM = @PictureM, PictureS = @PictureS, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iLuxury_ClassDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iLuxury_ClassDC.Name);
            //上级ID
            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iLuxury_ClassDC.ParentID);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iLuxury_ClassDC.Code);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iLuxury_ClassDC.Sort);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iLuxury_ClassDC.Enable);
            //图片
            db.AddInParameter(cmd, "@PictureL", DbType.String, iLuxury_ClassDC.PictureL);
            //图片
            db.AddInParameter(cmd, "@PictureM", DbType.String, iLuxury_ClassDC.PictureM);
            //图片
            db.AddInParameter(cmd, "@PictureS", DbType.String, iLuxury_ClassDC.PictureS);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iLuxury_ClassDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Luxury_Class_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Luxury_Class SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IList<Luxury_ClassDC> Luxury_Class_SELECT_ALL()
        {
            List<Luxury_ClassDC> list = new List<Luxury_ClassDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Luxury_Class WHERE Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Luxury_ClassDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Luxury_ClassDC Luxury_Class_SELECT_Entity(int iID)
        {
            Luxury_ClassDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Luxury_Class WHERE ID = @ID AND Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Luxury_ClassDC.GetEntity(reader);
                }
            }
            return entity;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iLuxury_ProductDC"></param>
        /// <returns></returns>
        public int Luxury_Product_ADD(Luxury_ProductDC iLuxury_ProductDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Luxury_Product(Name, ClassID, Keyword, Content, Site, Sort, ServiceFeeRate, MinPrice, MaxPrice, IntervalPrice, CommitStatus, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @ClassID, @Keyword, @Content, @Site, @Sort, @ServiceFeeRate, @MinPrice, @MaxPrice, @IntervalPrice, @CommitStatus, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iLuxury_ProductDC.Name);
            //类别ID
            db.AddInParameter(cmd, "@ClassID", DbType.Int32, iLuxury_ProductDC.ClassID);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iLuxury_ProductDC.Keyword);
            //描述
            db.AddInParameter(cmd, "@Content", DbType.String, iLuxury_ProductDC.Content);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iLuxury_ProductDC.Site);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iLuxury_ProductDC.Sort);
            //服务费率
            db.AddInParameter(cmd, "@ServiceFeeRate", DbType.Decimal, iLuxury_ProductDC.ServiceFeeRate);
            //最低价
            db.AddInParameter(cmd, "@MinPrice", DbType.Int32, iLuxury_ProductDC.MinPrice);
            //最高价
            db.AddInParameter(cmd, "@MaxPrice", DbType.Int32, iLuxury_ProductDC.MaxPrice);
            //价格区间
            db.AddInParameter(cmd, "@IntervalPrice", DbType.Int32, iLuxury_ProductDC.IntervalPrice);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iLuxury_ProductDC.CommitStatus);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iLuxury_ProductDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iLuxury_ProductDC">主键</param>
        /// <returns></returns>
        public bool Luxury_Product_UPDATE(Luxury_ProductDC iLuxury_ProductDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Luxury_Product SET Name = @Name, ClassID = @ClassID, Keyword = @Keyword, Content = @Content, Site = @Site, Sort = @Sort, ServiceFeeRate = @ServiceFeeRate, MinPrice = @MinPrice, MaxPrice = @MaxPrice, IntervalPrice = @IntervalPrice, CommitStatus = @CommitStatus, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iLuxury_ProductDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iLuxury_ProductDC.Name);
            //类别ID
            db.AddInParameter(cmd, "@ClassID", DbType.Int32, iLuxury_ProductDC.ClassID);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iLuxury_ProductDC.Keyword);
            //描述
            db.AddInParameter(cmd, "@Content", DbType.String, iLuxury_ProductDC.Content);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iLuxury_ProductDC.Site);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iLuxury_ProductDC.Sort);
            //服务费率
            db.AddInParameter(cmd, "@ServiceFeeRate", DbType.Decimal, iLuxury_ProductDC.ServiceFeeRate);
            //最低价
            db.AddInParameter(cmd, "@MinPrice", DbType.Int32, iLuxury_ProductDC.MinPrice);
            //最高价
            db.AddInParameter(cmd, "@MaxPrice", DbType.Int32, iLuxury_ProductDC.MaxPrice);
            //价格区间
            db.AddInParameter(cmd, "@IntervalPrice", DbType.Int32, iLuxury_ProductDC.IntervalPrice);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iLuxury_ProductDC.CommitStatus);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iLuxury_ProductDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Luxury_Product_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Luxury_Product SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IList<Luxury_ProductDC> Luxury_Product_SELECT_ALL()
        {
            List<Luxury_ProductDC> list = new List<Luxury_ProductDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Luxury_Product WHERE Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Luxury_ProductDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Luxury_ProductDC Luxury_Product_SELECT_Entity(int iID)
        {
            Luxury_ProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Luxury_Product WHERE ID = @ID AND Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Luxury_ProductDC.GetEntity(reader);
                }
            }
            return entity;
        }

    }
}
