using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Portal
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommonPortal : IBase
    {
        protected LazyAtHome.WCF.Common.Business.Business.Base BaseInstance = LazyAtHome.WCF.Common.Business.Business.Base.Instance;

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_LogDC>> Base_Log_SELECT_List(string iAppDomainName, string iTitle,
            int? iEventType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Base_Log_SELECT_List", iAppDomainName, iTitle, iEventType, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return BaseInstance.Base_Log_SELECT_List(iAppDomainName, iTitle, iEventType, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Log_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Base_LogDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Log_SELECT_List");
            }
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
            Log_DeBug("Base_Coupon_SELECT_List", iName, iUseClass, iUseType, iCommitStatus,
                iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return BaseInstance.Base_Coupon_SELECT_List(iName, iUseClass, iUseType, iCommitStatus,
                iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Coupon_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Base_CouponDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Coupon_SELECT_List");
            }
        }
        #region 网站SEO

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Base_WebAttribute_ADD(Base_WebAttributeDC iBase_WebAttributeDC)
        {
            Log_DeBug("Base_WebAttribute_ADD", iBase_WebAttributeDC);
            try
            {
                return BaseInstance.Base_WebAttribute_ADD(iBase_WebAttributeDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_WebAttribute_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_WebAttribute_ADD");
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_WebAttribute_UPDATE(Base_WebAttributeDC iBase_WebAttributeDC)
        {
            Log_DeBug("Base_WebAttribute_UPDATE", iBase_WebAttributeDC);
            try
            {
                return BaseInstance.Base_WebAttribute_UPDATE(iBase_WebAttributeDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_WebAttribute_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_WebAttribute_UPDATE");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_WebAttribute_DELETE(int iID)
        {
            Log_DeBug("Base_WebAttribute_DELETE", iID);
            try
            {
                return BaseInstance.Base_WebAttribute_DELETE(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_WebAttribute_DELETE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_WebAttribute_DELETE");
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_WebAttributeDC>> Base_WebAttribute_SELECT_List(string iName,
            string iPage, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Base_WebAttribute_SELECT_List", iName, iPage, iPageIndex, iPageSize);
            try
            {
                return BaseInstance.Base_WebAttribute_SELECT_List(iName, iPage, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_WebAttribute_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Base_WebAttributeDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_WebAttribute_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Base_WebAttributeDC> Base_WebAttribute_SELECT_Entity(int iID)
        {
            Log_DeBug("Base_WebAttribute_SELECT_Entity", iID);
            try
            {
                return BaseInstance.Base_WebAttribute_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_WebAttribute_SELECT_Entity", ex);
                return new ReturnEntity<Base_WebAttributeDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_WebAttribute_SELECT_Entity");
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Base_WebAttributeDC> web_Base_WebAttribute_SELECT_Entity(string iPage)
        {
            Log_DeBug("web_Base_WebAttribute_SELECT_Entity", iPage);
            try
            {
                return BaseInstance.web_Base_WebAttribute_SELECT_Entity(iPage);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_Base_WebAttribute_SELECT_Entity", ex);
                return new ReturnEntity<Base_WebAttributeDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_Base_WebAttribute_SELECT_Entity");
            }
        }
        #endregion

        #region 问卷

        /// <summary>
        /// 新增问卷
        /// </summary>
        /// <param name="iSurvey_MainDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Survey_Main_ADD(Survey_MainDC iSurvey_MainDC)
        {
            Log_DeBug("Survey_Main_ADD", iSurvey_MainDC);
            try
            {
                return BaseInstance.Survey_Main_ADD(iSurvey_MainDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Main_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Main_ADD");
            }
        }

        /// <summary>
        /// 修改问卷
        /// </summary>
        /// <param name="iSurvey_MainDC">主键</param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Main_UPDATE(Survey_MainDC iSurvey_MainDC)
        {
            Log_DeBug("Survey_Main_UPDATE", iSurvey_MainDC);
            try
            {
                return BaseInstance.Survey_Main_UPDATE(iSurvey_MainDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Main_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Main_UPDATE");
            }
        }

        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Main_DELETE(int iID, int iMuser)
        {
            Log_DeBug("Survey_Main_DELETE", iID, iMuser);
            try
            {
                return BaseInstance.Survey_Main_DELETE(iID, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Main_DELETE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Main_DELETE");
            }
        }

        /// <summary>
        /// 查询全部问卷
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Survey_MainDC>> Survey_Main_SELECT_List(
            string iTitle, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Survey_Main_SELECT_List", iTitle, iPageIndex, iPageSize);
            try
            {
                return BaseInstance.Survey_Main_SELECT_List(iTitle, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Main_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Survey_MainDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Main_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Survey_MainDC> Survey_Main_SELECT_Entity(int iID)
        {
            Log_DeBug("Survey_Main_SELECT_Entity", iID);
            try
            {
                return BaseInstance.Survey_Main_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Main_SELECT_Entity", ex);
                return new ReturnEntity<Survey_MainDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Main_SELECT_Entity");
            }
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Survey_Question_ADD(Survey_QuestionDC iSurvey_QuestionDC)
        {
            Log_DeBug("Survey_Question_ADD", iSurvey_QuestionDC);
            try
            {
                return BaseInstance.Survey_Question_ADD(iSurvey_QuestionDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Question_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Question_ADD");
            }
        }

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Question_UPDATE(Survey_QuestionDC iSurvey_QuestionDC)
        {
            Log_DeBug("Survey_Question_UPDATE", iSurvey_QuestionDC);
            try
            {
                return BaseInstance.Survey_Question_UPDATE(iSurvey_QuestionDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Question_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Question_UPDATE");
            }
        }

        /// <summary>
        /// 查询全部问题
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Survey_QuestionDC>> Survey_Question_SELECT_List(int iSurID)
        {
            Log_DeBug("Survey_Question_SELECT_List", iSurID);
            try
            {
                return BaseInstance.Survey_Question_SELECT_List(iSurID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Question_SELECT_List", ex);
                return new ReturnEntity<IList<Survey_QuestionDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Question_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询问题
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Survey_QuestionDC> Survey_Question_SELECT_Entity(int iID)
        {
            Log_DeBug("Survey_Question_SELECT_Entity", iID);
            try
            {
                return BaseInstance.Survey_Question_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Question_SELECT_Entity", ex);
                return new ReturnEntity<Survey_QuestionDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Question_SELECT_Entity");
            }
        }

        /// <summary>
        /// 新增回答
        /// </summary>
        /// <param name="iSurvey_AnswerDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Survey_Answer_ADD(Survey_AnswerDC iSurvey_AnswerDC)
        {
            Log_DeBug("Survey_Answer_ADD", iSurvey_AnswerDC);
            try
            {
                return BaseInstance.Survey_Answer_ADD(iSurvey_AnswerDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Answer_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Answer_ADD");
            }
        }

        /// <summary>
        /// 查询全部回答
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Survey_AnswerDC>> Survey_Answer_SELECT_List(int? iSurID,
            string iUserSource, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Survey_Answer_SELECT_List", iSurID, iUserSource, iPageIndex, iPageSize);
            try
            {
                return BaseInstance.Survey_Answer_SELECT_List(iSurID, iUserSource, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Answer_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Survey_AnswerDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Answer_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询回答
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Survey_AnswerDC> Survey_Answer_SELECT_Entity(int iID)
        {
            Log_DeBug("Survey_Answer_SELECT_Entity", iID);
            try
            {
                return BaseInstance.Survey_Answer_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Answer_SELECT_Entity", ex);
                return new ReturnEntity<Survey_AnswerDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Answer_SELECT_Entity");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSurveyID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Stat(int iSurveyID)
        {
            Log_DeBug("Survey_Stat", iSurveyID);
            try
            {
                return BaseInstance.Survey_Stat(iSurveyID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Survey_Stat", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Survey_Stat");
            }
        }

        #endregion

        public ReturnEntity<string> Tinyurl_CreateNew(string iUrl)
        {
            Log_DeBug("Tinyurl_CreateNew", iUrl);
            try
            {
                return BaseInstance.Tinyurl_CreateNew(iUrl);
            }
            catch (Exception ex)
            {
                Log_Fatal("Tinyurl_CreateNew", ex);
                return new ReturnEntity<string>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Tinyurl_CreateNew");
            }
        }

        public ReturnEntity<string> Tinyurl_Get(string iCode)
        {
            Log_DeBug("Tinyurl_Get", iCode);
            try
            {
                return BaseInstance.Tinyurl_Get(iCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("Tinyurl_Get", ex);
                return new ReturnEntity<string>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Tinyurl_Get");
            }
        }

        /// <summary>
        /// 查询Base_Notify
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_NotifyDC>> Base_Notify_SELECT_List(string iEventNumber,
            string iOrderNumber, int? iRoleID, int? iPersonnelID, string iTitle, int? iLevel, int? iStatus,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("Base_Notify_SELECT_List", iEventNumber, iOrderNumber, iRoleID, iPersonnelID,
                iTitle, iLevel, iStatus, iPageIndex, iPageSize);
            try
            {
                return BaseInstance.Base_Notify_SELECT_List(iEventNumber, iOrderNumber, iRoleID, iPersonnelID,
                    iTitle, iLevel, iStatus, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Notify_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Base_NotifyDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Notify_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询Base_Notify
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Base_NotifyDC> Base_Notify_SELECT_Entity(int iID)
        {
            Log_DeBug("Base_Notify_SELECT_Entity", iID);
            try
            {
                return BaseInstance.Base_Notify_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Notify_SELECT_Entity", ex);
                return new ReturnEntity<Base_NotifyDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Notify_SELECT_Entity");
            }
        }

        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Base_Notify_UPDATE_Get(int iNotifyID, int iMuser)
        {
            Log_DeBug("Base_Notify_UPDATE_Get", iNotifyID, iMuser);
            try
            {
                return BaseInstance.Base_Notify_UPDATE_Get(iNotifyID, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Notify_UPDATE_Get", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Notify_UPDATE_Get");
            }
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
            Log_DeBug("Base_Notify_UPDATE_Result", iNotifyID, iResult, iMuser);
            try
            {
                return BaseInstance.Base_Notify_UPDATE_Result(iNotifyID, iResult, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Notify_UPDATE_Result", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Notify_UPDATE_Result");
            }
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
            Log_DeBug("Base_Notify_UPDATE_Finish", iNotifyID, iResult, iStatus, iMuser);
            try
            {
                return BaseInstance.Base_Notify_UPDATE_Finish(iNotifyID, iResult, iStatus, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_Notify_UPDATE_Finish", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_Notify_UPDATE_Finish");
            }
        }
    }
}
