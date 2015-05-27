using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.SFSupport.CodeSource
{
    public class PagingInfo
    {
        string _PageParam;
        public PagingInfo()
        {
            pageNum = 1;
            pageNumShown = 10;
            numPerPage = 15;

            Type type = this.GetType();
            this.numPerPageName = "numPerPage" + type.Name;
        }

        public string status { get; set; }
        public string keywords { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageNum { get; set; }
        /// <summary>
        /// 每页显示数
        /// </summary>
        public int numPerPage { get; set; }

        /// <summary>
        /// 页标数字个数
        /// </summary>
        public int pageNumShown { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string orderField { get; set; }

        /// <summary>
        /// 升序降序(asc)
        /// </summary>
        public string orderDirection { get; set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public virtual string PageParam
        {
            set { _PageParam = value; }
            get { return _PageParam; }
        }

        /// <summary>
        /// 从相关查询中获取到的符合条件的总记录数
        /// </summary>
        public int RecCount;

        /// <summary>
        /// 每页显示数的名称
        /// </summary>
        public string numPerPageName;
    }
}