using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 问卷调查wcf代理类
    /// </summary>
    public class SurveyProxy
    {
        /// <summary>
        /// 获取问卷调查列表
        /// </summary>
        /// <param name="iTitle"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Survey_MainDC> GetSurveyList(string iTitle, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Survey_MainDC> rtn = null;
            ReturnEntity<RecordCountEntity<Survey_MainDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<RecordCountEntity<Survey_MainDC>>>
                    (client => client.Proxy.Survey_Main_SELECT_List(iTitle, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy GetSurveyList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }



        /// <summary>
        /// 新增问卷调查信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddSurvey(Survey_MainDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Survey_Main_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy AddSurvey|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取问卷调查信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Survey_MainDC> GetSurveyBYID(int id)
        {

            //调用计算选择数量

            ReturnEntity<bool> reStat = StatSurvey(id);

            ReturnEntity<Survey_MainDC> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<Survey_MainDC>>
                   (client => client.Proxy.Survey_Main_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy GetSurveyBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 更新问卷调查信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateSurvey(Survey_MainDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Survey_Main_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy UpdateSurvey|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 删除问卷调查信息
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> DeleteSurvey(int iID, int iMuser)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Survey_Main_DELETE(iID, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy DeleteSurvey|" + ex.Message);
            }
            return re;
        }




        /// <summary>
        /// 根据问卷ID获取问卷题目
        /// </summary>
        /// <param name="iSurID"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<Survey_QuestionDC>> GetQuestionList(int iSurID)
        {
            ReturnEntity<IList<Survey_QuestionDC>> rtn = null;

            try
            {
                rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<IList<Survey_QuestionDC>>>
                    (client => client.Proxy.Survey_Question_SELECT_List(iSurID));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy GetQuestionList|" + ex.Message);
            }
            //if (rtn != null && rtn.Success)
            //{
            //    rtn = rce.OBJ;
            //}
            return rtn;
        }


        /// <summary>
        /// 新增问卷问题信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddQuestion(Survey_QuestionDC entity)
        {
            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<int>>
                   (client => client.Proxy.Survey_Question_ADD(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy AddQuestion|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 根据ID获取问卷问题信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Survey_QuestionDC> GetQuestionBYID(int id)
        {
            ReturnEntity<Survey_QuestionDC> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<Survey_QuestionDC>>
                   (client => client.Proxy.Survey_Question_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy GetQuestionBYID|" + ex.Message);
            }
            return re;

        }

        /// <summary>
        /// 更新问卷问题信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UpdateQuestion(Survey_QuestionDC entity)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Survey_Question_UPDATE(entity));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy UpdateQuestion|" + ex.Message);
            }
            return re;
        }


        /// <summary>
        /// 获取用户问卷答题列表
        /// </summary>
        /// <param name="iSurID"></param>
        /// <param name="iUserSource"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Survey_AnswerDC> GetSurveyAnswerList(int? iSurID, string iUserSource, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Survey_AnswerDC> rtn = null;
            ReturnEntity<RecordCountEntity<Survey_AnswerDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<RecordCountEntity<Survey_AnswerDC>>>
                    (client => client.Proxy.Survey_Answer_SELECT_List(iSurID, iUserSource, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy GetSurveyAnswerList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        
        }



        /// <summary>
        /// 获取答题详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Survey_AnswerDC> GetAnswerBYID(int id)
        {
            ReturnEntity<Survey_AnswerDC> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<Survey_AnswerDC>>
                   (client => client.Proxy.Survey_Answer_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy GetAnswerBYID|" + ex.Message);
            }
            return re;

        }




        /// <summary>
        /// 问卷选项统计
        /// </summary>
        /// <param name="iSurveyID"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> StatSurvey(int iSurveyID)
        {
            ReturnEntity<bool> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Survey_Stat(iSurveyID));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|SurveyProxy StatSurvey|" + ex.Message);
            }
            return re;
        }

    }
}