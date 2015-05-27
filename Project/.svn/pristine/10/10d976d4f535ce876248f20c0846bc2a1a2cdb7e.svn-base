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
    public class ProductDAL : DALBase, LazyAtHome.WCF.Wash.Interface.IDAL.IProductDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProductDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        /// <summary>
        /// 创建code
        /// </summary>
        /// <param name="iParentID"></param>
        /// <returns></returns>
        public string Wash_Class_ADD_CreateCode(int iParentID)
        {
            var code = string.Empty;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            if (iParentID == 0)
            {
                sql.Append("SELECT RIGHT('000' + CONVERT(VARCHAR,COUNT(0) + 1,4),4) FROM Wash_Class WHERE ParentID = 0");
            }
            else
            {
                sql.Append(@"SELECT (SELECT CODE FROM Wash_Class WHERE ID = @ParentID) + 
                    RIGHT('000' + CONVERT(VARCHAR,COUNT(0) + 1,4),4) 
                    FROM Wash_Class WHERE ParentID = @ParentID");
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ParentID", DbType.String, iParentID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    code = reader.GetString(0);
                }
            }
            return code;
        }

        /// <summary>
        /// 新增类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        public int Wash_Class_ADD(Wash_ClassDC iWash_ClassDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Class(Name, ParentID, Code, IsDefault, Sort, Enable, PictureL, PictureM, PictureS, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @ParentID, @Code, @IsDefault, @Sort, @Enable, @PictureL, @PictureM, @PictureS, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_ClassDC.Name);
            //上级ID
            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iWash_ClassDC.ParentID);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iWash_ClassDC.Code);
            //预定义不可修改
            db.AddInParameter(cmd, "@IsDefault", DbType.Int32, iWash_ClassDC.IsDefault);
            //
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_ClassDC.Sort);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_ClassDC.Enable);

            db.AddInParameter(cmd, "@PictureL", DbType.String, iWash_ClassDC.PictureL);

            db.AddInParameter(cmd, "@PictureM", DbType.String, iWash_ClassDC.PictureM);

            db.AddInParameter(cmd, "@PictureS", DbType.String, iWash_ClassDC.PictureS);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_ClassDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        public bool Wash_Class_UPDATE(Wash_ClassDC iWash_ClassDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Wash_Class SET Name = @Name, Sort = @Sort, Enable = @Enable, PictureL = @PictureL, PictureM = @PictureM, PictureS = @PictureS, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iWash_ClassDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_ClassDC.Name);
            //
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_ClassDC.Sort);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_ClassDC.Enable);

            db.AddInParameter(cmd, "@PictureL", DbType.String, iWash_ClassDC.PictureL);

            db.AddInParameter(cmd, "@PictureM", DbType.String, iWash_ClassDC.PictureM);

            db.AddInParameter(cmd, "@PictureS", DbType.String, iWash_ClassDC.PictureS);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iWash_ClassDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Wash_ClassDC Wash_Class_SELECT_Entity(int iID)
        {
            Wash_ClassDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Class WHERE ID = @ID AND Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_ClassDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 产品类别表
        /// </summary>
        /// <returns></returns>
        public IList<Wash_ClassDC> Wash_Class_SELECT_List_ALL()
        {
            List<Wash_ClassDC> list = new List<Wash_ClassDC>();
            Wash_ClassDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Class WHERE Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Wash_ClassDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 创建code
        /// </summary>
        /// <param name="iParentID"></param>
        /// <returns></returns>
        public string Wash_Attribute_ADD_CreateCode(int iParentID)
        {
            var code = string.Empty;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            if (iParentID == 0)
            {
                sql.Append("SELECT RIGHT('000' + CONVERT(VARCHAR,COUNT(0) + 1,4),4) FROM Wash_Attribute WHERE ParentID = 0");
            }
            else
            {
                sql.Append(@"SELECT (SELECT CODE FROM Wash_Attribute WHERE ID = @ParentID) + 
                    RIGHT('000' + CONVERT(VARCHAR,COUNT(0) + 1,4),4) 
                    FROM Wash_Attribute WHERE ParentID = @ParentID");
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ParentID", DbType.String, iParentID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    code = reader.GetString(0);
                }
            }
            return code;
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        public int Wash_Attribute_ADD(Wash_AttributeDC iWash_AttributeDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Attribute(Name, ParentID, Code, IsDefault, Sort, Enable, Content, SelectType, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @ParentID, @Code, @IsDefault, @Sort, @Enable, @Content, @SelectType, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_AttributeDC.Name);
            //上级ID
            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iWash_AttributeDC.ParentID);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iWash_AttributeDC.Code);
            //预定义不可修改
            db.AddInParameter(cmd, "@IsDefault", DbType.Int32, iWash_AttributeDC.IsDefault);
            //
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_AttributeDC.Sort);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_AttributeDC.Enable);
            //启用
            db.AddInParameter(cmd, "@Content", DbType.String, iWash_AttributeDC.Content);
            //属性类型
            db.AddInParameter(cmd, "@SelectType", DbType.Int32, iWash_AttributeDC.SelectType);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_AttributeDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        public bool Wash_Attribute_UPDATE(Wash_AttributeDC iWash_AttributeDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Wash_Attribute SET Name = @Name, Sort = @Sort, Enable = @Enable, Content = @Content, SelectType = @SelectType, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iWash_AttributeDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_AttributeDC.Name);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_AttributeDC.Sort);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_AttributeDC.Enable);
            //说明
            db.AddInParameter(cmd, "@Content", DbType.String, iWash_AttributeDC.Content);
            //属性类型
            db.AddInParameter(cmd, "@SelectType", DbType.Int32, iWash_AttributeDC.SelectType);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iWash_AttributeDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 根据ID查询属性
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Wash_AttributeDC Wash_Attribute_SELECT_Entity(int iID)
        {
            Wash_AttributeDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Attribute WHERE ID = @ID AND Obj_Status = @Obj_Status");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //有效数据
            db.AddInParameter(cmd, "@Obj_Status", DbType.Byte, 1);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_AttributeDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 类别表
        /// </summary>
        /// <returns></returns>
        public IList<Wash_AttributeDC> Wash_Attribute_SELECT_List_ALL()
        {
            List<Wash_AttributeDC> list = new List<Wash_AttributeDC>();
            Wash_AttributeDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Attribute WHERE Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Wash_AttributeDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }
            return list;
        }

        #region 产品

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        public int Wash_Category_ADD(Wash_CategoryDC iWash_CategoryDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Category(Name, Code, ClassID, Unit, Enable, Keyword, Content, PictureL, PictureM, PictureS, PictureAlt, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @Code, @ClassID, @Unit, @Enable, @Keyword, @Content, @PictureL, @PictureM, @PictureS, @PictureAlt, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_CategoryDC.Name);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iWash_CategoryDC.Code);
            //类别表ID
            db.AddInParameter(cmd, "@ClassID", DbType.Int32, iWash_CategoryDC.ClassID);
            //单位
            db.AddInParameter(cmd, "@Unit", DbType.String, iWash_CategoryDC.Unit);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_CategoryDC.Enable);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iWash_CategoryDC.Keyword);
            //启用
            db.AddInParameter(cmd, "@Content", DbType.String, iWash_CategoryDC.Content);
            //大图
            db.AddInParameter(cmd, "@PictureL", DbType.String, iWash_CategoryDC.PictureL);
            //中图
            db.AddInParameter(cmd, "@PictureM", DbType.String, iWash_CategoryDC.PictureM);
            //小图
            db.AddInParameter(cmd, "@PictureS", DbType.String, iWash_CategoryDC.PictureS);
            //图片描述
            db.AddInParameter(cmd, "@PictureAlt", DbType.String, iWash_CategoryDC.PictureAlt);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_CategoryDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            //if (iWash_CategoryDC.AttributeList != null)
            //{
            //    foreach (var attribute in iWash_CategoryDC.AttributeList)
            //    {
            //        Wash_CategoryAttribute_ADD(id, attribute.ID);
            //    }
            //}

            return id;
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        public bool Wash_Category_UPDATE(Wash_CategoryDC iWash_CategoryDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Wash_Category SET Name = @Name, Code = @Code, ClassID = @ClassID, 
                Unit = @Unit, Enable = @Enable, Keyword = @Keyword, Content = @Content, 
                PictureL = @PictureL, PictureM = @PictureM, PictureS = @PictureS, PictureAlt = @PictureAlt, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iWash_CategoryDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_CategoryDC.Name);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iWash_CategoryDC.Code);
            //类别表ID
            db.AddInParameter(cmd, "@ClassID", DbType.Int32, iWash_CategoryDC.ClassID);
            //单位
            db.AddInParameter(cmd, "@Unit", DbType.String, iWash_CategoryDC.Unit);
            //启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_CategoryDC.Enable);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iWash_CategoryDC.Keyword);
            //启用
            db.AddInParameter(cmd, "@Content", DbType.String, iWash_CategoryDC.Content);
            //大图
            db.AddInParameter(cmd, "@PictureL", DbType.String, iWash_CategoryDC.PictureL);
            //中图
            db.AddInParameter(cmd, "@PictureM", DbType.String, iWash_CategoryDC.PictureM);
            //小图
            db.AddInParameter(cmd, "@PictureS", DbType.String, iWash_CategoryDC.PictureS);
            //图片描述
            db.AddInParameter(cmd, "@PictureAlt", DbType.String, iWash_CategoryDC.PictureAlt);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iWash_CategoryDC);
            var rtn = db.ExecuteNonQuery(cmd) > 0 ? true : false;

            //Wash_CategoryAttribute_DELETE(iWash_CategoryDC.ID);

            //if (iWash_CategoryDC.AttributeList != null)
            //{
            //    foreach (var attribute in iWash_CategoryDC.AttributeList)
            //    {
            //        Wash_CategoryAttribute_ADD(iWash_CategoryDC.ID, attribute.ID);
            //    }
            //}

            return rtn;
        }

        ///// <summary>
        ///// 新增产品属性
        ///// </summary>
        ///// <param name="iCategoryID"></param>
        ///// <param name="iAttributeID"></param>
        ///// <returns></returns>
        //public bool Wash_CategoryAttribute_ADD(int iCategoryID, int iAttributeID)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("INSERT INTO Wash_CategoryAttribute(CategoryID, AttributeID, Obj_Cuser, Obj_Muser");
        //    sql.Append(") VALUES (@CategoryID, @AttributeID,-1,-1); ");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //产品ID
        //    db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iCategoryID);
        //    //属性ID
        //    db.AddInParameter(cmd, "@AttributeID", DbType.Int32, iAttributeID);

        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        //        /// <summary>
        //        /// 产品属性查询
        //        /// </summary>
        //        /// <param name="iCategoryID"></param>
        //        /// <returns></returns>
        //        public IList<Wash_AttributeDC> Wash_CategoryAttribute_SELECT(int iCategoryID)
        //        {
        //            List<Wash_AttributeDC> list = new List<Wash_AttributeDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT Wash_Attribute.* 
        //                        FROM Wash_CategoryAttribute,Wash_Attribute
        //                        WHERE Wash_CategoryAttribute.AttributeID = Wash_Attribute.ID
        //                        AND Wash_CategoryAttribute.CategoryID = @CategoryID 
        //                        AND Wash_CategoryAttribute.Obj_Status = 1");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

        //            db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iCategoryID);

        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(Wash_AttributeDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        ///// <summary>
        ///// 删除产品属性关联
        ///// </summary>
        ///// <param name="iID"></param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool Wash_CategoryAttribute_DELETE(int iCategoryID)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Wash_CategoryAttribute SET Obj_Status = 6, Obj_Mdate = getdate() WHERE CategoryID = @CategoryID AND Obj_Status = 1");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //
        //    db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iCategoryID);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        /// <summary>
        /// 产品单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Wash_CategoryDC Wash_Category_SELECT_Entity(int iID)
        {
            Wash_CategoryDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM Wash_Category
                                    WHERE ID = @ID
                                    AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_CategoryDC.GetEntity(reader);
                }
            }
            //if (entity != null)
            //{
            //    entity.AttributeList = Wash_CategoryAttribute_SELECT(entity.ID);
            //}
            return entity;
        }

        /// <summary>
        /// 产品列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iEnable"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Wash_CategoryDC> Wash_Category_SELECT_List(string iName, string iCode, int? iEnable,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<Wash_CategoryDC> list = new List<Wash_CategoryDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@"Obj_Status = 1 ");
            sqlorder.Append(@" ID ");

            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append("AND Name LIKE '%" + iName + "%' ");
            //编号
            if (!string.IsNullOrEmpty(iCode))
                sqlwhere.Append("AND Code LIKE '%" + iCode + "%' ");
            //状态
            if (iEnable.HasValue)
                sqlwhere.Append("AND Enable = '" + iEnable + "' ");
            //注册时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //注册时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Wash_CategoryDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Wash_Category", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Wash_Category", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Wash_CategoryDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 产品列表页
        /// </summary>
        /// <returns></returns>
        public IList<Wash_CategoryDC> Wash_Category_SELECT_List_ByClassID(int iClassID)
        {
            List<Wash_CategoryDC> list = new List<Wash_CategoryDC>();
            Wash_CategoryDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Category WHERE ClassID = @ClassID AND Enable = 1 AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ClassID", DbType.Int32, iClassID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Wash_CategoryDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 新增产品价目
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        public int Wash_Product_ADD(Wash_ProductDC iWash_ProductDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Product(Name, WebName, Code, CategoryID, Keyword, Content, Type, MarketPrice, SalesPrice, Site, Sort, RecSort, [Group], VerifyStatus, SubmitOperatorID, SubmitDate, AduitOperatorID, AduitDate, Comment, CommitStatus, OnOperatorID, OnDate, OffOperatorID, OffDate, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Name, @WebName, @Code, @CategoryID, @Keyword, @Content, @Type, @MarketPrice, @SalesPrice, @Site, @Sort, @RecSort, @Group, @VerifyStatus, @SubmitOperatorID, @SubmitDate, @AduitOperatorID, @AduitDate, @Comment, @CommitStatus, @OnOperatorID, @OnDate, @OffOperatorID, @OffDate, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_ProductDC.Name);
            //名称
            db.AddInParameter(cmd, "@WebName", DbType.String, iWash_ProductDC.WebName);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iWash_ProductDC.Code);
            //产品ID
            db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iWash_ProductDC.CategoryID);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iWash_ProductDC.Keyword);
            //描述
            db.AddInParameter(cmd, "@Content", DbType.String, iWash_ProductDC.Content);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iWash_ProductDC.Type);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_ProductDC.Sort);
            //推荐排序
            db.AddInParameter(cmd, "@RecSort", DbType.Int32, iWash_ProductDC.RecSort);
            //分组
            db.AddInParameter(cmd, "@Group", DbType.Int32, iWash_ProductDC.Group);
            //市场价
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, iWash_ProductDC.MarketPrice);
            //销售价
            db.AddInParameter(cmd, "@SalesPrice", DbType.Decimal, iWash_ProductDC.SalesPrice);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iWash_ProductDC.Site);
            //审核状态
            db.AddInParameter(cmd, "@VerifyStatus", DbType.Int32, iWash_ProductDC.VerifyStatus);
            //审核提交人ID
            db.AddInParameter(cmd, "@SubmitOperatorID", DbType.Int32, iWash_ProductDC.SubmitOperatorID);
            //提交审核时间
            db.AddInParameter(cmd, "@SubmitDate", DbType.DateTime, iWash_ProductDC.SubmitDate);
            //审核人ID
            db.AddInParameter(cmd, "@AduitOperatorID", DbType.Int32, iWash_ProductDC.AduitOperatorID);
            //审核时间
            db.AddInParameter(cmd, "@AduitDate", DbType.DateTime, iWash_ProductDC.AduitDate);
            //审核人意见
            db.AddInParameter(cmd, "@Comment", DbType.String, iWash_ProductDC.Comment);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iWash_ProductDC.CommitStatus);
            //上线人ID
            db.AddInParameter(cmd, "@OnOperatorID", DbType.Int32, iWash_ProductDC.OnOperatorID);
            //上线时间
            db.AddInParameter(cmd, "@OnDate", DbType.DateTime, iWash_ProductDC.OnDate);
            //下线人ID
            db.AddInParameter(cmd, "@OffOperatorID", DbType.Int32, iWash_ProductDC.OffOperatorID);
            //下线时间
            db.AddInParameter(cmd, "@OffDate", DbType.DateTime, iWash_ProductDC.OffDate);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_ProductDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            //if (iWash_ProductDC.AttributeList != null)
            //{
            //    foreach (var attribute in iWash_ProductDC.AttributeList)
            //    {
            //        attribute.ProductID = id;
            //        Wash_ProductAttribute_ADD(attribute);
            //    }
            //}

            //if (iWash_ProductDC.PictureList != null)
            //{
            //    foreach (var picture in iWash_ProductDC.PictureList)
            //    {
            //        picture.ProductID = id;
            //        Wash_ProductPicture_ADD(picture);
            //    }
            //}

            return id;
        }

        /// <summary>
        /// 新增产品价目属性关联
        /// </summary>
        /// <param name="iWash_ProductAttributeDC"></param>
        /// <returns></returns>
        public bool Wash_ProductAttribute_ADD(Wash_ProductAttributeDC iWash_ProductAttributeDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_ProductAttribute(ProductID, AttributeID, Name, Content, Price, Selected, [Default], ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@ProductID, @AttributeID, @Name, @Content, @Price, @Selected, @Default, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //产品运价ID
            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iWash_ProductAttributeDC.ProductID);
            //属性ID
            db.AddInParameter(cmd, "@AttributeID", DbType.Int32, iWash_ProductAttributeDC.AttributeID);
            //产品运价名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_ProductAttributeDC.Name);
            //描述
            db.AddInParameter(cmd, "@Content", DbType.String, iWash_ProductAttributeDC.Content);
            //加价金额
            db.AddInParameter(cmd, "@Price", DbType.Decimal, iWash_ProductAttributeDC.Price);
            //强制选中
            db.AddInParameter(cmd, "@Selected", DbType.Int32, iWash_ProductAttributeDC.Selected);
            //默认选中
            db.AddInParameter(cmd, "@Default", DbType.Int32, iWash_ProductAttributeDC.Default);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_ProductAttributeDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 产品价目属性查询
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public IList<Wash_ProductAttributeDC> Wash_ProductAttribute_SELECT(int iProductID)
        {
            List<Wash_ProductAttributeDC> list = new List<Wash_ProductAttributeDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Wash_ProductAttribute.*,Wash_Attribute.Name AS AttributeName,
                Wash_Attribute.ParentID AS ParentAttributeID
                FROM Wash_ProductAttribute,Wash_Attribute
                WHERE Wash_ProductAttribute.AttributeID = Wash_Attribute.ID
                AND Wash_ProductAttribute.ProductID = @ProductID 
                AND Wash_ProductAttribute.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iProductID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Wash_ProductAttributeDC.GetEntity(reader));
                }
            }
            return list;
        }

        //        /// <summary>
        //        /// 新增产品价目图片关联
        //        /// </summary>
        //        /// <param name="iWash_ProductPictureDC"></param>
        //        /// <returns></returns>
        //        public bool Wash_ProductPicture_ADD(Wash_ProductPictureDC iWash_ProductPictureDC)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //            sql.Append("INSERT INTO Wash_ProductPicture(ProductID, Type, PictureL, PictureM, PictureS, Content, ");
        //            //字段拼接
        //            AddCommonInsertSql(sql);
        //            sql.Append(") VALUES (@ProductID, @Type, @PictureL, @PictureM, @PictureS, @Content, ");
        //            //值拼接
        //            AddCommonInsertValues(sql);
        //            sql.Append(");");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //产品运价ID
        //            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iWash_ProductPictureDC.ProductID);
        //            //类别
        //            db.AddInParameter(cmd, "@Type", DbType.Int32, iWash_ProductPictureDC.Type);
        //            //大图
        //            db.AddInParameter(cmd, "@PictureL", DbType.String, iWash_ProductPictureDC.PictureL);
        //            //中图
        //            db.AddInParameter(cmd, "@PictureM", DbType.String, iWash_ProductPictureDC.PictureM);
        //            //小图
        //            db.AddInParameter(cmd, "@PictureS", DbType.String, iWash_ProductPictureDC.PictureS);
        //            //描述
        //            db.AddInParameter(cmd, "@Content", DbType.String, iWash_ProductPictureDC.Content);
        //            //添加共通参数
        //            AddCommonInsertParameter(db, cmd, iWash_ProductPictureDC);
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        //        /// <summary>
        //        /// 产品价目图片查询
        //        /// </summary>
        //        /// <param name="iProductID"></param>
        //        /// <returns></returns>
        //        public IList<Wash_ProductPictureDC> Wash_ProductPicture_SELECT(int iProductID)
        //        {
        //            List<Wash_ProductPictureDC> list = new List<Wash_ProductPictureDC>();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT Wash_ProductPicture.* 
        //                FROM Wash_ProductPicture
        //                WHERE Wash_ProductPicture.ProductID = @ProductID 
        //                AND Wash_ProductPicture.Obj_Status = 1");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

        //            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iProductID);

        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                while (reader.Read())
        //                {
        //                    list.Add(Wash_ProductPictureDC.GetEntity(reader));
        //                }
        //            }
        //            return list;
        //        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        public bool Wash_Product_UPDATE(Wash_ProductDC iWash_ProductDC)
        {
            //清除排序
            Wash_Product_UPDATE_RecSort(iWash_ProductDC.RecSort);

            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Wash_Product 
                SET Name = @Name, WebName = @WebName, Code = @Code, CategoryID = @CategoryID,
                Keyword = @Keyword, Content = @Content, 
                MarketPrice = @MarketPrice, SalesPrice = @SalesPrice, 
                Type = @Type, Site = @Site, Sort = @Sort, RecSort = @RecSort, [Group] = @Group,
                VerifyStatus = @VerifyStatus, SubmitOperatorID = @SubmitOperatorID, 
                SubmitDate = @SubmitDate, AduitOperatorID = @AduitOperatorID, 
                AduitDate = @AduitDate, Comment = @Comment, CommitStatus = @CommitStatus, 
                OnOperatorID = @OnOperatorID, OnDate = @OnDate, OffOperatorID = @OffOperatorID, 
                OffDate = @OffDate, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iWash_ProductDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_ProductDC.Name);
            //网站名称
            db.AddInParameter(cmd, "@WebName", DbType.String, iWash_ProductDC.WebName);
            //代码
            db.AddInParameter(cmd, "@Code", DbType.String, iWash_ProductDC.Code);
            //产品ID
            db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iWash_ProductDC.CategoryID);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iWash_ProductDC.Keyword);
            //描述
            db.AddInParameter(cmd, "@Content", DbType.String, iWash_ProductDC.Content);
            //市场价
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, iWash_ProductDC.MarketPrice);
            //销售价
            db.AddInParameter(cmd, "@SalesPrice", DbType.Decimal, iWash_ProductDC.SalesPrice);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iWash_ProductDC.Type);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iWash_ProductDC.Site);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_ProductDC.Sort);
            //推荐排序
            db.AddInParameter(cmd, "@RecSort", DbType.Int32, iWash_ProductDC.RecSort);
            //分组
            db.AddInParameter(cmd, "@Group", DbType.Int32, iWash_ProductDC.Group);
            //审核状态
            db.AddInParameter(cmd, "@VerifyStatus", DbType.Int32, iWash_ProductDC.VerifyStatus);
            //审核提交人ID
            db.AddInParameter(cmd, "@SubmitOperatorID", DbType.Int32, iWash_ProductDC.SubmitOperatorID);
            //提交审核时间
            db.AddInParameter(cmd, "@SubmitDate", DbType.DateTime, iWash_ProductDC.SubmitDate);
            //审核人ID
            db.AddInParameter(cmd, "@AduitOperatorID", DbType.Int32, iWash_ProductDC.AduitOperatorID);
            //审核时间
            db.AddInParameter(cmd, "@AduitDate", DbType.DateTime, iWash_ProductDC.AduitDate);
            //审核人意见
            db.AddInParameter(cmd, "@Comment", DbType.String, iWash_ProductDC.Comment);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iWash_ProductDC.CommitStatus);
            //上线人ID
            db.AddInParameter(cmd, "@OnOperatorID", DbType.Int32, iWash_ProductDC.OnOperatorID);
            //上线时间
            db.AddInParameter(cmd, "@OnDate", DbType.DateTime, iWash_ProductDC.OnDate);
            //下线人ID
            db.AddInParameter(cmd, "@OffOperatorID", DbType.Int32, iWash_ProductDC.OffOperatorID);
            //下线时间
            db.AddInParameter(cmd, "@OffDate", DbType.DateTime, iWash_ProductDC.OffDate);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iWash_ProductDC);
            var rtn = db.ExecuteNonQuery(cmd) > 0 ? true : false;

            //Wash_ProductAttribute_DELETE(iWash_ProductDC.ID);

            //if (iWash_ProductDC.AttributeList != null)
            //{
            //    foreach (var attribute in iWash_ProductDC.AttributeList)
            //    {
            //        attribute.ProductID = iWash_ProductDC.ID;
            //        Wash_ProductAttribute_ADD(attribute);
            //    }
            //}

            //Wash_ProductPicture_DELETE(iWash_ProductDC.ID);

            //if (iWash_ProductDC.PictureList != null)
            //{
            //    foreach (var picture in iWash_ProductDC.PictureList)
            //    {
            //        picture.ProductID = iWash_ProductDC.ID;
            //        Wash_ProductPicture_ADD(picture);
            //    }
            //}

            return rtn;
        }

        /// <summary>
        /// 更新推荐排序
        /// </summary>
        /// <param name="iRecSort"></param>
        private void Wash_Product_UPDATE_RecSort(int iRecSort)
        {
            if (iRecSort <= 0)
                return;

            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Wash_Product SET RecSort = 0 WHERE RecSort = @RecSort AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@RecSort", DbType.Int32, iRecSort);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除产品价目属性关联
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public bool Wash_ProductAttribute_DELETE(int iProductID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Wash_ProductAttribute SET Obj_Status = 6, Obj_Mdate = getdate() WHERE ProductID = @ProductID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iProductID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        ///// <summary>
        ///// 删除图片关联
        ///// </summary>
        ///// <param name="iProductID"></param>
        ///// <returns></returns>
        //public bool Wash_ProductPicture_DELETE(int iProductID)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Wash_ProductPicture SET Obj_Status = 6, Obj_Mdate = getdate() WHERE ProductID = @ProductID AND Obj_Status = 1");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //
        //    db.AddInParameter(cmd, "@ProductID", DbType.Int32, iProductID);

        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        /// <summary>
        /// 价目单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Wash_ProductDC Wash_Product_SELECT_Entity(int iID)
        {
            Wash_ProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Wash_Product.*,Wash_Category.ClassID,Wash_Category.Name AS CategoryName
                            FROM Wash_Product,Wash_Category
                            WHERE Wash_Product.ID = @ID
                            AND Wash_Product.CategoryID = Wash_Category.ID
                            AND Wash_Product.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_ProductDC.GetEntity(reader);
                }
            }

            if (entity != null)
            {
                entity.AttributeList = Wash_ProductAttribute_SELECT(entity.ID);
                //entity.PictureList = Wash_ProductPicture_SELECT(entity.ID);
            }

            return entity;
        }

        /// <summary>
        /// 产品价目列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iType"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Wash_ProductDC> Wash_Product_SELECT_List(string iName, string iCode, int? iType,
            int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<Wash_ProductDC> list = new List<Wash_ProductDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@"Wash_Product.Obj_Status = 1 ");
            sqlwhere.Append(@"AND Wash_Product.CategoryID = Wash_Category.ID ");
            sqlorder.Append(@" Wash_Product.Sort,Wash_Product.ID ");

            sqlfields.Append(" Wash_Product.*,Wash_Category.ClassID,Wash_Category.Name AS CategoryName");

            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append("AND Wash_Product.Name LIKE '%" + iName + "%' ");
            //编号
            if (!string.IsNullOrEmpty(iCode))
                sqlwhere.Append("AND Wash_Product.Code LIKE '%" + iCode + "%' ");
            //类型,普通,活动
            if (iType.HasValue)
                sqlwhere.Append("AND Wash_Product.Type = '" + iType + "' ");
            //状态
            if (iCommitStatus.HasValue)
                sqlwhere.Append("AND Wash_Product.CommitStatus = '" + iCommitStatus + "' ");
            //注册时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND Wash_Product.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //注册时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND Wash_Product.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Wash_ProductDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Wash_Product,Wash_Category", "Wash_Product.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Wash_Product,Wash_Category", "Wash_Product.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Wash_ProductDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public IList<Wash_ProductDC> Wash_Product_SELECT_List_CategoryID(int iCategoryID)
        {
            List<Wash_ProductDC> list = new List<Wash_ProductDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Wash_Product.*,Wash_Category.Name AS CategoryName,
                    CA.Name AS ClassName,CB.Name AS ParentClassName
                FROM Wash_Product,Wash_Category,Wash_Class CA,Wash_Class CB
                WHERE Wash_Product.CategoryID = @CategoryID
                AND Wash_Product.CategoryID = Wash_Category.ID
                AND Wash_Category.ClassID = CA.ID
                AND CA.ParentID = CB.ID
                AND Wash_Product.VerifyStatus = 2 AND Wash_Product.CommitStatus = 1
                AND Wash_Product.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iCategoryID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Wash_ProductDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 新增活动
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        public int Wash_Activity_ADD(Wash_ActivityDC iWash_ActivityDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Activity(Title, Link, BeginDate, EndDate, Sort, Site, Channel, PictureL, PictureM, PictureS, PictureAlt, CommitStatus, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Title, @Link, @BeginDate, @EndDate, @Sort, @Site, @Channel, @PictureL, @PictureM, @PictureS, @PictureAlt, @CommitStatus, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iWash_ActivityDC.Title);
            //链接
            db.AddInParameter(cmd, "@Link", DbType.String, iWash_ActivityDC.Link);
            //开始时间
            db.AddInParameter(cmd, "@BeginDate", DbType.DateTime, iWash_ActivityDC.BeginDate);
            //结束时间
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, iWash_ActivityDC.EndDate);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_ActivityDC.Sort);
            //
            db.AddInParameter(cmd, "@Site", DbType.Int32, iWash_ActivityDC.Site);
            //
            db.AddInParameter(cmd, "@Channel", DbType.Int32, iWash_ActivityDC.Channel);
            //大图
            db.AddInParameter(cmd, "@PictureL", DbType.String, iWash_ActivityDC.PictureL);
            //中图
            db.AddInParameter(cmd, "@PictureM", DbType.String, iWash_ActivityDC.PictureM);
            //小图
            db.AddInParameter(cmd, "@PictureS", DbType.String, iWash_ActivityDC.PictureS);
            //图片描述
            db.AddInParameter(cmd, "@PictureAlt", DbType.String, iWash_ActivityDC.PictureAlt);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iWash_ActivityDC.CommitStatus);

            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_ActivityDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 活动更新
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        public bool Wash_Activity_UPDATE(Wash_ActivityDC iWash_ActivityDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Wash_Activity SET Title = @Title, Link = @Link, 
                BeginDate = @BeginDate, EndDate = @EndDate, Sort = @Sort, Site = @Site, Channel = @Channel,
                PictureL = @PictureL, PictureM = @PictureM, PictureS = @PictureS, PictureAlt = @PictureAlt, 
                CommitStatus = @CommitStatus, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.String, iWash_ActivityDC.ID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iWash_ActivityDC.Title);
            //链接
            db.AddInParameter(cmd, "@Link", DbType.String, iWash_ActivityDC.Link);
            //开始时间
            db.AddInParameter(cmd, "@BeginDate", DbType.DateTime, iWash_ActivityDC.BeginDate);
            //结束时间
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, iWash_ActivityDC.EndDate);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iWash_ActivityDC.Sort);
            //
            db.AddInParameter(cmd, "@Site", DbType.Int32, iWash_ActivityDC.Site);
            //
            db.AddInParameter(cmd, "@Channel", DbType.Int32, iWash_ActivityDC.Channel);
            //大图
            db.AddInParameter(cmd, "@PictureL", DbType.String, iWash_ActivityDC.PictureL);
            //中图
            db.AddInParameter(cmd, "@PictureM", DbType.String, iWash_ActivityDC.PictureM);
            //小图
            db.AddInParameter(cmd, "@PictureS", DbType.String, iWash_ActivityDC.PictureS);
            //图片描述
            db.AddInParameter(cmd, "@PictureAlt", DbType.String, iWash_ActivityDC.PictureAlt);
            //确认状态
            db.AddInParameter(cmd, "@CommitStatus", DbType.Int32, iWash_ActivityDC.CommitStatus);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iWash_ActivityDC);
            var rtn = db.ExecuteNonQuery(cmd) > 0 ? true : false;

            return rtn;
        }

        /// <summary>
        /// 新增活动图片
        /// </summary>
        /// <param name="iWash_ActivityPictureDC"></param>
        /// <returns></returns>
        public bool Wash_ActivityPicture_ADD(Wash_ActivityPictureDC iWash_ActivityPictureDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_ActivityPicture(ActivityID, Type, Picture, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@ActivityID, @Type, @Picture, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //活动ID
            db.AddInParameter(cmd, "@ActivityID", DbType.Int32, iWash_ActivityPictureDC.ActivityID);
            //
            db.AddInParameter(cmd, "@Type", DbType.Int32, iWash_ActivityPictureDC.Type);
            //图片路径
            db.AddInParameter(cmd, "@Picture", DbType.String, iWash_ActivityPictureDC.Picture);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_ActivityPictureDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iWash_ActivityProductDC"></param>
        /// <returns></returns>
        public bool Wash_ActivityProduct_ADD(int iActivityID, int iProductID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_ActivityProduct(ActivityID, ProductID, Obj_Cuser, Obj_Muser");
            sql.Append(") VALUES (@ActivityID, @ProductID, -1, -1);");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //活动ID
            db.AddInParameter(cmd, "@ActivityID", DbType.Int32, iActivityID);
            //产品ID
            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iProductID);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 活动单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Wash_ActivityDC Wash_Activity_SELECT_Entity(int iID)
        {
            Wash_ActivityDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM Wash_Activity
                            WHERE ID = @ID
                            AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_ActivityDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Wash_ActivityDC> Wash_Activity_SELECT_List(string iName, int? iCommitStatus,
            int? iSite, int? iChannel,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<Wash_ActivityDC> list = new List<Wash_ActivityDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");
            sqlorder.Append(@" Sort DESC");

            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append(" AND Name LIKE '%" + iName + "%' ");
            //站点
            if (iSite.HasValue)
                sqlwhere.Append(" AND Site = '" + iSite + "' ");
            //渠道
            if (iChannel.HasValue)
                sqlwhere.Append(" AND Channel = '" + iChannel + "' ");
            //状态
            if (iCommitStatus.HasValue)
                sqlwhere.Append(" AND CommitStatus = '" + iCommitStatus + "' ");
            //时间开始
            if (iStartDate.HasValue)
            {
                sqlwhere.Append(" AND EndDate >= '" + iStartDate.Value.ToString("yyyy-MM-dd") + "' ");
            }
            if (iEndDate.HasValue)
            {
                sqlwhere.Append(" AND BeginDate < '" + iEndDate.Value.ToString("yyyy-MM-dd") + "' ");
            }
            if (iStartDate.HasValue && iEndDate.HasValue)
            {
                sqlwhere.Append(" AND '" + iStartDate.Value.ToString("yyyy-MM-dd") + "' <= '" + iEndDate.Value.ToString("yyyy-MM-dd") + "'");
            }

            var rtn = new RecordCountEntity<Wash_ActivityDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Wash_Activity", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Wash_Activity", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Wash_ActivityDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        #region 合作门店

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        public int Wash_Store_ADD(Wash_StoreDC iWash_StoreDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Store(Code, Name, DistrictID, Site, LinkMan, Address, MPNo, Phone, ZipCode, Email, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Code, @Name, @DistrictID, @Site, @LinkMan, @Address, @MPNo, @Phone, @ZipCode, @Email, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //编号
            db.AddInParameter(cmd, "@Code", DbType.String, iWash_StoreDC.Code);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_StoreDC.Name);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iWash_StoreDC.DistrictID);
            //
            db.AddInParameter(cmd, "@Site", DbType.Int32, iWash_StoreDC.Site);
            //
            db.AddInParameter(cmd, "@LinkMan", DbType.String, iWash_StoreDC.LinkMan);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iWash_StoreDC.Address);
            //手机号
            db.AddInParameter(cmd, "@MPNo", DbType.String, iWash_StoreDC.MPNo);
            //电话号码
            db.AddInParameter(cmd, "@Phone", DbType.String, iWash_StoreDC.Phone);
            //邮编
            db.AddInParameter(cmd, "@ZipCode", DbType.String, iWash_StoreDC.ZipCode);
            //邮箱
            db.AddInParameter(cmd, "@Email", DbType.String, iWash_StoreDC.Email);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_StoreDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns></returns>
        public bool Wash_Store_CodeExists(string iCode)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(0) FROM Wash_Store WHERE Code = @Code");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
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
        /// 更新
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        public bool Wash_Store_UPDATE(Wash_StoreDC iWash_StoreDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Wash_Store SET Name = @Name, DistrictID = @DistrictID, Site = @Site, LinkMan = @LinkMan, Address = @Address, MPNo = @MPNo, Phone = @Phone, ZipCode = @ZipCode, Email = @Email, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iWash_StoreDC.ID);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_StoreDC.Name);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iWash_StoreDC.DistrictID);

            db.AddInParameter(cmd, "@Site", DbType.Int32, iWash_StoreDC.Site);
            //
            db.AddInParameter(cmd, "@LinkMan", DbType.String, iWash_StoreDC.LinkMan);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iWash_StoreDC.Address);
            //手机号
            db.AddInParameter(cmd, "@MPNo", DbType.String, iWash_StoreDC.MPNo);
            //电话号码
            db.AddInParameter(cmd, "@Phone", DbType.String, iWash_StoreDC.Phone);
            //邮编
            db.AddInParameter(cmd, "@ZipCode", DbType.String, iWash_StoreDC.ZipCode);
            //邮箱
            db.AddInParameter(cmd, "@Email", DbType.String, iWash_StoreDC.Email);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iWash_StoreDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="iID"></param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public bool Wash_Store_DELETE(int iID, int iMuser)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    Database db = DBHelper.CreateDataBase(APPModule, VEBS.Core.Enumerate.DataAccessPatterns.Write);
        //    sql.Append("UPDATE Wash_Store SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
        //    DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //    //
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
        //    //操作人
        //    db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
        //    return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //}

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Wash_StoreDC> Wash_Store_SELECT_List(string iName, string iCode, int? iSite,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<Wash_StoreDC> list = new List<Wash_StoreDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@"Obj_Status = 1 ");
            sqlorder.Append(@" ID DESC");

            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append("AND Name LIKE '%" + iName + "%' ");
            //编号
            if (!string.IsNullOrEmpty(iCode))
                sqlwhere.Append("AND Code ='" + iCode + "' ");

            if (iSite.HasValue)
                sqlwhere.Append("AND Site =" + iSite + " ");

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Wash_StoreDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Wash_Store", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Wash_Store", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Wash_StoreDC.GetEntity(reader));
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
        public Wash_StoreDC Wash_Store_SELECT_Entity(int iID)
        {
            Wash_StoreDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Store WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_StoreDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        public Wash_StoreDC Wash_Store_SELECT_Entity(Guid iStoreID)
        {
            Wash_StoreDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Store WHERE StoreID = @StoreID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@StoreID", DbType.Guid, iStoreID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_StoreDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        public int Wash_Store_Operator_ADD(Wash_Store_OperatorDC iWash_Store_OperatorDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Store_Operator(StoreID, StoreCode, LoginName, LoginPwd, Name, MPNo, Enable, IsAdmin, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@StoreID, @StoreCode, @LoginName, @LoginPwd, @Name, @MPNo, @Enable, @IsAdmin, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //门店ID
            db.AddInParameter(cmd, "@StoreID", DbType.Int32, iWash_Store_OperatorDC.StoreID);
            //编号
            db.AddInParameter(cmd, "@StoreCode", DbType.String, iWash_Store_OperatorDC.StoreCode);
            //登录名
            db.AddInParameter(cmd, "@LoginName", DbType.String, iWash_Store_OperatorDC.LoginName);
            //登录密码
            db.AddInParameter(cmd, "@LoginPwd", DbType.String, iWash_Store_OperatorDC.LoginPwd);
            //姓名
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_Store_OperatorDC.Name);
            //联系手机
            db.AddInParameter(cmd, "@MPNo", DbType.String, iWash_Store_OperatorDC.MPNo);
            //是否启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_Store_OperatorDC.Enable);
            //是否最高权限
            db.AddInParameter(cmd, "@IsAdmin", DbType.Int32, iWash_Store_OperatorDC.IsAdmin);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_Store_OperatorDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        public bool Wash_Store_Operator_UPDATE(Wash_Store_OperatorDC iWash_Store_OperatorDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Wash_Store_Operator SET StoreID = @StoreID, StoreCode = @StoreCode, 
                LoginName = @LoginName, LoginPwd = @LoginPwd, Name = @Name, MPNo = @MPNo, Enable = @Enable, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iWash_Store_OperatorDC.ID);
            //登录名
            db.AddInParameter(cmd, "@LoginName", DbType.String, iWash_Store_OperatorDC.LoginName);
            //姓名
            db.AddInParameter(cmd, "@Name", DbType.String, iWash_Store_OperatorDC.Name);
            //联系手机
            db.AddInParameter(cmd, "@MPNo", DbType.String, iWash_Store_OperatorDC.MPNo);
            //是否启用
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iWash_Store_OperatorDC.Enable);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iWash_Store_OperatorDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Wash_Store_Operator_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Wash_Store_Operator SET Obj_Status = 6, Obj_Muser = @Muser, 
                Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IList<Wash_Store_OperatorDC> Wash_Store_Operator_SELECT_ALL(int iStoreID)
        {
            List<Wash_Store_OperatorDC> list = new List<Wash_Store_OperatorDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Store_Operator WHERE StoreID = @StoreID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //有效数据
            db.AddInParameter(cmd, "@StoreID", DbType.Int32, iStoreID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Wash_Store_OperatorDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Wash_Store_OperatorDC Wash_Store_Operator_SELECT_Entity(int iID)
        {
            Wash_Store_OperatorDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Wash_Store_Operator WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_Store_OperatorDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStoreCode"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <returns></returns>
        public Wash_StoreLoginInfoDC Wash_Store_Operator_SELECT_Entity(string iStoreCode, string iLoginName, string iPassword)
        {
            Wash_StoreLoginInfoDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                    Wash_Store.StoreID
                    ,Wash_Store.Code StoreCode
                    ,Wash_Store.Name StoreName
                    ,Wash_Store.Address
                    ,Wash_Store.LinkMan
                    ,Wash_Store.MPNo
                    ,Wash_Store.Phone
                    ,Wash_Store.ZipCode
                    ,Wash_Store.Email
                    ,Wash_Store_Operator.ID OperatorID
                    ,Wash_Store_Operator.LoginName 
                    ,Wash_Store_Operator.Name OperatorName
                    ,Wash_Store_Operator.IsAdmin
                    ,C.FullName AS DistrictName
                    ,B.Name AS CityName
                    ,A.Name AS ProvinceName
                FROM Wash_Store_Operator,Wash_Store,Base_District C,Base_District B,Base_District A
                WHERE Wash_Store_Operator.StoreID = Wash_Store.ID
                AND Wash_Store.DistrictID = C.ID
                AND C.Code1 = B.Code1 AND C.Code2 = B.Code2 AND B.Code3 = '00' AND B.Obj_Status = 1 AND B.Level = 2
                AND B.Code1 = A.Code1 AND A.Code2 = '00' AND A.Code3 = '00' AND A.Obj_Status = 1 AND A.Level = 1
                AND Wash_Store_Operator.StoreCode = @StoreCode
                AND Wash_Store_Operator.LoginName = @LoginName
                AND Wash_Store_Operator.LoginPwd = @LoginPwd
                AND Wash_Store_Operator.Enable = 1
                AND Wash_Store_Operator.Obj_Status = 1
                AND Wash_Store.Obj_Status = 1

                ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //
            db.AddInParameter(cmd, "@StoreCode", DbType.String, iStoreCode);
            db.AddInParameter(cmd, "@LoginName", DbType.String, iLoginName);
            db.AddInParameter(cmd, "@LoginPwd", DbType.String, iPassword);



            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_StoreLoginInfoDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Wash_Store_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Wash_Store_Operator SET LoginPwd = @NewPassword, Obj_Muser = @Muser, 
                Obj_Mdate = getdate() WHERE ID = @ID AND LoginPwd = @OldPassword");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOperatorID);

            db.AddInParameter(cmd, "@OldPassword", DbType.String, iOldPassword);

            db.AddInParameter(cmd, "@NewPassword", DbType.String, iNewPassword);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iWash_Store_OperatorLogDC"></param>
        /// <returns></returns>
        public int Wash_Store_OperatorLog_ADD(Wash_Store_OperatorLogDC iWash_Store_OperatorLogDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Wash_Store_OperatorLog(OperatorID, OperatorName, Type, LogContent, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OperatorID, @OperatorName, @Type, @LogContent, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //操作人
            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iWash_Store_OperatorLogDC.OperatorID);
            //操作人名字
            db.AddInParameter(cmd, "@OperatorName", DbType.String, iWash_Store_OperatorLogDC.OperatorName);
            //日志类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iWash_Store_OperatorLogDC.Type);
            //日志内容
            db.AddInParameter(cmd, "@LogContent", DbType.String, iWash_Store_OperatorLogDC.LogContent);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iWash_Store_OperatorLogDC);
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
        public RecordCountEntity<Wash_Store_OperatorLogDC> Wash_Store_OperatorLog_SELECT_List(int? iType,
            string iOperatorName, string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            List<Wash_Store_OperatorLogDC> list = new List<Wash_Store_OperatorLogDC>();
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

            var rtn = new RecordCountEntity<Wash_Store_OperatorLogDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Wash_Store_OperatorLog", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Wash_Store_OperatorLog", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Wash_Store_OperatorLogDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        #endregion

        #region 商场产品

        /// <summary>
        /// 新增商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        public int Mall_Product_ADD(Mall_ProductDC iMall_ProductDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Mall_Product(Type, Class, Name, Content, Keyword, TypeValue, TotalCount, LeftCount, SaleCount, Site, Sort, MarketPrice, SalesPrice, Enable, PictureL, PictureM, PictureS, SellBeginDate, SellEndDate, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@Type, @Class, @Name, @Content, @Keyword, @TypeValue, @TotalCount, @LeftCount, @SaleCount, @Site, @Sort, @MarketPrice, @SalesPrice, @Enable, @PictureL, @PictureM, @PictureS, @SellBeginDate, @SellEndDate, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iMall_ProductDC.Type);
            //类别
            db.AddInParameter(cmd, "@Class", DbType.Int32, iMall_ProductDC.Class);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iMall_ProductDC.Name);
            //描述
            db.AddInParameter(cmd, "@Content", DbType.String, iMall_ProductDC.Content);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iMall_ProductDC.Keyword);
            //类型用数值
            db.AddInParameter(cmd, "@TypeValue", DbType.String, iMall_ProductDC.TypeValue);
            //总数量
            db.AddInParameter(cmd, "@TotalCount", DbType.Int32, iMall_ProductDC.TotalCount);
            //剩余数量
            db.AddInParameter(cmd, "@LeftCount", DbType.Int32, iMall_ProductDC.LeftCount);
            //已销售数量
            db.AddInParameter(cmd, "@SaleCount", DbType.Int32, iMall_ProductDC.SaleCount);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iMall_ProductDC.Site);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iMall_ProductDC.Sort);
            //市场价
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, iMall_ProductDC.MarketPrice);
            //销售价
            db.AddInParameter(cmd, "@SalesPrice", DbType.Decimal, iMall_ProductDC.SalesPrice);
            //启用状态
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iMall_ProductDC.Enable);
            //大图
            db.AddInParameter(cmd, "@PictureL", DbType.String, iMall_ProductDC.PictureL);
            //中图
            db.AddInParameter(cmd, "@PictureM", DbType.String, iMall_ProductDC.PictureM);
            //小图
            db.AddInParameter(cmd, "@PictureS", DbType.String, iMall_ProductDC.PictureS);
            //销售开始时间
            db.AddInParameter(cmd, "@SellBeginDate", DbType.DateTime, iMall_ProductDC.SellBeginDate);
            //销售结束时间
            db.AddInParameter(cmd, "@SellEndDate", DbType.DateTime, iMall_ProductDC.SellEndDate);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iMall_ProductDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 更新商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        public bool Mall_Product_UPDATE(Mall_ProductDC iMall_ProductDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Mall_Product SET Name = @Name, Content = @Content, Keyword = @Keyword, TotalCount = @TotalCount, LeftCount = @LeftCount, SaleCount = @SaleCount, Site = @Site, Sort = @Sort, MarketPrice = @MarketPrice, SalesPrice = @SalesPrice, Enable = @Enable, PictureL = @PictureL, PictureM = @PictureM, PictureS = @PictureS, SellBeginDate = @SellBeginDate, SellEndDate = @SellEndDate, ");
            //字段拼接
            AddCommonUpdateSql(sql);
            sql.Append(" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iMall_ProductDC.ID);
            //类型
            //db.AddInParameter(cmd, "@Type", DbType.Int32, iMall_ProductDC.Type);
            //类别
            //db.AddInParameter(cmd, "@Class", DbType.Int32, iMall_ProductDC.Class);
            //名称
            db.AddInParameter(cmd, "@Name", DbType.String, iMall_ProductDC.Name);
            //描述
            db.AddInParameter(cmd, "@Content", DbType.String, iMall_ProductDC.Content);
            //搜索关键字
            db.AddInParameter(cmd, "@Keyword", DbType.String, iMall_ProductDC.Keyword);
            //类型用数值
            //db.AddInParameter(cmd, "@TypeValue", DbType.String, iMall_ProductDC.TypeValue);
            //总数量
            db.AddInParameter(cmd, "@TotalCount", DbType.Int32, iMall_ProductDC.TotalCount);
            //剩余数量
            db.AddInParameter(cmd, "@LeftCount", DbType.Int32, iMall_ProductDC.LeftCount);
            //已销售数量
            db.AddInParameter(cmd, "@SaleCount", DbType.Int32, iMall_ProductDC.SaleCount);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iMall_ProductDC.Site);
            //排序
            db.AddInParameter(cmd, "@Sort", DbType.Int32, iMall_ProductDC.Sort);
            //市场价
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, iMall_ProductDC.MarketPrice);
            //销售价
            db.AddInParameter(cmd, "@SalesPrice", DbType.Decimal, iMall_ProductDC.SalesPrice);
            //启用状态
            db.AddInParameter(cmd, "@Enable", DbType.Int32, iMall_ProductDC.Enable);
            //大图
            db.AddInParameter(cmd, "@PictureL", DbType.String, iMall_ProductDC.PictureL);
            //中图
            db.AddInParameter(cmd, "@PictureM", DbType.String, iMall_ProductDC.PictureM);
            //小图
            db.AddInParameter(cmd, "@PictureS", DbType.String, iMall_ProductDC.PictureS);
            //销售开始时间
            db.AddInParameter(cmd, "@SellBeginDate", DbType.DateTime, iMall_ProductDC.SellBeginDate);
            //销售结束时间
            db.AddInParameter(cmd, "@SellEndDate", DbType.DateTime, iMall_ProductDC.SellEndDate);
            //添加共通参数
            AddCommonUpdateParameter(db, cmd, iMall_ProductDC);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public bool Mall_Product_DELETE(int iID, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Mall_Product SET Obj_Status = '6', Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询商场产品
        /// </summary>
        /// <returns></returns>
        public RecordCountEntity<Mall_ProductDC> Mall_Product_SELECT_List(int? iType, int? iClass,
            string iName, int? iSite, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            List<Mall_ProductDC> list = new List<Mall_ProductDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 ");
            sqlorder.Append(@" ID DESC ");

            //类型
            if (iType.HasValue)
                sqlwhere.Append("AND Type = '" + iType + "' ");
            //类别
            if (iClass.HasValue)
                sqlwhere.Append("AND Class = '" + iClass + "' ");
            //站点
            if (iSite.HasValue)
                sqlwhere.Append("AND Site = '" + iSite + "' ");
            //名称
            if (!string.IsNullOrEmpty(iName))
                sqlwhere.Append("AND Name LIKE '%" + iName + "%' ");

            //时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append("AND Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Mall_ProductDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Mall_Product", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Mall_Product", "ID", null, sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(Mall_ProductDC.GetEntity(reader));
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Mall_ProductDC Mall_Product_SELECT_Entity(int iID)
        {
            Mall_ProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Mall_Product WHERE ID = @ID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Mall_ProductDC.GetEntity(reader);
                }
            }
            return entity;
        }


        #endregion

    }
}
