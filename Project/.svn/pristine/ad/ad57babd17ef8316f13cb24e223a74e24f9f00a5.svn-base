using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Business.Business;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Business.Portal
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WashPortal : IProduct
    {
        protected LazyAtHome.WCF.Wash.Business.Business.Product ProductInstance = LazyAtHome.WCF.Wash.Business.Business.Product.Instance;

        public ReturnEntity<int> Wash_Class_ADD(Wash_ClassDC iWash_ClassDC)
        {
            Log_DeBug("Wash_Class_ADD", iWash_ClassDC);
            try
            {
                return ProductInstance.Wash_Class_ADD(iWash_ClassDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Class_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Class_ADD");
            }
        }

        public ReturnEntity<bool> Wash_Class_UPDATE(Wash_ClassDC iWash_ClassDC)
        {
            Log_DeBug("Wash_Class_UPDATE", iWash_ClassDC);
            try
            {
                return ProductInstance.Wash_Class_UPDATE(iWash_ClassDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Class_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Class_UPDATE");
            }
        }

        public ReturnEntity<Wash_ClassDC> Wash_Class_SELECT_Entity(int iID)
        {
            Log_DeBug("Wash_Class_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Wash_Class_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Class_SELECT_Entity", ex);
                return new ReturnEntity<Wash_ClassDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Class_SELECT_Entity");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ClassDC>> Wash_Class_SELECT_List_ALL()
        {
            Log_DeBug("Wash_Class_SELECT_List_ALL");
            try
            {
                return ProductInstance.Wash_Class_SELECT_List_ALL();
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Class_SELECT_List_ALL", ex);
                return new ReturnEntity<IList<Wash_ClassDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Class_SELECT_List_ALL");
            }
        }

        public ReturnEntity<int> Wash_Attribute_ADD(Wash_AttributeDC iWash_AttributeDC)
        {
            Log_DeBug("Wash_Attribute_ADD", iWash_AttributeDC);
            try
            {
                return ProductInstance.Wash_Attribute_ADD(iWash_AttributeDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Attribute_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Attribute_ADD");
            }
        }

        public ReturnEntity<bool> Wash_Attribute_UPDATE(Wash_AttributeDC iWash_AttributeDC)
        {
            Log_DeBug("Wash_Attribute_UPDATE", iWash_AttributeDC);
            try
            {
                return ProductInstance.Wash_Attribute_UPDATE(iWash_AttributeDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Attribute_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Attribute_UPDATE");
            }
        }

        public ReturnEntity<Wash_AttributeDC> Wash_Attribute_SELECT_Entity(int iID)
        {
            Log_DeBug("Wash_Attribute_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Wash_Attribute_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Attribute_SELECT_Entity", ex);
                return new ReturnEntity<Wash_AttributeDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Attribute_SELECT_Entity");
            }
        }


        public ReturnEntity<int> Wash_Category_ADD(Wash_CategoryDC iWash_CategoryDC)
        {
            Log_DeBug("Wash_Category_ADD", iWash_CategoryDC);
            try
            {
                return ProductInstance.Wash_Category_ADD(iWash_CategoryDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Category_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Category_ADD");
            }
        }

        public ReturnEntity<bool> Wash_Category_UPDATE(Wash_CategoryDC iWash_CategoryDC)
        {
            Log_DeBug("Wash_Category_UPDATE", iWash_CategoryDC);
            try
            {
                return ProductInstance.Wash_Category_UPDATE(iWash_CategoryDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Category_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Category_UPDATE");
            }
        }

        public ReturnEntity<Wash_CategoryDC> Wash_Category_SELECT_Entity(int iID)
        {
            Log_DeBug("Wash_Category_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Wash_Category_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Category_SELECT_Entity", ex);
                return new ReturnEntity<Wash_CategoryDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Category_SELECT_Entity");
            }
        }

        public ReturnEntity<RecordCountEntity<Wash_CategoryDC>> Wash_Category_SELECT_List(string iName, string iCode,
            int? iEnable, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Wash_Category_SELECT_List", iName, iCode, iEnable, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return ProductInstance.Wash_Category_SELECT_List(iName, iCode, iEnable, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Category_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Wash_CategoryDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Category_SELECT_List");
            }
        }

        /// <summary>
        /// 产品查询列表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_CategoryDC>> Wash_Category_SELECT_List_ByClassID(int iClassID)
        {
            Log_DeBug("Wash_Category_SELECT_List_ByClassID", iClassID);
            try
            {
                return ProductInstance.Wash_Category_SELECT_List_ByClassID(iClassID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Category_SELECT_List_ByClassID", ex);
                return new ReturnEntity<IList<Wash_CategoryDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Category_SELECT_List_ByClassID");
            }
        }

        public ReturnEntity<int> Wash_Product_ADD(Wash_ProductDC iWash_ProductDC)
        {
            Log_DeBug("Wash_Product_ADD", iWash_ProductDC);
            try
            {
                return ProductInstance.Wash_Product_ADD(iWash_ProductDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Product_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Product_ADD");
            }
        }

        public ReturnEntity<bool> Wash_Product_UPDATE(Wash_ProductDC iWash_ProductDC)
        {
            Log_DeBug("Wash_Product_UPDATE", iWash_ProductDC);
            try
            {
                return ProductInstance.Wash_Product_UPDATE(iWash_ProductDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Product_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Product_UPDATE");
            }
        }

        public ReturnEntity<Wash_ProductDC> Wash_Product_SELECT_Entity(int iID)
        {
            Log_DeBug("Wash_Product_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Wash_Product_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Product_SELECT_Entity", ex);
                return new ReturnEntity<Wash_ProductDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Product_SELECT_Entity");
            }
        }

        public ReturnEntity<RecordCountEntity<Wash_ProductDC>> Wash_Product_SELECT_List(string iName, string iCode,
            int? iType, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Wash_Product_SELECT_List", iName, iCode, iType, iCommitStatus, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return ProductInstance.Wash_Product_SELECT_List(iName, iCode, iType, iCommitStatus, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Product_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Wash_ProductDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Product_SELECT_List");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ProductDC>> Wash_Product_SELECT_List_CategoryID(int iCategoryID)
        {
            Log_DeBug("Wash_Product_SELECT_List_CategoryID", iCategoryID);
            try
            {
                return ProductInstance.Wash_Product_SELECT_List_CategoryID(iCategoryID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Product_SELECT_List_CategoryID", ex);
                return new ReturnEntity<IList<Wash_ProductDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Product_SELECT_List_CategoryID");
            }
        }

        /// <summary>
        /// 产品价目属性查询
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ProductAttributeDC>> Wash_ProductAttribute_SELECT(int iProductID)
        {
            Log_DeBug("Wash_ProductAttribute_SELECT", iProductID);
            try
            {
                return ProductInstance.Wash_ProductAttribute_SELECT(iProductID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_ProductAttribute_SELECT", ex);
                return new ReturnEntity<IList<Wash_ProductAttributeDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_ProductAttribute_SELECT");
            }
        }

        #region 合作门店

        /// <summary>
        /// 新增门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_ADD(Wash_StoreDC iWash_StoreDC)
        {
            Log_DeBug("Wash_Store_ADD", iWash_StoreDC);
            try
            {
                return ProductInstance.Wash_Store_ADD(iWash_StoreDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_ADD", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_ADD");
            }
        }

        /// <summary>
        /// 更新门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_UPDATE(Wash_StoreDC iWash_StoreDC)
        {
            Log_DeBug("Wash_Store_UPDATE", iWash_StoreDC);
            try
            {
                return ProductInstance.Wash_Store_UPDATE(iWash_StoreDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_UPDATE");
            }
        }

        /// <summary>
        /// 查询门店
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Wash_StoreDC>> Wash_Store_SELECT_List(string iName, string iCode, int? iSite,
              DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Wash_Store_SELECT_List", iName, iCode, iSite,
                iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return ProductInstance.Wash_Store_SELECT_List(iName, iCode, iSite,
                    iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Wash_StoreDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_StoreDC> Wash_Store_SELECT_Entity(int iID)
        {
            Log_DeBug("Wash_Store_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Wash_Store_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_SELECT_Entity", ex);
                return new ReturnEntity<Wash_StoreDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_SELECT_Entity");
            }
        }

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_StoreDC> Wash_Store_SELECT_Entity(Guid iStoreID)
        {
            Log_DeBug("Wash_Store_SELECT_Entity", iStoreID);
            try
            {
                return ProductInstance.Wash_Store_SELECT_Entity(iStoreID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_SELECT_Entity", ex);
                return new ReturnEntity<Wash_StoreDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_SELECT_Entity");
            }
        }

        /// <summary>
        /// 新增门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_Operator_ADD(Wash_Store_OperatorDC iWash_Store_OperatorDC)
        {
            Log_DeBug("Wash_Store_Operator_ADD", iWash_Store_OperatorDC);
            try
            {
                return ProductInstance.Wash_Store_Operator_ADD(iWash_Store_OperatorDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_Operator_ADD", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_Operator_ADD");
            }
        }


        /// <summary>
        /// 更新门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_Operator_UPDATE(Wash_Store_OperatorDC iWash_Store_OperatorDC)
        {
            Log_DeBug("Wash_Store_Operator_UPDATE", iWash_Store_OperatorDC);
            try
            {
                return ProductInstance.Wash_Store_Operator_UPDATE(iWash_Store_OperatorDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_Operator_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_Operator_UPDATE");
            }
        }

        /// <summary>
        /// 删除门店操作员
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_Operator_DELETE(int iID, int iMuser)
        {
            Log_DeBug("Wash_Store_Operator_DELETE", iID, iMuser);
            try
            {
                return ProductInstance.Wash_Store_Operator_DELETE(iID, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_Operator_DELETE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_Operator_DELETE");
            }
        }

        /// <summary>
        /// 查询全部门店操作员
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_Store_OperatorDC>> Wash_Store_Operator_SELECT_ALL(int iStoreID)
        {
            Log_DeBug("Wash_Store_Operator_SELECT_ALL", iStoreID);
            try
            {
                return ProductInstance.Wash_Store_Operator_SELECT_ALL(iStoreID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_Operator_SELECT_ALL", ex);
                return new ReturnEntity<IList<Wash_Store_OperatorDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_Operator_SELECT_ALL");
            }
        }

        /// <summary>
        /// 根据ID查询门店操作员
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_Store_OperatorDC> Wash_Store_Operator_SELECT_Entity(int iID)
        {
            Log_DeBug("Wash_Store_Operator_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Wash_Store_Operator_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_Operator_SELECT_Entity", ex);
                return new ReturnEntity<Wash_Store_OperatorDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_Operator_SELECT_Entity");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStoreCode"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_StoreLoginInfoDC> Wash_Store_Login(string iStoreCode, string iLoginName, string iPassword)
        {
            Log_DeBug("Wash_Store_Login", iStoreCode, iLoginName, iPassword);
            try
            {
                return ProductInstance.Wash_Store_Login(iStoreCode, iLoginName, iPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_Login", ex);
                return new ReturnEntity<Wash_StoreLoginInfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_Login");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword, int iMuser)
        {
            Log_DeBug("Wash_Store_Operator_UPDATE_Password", iOperatorID, iOldPassword, iNewPassword, iMuser);
            try
            {
                return ProductInstance.Wash_Store_Operator_UPDATE_Password(iOperatorID, iOldPassword, iNewPassword, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_Operator_UPDATE_Password", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_UPDATE_Password");
            }
        }


        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_OperatorLog_ADD(int iType, int iOperatorID, string iOperatorName, string iLogContent)
        {
            Log_DeBug("Wash_Store_OperatorLog_ADD", iType, iOperatorID, iOperatorName, iLogContent);
            try
            {
                return ProductInstance.Wash_Store_OperatorLog_ADD(iType, iOperatorID, iOperatorName, iLogContent);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_OperatorLog_ADD", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_OperatorLog_ADD");
            }
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
        public ReturnEntity<RecordCountEntity<Wash_Store_OperatorLogDC>> Wash_Store_OperatorLog_SELECT_List(int? iType, string iOperatorName,
            string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Wash_Store_OperatorLogDC", iType, iOperatorName,
                    iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return ProductInstance.Wash_Store_OperatorLog_SELECT_List(iType, iOperatorName,
                    iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Store_OperatorLogDC", ex);
                return new ReturnEntity<RecordCountEntity<Wash_Store_OperatorLogDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Store_OperatorLogDC");
            }
        }

        #endregion

        #region 商场产品

        /// <summary>
        /// 新增商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Mall_Product_ADD(Mall_ProductDC iMall_ProductDC)
        {
            Log_DeBug("Mall_Product_ADD", iMall_ProductDC);
            try
            {
                return ProductInstance.Mall_Product_ADD(iMall_ProductDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Mall_Product_ADD", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Mall_Product_ADD");
            }
        }

        /// <summary>
        /// 更新商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Mall_Product_UPDATE(Mall_ProductDC iMall_ProductDC)
        {
            Log_DeBug("Mall_Product_UPDATE", iMall_ProductDC);
            try
            {
                return ProductInstance.Mall_Product_UPDATE(iMall_ProductDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Mall_Product_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Mall_Product_UPDATE");
            }
        }

        /// <summary>
        /// 查询商场产品
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Mall_ProductDC>> Mall_Product_SELECT_List(int? iType, int? iClass,
            string iName, int? iSite, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Mall_Product_SELECT_List", iType, iClass,
                    iName, iSite, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return ProductInstance.Mall_Product_SELECT_List(iType, iClass,
                    iName, iSite, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Mall_Product_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Mall_ProductDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Mall_Product_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Mall_ProductDC> Mall_Product_SELECT_Entity(int iID)
        {
            Log_DeBug("Mall_Product_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Mall_Product_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Mall_Product_SELECT_Entity", ex);
                return new ReturnEntity<Mall_ProductDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Mall_Product_SELECT_Entity");
            }
        }

        #endregion

        #region 活动

        /// <summary>
        /// 新增活动
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Wash_Activity_ADD(Wash_ActivityDC iWash_ActivityDC)
        {
            Log_DeBug("Wash_Activity_ADD", iWash_ActivityDC);
            try
            {
                return ProductInstance.Wash_Activity_ADD(iWash_ActivityDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Activity_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Activity_ADD");
            }
        }

        /// <summary>
        /// 活动更新
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Activity_UPDATE(Wash_ActivityDC iWash_ActivityDC)
        {
            Log_DeBug("Wash_Activity_UPDATE", iWash_ActivityDC);
            try
            {
                return ProductInstance.Wash_Activity_UPDATE(iWash_ActivityDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Activity_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Activity_UPDATE");
            }
        }

        /// <summary>
        /// 活动单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_ActivityDC> Wash_Activity_SELECT_Entity(int iID)
        {
            Log_DeBug("Wash_Activity_SELECT_Entity", iID);
            try
            {
                return ProductInstance.Wash_Activity_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Activity_SELECT_Entity", ex);
                return new ReturnEntity<Wash_ActivityDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Activity_SELECT_Entity");
            }
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
        public ReturnEntity<RecordCountEntity<Wash_ActivityDC>> Wash_Activity_SELECT_List(string iName, int? iCommitStatus,
            int? iSite, int? iChannel,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Wash_Activity_SELECT_List", iName, iCommitStatus,
                    iSite, iChannel,
                    iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return ProductInstance.Wash_Activity_SELECT_List(iName, iCommitStatus,
                    iSite, iChannel,
                    iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Wash_Activity_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Wash_ActivityDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Wash_Activity_SELECT_List");
            }
        }

        #endregion

    }
}
