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
    public partial class frmStockInNotifyItemError : Form
    {
        private Business.WashOrder o_business = null;

        public frmStockInNotifyItemError()
        {
            InitializeComponent();

            if (o_business == null)
            {
                o_business = new Business.WashOrder();
            }
        }

        /// <summary>
        /// 异件上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cboNotifyType.SelectedIndex < 0)
            {
                MessageBox.Show("请选择异件类型！");
                return;
            }
            if (!o_business.OrderInFactory_FailNotify_ItemError(this.txtOrderNumber.Text, this.cboNotifyType.Text + "：" + this.txtRemark.Text, Common.ConstConfig.currOperator.ID))
            {
                MessageBox.Show("上报异常发生错误，请联系管理员！", "工厂管理系统", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("上报成功，请将衣物保管好等待客服处理！");
                
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
