using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Iyond.Utility
{
    public partial class DownloadProgress : Form
    {
        private bool isFinished = false;
        public bool isCancel = false;
        public bool isEorrer = false;
        private List<DownloadFileInfo> downloadFileList = null;
        private ManualResetEvent evtDownload = null;
        private ManualResetEvent evtPerDonwload = null;
        //Delete(B) by wubing 20101119
        //private WebClient clientDownload = null;
        //Delete(E) by wubing 20101119
        // 服务器地址包括端口
        private string ftpServerIP;
        // 服务器登录用户账号
        private string ftpUser;
        // 服务器登录用户密码
        private string ftpPwd;

        public DownloadProgress(List<DownloadFileInfo> downloadFileList)
        {
            InitializeComponent();

            this.downloadFileList = downloadFileList;

        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isFinished && DialogResult.No == MessageBox.Show("当前正在更新，是否取消？", "自动升级", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                //Delete(B) by wubing 20101119
                //if (clientDownload != null)
                //    clientDownload.CancelAsync();
                //Delete(E) by wubing 20101119

                evtDownload.Set();
                evtPerDonwload.Set();
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            evtDownload = new ManualResetEvent(true);
            evtDownload.Reset();
            Thread t = new Thread(new ThreadStart(FTPDownload));
            t.Name = "download";
            t.Start();
        }

        long total = 0;
        long nDownloadedTotal = 0;

        //private void ProcDownload()
        //{
        //    evtPerDonwload = new ManualResetEvent(false);

        //    foreach (DownloadFileInfo file in this.downloadFileList)
        //    {
        //        total += file.Size;
        //    }

        //    while (!evtDownload.WaitOne(0, false))
        //    {
        //        if (this.downloadFileList.Count == 0)
        //            break;

        //        DownloadFileInfo file = this.downloadFileList[0];


        //        //Debug.WriteLine(String.Format("Start Download:{0}", file.FileName));

        //        this.ShowCurrentDownloadFileName(file.FileName);

        //        //下载
        //        clientDownload = new WebClient();
        //        clientDownload.Credentials = new NetworkCredential(BasicFTPClient.Username, BasicFTPClient.Password);

        //        clientDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);
        //        clientDownload.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);

        //        evtPerDonwload.Reset();

        //        CreateDir(AppDomain.CurrentDomain.BaseDirectory, file.FileFullName);

        //        clientDownload.DownloadFileAsync(new Uri(file.DownloadUrl), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileFullName + ".tmp"), file);

        //        //等待下载完成
        //        evtPerDonwload.WaitOne();

        //        clientDownload.Dispose();
        //        clientDownload = null;

        //        //移除已下载的文件
        //        this.downloadFileList.Remove(file);
        //    }

        //    //Debug.WriteLine("All Downloaded");

        //    if (isCancel)
        //        Exit(false);
        //    {
        //        if (this.downloadFileList.Count == 0)
        //            Exit(true);
        //        else
        //            Exit(false);
        //    }

        //    evtDownload.Set();
        //}

        /// <summary>
        /// FTP下载文件
        /// </summary>
        private void FTPDownload()
        {
            //this.ftpServerIP = "114.80.81.203";
            //this.ftpUser = "zjnbvrjtu7yjmn";
            //this.ftpPwd = "9klj,gc,9y5762";
            this.ftpServerIP = "newsyue.8866.org:9121";
            this.ftpUser = "landaojia";
            this.ftpPwd = "+rYyKbXs";

            evtPerDonwload = new ManualResetEvent(false);

            foreach (DownloadFileInfo file in this.downloadFileList)
            {
                total += file.Size;
            }

            // 下载类
            FtpOperater ftp = null;
            // 下载文件返回值
            bool blnRet = false;

            while (!evtDownload.WaitOne(0, false))
            {
                if (this.downloadFileList.Count == 0)
                    break;

                DownloadFileInfo file = this.downloadFileList[0];

                this.ShowCurrentDownloadFileName(file.FileName);

                CreateDir(AppDomain.CurrentDomain.BaseDirectory, file.FileFullName);

                //ftp = new FtpOperater();
                //// 下载文件返回值
                //blnRet = ftp.Download(AppDomain.CurrentDomain.BaseDirectory.Trim(new char[] { '\\' }), file.FileFullName);

                //if (!blnRet)
                //{
                //    continue;
                //}

                //this.SetProcessBar(100, 100);

                DownloadByStep(AppDomain.CurrentDomain.BaseDirectory.Trim(new char[] { '\\' }), file.FileFullName);

                //nDownloadedTotal += file.Size;
                //this.SetProcessBar(0, (int)(nDownloadedTotal * 100 / total));

                evtPerDonwload.Reset();

                //等待下载完成
                // Modify(B) by wubing 20101119
                //evtPerDonwload.WaitOne();
                evtPerDonwload.WaitOne(1000, false);
                // Modify(E) by wubing 20101119                

                //clientDownload.Dispose();
                //clientDownload = null;

                //移除已下载的文件
                this.downloadFileList.Remove(file);
                if (this.downloadFileList.Count == 0)
                    break;
            }
            //ff.close();
            if (isCancel)
                Exit(false);
            {
                if (this.downloadFileList.Count == 0)
                {
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "temp"))
                    {
                        Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "temp", true);
                    }
                    Exit(true);
                }
                else
                    Exit(false);
            }

            evtDownload.Set();
        }

        /// <summary>
        /// 实现ftp下载操作
        /// </summary>
        /// <param name="filePath">保存到本地的文件名</param>
        /// <param name="fileName">远程文件名</param>
        private bool DownloadByStep(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            // 返回值
            bool blnRet = false;

            FileStream outputStream = null;
            FtpWebResponse response = null;
            Stream ftpStream = null;

            try
            {
                //filePath = <<The full path where the file is to be created.>>,
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                string dir = filePath + "\\" + fileName.Replace('/', '\\');
                dir = dir.Substring(0, dir.LastIndexOf('\\'));
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                outputStream = new FileStream(filePath + "\\" + fileName.Replace('/', '\\'), FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName.Replace('\\', '/')));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPwd);
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                nDownloadedTotal += readCount;
                this.SetProcessBar(0, (int)(nDownloadedTotal * 100 / total));
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    nDownloadedTotal += readCount;
                    this.SetProcessBar(0, (int)(nDownloadedTotal * 100 / total));
                }

                blnRet = true;
            }
            catch (Exception ex)
            {
                blnRet = false;
            }
            finally
            {
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
                if (outputStream != null)
                {
                    outputStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }

            return blnRet;
        }

        void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadFileInfo file = e.UserState as DownloadFileInfo;
            nDownloadedTotal += file.Size;
            this.SetProcessBar(0, (int)(nDownloadedTotal * 100 / total));
            //Debug.WriteLine(String.Format("Finish Download:{0}", file.FileName));
            //替换现有文件
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileFullName);
            string fileOldPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "old\\", file.FileFullName);

            CreateDir(AppDomain.CurrentDomain.BaseDirectory + "old\\", file.FileFullName);
            FileInfo fi = new FileInfo(filePath + ".tmp");

            if (File.Exists(filePath))
            {
                if (File.Exists(fileOldPath + ".old"))
                    File.Delete(fileOldPath + ".old");

                if (fi.Length != 0)
                    File.Move(filePath, fileOldPath + ".old");
            }

            if (fi.Length == 0)
                isEorrer = true;

            if (!"AutoUpdater.exe".Equals(file.FileName) || fi.Length != 0)
                File.Move(filePath + ".tmp", filePath);

            //继续下载其它文件
            evtPerDonwload.Set();
        }

        void CreateDir(string dir, string fileName)
        {
            // DirectoryInfo di = new DirectoryInfo(dir);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (fileName.LastIndexOf("\\") <= 0)
                return;
            string subDir = fileName.Substring(0, fileName.LastIndexOf("\\"));
            Directory.CreateDirectory(dir + subDir); //.CreateSubdirectory("sss\\bbb");
        }

        void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.SetProcessBar(e.ProgressPercentage, (int)((nDownloadedTotal + e.BytesReceived) * 100 / total));
        }

        delegate void ShowCurrentDownloadFileNameCallBack(string name);
        private void ShowCurrentDownloadFileName(string name)
        {
            if (this.labelCurrentItem.InvokeRequired)
            {
                ShowCurrentDownloadFileNameCallBack cb = new ShowCurrentDownloadFileNameCallBack(ShowCurrentDownloadFileName);
                this.Invoke(cb, new object[] { name });
            }
            else
            {
                this.labelCurrentItem.Text = name;
            }
        }

        delegate void SetProcessBarCallBack(int current, int total);
        private void SetProcessBar(int current, int total)
        {

            if (this.progressBarCurrent.InvokeRequired)
            {
                if (isCancel)
                    return;

                SetProcessBarCallBack cb = new SetProcessBarCallBack(SetProcessBar);
                this.Invoke(cb, new object[] { current, total });
            }
            else
            {
                this.progressBarCurrent.Value = current;
                this.progressBarTotal.Value = total;
            }
        }

        delegate void ExitCallBack(bool success);
        private void Exit(bool success)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    ExitCallBack cb = new ExitCallBack(Exit);
                    this.Invoke(cb, new object[] { success });
                }
                catch (Exception e)
                {

                }

            }
            else
            {
                this.isFinished = success;
                this.DialogResult = success ? DialogResult.OK : DialogResult.Cancel;
                this.Close();
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            //  Exit(true);
            this.isCancel = true;
            evtDownload.Set();
            evtPerDonwload.Set();
        }
    }
}