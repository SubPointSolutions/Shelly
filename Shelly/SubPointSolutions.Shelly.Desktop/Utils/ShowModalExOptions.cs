using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quark.Desktop.Controls;
using SubPointSolutions.Shelly.Desktop.Controls;

namespace SubPointSolutions.Shelly.Desktop.Utils
{
    public class ShowModalExOptions
    {
        #region properties

        public Control Control { get; set; }

        public Action<Form> OnFormSetup;
        public Action<ShModalDialogControlBase> OnControlSetup;

        public Action<object, ShModalDialogControlClosingEventArgs> OnOk;
        public Action<object, ShModalDialogControlClosingEventArgs> OnCancel;

        #endregion
    }
}
