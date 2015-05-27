namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    partial class frmStockOut
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlOrderInfo = new System.Windows.Forms.Panel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMPNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConsignee = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrintRemark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExpressNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNotifyItemBad = new System.Windows.Forms.ToolStripButton();
            this.dgvOrderProduct = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.pnlOrderInfo.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOrderInfo
            // 
            this.pnlOrderInfo.Controls.Add(this.txtAttribute);
            this.pnlOrderInfo.Controls.Add(this.label4);
            this.pnlOrderInfo.Controls.Add(this.txtAddress);
            this.pnlOrderInfo.Controls.Add(this.label7);
            this.pnlOrderInfo.Controls.Add(this.txtMPNo);
            this.pnlOrderInfo.Controls.Add(this.label6);
            this.pnlOrderInfo.Controls.Add(this.txtConsignee);
            this.pnlOrderInfo.Controls.Add(this.label5);
            this.pnlOrderInfo.Controls.Add(this.txtPrintRemark);
            this.pnlOrderInfo.Controls.Add(this.label3);
            this.pnlOrderInfo.Controls.Add(this.txtCode);
            this.pnlOrderInfo.Controls.Add(this.label2);
            this.pnlOrderInfo.Controls.Add(this.txtExpressNum);
            this.pnlOrderInfo.Controls.Add(this.label1);
            this.pnlOrderInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderInfo.Location = new System.Drawing.Point(0, 25);
            this.pnlOrderInfo.Name = "pnlOrderInfo";
            this.pnlOrderInfo.Size = new System.Drawing.Size(784, 156);
            this.pnlOrderInfo.TabIndex = 0;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(119, 82);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(620, 21);
            this.txtAddress.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "送件地址：";
            // 
            // txtMPNo
            // 
            this.txtMPNo.Location = new System.Drawing.Point(420, 46);
            this.txtMPNo.Name = "txtMPNo";
            this.txtMPNo.ReadOnly = true;
            this.txtMPNo.Size = new System.Drawing.Size(160, 21);
            this.txtMPNo.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(322, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "用户手机：";
            // 
            // txtConsignee
            // 
            this.txtConsignee.Location = new System.Drawing.Point(119, 46);
            this.txtConsignee.Name = "txtConsignee";
            this.txtConsignee.ReadOnly = true;
            this.txtConsignee.Size = new System.Drawing.Size(160, 21);
            this.txtConsignee.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "用户姓名：";
            // 
            // txtPrintRemark
            // 
            this.txtPrintRemark.Location = new System.Drawing.Point(669, 13);
            this.txtPrintRemark.Multiline = true;
            this.txtPrintRemark.Name = "txtPrintRemark";
            this.txtPrintRemark.Size = new System.Drawing.Size(81, 23);
            this.txtPrintRemark.TabIndex = 5;
            this.txtPrintRemark.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(598, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "打印备注：";
            this.label3.Visible = false;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(119, 13);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(160, 21);
            this.txtCode.TabIndex = 3;
            this.txtCode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtCode_MouseClick);
            this.txtCode.Enter += new System.EventHandler(this.txtCode_Enter);
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "请扫描洗涤条码：";
            // 
            // txtExpressNum
            // 
            this.txtExpressNum.Location = new System.Drawing.Point(420, 13);
            this.txtExpressNum.Name = "txtExpressNum";
            this.txtExpressNum.Size = new System.Drawing.Size(160, 21);
            this.txtExpressNum.TabIndex = 1;
            this.txtExpressNum.Visible = false;
            this.txtExpressNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExpressNum_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请扫描物流单号：";
            this.label1.Visible = false;
            // 
            // mnuMain
            // 
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnuMain.Size = new System.Drawing.Size(784, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            this.mnuMain.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrint,
            this.toolStripSeparator1,
            this.tsbNotifyItemBad});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPrint
            // 
            this.tsbPrint.Image = global::LazyAtHome.Winform.FactoryPortal.Properties.Resources._6;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(88, 22);
            this.tsbPrint.Text = "打印快递单";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbNotifyItemBad
            // 
            this.tsbNotifyItemBad.Image = global::LazyAtHome.Winform.FactoryPortal.Properties.Resources._3;
            this.tsbNotifyItemBad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNotifyItemBad.Name = "tsbNotifyItemBad";
            this.tsbNotifyItemBad.Size = new System.Drawing.Size(76, 22);
            this.tsbNotifyItemBad.Text = "异件上报";
            this.tsbNotifyItemBad.Click += new System.EventHandler(this.tsbNotifyItemBad_Click);
            // 
            // dgvOrderProduct
            // 
            this.dgvOrderProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderProduct.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderProduct.Location = new System.Drawing.Point(0, 181);
            this.dgvOrderProduct.Name = "dgvOrderProduct";
            this.dgvOrderProduct.ReadOnly = true;
            this.dgvOrderProduct.RowTemplate.Height = 23;
            this.dgvOrderProduct.Size = new System.Drawing.Size(784, 380);
            this.dgvOrderProduct.TabIndex = 3;
            this.dgvOrderProduct.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrderProduct_CellFormatting);
            this.dgvOrderProduct.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvOrderProduct_CellPainting);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "衣物描述：";
            // 
            // txtAttribute
            // 
            this.txtAttribute.Location = new System.Drawing.Point(119, 117);
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.ReadOnly = true;
            this.txtAttribute.Size = new System.Drawing.Size(620, 21);
            this.txtAttribute.TabIndex = 22;
            // 
            // frmStockOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dgvOrderProduct);
            this.Controls.Add(this.pnlOrderInfo);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmStockOut";
            this.Text = "扫描出库";
            this.Load += new System.EventHandler(this.frmStockOut_Load);
            this.pnlOrderInfo.ResumeLayout(false);
            this.pnlOrderInfo.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlOrderInfo;
        private System.Windows.Forms.TextBox txtExpressNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.DataGridView dgvOrderProduct;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrintRemark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMPNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConsignee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tsbNotifyItemBad;
        private System.Windows.Forms.TextBox txtAttribute;
        private System.Windows.Forms.Label label4;
    }
}