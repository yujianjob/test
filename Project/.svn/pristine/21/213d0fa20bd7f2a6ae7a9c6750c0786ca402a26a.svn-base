using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract.weixin;
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
    public class wx_ProductDAL : DALBase, LazyAtHome.WCF.Wash.Interface.IDAL.Iwx_ProductDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public wx_ProductDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        public IList<wx_Wash_ClassDC> wx_Wash_Class_SELECT_List()
        {
            List<wx_Wash_ClassDC> list = new List<wx_Wash_ClassDC>();
            wx_Wash_ClassDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT ID, Name, PictureS Picture FROM Wash_Class WHERE Obj_Status = 1 AND ParentID != 0");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = wx_Wash_ClassDC.GetEntity(reader);
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
        public IList<wx_Wash_CategoryDC> wx_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            List<wx_Wash_CategoryDC> list = new List<wx_Wash_CategoryDC>();
            wx_Wash_CategoryDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Category.ID
                ,Wash_Category.Name
                ,Wash_Category.Content
                ,Wash_Category.PictureS
            FROM Wash_Category
            WHERE Obj_Status = 1
                AND ClassID = @ClassID
                AND EXISTS(SELECT 1 FROM Wash_Product WHERE Wash_Product.CategoryID = Wash_Category.ID
                    AND Wash_Product.Site = @Site AND Wash_Product.Type = 1 AND Wash_Product.VerifyStatus = 2
                    AND Wash_Product.CommitStatus = 1 AND Wash_Product.Obj_Status = 1)
            ORDER BY Wash_Category.Sort,Wash_Category.ID
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ClassID
            db.AddInParameter(cmd, "@ClassID", DbType.Int32, iClassID);

            db.AddInParameter(cmd, "@Site", DbType.Int32, iSite);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = wx_Wash_CategoryDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public wx_Wash_CategoryDC wx_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            wx_Wash_CategoryDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Category.ID
                ,Wash_Category.Name
                ,Wash_Category.Content
                ,Wash_Category.PictureS
            FROM Wash_Category
            WHERE Obj_Status = 1 AND ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ClassID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iCategoryID);


            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = wx_Wash_CategoryDC.GetEntity(reader);
                    entity.ProductList = wx_Wash_Product_SELECT_List(entity.ID);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据CategoryID获取运价
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        private IList<wx_Wash_ProductDC> wx_Wash_Product_SELECT_List(int iCategoryID)
        {
            List<wx_Wash_ProductDC> list = new List<wx_Wash_ProductDC>();
            wx_Wash_ProductDC entity = null;

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT 
                Wash_Product.ID
                ,Wash_Product.WebName AS Name
                ,Wash_Product.Content 
                ,Wash_Product.SalesPrice
                ,Wash_Product.MarketPrice
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
                    entity = wx_Wash_ProductDC.GetEntity(reader);
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
        public wx_Wash_ProductDC wx_Wash_Product_SELECT_Entity(int iProductID)
        {
            wx_Wash_ProductDC entity = null;

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
                ,Wash_Category.PictureS
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
                    entity = wx_Wash_ProductDC.GetEntity(reader);
                }
            }
            return entity;
        }
    }
}
