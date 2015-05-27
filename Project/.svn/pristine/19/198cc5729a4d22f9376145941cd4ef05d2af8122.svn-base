using Camera_NET;
using LazyAtHome.Winform.FactoryPortal.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyAtHome.Winform.FactoryPortal.UI.Camera
{
    public partial class frmPreview : Form
    {
        public IMoniker SelectMoniker { get; set; }
        public Resolution SelectResolution { get; set; }

        public frmPreview()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void frmPreview_Load(object sender, EventArgs e)
        {
            CreatePreview();

            LoadConfig();
        }

        private void LoadConfig()
        {
            Camera.frmSetup frmNew = new Camera.frmSetup();
            frmNew.Tag = this;
            frmNew.WindowState = FormWindowState.Normal;

            frmNew.frmSetup_Load(null, null);

            if (frmNew.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                frmNew.Close();
            }
        }

        public void CreatePreview()
        {
            if (this.SelectMoniker == null)
            {
                if (this.pnlCamera.CameraCreated)
                {
                    this.pnlCamera.CloseCamera();
                }
                return;
            }
            else
            {
                this.pnlCamera.SetCamera(this.SelectMoniker, this.SelectResolution);
            }
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            Camera.frmSetup frmNew = new Camera.frmSetup();
            frmNew.Tag = this;
            frmNew.WindowState = FormWindowState.Normal;
            frmNew.ShowDialog();
        }


        private void frmPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
            this.Visible = false;
        }

        public Image SaveSnapshotImage(string iFileName)
        {

            if (!this.pnlCamera.CameraCreated)
                return null;

            var SnapshotImg = this.pnlCamera.SnapshotSourceImage();

            if (SnapshotImg != null)
            {
                var path = "D:\\Pic\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                SnapshotImg.Save(path + iFileName + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);

                //SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                //SaveFileDialog1.FileName = iFileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                //SaveFileDialog1.Filter = "Image Files(*.JPG;*.GIF)|*.JPG;*.GIF|All files (*.*)|*.*";
                //SaveFileDialog1.InitialDirectory = "D:\\";
                //if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    img.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                //}
            }

            return SnapshotImg;
        }

        public static Image Snapshot(string iFileName)
        {
            try
            {
                if (frmMain.frmCameraPreview != null)
                {
                    return frmMain.frmCameraPreview.SaveSnapshotImage(iFileName);
                }
            }
            catch (Exception ex)
            {
                Common.WriteLog.tradeLog(LazyAtHome.Winform.FactoryPortal.Common.ConstConfig.WRONG_LOG_PATH, ex.Message);
            }

            return null;
        }
    }
}
