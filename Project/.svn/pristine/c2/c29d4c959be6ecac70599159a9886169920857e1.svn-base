using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LazyAtHome.Web.WebManage
{
    public class LoadExlData
    {
        public LoadExlData()
        {
            ToExcel.DownTitle = string.Empty;
            ToExcel.DownData = null;
        }
        public string Title { get; set; }

        public string DataObject { get; set; }

        public bool OnLoadData()
        {
            try
            {
                if (string.IsNullOrEmpty(Title))
                    return false;
                else 
                    ToExcel.DownTitle = Title;

                if (DataObject == null)
                    return false;
                else 
                    ToExcel.DownData = DataObject;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    public partial class ToExcel : System.Web.UI.Page
    {
        public static string DownTitle { get; set; }
        public static string DownData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Mothed();
        }

        private void Mothed()
        {
            //* 说明：outputFileName设置文件名称时，必须将后缀一起算进去，为了支持FireFox浏览器的兼容性
            string outputFileName = null;  
            string browser = this.Context.Request.UserAgent.ToUpper();  
  
            if (browser.Contains("FIREFOX") == true)  
            {
                outputFileName = "\"" + DownTitle + ".xls"  + "\"";  
            }  
            else  
            {
                outputFileName = HttpUtility.UrlEncode(DownTitle + ".xls", System.Text.Encoding.UTF8);  
            }  
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            //attachment 参数表示作为附件下载，您可以改成 online在线打开 
            //filename=FileFlow.xls 指定输出文件的名称，注意其扩展名和指定文件类型相符，可以为：.doc 　　 .xls 　　 .txt 　　.htm　　
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + outputFileName );
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //Response.ContentType指定文件类型 可以为application/ms-excel 　　 application/ms-word 　　 application/ms-txt 　　 application/ms-html 　　 或其他浏览器可直接支持文档　 
            Response.ContentType = "application/vnd.ms-excel";  //* ！！！此处为了支持FireFox浏览器必须是“vnd.ms-excel”，不能是“ms-excel”
            this.EnableViewState = false;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            this.RenderControl(oHtmlTextWriter);
            //this 表示输出本页，你也可以绑定datagrid,或其他支持obj.RenderControl()属性的控件　
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        public string DownLoadExcelData()
        {
            return DownData;
        }

    }


}