using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Streamlet.xlFlashPlayer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //if (args.Length > 0)
            //{
            //    Application.Run(new MainForm(args[0]));
            //}
            //else
            //{
            try
            {
                Application.Run(new MainForm());
            }
            catch
            {
                MessageBox.Show("程序初始化错误。\r\n\r\n本程序需要 Flash Player ActiveX 控件支持，您的系统中可能没有安装，请先安装。", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // http://www.adobe.com/go/getflashplayer/
                Application.Exit();
            }
            //}
        }
    }
}
