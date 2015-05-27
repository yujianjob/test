using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LazyAtHome.Winform.FactoryPortal.InternalExpressService;

namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    public partial class frmStockPre : Form
    {
        //业务逻辑
        private Business.WashOrder business = null;

        //应收列表
        IList<Exp_StorageItemDC> listReceivable;

        //错件列表
        IList<Exp_StorageItemDC> listError = new List<Exp_StorageItemDC>();

        //代收列表
        IList<Exp_StorageItemDC> listWait = new List<Exp_StorageItemDC>();

        //当前选择的干线
        Exp_NodeDC currLine = null;

        public frmStockPre()
        {
            InitializeComponent();

            if (business == null)
            {
                business = new Business.WashOrder();
            }
        }


        private void Init()
        {
            //加载干线列表
            IList<Exp_NodeDC> list = business.GetLineList();
            if (list != null)
            {
                Exp_NodeDC[] sellist = new Exp_NodeDC[list.Count + 1];
                sellist[0] = new Exp_NodeDC(){ ID = -1, Name= "请选择" };
                list.CopyTo(sellist, 1);
                this.cboLine.DataSource = sellist;
                this.cboLine.DisplayMember = "Name";         
            }



            ////临时测试数据使用
            //listReceivable = new List<Exp_StorageItemDC>();        
            //for (int i = 1; i <= 10; i++)
            //{
            //    Exp_StorageItemDC entity = new Exp_StorageItemDC();
            //    entity.OtherNumber = "002101" + i.ToString("000000");
            //    listReceivable.Add(entity);
            //}
            

        }

        private void ListBind()
        {
            //if (listReceivable != null)
            //{
                //绑定到listbox上
            this.lbReceivable.DataSource = null;
                this.lbReceivable.DataSource = listReceivable;
                this.lbReceivable.DisplayMember = "OtherNumber";

                this.lbReceivable.Refresh();
            //}
            //if (listWait != null)
            //{
                //绑定到listbox上
                this.lbWait.DataSource = null;
                this.lbWait.DataSource = listWait;
                this.lbWait.DisplayMember = "OtherNumber";

                this.lbWait.Refresh();
            //}
            //if (listError != null)
            //{
                //绑定到listbox上
                this.lbError.DataSource = null;
                this.lbError.DataSource = listError;
                this.lbError.DisplayMember = "OtherNumber";

                this.lbError.Refresh();
            //}
            this.txtScan.Focus();

        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStockPre_Load(object sender, EventArgs e)
        {
            Init();
        }


        /// <summary>
        /// 下拉框下拉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cboLine.SelectedIndex == 0)
            {
                return;
            }

            currLine = (Exp_NodeDC)this.cboLine.SelectedItem;

            //获取库存列表
            listReceivable = business.GetLineStorageItem(currLine.StorageID);
            ListBind();
            if (listReceivable == null)
            {
                MessageBox.Show("获取干线库存失败");
            }

            //MessageBox.Show(node.Name);
        }


        /// <summary>
        /// 文本框按下某个按键并释放后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                if (this.currLine == null)
                {
                    MessageBox.Show("请先选择一条干线");
                    this.txtScan.SelectAll();
                    return;
                }

                //按下回车键后
                if (string.IsNullOrEmpty(this.txtScan.Text))
                {
                    return;
                }

                //处理
                if (this.txtScan.Text.Length == 6)
                {
                    this.txtScan.Text = "002101" + this.txtScan.Text;
                }
                else
                {
                    if (this.txtScan.Text.Length == 12)
                    {
                        try
                        {
                            Convert.ToInt64(this.txtScan.Text);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("条码格式无效");
                            this.txtScan.SelectAll();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("条码格式无效");
                        this.txtScan.SelectAll();
                        return;
                    }
                    
                }

                Deal(this.txtScan.Text);

                if ((this.listWait.Count > 0 || this.listError.Count > 0) && this.cboLine.Enabled)
                {
                    this.cboLine.Enabled = false;
                }


                this.txtScan.SelectAll();
            }
        }


        /// <summary>
        /// 核心处理
        /// </summary>
        /// <param name="ExpressNum"></param>
        private void Deal(string ExpressNum)
        {
            //先判断 listReceivable 里面有没有
            Exp_StorageItemDC item = listReceivable.FirstOrDefault(p => p.OtherNumber == ExpressNum);
            if (item == null)
            {
                //listReceivable没有的话 判listWait里面有没有
                item = listWait.FirstOrDefault(p => p.OtherNumber == ExpressNum);
                if (item == null)
                {
                    //listWait没有的话 判listError里面有没有
                    item = listError.FirstOrDefault(p => p.OtherNumber == ExpressNum);
                    if (item == null)
                    {
                        //listError没有的话 就往listError加
                        item = new Exp_StorageItemDC();
                        item.OtherNumber = ExpressNum;
                        listError.Add(item);
                    }
                }
            }
            else
            {
                //listReceivable有的话
                //往listWait里加
                listWait.Add(item);

                //listReceivable去掉
                listReceivable.Remove(item);


                MessageBeep(MessageBeepType.Error);
            }

            ListBind();
        }


        /// <summary>
        /// 差件上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotGetReport_Click(object sender, EventArgs e)
        {
            if (this.currLine == null)
            {
                MessageBox.Show("请先选择一条干线");
                return;
            }

            if(business.Factory_TakeLine_ErrorCount(currLine.StorageID, Common.ConstConfig.currOperator.ID))
            {
                MessageBox.Show("“未收上报”成功！");

            }
            else
            {
                MessageBox.Show("“未收上报”发生错误，请联系管理员！");
            }
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.currLine == null)
            {
                MessageBox.Show("请先选择一条干线");
                return;
            }
            if (business.Factory_TakeLine_Complete(currLine.StorageID, listWait.Select(p => p.Number).ToArray(), Common.ConstConfig.currOperator.ID))
            {
                MessageBox.Show("“入库上传”成功！");
                this.btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("“入库上传”发生错误，请联系管理员！");
            }
        }

        /// <summary>
        /// 错件上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErrorReport_Click(object sender, EventArgs e)
        {
            if (this.currLine == null)
            {
                MessageBox.Show("请先选择一条干线");
                return;
            }
            if (business.Factory_TakeLine_ErrorNumber(currLine.StorageID, listError.Select(p => p.OtherNumber).ToArray(), Common.ConstConfig.currOperator.ID))
            {
                MessageBox.Show("“错件上报”成功！");
            }
            else
            {
                MessageBox.Show("“错件上报”发生错误，请联系管理员！");
            }
        }



        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MessageBeep(MessageBeepType type);
    }


    public enum MessageBeepType
    {
        Default = -1,
        Ok = 0x00000000,
        Error = 0x00000010,
        Question = 0x00000020,
        Warning = 0x00000030,
        Information = 0x00000040
    }
}
