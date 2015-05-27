using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyAtHome.Winform.FactoryPortal
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new UI.frmLogin(null));


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string update = "";
            if (args.Length > 0)
                update = args[0];
            Application.Run(new UI.frmLogin(update));
        }
    }
}
