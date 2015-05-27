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
using LazyAtHome.Winform.FactoryPortal.WashProductService;
using FastReport;

namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    public partial class frmStockIn : Form
    {
        //业务逻辑
        private Business.WashProduct p_business = null;
        private Business.WashOrder o_business = null;

        //运价选择界面
        frmProductSelect frmProSelect = null;

        //订单对象
        Order_OrderDC entity = null;


        //类别集合
        IList<Wash_ClassDC> wcList = null;
        //运价集合
        IList<Wash_ProductDC> wpList = null;

        //已选运价集合
        //IList<Wash_ProductDC> selectedList = null;

        IList<Order_ProductDC> OrderProductList = null;

        //订单衣物临时记数
        int OrderProductIndex = 1;

        //洗涤条码临时记数
        int CodeBarIndex = 1;

        public frmStockIn()
        {
            InitializeComponent();

            if (o_business == null)
            {
                o_business = new Business.WashOrder();
            }
            if (p_business == null)
            {
                p_business = new Business.WashProduct();
            }
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStockIn_Load(object sender, EventArgs e)
        {
            //this.txtExpressNum.Text = "201405191906500535";

            Init();

            //GetData();
        }


        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            wcList = p_business.GetWashClassList();
            wpList = p_business.GetWashProductList();

            if (wcList == null || wpList == null)
            {
                MessageBox.Show("获取数据发生错误！", "错误");
            }


            //隐藏掉 添加衣物按钮
            //this.tsbAddProduct.Visible = false;

            this.lblOrderMessage.Text = string.Empty;

            this.tsbAddProductOrder.Enabled = false;
            this.tsbAddProduct.Enabled = false;

            this.tsbNotifyItemError.Enabled = false;
        }

        /// <summary>
        /// 界面数据清除
        /// </summary>
        private void DataClear()
        {
            this.tsbAddProductOrder.Enabled = false;
            this.tsbAddProduct.Enabled = false;

            this.tsbNotifyItemError.Enabled = false;

            this.txtExpressNum.Text = string.Empty;
            this.txtOrderNum.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.txtUserRemark.Text = string.Empty;

            this.lblOrderMessage.Text = string.Empty;

            this.txtConsignee.Text = string.Empty;
            this.txtMPNo.Text = string.Empty;
            this.txtAddress.Text = string.Empty;

            //this.selectedList = null;
            this.dgvProduct.DataSource = null;
            this.dgvProduct.Refresh();
            this.entity = null;

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetData()
        {
            entity = o_business.GetOrder(this.txtExpressNum.Text);

            if (entity != null)
            {
                if (entity.OrderClass == 2)
                {
                    //是一键下单
                    if (entity.ProductList != null  && entity.ProductList.Count() > 0)
                    {
                        //说明已经添加过衣物了
                        this.tsbAddProduct.Enabled = false;
                        this.tsbAddProductOrder.Enabled = false;

                        this.lblOrderMessage.Text = "该订单为微信一键下单，并已经添加过衣物了，如需要补添加过衣物，请联系管理员！";

                    }
                    else
                    {
                        this.tsbAddProduct.Enabled = true;
                        this.tsbAddProductOrder.Enabled = true;
                        this.lblOrderMessage.Text = "该订单为微信一键下单，请根据包裹添加衣物，然后再关联洗涤条码！";
                    }
                    

                    
                }
                else
                {
                    //是普通下单
                    this.tsbAddProduct.Enabled = false;
                    this.tsbAddProductOrder.Enabled = true;

                    this.lblOrderMessage.Text = "该订单为普通订单，请关联洗涤条码！";
                }

                if (entity.ProductList != null)
                {
                    //把衣物列表赋值给
                    OrderProductList = new List<Order_ProductDC>();

                    foreach (Order_ProductDC item in entity.ProductList)
                    {
                        OrderProductList.Add(item);
                    }

                    //绑定
                    this.dgvProduct.DataSource = OrderProductList.ToArray();
                    this.dgvProduct.Refresh();
                    dgvColumnsHeaderSet();
                }


                this.txtOrderNum.Text = entity.OrderNumber;
                this.txtRemark.Text = entity.CSRemark;
                this.txtUserRemark.Text = entity.UserRemark;

                if (entity.GetAddress != null)
                {
                    this.txtConsignee.Text = entity.GetAddress.Consignee;
                    this.txtMPNo.Text = entity.GetAddress.Mpno;
                    this.txtAddress.Text = entity.GetAddress.Address;
                }


                //查到订单 异件上报 按钮亮起 可供上报
                this.tsbNotifyItemError.Enabled = true;
                
            }
            else
            {
                //不是懒到家的订单
                //MessageBox.Show("该物流单无效，请重新扫描其他物流单号！", "提示");

                if (MessageBox.Show("该物流条码无效，是否需要上报异常？建议点击按钮“是”进行上报！", "工厂管理系统", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                { 
                    //处理上报流程
                    if (!o_business.OrderInFactory_FailNotify_NotFound(this.txtExpressNum.Text, Common.ConstConfig.currOperator.ID))
                    {
                        MessageBox.Show("上报异常发生错误，请联系管理员！", "工厂管理系统", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("上报成功，请将衣物保管好等待客服处理！");
                    }
                }

                DataClear();
                //this.txtExpressNum.Text = string.Empty;
                //this.dgvProduct.DataSource = null;

                //this.txtOrderNum.Text = string.Empty;
                //this.txtRemark.Text = string.Empty;
                //this.txtUserRemark.Text = string.Empty;
                //this.lblOrderMessage.Text = string.Empty;

                //this.txtConsignee.Text = string.Empty;
                //this.txtMPNo.Text = string.Empty;
                //this.txtAddress.Text = string.Empty;


                ////未查到订单 异件上报 按钮暗下
                //this.tsbNotifyItemError.Enabled = false;




            }


            //if (entity != null)
            //{
            //    if (entity.OrderClass != 2)
            //    {
            //        //不是一键下单的
            //        MessageBox.Show("非一键下单的订单不需要再次生成订单，请重新扫描其他物流单号！", "提示");

            //        this.lblOrderMessage.Text = "非一键下单的订单不需要再次生成订单，请重新扫描其他物流单号！";
            //        this.txtExpressNum.Text = string.Empty;
            //        this.lblOrderMessage.Text = string.Empty; //"此订单非一键下单，请重新扫描其他物流单号！";
            //        return;
            //    }
            //    else
            //    {
            //        this.lblOrderMessage.Text = "此订单为一键下单，需要为此添加衣物！";
            //    }

            //    if (entity.ProductList != null && entity.ProductList.Count() > 0)
            //    {
            //        //不是一键下单的
            //        MessageBox.Show("该物流单已经生成订单，请重新扫描其他物流单号！", "提示");

            //        this.lblOrderMessage.Text = "该物流单已经生成订单，请重新扫描其他物流单号！";

            //        this.txtExpressNum.Text = string.Empty;
            //        return;
            //    }

            //    this.txtOrderNum.Text = entity.OrderNumber;
            //    this.txtRemark.Text = entity.CSRemark;
            //}
            //else
            //{ 
            //    //不是懒到家的订单
            //    MessageBox.Show("该物流单不是懒到家的订单，请重新扫描其他物流单号！", "提示");
            //    this.txtExpressNum.Text = string.Empty;
            //}
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAddProduct_Click(object sender, EventArgs e)
        {
            //先check
            if (this.entity == null)
            {
                MessageBox.Show("请重新扫描物流单号！","错误");
                return;
            }

            //实例化一个运价选择界面
            if (frmProSelect == null)
            {
                frmProSelect = new frmProductSelect();
            }
            //frmProductSelect frm = new frmProductSelect();
            //数据赋值
            frmProSelect.wcList = this.wcList;
            frmProSelect.wpList = this.wpList;
            //弹框
            if (frmProSelect.ShowDialog() == DialogResult.OK)
            {
                if (OrderProductList == null)
                {
                    OrderProductList = new List<Order_ProductDC>();
                }

                Order_ProductDC item = new Order_ProductDC();
                item.ID = OrderProductIndex++;
                item.ProductID = frmProSelect.wpSelect.ID;
                item.Name = frmProSelect.wpSelect.Name;
                item.Price = frmProSelect.wpSelect.SalesPrice;
                item.Type = 1;
                //item.CategoryName = frmProSelect.wpSelect.CategoryName;

                OrderProductList.Add(item);

                this.dgvProduct.DataSource = OrderProductList.ToArray();
                this.dgvProduct.Refresh();
                dgvColumnsHeaderSet();

                //if (selectedList == null)
                //{
                //    selectedList = new List<Wash_ProductDC>();
                //}
                //selectedList.Add(frmProSelect.wpSelect);


                //this.dgvProduct.DataSource = selectedList.ToArray();
                //this.dgvProduct.Refresh();
                //dgvColumnsHeaderSet();

                

            }

        }

        /// <summary>
        /// 添加产品到订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAddProductOrder_Click(object sender, EventArgs e)
        {
            if (entity != null)
            {

                if (this.OrderProductList != null && this.OrderProductList.Count() > 0)
                {
                    //IList<Order_ProductDC> pList = new List<Order_ProductDC>();
                    ////转化成接口需要的List
                    //foreach (Wash_ProductDC item in selectedList)
                    //{
                    //    Order_ProductDC op = new Order_ProductDC();
                    //    op.ProductID = item.ID;
                    //    op.Type = 1;
                    //    pList.Add(op);
                    //}

                    //判断是否关联过洗涤条码
                    bool tmpCheck = true;
                    foreach (Order_ProductDC item in this.OrderProductList)
                    {
                        if (string.IsNullOrEmpty(item.Code))
                        {
                            tmpCheck = false;
                            break;
                        }

                    }
                    if (tmpCheck)
                    {
                        if (o_business.Factory_AddProduct(entity.ID, OrderProductList, Common.ConstConfig.currOperator.ID))
                        {
                            MessageBox.Show("订单生成成功，如要继续请继续扫描其他物流单号");

                            //this.tsbAddProductOrder.Enabled = false;
                            //this.tsbAddProduct.Enabled = false;

                            //this.tsbNotifyItemError.Enabled = false;

                            //this.txtExpressNum.Text = string.Empty;
                            //this.txtOrderNum.Text = string.Empty;
                            //this.txtRemark.Text = string.Empty;
                            //this.txtUserRemark.Text = string.Empty;

                            //this.lblOrderMessage.Text = string.Empty;

                            //this.txtConsignee.Text = string.Empty;
                            //this.txtMPNo.Text = string.Empty;
                            //this.txtAddress.Text = string.Empty;

                            ////this.selectedList = null;
                            //this.dgvProduct.DataSource = null;
                            //this.dgvProduct.Refresh();
                            //this.entity = null;
                            DataClear();

                            this.txtExpressNum.Focus();
                        }
                        else
                        {
                            MessageBox.Show(o_business.LastError, "错误");
                        }
                    }
                    else
                    {
                        MessageBox.Show("衣物中还未关联洗涤条码！", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("请先添加衣物！", "提示");
                }
            }
            else
            {
                MessageBox.Show("请先扫描物流单号！", "提示");
            }


            //if (this.selectedList != null)
            //{
            //    IList<Order_ProductDC> pList = new List<Order_ProductDC>();
            //    //转化成接口需要的List
            //    foreach (Wash_ProductDC item in selectedList)
            //    {
            //        Order_ProductDC op = new Order_ProductDC();
            //        op.ProductID = item.ID;
            //        op.Type = 1;
            //        pList.Add(op);
            //    }

            //    if (o_business.Onekey_AddProduct(entity.ID, pList, Common.ConstConfig.currOperator.ID))
            //    {
            //        MessageBox.Show("订单生成成功，如要继续请继续扫描其他物流单号");

            //        this.txtExpressNum.Text = string.Empty;
            //        this.txtOrderNum.Text = string.Empty;
            //        this.txtRemark.Text = string.Empty;

            //        this.lblOrderMessage.Text = string.Empty;

            //        //this.selectedList = null;
            //        this.dgvProduct.DataSource = null;
            //        this.entity = null;
            //    }
            //    else
            //    {
            //        MessageBox.Show(o_business.LastError, "错误");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请选择衣物！", "错误");
            //}
        }



        /// <summary>
        /// 表格表头设置
        /// </summary>
        private void dgvColumnsHeaderSet()
        {
            foreach (DataGridViewColumn item in this.dgvProduct.Columns)
            {
                if (item.Name != "Del" && item.Name != "CodeOperator" && item.Name != "CodePrint")
                {
                    item.Visible = false;
                }
                //item.Visible = false;

            }

            int index = 0;
            this.dgvProduct.Columns["ID"].Visible = true;
            this.dgvProduct.Columns["ID"].HeaderText = "ID";
            this.dgvProduct.Columns["ID"].DisplayIndex = index++;

            this.dgvProduct.Columns["ProductID"].Visible = true;
            this.dgvProduct.Columns["ProductID"].HeaderText = "衣物编号";
            this.dgvProduct.Columns["ProductID"].DisplayIndex = index++;

            this.dgvProduct.Columns["Name"].Visible = true;
            this.dgvProduct.Columns["Name"].HeaderText = "衣物名称";
            this.dgvProduct.Columns["Name"].DisplayIndex = index++;

            this.dgvProduct.Columns["Price"].Visible = true;
            this.dgvProduct.Columns["Price"].HeaderText = "销售价";
            this.dgvProduct.Columns["Price"].DisplayIndex = index++;

            //增加衣物描述
            this.dgvProduct.Columns["Attribute"].Visible = true;
            this.dgvProduct.Columns["Attribute"].HeaderText = "衣物描述";
            this.dgvProduct.Columns["Attribute"].DisplayIndex = index++;


            this.dgvProduct.Columns["Code"].Visible = true;
            this.dgvProduct.Columns["Code"].HeaderText = "已关联洗涤条码";
            this.dgvProduct.Columns["Code"].DisplayIndex = index++;


            

            if (Common.ConstConfig.CodeBarChannel == "1")
            {
                if (!dgvProduct.Columns.Contains("CodeOperator"))
                {
                    DataGridView_LinkColAdd("CodeOperator", "打印洗涤条码", "打印洗涤条码", index++);
                }
                if (!dgvProduct.Columns.Contains("CodePrint"))
                {
                    DataGridView_LinkColAdd("CodePrint", "重打洗涤条码", "重打洗涤条码", index++);
                }
            }
            else if (Common.ConstConfig.CodeBarChannel == "2")
            {
                if (!dgvProduct.Columns.Contains("CodeOperator"))
                {
                    DataGridView_LinkColAdd("CodeOperator", "扫描洗涤条码", "扫描洗涤条码", index++);
                }
            }
            
            if (entity.OrderClass == 2 && (entity.ProductList == null || entity.ProductList.Count() == 0))
            {
                if (!dgvProduct.Columns.Contains("Del"))
                {
                    DataGridView_LinkColAdd("Del", "操作", "删除", index);
                }
            }
            //if (entity.OrderClass == 2)
            //{
            //    if (!dgvProduct.Columns.Contains("Del"))
            //    {
            //        DataGridView_LinkColAdd("Del", "操作", "删除", index);
            //    }
            //}
            
        }



        /// <summary>
        /// 添加链接列
        /// </summary>
        /// <param name="ColName">列名</param>
        /// <param name="ColHeadName">列名表头</param>
        /// <param name="ColText">列显示内容</param>
        /// <param name="ColumnsPlus">列位置</param>
        /// <returns></returns>
        private void DataGridView_LinkColAdd(string ColName, string ColHeadName, string ColText, int ColumnsPlus)
        {
            DataGridViewLinkColumn linkColAdd = new DataGridViewLinkColumn();
            dgvProduct.Columns.Insert(dgvProduct.Columns.Count - ColumnsPlus, linkColAdd);
            dgvProduct.Columns[dgvProduct.Columns.Count - ColumnsPlus - 1].Name = ColName;
            dgvProduct.Columns[dgvProduct.Columns.Count - ColumnsPlus - 1].HeaderText = ColHeadName;
            dgvProduct.Columns[dgvProduct.Columns.Count - ColumnsPlus - 1].Visible = true;

            linkColAdd.TrackVisitedState = false;
            linkColAdd.Name = ColName;
            linkColAdd.Text = ColText;
            linkColAdd.UseColumnTextForLinkValue = true;
            linkColAdd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        /// <summary>
        /// 表格单元格内容文本单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (this.dgvProduct.Columns[e.ColumnIndex].Name == "CodeOperator")
            {
                //处理洗涤条码
                if (Common.ConstConfig.CodeBarChannel == "1")
                {
                    //自己生成洗涤条码 并打印

                    string CodeBar = entity.OrderNumber + CodeBarIndex.ToString("000");
                    CodeBarIndex++;
                    string GoodsInfo = this.dgvProduct.Rows[e.RowIndex].Cells["Name"].Value.ToString();


                    int pid = (int)this.dgvProduct.Rows[e.RowIndex].Cells["ID"].Value;
                    foreach (Order_ProductDC item in OrderProductList)
                    {
                        if (item.ID == pid)
                        {
                            item.Code = CodeBar;
                            break;
                        }

                    }

                    this.dgvProduct.DataSource = OrderProductList.ToArray();
                    this.dgvProduct.Refresh();
                    dgvColumnsHeaderSet();



                    CodeBarPrint(CodeBar, GoodsInfo);
                }
                else if (Common.ConstConfig.CodeBarChannel == "2")
                {
                    //扫描洗涤条码
                    frmCodeRelation frm = new frmCodeRelation();

                    IList<string> codes = new List<string>();
                    string currcode = string.Empty;
                    if(this.dgvProduct.Rows[e.RowIndex].Cells["Code"].Value != null)
                    {
                        currcode  = this.dgvProduct.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                    }                
                    foreach (Order_ProductDC item in OrderProductList)
                    {
                        if (!string.IsNullOrEmpty(item.Code) && item.Code != currcode)
                        {
                            codes.Add(item.Code);
                        }
                    }
                    frm.Codes = codes;

                    frm.OrderNumber = entity.OrderNumber;

                    if (this.dgvProduct.Rows[e.RowIndex].Cells["Attribute"].Value != null)
                    {
                        string atmp = this.dgvProduct.Rows[e.RowIndex].Cells["Attribute"].Value.ToString();
                        string[] a = atmp.Split('|');
                        frm.txtSelectKey.Text = a[0];
                        if (a.Length > 1)
                        {
                            frm.txtCustomKey.Text = a[1];
                        }
                    }

                    frm.lblProductName.Text = this.dgvProduct.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        int pid = (int)this.dgvProduct.Rows[e.RowIndex].Cells["ID"].Value;
                        foreach (Order_ProductDC item in OrderProductList)
                        {
                            if (item.ID == pid)
                            {
                                item.Code = frm.txtCode.Text;
                                item.Attribute = frm.txtSelectKey.Text.Trim() + (string.IsNullOrEmpty(frm.txtCustomKey.Text)? "" : "|" + frm.txtCustomKey.Text.Trim());
                                break;
                            }

                        }

                        this.dgvProduct.DataSource = OrderProductList.ToArray();
                        this.dgvProduct.Refresh();
                        dgvColumnsHeaderSet();
                    }
                }
            }
            if (this.dgvProduct.Columns[e.ColumnIndex].Name == "CodePrint")
            {
                string CodeBar = string.Empty;
                if (this.dgvProduct.Rows[e.RowIndex].Cells["CodeOperator"].Value != null)
                {
                    CodeBar = this.dgvProduct.Rows[e.RowIndex].Cells["Code"].Value.ToString();//entity.OrderNumber + CodeBarIndex.ToString("000");
                }

                if (string.IsNullOrEmpty(CodeBar))
                {
                    MessageBox.Show("请先打印洗涤条码");
                    return;
                }

                string GoodsInfo = this.dgvProduct.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                CodeBarPrint(CodeBar, GoodsInfo);

            }
            if (this.dgvProduct.Columns[e.ColumnIndex].Name == "Del")
            {
                //删除该行

                int pid = (int)this.dgvProduct.Rows[e.RowIndex].Cells["ID"].Value;

                //this.selectedList = this.selectedList.Where(p => p.ID != pid).ToList();
                //this.dgvProduct.DataSource = selectedList.ToArray();

                this.OrderProductList = this.OrderProductList.Where(p => p.ID != pid).ToList();
                this.dgvProduct.DataSource = OrderProductList.ToArray();

                this.dgvProduct.Refresh();

                dgvColumnsHeaderSet();
            }
        }

        /// <summary>
        /// 打印配饰条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPrintAccessory_Click(object sender, EventArgs e)
        {
            if (this.dgvProduct.SelectedRows.Count != 1)
            {
                MessageBox.Show("请选择一条记录", "提示");
                return;
            }
            //if (this.dgvProduct.SelectedRows[0].Cells["Code"].Value == null)
            //{
            //    MessageBox.Show("请先关联水洗条码", "提示");
            //    return;
            //}

            //string code = this.dgvProduct.SelectedRows[0].Cells["Code"].Value.ToString();

            string code = string.Empty;
            string goodinfo = string.Empty;
            if (this.dgvProduct.SelectedRows[0].Cells["Code"].Value != null)
            {
                code = this.dgvProduct.SelectedRows[0].Cells["Code"].Value.ToString();
            }
            if (this.dgvProduct.SelectedRows[0].Cells["Attribute"].Value != null)
            {
                goodinfo = this.dgvProduct.SelectedRows[0].Cells["Attribute"].Value.ToString();
            }
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("请先关联水洗条码", "提示");
                return;
            }

            CodeBarPrint(code, goodinfo);
        }


        /// <summary>
        /// 物流单号文本框按下某个按键并释放后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExpressNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                if (string.IsNullOrEmpty(this.txtExpressNum.Text))
                {
                    return;
                }

                if (this.txtExpressNum.Text.Length == 6)
                {
                    this.txtExpressNum.Text = "002101" + this.txtExpressNum.Text;
                }


                //按下回车键后
                GetData();

                this.txtExpressNum.SelectAll();
                //MessageBox.Show(this.txtExpressNum.Text);
            }
        }

        /// <summary>
        /// 物流单号文本框设为活动控件后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExpressNum_Enter(object sender, EventArgs e)
        {
            this.txtExpressNum.SelectAll();
        }
        /// <summary>
        /// 物流单号文本框设为非活动控件后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExpressNum_Leave(object sender, EventArgs e)
        {
            this.txtExpressNum.Tag = null;
        }
        /// <summary>
        /// 物流单号文本框鼠标单击后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExpressNum_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.txtExpressNum.Tag == null)
            {
                this.txtExpressNum.SelectAll();
                this.txtExpressNum.Tag = 0;
            }
        }

        /// <summary>
        /// 列表单元格重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProduct_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Common.Function.DataGridViewCellPainting(sender, e);
        }


        private void frmStockIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.dgvProduct.DataSource != null)
            {
                //说明衣物列表 有数据
                if (MessageBox.Show("订单正在处理中，确定要关闭此窗口吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            
            
        }


        TfrxReportClass report;
        FrxDataTable datatable;

        private void CodeBarPrint(string CodeBar, string GoodsInfo)
        {
            // Create report object
            report = new TfrxReportClass();

            // Create the FR compatible DataTable object
            datatable = new FrxDataTable("ClothesCodeBar");

            //FillTableWithSampleData(datatable);
            datatable.Columns.Add("CodeBar", typeof(string));
            datatable.Columns.Add("GoodsInfo", typeof(string));
            datatable.Rows.Add(new object[] { 
                CodeBar,
                GoodsInfo,
                });

            datatable.AcceptChanges();


            string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\ClothesCodeBar.fr3";
            // Load demmonstration report from file
            report.LoadReportFromFile(FilePath);
            //report.LoadReportFromFile(@"D:\SF.fr3");

            report.ClearDatasets();
            // Asiign datasets to report one more time
            // beacuse theLoadReport... family functions breaks links between report and dataset
            datatable.AssignToReport(true, report);

            // Assigns DataTable to DataBand
            datatable.AssignToDataBand("MasterData1", report);

            //report.ShowReport();
            report.PrintOptions.Printer = Common.ConstConfig.ClothesCodeBarPrintName;
            report.PrintOptions.ShowDialog = false;


            report.PrepareReport(false);
            report.PrintReport();
        }


        /// <summary>
        /// 启用状态更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAddProductOrder_EnabledChanged(object sender, EventArgs e)
        {
            this.txtExpressNum.Enabled = !this.tsbAddProductOrder.Enabled;
        }


        /// <summary>
        /// 异件上报 物品错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNotifyItemError_Click(object sender, EventArgs e)
        {
            if (entity == null)
            {
                MessageBox.Show("请先扫描物流条码！");
                return;
            }

            frmStockInNotifyItemError frm = new frmStockInNotifyItemError();
            frm.txtOrderNumber.Text = entity.OrderNumber;
            frm.ShowDialog();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            
            //}

            DataClear();
        }

        

        

        

        

        

        

        
    }
}
