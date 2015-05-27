using System;
using System.Web;
using System.Text;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.Core.Web.Extension;

namespace LazyAtHome.Core.Web.Helper
{
	/// <summary>
	///JQueryEasyUiHelper 的摘要说明
	/// </summary>
	public static class JQueryEasyUiHelper
	{

		/// <summary>
		/// 根据分页参数生成与JQuery Easy UI 兼容的分页条代码
		/// </summary>
		/// <param name="pagingInfo">分页参数</param>
		/// <param name="urlParamName">URL中的分页序号参数</param>		
		/// <param name="colspan">td元素要跨越的列数</param>
		/// <returns>分页条代码</returns>
		public static string ToHtml(this PagingInfo pagingInfo, string urlParamName, int colspan)
		{
			string bar = ToHtml(pagingInfo, urlParamName);
			if( string.IsNullOrEmpty(bar) )
				return bar;


			string html = string.Concat(
				string.Format("<tfoot><tr class='GridView_FooterStyle'><td colspan='{0}'>", colspan),
				bar.ToString(),
				"</td></tr></tfoot>");
			return html;
		}



		/// <summary>
		/// 根据分页参数生成与JQuery Easy UI 兼容的分页条代码
		/// </summary>
		/// <param name="pagingInfo">分页参数</param>
		/// <param name="urlParamName">URL中的分页序号参数</param>
		/// <returns>分页条代码</returns>
		public static string ToHtml(this PagingInfo pagingInfo, string urlParamName)
		{
			if( pagingInfo == null )
				throw new ArgumentNullException("pagingInfo");

			if( pagingInfo.PageSize == 0 )
				throw new ArgumentOutOfRangeException("PagingInfo.PageSize is zero.");

			if( string.IsNullOrEmpty(urlParamName) )
				throw new ArgumentNullException("urlParamName");

			int pageCount = (int)Math.Ceiling((double)pagingInfo.RecCount / (double)pagingInfo.PageSize);

			if( pageCount < 1 )
				return string.Empty;

            if (pagingInfo.PageIndex == 0)
                pagingInfo.PageIndex = 1;

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<div class='pagination'>");
			sb.AppendLine("<table cellpadding='0' cellspacing='0' border='0'><tr>");
			sb.AppendLine("<td>{labFirst}</td>");
			sb.AppendLine("<td>{labPrev}</td>");
			sb.AppendLine("<td><div class='pagination-btn-separator'></div></td>");
			sb.AppendLine("<td>&nbsp;页&nbsp;</td>");
			sb.AppendLine("<td>{txtCurrentPage}</td>");
			sb.AppendLine("<td>&nbsp;/{labPageCount}&nbsp;</td>");
			sb.AppendLine("<td><div class='pagination-btn-separator'></div></td>");
			sb.AppendLine("<td>{labNext}</td>");
			sb.AppendLine("<td>{labLast}</td>");
			sb.AppendLine("<td><div class='pagination-btn-separator'></div></td>");
			sb.AppendLine("<td>{labRefresh}</td>");
			sb.AppendLine("</tr></table>");
			sb.AppendLine("<div class='pagination-info'>{labInfo}</div>");
			sb.AppendLine("{script}");
			sb.AppendLine("<div style='clear: both;'></div></div>");

			bool firstEnabled = (pagingInfo.PageIndex != 1);
			bool lastEnabled = (pagingInfo.PageIndex < pageCount);
            //int urlActionLength = HttpContextHelper.RequestRawUrl.IndexOf('?');
            //if (urlActionLength == -1)
            //    urlActionLength = HttpContextHelper.RequestRawUrl.Length;
            string requestRawUrl = HttpContextHelper.Authority + pagingInfo.RawUrl; //HttpContextHelper.RequestRawUrl.Substring(0, urlActionLength);
            if (!string.IsNullOrWhiteSpace(pagingInfo.PageParam))
                requestRawUrl += "?" + pagingInfo.PageParam;

			MyUrlGenerator generate = new MyUrlGenerator(requestRawUrl);
			string linkFirst = generate.GetNewUrl(urlParamName, "1");
			string linkLast = generate.GetNewUrl(urlParamName, pageCount.ToString());
            string linkPrevious = generate.GetNewUrl(urlParamName, (pagingInfo.PageIndex - 1).ToString());
            string linkNext = generate.GetNewUrl(urlParamName, (pagingInfo.PageIndex + 1).ToString());

			string ctlId = "txt" + Guid.NewGuid().ToString("N");
			string txtCurrentPage = string.Format("<input type='text' class='pageNumber' value='{0}' baseUrl=\"{1}\" max='{2}' autoRedire='true' param=\"{3}\" id='{4}' />",
				(pagingInfo.PageIndex).ToString(),
				HttpUtility.HtmlEncode(generate.GetNewUrl(null, null, urlParamName)),
				pageCount.ToString(),
				HttpUtility.HtmlEncode(urlParamName),
				ctlId);

			string script = string.Format("<script type=\"text/javascript\">$(function(){{ SetPageNumberTextbox('{0}'); }});</script>", ctlId);

			string labFirst = GenerateButton("pagination-first", linkFirst, null, firstEnabled, "转到第一页");
			string labPrev = GenerateButton("pagination-prev", linkPrevious, null, firstEnabled, "转到前一页");
			string labNext = GenerateButton("pagination-next", linkNext, null, lastEnabled, "转到后一页");
			string labLast = GenerateButton("pagination-last", linkLast, null, lastEnabled, "转到最后一页");
			string labPageCount = pageCount.ToString();
            string labRefresh = GenerateButton("pagination-load", requestRawUrl, "刷新", true, "刷新当前页");
            int startRecord = 1;
            if (pagingInfo.PageIndex != 1)
                startRecord = (pagingInfo.PageIndex - 1) * pagingInfo.PageSize + 1;
			int endRecord = pagingInfo.PageIndex * pagingInfo.PageSize;
            string labInfo = string.Format("显示范围：{0} 到 {1} ，  共 {2} 条记录",
                startRecord, ((endRecord < pagingInfo.RecCount) ? endRecord : pagingInfo.RecCount), pagingInfo.RecCount);

			sb.Replace("{labFirst}", labFirst).Replace("{labPrev}", labPrev).Replace("{txtCurrentPage}", txtCurrentPage);
			sb.Replace("{labPageCount}", labPageCount).Replace("{labNext}", labNext).Replace("{labLast}", labLast);
			sb.Replace("{labRefresh}", labRefresh).Replace("{labInfo}", labInfo).Replace("{script}", script);

			return sb.ToString();
		}

		private static string GenerateButton(string icon, string href, string text, bool enabled, string tipText)
		{
			if( enabled == false ) {
				href = "javascript:void(0);";
				tipText = string.Empty;
			}

			if( string.IsNullOrEmpty(text) )
				return string.Format(string.Concat(
					"<a icon='{0}' href='{1}' class='l-btn l-btn-plain {2}' title='{3}' autoRedire='{4}'>",
					"<span class='l-btn-left'><span class='l-btn-text'>",
					"<span class='l-btn-empty {0}'>&nbsp;</span></span></span></a>"),
					icon, href, (enabled ? string.Empty : "l-btn-disabled"), tipText, enabled.ToString().ToLower());

			else
				return string.Format(string.Concat(
					"<a icon='{0}' href='{1}' class='l-btn l-btn-plain' style='float: left;' title='{3}' autoRedire='{4}'>",
					"<span class='l-btn-left'>",
					"<span class='l-btn-text {0}' style='padding-left: 20px;'>{2}</span></span></a>"),
					icon, href, text, tipText, enabled.ToString().ToLower());
		}

	}
}