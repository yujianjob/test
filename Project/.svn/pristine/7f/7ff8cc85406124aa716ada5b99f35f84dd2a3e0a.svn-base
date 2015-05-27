using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    public partial class frmCodeRelation : Form
    {
        public frmCodeRelation()
        {
            InitializeComponent();
        }

        public IList<string> Codes = null;

        public string OrderNumber = string.Empty;

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCodeRelation_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            //初始化配件列表
            this.dgvAccessory.Rows.Add(new[] { "里子", "毛" });
            this.dgvAccessory.Rows.Add(new[] { "装饰扣", "饰物" });
            this.dgvAccessory.Rows.Add(new[] { "帽子", "内胆" });
            this.dgvAccessory.Rows.Add(new[] { "腰带", "袖带" });
            this.dgvAccessory.Rows.Add(new[] { "腰带", "袖带1" });
            this.dgvAccessory.Rows.Add(new[] { "袖带2", "颈带" });
            this.dgvAccessory.Rows.Add(new[] { "毛领", "毛条" });

            //初始化样式列表
            this.dgvStripe.Rows.Add(new[] { "条", "花", "点", "格" });


            //初始化颜色按钮事件
            foreach (Control item in this.gbColor.Controls)
            {
                if (item is Button)
                {
                    item.Click += item_Click;
                }
            }

        }

        /// <summary>
        /// 颜色按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Click(object sender, EventArgs e)
        {
            //this.txtSelectKey.Text += ((Button)sender).Tag.ToString() + " ";
            AddSelectKey(((Button)sender).Tag.ToString());
        }


        /// <summary>
        /// 配件选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAccessory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //this.txtSelectKey.Text += this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + " ";
                AddSelectKey(this.dgvAccessory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }

        }
        /// <summary>
        /// 样式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStripe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //this.txtSelectKey.Text += this.dgvStripe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + " ";
                AddSelectKey(this.dgvStripe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }

        }

        private void AddSelectKey(string key)
        {
            if (!this.txtSelectKey.Text.Contains(key))
            {
                this.txtSelectKey.Text += key + " ";
            }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            //{
            //    if (string.IsNullOrEmpty(this.txtCode.Text))
            //    {
            //        MessageBox.Show("请扫描洗涤条码");
            //    }
            //    else
            //    {
            //        try
            //        {
            //            int var1 = Convert.ToInt32(this.txtCode.Text);
            //        }
            //        catch
            //        {
            //            MessageBox.Show("请确认输入的是数字");
            //            this.txtCode.SelectAll();
            //            return;
            //        }

            //        if (this.txtCode.Text.Length != 9)
            //        {
            //            MessageBox.Show("请输入9位是数字");
            //            this.txtCode.SelectAll();
            //            return;
            //        }

            //        if (Codes != null && Codes.Contains(this.txtCode.Text))
            //        {
            //            MessageBox.Show("洗涤条码已经使用，请更换！");
            //            this.txtCode.SelectAll();
            //        }
            //        else
            //        {
            //            DialogResult = DialogResult.OK;
            //        }
            //    }
            //}
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCode.Text))
            {
                MessageBox.Show("请扫描洗涤条码");
                return;
            }
            else
            {
                try
                {
                    int var1 = Convert.ToInt32(this.txtCode.Text);
                }
                catch
                {
                    MessageBox.Show("请确认输入的是数字");
                    this.txtCode.SelectAll();
                    return;
                }

                if (this.txtCode.Text.Length != 9)
                {
                    MessageBox.Show("请输入9位是数字");
                    this.txtCode.SelectAll();
                    return;
                }

                if (Codes != null && Codes.Contains(this.txtCode.Text))
                {
                    MessageBox.Show("洗涤条码已经使用，请更换！");
                    this.txtCode.SelectAll();
                    return;
                }
                //else
                //{
                //    DialogResult = DialogResult.OK;
                //}
            }
            if (this.txtSelectKey.Text == string.Empty && this.txtCustomKey.Text == string.Empty)
            {
                MessageBox.Show("请填写衣物描述");
                return;
            }

            DialogResult = DialogResult.OK;
        }
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.txtCode.Text))
        //    {
        //        MessageBox.Show("请扫描洗涤条码");
        //    }
        //    else
        //    {
        //        if (Codes != null && Codes.Contains(this.txtCode.Text))
        //        {
        //            MessageBox.Show("洗涤条码已经使用，请更换！");
        //            this.txtCode.SelectAll();
        //        }
        //        else
        //        {
        //            DialogResult = DialogResult.OK;
        //        }
        //    }

        //}


        /// <summary>
        /// 清除衣物描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectKeyClear_Click(object sender, EventArgs e)
        {
            this.txtSelectKey.Text = string.Empty;
        }
        /// <summary>
        /// 清除衣物描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustomKeyClear_Click(object sender, EventArgs e)
        {
            this.txtCustomKey.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue >= 48 && e.KeyValue <= 57)
            {
                if (this.txtCode.Text.Length == 9)
                {
                    try
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem((args) => { this.pbxSnapshot.Image = Camera.frmPreview.Snapshot(this.txtCode.Text + "-" + this.OrderNumber); });
                    }
                    catch (Exception ex)
                    {
                        Common.WriteLog.tradeLog(LazyAtHome.Winform.FactoryPortal.Common.ConstConfig.WRONG_LOG_PATH, ex.Message);
                        return;
                    }
                }
            }
        }












    }
}
