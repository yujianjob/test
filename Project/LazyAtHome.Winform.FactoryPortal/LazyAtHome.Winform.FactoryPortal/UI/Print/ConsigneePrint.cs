using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyAtHome.Winform.FactoryPortal.WashOrderService;
using FastReport;
using LazyAtHome.Winform.FactoryPortal.UI.WareHouse;

namespace LazyAtHome.Winform.FactoryPortal.UI.Print
{
    public partial class ConsigneePrint : Form
    {

        //业务逻辑
        private Business.WashOrder business = null;

        //订单实体
        Order_OrderDC entity = null;

        public ConsigneePrint()
        {
            InitializeComponent();

            if (business == null)
            {
                business = new Business.WashOrder();
            }
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsigneePrint_Load(object sender, EventArgs e)
        {
            Init();
        }


        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.btnPrint.Enabled = false;
            
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtOrderNumber.Text))
            {
                //提示
                MessageBox.Show("请填写订单号");
                return;
            }
            GetData();

        }

        /// <summary>
        /// 订单号按下回车 获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrderNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                //按下回车键后
                GetData();

                //this.txtOrderNumber.SelectAll();
            }
        }

        private void GetData()
        {
            if (string.IsNullOrEmpty(this.txtOrderNumber.Text))
            {
                return;
            }
            entity = business.GetOrderByOrderNumber(this.txtOrderNumber.Text);

            if (entity != null)
            {
                //信息赋值
                if (entity.GetAddress != null)
                {
                    this.btnPrint.Enabled = true;

                    this.txtSendExpressNo.Text = entity.GetExpressNumber;

                    this.txtSendMan.Text = entity.GetAddress.Consignee;
                    this.txtSendPhone.Text = entity.GetAddress.Mpno;
                    this.txtSendAddress.Text = entity.GetAddress.Address;
                    this.txtRemark.Text = entity.UserRemark;

                }
                else
                {
                    this.btnPrint.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("未找到该订单，请确认订单号是否正确！");

                //清空

                this.txtSendExpressNo.Text = string.Empty;
                this.txtSendMan.Text = string.Empty;
                this.txtSendPhone.Text = string.Empty;
                this.txtSendAddress.Text = string.Empty;
                this.txtRemark.Text = string.Empty;
                this.btnPrint.Enabled = false;
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            if (entity != null)
            {
                if (string.IsNullOrEmpty(entity.GetExpressNumber))
                {
                    //收衣单号为空 生成一个
                    entity.GetExpressNumber = System.DateTime.Now.ToString("yyMMddHHmmss");

                    this.txtSendExpressNo.Text = entity.GetExpressNumber;

                    //写入收衣单号
                    if (!business.OrderExpressFinish(entity.ID, entity.GetExpressNumber))
                    {
                        MessageBox.Show("收衣单号写入发生错误，请重新打印！", "错误");
                        return;
                    }
                    
                }

                try
                {
                    Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误");
                    return;
                }

            }
            else
            {
                MessageBox.Show("请先根据订单号搜索");
                return;
            }


            //if (Print())
            //{
            //    //打印成功

            //    //推送订单系统 写入收衣单号
            //    if (!true)
            //    {
            //        MessageBox.Show("收衣单号写入发生错误，请重新打印！", "错误");
            //        return;
            //    }
            //}
        }


        /// <summary>
        /// 打印收衣面单
        /// </summary>
        private void Print()
        {

            TfrxReportClass report;
            FrxDataTable datatable;

            // Create report object
            report = new TfrxReportClass();

            // Create the FR compatible DataTable object
            datatable = new FrxDataTable("Express");

            FillExpressData(datatable);

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\Express.fr3";

            // Load demmonstration report from file
            report.LoadReportFromFile(FilePath);
            //report.LoadReportFromFile(@"D:\SF.fr3");

            report.ClearDatasets();
            // Asiign datasets to report one more time
            // beacuse theLoadReport... family functions breaks links between report and dataset
            datatable.AssignToReport(true, report);

            // Assigns DataTable to DataBand
            datatable.AssignToDataBand("MasterData1", report);

            report.PrintOptions.Printer = Common.ConstConfig.PrintName;
            report.PrintOptions.ShowDialog = false;

            //TfrxPrintOptions op = new TfrxPrintOptions();


            report.PrepareReport(false);
            report.PrintReport();


        }


        /// <summary>
        /// 打印收衣面单 数据填充
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        private void FillExpressData(FrxDataTable datatable)
        {

            //datatable.Columns.Add("打印顺序", typeof(string));

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

            //datatable.Columns.Add("收件员", typeof(string));
            datatable.Columns.Add("货运单号", typeof(string));
            //datatable.Columns.Add("付款方式", typeof(string));
            //datatable.Columns.Add("月结卡号", typeof(string));
            //datatable.Columns.Add("货品信息", typeof(string));

            datatable.Columns.Add("业务类型", typeof(string));
            datatable.Columns.Add("订单号", typeof(string));

            //datatable.Columns.Add("代收货款", typeof(string));
            //datatable.Columns.Add("代收货款金额", typeof(string));

            datatable.Columns.Add("应收", typeof(string));
            datatable.Columns.Add("备注", typeof(string));



            datatable.Rows.Add(new object[] { 
                //"1/1",
                this.txtSendMan.Text,//s.Consignee,
                this.txtSendPhone.Text,//s.Mpno,
                this.txtSendAddress.Text,//s.Address,//s.DistrictName + s.Address,
                "",//邮编
                "021",//sforder.destcode,

                Common.ConstConfig.S_Company,
                Common.ConstConfig.S_Tel,
                "",//不需要写懒到家的地址 Common.ConstConfig.S_Address,
                "",//邮编
                "021",//sforder.origincode,

                //"",//收件员
                this.txtSendExpressNo.Text,//ExpressNum,//自定义快递单号 sforder.mailno,
                //"第三方付",//"寄付月结",//付款方式
                //Common.ConstConfig.SFMonthCardNo,//"12345678",//月结卡号
                //"衣物",
                "普通件",//业务类型
                entity.OrderNumber,
                //entity.OrderClass == 2 ? "COD" : "",
                //entity.OrderClass == 2 ? "代收货款金额：￥" + entity.TotalAmount.ToString() + "元      " +  Common.ConstConfig.SFMonthCardNo: "",

                "",//entity.TotalAmount - entity.PayAmount == 0 ? "" : "应收：￥" + (entity.TotalAmount - entity.PayAmount).ToString("0.00"),
                this.txtRemark.Text
                });

            datatable.AcceptChanges();
        }



        


    }
}
