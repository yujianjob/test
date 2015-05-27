using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public partial class Base
    {
        private LazyAtHome.WCF.Common.Interface.IDAL.IBaseDAL baseDAL;
        private LazyAtHome.WCF.Common.Interface.IDAL.ITinyurlDAL tinyurlDAL;

        public Base()
        {
            if (baseDAL == null)
                baseDAL = new BaseDAL();

            if (tinyurlDAL == null)
                tinyurlDAL = new TinyurlDAL();
        }

        static Base _Base;

        public static Base Instance
        {
            get
            {
                if (_Base == null)
                {
                    _Base = new Base();
                }
                return _Base;
            }
        }


        public void SetCache()
        {
            Console.WriteLine("获取行政区缓存..........");
            SetCacheBase_District();
            Console.WriteLine("获取行政区缓存完成......");
            Console.WriteLine("获取站点缓存..........");
            SetCacheBase_Site();
            Console.WriteLine("获取站点缓存完成......");
        }

        /// <summary>
        /// 行政区
        /// </summary>
        private void SetCacheBase_District()
        {
            IList<Base_DistrictDC> dis = baseDAL.Base_District_SELECT_List_ALL();
            if (dis != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("Base_District_List", dis, CacheTimer.GetTimeSpanOneDay);
                Console.WriteLine("\t获取行政区缓存" + dis.Count.ToString() + "条");
            }
            else
                Console.WriteLine("\t获取行政区缓存0条");
        }

        /// <summary>
        /// 站点
        /// </summary>
        private void SetCacheBase_Site()
        {
            IList<Base_SiteDC> bs = baseDAL.Base_Site_SELECT_List_ALL();
            if (bs != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("Base_SiteDC_List", bs, CacheTimer.GetTimeSpanOneDay);
                Console.WriteLine("\t获取站点缓存" + bs.Count.ToString() + "条");
            }
            else
                Console.WriteLine("\t获取站点缓存0条");
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_LogDC>> Base_Log_SELECT_List(string iAppDomainName, string iTitle,
            int? iEventType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<RecordCountEntity<Base_LogDC>>(baseDAL.Base_Log_SELECT_List(iAppDomainName, iTitle,
             iEventType, iStartDate, iEndDate, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 查询优惠券列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iUseClass"></param>
        /// <param name="iUseType"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_CouponDC>> Base_Coupon_SELECT_List(string iName, int? iUseClass,
           int? iUseType, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = baseDAL.Base_Coupon_SELECT_List(iName, iUseClass, iUseType, iCommitStatus,
                iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Base_CouponDC>>(rtn);
        }

        #region 网站SEO

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Base_WebAttribute_ADD(Base_WebAttributeDC iBase_WebAttributeDC)
        {
            if (string.IsNullOrWhiteSpace(iBase_WebAttributeDC.Page))
            {
                return new ReturnEntity<int>(-1, "链接不能为空");
            }
            var rtn = baseDAL.Base_WebAttribute_ADD(iBase_WebAttributeDC);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_WebAttribute_UPDATE(Base_WebAttributeDC iBase_WebAttributeDC)
        {
            if (string.IsNullOrWhiteSpace(iBase_WebAttributeDC.Page))
            {
                return new ReturnEntity<bool>(-1, "链接不能为空");
            }
            var rtn = baseDAL.Base_WebAttribute_UPDATE(iBase_WebAttributeDC);

            if (rtn)
            {
                LazyAtHome.Core.Helper.CacheHelper.Remove("WEB_Base_WebAttribute_" + iBase_WebAttributeDC.Page);
            }

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_WebAttribute_DELETE(int iID)
        {
            var rtn = baseDAL.Base_WebAttribute_DELETE(iID);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_WebAttributeDC>> Base_WebAttribute_SELECT_List(string iName,
            string iPage, int iPageIndex, int iPageSize)
        {
            var rtn = baseDAL.Base_WebAttribute_SELECT_List(iName, iPage, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Base_WebAttributeDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Base_WebAttributeDC> Base_WebAttribute_SELECT_Entity(int iID)
        {
            var rtn = baseDAL.Base_WebAttribute_SELECT_Entity(iID);

            return new ReturnEntity<Base_WebAttributeDC>(rtn);
        }

        /// <summary>
        /// 根据Page查询
        /// </summary>
        /// <param name="iPage"></param>
        /// <returns></returns>
        public ReturnEntity<Base_WebAttributeDC> web_Base_WebAttribute_SELECT_Entity(string iPage)
        {
            var rtn = baseDAL.web_Base_WebAttribute_SELECT_Entity(iPage);

            if (rtn != null)
            {
                LazyAtHome.Core.Helper.CacheHelper.Put("WEB_Base_WebAttribute_" + iPage, rtn, CacheTimer.GetTimeSpanOneHour);
                return new ReturnEntity<Base_WebAttributeDC>(rtn);
            }
            else
            {
                return new ReturnEntity<Base_WebAttributeDC>(-1, "配置不存在");
            }

            return new ReturnEntity<Base_WebAttributeDC>(rtn);
        }

        #endregion

        /// <summary>
        /// 查询Base_Notify
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_NotifyDC>> Base_Notify_SELECT_List(string iEventNumber,
            string iOrderNumber, int? iRoleID, int? iPersonnelID, string iTitle, int? iLevel, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            var rtn = baseDAL.Base_Notify_SELECT_List(iEventNumber, iOrderNumber, iRoleID, iPersonnelID,
                iTitle, iLevel, iStatus, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Base_NotifyDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询Base_Notify
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Base_NotifyDC> Base_Notify_SELECT_Entity(int iID)
        {
            var rtn = baseDAL.Base_Notify_SELECT_Entity(iID);

            return new ReturnEntity<Base_NotifyDC>(rtn);
        }


        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_Notify_UPDATE_Get(int iNotifyID, int iMuser)
        {
            var rtn = baseDAL.Base_Notify_UPDATE_Get(iNotifyID, iMuser);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 更新备注
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_Notify_UPDATE_Result(int iNotifyID, string iResult, int iMuser)
        {
            var rtn = baseDAL.Base_Notify_UPDATE_Result(iNotifyID, iResult, iMuser);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 完成处理
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iStatus"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_Notify_UPDATE_Finish(int iNotifyID, string iResult, int iStatus, int iMuser)
        {
            var rtn = baseDAL.Base_Notify_UPDATE_Finish(iNotifyID, iResult, iStatus, iMuser);

            return new ReturnEntity<bool>(rtn);
        }

    }
}
