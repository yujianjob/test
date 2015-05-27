using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.Web.WebManage.Models.Export;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using System.Text;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class ExportController : BaseController
    {

        #region 订单跟踪报表

        /// <summary>
        /// 订单跟踪展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderStepIndex()
        {
            return SearchOrderStep(null, null);
        }

        /// <summary>
        /// 订单跟踪搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchOrderStep(OrderStepSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new OrderStepSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-2);
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OrderStepListModel list = new OrderStepListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetOrderStepReportList(search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.OrderStepList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("OrderStepIndex", list);
        }


        /// <summary>
        /// 订单跟踪导出
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public ActionResult ExportOrderStep(string StartDate, string EndDate)
        {
            //处理时间
            DateTime begin = Convert.ToDateTime(StartDate);
            DateTime end = Convert.ToDateTime(EndDate);

            //表格Title
            string ExTitle = begin.ToString("yy年MM月dd日") + "至" + end.ToString("yy年MM月dd日") + "订单跟踪清单";

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;

            List<Order_Order_StatDC> listall = new List<Order_Order_StatDC>();
            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetOrderStepReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            if (totalcount > pagesize)
            {
                loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                    : ((totalcount - pagesize) / pagesize + 1);
                for (int i = 2; i <= loopcount + 1; i++)
                {
                    pageinfo.pageNum = i;
                    RecordCountEntity<Order_Order_StatDC> rce2 = OrderProxy.GetOrderStepReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
                    if (rce2 != null)
                    {
                        //totalcount = rce2.RecordCount;//设置查询总记录数
                        listall.AddRange(rce2.ReturnList);
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    }
                }
            }

            #region 数据排版

            StringBuilder strRecord = new StringBuilder();
            strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
            strRecord.Append("<tr><td colspan=\"12\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");
            strRecord.Append("<tr>");
            strRecord.Append("<td align=\"center\"><strong>序号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单地址</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>下单渠道</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>预约日期</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>进程</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>下单日期</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>推送物流时间</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>绑物流条码时间</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>入库时间</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>出库时间</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单完成时间</strong></td>");
            strRecord.Append("</tr>");

            int index = 0;
            foreach (Order_Order_StatDC item in listall)
            {
                index++;

                strRecord.Append("<tr>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + index + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Address + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + FormatRegistSource(item.Channel) + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.ExpectTime + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + FormatOrderStep(item.Step) + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.CompleteTime + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.PushExpressTime + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.StepTime3 + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.StepTime4 + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.StepTime5 + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.AllFinishTime + "</td>");
                strRecord.Append("</tr>");
            }

            strRecord.Append("</table>");

            LoadExlData downdata = new LoadExlData
            {
                Title = ExTitle,
                DataObject = strRecord.ToString()
            };
            if (downdata.OnLoadData())
                return Json(new { success = true, message = "../Aspx/ToExcel.aspx" });
            else
                return Json(new { success = false, message = string.Format("error") });

            #endregion

        }

        #endregion


        #region 入库报表

        /// <summary>
        /// 入库报表展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult StockInIndex()
        {
            return SearchStockIn(null, null);
        }

        /// <summary>
        /// 出库报表搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchStockIn(StockInSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new StockInSearchInfo();
                search.DateFrom = System.DateTime.Now.Date;
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            StockInListModel list = new StockInListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetStockInReportList(search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.StockInList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("StockInIndex", list);
        }



        /// <summary>
        /// 入库清单导出
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public ActionResult ExportStockIn(string StartDate, string EndDate)
        {
            //处理时间
            DateTime begin = Convert.ToDateTime(StartDate);
            DateTime end = Convert.ToDateTime(EndDate);

            //表格Title
            string ExTitle = begin.ToString("yy年MM月dd日") + "至" + end.ToString("yy年MM月dd日") + "入库清单";

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;

            List<Order_Order_StatDC> listall = new List<Order_Order_StatDC>();
            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetStockInReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            if (totalcount > pagesize)
            {
                loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                    : ((totalcount - pagesize) / pagesize + 1);
                for (int i = 2; i <= loopcount + 1; i++)
                {
                    pageinfo.pageNum = i;
                    RecordCountEntity<Order_Order_StatDC> rce2 = OrderProxy.GetStockInReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
                    if (rce2 != null)
                    {
                        //totalcount = rce2.RecordCount;//设置查询总记录数
                        listall.AddRange(rce2.ReturnList);
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    }
                }
            }

            #region 数据排版

            StringBuilder strRecord = new StringBuilder();
            strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
            strRecord.Append("<tr><td colspan=\"9\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");
            strRecord.Append("<tr>");
            strRecord.Append("<td align=\"center\"><strong>序号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>产品信息</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>水洗条码</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>出库时间</strong></td>");
            //strRecord.Append("<td align=\"center\"><strong>产品标签</strong></td>");    
            strRecord.Append("<td align=\"center\"><strong>收件手机</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>收件人</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>收件地址</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>下单日期</strong></td>");
            strRecord.Append("</tr>");

            int index = 0;
            foreach (Order_Order_StatDC item in listall)
            {
                index++;

                strRecord.Append("<tr>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + index + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Mpno + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Consignee + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Address + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.CompleteTime + "</td>");
                strRecord.Append("</tr>");

                foreach (Order_ProductDC pitem in item.ProductList)
                {

                    strRecord.Append("<tr>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + (string.IsNullOrEmpty(pitem.Attribute) ? pitem.Name : pitem.Name + "(" + pitem.Attribute + ")") + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + pitem.Code + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + pitem.Step5Time + "</td>");
                    //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + pitem.Attribute + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + "" + "</td>");
                    strRecord.Append("</tr>");
                }
            }

            strRecord.Append("</table>");

            LoadExlData downdata = new LoadExlData
            {
                Title = ExTitle,
                DataObject = strRecord.ToString()
            };
            if (downdata.OnLoadData())
                return Json(new { success = true, message = "../Aspx/ToExcel.aspx" });
            else
                return Json(new { success = false, message = string.Format("error") });

            #endregion
        }


        /// <summary>
        /// 入库清单导出(物流格式)
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public ActionResult ExportExpressStockIn(string StartDate, string EndDate)
        {
            //处理时间
            DateTime begin = Convert.ToDateTime(StartDate);
            DateTime end = Convert.ToDateTime(EndDate);

            //表格Title
            string ExTitle = begin.ToString("yy年MM月dd日") + "至" + end.ToString("yy年MM月dd日") + "入库清单(物流格式)";

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;

            List<Order_Order_StatDC> listall = new List<Order_Order_StatDC>();
            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetStockInReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            if (totalcount > pagesize)
            {
                loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                    : ((totalcount - pagesize) / pagesize + 1);
                for (int i = 2; i <= loopcount + 1; i++)
                {
                    pageinfo.pageNum = i;
                    RecordCountEntity<Order_Order_StatDC> rce2 = OrderProxy.GetStockInReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
                    if (rce2 != null)
                    {
                        //totalcount = rce2.RecordCount;//设置查询总记录数
                        listall.AddRange(rce2.ReturnList);
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    }
                }
            }


            #region 数据排版

            StringBuilder strRecord = new StringBuilder();
            strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
            strRecord.Append("<tr><td colspan=\"8\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");
            strRecord.Append("<tr>");
            strRecord.Append("<td align=\"center\"><strong>序号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>物流号</strong></td>");   
            strRecord.Append("<td align=\"center\"><strong>客户名称</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>客户地址</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>客户电话</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>件数</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>金额</strong></td>");
            strRecord.Append("</tr>");

            int index = 0;
            foreach (Order_Order_StatDC item in listall)
            {
                index++;

                strRecord.Append("<tr>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + index + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Consignee + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Address + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Mpno + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + item.ProductList.Count + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + "" + "</td>");
                strRecord.Append("</tr>");
            }

            strRecord.Append("</table>");

            LoadExlData downdata = new LoadExlData
            {
                Title = ExTitle,
                DataObject = strRecord.ToString()
            };
            if (downdata.OnLoadData())
                return Json(new { success = true, message = "../Aspx/ToExcel.aspx" });
            else
                return Json(new { success = false, message = string.Format("error") });

            #endregion
        }

        #endregion


        #region 出库报表

        /// <summary>
        /// 出库报表展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult StockOutIndex()
        {
            return SearchStockOut(null, null);
        }

        /// <summary>
        /// 出库报表搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchStockOut(StockOutSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new StockOutSearchInfo();
                search.DateFrom = System.DateTime.Now.Date;
                search.DateTo = System.DateTime.Now.Date;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            StockOutListModel list = new StockOutListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetStockOutReportList(search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.StockOutList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("StockOutIndex", list);
        }


        /// <summary>
        /// 出库清单导出
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public ActionResult ExportStockOut(string StartDate, string EndDate)
        {
            //处理时间
            DateTime begin = Convert.ToDateTime(StartDate);
            DateTime end = Convert.ToDateTime(EndDate);

            //表格Title
            string ExTitle = begin.ToString("yy年MM月dd日") + "至" + end.ToString("yy年MM月dd日") + "出库清单";

            int pagesize = CodeSource.Common.ConstConfig.ExportPageSize;//分页系数
            int loopcount = 0;//循环次数
            int totalcount = 0;//总数据数

            CodeSource.PagingInfo pageinfo = new CodeSource.PagingInfo();//设置分页参数
            pageinfo.pageNum = 1;
            pageinfo.numPerPage = CodeSource.Common.ConstConfig.ExportPageSize; //pagesize;

            List<Order_Order_StatDC> listall = new List<Order_Order_StatDC>();
            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetStockOutReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
            if (rce != null)
            {
                totalcount = rce.RecordCount;//设置查询总记录数
                listall = rce.ReturnList.ToList();
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            if (totalcount > pagesize)
            {
                loopcount = ((totalcount - pagesize) % pagesize == 0) ? (totalcount - pagesize) / pagesize
                    : ((totalcount - pagesize) / pagesize + 1);
                for (int i = 2; i <= loopcount + 1; i++)
                {
                    pageinfo.pageNum = i;
                    RecordCountEntity<Order_Order_StatDC> rce2 = OrderProxy.GetStockOutReportList(begin, end.AddDays(1), pageinfo.pageNum, pageinfo.numPerPage);
                    if (rce2 != null)
                    {
                        //totalcount = rce2.RecordCount;//设置查询总记录数
                        listall.AddRange(rce2.ReturnList);
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                    }
                }
            }

            #region 数据排版

            StringBuilder strRecord = new StringBuilder();
            strRecord.Append("<table id=\"data\" cellspacing=\"0\" cellpadding=\"0\"  border=\"1\">");
            strRecord.Append("<tr><td colspan=\"9\" align=\"center\" style=\"color:#ff0000;font-weight: bold; font-size: 12pt;\">" + ExTitle + "</td></tr>");
            strRecord.Append("<tr>");
            strRecord.Append("<td align=\"center\"><strong>序号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>订单号</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>产品信息</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>水洗条码</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>出库时间</strong></td>");
            //strRecord.Append("<td align=\"center\"><strong>产品标签</strong></td>");    
            strRecord.Append("<td align=\"center\"><strong>收件手机</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>收件人</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>收件地址</strong></td>");
            strRecord.Append("<td align=\"center\"><strong>下单日期</strong></td>");
            strRecord.Append("</tr>");

            int index = 0;
            foreach (Order_Order_StatDC item in listall)
            {
                index++;

                strRecord.Append("<tr>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + index + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.OrderNumber + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Mpno + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Consignee + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + item.Address + "</td>");
                strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + item.CompleteTime + "</td>");
                strRecord.Append("</tr>");

                foreach (Order_ProductDC pitem in item.ProductList)
                {

                    strRecord.Append("<tr>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + (string.IsNullOrEmpty(pitem.Attribute) ? pitem.Name : pitem.Name + "(" + pitem.Attribute + ")") + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + pitem.Code + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:yyyy-mm-dd HH:mm:ss\">" + pitem.Step5Time + "</td>");
                    //strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + pitem.Attribute + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:@\">" + "" + "</td>");
                    strRecord.Append("<td style=\"color:#000000;font-size: 9pt;vnd.ms-excel.numberformat:#,##0\">" + "" + "</td>");
                    strRecord.Append("</tr>");
                }
            }

            strRecord.Append("</table>");

            LoadExlData downdata = new LoadExlData
            {
                Title = ExTitle,
                DataObject = strRecord.ToString()
            };
            if (downdata.OnLoadData())
                return Json(new { success = true, message = "../Aspx/ToExcel.aspx" });
            else
                return Json(new { success = false, message = string.Format("error") });

            #endregion
        }

        #endregion


        #region 订单超时

        /// <summary>
        ///订单超时展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderAlarmIndex()
        {
            return SearchOrderAlarm(null, null);
        }

        /// <summary>
        /// 订单超时搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchOrderAlarm(OrderAlarmSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new OrderAlarmSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-30);
                search.DateTo = System.DateTime.Now.Date;

                search.isGetPackage = true;
                search.isWash = true;
                search.isSendPackage = true;
                search.isAll = true;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OrderAlarmListModel list = new OrderAlarmListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetOrderAlarmList(search.isGetPackage, search.isWash, search.isSendPackage, search.isAll, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.OrderAlarmList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("OrderAlarmIndex", list);
        }

        #endregion


        #region 订单预警

        public ActionResult OrderWarningIndex()
        {
            return SearchOrderWarning(null, null);
        }

        public ActionResult SearchOrderWarning(OrderWarningSearchInfo search, int? pageNum)
        {
            if (search == null)
            {
                search = new OrderWarningSearchInfo();
                search.DateFrom = System.DateTime.Now.Date.AddDays(-30);
                search.DateTo = System.DateTime.Now.Date;

                search.isGetPackage = true;
                search.isWash = true;
                search.isSendPackage = true;
            }

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            OrderWarningListModel list = new OrderWarningListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理

            RecordCountEntity<Order_Order_StatDC> rce = OrderProxy.GetOrderWarningList(search.isGetPackage, search.isWash, search.isSendPackage, search.DateFrom, search.DateTo.AddDays(1), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.OrderWarningList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("OrderWarningIndex", list);
        }

        #endregion

        private string FormatRegistSource(int RegistSource)
        {
            string rtn = string.Empty;
            switch (RegistSource)
            {
                case -1:
                    rtn = "客服";
                    break;
                case 1:
                    rtn = "网站";
                    break;
                case 2:
                    rtn = "APP";
                    break;
                case 3:
                    rtn = "新浪微博";
                    break;
                case 4:
                    rtn = "微信";
                    break;
                case 6:
                    rtn = "短信";
                    break;
                case 90:
                    rtn = "合作门店";
                    break;

                default:
                    rtn = "未知：" + RegistSource;
                    break;
            }
            return rtn;

        }

        private string FormatOrderStep(int? OrderStep)
        {
            string rtn = string.Empty;
            switch (OrderStep)
            {
                case 1:
                    rtn = "下单";
                    break;
                case 2:
                    rtn = "取件中";
                    break;
                case 3:
                    rtn = "送洗中";
                    break;
                case 4:
                    rtn = "洗涤中";
                    break;
                case 5:
                    rtn = "出库中";
                    break;
                case 6:
                    rtn = "送件中";
                    break;
                case 7:
                    rtn = "完成";
                    break;
                default:
                    rtn = "未知：" + OrderStep;
                    break;
            }
            return rtn;
        }
	}
}