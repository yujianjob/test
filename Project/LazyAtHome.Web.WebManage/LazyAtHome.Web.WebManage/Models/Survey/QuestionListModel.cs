using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Models.Survey
{
    /// <summary>
    /// 问卷问题列表页绑定实体
    /// </summary>
    [Serializable]
    public class QuestionListModel
    {

        public QuestionSearchInfo SearchInfo;

        /// <summary>
        /// 问题集合
        /// </summary>
        public IList<Survey_QuestionDC> QuestionList;

        ///// <summary>
        ///// 问卷ID
        ///// </summary>
        //public int SID;

        /// <summary>
        /// 问卷确认状态（0：未上线 1：已上线 2：已下线）
        /// 只有0未上线 才能编辑
        /// </summary>
        public int CommitStatus;
    }
}