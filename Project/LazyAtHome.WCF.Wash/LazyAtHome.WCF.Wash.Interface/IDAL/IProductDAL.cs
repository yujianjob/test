using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Interface.IDAL
{
    public interface IProductDAL
    {
        #region 类别

        /// <summary>
        /// 创建code
        /// </summary>
        /// <param name="iParentID"></param>
        /// <returns></returns>
        string Wash_Class_ADD_CreateCode(int iParentID);

        /// <summary>
        /// 新增类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        int Wash_Class_ADD(Wash_ClassDC iWash_ClassDC);

        /// <summary>
        /// 更新类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        bool Wash_Class_UPDATE(Wash_ClassDC iWash_ClassDC);

        /// <summary>
        /// 根据ID查询类别
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Wash_ClassDC Wash_Class_SELECT_Entity(int iID);

        /// <summary>
        /// 查询全部类别
        /// </summary>
        /// <returns></returns>
        IList<Wash_ClassDC> Wash_Class_SELECT_List_ALL();

        #endregion

        #region 属性

        /// <summary>
        /// 创建code
        /// </summary>
        /// <param name="iParentID"></param>
        /// <returns></returns>
        string Wash_Attribute_ADD_CreateCode(int iParentID);

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        int Wash_Attribute_ADD(Wash_AttributeDC iWash_AttributeDC);

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        bool Wash_Attribute_UPDATE(Wash_AttributeDC iWash_AttributeDC);

        /// <summary>
        /// 根据ID查询属性
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Wash_AttributeDC Wash_Attribute_SELECT_Entity(int iID);

        /// <summary>
        /// 查询全部属性
        /// </summary>
        /// <returns></returns>
        IList<Wash_AttributeDC> Wash_Attribute_SELECT_List_ALL();

        #endregion

        #region 产品

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        int Wash_Category_ADD(Wash_CategoryDC iWash_CategoryDC);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        bool Wash_Category_UPDATE(Wash_CategoryDC iWash_CategoryDC);

        /// <summary>
        /// 产品单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Wash_CategoryDC Wash_Category_SELECT_Entity(int iID);

        /// <summary>
        /// 产品列表页
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iEnable"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<Wash_CategoryDC> Wash_Category_SELECT_List(string iName, string iCode, int? iEnable,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 产品列表页
        /// </summary>
        /// <returns></returns>
        IList<Wash_CategoryDC> Wash_Category_SELECT_List_ByClassID(int iClassID);

        #endregion

        #region 运价

        /// <summary>
        /// 新增产品价目
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        int Wash_Product_ADD(Wash_ProductDC iWash_ProductDC);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        bool Wash_Product_UPDATE(Wash_ProductDC iWash_ProductDC);

        /// <summary>
        /// 价目单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Wash_ProductDC Wash_Product_SELECT_Entity(int iID);

        /// <summary>
        /// 产品列表
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
        RecordCountEntity<Wash_ProductDC> Wash_Product_SELECT_List(string iName, string iCode, int? iType,
            int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        IList<Wash_ProductDC> Wash_Product_SELECT_List_CategoryID(int iCategoryID);

        /// <summary>
        /// 产品价目属性查询
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        IList<Wash_ProductAttributeDC> Wash_ProductAttribute_SELECT(int iProductID);
        #endregion

        #region 合作门店

        /// <summary>
        /// 新增门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        int Wash_Store_ADD(Wash_StoreDC iWash_StoreDC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns></returns>
        bool Wash_Store_CodeExists(string iCode);

        /// <summary>
        /// 更新门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        bool Wash_Store_UPDATE(Wash_StoreDC iWash_StoreDC);

        /// <summary>
        /// 查询门店
        /// </summary>
        /// <returns></returns>
        RecordCountEntity<Wash_StoreDC> Wash_Store_SELECT_List(string iName, string iCode, int? iSite,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Wash_StoreDC Wash_Store_SELECT_Entity(int iID);

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        Wash_StoreDC Wash_Store_SELECT_Entity(Guid iStoreID);

        /// <summary>
        /// 新增门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        int Wash_Store_Operator_ADD(Wash_Store_OperatorDC iWash_Store_OperatorDC);


        /// <summary>
        /// 更新门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        bool Wash_Store_Operator_UPDATE(Wash_Store_OperatorDC iWash_Store_OperatorDC);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        bool Wash_Store_Operator_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IList<Wash_Store_OperatorDC> Wash_Store_Operator_SELECT_ALL(int iStoreID);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Wash_Store_OperatorDC Wash_Store_Operator_SELECT_Entity(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStoreCode"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <returns></returns>
        Wash_StoreLoginInfoDC Wash_Store_Operator_SELECT_Entity(string iStoreCode, string iLoginName, string iPassword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        bool Wash_Store_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword, int iMuser);

        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iWash_Store_OperatorLogDC"></param>
        /// <returns></returns>
        int Wash_Store_OperatorLog_ADD(Wash_Store_OperatorLogDC iWash_Store_OperatorLogDC);

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
        RecordCountEntity<Wash_Store_OperatorLogDC> Wash_Store_OperatorLog_SELECT_List(int? iType, string iOperatorName,
            string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);



        #endregion

        #region 商场产品

        /// <summary>
        /// 新增商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        int Mall_Product_ADD(Mall_ProductDC iMall_ProductDC);

        /// <summary>
        /// 更新商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        bool Mall_Product_UPDATE(Mall_ProductDC iMall_ProductDC);

        /// <summary>
        /// 删除商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        bool Mall_Product_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询商场产品
        /// </summary>
        /// <returns></returns>
        RecordCountEntity<Mall_ProductDC> Mall_Product_SELECT_List(int? iType, int? iClass,
          string iName, int? iSite, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        Mall_ProductDC Mall_Product_SELECT_Entity(int iID);

        #endregion

        #region 活动

        /// <summary>
        /// 新增活动
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        int Wash_Activity_ADD(Wash_ActivityDC iWash_ActivityDC);

        /// <summary>
        /// 活动更新
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        bool Wash_Activity_UPDATE(Wash_ActivityDC iWash_ActivityDC);

        /// <summary>
        /// 活动单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        Wash_ActivityDC Wash_Activity_SELECT_Entity(int iID);

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<Wash_ActivityDC> Wash_Activity_SELECT_List(string iName, int? iCommitStatus,
            int? iSite, int? iChannel,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        #endregion
    }
}
