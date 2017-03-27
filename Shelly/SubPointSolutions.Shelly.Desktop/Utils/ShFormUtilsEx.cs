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
    public static class ShFormUtilsEx
    {
        public static void ShowModal(Control control)
        {
            ShowModal(new ShowModalExOptions
            {
                Control = control
            });
        }

        public static void ShowModal(ShowModalExOptions options)
        {
            ShFormUtils.ShowModal(options.Control, options.OnFormSetup, options.OnControlSetup, options.OnOk, options.OnCancel);
        }
    }

   
}
