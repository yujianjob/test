using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Iyond.Utility
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DownloadConfirm());

            try
            {
                AutoUpdater oAutoUpdater = new AutoUpdater();
                oAutoUpdater.StartUpdate();
                string tmpPath = AppDomain.CurrentDomain.BaseDirectory;
                System.Diagnostics.Process.Start(tmpPath + "LazyAtHome.Winform.FactoryPortal.exe", "updated");
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            Application.Exit();
        }
    }
}
