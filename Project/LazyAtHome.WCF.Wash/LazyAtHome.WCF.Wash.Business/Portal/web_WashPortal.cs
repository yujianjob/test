using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;
using LazyAtHome.WCF.Wash.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Business.Portal
{
    public partial class WashPortal : IwebProduct
    {

        protected LazyAtHome.WCF.Wash.Business.Business.web_Product web_ProductInstance = LazyAtHome.WCF.Wash.Business.Business.web_Product.Instance;

        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_ClassDC>> web_Wash_Class_SELECT_List()
        {
            Log_DeBug("web_Wash_Class_SELECT_List");
            try
            {
                return web_ProductInstance.web_Wash_Class_SELECT_List();
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Class_SELECT_List", ex);
                return new ReturnEntity<IList<web_Wash_ClassDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Class_SELECT_List");
            }
        }

        /// <summary>
        /// 根据小类ID获取运产品表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_CategoryDC>> web_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            Log_DeBug("web_Wash_Category_SELECT_List", iClassID);
            try
            {
                return web_ProductInstance.web_Wash_Category_SELECT_List(iClassID, iSite);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Category_SELECT_List", ex);
                return new ReturnEntity<IList<web_Wash_CategoryDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Category_SELECT_List");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public ReturnEntity<web_Wash_CategoryDC> web_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            Log_DeBug("web_Wash_Category_SELECT_Entity", iCategoryID);
            try
            {
                return web_ProductInstance.web_Wash_Category_SELECT_Entity(iCategoryID);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Category_SELECT_Entity", ex);
                return new ReturnEntity<web_Wash_CategoryDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Category_SELECT_Entity");
            }
        }

        /// <summary>
        /// 根据运价ID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public ReturnEntity<web_Wash_ProductDC> web_Wash_Product_SELECT_Entity(int iProductID)
        {
            Log_DeBug("web_Wash_Product_SELECT_Entity", iProductID);
            try
            {
                return web_ProductInstance.web_Wash_Product_SELECT_Entity(iProductID);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Product_SELECT_Entity", ex);
                return new ReturnEntity<web_Wash_ProductDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Product_SELECT_Entity");
            }
        }


        /// <summary>
        /// 根据ProductIDList获取运价
        /// </summary>
        /// <param name="iProductIDList"></param>
        /// <returns></returns>
        public ReturnEntity<IDictionary<int, web_Wash_ProductDC>> web_Wash_Product_SELECT_Entity(IList<int> iProductIDList)
        {
            Log_DeBug("web_Wash_Product_SELECT_Entity", iProductIDList);
            try
            {
                return web_ProductInstance.web_Wash_Product_SELECT_Entity(iProductIDList);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Product_SELECT_Entity", ex);
                return new ReturnEntity<IDictionary<int, web_Wash_ProductDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Product_SELECT_Entity");
            }
        }

        /// <summary>
        /// 查询小类(包括运价)
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_ClassDC>> web_Wash_Class_SELECT_List_Second_Detail(int iSite)
        {
            Log_DeBug("web_Wash_Class_SELECT_List_Second_Detail", iSite);
            try
            {
                return web_ProductInstance.web_Wash_Class_SELECT_List_Second_Detail(iSite);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Class_SELECT_List_Second_Detail", ex);
                return new ReturnEntity<IList<web_Wash_ClassDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Class_SELECT_List_Second_Detail");
            }
        }

        /// <summary>
        /// 查询推荐产品(主页用)
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<web_Wash_ProductDC>> web_Wash_Product_SELECT_Recommend(int iSite)
        {
            Log_DeBug("web_Wash_Product_SELECT_Recommend", iSite);
            try
            {
                return web_ProductInstance.web_Wash_Product_SELECT_Recommend(iSite);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Product_SELECT_Recommend", ex);
                return new ReturnEntity<IList<web_Wash_ProductDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Product_SELECT_Recommend");
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
            Log_DeBug("web_Mall_Product_SELECT_List", iType, iClass, iKeyword, iTypeValue, iSite, iOrderType, iPageIndex, iPageSize);
            try
            {
                return web_ProductInstance.web_Mall_Product_SELECT_List(iType, iClass, iKeyword, iTypeValue, iSite, iOrderType, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Mall_Product_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<web_Mall_ProductDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Mall_Product_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<web_Mall_ProductDC> web_Mall_Product_SELECT_Entity(int iID)
        {
            Log_DeBug("web_Mall_Product_SELECT_Entity", iID);
            try
            {
                return web_ProductInstance.web_Mall_Product_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Mall_Product_SELECT_Entity", ex);
                return new ReturnEntity<web_Mall_ProductDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Mall_Product_SELECT_Entity");
            }
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
            Log_DeBug("web_Wash_Category_SELECT_Search", iKeyword, iSite, iPageIndex, iPageSize);
            try
            {
                return web_ProductInstance.web_Wash_Category_SELECT_Search(iKeyword, iSite, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Category_SELECT_Search", ex);
                return new ReturnEntity<RecordCountEntity<web_Wash_ClassDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Category_SELECT_Search");
            }
        }

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ActivityDC>> web_Wash_Activity_SELECT_List(int iSite)
        {
            Log_DeBug("web_Wash_Activity_SELECT_List", iSite);
            try
            {
                return web_ProductInstance.web_Wash_Activity_SELECT_List(iSite);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Wash_Activity_SELECT_List", ex);
                return new ReturnEntity<IList<Wash_ActivityDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Wash_Activity_SELECT_List");
            }
        }
    }
}
