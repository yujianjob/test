using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;
using LazyAtHome.Winform.FactoryPortal.Model;

namespace LazyAtHome.Winform.FactoryPortal.UI.Test
{
    public partial class Form2 : Form
    {
        TfrxReportClass report;
        FrxDataTable datatable;
        


        public Form2()
        {
            InitializeComponent();


            // Create report object
            report = new TfrxReportClass();

            // Create the FR compatible DataTable object
            datatable = new FrxDataTable("Express");
            FillTableWithSampleData(datatable);


            // Load demmonstration report from file
            //report.LoadReportFromFile(@"D:\SF-Express-New2.fr3");
            //report.LoadReportFromFile(@"D:\SF.fr3");


            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\SF-Express-New2.fr3";
            // Load demmonstration report from file
            report.LoadReportFromFile(FilePath);
            //report.LoadReportFromFile(@"D:\SF.fr3");

            report.ClearDatasets();
            // Asiign datasets to report one more time
            // beacuse theLoadReport... family functions breaks links between report and dataset
            datatable.AssignToReport(true, report);

            // Assigns DataTable to DataBand
            datatable.AssignToDataBand("MasterData1", report);
        }


        private void FillTableWithSampleData(FrxDataTable datatable)
        {
            //datatable.Columns.Add("打印顺序", typeof(string));

            //datatable.Columns.Add("发件人", typeof(string));
            //datatable.Columns.Add("发件电话", typeof(string));
            //datatable.Columns.Add("发件地址", typeof(string));
            //datatable.Columns.Add("发件邮编", typeof(string));
            //datatable.Columns.Add("原寄地", typeof(string));

            //datatable.Columns.Add("收件人", typeof(string));
            //datatable.Columns.Add("收件电话", typeof(string));
            //datatable.Columns.Add("收件地址", typeof(string));
            //datatable.Columns.Add("收件邮编", typeof(string));
            //datatable.Columns.Add("目的地", typeof(string));

            //datatable.Columns.Add("收件员", typeof(string));

            //datatable.Columns.Add("货运单号", typeof(string));

            //datatable.Columns.Add("付款方式", typeof(string));

            //datatable.Columns.Add("月结卡号", typeof(string));


            //datatable.Columns.Add("货品信息", typeof(string));

            //datatable.Rows.Add(new object[] { 
            //"1",
            //"张吉利",
            //"13480150745",
            //"广东省深圳市中华楼",
            //"123456",
            //"原寄地",
            //"收件人",
            //"收件电话",
            //"收件地址",
            //"收件邮编",
            //"目的地",
            //"收件员",
            //"966999986668",
            //"付款方式",
            //"月结卡号",
            //"货品信息"
            //});

            datatable.Columns.Add("打印顺序", typeof(string));

            datatable.Columns.Add("发件人", typeof(string));
            datatable.Columns.Add("发件电话", typeof(string));
            datatable.Columns.Add("发件地址", typeof(string));
            datatable.Columns.Add("发件邮编", typeof(string));
            datatable.Columns.Add("原寄地", typeof(string));

            datatable.Columns.Add("收件人", typeof(string));
            datatable.Columns.Add("收件电话", typeof(string));
            datatable.Columns.Add("收件地址", typeof(string));
            datatable.Columns.Add("收件邮编", typeof(string));
            datatable.Columns.Add("目的地", typeof(string));

            datatable.Columns.Add("收件员", typeof(string));
            datatable.Columns.Add("货运单号", typeof(string));
            datatable.Columns.Add("付款方式", typeof(string));
            datatable.Columns.Add("月结卡号", typeof(string));
            datatable.Columns.Add("货品信息", typeof(string));

            datatable.Columns.Add("业务类型", typeof(string));
            datatable.Columns.Add("订单号", typeof(string));

            datatable.Columns.Add("代收货款", typeof(string));
            datatable.Columns.Add("代收货款金额", typeof(string));


            datatable.Rows.Add(new object[] { 
                "1-1",


                "任炯 ",//s.Consignee + " ",
                "15921728246",//s.Mpno + " ",
                "上海市龙漕路135弄37号202室",//s.DistrictName + s.Address,
                "",//邮编
                "021",//sforder.destcode,



                Common.ConstConfig.S_Company + " ",
                Common.ConstConfig.S_Tel + " ",
                Common.ConstConfig.S_Address,
                "",//邮编
                "021",//sforder.origincode,


                


                "",//收件员
                "207176460043",//"388400519937",//sforder.mailno,
                "第三方付",//付款方式
                Common.ConstConfig.SFMonthCardNo,//月结卡号
                "衣物",
                "",//"电商特惠",//"同城即日件",//"电商特惠",//"同城即日件",
                "114217360000010",//"114165740000001",//entity.OrderNumber
                "",//"COD",
                ""//"代收货款金额：￥" + "50.00" + "元"
                });



            datatable.AcceptChanges();


            //SFExpress Express = new SFExpress();
            //Express.No = "966999986668";
            //Express.Sender = "张吉利";
            //Express.SenderMpno = "13480150745";
            //Express.SenderAddress = "广东省深圳市中华楼4楼";
            //Express.Acceptor = "111";
            //Express.AcceptorMpno = "13510142807";
            //Express.AcceptorAddress = "江西省萍乡市莲花县111";
            //Express.OriginalPost = "755";
            //Express.Destination = "799";
            //Express.GoodsInfo = "1";
            //Express.AcceptType = "";
            //Express.SendType = "";
            //Express.GoodsCount = "1";
            //Express.GoodsWeight = "11";
            //Express.PayType = "寄付";
            //Express.CardNum = "7552326436";
            //Express.SenderSign = "张三";
            //Express.Courier = "415308";
            //Express.Service = "";
            //Express.SendDate = "2014-04-30";
            //Express.CSRemark = "";




            //datatable.Columns.Add("No", typeof(string));
            //datatable.Columns.Add("Sender", typeof(string));
            //datatable.Columns.Add("SenderMpno", typeof(string));
            //datatable.Columns.Add("SenderAddress", typeof(string));
            //datatable.Columns.Add("Acceptor", typeof(string));
            //datatable.Columns.Add("AcceptorMpno", typeof(string));
            //datatable.Columns.Add("AcceptorAddress", typeof(string));
            //datatable.Columns.Add("OriginalPost", typeof(string));
            //datatable.Columns.Add("Destination", typeof(string));
            //datatable.Columns.Add("GoodsInfo", typeof(string));
            //datatable.Columns.Add("AcceptType", typeof(string));
            //datatable.Columns.Add("SendType", typeof(string));
            //datatable.Columns.Add("GoodsCount", typeof(string));
            //datatable.Columns.Add("GoodsWeight", typeof(string));
            //datatable.Columns.Add("PayType", typeof(string));
            //datatable.Columns.Add("CardNum", typeof(string));
            //datatable.Columns.Add("SenderSign", typeof(string));
            //datatable.Columns.Add("Courier", typeof(string));
            //datatable.Columns.Add("Service", typeof(string));
            //datatable.Columns.Add("SendDate", typeof(string));
            //datatable.Columns.Add("CSRemark", typeof(string));

            // //Add one rows
            //datatable.Rows.Add(new object[] { 
            //Express.No,
            //Express.Sender,
            //Express.SenderMpno,
            //Express.SenderAddress,
            //Express.Acceptor,
            //Express.AcceptorMpno,
            //Express.AcceptorAddress,
            //Express.OriginalPost,
            //Express.Destination,
            //Express.GoodsInfo,
            //Express.AcceptType,
            //Express.SendType,
            //Express.GoodsCount,
            //Express.GoodsWeight,
            //Express.PayType,
            //Express.CardNum,
            //Express.SenderSign,
            //Express.Courier,
            //Express.Service,
            //Express.SendDate,
            //Express.CSRemark
            //});

            //datatable.AcceptChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            report.ShowReport();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            report.PrintOptions.Printer = Common.ConstConfig.SFPrintName;
            report.PrintOptions.ShowDialog = false;


            report.PrepareReport(false);
            report.PrintReport();
        }
    }
}
