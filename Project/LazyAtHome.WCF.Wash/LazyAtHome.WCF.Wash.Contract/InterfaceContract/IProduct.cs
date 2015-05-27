using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IProduct
    {
        #region 类别

        /// <summary>
        /// 新增类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Wash_Class_ADD(Wash_ClassDC iWash_ClassDC);

        /// <summary>
        /// 更新类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Class_UPDATE(Wash_ClassDC iWash_ClassDC);

        /// <summary>
        /// 根据ID查询类别
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_ClassDC> Wash_Class_SELECT_Entity(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Wash_ClassDC>> Wash_Class_SELECT_List_ALL();

        #endregion

        #region 属性

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Wash_Attribute_ADD(Wash_AttributeDC iWash_AttributeDC);

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Attribute_UPDATE(Wash_AttributeDC iWash_AttributeDC);

        /// <summary>
        /// 根据ID查询属性
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_AttributeDC> Wash_Attribute_SELECT_Entity(int iID);


        #endregion

        #region 产品

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Wash_Category_ADD(Wash_CategoryDC iWash_CategoryDC);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Category_UPDATE(Wash_CategoryDC iWash_CategoryDC);

        /// <summary>
        /// 产品单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_CategoryDC> Wash_Category_SELECT_Entity(int iID);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Wash_CategoryDC>> Wash_Category_SELECT_List(
          string iName, string iCode, int? iEnable, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 产品查询列表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Wash_CategoryDC>> Wash_Category_SELECT_List_ByClassID(int iClassID);

        #endregion

        #region 运价

        /// <summary>
        /// 新增产品价目
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Wash_Product_ADD(Wash_ProductDC iWash_ProductDC);

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Product_UPDATE(Wash_ProductDC iWash_ProductDC);

        /// <summary>
        /// 价目单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_ProductDC> Wash_Product_SELECT_Entity(int iID);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Wash_ProductDC>> Wash_Product_SELECT_List(string iName, string iCode,
            int? iType, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Wash_ProductDC>> Wash_Product_SELECT_List_CategoryID(int iCategoryID);

        /// <summary>
        /// 产品价目属性查询
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Wash_ProductAttributeDC>> Wash_ProductAttribute_SELECT(int iProductID);
        #endregion

        #region 合作门店

        /// <summary>
        /// 新增门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Store_ADD(Wash_StoreDC iWash_StoreDC);

        /// <summary>
        /// 更新门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Store_UPDATE(Wash_StoreDC iWash_StoreDC);

        /// <summary>
        /// 查询门店
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Wash_StoreDC>> Wash_Store_SELECT_List(string iName, string iCode, int? iSite,
        DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_StoreDC> Wash_Store_SELECT_Entity(int iID);

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        [OperationContract(Name = "Wash_Store_SELECT_Entity_GUID")]
        ReturnEntity<Wash_StoreDC> Wash_Store_SELECT_Entity(Guid iStoreID);

        /// <summary>
        /// 新增门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Store_Operator_ADD(Wash_Store_OperatorDC iWash_Store_OperatorDC);


        /// <summary>
        /// 更新门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Store_Operator_UPDATE(Wash_Store_OperatorDC iWash_Store_OperatorDC);

        /// <summary>
        /// 删除门店操作员
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Store_Operator_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询全部门店操作员
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Wash_Store_OperatorDC>> Wash_Store_Operator_SELECT_ALL(int iStoreID);

        /// <summary>
        /// 根据ID查询门店操作员
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_Store_OperatorDC> Wash_Store_Operator_SELECT_Entity(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStoreCode"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_StoreLoginInfoDC> Wash_Store_Login(string iStoreCode, string iLoginName, string iPassword);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Store_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword, int iMuser);

        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Store_OperatorLog_ADD(int iType, int iOperatorID, string iOperatorName, string iLogContent);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Wash_Store_OperatorLogDC>> Wash_Store_OperatorLog_SELECT_List(int? iType, string iOperatorName,
          string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);

        #endregion

        #region 商场产品

        /// <summary>
        /// 新增商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Mall_Product_ADD(Mall_ProductDC iMall_ProductDC);

        /// <summary>
        /// 更新商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Mall_Product_UPDATE(Mall_ProductDC iMall_ProductDC);

        /// <summary>
        /// 查询商场产品
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Mall_ProductDC>> Mall_Product_SELECT_List(int? iType, int? iClass,
          string iName, int? iSite, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Mall_ProductDC> Mall_Product_SELECT_Entity(int iID);

        #endregion

        #region 活动

        /// <summary>
        /// 新增活动
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Wash_Activity_ADD(Wash_ActivityDC iWash_ActivityDC);

        /// <summary>
        /// 活动更新
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Wash_Activity_UPDATE(Wash_ActivityDC iWash_ActivityDC);

        /// <summary>
        /// 活动单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Wash_ActivityDC> Wash_Activity_SELECT_Entity(int iID);

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Wash_ActivityDC>> Wash_Activity_SELECT_List(string iName,
            int? iSite, int? iChannel,
            int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        #endregion
    }
}
