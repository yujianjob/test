using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Iyond.Utility
{
    public class AutoUpdater
    {
        const string FILENAME = "update.config";
        private Config config = null;
        private bool bNeedRestart = false;
        string _vesion = "";
        string m_Url = "";

        List<DownloadFileInfo> _downloadList;
        List<LocalFile> preDeleteFile;


        public List<DownloadFileInfo> downloadList { get; set; }

        public AutoUpdater()
        {
            config = Config.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));

        }

        public string GetVesion()
        {
            // return config.Version;
            return _vesion;
        }

        public string GetLocalVesion()
        {
            return config.Version;
        }
        /// <summary>
        /// 检查新版本
        /// </summary>
        /// <exception cref="System.Net.WebException">无法找到指定资源</exception>
        /// <exception cref="System.NotSupportException">升级地址配置错误</exception>
        /// <exception cref="System.Xml.XmlException">下载的升级文件有错误</exception>
        /// <exception cref="System.ArgumentException">下载的升级文件有错误</exception>
        /// <exception cref="System.Excpetion">未知错误</exception>
        /// <returns></returns>
        public bool Update()
        {
            if (!config.Enabled)
                return false;

            //IPAddress myIP = IPAddress.Parse(config.ServerUrl.Substring(2, config.ServerUrl.IndexOf(@"\", 2) - 2));
            //try
            //{
            //    IPHostEntry myHost = Dns.GetHostByAddress(myIP);//svctag-7d12s2x
            //}
            //catch(Exception ex)
            //{
            //    return false;
            //}
            ///*
            // * 请求Web服务器，得到当前最新版本的文件列表，格式同本地的FileList.xml。
            // * 与本地的FileList.xml比较，找到不同版本的文件
            // * 生成一个更新文件列表，开始DownloadProgress
            // * <UpdateFile>
            // *  <File path="" url="" lastver="" size=""></File>
            // * </UpdateFile>
            // * path为相对于应用程序根目录的相对目录位置，包括文件名
            // */

            //LoginRemotingDir(config.ServerUrl.Substring(0, config.ServerUrl.LastIndexOf(@"\")));

            string fileName = config.ServerUrl.Substring(0);
            FtpOperater ftp = new FtpOperater();
            // 下载配置文件到临时文件夹
            ftp.Download(AppDomain.CurrentDomain.BaseDirectory + "temp", fileName);
            File.SetAttributes(AppDomain.CurrentDomain.BaseDirectory + "temp", FileAttributes.Hidden);

            string strXml = string.Empty;
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "temp\\" + "AutoUpdaterList.xml"))
            {
                return false;
            }

            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "temp\\" + "AutoUpdaterList.xml");
                if (fi.Length == 0)
                    return false;

                fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "temp\\" + "AutoUpdaterList.xml", FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                strXml = sr.ReadToEnd();
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }

            // 读取配置文件内容
            Dictionary<string, RemoteFile> listRemotFile = ParseRemoteXml(strXml);

            _downloadList = new List<DownloadFileInfo>();

            //某些文件不再需要了，删除
            preDeleteFile = new List<LocalFile>();

            foreach (LocalFile file in config.UpdateFileList)
            {
                if (listRemotFile.ContainsKey(file.Path))
                {
                    RemoteFile rf = listRemotFile[file.Path];
                    if (rf.LastVer != file.LastVer)
                    {
                        _downloadList.Add(new DownloadFileInfo(rf.Url, file.Path, rf.LastVer, rf.Size));
                        file.LastVer = rf.LastVer;
                        file.Size = rf.Size;

                        if (rf.NeedRestart)
                            bNeedRestart = true;
                    }

                    listRemotFile.Remove(file.Path);
                }
                else
                {
                    preDeleteFile.Add(file);
                }
            }

            foreach (RemoteFile file in listRemotFile.Values)
            {
                _downloadList.Add(new DownloadFileInfo(file.Url, file.Path, file.LastVer, file.Size));
                config.UpdateFileList.Add(new LocalFile(file.Path, file.LastVer, file.Size));

                if (file.NeedRestart)
                    bNeedRestart = true;
            }

            if (_downloadList.Count > 0)
                return true;

            return false;

            //if (downloadList.Count > 0)
            //{
            //    DownloadConfirm dc = new DownloadConfirm(downloadList);

            //    if (this.OnShow != null)
            //        this.OnShow();

            //    if (DialogResult.OK == dc.ShowDialog())
            //    {
            //        foreach (LocalFile file in preDeleteFile)
            //        {
            //            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.Path);
            //            if (File.Exists(filePath))
            //                File.Delete(filePath);

            //            config.UpdateFileList.Remove(file);
            //        }

            //        StartDownload(downloadList);
            //    }
            //}
        }

        public bool StartUpdate()
        {
            if (!Update())
                return true;

            return RunDownload();

        }

        public bool RunDownload()
        {
            foreach (LocalFile file in preDeleteFile)
            {
                if (file.Path != "AutoUpdater.exe" && file.Path != "update.config" && file.Path != "最新更新.txt")
                {
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.Path);
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }

                config.UpdateFileList.Remove(file);
            }
            config.Version = GetVesion();
            return StartDownload(_downloadList);
        }

        //private void LoginRemotingDir(string url)
        //{
        //    //Process p = new Process();
        //    //ProcessStartInfo startInfo = new ProcessStartInfo();
        //    //startInfo.FileName = "cmd.exe  /c net use " + m_Url + " velovip /user:velovip";//+@"\opmanage.exe";// +" velovip /user:velovip";
        //    //p.StartInfo = startInfo;// "cmd.exe  /c net use " + m_Url + " velovip /user:velovip";
        //    //p.Start();
        //    //p.WaitForExit();
        //    //p.Close();

        //    Process p;
        //    p = new Process();
        //    p.StartInfo.FileName = "cmd.exe";


        //    // 这里是关键点,不用Shell启动/重定向输入/重定向输出/不显示窗口 
        //    p.StartInfo.UseShellExecute = false;
        //    p.StartInfo.RedirectStandardInput = true;
        //    p.StartInfo.RedirectStandardOutput = true;
        //    p.StartInfo.CreateNoWindow = true;

        //    p.Start();
        //    p.StandardInput.WriteLine("net use " + url + " velovip /user:velovip");// 向cmd.exe输入command 
        //    p.StandardInput.WriteLine("exit");
        //    p.WaitForExit();
        //    string s = p.StandardOutput.ReadToEnd();// 得到cmd.exe的输出 
        //    p.Close();
        //    //return s;
        //}

        private bool StartDownload(List<DownloadFileInfo> downloadList)
        {
            //LoginRemotingDir(m_Url);

            DownloadProgress dp = new DownloadProgress(downloadList);
            if (dp.ShowDialog() == DialogResult.OK && !dp.isCancel && !dp.isEorrer)
            {
                //更新成功
                config.SaveConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME));

                if (bNeedRestart)
                {
                    MessageBox.Show("程序需要重新启动才能应用更新，请点击确定重新启动程序。", "自动更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(Application.ExecutablePath);
                    Environment.Exit(0);
                }
            }
            if (dp.isEorrer)
            {
                MessageBox.Show("下载文件报错\n请确认是否登录" + m_Url + "文件夹，如未登录请在记住密码复选框中打勾。\n如仍然无法解决问题\n请进入" + m_Url + "获取新版版本");

                return false;
            }
            return true;
        }

        private Dictionary<string, RemoteFile> ParseRemoteXml(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            _vesion = document.DocumentElement.SelectSingleNode("Application").SelectSingleNode("Version").InnerText;
            string url = document.DocumentElement.SelectSingleNode("Updater").FirstChild.InnerXml;
            m_Url = url;
            Dictionary<string, RemoteFile> list = new Dictionary<string, RemoteFile>();
            foreach (XmlNode node in document.DocumentElement.SelectSingleNode("Files").ChildNodes)
            {
                list.Add(node.Attributes["Name"].Value, new RemoteFile(node, url));
            }

            return list;
        }
        public event ShowHandler OnShow;
    }

    public class RemoteFile
    {
        private string path = "";
        private string url = "";
        private string lastver = "";
        private long size = 0;
        private bool needRestart = false;

        public string Path { get { return path; } }
        public string Url { get { return url; } }
        public string LastVer { get { return lastver; } }
        public long Size { get { return size; } }
        public bool NeedRestart { get { return needRestart; } }

        public RemoteFile(XmlNode node, string url)
        {
            this.path = node.Attributes["Name"].Value;
            this.url = url + "\\" + path;
            this.lastver = node.Attributes["LastTime"].Value;
            this.size = Convert.ToInt64(node.Attributes["size"].Value);
            //this.needRestart = Convert.ToBoolean(node.Attributes["needRestart"].Value);
        }
    }

    public class LocalFile
    {
        private string path = "";
        private string lastver = "";
        private long size = 0;

        [XmlAttribute("Name")]
        public string Path { get { return path; } set { path = value; } }
        [XmlAttribute("LastTime")]
        public string LastVer { get { return lastver; } set { lastver = value; } }
        [XmlAttribute("size")]
        public long Size { get { return size; } set { size = value; } }

        public LocalFile(string path, string ver, long size)
        {
            this.path = path;
            this.lastver = ver;
            this.size = size;
        }

        public LocalFile()
        {
        }

    }


    public delegate void ShowHandler();

    public class DownloadFileInfo
    {
        string downloadUrl = "";
        string fileName = "";
        string lastver = "";
        long size = 0;

        /// <summary>
        /// 要从哪里下载文件
        /// </summary>
        public string DownloadUrl { get { return downloadUrl; } }
        /// <summary>
        /// 下载完成后要放到哪里去
        /// </summary>
        public string FileFullName { get { return fileName; } }
        public string FileName { get { return Path.GetFileName(FileFullName); } }
        public string LastVer { get { return lastver; } set { lastver = value; } }
        public long Size { get { return size; } }

        public DownloadFileInfo(string url, string name, string ver, long size)
        {
            this.downloadUrl = url;
            this.fileName = name;
            this.lastver = ver;
            this.size = size;
        }
    }

    public class FtpOperater
    {
        // 服务器地址包括端口
        private string ftpServerIP;
        // 服务器登录用户账号
        private string ftpUser;
        // 服务器登录用户密码
        private string ftpPwd;

        public FtpOperater()
        {
            //this.ftpServerIP = "114.80.81.203";
            //this.ftpUser = "zjnbvrjtu7yjmn";
            //this.ftpPwd = "9klj,gc,9y5762";

            this.ftpServerIP = "newsyue.8866.org:9121";
            this.ftpUser = "landaojia";
            this.ftpPwd = "+rYyKbXs";
        }

        /// <summary>
        /// 获取ftp服务器上的文件信息
        /// </summary>
        /// <returns>存储了所有文件信息的字符串数组</returns>
        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPwd);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);

                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                return downloadFiles;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// 获取FTP上指定文件的大小
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            WebResponse response = null;
            Stream ftpStream = null;

            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPwd);
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
            }
            return fileSize;
        }

        /// <summary>
        /// 实现ftp下载操作
        /// </summary>
        /// <param name="filePath">保存到本地的文件名</param>
        /// <param name="fileName">远程文件名</param>
        public bool Download(string filePath, string fileName)
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
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
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
    }    
}