namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    partial class frmStockPre
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
            this.cboLine = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbReceivable = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbWait = new System.Windows.Forms.ListBox();
            this.lbError = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnErrorReport = new System.Windows.Forms.Button();
            this.btnNotGetReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboLine
            // 
            this.cboLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLine.FormattingEnabled = true;
            this.cboLine.Location = new System.Drawing.Point(84, 10);
            this.cboLine.Name = "cboLine";
            this.cboLine.Size = new System.Drawing.Size(181, 20);
            this.cboLine.TabIndex = 0;
            this.cboLine.SelectedIndexChanged += new System.EventHandler(this.cboLine_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "干线选择：";
            // 
            // lbReceivable
            // 
            this.lbReceivable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbReceivable.FormattingEnabled = true;
            this.lbReceivable.ItemHeight = 12;
            this.lbReceivable.Location = new System.Drawing.Point(15, 65);
            this.lbReceivable.Name = "lbReceivable";
            this.lbReceivable.Size = new System.Drawing.Size(250, 448);
            this.lbReceivable.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "待扫区(物流条码)：";
            // 
            // txtScan
            // 
            this.txtScan.Location = new System.Drawing.Point(404, 9);
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(165, 21);
            this.txtScan.TabIndex = 4;
            this.txtScan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtScan_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "应收列表：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(294, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "已收列表：";
            // 
            // lbWait
            // 
            this.lbWait.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbWait.FormattingEnabled = true;
            this.lbWait.ItemHeight = 12;
            this.lbWait.Location = new System.Drawing.Point(296, 65);
            this.lbWait.Name = "lbWait";
            this.lbWait.Size = new System.Drawing.Size(273, 448);
            this.lbWait.TabIndex = 7;
            // 
            // lbError
            // 
            this.lbError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbError.FormattingEnabled = true;
            this.lbError.ItemHeight = 12;
            this.lbError.Location = new System.Drawing.Point(599, 65);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(273, 448);
            this.lbError.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(597, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "错件列表：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(494, 526);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "确认入库";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnErrorReport
            // 
            this.btnErrorReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnErrorReport.Location = new System.Drawing.Point(797, 526);
            this.btnErrorReport.Name = "btnErrorReport";
            this.btnErrorReport.Size = new System.Drawing.Size(75, 23);
            this.btnErrorReport.TabIndex = 11;
            this.btnErrorReport.Text = "错件上报";
            this.btnErrorReport.UseVisualStyleBackColor = true;
            this.btnErrorReport.Click += new System.EventHandler(this.btnErrorReport_Click);
            // 
            // btnNotGetReport
            // 
            this.btnNotGetReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNotGetReport.Location = new System.Drawing.Point(190, 526);
            this.btnNotGetReport.Name = "btnNotGetReport";
            this.btnNotGetReport.Size = new System.Drawing.Size(75, 23);
            this.btnNotGetReport.TabIndex = 12;
            this.btnNotGetReport.Text = "未收上报";
            this.btnNotGetReport.UseVisualStyleBackColor = true;
            this.btnNotGetReport.Click += new System.EventHandler(this.btnNotGetReport_Click);
            // 
            // frmStockPre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.btnNotGetReport);
            this.Controls.Add(this.btnErrorReport);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbWait);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbReceivable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboLine);
            this.Name = "frmStockPre";
            this.Text = "工厂收件";
            this.Load += new System.EventHandler(this.frmStockPre_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbReceivable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtScan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbWait;
        private System.Windows.Forms.ListBox lbError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnErrorReport;
        private System.Windows.Forms.Button btnNotGetReport;
    }
}