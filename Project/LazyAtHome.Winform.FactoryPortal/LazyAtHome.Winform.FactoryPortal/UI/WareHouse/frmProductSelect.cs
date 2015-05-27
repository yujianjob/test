using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyAtHome.Winform.FactoryPortal.WashProductService;

namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    public partial class frmProductSelect : Form
    {
        //类别集合
        public IList<Wash_ClassDC> wcList = null;

        //运价集合
        public IList<Wash_ProductDC> wpList = null;

        //选择的产品运价
        public Wash_ProductDC wpSelect = new Wash_ProductDC();

        public frmProductSelect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductSelect_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        { 
            //循环类别集合
            if (wcList != null)
            {
                for (int i = 0; i < wcList.Count; i++)
                {
                    Button btnClass = new Button();

                    btnClass.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    btnClass.Location = new System.Drawing.Point(0, i * 30);
                    btnClass.Name = "btnClass";
                    btnClass.Size = new System.Drawing.Size(120, 30);
                    //button.TabIndex = 0;
                    btnClass.UseVisualStyleBackColor = true;
                    btnClass.Text = wcList[i].Name;
                    btnClass.Tag = wcList[i].ID;

                    btnClass.Click += btnClass_Click;

                    this.splitContainer.Panel1.Controls.Add(btnClass);
                }
            }

        }

        /// <summary>
        /// 点击左边类别按钮，右边刷新产品运价列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClass_Click(object sender, EventArgs e)
        {
            //先移除所有控件
            this.pnlProduct.Controls.Clear();
            //this.splitContainer.Panel2.Controls.Clear();
            //获取事件来源宿主
            Button tmp = (Button)sender;
            //按照类别ID筛选
            IList<Wash_ProductDC> tmplist = wpList.Where(p => p.ClassID == (int)tmp.Tag).ToList();

            int unit = 40;

            //循环产品运价集合
            for (int i = 0; i < tmplist.Count; i++)
            {
                Button btnProduct = new Button();
                int x = i / 5;
                int y = i % 5;
                btnProduct.Location = new System.Drawing.Point(unit * (y * 3 + 1), unit * (x * 3 + 1));
                btnProduct.Name = "btnProduct";
                btnProduct.Size = new System.Drawing.Size(unit * 2, unit * 2);
                btnProduct.TabIndex = 0;
                btnProduct.UseVisualStyleBackColor = true;
                //btnProduct.Text = tmplist[i].Name + "\r\n\r\n￥" + tmplist[i].SalesPrice.ToString();
                btnProduct.Text = tmplist[i].Name;
                btnProduct.Tag = tmplist[i];

                btnProduct.Click += btnProduct_Click;

                this.pnlProduct.Controls.Add(btnProduct);
                
            }

        }

        /// <summary>
        /// 点击产品运价，返回主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnProduct_Click(object sender, EventArgs e)
        {
            //获取事件来源宿主
            Button tmp = (Button)sender;

            //进行赋值
            wpSelect = (Wash_ProductDC)tmp.Tag;

            DialogResult = DialogResult.OK;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 界面关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
