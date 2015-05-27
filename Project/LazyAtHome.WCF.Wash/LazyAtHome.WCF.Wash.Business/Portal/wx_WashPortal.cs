using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract.weixin;
using LazyAtHome.WCF.Wash.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Business.Portal
{
    public partial class WashPortal : IwxProduct
    {

        protected LazyAtHome.WCF.Wash.Business.Business.wx_Product wx_ProductInstance = LazyAtHome.WCF.Wash.Business.Business.wx_Product.Instance;

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<wx_Wash_ClassDC>> wx_Wash_Class_SELECT_List()
        {
            Log_DeBug("wx_Wash_Class_SELECT_List");
            try
            {
                return wx_ProductInstance.wx_Wash_Class_SELECT_List();
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_Wash_Class_SELECT_List", ex);
                return new ReturnEntity<IList<wx_Wash_ClassDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_Wash_Class_SELECT_List");
            }
        }

        /// <summary>
        /// 根据小类ID获取运产品表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        public ReturnEntity<IList<wx_Wash_CategoryDC>> wx_Wash_Category_SELECT_List(int iClassID, int iSite)
        {
            Log_DeBug("wx_Wash_Category_SELECT_List", iClassID);
            try
            {
                return wx_ProductInstance.wx_Wash_Category_SELECT_List(iClassID, iSite);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_Wash_Category_SELECT_List", ex);
                return new ReturnEntity<IList<wx_Wash_CategoryDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_Wash_Category_SELECT_List");
            }
        }

        /// <summary>
        /// 根据CategoryID获取运价
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public ReturnEntity<wx_Wash_CategoryDC> wx_Wash_Category_SELECT_Entity(int iCategoryID)
        {
            Log_DeBug("wx_Wash_Category_SELECT_Entity", iCategoryID);
            try
            {
                return wx_ProductInstance.wx_Wash_Category_SELECT_Entity(iCategoryID);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_Wash_Category_SELECT_Entity", ex);
                return new ReturnEntity<wx_Wash_CategoryDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_Wash_Category_SELECT_Entity");
            }
        }

        /// <summary>
        /// 根据ProductID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public ReturnEntity<wx_Wash_ProductDC> wx_Wash_Product_SELECT_Entity(int iProductID)
        {
            Log_DeBug("wx_Wash_Product_SELECT_Entity", iProductID);
            try
            {
                return wx_ProductInstance.wx_Wash_Product_SELECT_Entity(iProductID);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_Wash_Product_SELECT_Entity", ex);
                return new ReturnEntity<wx_Wash_ProductDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_Wash_Product_SELECT_Entity");
            }
        }
    }
}
