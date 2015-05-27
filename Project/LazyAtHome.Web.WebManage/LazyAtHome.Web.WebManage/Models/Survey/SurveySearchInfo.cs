using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Survey
{
    /// <summary>
    /// 问卷调查列表查询实体
    /// </summary>
    [Serializable]
    public class SurveySearchInfo : CodeSource.PagingInfo
    {
        /// <summary>
        /// 问卷调查名称
        /// </summary>
        public string SurveyName { get; set; }
    }
}