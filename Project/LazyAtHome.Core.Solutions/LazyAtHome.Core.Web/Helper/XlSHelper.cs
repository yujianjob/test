using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace LazyAtHome.Core.Web.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class XlSHelper
    {
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="ServerFileName">xls文件路径</param>
        /// <param name="SelectSQL">查询SQL语句</param>
        /// <returns>DataSet</returns>
        public static DataSet SelectFromXLS(string ServerFileName, string SelectSQL)
        {
            string connStr = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + ServerFileName + "';Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da = new OleDbDataAdapter(SelectSQL, conn);
                da.Fill(ds, "SelectResult");
            }
            catch (Exception e)
            {
                conn.Close();
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return ds;

        }

        /// <summary>
        /// 获取工作表对应的SQL表名
        /// </summary>
        /// <param name="SheetName">工作表名</param>
        /// <returns>SQL表名</returns>
        public static string ConvertToSQLSheetName(string SheetName)
        {
            return "[" + SheetName + "$]";
        }

        /// <summary>
        /// 执行无返回查询
        /// </summary>
        /// <param name="ServerFileName">xls文件路径</param>
        /// <param name="QuerySQL">待执行的SQL语句</param>
        public static void ExcuteNonQuery(string ServerFileName, string QuerySQL)
        {
            string connStr = "Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = '" + ServerFileName + "';Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbCommand cmd = new OleDbCommand(QuerySQL, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception AnyError)
            {
                conn.Close();
                throw AnyError;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 上传Excel文件
        /// </summary>
        /// <param name="inputfile">上传的控件名</param>
        /// <returns></returns>
        //public static string UpLoadXls(HttpPostedFileBase inputfile)
        //{
        //    string orifilename = string.Empty;
        //    string uploadfilepath = string.Empty;
        //    string modifyfilename = string.Empty;
        //    string fileExt = "";//文件扩展名
        //    int fileSize = 0;//文件大小
        //    try
        //    {
        //        if (inputfile.FileName != string.Empty) //aa.FileName;
        //        {
        //            //得到文件的大小
        //            fileSize = inputfile.ContentLength;  //aa.ContentLength;
        //            if (fileSize == 0)
        //            {
        //                throw new Exception("导入的Excel文件大小为0，请检查是否正确！");
        //            }
        //            //得到扩展名
        //            fileExt = inputfile.FileName.Substring(inputfile.FileName.LastIndexOf(".") + 1);
        //            if (fileExt.ToLower() != "xls")
        //            {
        //                throw new Exception("你选择的文件格式不正确，只能导入EXCEL文件！");
        //            }
        //            //路径
        //            //System.Web.HttpServerUtility xlsServer = new System.Web.HttpServerUtility();
        //            //uploadfilepath = Server.MapPath("~/");
        //            //新文件名
        //            modifyfilename = System.Guid.NewGuid().ToString();
        //            modifyfilename += "." + inputfile.FileName.Substring(inputfile.FileName.LastIndexOf(".") + 1);
        //            //判断是否有该目录
        //            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadfilepath);
        //            if (!dir.Exists)
        //            {
        //                dir.Create();
        //            }
        //            orifilename = uploadfilepath + modifyfilename;
        //            //如果存在,删除文件
        //            if (File.Exists(orifilename))
        //            {
        //                File.Delete(orifilename);
        //            }
        //            // 上传文件
        //            inputfile.SaveAs(orifilename);
        //        }
        //        else
        //        {
        //            throw new Exception("请选择要导入的Excel文件!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return orifilename;
        //}
    }
}
