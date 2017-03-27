using System;
using System.Drawing;
using System.Windows.Forms;
using Quark.Desktop.Controls;
using SubPointSolutions.Shelly.Core.Exceptions;
using SubPointSolutions.Shelly.Core.Services;
using SubPointSolutions.Shelly.Desktop.Controls;
using SubPointSolutions.Shelly.Desktop.Extensions;
using SubPointSolutions.Shelly.Desktop.Interfaces;
using SubPointSolutions.Shelly.Desktop.Utils;

namespace SubPointSolutions.Shelly.Desktop.Services
{
    public class ShBusinessEntityDataService : ShAppDataServiceBase
    {
        #region properties

        #endregion

        #region methods

        public void EditEntity(string title, object entity,
            Action onOk)
        {
            Type defaultEditorType = ShUtils.GetUIComponentType<IEntityEditorControl>();

            if (defaultEditorType == null)
                throw new ShAppException("Can't find impl for interface IEntityEditorControl");

            EditEntity(defaultEditorType, title, entity, null, onOk, null);
        }

        public void EditEntity<TEditorControl>(string title, object entity,
            Action onOk)
            where TEditorControl : ShModalDialogControlBase, IEntityEditorControl, new()
        {
            EditEntity<TEditorControl>(title, entity, null, onOk, null);
        }

        public void EditEntity<TEditorControl>(string title, object entity,
            Action onOk,
            Action onCancel)
            where TEditorControl : ShModalDialogControlBase, IEntityEditorControl, new()
        {
            EditEntity<TEditorControl>(title, entity, null, onOk, onCancel);
        }

        public void EditEntity<TEditorControl>(string title, object entity, Action<Form> formSettings, Action onOk,
            Action onCancel)
            where TEditorControl : ShModalDialogControlBase, IEntityEditorControl, new()
        {
            EditEntity(typeof(TEditorControl), title, entity, formSettings, onOk, onCancel);
        }

        public void EditEntity(Type editorType,
            string title,
            object entity, Action<Form> formSettings, Action onOk, Action onCancel)
        {
            var form = new Form();

            form.KeyDown += (ss, ee) =>
            {
                if (ee.KeyCode == Keys.Escape)
                {
                    form.Close();

                    if (onCancel != null)
                        onCancel();
                }
            };

            form.ShowIcon = false;
            form.ShowInTaskbar = false;

            form.MinimumSize = new Size(500, 350);
            form.MinimumSize = new Size(500, 350);

            form.MaximizeBox = false;
            form.MinimizeBox = false;

            form.FormBorderStyle = FormBorderStyle.FixedDialog;

            if (!string.IsNullOrEmpty(title))
                form.Text = title;

            form.Icon = null;

            form.Width = 500;
            form.Height = 350;

            form.StartPosition = FormStartPosition.CenterScreen;

            var d = Activator.CreateInstance(editorType) as IEntityEditorControl;

            if (d == null)
                throw new ShAppException("editorType should implement IEntityEditorControl");

            d.SetEntity(entity);

            var modalDialog = d as ShModalDialogControlBase;

            modalDialog.Dock = DockStyle.Fill;
            form.Controls.Add(modalDialog);

            if (onOk != null)
            {
                modalDialog.OnOk += (ss, ee) =>
                {
                    form.Close();
                    onOk();
                };
            }

            if (onCancel != null)
            {
                modalDialog.OnCancel += (ss, ee) =>
                {
                    form.Close();
                    onCancel();
                };
            }

            if (formSettings != null)
                formSettings(form);

            form.ShowDialog();
        }

       

        public void EditSettings(object settings)
        {
            EditSettings(settings, null);
        }

        public void EditSettings(object settings, Action onOk)
        {
            EditSettings(settings, onOk, null);
        }

        public void EditSettings(object settings, Action onOk, Action onCancel)
        {
            var settingsExplorerType = ShUtils.GetUIComponentType<ISettingsEditorControl>();

            if (settingsExplorerType == null)
                throw new ShAppException("Cannot find implementation for type ISettingsEditorControl");


            var instance = Activator.CreateInstance(settingsExplorerType) as ISettingsEditorControl;
            instance.SetOptionsObject(settings);

            var instanceControl = instance as ShUserControlBase;

            ShFormUtils.ShowModal(instanceControl,
                (s, e) =>
                {
                    if (onOk != null)
                        onOk();
                },
                (s, e) =>
                {
                    if (onCancel != null)
                        onCancel();
                });
        }

        public class LongOperationHandle
        {
            public EventHandler OnEnd;
            public Action<string> OnOutput;

            public void End()
            {
                if (OnEnd != null)
                    OnEnd(this, EventArgs.Empty);
            }

            public void Output(string p)
            {
                if (OnOutput != null)
                    OnOutput(p);
            }
        }

        public void WithLongOperation(Action<LongOperationHandle> action)
        {
            var form = new Form
            {
                Width = 750,
                Height = 450
            };

            var hasClose = false;

            try
            {
                form.ShowIcon = false;
                form.ShowInTaskbar = false;

                var longOperationControl = ShUtils.GetUIComponentType<ILongOperationControl>();
                var instance = Activator.CreateInstance(longOperationControl);

                var instanceControl = instance as ShUserControlBase;

                instanceControl.Dock = DockStyle.Fill;

                form.MaximizeBox = false;
                form.MinimizeBox = false;

                form.StartPosition = FormStartPosition.CenterScreen;

                form.Controls.Add(instanceControl);

                form.Shown += (sss, eee) =>
                {
                    var handle = new LongOperationHandle();

                    handle.OnEnd += (rr, tt) =>
                    {
                        form.WithSafeUIUpdate(() =>
                        {
                            form.DialogResult = DialogResult.OK;
                            form.Close();
                        });

                        hasClose = true;
                    };

                    action(handle);
                };

                form.ShowDialog();

                hasClose = true;
            }
            catch (Exception e)
            {
                if (!hasClose)
                {
                    form.WithSafeUIUpdate(() =>
                    {
                        form.DialogResult = DialogResult.OK;
                        form.Close();
                    });
                }

                throw e;
            }
        }

        #endregion
    }
}