using System.Windows.Forms;
using SubPointSolutions.Shelly.Desktop.Extensions;

namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    public partial class WfOutputControl : UserControl
    {
        public WfOutputControl()
        {
            Text = "Output";

            InitializeComponent();

        }

        public void TsCleanOutput()
        {
            this.WithSafeUIUpdate(() =>
            {
                lbLog.Items.Clear();

            });
        }

        public void TsAddOutput(string message)
        {
            this.WithSafeUIUpdate(() =>
            {
                if (string.IsNullOrEmpty(message))
                    return;

                lbLog.Items.Add(message);

                if (lbLog.Items.Count > 0)
                    lbLog.SelectedIndex = lbLog.Items.Count - 1;
            });
        }
    }
}
