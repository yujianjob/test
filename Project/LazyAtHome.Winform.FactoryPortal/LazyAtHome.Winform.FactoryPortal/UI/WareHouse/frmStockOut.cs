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
using LazyAtHome.Winform.FactoryPortal.InternalExpressService;
using LazyAtHome.Winform.FactoryPortal.Model;
using FastReport;
using UDF.GUI.PrintControl;


namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    public partial class frmStockOut : Form
    {
        //业务逻辑
        private Business.WashOrder business = null;

        //订单实体
        Order_OrderDC entity = null;
        //洗涤条码对应的产品
        Order_ProductDC currProduct = null;
        //送件订单实体
        Exp_OrderDC expOrder = null;

        //顺丰快递单号
        string SFExpressNum = string.Empty;

        //送衣单号 上面顺丰快递单号弃用
        //string ExpressNum = string.Empty;

        public frmStockOut()
        {
            InitializeComponent();
            if (business == null)
            {
                business = new Business.WashOrder();
            }
        }


        private void DataClear()
        {

            entity = null;
            currProduct = null;
            expOrder = null;

            this.txtCode.Text = string.Empty;
            this.txtConsignee.Text = string.Empty;
            this.txtMPNo.Text = string.Empty;
            this.txtAddress.Text = string.Empty;

            this.txtAttribute.Text = string.Empty;

            this.dgvOrderProduct.DataSource = null;
            this.tsbPrint.Enabled = false;

            this.tsbNotifyItemBad.Enabled = false;
        }

        private void Init()
        {
            this.tsbPrint.Enabled = false;

            this.tsbNotifyItemBad.Enabled = false;
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStockOut_Load(object sender, EventArgs e)
        {
            Init();

            //this.txtExpressNum.Text = "201405191906500535";
            //this.GetData();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetData()
        {
            //entity = business.GetOrder(this.txtExpressNum.Text);
            entity = business.GetOrderByFactoryCode(this.txtCode.Text);
            //GetOrderByFactoryCode
            if (entity != null && entity.ProductList != null)
            {
                Order_ProductDC[] pList = new Order_ProductDC[1];

                //循环ProductList，算出序号
                int i = 1;
                foreach (var item in entity.ProductList)
                {
                    //item.Attribute = i.ToString("00");
                    item.Index = i;

                    i++;

                    if (item.Code == this.txtCode.Text)
                    {
                        currProduct = item;
                        pList[0] = item;
                        //break;
                    }
                }
                
                this.dgvOrderProduct.DataSource = entity.ProductList.ToArray();
                //this.dgvOrderProduct.DataSource = pList.ToArray();
                dgvColumnsHeaderSet();


                this.tsbPrint.Enabled = true;
                //查到订单 异件上报 按钮亮起 可供上报
                this.tsbNotifyItemBad.Enabled = true;

                if (entity.OrderClass == 1 || entity.OrderClass == 2)
                {
                    //this.tsbPrint.Text = "打印顺丰面单";
                    this.tsbPrint.Text = "打印面单";
                }
                else if (entity.OrderClass == 3)
                {
                    this.tsbPrint.Text = "打印全峰快递单";

                    //暂时没有OrderClass =3 的全峰快递单
                    this.tsbPrint.Enabled = false;
                }




                if (entity.SendAddress != null)
                {
                    this.txtConsignee.Text = entity.SendAddress.Consignee;
                    this.txtMPNo.Text = entity.SendAddress.Mpno;
                    this.txtAddress.Text = entity.SendAddress.Address;
                }


                this.txtAttribute.Text = currProduct.Attribute;
                
            }
            else
            {
                //MessageBox.Show("未查到订单信息，请确认洗涤条码是否正确");
                //return;
                if (MessageBox.Show("该水洗条码无效，是否需要上报异常？建议点击按钮“是”进行上报！", "工厂管理系统", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                {
                    //处理上报流程
                    if (!business.OrderOutFactory_FailNotify_NotFound(this.txtCode.Text, Common.ConstConfig.currOperator.ID))
                    {
                        MessageBox.Show("上报异常发生错误，请联系管理员！", "工厂管理系统", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("上报成功，请将衣物保管好等待客服处理！");
                    }
                }


                DataClear();
            }
        }



        private void dgvColumnsHeaderSet()
        {
            foreach (DataGridViewColumn item in this.dgvOrderProduct.Columns)
            {
                item.Visible = false;
            }

            int index = 0;
            //this.dgvOrderProduct.Columns["ID"].Visible = true;
            //this.dgvOrderProduct.Columns["ID"].HeaderText = "编号";
            //this.dgvOrderProduct.Columns["ID"].DisplayIndex = index++;

            //this.dgvOrderProduct.Columns["Attribute"].Visible = true;
            //this.dgvOrderProduct.Columns["Attribute"].HeaderText = "编号";
            //this.dgvOrderProduct.Columns["Attribute"].DisplayIndex = index++;
            this.dgvOrderProduct.Columns["Index"].Visible = true;
            this.dgvOrderProduct.Columns["Index"].HeaderText = "编号";
            this.dgvOrderProduct.Columns["Index"].DisplayIndex = index++;

            this.dgvOrderProduct.Columns["Name"].Visible = true;
            this.dgvOrderProduct.Columns["Name"].HeaderText = "名称";
            this.dgvOrderProduct.Columns["Name"].DisplayIndex = index++;

            this.dgvOrderProduct.Columns["Price"].Visible = true;
            this.dgvOrderProduct.Columns["Price"].HeaderText = "销售价";
            this.dgvOrderProduct.Columns["Price"].DisplayIndex = index++;

            this.dgvOrderProduct.Columns["Code"].Visible = true;
            this.dgvOrderProduct.Columns["Code"].HeaderText = "洗涤条码";
            this.dgvOrderProduct.Columns["Code"].DisplayIndex = index++;


            this.dgvOrderProduct.Columns["Step"].Visible = true;
            this.dgvOrderProduct.Columns["Step"].HeaderText = "进程";
            this.dgvOrderProduct.Columns["Step"].DisplayIndex = index++;

            //this.dgvOrderProduct.Columns["ClassName"].Visible = true;
            //this.dgvOrderProduct.Columns["ClassName"].HeaderText = "类别";
            //this.dgvOrderProduct.Columns["ClassName"].DisplayIndex = index++;

            //this.dgvOrderProduct.Columns["Content"].Visible = true;
            //this.dgvOrderProduct.Columns["Content"].HeaderText = "描述";
            //this.dgvOrderProduct.Columns["Content"].DisplayIndex = index++;


        }


        /// <summary>
        /// 打印快递单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPrint_Click(object sender, EventArgs e)
        {

            #region 打印面单V2

            //获取送件信息
            if (entity != null)
            {
                if (entity.OrderClass == 1 || entity.OrderClass == 2)
                {
                    //普通下单和一键下单的 打印送衣面单 写入送衣单号
                    expOrder = business.ExpOrderCreateSend(entity.OrderNumber);

                    if (expOrder != null)
                    {
                        //InternalPrint();

                        //进行出库操作
                        //2014-11-27 两步操作合为一步
                        int pIndex = business.OrderProductOutFactory(entity.ID, currProduct.ID, expOrder.ExpNumber, Common.ConstConfig.currOperator.ID);
                        if (pIndex == -1)
                        {
                            MessageBox.Show("出库发生错误，请重新打印！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            InternalPrint(pIndex == 1 ? true : false);
                        }

                    }
                    else
                    {
                        MessageBox.Show("获取送件信息发生错误！","错误");
                        return;
                    }

                    ////更新衣物步骤 5:已出库(打完面单)
                    //if (!business.OrderProductStepUPDATE(entity.ID, currProduct.ID, 5))
                    //{
                    //    MessageBox.Show("更新衣物步骤发生错误，请重新打印！", "错误");
                    //    return;
                    //}

                    ////推送订单系统
                    //if (string.IsNullOrEmpty(entity.SendExpressNumber))
                    //{
                    //    if (!business.OrderExpressADD(entity.ID, expOrder.ExpNumber, expOrder.ExpNumber, Common.ConstConfig.currOperator.ID))
                    //    {
                    //        MessageBox.Show("送衣单号写入发生错误，请重新打印！", "错误");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        entity.SendExpressNumber = expOrder.ExpNumber;
                    //    }
                    //}

                    

                    DataClear();

                    this.txtCode.Focus();
                    this.txtCode.SelectAll();


                    //this.txtConsignee.Text = string.Empty;
                    //this.txtMPNo.Text = string.Empty;
                    //this.txtAddress.Text = string.Empty;

                    //else
                    //{
                    //    if (!business.OrderExpressUpdate(entity.ID, expOrder.ExpNumber, expOrder.ExpNumber, Common.ConstConfig.currOperator.ID))
                    //    {
                    //        MessageBox.Show("送衣单号写入发生错误，请重新打印！", "错误");
                    //        return;
                    //    }
                    //}

                }
            }
            else
            {
                MessageBox.Show("请先扫描洗涤条码获取订单信息！", "提示");
            }
            #endregion


            //#region 打印面单V1
            //if (entity != null)
            //{
            //    if (entity.OrderClass == 1 || entity.OrderClass == 2)
            //    {
            //        //普通下单和一键下单的 写入送衣单号 打印送衣面单

            //        if (string.IsNullOrEmpty(entity.SendExpressNumber))
            //        {
            //            //生成送衣单号
            //            entity.SendExpressNumber = System.DateTime.Now.ToString("yyMMddHHmmss");

            //            //写入
            //            if (!business.OrderExpressADD(entity.ID, entity.SendExpressNumber, entity.SendExpressNumber, Common.ConstConfig.currOperator.ID))
            //            {
            //                MessageBox.Show("送衣单号写入发生错误，请重新打印！", "错误");
            //                return;
            //            }
                        
            //        }

            //        //打印
            //        try
            //        {
            //            Print();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "错误");
            //            return;
            //        }



            //        //下单并打印
            //        //if (SFPrint())
            //        //{
            //            //推送订单系统
            //            //if (string.IsNullOrEmpty(entity.SendExpressNumber))
            //            //{
            //            //    if (!business.OrderExpressADD(entity.ID, ExpressNum, ExpressNum, Common.ConstConfig.currOperator.ID))
            //            //    {
            //            //        MessageBox.Show("送衣单号写入发生错误，请重新打印！", "错误");
            //            //        return;
            //            //    }
            //            //}
            //            //else
            //            //{
            //            //    if (!business.OrderExpressUpdate(entity.ID, ExpressNum, ExpressNum, Common.ConstConfig.currOperator.ID))
            //            //    {
            //            //        MessageBox.Show("送衣单号写入发生错误，请重新打印！", "错误");
            //            //        return;
            //            //    }
            //            //}



            //            //推送订单系统
            //            //if (entity.ExpressList.Count(p => p.Type == 2) == 0)
            //            //{
            //                //if (!business.OrderExpressADD(entity.ID, SFExpressNum, SFExpressNum + Common.ConstConfig.SFOrderPostfix, Common.ConstConfig.currOperator.ID))
            //                //{
            //                //    MessageBox.Show("顺丰物流单号写入发生错误，请重新打印！", "错误");
            //                //}
            //                //else
            //                //{ 
            //                //    //成功后
            //                //    this.tsbPrint.Visible = false;
            //                //}
            //            //}
            //            //else
            //            //{
                            
            //                //if (!business.OrderExpressUpdate(entity.ID, SFExpressNum, SFExpressNum + Common.ConstConfig.SFOrderPostfix, Common.ConstConfig.currOperator.ID))
            //                //{
            //                //    MessageBox.Show("顺丰物流单号写入发生错误，请重新打印！", "错误");
            //                //}
            //                //else
            //                //{
            //                //    //成功后
            //                //    this.tsbPrint.Visible = false;
            //                //}
            //            //}


            //        //}

                    

            //    }
            //    //if (entity.OrderClass == 3)
            //    //{
            //    //    //合作方订单
            //    //    bool tmp = false;
            //    //    try
            //    //    {
            //    //        tmp = QFPrint();
            //    //    }
            //    //    catch (Exception ex)
            //    //    {
            //    //        MessageBox.Show(ex.Message);
            //    //    }
            //    //    if (tmp)
            //    //    {
            //    //        frmQFOrderNumInput frm = new frmQFOrderNumInput();
            //    //        frm.OrderNumber = entity.OrderNumber;
            //    //        if (frm.ShowDialog() == DialogResult.OK)
            //    //        {
            //    //            this.txtExpressNum.Text = string.Empty;
            //    //            this.dgvOrderProduct.DataSource = null;
            //    //        }
            //    //    }

            //    //    //打印完成 引导操作员写入物流单号
            //    //}
            //}
            //else
            //{ 
            //    //没获取到数据 处理报错信息
            //}

            //#endregion

        }


        #region 自主打印

        /// <summary>
        /// 自定义打印
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

            report.PrepareReport(false);
            report.PrintReport();
        }


        /// <summary>
        /// 打印送衣面单 数据填充
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        private void FillExpressData(FrxDataTable datatable)
        {

            //收件人信息
            Order_ConsigneeAddressDC s = null;

            if (entity.SendAddress != null)
            {
                s = entity.SendAddress;
            }
            else
            {
                throw new Exception("未获取到送货地址！");
                //rtn = false;
                //MessageBox.Show("未获取到送货地址！", "错误");
            }


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
                Common.ConstConfig.S_Company,
                Common.ConstConfig.S_Tel,
                "",//不需要写懒到家的地址 Common.ConstConfig.S_Address,
                "",//邮编
                "021",//sforder.origincode,
                s.Consignee,
                s.Mpno,
                s.Address,//s.DistrictName + s.Address,
                "",//邮编
                "021",//sforder.destcode,
                //"",//收件员
                entity.SendExpressNumber,//ExpressNum,//自定义快递单号 sforder.mailno,
                //"第三方付",//"寄付月结",//付款方式
                //Common.ConstConfig.SFMonthCardNo,//"12345678",//月结卡号
                //"衣物",
                "普通件",//业务类型
                entity.OrderNumber,
                //entity.OrderClass == 2 ? "COD" : "",
                //entity.OrderClass == 2 ? "代收货款金额：￥" + entity.TotalAmount.ToString() + "元      " +  Common.ConstConfig.SFMonthCardNo: "",

                entity.TotalAmount - entity.PayAmount == 0 ? "" : "应收：￥" + (entity.TotalAmount - entity.PayAmount).ToString("0.00"),
                this.txtPrintRemark.Text
                });

            datatable.AcceptChanges();



        }


        /// <summary>
        /// 自主打印
        /// </summary>
        private void InternalPrint(bool isLast)
        {
            TfrxReportClass report;
            FrxDataTable datatable;

            // Create report object
            report = new TfrxReportClass();

            // Create the FR compatible DataTable object
            datatable = new FrxDataTable("Express");

            FillExpressDataV2(datatable, isLast);
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\Express_V3.fr3";

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

            report.PrepareReport(false);
            report.PrintReport();
        }

        private void FillExpressDataV2(FrxDataTable datatable, bool isLast)
        {
            datatable.Columns.Add("站点名称", typeof(string));
            datatable.Columns.Add("站点地址", typeof(string));
            datatable.Columns.Add("站点编号", typeof(string));

            datatable.Columns.Add("收件人", typeof(string));
            datatable.Columns.Add("收件电话", typeof(string));
            datatable.Columns.Add("收件地址", typeof(string));
            datatable.Columns.Add("收件邮编", typeof(string));
            //datatable.Columns.Add("目的地", typeof(string));

            datatable.Columns.Add("货运单号", typeof(string));
            datatable.Columns.Add("业务类型", typeof(string));
            //datatable.Columns.Add("订单号", typeof(string));
            datatable.Columns.Add("应收", typeof(string));
            datatable.Columns.Add("备注", typeof(string));

            datatable.Columns.Add("序号", typeof(string));
            datatable.Columns.Add("打印时间", typeof(string));
            datatable.Columns.Add("订单详情", typeof(string));

            //add by yfyang 2014-11-27 V3版面单增加
            datatable.Columns.Add("干线编号", typeof(string));
            datatable.Columns.Add("货件号", typeof(string));
            datatable.Columns.Add("派件员", typeof(string));
            datatable.Columns.Add("出库状态", typeof(string));

            string OutType = string.Empty;
            //判断出库状态  entity.SendType 2：分单 1：整单
            if (entity.SendType != 2 && entity.ProductList.Count() > 1)
            {
                //需要打印出库状态
                //判断是否是最后一件
                if (isLast)
                {
                    OutType = "整合出库";
                }
                else
                {
                    OutType = "等待出库";
                }
                
            }


            string proTemp = string.Empty;
            foreach (var item in entity.ProductList)
            {
                if (currProduct.Name != item.Name)
                {
                    proTemp += item.Name + " ";
                }
                
            }

            datatable.Rows.Add(new object[] { 
                expOrder.NodeName,
                expOrder.NodeAddress,
                expOrder.NodeNo,
                expOrder.Contacts,
                expOrder.Mpno,
                expOrder.Address,
                "",//邮编
                expOrder.ExpNumber,
                expOrder.QuickType == 0 ? "普通件" : "加急件",//业务类型
                isLast ? "应收：￥" + (entity.TotalAmount - entity.PayAmount).ToString("0.00") : "应收：￥0.00",//+ " (共" + entity.ProductList.Count().ToString() + "件)",
                //"应收：￥" + expOrder.ChargeFee.ToString("0.00") + " (共" + entity.ProductList.Count().ToString() + "件)",
                entity.PrintRemark,//备注
                currProduct.Index.ToString("00"),//entity.ProductList.Count().ToString() + "-" + currProduct.Index.ToString("00"),
                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                currProduct.Name + " (订单共" + entity.ProductList.Count().ToString() + "件)",// + "  其他衣物：( " + proTemp + ")",//expOrder.PackageInfo
                expOrder.LineNo,
                currProduct.Code,
                expOrder.OperatorName,
                OutType
                });

            datatable.AcceptChanges();
        }

        #endregion

        #region 全峰打印

        /// <summary>
        /// 全峰打印快递单
        /// </summary>
        private bool QFPrint()
        {
            return true;

            //收件人信息
            Order_ConsigneeAddressDC s = null;

            if (entity.SendAddress != null)
            {
                //s = entity.ConsigneeAddressList.FirstOrDefault(p => p.Type == 2);
                s = entity.SendAddress;
            }

            FormatPrint myFormatPrint = new FormatPrint();

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\QF.xml";
            myFormatPrint.loadPrintTemplate(FilePath);


            myFormatPrint.PrintPageSettings.Landscape = true;
            myFormatPrint.PrinterSettings.PrinterName = Common.ConstConfig.QFPrintName;
            

            myFormatPrint.SetValue("SCompany", Common.ConstConfig.S_Company);
            myFormatPrint.SetValue("SContact", Common.ConstConfig.S_Contact);
            myFormatPrint.SetValue("SPhone", Common.ConstConfig.S_Tel);
            myFormatPrint.SetValue("SAddress", Common.ConstConfig.S_Address);
            myFormatPrint.SetValue("RCompany", s.Consignee);
            myFormatPrint.SetValue("RContact", s.Consignee);
            myFormatPrint.SetValue("RPhone", s.Mpno);
            myFormatPrint.SetValue("RAddress", s.DistrictName + s.Address);


            myFormatPrint.Print();

            return true;
        }

        #endregion

        #region 顺丰打印

        

        /// <summary>
        /// 顺丰打印面单
        /// </summary>
        private bool SFPrint()
        {

            TfrxReportClass report;
            FrxDataTable datatable;

            // Create report object
            report = new TfrxReportClass();

            // Create the FR compatible DataTable object
            datatable = new FrxDataTable("Express");

            FillTableWithExpressData(datatable);
            if (!FillTableWithExpressData(datatable))
            {
                return false;
            }


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

            report.PrintOptions.Printer = Common.ConstConfig.SFPrintName;
            report.PrintOptions.ShowDialog = false;
            
            //TfrxPrintOptions op = new TfrxPrintOptions();


            report.PrepareReport(false);
            report.PrintReport();

            

            return true;
        }


        /// <summary>
        /// 打印快递单 数据填充
        /// </summary>
        /// <param name="datatable"></param>
        private bool FillTableWithExpressData(FrxDataTable datatable)
        {
            bool rtn = true;

            //顺丰订单对象
            SFExpressService.OrderDC sforder = null;

            //顺丰面单填充对象
            SFExpress Express = null;

            //收件人信息
            Order_ConsigneeAddressDC s = null;


            if (entity.SendAddress != null)
            {
                //获取收件人信息
                //s = entity.ConsigneeAddressList.FirstOrDefault(p => p.Type == 2);
                s = entity.SendAddress;

                if (entity.ExpressList == null || entity.ExpressList.Count(p => p.Type == 2) == 0)
                {
                    //是新订单的话 根据订单中的发货信息 调用顺丰下单

                    //先调用快递接口下单 并获取下单信息 若已经下过单会返回下过单的信息
                    decimal? pay = null;
                    //pay = entity.OrderClass == 2 ? entity.PayAmount : null
                    if (entity.OrderClass == 2)
                    {
                        pay = entity.TotalAmount;
                    }
                    sforder = business.SFExpressOrderCreate(entity.OrderNumber + Common.ConstConfig.SFOrderPostfix, s.Consignee, s.Consignee, s.Mpno, s.Address, s.CityName, s.ProvinceName, pay);

                    if (sforder == null)
                    {
                        rtn = false;
                        MessageBox.Show("顺丰下单发生错误，" + business.LastError, "错误");
                    }
                    else
                    {
                        //对顺丰快递单号全局变量赋值
                        SFExpressNum = sforder.mailno;
                    }
                }
                else
                {
                    //已经生成订单的话 调用顺丰查询订单

                    //这里传的orderid需要确认
                    sforder = business.SFExpressOrderSearch(entity.OrderNumber + Common.ConstConfig.SFOrderPostfix);
                    if (sforder == null)
                    {
                        rtn = false;
                        MessageBox.Show("顺丰查单发生错误，" + business.LastError, "错误");
                    }
                    else
                    {
                        //对顺丰快递单号全局变量赋值
                        SFExpressNum = sforder.mailno;
                    }

                }
            }
            else
            {
                rtn = false;
                MessageBox.Show("未获取到送货地址！", "错误");
            }

            if (rtn)
            {
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
                "1/1",
                Common.ConstConfig.S_Company,
                Common.ConstConfig.S_Tel,
                Common.ConstConfig.S_Address,
                "",//邮编
                sforder.origincode,
                s.Consignee,
                s.Mpno,
                s.Address,//s.DistrictName + s.Address,
                "",//邮编
                sforder.destcode,
                "",//收件员
                sforder.mailno,
                "第三方付",//"寄付月结",//付款方式
                Common.ConstConfig.SFMonthCardNo,//"12345678",//月结卡号
                "衣物",
                "电商特惠",//业务类型
                entity.OrderNumber,
                entity.OrderClass == 2 ? "COD" : "",
                entity.OrderClass == 2 ? "代收货款金额：￥" + entity.TotalAmount.ToString() + "元      " +  Common.ConstConfig.SFMonthCardNo: "",

                });


                //Express = new SFExpress();
                //Express.No = sforder.mailno;//"966999986668";//sforder.mailno
                //Express.Sender = Common.ConstConfig.S_Company + " ";
                //Express.SenderMpno = Common.ConstConfig.S_Tel + " ";//"13012345678";
                //Express.SenderAddress = Common.ConstConfig.S_Address + " ";//"上海市XXX洗衣工厂";

                //Express.Acceptor = s.Consignee;//"111";
                //Express.AcceptorMpno = s.Mpno;//"13510142807";
                //Express.AcceptorAddress = s.DistrictName + s.Address;//"江西省萍乡市莲花县111";
                //Express.OriginalPost = sforder.origincode;// "755";//sforder.origincode
                //Express.Destination = sforder.destcode;// "799";//sforder.destcode
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

                //// Add one rows
                //datatable.Rows.Add(new object[] { 
                //        Express.No,
                //        Express.Sender,
                //        Express.SenderMpno,
                //        Express.SenderAddress,
                //        Express.Acceptor,
                //        Express.AcceptorMpno,
                //        Express.AcceptorAddress,
                //        Express.OriginalPost,
                //        Express.Destination,
                //        Express.GoodsInfo,
                //        Express.AcceptType,
                //        Express.SendType,
                //        Express.GoodsCount,
                //        Express.GoodsWeight,
                //        Express.PayType,
                //        Express.CardNum,
                //        Express.SenderSign,
                //        Express.Courier,
                //        Express.Service,
                //        Express.SendDate,
                //        Express.CSRemark
                //        });

                datatable.AcceptChanges();
            }
            return rtn;
        }

        #endregion

        /// <summary>
        /// 物流单号文本框按下某个按键并释放后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExpressNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                //按下回车键后
                GetData();

                this.txtExpressNum.SelectAll();
            }
        }

        /// <summary>
        /// 洗涤条码文本框按下某个按键并释放后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                //按下回车键后
                if (string.IsNullOrEmpty(this.txtCode.Text))
                {
                    return;
                }

                GetData();

                this.txtCode.SelectAll();
            }
        }

        /// <summary>
        /// 洗涤条码文本框设为活动控件后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_Enter(object sender, EventArgs e)
        {
            this.txtCode.SelectAll();
        }
        /// <summary>
        /// 洗涤条码文本框设为非活动控件后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_Leave(object sender, EventArgs e)
        {
            this.txtCode.Tag = null;
        }
        /// <summary>
        /// 洗涤条码文本框鼠标单击后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.txtCode.Tag == null)
            {
                this.txtCode.SelectAll();
                this.txtCode.Tag = 0;
            }
        }





        /// <summary>
        /// 列表单元格重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrderProduct_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Common.Function.DataGridViewCellPainting(sender, e);



            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (this.dgvOrderProduct["Code", e.RowIndex].Value.ToString() == this.currProduct.Code)
                {
                    //e.CellStyle.BackColor = Color.FromArgb(255, 230, 122);
                    e.CellStyle.BackColor = Color.FromArgb(180, 215, 141);
                }

            }
        }

        /// <summary>
        /// 列表单元格格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrderProduct_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (this.dgvOrderProduct.Columns[e.ColumnIndex].Name == "Step")
            {
                string rtn;
                switch ((int)e.Value)
                {
                    case 0:
                        rtn = "";
                        break;
                    case 5:
                        rtn = "已出库";
                        break;

                    default:
                        rtn = "未知：" + ((int)e.Value).ToString();
                        break;
                }

                e.Value = rtn;
            }
        }


        /// <summary>
        /// 异件上报 物品错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNotifyItemBad_Click(object sender, EventArgs e)
        {
            if (entity == null)
            {
                MessageBox.Show("请先扫描物水洗码！");
                return;
            }

            frmStockOutNotifyItemBad frm = new frmStockOutNotifyItemBad();
            frm.txtOrderNumber.Text = entity.OrderNumber;
            frm.txtRemark.Text = "水洗条码：" + currProduct.Code;
            frm.ShowDialog();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{

            //}

            DataClear();
        }

        

        

        
    }
}
