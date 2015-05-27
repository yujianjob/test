using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyAtHome.Winform.FactoryPortal.PRService;
using System.Reflection;
using System.IO;

namespace LazyAtHome.Winform.FactoryPortal.UI
{
    public partial class frmLogin : Form
    {
        //主窗体对象
        public frmMain fm;
        //业务逻辑对象
        private Business.Operator business = null;

        public frmLogin(frmMain Main)
        {
            this.fm = Main;
            if (business == null)
            {
                business = new Business.Operator();
            }

            InitializeComponent();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            //读注册表
            ReadRegistry();

            //获取版本号
            readVersion();

            MoveAutoUpdate();

            txtLoginName.Focus();

            if (IsAutoUpdate)
                StartAutoUpdate();
            else
                pnlAutoUp.Visible = false;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginCredentials login = new LoginCredentials();
            login.LoginName = this.txtLoginName.Text;
            login.Password = this.txtPassword.Text;
            login.OperatorType = OperatorType.Factory;


            OperatorDC entity = business.Login(login);
            if (entity != null)
            {
                Common.ConstConfig.currOperator = entity;
                Common.ConstConfig.currPwd = this.txtPassword.Text;

                //写注册表
                WriteRegistry();

                if (this.fm != null)
                {
                    this.fm.Visible = true;
                }
                else
                {
                    fm = new frmMain(this);
                    fm.Show();
                }

                this.Visible = false;
            }
            else
            {
                MessageBox.Show(business.LastError, "错误");
                txtLoginName.Focus();
            }


        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        #region 注册表相关

        private Microsoft.Win32.RegistryKey parentKey;
        private bool bolExistKey = false;

        /// <summary>
        /// 写注册表
        /// </summary>
        private void WriteRegistry()
        {
            try
            {
                parentKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SoftWare", true).OpenSubKey("LazyAtHomeFactory", true);
            }
            catch
            {
                bolExistKey = false;
            }
            if (!bolExistKey)
            {
                parentKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SoftWare", true).CreateSubKey("LazyAtHomeFactory");
            }
            //写入注册表


            parentKey.SetValue("LazyAtHomeFactoryLoginName", txtLoginName.Text);
            parentKey.SetValue("LazyAtHomeFactoryPassword", Common.Function.Encrypt(txtPassword.Text, "CtrlAltD"));

            if (chkRemember.Checked)
            {
                parentKey.SetValue("LazyAtHomeFactoryNtLogin", "1");
            }
            else
            {
                parentKey.SetValue("LazyAtHomeFactoryNtLogin", "0");
            }

        }

        /// <summary>
        /// 读注册表
        /// </summary>
        private void ReadRegistry()
        {
            try
            {
                if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SoftWare").OpenSubKey("LazyAtHomeFactory").GetValue("LazyAtHomeFactoryNtLogin").ToString() == "0")
                {
                    chkRemember.Checked = false;
                }
                else
                {
                    chkRemember.Checked = true;
                    txtLoginName.Text = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SoftWare").OpenSubKey("LazyAtHomeFactory").GetValue("LazyAtHomeFactoryLoginName").ToString();
                    txtPassword.Text = Common.Function.Decrypt(Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SoftWare").OpenSubKey("LazyAtHomeFactory").GetValue("LazyAtHomeFactoryPassword").ToString(), "CtrlAltD");
                }
            }
            catch
            {
                //Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SoftWare", true).CreateSubKey("OPManage");
                //MessageBox.Show(k.Message, k.Source + k.GetHashCode(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        #endregion





        /// <summary>
        /// 获取版本号
        /// </summary>
        private void readVersion()
        {
            string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblVersion.Text = string.Format("懒到家工厂系统   V {0}", Version);
        }


        #region 自动更新

        private bool IsAutoUpdate = true;
        private string m_AutoUpdatePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater.exe";
        Iyond.Utility.AutoUpdater oAutoUpdater;

        public frmLogin(string autoUp)
        {
            if ("updated".Equals(autoUp))
                IsAutoUpdate = false;

            if (business == null)
            {
                business = new Business.Operator();
            }

            InitializeComponent();
        }

        private void MoveAutoUpdate()
        {
            string path = m_AutoUpdatePath + ".tmp";
            if (File.Exists(path))
            {
                File.Delete(m_AutoUpdatePath);
                File.Move(path, m_AutoUpdatePath);
            }
        }


        private void StartAutoUpdate()
        {
            if (oAutoUpdater != null)
                oAutoUpdater = null;
            oAutoUpdater = new Iyond.Utility.AutoUpdater();
            bool result = false;
            try
            {
                result = oAutoUpdater.Update();
            }
            catch (Exception ex)
            { }
            if (!result)
            {
                pnlAutoUp.Visible = false;
                return;
            }
            string strRemoteVersion = oAutoUpdater.GetVesion();
            string strLocalVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //如果本地版本号大于远程版本号,默认不需要更新
            if (string.Compare(strLocalVersion, strRemoteVersion) > 0)
            {
                pnlAutoUp.Visible = false;
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "temp"))
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "temp", true);
                return;
            }
            else if (string.Compare(strLocalVersion, strRemoteVersion) == 0)
                lblUpStatus.Text = "发现有文件需要更新";
            else
                lblUpStatus.Text = "发现新版本： V " + strRemoteVersion;

            btnUpt.Enabled = true;

        }

        private void btnUpt_Click(object sender, EventArgs e)
        {
            //备份旧版的程序到History目录下
            string Vision = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            CopyFolder(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.BaseDirectory + @"Histroy\" + Vision + @"\");

            System.Diagnostics.Process.Start(m_AutoUpdatePath);
            Application.Exit();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAutoUp.Visible = false;
        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="SourcePath">原始路径</param>
        /// <returns></returns>
        public static bool CreateFolder(string SourcePath)
        {
            try
            {
                Directory.CreateDirectory(SourcePath);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 复制文件夹[循环遍历]
        /// </summary>
        /// <param name="SourcePath">原始路径</param>
        /// <param name="DestinPath">目地的路径</param>
        /// <returns></returns>
        public static bool CopyFolder(string SourcePath, string DestinPath)
        {
            if (Directory.Exists(SourcePath))
            {
                CreateFolder(DestinPath);//第一次创建跟目录文件夹
                DirectoryInfo di = new DirectoryInfo(SourcePath);
                FileInfo[] fi = di.GetFiles();
                foreach (FileInfo f in fi)
                {
                    File.Copy(f.FullName, DestinPath + f.Name, true);
                }
                return true;
            }
            else
                return false;
            //if (Directory.Exists(SourcePath))
            //{
            //    CreateFolder(DestinPath);//第一次创建跟目录文件夹
            //    string sourcePath = SourcePath;//[变化的]原始路径
            //    string destinPath = DestinPath;//[变化的]目地的路径
            //    Queue source = new Queue();//存原始文件夹路径
            //    Queue destin = new Queue();//存目地的文件夹路径
            //    bool IsHasChildFolder = true;//是否有子文件夹
            //    string tempDestinPath = string.Empty;//临时目地的,将被存于destin中
            //    while (IsHasChildFolder)
            //    {
            //        string[] fileList = Directory.GetFileSystemEntries(sourcePath);// 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
            //        for (int i = 0; i < fileList.Length; i++)// 遍历所有的文件和目录
            //        {
            //            tempDestinPath = destinPath + "\\" + Path.GetFileName(fileList[i]);//取得子文件路径
            //            if (Directory.Exists(fileList[i]))//存在文件夹时
            //            {
            //                source.Enqueue(fileList[i]);//当前的子目录的原始路径进队列
            //                destin.Enqueue(tempDestinPath);//当前的子目录的目地的路径进队列
            //                CreateFolder(tempDestinPath);//创建子文件夹
            //            }
            //            else//存在文件
            //            {
            //                File.Copy(fileList[i], tempDestinPath, true);//复制文件
            //            }
            //        }
            //        if (source.Count > 0 && source.Count == destin.Count)//存在子文件夹时
            //        {
            //            sourcePath = source.Dequeue();
            //            destinPath = destin.Dequeue();
            //        }
            //        else
            //        {
            //            IsHasChildFolder = false;
            //        }
            //    }
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }


        #endregion





    }
}
