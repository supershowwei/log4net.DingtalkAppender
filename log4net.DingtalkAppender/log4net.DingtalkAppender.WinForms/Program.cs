using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net.Config;

namespace log4net.DingtalkAppender.WinForms
{
    internal static class Program
    {
        private static readonly string CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        ///     應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            InitialLog4NetConfiguration();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void InitialLog4NetConfiguration()
        {
            var configFile = Path.Combine(CurrentDirectory, "log4net.config");

            GlobalContext.Properties["ApplicationName"] = Assembly.GetExecutingAssembly().GetName().Name;
            GlobalContext.Properties["CurrentDirectory"] = CurrentDirectory;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
        }
    }
}