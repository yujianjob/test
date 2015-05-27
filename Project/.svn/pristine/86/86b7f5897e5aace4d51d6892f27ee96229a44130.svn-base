using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 产品wcf代理类
    /// </summary>
    public class CategoryProxy
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iCode"></param>
        /// <param name="iEnableType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Wash_CategoryDC> GetCategoryList(string iName, string iCode, int? iEnableType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Wash_CategoryDC> rtn = null;
            ReturnEntity<RecordCountEntity<Wash_CategoryDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<RecordCountEntity<Wash_CategoryDC>>>
                    (client => client.Proxy.Wash_Category_SELECT_List(iName, iCode, iEnableType, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy GetCategoryList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 根据类别ID获取产品
        /// </summary>
        /// <param name="iClassId"></param>
        /// <returns></returns>
        public static IList<Wash_CategoryDC> GetCategoryListBYClassID(int iClassId)
        {
            if (iClassId <= 0)
            {
                return new List<Wash_CategoryDC>();
            }

            ReturnEntity<IList<Wash_CategoryDC>> re = null;
            IList<Wash_CategoryDC> list = null;
            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<IList<Wash_CategoryDC>>>
                    (client => client.Proxy.Wash_Category_SELECT_List_ByClassID(iClassId));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy GetCategoryListBYClassID|" + ex.Message);
                return null;
            }
            if (re.Success)
            {
                list = re.OBJ;
            }

            return list;
        }


        /// <summary>
        /// 新增产品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddCategory(Wash_CategoryDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Wash_Category_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy AddCategory|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Wash_CategoryDC> GetCategoryBYID(int id)
        {
            ReturnEntity<Wash_CategoryDC> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<Wash_CategoryDC>>
                   (client => client.Proxy.Wash_Category_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy GetCategoryBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新产品信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateCategory(Wash_CategoryDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Wash_Category_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy UpdateCategory|" + ex.Message);
            }
            return re;
        }




        public static ReturnEntity<bool> EditCategorySort(int classid, string sort)
        {
            return new ReturnEntity<bool>(true);
            //ReturnEntity<bool> re = null;

            //try
            //{
            //    re = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<bool>>
            //       (client => client.Proxy.(classid, sort));
            //}
            //catch (System.ServiceModel.FaultException ex)
            //{
            //    //return ex.Message;
            //    WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CategoryProxy EditCategorySort|" + ex.Message);
            //}
            //return re;
        }
    }
}