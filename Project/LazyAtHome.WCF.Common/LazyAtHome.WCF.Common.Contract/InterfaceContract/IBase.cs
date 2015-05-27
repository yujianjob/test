﻿using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iAppDomainName"></param>
        /// <param name="iTitle"></param>
        /// <param name="iEventType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Base_LogDC>> Base_Log_SELECT_List(string iAppDomainName, string iTitle,
            int? iEventType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Base_CouponDC>> Base_Coupon_SELECT_List(string iName, int? iUseClass,
            int? iUseType, int? iCommitStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize);

        #region 网站SEO

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Base_WebAttribute_ADD(Base_WebAttributeDC iBase_WebAttributeDC);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iBase_WebAttributeDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Base_WebAttribute_UPDATE(Base_WebAttributeDC iBase_WebAttributeDC);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Base_WebAttribute_DELETE(int iID);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Base_WebAttributeDC>> Base_WebAttribute_SELECT_List(string iName,
          string iPage, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Base_WebAttributeDC> Base_WebAttribute_SELECT_Entity(int iID);

        /// <summary>
        /// 根据Page查询
        /// </summary>
        /// <param name="iPage"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Base_WebAttributeDC> web_Base_WebAttribute_SELECT_Entity(string iPage);

        #endregion

        #region 问卷

        /// <summary>
        /// 新增问卷
        /// </summary>
        /// <param name="iSurvey_MainDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Survey_Main_ADD(Survey_MainDC iSurvey_MainDC);

        /// <summary>
        /// 修改问卷
        /// </summary>
        /// <param name="iSurvey_MainDC">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Survey_Main_UPDATE(Survey_MainDC iSurvey_MainDC);

        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Survey_Main_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询全部问卷
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Survey_MainDC>> Survey_Main_SELECT_List(
          string iTitle, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Survey_MainDC> Survey_Main_SELECT_Entity(int iID);

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Survey_Question_ADD(Survey_QuestionDC iSurvey_QuestionDC);

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Survey_Question_UPDATE(Survey_QuestionDC iSurvey_QuestionDC);

        /// <summary>
        /// 查询全部问题
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Survey_QuestionDC>> Survey_Question_SELECT_List(int iSurID);

        /// <summary>
        /// 根据ID查询问题
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Survey_QuestionDC> Survey_Question_SELECT_Entity(int iID);

        /// <summary>
        /// 新增回答
        /// </summary>
        /// <param name="iSurvey_AnswerDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> Survey_Answer_ADD(Survey_AnswerDC iSurvey_AnswerDC);

        /// <summary>
        /// 查询全部回答
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Survey_AnswerDC>> Survey_Answer_SELECT_List(int? iSurID,
          string iUserSource, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询回答
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Survey_AnswerDC> Survey_Answer_SELECT_Entity(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSurveyID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Survey_Stat(int iSurveyID);

        #endregion

        [OperationContract]
        ReturnEntity<string> Tinyurl_CreateNew(string iUrl);

        [OperationContract]
        ReturnEntity<string> Tinyurl_Get(string iCode);

        /// <summary>
        /// 查询Base_Notify
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<Base_NotifyDC>> Base_Notify_SELECT_List(string iEventNumber,
            string iOrderNumber, int? iRoleID, int? iPersonnelID, string iTitle, int? iLevel, int? iStatus,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询Base_Notify
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<Base_NotifyDC> Base_Notify_SELECT_Entity(int iID);

        /// <summary>
        /// 领取
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Base_Notify_UPDATE_Get(int iNotifyID, int iMuser);

        /// <summary>
        /// 更新备注
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Base_Notify_UPDATE_Result(int iNotifyID, string iResult, int iMuser);

        /// <summary>
        /// 完成处理
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iStatus"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> Base_Notify_UPDATE_Finish(int iNotifyID, string iResult, int iStatus, int iMuser);
    }
}