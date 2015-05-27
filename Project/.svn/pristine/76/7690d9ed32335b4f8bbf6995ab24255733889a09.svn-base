using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LazyAtHome.Winform.FactoryPortal.UI
{
    public partial class frmMain : Form
    {

        //登录窗体对象
        private frmLogin Login;

        

        public frmMain(frmLogin Login)
        {
            this.Login = Login;
            InitializeComponent();

            
        }

        #region 菜单事件

        /// <summary>
        /// 主界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //this.tsmiSFPrint_Click(null, null);

            //加载摄像头监控
            CameraPreviewLoad();


            ////加载干线列表
            //IList<Exp_NodeDC> list = business.GetLineList();
            //if (list != null)
            //{
            //    foreach (Exp_NodeDC item in list)
            //    {
            //        ToolStripMenuItem tsmiStockPreByLine = new System.Windows.Forms.ToolStripMenuItem();

            //        tsmiStockPreByLine.Name = "tsmiStockPreByLine" + item.ID;
            //        tsmiStockPreByLine.Size = new System.Drawing.Size(152, 22);
            //        tsmiStockPreByLine.Text = item.Name + "[" + item.No + "]";
            //        tsmiStockPre.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {tsmiStockPreByLine});
            //        tsmiStockPreByLine.Click += tsmiStockPreByLine_Click;
                    
            //        tsmiStockPreByLine.Tag = item;
            //    } 
            //}
        }

        //void tsmiStockPreByLine_Click(object sender, EventArgs e)
        //{
        //    foreach (Form frm in this.MdiChildren)
        //    {
        //        if (frm is WareHouse.frmStockIn)
        //        {
        //            if (((WareHouse.frmStockIn)frm).dgvProduct.DataSource != null)
        //            {
        //                //说明衣物列表 有数据
        //                if (MessageBox.Show("入库界面还有订单正在处理中，确定要离开此窗口吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
        //                {
        //                    return;
        //                }
        //            }
        //        }
        //    }

        //    foreach (Form frm in this.MdiChildren)
        //    {
        //        if (frm is WareHouse.frmStockPre)
        //        {
        //            frm.WindowState = FormWindowState.Maximized;
        //            frm.BringToFront();
        //            return;
        //        }
        //        //else
        //        //{
        //        //    frm.Close();
        //        //}
        //    }
        //    WareHouse.frmStockPre frmNew = new WareHouse.frmStockPre();
        //    frmNew.MdiParent = this;
        //    frmNew.WindowState = FormWindowState.Maximized;
        //    frmNew.Show();


        //    ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            
        //    //MessageBox.Show("123");//throw new NotImplementedException();
        //}

        #region 文件

        /// <summary>
        /// 回到登录界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiReLogin_Click(object sender, EventArgs e)
        {
            foreach (Form frm in MdiChildren)
            {
                //关闭所有子窗体
                frm.Close();
            }
            this.Visible = false;
            Login.fm = this;
            Login.Visible = true;
        }


        /// <summary>
        /// 修改密码菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is Operator.frmPasswordChange)
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.BringToFront();
                    return;
                }
                else
                {
                    frm.Close();
                }
            }
            Operator.frmPasswordChange frmNew = new Operator.frmPasswordChange();
            //frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Normal;
            frmNew.ShowDialog();
        }


        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region 出入库

        /// <summary>
        /// 工厂收件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStockPre_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is WareHouse.frmStockIn)
                {
                    if (((WareHouse.frmStockIn)frm).dgvProduct.DataSource != null)
                    {
                        //说明衣物列表 有数据
                        if (MessageBox.Show("入库界面还有订单正在处理中，确定要离开此窗口吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is WareHouse.frmStockPre)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BringToFront();
                    return;
                }
                //else
                //{
                //    frm.Close();
                //}
            }
            WareHouse.frmStockPre frmNew = new WareHouse.frmStockPre();
            frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.Show();
        }

        /// <summary>
        /// 扫描入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStockIn_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is WareHouse.frmStockIn)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BringToFront();
                    return;
                }
                //else
                //{
                //    frm.Close();
                //}
            }
            WareHouse.frmStockIn frmNew = new WareHouse.frmStockIn();
            frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.Show();
        }


        /// <summary>
        /// 送洗检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWashCheck_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is WareHouse.frmStockIn)
                {
                    if (((WareHouse.frmStockIn)frm).dgvProduct.DataSource != null)
                    {
                        //说明衣物列表 有数据
                        if (MessageBox.Show("入库界面还有订单正在处理中，确定要离开此窗口吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                    }

                }
            }

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is WareHouse.frmWashCheck)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BringToFront();
                    return;
                }
                //else
                //{
                //    frm.Close();
                //}
            }
            WareHouse.frmWashCheck frmNew = new WareHouse.frmWashCheck();
            frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.Show();
        }

        /// <summary>
        /// 扫描出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStockOut_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is WareHouse.frmStockIn)
                {
                    if (((WareHouse.frmStockIn)frm).dgvProduct.DataSource != null)
                    {
                        //说明衣物列表 有数据
                        if (MessageBox.Show("入库界面还有订单正在处理中，确定要离开此窗口吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }

                    }

                }
            }


            foreach (Form frm in this.MdiChildren)
            {
                if (frm is WareHouse.frmStockOut)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BringToFront();
                    return;
                }
                //else
                //{
                //    frm.Close();
                //}
            }
            WareHouse.frmStockOut frmNew = new WareHouse.frmStockOut();
            frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.Show();
        }
        #endregion

        #region 面单打印

        /// <summary>
        /// 收衣面单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiGetPrint_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is Print.ConsigneePrint)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BringToFront();
                    return;
                }
                else
                {
                    frm.Close();
                }
            }
            Print.ConsigneePrint frmNew = new Print.ConsigneePrint();
            frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.Show();
        }

        #endregion




        #endregion







        /// <summary>
        /// 主界面退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }




        private void tsmiTest_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is Test.Form1)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BringToFront();
                    return;
                }
                else
                {
                    frm.Close();
                }
            }
            Test.Form1 frmNew = new Test.Form1();
            frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.Show();
        }

        private void tsmiSFPrint_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is Test.Form2)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.BringToFront();
                    return;
                }
                else
                {
                    frm.Close();
                }
            }
            Test.Form2 frmNew = new Test.Form2();
            frmNew.MdiParent = this;
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.Show();
        }

        public static Camera.frmPreview frmCameraPreview = null;

        private void tsmiCameraPreview_Click(object sender, EventArgs e)
        {
            if (frmCameraPreview != null && !frmCameraPreview.IsDisposed)
            {
                frmCameraPreview.Visible = true;
                //frmCameraPreview.WindowState = FormWindowState.Normal;
                frmCameraPreview.Activate();
                frmCameraPreview.Show();
            }
            else
            {
                frmCameraPreview = new Camera.frmPreview();
                frmCameraPreview.WindowState = FormWindowState.Normal;
                frmCameraPreview.Show();
            }
        }

        private void CameraPreviewLoad()
        {
            frmCameraPreview = new Camera.frmPreview();
            frmCameraPreview.WindowState = FormWindowState.Normal;
            frmCameraPreview.Show();
        }

        

        

        







    }
}
