using System;

namespace LazyAtHome.Core.Web.Base
{
	/// <summary>
	/// 分页信息
	/// </summary>
    /// 
    [Serializable]
    public class PagingInfo
    {
        string _PageParam;

        /// <summary>
        /// 分页序号，从0开始计数
        /// </summary>
        public int PageIndex = 1;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize = 20;
        /// <summary>
        /// 从相关查询中获取到的符合条件的总记录数
        /// </summary>
        public int RecCount;

        /// <summary>
        /// 查询参数
        /// </summary>
        public virtual string PageParam 
        {
            set { _PageParam = value; }
            get { return _PageParam; } 
        }

        public string RawUrl = Helper.HttpContextHelper.AbsolutePath;

    }
}