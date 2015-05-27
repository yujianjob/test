using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyAtHome.Winform.FactoryPortal.UI.Operator
{
    public partial class frmPasswordChange : Form
    {
        private Business.Operator business = null;

        public frmPasswordChange()
        {
            InitializeComponent();

            if (business == null)
            {
                business = new Business.Operator();
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPasswordChange_Load(object sender, EventArgs e)
        {
            this.txtOperatorName.Text = Common.ConstConfig.currOperator.Name;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (!CheckPassword())
            //    return;
            //if (business.PasswordChange(this.txtNewPassword.Text))
            //{
            //    MessageBox.Show("修改密码成功");
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show(business.LastError, "错误");
            //}
        }



        private bool CheckPassword()
        {
            if (this.txtOldPassword.Text == string.Empty)
            {
                MessageBox.Show("请填写旧密码");
                return false;
            }
            if (this.txtNewPassword.Text == string.Empty)
            {
                MessageBox.Show("请填写新密码");
                return false;
            }
            if (this.txtPasswordConfirm.Text == string.Empty)
            {
                MessageBox.Show("请填写确认密码");
                return false;
            }
            if (this.txtNewPassword.Text.Length < 6)
            {
                MessageBox.Show("请确认新密码在6位以上");
                return false;
            }
            if (this.txtNewPassword.Text != this.txtPasswordConfirm.Text)
            {
                MessageBox.Show("您填写的新密码与确认密码不一致，请再次确认");
                return false;
            }
            return true;
        }
    }
}
