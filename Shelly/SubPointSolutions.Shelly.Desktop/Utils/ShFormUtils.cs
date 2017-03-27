using System;
using System.Linq;
using System.Windows.Forms;
using Quark.Desktop.Controls;
using SubPointSolutions.Shelly.Desktop.Controls;
using SubPointSolutions.Shelly.Core;

namespace SubPointSolutions.Shelly.Desktop.Utils
{
    public static class ShFormUtils
    {
        #region confirm

        public static void Confirm(string title, Action ok)
        {
            Confirm(title, ok, null);
        }

        public static void Confirm(string title, Action ok, Action cancel)
        {
            var confirmResult = MessageBox.Show(title,
                                     "Confirm Delettion",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (ok != null)
                    ok.Invoke();
            }
            else
            {
                if (cancel != null)
                    cancel.Invoke();
            }
        }

        #endregion

        #region ShowModal
        public static void ShowModal(Control control)
        {
            ShowModal(control, (Action<object, ShModalDialogControlClosingEventArgs>)null);
        }

        public static void ShowModal(Control control, Action<object, ShModalDialogControlClosingEventArgs> ok)
        {
            ShowModal(control, null, ok, null);
        }

        public static void ShowModal(Control control,
            Action<object, ShModalDialogControlClosingEventArgs> ok,
            Action<object, ShModalDialogControlClosingEventArgs> cancel)
        {
            ShowModal(control, null, ok, cancel);
        }

        public static void ShowModal(Control control, Action<Form> actionForm)
        {
            ShowModal(control, actionForm, null, null);
        }

        public static void ShowModal(Control control, Action<Form> actionForm,
            Action<object, EventArgs> ok)
        {
            ShowModal(control, actionForm, ok, null);
        }

        public static void ShowModal(Control control,
            Action<Form> actionForm,
            Action<object, ShModalDialogControlClosingEventArgs> ok,
            Action<object, ShModalDialogControlClosingEventArgs> cancel)
        {
            ShowModal(control, actionForm, null, ok, cancel);
        }

        public static void ShowModal(Control control,
            Action<Form> actionForm,
            Action<ShModalDialogControlBase> actionControl,
            Action<object, ShModalDialogControlClosingEventArgs> ok,
            Action<object, ShModalDialogControlClosingEventArgs> cancel)
        {
            ShowForm(control, actionForm, actionControl, ok, cancel, true);
        }

        public static void ShowForm(Control control,
            Action<Form> actionForm,
            Action<ShModalDialogControlBase> actionControl,
            Action<object, ShModalDialogControlClosingEventArgs> ok,
            Action<object, ShModalDialogControlClosingEventArgs> cancel,
            bool isModal = true)
        {
            Form form = null;

            var uiService = ShServiceContainer.Instance.AppMetadataService.AppUIServices.FirstOrDefault();

            if (uiService != null)
                form = uiService.CreateControlInstance<Form>();
            else
                form = new Form();

            var modalDialogContainer = new ShModalDialogControlBase();

            control.Dock = DockStyle.Fill;

            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.ShowIcon = false;
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.CenterParent;

            if (!string.IsNullOrEmpty(control.Text))
                form.Text = control.Text;

            modalDialogContainer.Width = control.Width;
            modalDialogContainer.Height = control.Height + modalDialogContainer.PanelBottom.Height + 10;

            form.Width = modalDialogContainer.Width + 60;
            //form.Height = modalDialogContainer.Height + 40;

            form.Width = modalDialogContainer.Width;
            form.Height = modalDialogContainer.Height;

            form.BringToFront();

            //form.FormBorderStyle = FormBorderStyle.FixedSingle;

            modalDialogContainer.PanelBody.Controls.Add(control);
            form.Controls.Add(modalDialogContainer);

            if (actionControl != null)
                actionControl(modalDialogContainer);

            if (actionForm != null)
                actionForm(form);

            modalDialogContainer.OnOk += (s, e) =>
            {
                if (ok != null)
                    ok(s, e);

                if (e.Cancel)
                    return;

                form.DialogResult = DialogResult.OK;
                form.Close();
            };

            modalDialogContainer.OnCancel += (s, e) =>
            {
                if (cancel != null)
                    cancel(s, e);

                if (e.Cancel)
                    return;

                form.DialogResult = DialogResult.Cancel;
                form.Close();
            };

            if (isModal)
                form.ShowDialog();
            else
                form.Show();
        }

        #endregion
    }
}
