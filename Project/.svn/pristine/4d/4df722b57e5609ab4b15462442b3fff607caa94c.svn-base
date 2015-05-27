using Camera_NET;
using LazyAtHome.Winform.FactoryPortal.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyAtHome.Winform.FactoryPortal.UI.Camera
{
    public partial class frmSetup : Form
    {
        public frmSetup()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        private Camera_NET.CameraControl pnlCamera;
        public CameraChoice CameraChoice = new CameraChoice();

        public void frmSetup_Load(object sender, EventArgs e)
        {
            GetCamera();

            //设备列表
            FillCameraList();

            if (this.cbxCameraList.Items.Count > 0)
            {
                this.cbxCameraList.SelectedIndex = 0;
            }

            SetCamera();
        }

        private void GetCamera()
        {
            this.pnlCamera = (this.Tag as frmPreview).pnlCamera;
        }

        //设备列表
        private void FillCameraList()
        {
            this.cbxCameraList.Items.Clear();

            this.CameraChoice.UpdateDeviceList();

            foreach (var camera_device in this.CameraChoice.Devices)
            {
                this.cbxCameraList.Items.Add(camera_device.Name);
            }
        }

        private void FillResolutionList()
        {
            this.cbxResolutionList.Items.Clear();

            if (!this.pnlCamera.CameraCreated)
                return;

            ResolutionList resolutions = Camera_NET.Camera.GetResolutionList(this.pnlCamera.Moniker);

            if (resolutions == null)
                return;

            int index_to_select = -1;

            for (int index = 0; index < resolutions.Count; index++)
            {
                this.cbxResolutionList.Items.Add(resolutions[index].ToString());

                if (resolutions[index].CompareTo(this.pnlCamera.Resolution) == 0)
                {
                    index_to_select = index;
                }
            }

            // select current resolution
            if (index_to_select >= 0)
            {
                this.cbxResolutionList.SelectedIndex = index_to_select;
            }
        }

        //设备选择变更
        private void cbxCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxCameraList.SelectedIndex < 0)
            {
                this.pnlCamera.CloseCamera();
            }
            else
            {
                this.pnlCamera.SetCamera(this.CameraChoice.Devices[this.cbxCameraList.SelectedIndex].Mon, null);
            }

            FillResolutionList();
        }

        //分辨率选择变更
        private void cbxResolutionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.pnlCamera.CameraCreated)
                return;

            int comboBoxResolutionIndex = this.cbxResolutionList.SelectedIndex;
            if (comboBoxResolutionIndex < 0)
            {
                return;
            }

            ResolutionList resolutions = Camera_NET.Camera.GetResolutionList(this.pnlCamera.Moniker);

            if (resolutions == null)
                return;

            if (comboBoxResolutionIndex >= resolutions.Count)
                return; // throw

            if (0 == resolutions[comboBoxResolutionIndex].CompareTo(this.pnlCamera.Resolution))
            {
                // this resolution is already selected
                return;
            }

            this.pnlCamera.SetCamera(this.pnlCamera.Moniker, resolutions[comboBoxResolutionIndex]);
        }

        //读取配置
        public void SetCamera()
        {
            var cameraName = System.Configuration.ConfigurationManager.AppSettings["Camera_Device"];
            var cameraResolution = System.Configuration.ConfigurationManager.AppSettings["Camera_Resolution"];

            if (string.IsNullOrWhiteSpace(cameraName) || string.IsNullOrWhiteSpace(cameraResolution))
            {
                //配置为空
                //this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                //this.Close();
                this.cbxCameraList.DropDownStyle = ComboBoxStyle.DropDown;
                this.cbxResolutionList.DropDownStyle = ComboBoxStyle.DropDown;
                MessageBox.Show("找不到配置");
            }

            for (int i = 0; i < this.cbxCameraList.Items.Count; i++)
            {
                if (this.cbxCameraList.Items[i].ToString() == cameraName)
                {
                    this.cbxCameraList.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < this.cbxResolutionList.Items.Count; i++)
            {
                if (this.cbxResolutionList.Items[i].ToString() == cameraResolution)
                {
                    this.cbxResolutionList.SelectedIndex = i;
                    break;
                }
            }

            if (this.cbxCameraList.SelectedIndex > 0 && this.cbxResolutionList.SelectedIndex > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }

        }

        //保存配置
        private void SaveConfig()
        {
            //System.Configuration.ConfigurationManager.AppSettings.Set("Camera_Device", this.cbxCameraList.SelectedText);
            //System.Configuration.ConfigurationManager.AppSettings.Set("Camera_Resolution", this.cbxResolutionList.SelectedText);

            //System.Configuration.Configuration config =
            //   System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //// You need to remove the old settings object before you can replace it

            //config.AppSettings.Settings["Camera_Device"].Value = this.cbxCameraList.SelectedText;
            //config.AppSettings.Settings["Camera_Resolution"].Value = this.cbxResolutionList.SelectedText;

            //// Save the changes in App.config file.
            //config.Save(System.Configuration.ConfigurationSaveMode.Full);//我发现这个方法其实并没有将配置保存到磁盘，怎么这么奇怪呢
            //// Force a reload of a changed section.
            //System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
