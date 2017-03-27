using System.Windows.Forms;
using SubPointSolutions.Shelly.Core;

namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    public partial class WfAboutControl : UserControl
    {
        public WfAboutControl()
        {
            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            this.Text = string.Format("About");

            lAppTitle.Text = ShServiceContainer.Instance.AppMetadataService.AppName;
            lAppVersion.Text = string.Format("Version: {0}", ShServiceContainer.Instance.AppMetadataService.AppVersion);
        }
    }
}
