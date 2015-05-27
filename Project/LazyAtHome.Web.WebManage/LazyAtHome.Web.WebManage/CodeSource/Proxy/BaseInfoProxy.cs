using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.ClientProxy;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 基础信息代理类
    /// </summary>
    public class BaseInfoProxy
    {
        #region 城市站点

        /// <summary>
        /// 获取站点
        /// </summary>
        /// <returns></returns>
        public static IList<Base_SiteDC> Base_GetAllSite()
        {
            IList<Base_SiteDC> list = null;
            try
            {
                list = BaseClient.Cache_Base_SiteDC_GetList();

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|BaseInfo Base_GetAllSite|" + ex.Message);
                return null;
            }
            return list;
        }

        #endregion


        #region 行政区
        /// <summary>
        /// 获取所有行政区列表
        /// </summary>
        /// <returns></returns>
        public static IList<Base_DistrictDC> Base_GetAllDistrict()
        {     
            IList<Base_DistrictDC> list = null;

            //先判断缓存中是否存在获取
            if (CodeSource.SiteSession.AllDistrictList == null)
            {
                try
                {
                    list = BaseClient.Cache_Base_District_GetList();

                }
                catch (Exception ex)
                {
                    WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|BaseInfo Base_GetAllDistrict|" + ex.Message);
                    return null;
                }

                //获取后放入缓存中
                CodeSource.SiteSession.AllDistrictList = list;
            }
            else
            {
                list = CodeSource.SiteSession.AllDistrictList;
            }

            
            return list;
        }

        /// <summary>
        /// 根据Level1的Code获取Level2数据
        /// </summary>
        /// <param name="code1"></param>
        /// <returns></returns>
        public static IList<Base_DistrictDC> Base_GetDistrict_L2(string code1)
        {
            IList<Base_DistrictDC> list = Base_GetAllDistrict();
            return list.Where(p => p.Code1 == code1 && p.Level == 2).ToList();
        }


        /// <summary>
        /// 根据Level2的Code获取Level3数据
        /// </summary>
        /// <param name="code1"></param>
        /// <returns></returns>
        public static IList<Base_DistrictDC> Base_GetDistrict_L3(string code1, string code2)
        {
            IList<Base_DistrictDC> list = Base_GetAllDistrict();
            return list.Where(p => p.Code1 == code1 && p.Code2 == code2 && p.Level == 3).ToList();
        }

        #endregion
    }
}