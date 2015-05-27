using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Business.Business
{
    public class web_Product
    {
        private LazyAtHome.WCF.Wash.Interface.IDAL.Iweb_ProductDAL web_ProductDAL;
        public web_Product()
        {
            if (web_ProductDAL == null)
                web_ProductDAL = new LazyAtHome.WCF.Wash.DAL.web_ProductDAL();
        }

        static web_Product _web_Product;

        public static web_Product Instance
        {
            get
            {
                if (_web_Product == null)
                {
                    _web_Product = new web_Product();
                }
                return _web_Product;
            }
        }

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_ClassDC>> web_Wash_Class_SELECT_List()
        {
            var list = web_ProductDAL.web_Wash_Class_SELECT_List();
            if (list != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WEB_Wash_Class_List", list, CacheTimer.GetTimeSpanOneHour);
            }
            return new ReturnEntity<IList<web_Wash_ClassDC>>(list);
        }

        /// <summary>
        /// 根据小类ID获取运产品表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_CategoryDC>> web_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            var list = web_ProductDAL.web_Wash_Category_SELECT_List(iClassID, iSite);
            if (list != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WEB_Wash_Category_ClassID_" + iClassID + "_" + iSite, list, CacheTimer.GetTimeSpanOneHour);
            }
            return new ReturnEntity<IList<web_Wash_CategoryDC>>(list);
        }

        /// <summary>
        /// 根据产品ID获取产品
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public ReturnEntity<web_Wash_CategoryDC> web_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            var entity = web_ProductDAL.web_Wash_Category_SELECT_Entity(iCategoryID);
            if (entity != null && entity.ProductList.Count > 0)
            {
                var groupid = entity.ProductList.Max(p => p.Group);
                if (groupid > 0)
                {
                    entity.GroupList = web_ProductDAL.web_Wash_Product_SELECT_Group(groupid);
                }

                LazyAtHome.Core.Helper.CacheHelper.Put("WEB_Wash_Category_ID_" + iCategoryID, entity, CacheTimer.GetTimeSpanOneHour);
                return new ReturnEntity<web_Wash_CategoryDC>(entity);
            }
            else
            {
                return new ReturnEntity<web_Wash_CategoryDC>(-1, "产品不存在");
            }
        }

        /// <summary>
        /// 根据运价ID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public ReturnEntity<web_Wash_ProductDC> web_Wash_Product_SELECT_Entity(int iProductID)
        {
            var entity = web_ProductDAL.web_Wash_Product_SELECT_Entity(iProductID);
            if (entity != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WEB_Wash_Product_ID_" + iProductID, entity, CacheTimer.GetTimeSpanOneHour);
                return new ReturnEntity<web_Wash_ProductDC>(entity);
            }
            else
            {
                return new ReturnEntity<web_Wash_ProductDC>(-1, "运价不存在");
            }
        }

        /// <summary>
        /// 根据ProductIDList获取运价
        /// </summary>
        /// <param name="iProductIDList"></param>
        /// <returns></returns>
        public ReturnEntity<IDictionary<int, web_Wash_ProductDC>> web_Wash_Product_SELECT_Entity(IList<int> iProductIDList)
        {
            var list = web_ProductDAL.web_Wash_Product_SELECT_Entity(iProductIDList);

            foreach (var item in list)
            {
                if (item.Value == null)
                    return new ReturnEntity<IDictionary<int, web_Wash_ProductDC>>(-1, "产品已失效" + item.Key);
            }

            return new ReturnEntity<IDictionary<int, web_Wash_ProductDC>>(list);
        }

        /// <summary>
        /// 查询小类(包括运价)
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_ClassDC>> web_Wash_Class_SELECT_List_Second_Detail(int iSite)
        {
            var classList = web_ProductDAL.web_Wash_Class_SELECT_List();
            if (classList == null)
            {
                return new ReturnEntity<IList<web_Wash_ClassDC>>(-1, "类别读取失败");
            }

            classList = classList.Where(p => p.ParentID != 0).ToList();

            foreach (var item in classList)
            {
                var categoryList = web_ProductDAL.web_Wash_Category_SELECT_List(item.ID, iSite);
                if (categoryList == null || categoryList.Count == 0)
                {
                    //去除没有产品的类别
                    item.ID = 0;
                    continue;
                }
                foreach (var category in categoryList)
                {
                    var productList = web_ProductDAL.web_Wash_Product_SELECT_List(category.ID);
                    if (productList == null || productList.Count == 0)
                    {
                        category.ID = 0;
                        continue;
                    }
                    category.ProductList = productList;
                }
                categoryList = categoryList.Where(p => p.ID != 0).ToList();
                if (categoryList.Count == 0)
                {
                    item.ID = 0;
                    continue;
                }
                item.CategoryList = categoryList;
            }
            classList = classList.Where(p => p.ID != 0).ToList();

            return new ReturnEntity<IList<web_Wash_ClassDC>>(classList);
        }

        /// <summary>
        /// 查询推荐产品
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_ProductDC>> web_Wash_Product_SELECT_Recommend(int iSite)
        {
            var list = web_ProductDAL.web_Wash_Product_SELECT_Recommend(iSite);
            if (list != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WEB_Wash_Product_Recommend_" + iSite, list, CacheTimer.GetTimeSpanOneHour);
                return new ReturnEntity<IList<web_Wash_ProductDC>>(list);
            }
            else
            {
                return new ReturnEntity<IList<web_Wash_ProductDC>>(-1, "推荐产品不存在");
            }
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
        public ReturnEntity<RecordCountEntity<web_Mall_ProductDC>> web_Mall_Product_SELECT_List(int? iType, int? iClass,
            string iKeyword, string iTypeValue, int? iSite, int? iOrderType, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<web_Mall_ProductDC>>(web_ProductDAL.web_Mall_Product_SELECT_List(
                iType, iClass, iKeyword, iTypeValue, iSite, iOrderType, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<web_Mall_ProductDC> web_Mall_Product_SELECT_Entity(int iID)
        {
            return new ReturnEntity<web_Mall_ProductDC>(web_ProductDAL.web_Mall_Product_SELECT_Entity(iID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iKeyword"></param>
        /// <param name="iSite"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<web_Wash_ClassDC>> web_Wash_Category_SELECT_Search(
            string iKeyword, int iSite, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<web_Wash_ClassDC>>(web_ProductDAL.web_Wash_Category_SELECT_Search(iKeyword, iSite, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ActivityDC>> web_Wash_Activity_SELECT_List(int iSite)
        {
            var list = web_ProductDAL.web_Wash_Activity_SELECT_List(iSite);

            if (list != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WEB_Wash_Activity_" + iSite, list, CacheTimer.GetTimeSpanOneHour);
                return new ReturnEntity<IList<Wash_ActivityDC>>(list);
            }
            else
            {
                return new ReturnEntity<IList<Wash_ActivityDC>>(-1, "活动列表不存在");
            }

            return new ReturnEntity<IList<Wash_ActivityDC>>(list);
        }

    }
}
