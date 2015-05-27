using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Business.Business
{
    public class Product
    {
        private LazyAtHome.WCF.Wash.Interface.IDAL.IProductDAL productDAL;
        public Product()
        {
            if (productDAL == null)
                productDAL = new LazyAtHome.WCF.Wash.DAL.ProductDAL();
        }

        static Product _Product;

        public static Product Instance
        {
            get
            {
                if (_Product == null)
                {
                    _Product = new Product();
                }
                return _Product;
            }
        }

        #region 缓存
        public void SetCache()
        {
            Console.WriteLine("获取类别缓存..........");
            SetCacheWash_Class();
            Console.WriteLine("获取类别缓存完成......");
            //Console.WriteLine("获取属性缓存..........");
            //SetCacheWash_Attribute();
            //Console.WriteLine("获取属性缓存完成......");
        }

        /// <summary>
        /// 类别缓存
        /// </summary>
        private void SetCacheWash_Class()
        {
            var rtn = Wash_Class_SELECT_List_ALL();
            if (rtn.Success)
            {
                Console.WriteLine("\t获取类别缓存" + rtn.OBJ.Count + "条");
            }
            else
                Console.WriteLine("\t获取类别缓存0条");
        }

        /// <summary>
        /// 属性缓存
        /// </summary>
        private void SetCacheWash_Attribute()
        {
            //IList<Wash_AttributeDC> list = productDAL.Wash_Attribute_SELECT_List_ALL();
            //if (list != null)
            //{
            //    LazyAtHome.Core.Helper.CacheHelper.Put("Wash_Attribute_List", list, CacheTimer.GetTimeSpanOneDay);
            //    Console.WriteLine("\t获取属性缓存" + list.Count.ToString() + "条");
            //}
            //else
            //    Console.WriteLine("\t获取属性缓存0条");
        }

        #endregion

        /// <summary>
        /// 新增类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Wash_Class_ADD(Wash_ClassDC iWash_ClassDC)
        {
            if (iWash_ClassDC == null)
                return new ReturnEntity<int>(-1, "对象错误");

            iWash_ClassDC.Code = productDAL.Wash_Class_ADD_CreateCode(iWash_ClassDC.ParentID);

            var rtn = productDAL.Wash_Class_ADD(iWash_ClassDC);

            SetCacheWash_Class();

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 更新类别
        /// </summary>
        /// <param name="iWash_ClassDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Class_UPDATE(Wash_ClassDC iWash_ClassDC)
        {
            var rtn = productDAL.Wash_Class_UPDATE(iWash_ClassDC);
            SetCacheWash_Class();
            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 获取类别
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_ClassDC> Wash_Class_SELECT_Entity(int iID)
        {
            return new ReturnEntity<Wash_ClassDC>(productDAL.Wash_Class_SELECT_Entity(iID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ClassDC>> Wash_Class_SELECT_List_ALL()
        {
            var list = productDAL.Wash_Class_SELECT_List_ALL();
            if (list != null && list.Count > 0)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("Wash_Class_List", list, CacheTimer.GetTimeSpanOneDay);
                return new ReturnEntity<IList<Wash_ClassDC>>(list);
            }
            else
            {
                return new ReturnEntity<IList<Wash_ClassDC>>(-1, "类别读取失败");
            }
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Wash_Attribute_ADD(Wash_AttributeDC iWash_AttributeDC)
        {
            if (iWash_AttributeDC == null)
                return new ReturnEntity<int>(-1, "对象错误");

            iWash_AttributeDC.Code = productDAL.Wash_Attribute_ADD_CreateCode(iWash_AttributeDC.ParentID);

            var rtn = productDAL.Wash_Attribute_ADD(iWash_AttributeDC);

            SetCacheWash_Attribute();

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="iWash_AttributeDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Attribute_UPDATE(Wash_AttributeDC iWash_AttributeDC)
        {
            var rtn = productDAL.Wash_Attribute_UPDATE(iWash_AttributeDC);
            SetCacheWash_Attribute();
            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_AttributeDC> Wash_Attribute_SELECT_Entity(int iID)
        {
            return new ReturnEntity<Wash_AttributeDC>(productDAL.Wash_Attribute_SELECT_Entity(iID));
        }

        /// <summary>
        /// 产品添加
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Wash_Category_ADD(Wash_CategoryDC iWash_CategoryDC)
        {
            return new ReturnEntity<int>(productDAL.Wash_Category_ADD(iWash_CategoryDC));
        }

        /// <summary>
        /// 产品修改
        /// </summary>
        /// <param name="iWash_CategoryDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Category_UPDATE(Wash_CategoryDC iWash_CategoryDC)
        {
            return new ReturnEntity<bool>(productDAL.Wash_Category_UPDATE(iWash_CategoryDC));
        }

        /// <summary>
        /// 产品查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_CategoryDC> Wash_Category_SELECT_Entity(int iID)
        {
            var entity = productDAL.Wash_Category_SELECT_Entity(iID);

            if (entity != null)
            {
                var classList = ProductClient.Wash_Class_SELECT_List_ALL();
                if (classList != null)
                {

                    var classSecond = classList.FirstOrDefault(p => p.ID == entity.ClassID);
                    if (classSecond != null)
                    {
                        entity.ClassName = classSecond.Name;
                        var classFirst = classList.FirstOrDefault(p => p.ID == classSecond.ParentID);
                        if (classFirst != null)
                        {
                            entity.ParentClassID = classFirst.ID;
                            entity.ParentClassName = classFirst.Name;
                        }
                    }
                }
            }

            return new ReturnEntity<Wash_CategoryDC>(entity);
        }

        /// <summary>
        /// 产品查询列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iEnable"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Wash_CategoryDC>> Wash_Category_SELECT_List(string iName, string iCode,
            int? iEnable, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var list = productDAL.Wash_Category_SELECT_List(
                iName, iCode, iEnable, iStartDate, iEndDate, iPageIndex, iPageSize);

            if (list != null && list.ReturnList != null)
            {
                var classList = ProductClient.Wash_Class_SELECT_List_ALL();
                if (classList != null)
                {
                    foreach (var item in list.ReturnList)
                    {
                        var classSecond = classList.FirstOrDefault(p => p.ID == item.ClassID);
                        if (classSecond == null)
                        {
                            continue;
                        }
                        item.ClassName = classSecond.Name;
                        var classFirst = classList.FirstOrDefault(p => p.ID == classSecond.ParentID);
                        if (classFirst == null)
                        {
                            continue;
                        }
                        item.ParentClassID = classFirst.ID;
                        item.ParentClassName = classFirst.Name;
                    }
                }
            }

            return new ReturnEntity<RecordCountEntity<Wash_CategoryDC>>(list);
        }

        /// <summary>
        /// 产品查询列表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_CategoryDC>> Wash_Category_SELECT_List_ByClassID(int iClassID)
        {
            return new ReturnEntity<IList<Wash_CategoryDC>>(productDAL.Wash_Category_SELECT_List_ByClassID(iClassID));
        }

        /// <summary>
        /// 运价添加
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Wash_Product_ADD(Wash_ProductDC iWash_ProductDC)
        {
            iWash_ProductDC.VerifyStatus = 2;
            iWash_ProductDC.SubmitOperatorID = iWash_ProductDC.Obj_Cuser;
            iWash_ProductDC.SubmitDate = DateTime.Now;
            iWash_ProductDC.AduitOperatorID = iWash_ProductDC.Obj_Cuser;
            iWash_ProductDC.AduitDate = DateTime.Now;

            return new ReturnEntity<int>(productDAL.Wash_Product_ADD(iWash_ProductDC));
        }

        /// <summary>
        /// 运价修改
        /// </summary>
        /// <param name="iWash_ProductDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Product_UPDATE(Wash_ProductDC iWash_ProductDC)
        {
            return new ReturnEntity<bool>(productDAL.Wash_Product_UPDATE(iWash_ProductDC));
        }

        /// <summary>
        /// 运价查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_ProductDC> Wash_Product_SELECT_Entity(int iID)
        {
            var entity = productDAL.Wash_Product_SELECT_Entity(iID);

            if (entity != null)
            {
                var classList = ProductClient.Wash_Class_SELECT_List_ALL();
                if (classList != null)
                {

                    var classSecond = classList.FirstOrDefault(p => p.ID == entity.ClassID);
                    if (classSecond != null)
                    {
                        entity.ClassName = classSecond.Name;
                        var classFirst = classList.FirstOrDefault(p => p.ID == classSecond.ParentID);
                        if (classFirst != null)
                        {
                            entity.ParentClassID = classFirst.ID;
                            entity.ParentClassName = classFirst.Name;
                        }
                    }
                }
            }


            return new ReturnEntity<Wash_ProductDC>(entity);
        }

        /// <summary>
        /// 运价查询列表
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
        public ReturnEntity<RecordCountEntity<Wash_ProductDC>> Wash_Product_SELECT_List(string iName, string iCode,
            int? iType, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var list = productDAL.Wash_Product_SELECT_List(
                   iName, iCode, iType, iCommitStatus, iStartDate, iEndDate, iPageIndex, iPageSize);


            if (list != null && list.ReturnList != null)
            {
                var classList = ProductClient.Wash_Class_SELECT_List_ALL();
                if (classList != null)
                {
                    foreach (var item in list.ReturnList)
                    {
                        var classSecond = classList.FirstOrDefault(p => p.ID == item.ClassID);
                        if (classSecond == null)
                        {
                            continue;
                        }
                        item.ClassName = classSecond.Name;
                        var classFirst = classList.FirstOrDefault(p => p.ID == classSecond.ParentID);
                        if (classFirst == null)
                        {
                            continue;
                        }
                        item.ParentClassID = classFirst.ID;
                        item.ParentClassName = classFirst.Name;
                    }
                }
            }

            return new ReturnEntity<RecordCountEntity<Wash_ProductDC>>(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ProductDC>> Wash_Product_SELECT_List_CategoryID(int iCategoryID)
        {
            var list = productDAL.Wash_Product_SELECT_List_CategoryID(iCategoryID);
            return new ReturnEntity<IList<Wash_ProductDC>>(list);
        }

        /// <summary>
        /// 产品价目属性查询
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ProductAttributeDC>> Wash_ProductAttribute_SELECT(int iProductID)
        {
            return new ReturnEntity<IList<Wash_ProductAttributeDC>>(productDAL.Wash_ProductAttribute_SELECT(iProductID));

        }

        #region 合作门店

        /// <summary>
        /// 新增门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_ADD(Wash_StoreDC iWash_StoreDC)
        {
            if (iWash_StoreDC == null)
            {
                return new ReturnEntity<bool>(-1, "门店对象为空");
            }

            iWash_StoreDC.Code = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

            while (productDAL.Wash_Store_CodeExists(iWash_StoreDC.Code))
            {
                iWash_StoreDC.Code = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            }

            var storeID = productDAL.Wash_Store_ADD(iWash_StoreDC);

            Wash_Store_Operator_ADD(new Wash_Store_OperatorDC()
            {
                StoreID = storeID,
                StoreCode = iWash_StoreDC.Code,
                LoginName = "Admin",
                LoginPwd = "123456",
                Enable = 1,
                MPNo = string.Empty,
                Name = "超级管理员",
                IsAdmin = 1,
            });

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 更新门店
        /// </summary>
        /// <param name="iWash_StoreDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_UPDATE(Wash_StoreDC iWash_StoreDC)
        {
            return new ReturnEntity<bool>(productDAL.Wash_Store_UPDATE(iWash_StoreDC));
        }

        /// <summary>
        /// 查询门店
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Wash_StoreDC>> Wash_Store_SELECT_List(string iName, string iCode, int? iSite,
              DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<Wash_StoreDC>>(productDAL.Wash_Store_SELECT_List(iName, iCode, iSite,
               iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_StoreDC> Wash_Store_SELECT_Entity(int iID)
        {
            return new ReturnEntity<Wash_StoreDC>(productDAL.Wash_Store_SELECT_Entity(iID));
        }

        /// <summary>
        /// 根据ID查询门店
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_StoreDC> Wash_Store_SELECT_Entity(Guid iStoreID)
        {
            return new ReturnEntity<Wash_StoreDC>(productDAL.Wash_Store_SELECT_Entity(iStoreID));
        }


        /// <summary>
        /// 新增门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_Operator_ADD(Wash_Store_OperatorDC iWash_Store_OperatorDC)
        {
            if (iWash_Store_OperatorDC == null)
            {
                return new ReturnEntity<bool>(-1, "登录对象空");
            }

            iWash_Store_OperatorDC.LoginPwd = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iWash_Store_OperatorDC.LoginPwd, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            return new ReturnEntity<bool>(productDAL.Wash_Store_Operator_ADD(iWash_Store_OperatorDC) > 0 ? true : false);
        }


        /// <summary>
        /// 更新门店操作员
        /// </summary>
        /// <param name="iWash_Store_OperatorDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_Operator_UPDATE(Wash_Store_OperatorDC iWash_Store_OperatorDC)
        {
            return new ReturnEntity<bool>(productDAL.Wash_Store_Operator_UPDATE(iWash_Store_OperatorDC));
        }

        /// <summary>
        /// 删除门店操作员
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Store_Operator_DELETE(int iID, int iMuser)
        {
            return new ReturnEntity<bool>(productDAL.Wash_Store_Operator_DELETE(iID, iMuser));
        }

        /// <summary>
        /// 查询全部门店操作员
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_Store_OperatorDC>> Wash_Store_Operator_SELECT_ALL(int iStoreID)
        {
            return new ReturnEntity<IList<Wash_Store_OperatorDC>>(productDAL.Wash_Store_Operator_SELECT_ALL(iStoreID));
        }

        /// <summary>
        /// 根据ID查询门店操作员
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_Store_OperatorDC> Wash_Store_Operator_SELECT_Entity(int iID)
        {
            return new ReturnEntity<Wash_Store_OperatorDC>(productDAL.Wash_Store_Operator_SELECT_Entity(iID));
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
            //把明码进行MD5加密
            iPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            //获得员工信息
            Wash_StoreLoginInfoDC entity = productDAL.Wash_Store_Operator_SELECT_Entity(iStoreCode, iLoginName, iPassword);
            if (entity == null)
            {
                return new ReturnEntity<Wash_StoreLoginInfoDC>(-1, "用户名或密码错误，请重新输入！");
            }
            return new ReturnEntity<Wash_StoreLoginInfoDC>(entity);
        }

        public ReturnEntity<bool> Wash_Store_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword, int iMuser)
        {
            iOldPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iOldPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);
            iNewPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iNewPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5);

            var rtn = productDAL.Wash_Store_Operator_UPDATE_Password(iOperatorID, iOldPassword, iNewPassword, iMuser);
            if (rtn)
            {
                return new ReturnEntity<bool>(true);
            }
            else
            {
                return new ReturnEntity<bool>(-1, "原密码错误");
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
            var entity = new Wash_Store_OperatorLogDC()
            {
                Type = iType,
                OperatorID = iOperatorID,
                OperatorName = iOperatorName,
                LogContent = iLogContent,
            };
            var rtn = productDAL.Wash_Store_OperatorLog_ADD(entity);
            if (rtn > 0)
            {
                return new ReturnEntity<bool>(true);
            }
            else
            {
                return new ReturnEntity<bool>(-1, "添加日志失败");
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
            return new ReturnEntity<RecordCountEntity<Wash_Store_OperatorLogDC>>(productDAL.Wash_Store_OperatorLog_SELECT_List(
              iType, iOperatorName, iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize));
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
            productDAL.Mall_Product_ADD(iMall_ProductDC);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 更新商场产品
        /// </summary>
        /// <param name="iMall_ProductDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Mall_Product_UPDATE(Mall_ProductDC iMall_ProductDC)
        {
            return new ReturnEntity<bool>(productDAL.Mall_Product_UPDATE(iMall_ProductDC));
        }

        ///// <summary>
        ///// 删除商场产品
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <param name="iMuser">操作人</param>
        ///// <returns></returns>
        //public ReturnEntity< bool> Mall_Product_DELETE(int iID, int iMuser)
        //{

        //}

        /// <summary>
        /// 查询商场产品
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Mall_ProductDC>> Mall_Product_SELECT_List(int? iType, int? iClass,
            string iName, int? iSite, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<Mall_ProductDC>>(productDAL.Mall_Product_SELECT_List(iType, iClass,
                    iName, iSite, iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Mall_ProductDC> Mall_Product_SELECT_Entity(int iID)
        {
            return new ReturnEntity<Mall_ProductDC>(productDAL.Mall_Product_SELECT_Entity(iID));
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
            var rtn = productDAL.Wash_Activity_ADD(iWash_ActivityDC);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 活动更新
        /// </summary>
        /// <param name="iWash_ActivityDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Wash_Activity_UPDATE(Wash_ActivityDC iWash_ActivityDC)
        {
            var rtn = productDAL.Wash_Activity_UPDATE(iWash_ActivityDC);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 活动单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Wash_ActivityDC> Wash_Activity_SELECT_Entity(int iID)
        {
            var rtn = productDAL.Wash_Activity_SELECT_Entity(iID);

            return new ReturnEntity<Wash_ActivityDC>(rtn);
        }

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
        public ReturnEntity<RecordCountEntity<Wash_ActivityDC>> Wash_Activity_SELECT_List(string iName, int? iCommitStatus,
            int? iSite, int? iChannel,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = productDAL.Wash_Activity_SELECT_List(iName, iCommitStatus,
                    iSite, iChannel, iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Wash_ActivityDC>>(rtn);
        }

        #endregion
    }
}
