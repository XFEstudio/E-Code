using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;

namespace TranslationAPI
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        public static bool RunInStartUp = false;
        [STAThread]
        static void Main(string[] arg)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Mutex instance = new Mutex(true, "MutexName", out bool createdNew);
            if(!File.Exists(Application.UserAppDataPath.Substring(0, Application.UserAppDataPath.LastIndexOf(@"\")) + @"\GetAdmin"))
            {
                if (createdNew)
                {
                    MainForm mainForm = new MainForm();
                    instance.ReleaseMutex();
                    if (arg.Count() > 0 && arg[0] == "StartUp")
                    {
                        RunInStartUp = true;
                        Application.Run(mainForm);
                    }
                    else
                    {
                        Application.Run(mainForm);
                    }
                }
                else
                {
                    FileInfo rd = new FileInfo(Application.UserAppDataPath + @"\Statue.xfe");
                    StreamWriter sw = rd.CreateText();
                    sw.WriteLine("1");
                    sw.Flush();
                    sw.Close();
                    Application.Exit();
                }
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
