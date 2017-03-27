using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using SubPointSolutions.Shelly.Core.Exceptions;
using SubPointSolutions.Shelly.Core.Services;
using SubPointSolutions.Shelly.Desktop.Definitions.Base;
using SubPointSolutions.Shelly.Desktop.Definitions.UI;
using SubPointSolutions.Shelly.Desktop.Events.App;
using SubPointSolutions.Shelly.Desktop.Events.UI;
using System.ComponentModel;
using System.Drawing;

namespace SubPointSolutions.Shelly.Desktop.MetroFramework.Services
{
    public class MetroFrameworkAppUIService : ShAppUIServiceBase
    {
        #region cconstructors

        public MetroFrameworkAppUIService()
        {
            InitControlTypes();
        }

        #endregion

        #region constructors
        public override void Init(object uiHostControl)
        {
            var appForm = uiHostControl as Form;

            AppForm = appForm;

            InitUI(appForm);
            InitAppEvents(appForm);
        }

        private void InitControlTypes()
        {
            _controlTypes.Add(typeof(Form), typeof(MetroForm));
            //_controlTypes.Add(typeof(LinkLabel), typeof(MetroLink));
            _controlTypes.Add(typeof(TextBox), typeof(MetroTextBox));
            _controlTypes.Add(typeof(ComboBox), typeof(MetroComboBox));
            _controlTypes.Add(typeof(CheckBox), typeof(MetroCheckBox));
            _controlTypes.Add(typeof(DataGridView), typeof(MetroGrid));
        }
        #endregion

        #region properties

        public Form AppForm { get; set; }

        #endregion

        //public override TControl CreateControlInstance<TControl>()
        //{
        //    if(typeof(TControl) == typeof(Form))
        //        return new MetroForm();

        //    return base.CreateControlInstance<TControl>();
        //}

        private Dictionary<Type, Type> _controlTypes = new Dictionary<Type, Type>();

        public override TControl CreateControlInstance<TControl>()
        {
            var targetType = typeof(TControl);

            if (_controlTypes.ContainsKey(targetType))
            {
                var instance = Activator.CreateInstance(_controlTypes[targetType]);
                return instance as TControl;
            }

            return base.CreateControlInstance<TControl>();
        }

        private void InitUI(Form appForm)
        {

        }

        private void InitAppEvents(Form appForm)
        {
            ReceiveEvent<ShOnAppLoadCompletedEvent>(e => AppLoadCompleted(e));
            ReceiveEvent<ShAddAppViewWindowEvent>(e => AddAppViewWindow(e.ViewWindowControl));
            ReceiveEvent<ShOnAppExitEvent>(e => OnAppExitEvent(e));
        }

        private void OnAppExitEvent(ShOnAppExitEvent e)
        {
            Application.Exit();
        }

        private void AppLoadCompleted(ShOnAppLoadCompletedEvent e)
        {

        }

        private MetroTabControl _tabControl;



        private void AddAppViewWindow(ShAppViewWindowControlDefinition appViewWindowControlDefinition)
        {
            try
            {
                var tabControl = EnsureMainLayout();


                var viewId = appViewWindowControlDefinition.Id;
                var viewTitle = appViewWindowControlDefinition.Title;

                var newTabPage = new MetroTabPage();

                newTabPage.Text = viewTitle;
                newTabPage.Tag = appViewWindowControlDefinition;

                if (!string.IsNullOrEmpty(appViewWindowControlDefinition.AssemblyQualifiedName))
                {
                    var viewName = appViewWindowControlDefinition.AssemblyQualifiedName;
                    var viewType = Type.GetType(viewName);

                    if (viewType == null)
                    {
                        throw new ShAppException(
                            string.Format("Cannot find type:[{0}]", viewName));
                    }

                    var control = Activator.CreateInstance(viewType) as UserControl;

                    if (control == null)
                    {
                        throw new ShAppException(
                            string.Format("Cannot create an instance of the type:[{0}] as UserControl", viewType));
                    }

                    control.Dock = DockStyle.Fill;
                    control.Tag = viewId;

                    control.BackColor = Color.White;

                    newTabPage.Controls.Add(control);
                }

                // MetroUI fix
                var scrolls = newTabPage.Controls.OfType<MetroScrollBar>().ToArray();
                foreach (var scroll in scrolls)
                    scroll.Visible = false;

                tabControl.TabPages.Add(newTabPage);
                tabControl.SelectedIndex = 0;

            }
            catch (Exception e)
            {
                throw new ShAppException(
                    string.Format("Error while initialting the view:[{0}] of type:[{1}]",
                    appViewWindowControlDefinition.Title,
                    appViewWindowControlDefinition.AssemblyQualifiedName),
                    e);
            }
        }

        private MetroTabControl EnsureMainLayout()
        {
            if (_tabControl == null)
            {
                _tabControl = new MetroTabControl();
                _tabControl.Dock = DockStyle.Fill;

                _tabControl.Selecting += _tabControl_Selecting;

                this.AppForm.Controls.Add(_tabControl);
            }

            return _tabControl;
        }

        void _tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            var def = e.TabPage.Tag as ShAppViewWindowControlDefinition;

            if (def != null)
            {
                if (def.Click != null)
                {
                    var cancelArgs = new CancelEventArgs();

                    def.Click(sender, cancelArgs);

                    e.Cancel = cancelArgs.Cancel;
                }
            }
        }
    }
}
