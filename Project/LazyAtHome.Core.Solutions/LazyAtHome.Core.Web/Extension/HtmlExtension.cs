using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.Core.Web.Helper;


namespace LazyAtHome.Core.Web.Extension
{
	/// <summary>
	/// 一些针对 HTML 的扩展方法
	/// </summary>
	public static class HtmlExtension
	{
		public static string ToHtml(this HtmlOptionItem item)
		{
			if( item == null )
				return string.Empty;

			return item.Text.ToHtmlOption(item.Value, item.Selected);
		}

		public static string ToHtmlOption(this string text)
		{
			return string.Format("<option value='{0}'>{0}</option>\r\n", text.HtmlEncode());
		}

		public static string ToHtmlOption(this string text, bool selected)
		{
			return string.Format("<option value='{0}' {1}>{0}</option>\r\n", text.HtmlEncode(), (selected ? "selected='selected'" : string.Empty));
		}

		public static string ToHtmlOption(this string text, string value)
		{
			return string.Format("<option value='{0}'>{1}</option>\r\n", value.HtmlEncode(), text.HtmlEncode());
		}

		public static string ToHtmlOption(this string text, string value, bool selected)
		{
			return string.Format("<option value='{0}' {1}>{2}</option>\r\n", value.HtmlEncode(), (selected ? "selected='selected'" : string.Empty), text.HtmlEncode());
		}

		public static string ToHtmlOptionUrl(this string text, string value)
		{
			return string.Format("<option value='{0}'>{1}</option>\r\n", value, text.HtmlEncode());
		}

		public static string ToHtmlOptionUrl(this string text, string value, bool selected)
		{
			return string.Format("<option value='{0}' {1}>{2}</option>\r\n", value, (selected ? "selected='selected'" : string.Empty), text.HtmlEncode());
		}

		public static string ToHtml(this List<HtmlOptionItem> list)
		{
			if( list == null || list.Count == 0 )
				return string.Empty;


			StringBuilder sb = new StringBuilder();
			foreach( var item in list )
				sb.Append(item.ToHtml());

			return sb.ToString();
		}

		public static string ToCheckBox(this bool value, string id, string name, bool disabled)
		{
			return string.Format("<input type='checkbox' {0} {1} {2} {3} />",
				(id == null ? string.Empty : string.Format("id='{0}'", id)),
				(name == null ? string.Empty : string.Format("name='{0}'", name)),
				(value ? "checked='checked'" : string.Empty),
				(disabled ? "disabled='disabled'" : string.Empty)
				);
		}

		public static string PaginationBar(this PagingInfo pagingInfo, int colspan)
		{
			return pagingInfo.ToHtml("pageindex", colspan);
		}

		public static string PaginationBar(this PagingInfo pagingInfo)
		{
			return pagingInfo.ToHtml("pageindex");
		}

		/// <summary>
		/// 让某个下拉框拥有选择后自动刷新功能，这要求每个option的value是一个URL
		/// </summary>
		public static string DropDownListAutoRedir(string ctlId)
		{
			return string.Format(@"javascript:setTimeout('window.location.href = document.getElementById(\'{0}\').value', 0)", ctlId);
		}


	}
}