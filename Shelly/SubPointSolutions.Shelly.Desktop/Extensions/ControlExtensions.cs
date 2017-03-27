using System;
using System.Windows.Forms;

namespace SubPointSolutions.Shelly.Desktop.Extensions
{
    public static class ControlExtensions
    {
        public static void WithSafeUIUpdate(this Control control, Action a)
        {
            InvokeIfRequired(control, () => { a(); });
        }

        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control == null)
                return;

            //When the form, thus the control, isn't visible yet, InvokeRequired returns false, resulting still in a cross-thread exception.
            //while (!control.Visible)
            //{
            //    System.Threading.Thread.Sleep(50);
            //}

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
