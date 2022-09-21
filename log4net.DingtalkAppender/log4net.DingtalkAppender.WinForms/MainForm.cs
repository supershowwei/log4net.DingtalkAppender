using System;
using System.Windows.Forms;

namespace log4net.DingtalkAppender.WinForms
{
    public partial class MainForm : Form
    {
        private static readonly ILog Log = LogManager.GetLogger("InvestingWebCrawler");

        public MainForm()
        {
            this.InitializeComponent();
        }

        private void OnDebugLogButtonClick(object sender, EventArgs e)
        {
            Log.Debug("This is DEBUG.");
        }

        private void OnErrorLogButtonClick(object sender, EventArgs e)
        {
            Log.Error("This is ERROR.");
        }
    }
}