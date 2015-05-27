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

namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    public partial class frmWashCheck : Form
    {

        //业务逻辑
        private Business.WashOrder business = null;

        //订单实体
        Order_OrderDC entity = null;

        public frmWashCheck()
        {
            InitializeComponent();
            if (business == null)
            {
                business = new Business.WashOrder();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.gbTPCode.Visible = false;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetData()
        {
            entity = business.GetOrderByFactoryCode(this.txtWashCode.Text);

            if (entity != null && entity.ProductList != null)
            {
                this.gbTPCode.Visible = true;
                this.txtWashCode.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("未查到订单信息，请确认洗涤条码是否正确");
                return;
            }
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWashCheck_Load(object sender, EventArgs e)
        {
            Init();
        }



        /// <summary>
        /// 条码关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTPCode.Text))
            {
                MessageBox.Show("请扫描威士条码！");
                return;
            }
            if (entity == null)
            {
                MessageBox.Show("请扫描水洗条码！");
                return;
            }

            //根据水洗条码找出ID
            //Order_ProductDC[] pList = new Order_ProductDC[1];
            int pid = 0;
            //循环ProductList
            foreach (var item in entity.ProductList)
            {
                if (item.Code == this.txtWashCode.Text)
                {
                    pid = item.ID;
                    break;
                }
            }
            if (pid == 0)
            {
                MessageBox.Show("水洗条码错误，请联系管理员！","错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (business.OrderProductOtherCodeUPDATE(entity.ID, pid, this.txtTPCode.Text))
            {
                MessageBox.Show("关联成功！");

                //更新成功
                this.gbTPCode.Visible = false;
                this.txtWashCode.ReadOnly = false;
                this.txtWashCode.Text = string.Empty;

                this.txtTPCode.Text = string.Empty;

                this.txtWashCode.Focus();
            }
            else
            {
                MessageBox.Show("关联第三方条码错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 洗涤条码文本框按下某个按键并释放后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWashCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                //按下回车键后
                GetData();

                this.txtTPCode.Focus();
            }
        }

        /// <summary>
        /// 第三方条码文本框按下某个按键并释放后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTPCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                this.btnSave.Focus();
            }
            
        }

        

        
    }
}
