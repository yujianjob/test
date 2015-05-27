using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public partial class Base
    {
        /// <summary>
        /// 新增问卷
        /// </summary>
        /// <param name="iSurvey_MainDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Survey_Main_ADD(Survey_MainDC iSurvey_MainDC)
        {
            var rtn = baseDAL.Survey_Main_ADD(iSurvey_MainDC);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 修改问卷
        /// </summary>
        /// <param name="iSurvey_MainDC">主键</param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Main_UPDATE(Survey_MainDC iSurvey_MainDC)
        {
            var rtn = baseDAL.Survey_Main_UPDATE(iSurvey_MainDC);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Main_DELETE(int iID, int iMuser)
        {
            var rtn = baseDAL.Survey_Main_DELETE(iID, iMuser);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 查询全部问卷
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Survey_MainDC>> Survey_Main_SELECT_List(
            string iTitle, int iPageIndex, int iPageSize)
        {
            var rtn = baseDAL.Survey_Main_SELECT_List(iTitle, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Survey_MainDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询问卷
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Survey_MainDC> Survey_Main_SELECT_Entity(int iID)
        {
            var rtn = baseDAL.Survey_Main_SELECT_Entity(iID);

            return new ReturnEntity<Survey_MainDC>(rtn);
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Survey_Question_ADD(Survey_QuestionDC iSurvey_QuestionDC)
        {
            var rtn = baseDAL.Survey_Question_ADD(iSurvey_QuestionDC);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="iSurvey_QuestionDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Question_UPDATE(Survey_QuestionDC iSurvey_QuestionDC)
        {
            var rtn = baseDAL.Survey_Question_UPDATE(iSurvey_QuestionDC);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 查询全部问题
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Survey_QuestionDC>> Survey_Question_SELECT_List(int iSurID)
        {
            var rtn = baseDAL.Survey_Question_SELECT_List(iSurID);

            return new ReturnEntity<IList<Survey_QuestionDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询问题
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Survey_QuestionDC> Survey_Question_SELECT_Entity(int iID)
        {
            var rtn = baseDAL.Survey_Question_SELECT_Entity(iID);

            return new ReturnEntity<Survey_QuestionDC>(rtn);
        }

        /// <summary>
        /// 新增回答
        /// </summary>
        /// <param name="iSurvey_AnswerDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Survey_Answer_ADD(Survey_AnswerDC iSurvey_AnswerDC)
        {
            var rtn = baseDAL.Survey_Answer_ADD(iSurvey_AnswerDC);

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 查询全部回答
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Survey_AnswerDC>> Survey_Answer_SELECT_List(int? iSurID,
            string iUserSource, int iPageIndex, int iPageSize)
        {
            var rtn = baseDAL.Survey_Answer_SELECT_List(iSurID, iUserSource, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Survey_AnswerDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询回答
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Survey_AnswerDC> Survey_Answer_SELECT_Entity(int iID)
        {
            var rtn = baseDAL.Survey_Answer_SELECT_Entity(iID);

            return new ReturnEntity<Survey_AnswerDC>(rtn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSurveyID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Survey_Stat(int iSurveyID)
        {
            baseDAL.Survey_Stat(iSurveyID);

            return new ReturnEntity<bool>(true);
        }
    }
}
