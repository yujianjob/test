using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;
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
    public class web_ProductDAL : DALBase, LazyAtHome.WCF.Wash.Interface.IDAL.Iweb_ProductDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public web_ProductDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        public IList<web_Wash_ClassDC> web_Wash_Class_SELECT_List()
        {
            List<web_Wash_ClassDC> list = new List<web_Wash_ClassDC>();
            web_Wash_ClassDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT ID, Name, ParentID,PictureS Picture FROM Wash_Class WHERE Obj_Status = 1 ORDER BY Sort,ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = web_Wash_ClassDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据小类ID获取运产品表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public IList<web_Wash_CategoryDC> web_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            List<web_Wash_CategoryDC> list = new List<web_Wash_CategoryDC>();
            web_Wash_CategoryDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Category.ID
                ,Wash_Category.Name
                ,Wash_Category.Content
                ,Wash_Category.PictureM
                ,Wash_Category.PictureAlt
            FROM Wash_Category
            WHERE Obj_Status = 1
                AND ClassID = @ClassID
                AND EXISTS(SELECT 1 FROM Wash_Product WHERE Wash_Product.CategoryID = Wash_Category.ID
                    AND Wash_Product.Site = @Site AND Wash_Product.Type = 1 AND Wash_Product.VerifyStatus = 2
                    AND Wash_Product.CommitStatus = 1 AND Wash_Product.Obj_Status = 1)
            ORDER BY Wash_Category.Sort,Wash_Category.ID
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ClassID", DbType.Int32, iClassID);

            db.AddInParameter(cmd, "@Site", DbType.Int32, iSite);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = web_Wash_CategoryDC.GetEntity(reader);
                    if (entity != null)
                    {
                        entity.ProductList = web_Wash_Product_SELECT_List(entity.ID);
                        list.Add(entity);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public web_Wash_CategoryDC web_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            web_Wash_CategoryDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Category.ID
                ,Wash_Category.Name
                ,Wash_Category.Content
                ,Wash_Category.PictureL 
                ,Wash_Category.PictureAlt
                ,A.ID ClassID
                ,A.Name ClassName
                ,B.ID ParentClassID
                ,B.Name ParentClassName
            FROM Wash_Category,Wash_Class A,Wash_Class B
            WHERE Wash_Category.ClassID = A.ID AND A.ParentID = B.ID
			AND Wash_Category.Obj_Status = 1 AND Wash_Category.ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ClassID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iCategoryID);


            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = web_Wash_CategoryDC.GetEntity(reader);
                    entity.ProductList = web_Wash_Product_SELECT_List(entity.ID);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据CategoryID获取运价
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public IList<web_Wash_ProductDC> web_Wash_Product_SELECT_List(int iCategoryID)
        {
            List<web_Wash_ProductDC> list = new List<web_Wash_ProductDC>();
            web_Wash_ProductDC entity = null;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Product.ID
                ,Wash_Product.WebName AS Name
                ,Wash_Product.Content
                ,Wash_Product.SalesPrice
                ,Wash_Product.MarketPrice
                ,Wash_Product.CategoryID
            FROM Wash_Product
            WHERE Wash_Product.CategoryID = @CategoryID
                AND Wash_Product.Obj_Status = 1
                AND Wash_Product.VerifyStatus = 2 AND Wash_Product.CommitStatus = 1
            ORDER BY Wash_Product.Sort,Wash_Product.ID
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ProductID
            db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iCategoryID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = web_Wash_ProductDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }
            return list;
        }


        /// <summary>
        /// 根据CategoryID获取运价
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public IList<web_Wash_ProductDC> web_Wash_Product_SELECT_List(int iCategoryID,string iKeyword)
        {
            List<web_Wash_ProductDC> list = new List<web_Wash_ProductDC>();
            web_Wash_ProductDC entity = null;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Product.ID
                ,Wash_Product.WebName AS Name
                ,Wash_Product.Content
                ,Wash_Product.SalesPrice
                ,Wash_Product.MarketPrice
                ,Wash_Product.CategoryID
            FROM Wash_Product
            WHERE Wash_Product.CategoryID = @CategoryID
                AND Wash_Product.Obj_Status = 1
                AND Wash_Product.VerifyStatus = 2 AND Wash_Product.CommitStatus = 1
            ");
            
            if (!string.IsNullOrWhiteSpace(iKeyword))
            {
                sql.Append(@" AND Wash_Product.Name LIKE '%" + iKeyword + "%' ");
            }

            sql.Append(@" ORDER BY Wash_Product.Sort,Wash_Product.ID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ProductID
            db.AddInParameter(cmd, "@CategoryID", DbType.Int32, iCategoryID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = web_Wash_ProductDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据ProductID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public web_Wash_ProductDC web_Wash_Product_SELECT_Entity(int iProductID)
        {
            web_Wash_ProductDC entity = null;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Product.ID
                ,Wash_Product.Name
                ,Wash_Product.WebName
                ,Wash_Product.Content
                ,Wash_Product.SalesPrice 
                ,Wash_Product.MarketPrice
                ,Wash_Product.[Group]
                ,Wash_Product.CategoryID
                ,Wash_Category.PictureM
            FROM Wash_Product,Wash_Category
            WHERE Wash_Product.ID = @ProductID
                AND Wash_Product.CategoryID = Wash_Category.ID
                AND Wash_Product.Obj_Status = 1
                AND Wash_Product.VerifyStatus = 2 AND Wash_Product.CommitStatus = 1
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ProductID
            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iProductID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = web_Wash_ProductDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据ProductIDList获取运价
        /// </summary>
        /// <param name="iProductIDList"></param>
        /// <returns></returns>
        public IDictionary<int, web_Wash_ProductDC> web_Wash_Product_SELECT_Entity(IList<int> iProductIDList)
        {
            IDictionary<int, web_Wash_ProductDC> list = new Dictionary<int, web_Wash_ProductDC>();

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Product.ID
                ,Wash_Product.Name
                ,Wash_Product.WebName
                ,Wash_Product.Content
                ,Wash_Product.SalesPrice 
                ,Wash_Product.MarketPrice
                ,Wash_Product.[Group]
                ,Wash_Product.CategoryID
                ,Wash_Category.PictureM
            FROM Wash_Product,Wash_Category
            WHERE Wash_Product.CategoryID = Wash_Category.ID
                AND Wash_Product.Obj_Status = 1
                AND Wash_Product.VerifyStatus = 2 AND Wash_Product.CommitStatus = 1
            ");

            sql.Append(@" AND Wash_Product.ID IN(" + string.Join(",", iProductIDList.ToArray()) + ")");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var entity = web_Wash_ProductDC.GetEntity(reader);
                    if (entity != null)
                    {
                        list.Add(entity.ID, entity);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 根据分组获取运价
        /// </summary>
        /// <param name="iGroup"></param>
        /// <returns></returns>
        public IList<web_Wash_ProductDC> web_Wash_Product_SELECT_Group(int iGroup)
        {
            List<web_Wash_ProductDC> list = new List<web_Wash_ProductDC>();
            web_Wash_ProductDC entity = null;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Product.ID
                ,Wash_Product.Name
                ,Wash_Product.WebName
                ,Wash_Product.Content
                ,Wash_Product.SalesPrice 
                ,Wash_Product.MarketPrice
                ,Wash_Product.[Group]
                ,Wash_Product.CategoryID
                ,Wash_Category.PictureM
            FROM Wash_Product,Wash_Category
            WHERE Wash_Product.[Group] = @Group
                AND Wash_Product.CategoryID = Wash_Category.ID
                AND Wash_Product.Obj_Status = 1
                AND Wash_Product.VerifyStatus = 2 AND Wash_Product.CommitStatus = 1
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ProductID
            db.AddInParameter(cmd, "@Group", DbType.Int32, iGroup);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = web_Wash_ProductDC.GetEntity(reader);
                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 查询推荐产品
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public IList<web_Wash_ProductDC> web_Wash_Product_SELECT_Recommend(int iSite)
        {
            List<web_Wash_ProductDC> list = new List<web_Wash_ProductDC>();
            web_Wash_ProductDC entity = null;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Product.ID
                ,Wash_Product.Name
                ,Wash_Product.WebName
                ,Wash_Product.Content
                ,Wash_Product.SalesPrice
                ,Wash_Product.MarketPrice
                ,Wash_Product.CategoryID
                ,Wash_Category.PictureM
            FROM Wash_Product,Wash_Category
            WHERE Wash_Product.Site = @Site
                AND Wash_Product.Obj_Status = 1 AND Wash_Product.CategoryID = Wash_Category.ID
                AND Wash_Product.VerifyStatus = 2 AND Wash_Product.CommitStatus = 1
                AND Wash_Product.RecSort != 0
            ORDER BY Wash_Product.RecSort
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ProductID
            db.AddInParameter(cmd, "@Site", DbType.Int32, iSite);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = web_Wash_ProductDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 查询商场产品
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iClass"></param>
        /// <param name="iKeyword"></param>
        /// <param name="iTypeValue"></param>
        /// <param name="iSite"></param>
        /// <param name="iOrderType"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<web_Mall_ProductDC> web_Mall_Product_SELECT_List(int? iType, int? iClass,
            string iKeyword, string iTypeValue, int? iSite, int? iOrderType, int iPageIndex, int iPageSize)
        {
            List<web_Mall_ProductDC> list = new List<web_Mall_ProductDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Obj_Status = 1 AND Enable = 1 ");
            if (iOrderType.HasValue)
            {
                if (iOrderType.Value == 1)
                {
                    sqlorder.Append(@" TypeValue ");
                }
                else
                {
                    sqlorder.Append(@" SaleCount DESC ");
                }
            }
            else
            {
                sqlorder.Append(@" Sort ");
            }

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
            if (!string.IsNullOrEmpty(iKeyword))
                sqlwhere.Append("AND (Name LIKE '%" + iKeyword + "%' OR Keyword LIKE '%" + iKeyword + "%')");
            //类型用数值
            if (!string.IsNullOrEmpty(iTypeValue))
                sqlwhere.Append("AND TypeValue = '" + iTypeValue + "' ");

            var rtn = new RecordCountEntity<web_Mall_ProductDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Mall_Product", "ID", "*,PictureM Picture", sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Mall_Product", "ID", "*,PictureM Picture", sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    list.Add(web_Mall_ProductDC.GetEntity(reader));
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
        public web_Mall_ProductDC web_Mall_Product_SELECT_Entity(int iID)
        {
            web_Mall_ProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT *,PictureL Picture FROM Mall_Product WHERE ID = @ID AND Obj_Status = 1 AND Enable = 1 AND SellBeginDate <= GETDATE() AND SellEndDate >= GETDATE()");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = web_Mall_ProductDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iKeyword"></param>
        /// <param name="iSite"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<web_Wash_ClassDC> web_Wash_Category_SELECT_Search(
            string iKeyword, int iSite, int iPageIndex, int iPageSize)
        {
            if (!LazyAtHome.Core.Helper.DBHelper.CheckParms(iKeyword))
            {
                return new RecordCountEntity<web_Wash_ClassDC>()
                {
                    RecordCount = 0,
                    ReturnList = new List<web_Wash_ClassDC>()
                };
            }

            List<web_Wash_CategoryDC> list = new List<web_Wash_CategoryDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT  TOP 50
                Wash_Category.ID
                ,Wash_Category.Name
                ,Wash_Category.Content
                ,Wash_Category.PictureM
                ,Wash_Category.PictureAlt
                ,Wash_Category.ClassID
                ,Wash_Class.Name AS ClassName
            FROM Wash_Category,Wash_Class
            WHERE Wash_Category.Obj_Status = 1
            AND Wash_Category.ClassID = Wash_Class.ID
            ");

            sql.Append(@" AND EXISTS(SELECT 1 FROM Wash_Product WHERE Wash_Product.CategoryID = Wash_Category.ID
                            AND Wash_Product.Site = '" + iSite + @"' AND Wash_Product.Type = 1 AND Wash_Product.VerifyStatus = 2
                            AND Wash_Product.CommitStatus = 1 ");

            if (!string.IsNullOrWhiteSpace(iKeyword))
            {
                //sql.Append(" AND (Wash_Category.Keyword LIKE '%" + iKeyword + "%' ");

                //sql.Append(" AND (1 = 1 ");
                sql.Append(@" AND Wash_Product.Name LIKE '%" + iKeyword + "%' ");
                //sql.Append(" OR  1 = 1 )");

                //sql.Append(" OR Wash_Category.Name LIKE '%" + iKeyword + "%' )");
            }

            sql.Append(" AND Wash_Product.Obj_Status = 1) ");

            sql.Append(" ORDER BY Wash_Category.Sort ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@Site", DbType.Int32, iSite);

            var rtn = new RecordCountEntity<web_Wash_ClassDC>();

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var entity = web_Wash_CategoryDC.GetEntity(reader);
                    if (entity != null)
                    {
                        entity.ProductList = web_Wash_Product_SELECT_List(entity.ID, iKeyword);
                        list.Add(entity);
                    }

                }
            }

            var dilist = new Dictionary<int, web_Wash_ClassDC>();

            foreach (var item in list)
            {
                if (dilist.ContainsKey(item.ClassID))
                {
                    dilist[item.ClassID].CategoryList.Add(item);
                }
                else
                {
                    dilist.Add(item.ClassID, new web_Wash_ClassDC()
                    {
                        ID = item.ClassID,
                        Name = item.ClassName,
                        CategoryList = new List<web_Wash_CategoryDC>(),
                    });
                    dilist[item.ClassID].CategoryList.Add(item);
                }
            }

            rtn.ReturnList = dilist.Values.ToList();

            return rtn;

            //StringBuilder sqlwhere = new StringBuilder();
            //StringBuilder sqlfield = new StringBuilder();
            //StringBuilder sqlorder = new StringBuilder();
            //            sqlwhere.Append(@" Wash_Category.Obj_Status = 1 ");
            //            sqlwhere.Append(@" AND EXISTS(SELECT 1 FROM Wash_Product WHERE Wash_Product.CategoryID = Wash_Category.ID
            //                AND Wash_Product.Site = @Site AND Wash_Product.Type = 1 AND Wash_Product.VerifyStatus = 2
            //                AND Wash_Product.CommitStatus = 1 AND Wash_Product.Obj_Status = 1) ");


            //            sqlorder.Append(@" Wash_Category.Sort,Wash_Category.ID ");

            //            sqlfield.Append(@" Wash_Category.ID,Wash_Category.Name,Wash_Category.Content,Wash_Category.PictureM ");

            //            //类型
            //            if (!string.IsNullOrWhiteSpace(iKeyword))
            //            {
            //                sqlwhere.Append("AND Keyword LIKE '%" + iKeyword + "%' ");
            //            }


            //            sqlwhere.Append("AND Site = '" + iSite + "' ");

            //            var rtn = new RecordCountEntity<web_Wash_CategoryDC>();

            //            //取得总数量
            //            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Wash_Category", "ID", sqlfield.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //            //取得列表
            //            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Wash_Category", "ID", sqlfield.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            //            {
            //                while (reader.Read())
            //                {
            //                    list.Add(web_Wash_CategoryDC.GetEntity(reader));
            //                }
            //            }
            //            rtn.ReturnList = list;
            //            return rtn;
        }

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public IList<Wash_ActivityDC> web_Wash_Activity_SELECT_List(int iSite)
        {
            List<Wash_ActivityDC> list = new List<Wash_ActivityDC>();

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append(@"
            SELECT  * FROM Wash_Activity
                WHERE Obj_Status = 1 AND Site = @Site AND Channel = 1 
                AND BeginDate <= GETDATE() AND EndDate >= GETDATE()
            ");

            sql.Append(" ORDER BY Sort ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Site", DbType.Int32, iSite);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var entity = Wash_ActivityDC.GetEntity(reader);
                    if (entity != null)
                    {
                        list.Add(entity);
                    }

                }
            }
            return list;
        }
    }
}
