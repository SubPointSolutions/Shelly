using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quark.Desktop.Controls;

namespace SubPointSolutions.Shelly.Desktop.Controls
{
    public class ShModalDialogControlClosingEventArgs : EventArgs
    {
        public Form Form { get; set; }
        public ShModalDialogControlBase Control { get; set; }
        public Control ModalControl { get; set; }
        public bool Cancel { get; set; }
    }
}
