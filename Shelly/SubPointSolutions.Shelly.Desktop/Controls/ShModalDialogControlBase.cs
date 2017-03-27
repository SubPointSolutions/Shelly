using System;
using System.Linq;
using System.Windows.Forms;
using SubPointSolutions.Shelly.Desktop.Controls;

namespace Quark.Desktop.Controls
{
    public partial class ShModalDialogControlBase : ShUserControlBase
    {
        #region constructors

        public ShModalDialogControlBase()
        {
            InitializeComponent();

            InitUI();
        }

        private void InitUI()
        {
            // events
            bOk.Click += OnOkClick;
            bCancel.Click += OnCancelClick;

            // UI
            PanelBody.Padding = new Padding(5);
        }

        #endregion

        #region properties

        public Panel PanelBottom
        {
            get { return pBottom; }
        }

        public Panel PanelBody
        {
            get { return pMainContent; }
        }

        public Button OkButton
        {
            get { return bOk; }
        }

        public Button CancelButton
        {
            get { return bCancel; }
        }

        protected Control ModalControl
        {
            get
            {
                if (Controls.Count > 0)
                    return Controls[0];

                return null;
            }
        }

        #endregion

        #region events

        public event EventHandler<ShModalDialogControlClosingEventArgs> OnOk;

        public event EventHandler<ShModalDialogControlClosingEventArgs> OnCancel;

        #endregion

        #region methods

        public void TsEnableOkButton(bool enable)
        {
            this.WithSafeUIUpdate(() => { bOk.Enabled = enable; });
        }

        public void TsEnableCancelButton(bool enable)
        {
            this.WithSafeUIUpdate(() => { bCancel.Enabled = enable; });
        }

        protected virtual void OnCancelClick(object sender, EventArgs e)
        {
            if (OnCancel != null)
            {
                OnCancel(sender, new ShModalDialogControlClosingEventArgs()
                {
                    Form = this.ParentForm,
                    Control = this,
                    ModalControl = this.ModalControl
                });
            }
        }

        protected virtual void OnOkClick(object sender, EventArgs e)
        {
            if (OnOk != null)
            {
                OnOk(sender, new ShModalDialogControlClosingEventArgs()
                {
                    Form = this.ParentForm,
                    Control = this,
                    ModalControl = this.ModalControl
                });
            }
        }

        #endregion
    }

   
}