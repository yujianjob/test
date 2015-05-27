using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract.weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Business.Business
{
    public class wx_Product
    {
        private LazyAtHome.WCF.Wash.Interface.IDAL.Iwx_ProductDAL wx_ProductDAL;
        public wx_Product()
        {
            if (wx_ProductDAL == null)
                wx_ProductDAL = new LazyAtHome.WCF.Wash.DAL.wx_ProductDAL();
        }

        static wx_Product _wx_Product;

        public static wx_Product Instance
        {
            get
            {
                if (_wx_Product == null)
                {
                    _wx_Product = new wx_Product();
                }
                return _wx_Product;
            }
        }

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<wx_Wash_ClassDC>> wx_Wash_Class_SELECT_List()
        {
            var list = wx_ProductDAL.wx_Wash_Class_SELECT_List();
            if (list != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WX_Wash_Class_List", list, CacheTimer.GetTimeSpanOneHour);
            }
            return new ReturnEntity<IList<wx_Wash_ClassDC>>(list);
        }

        /// <summary>
        /// 根据小类ID获取运产品表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<wx_Wash_CategoryDC>> wx_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            var list = wx_ProductDAL.wx_Wash_Category_SELECT_List(iClassID, iSite);
            if (list != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WX_Wash_Category_ClassID_" + iClassID + "_" + iSite, list, CacheTimer.GetTimeSpanOneHour);
            }
            return new ReturnEntity<IList<wx_Wash_CategoryDC>>(list);
        }

        /// <summary>
        /// 根据产品ID获取产品
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public ReturnEntity<wx_Wash_CategoryDC> wx_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            var entity = wx_ProductDAL.wx_Wash_Category_SELECT_Entity(iCategoryID);
            if (entity != null && entity.ProductList.Count > 0)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WX_Wash_Category_ID_" + iCategoryID, entity, CacheTimer.GetTimeSpanOneHour);
                return new ReturnEntity<wx_Wash_CategoryDC>(entity);
            }
            else
            {
                return new ReturnEntity<wx_Wash_CategoryDC>(-1, "产品不存在");
            }
        }

        /// <summary>
        /// 根据运价ID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public ReturnEntity<wx_Wash_ProductDC> wx_Wash_Product_SELECT_Entity(int iProductID)
        {
            var entity = wx_ProductDAL.wx_Wash_Product_SELECT_Entity(iProductID);
            if (entity != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WX_Wash_Product_ID_" + iProductID, entity, CacheTimer.GetTimeSpanOneHour);
                return new ReturnEntity<wx_Wash_ProductDC>(entity);
            }
            else
            {
                return new ReturnEntity<wx_Wash_ProductDC>(-1, "运价不存在");
            }
        }
    }
}
