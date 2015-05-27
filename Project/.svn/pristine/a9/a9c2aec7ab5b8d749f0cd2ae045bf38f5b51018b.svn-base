using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Interface.IDAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface Iweb_ProductDAL
    {
        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        IList<web_Wash_ClassDC> web_Wash_Class_SELECT_List();

        /// <summary>
        /// 根据小类ID获取运产品表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        IList<web_Wash_CategoryDC> web_Wash_Category_SELECT_List(int iClassID, int iSite);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        web_Wash_CategoryDC web_Wash_Category_SELECT_Entity(int iCategoryID);

        /// <summary>
        /// 根据CategoryID获取运价
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        IList<web_Wash_ProductDC> web_Wash_Product_SELECT_List(int iCategoryID);

        /// <summary>
        /// 根据ProductID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        web_Wash_ProductDC web_Wash_Product_SELECT_Entity(int iProductID);

        /// <summary>
        /// 根据ProductIDList获取运价
        /// </summary>
        /// <param name="iProductIDList"></param>
        /// <returns></returns>
        IDictionary<int, web_Wash_ProductDC> web_Wash_Product_SELECT_Entity(IList<int> iProductIDList);

        /// <summary>
        /// 分组获取运价
        /// </summary>
        /// <param name="iGroup"></param>
        /// <returns></returns>
        IList<web_Wash_ProductDC> web_Wash_Product_SELECT_Group(int iGroup);

        /// <summary>
        /// 查询推荐产品
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        IList<web_Wash_ProductDC> web_Wash_Product_SELECT_Recommend(int iSite);

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
        RecordCountEntity<web_Mall_ProductDC> web_Mall_Product_SELECT_List(int? iType, int? iClass,
          string iKeyword, string iTypeValue, int? iSite, int? iOrderType, int iPageIndex, int iPageSize);


        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        web_Mall_ProductDC web_Mall_Product_SELECT_Entity(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iKeyword"></param>
        /// <param name="iSite"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<web_Wash_ClassDC> web_Wash_Category_SELECT_Search(
          string iKeyword, int iSite, int iPageIndex, int iPageSize);

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iSite"></param>
        /// <returns></returns>
        IList<Wash_ActivityDC> web_Wash_Activity_SELECT_List(int iSite);
    }
}
