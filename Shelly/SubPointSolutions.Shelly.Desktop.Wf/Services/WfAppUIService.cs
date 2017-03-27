using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SubPointSolutions.Shelly.Core.Services;
using SubPointSolutions.Shelly.Desktop.Definitions.Base;
using SubPointSolutions.Shelly.Desktop.Definitions.UI;
using SubPointSolutions.Shelly.Desktop.Events.App;
using SubPointSolutions.Shelly.Desktop.Events.UI;

namespace SubPointSolutions.Shelly.Desktop.Wf.Services
{
    public class WfAppUIService : ShAppUIServiceBase
    {
        public override void Init(object uiHostControl)
        {
            var appForm = uiHostControl as Form;

            AppForm = appForm;

            InitUI(appForm);
            InitAppEvents(appForm);
        }

        private void InitAppEvents(Form appForm)
        {
            ReceiveEvent<ShAddAppTopMenuItemEvent>(e => AddAppTopMenu(e.MenuItem));
            ReceiveEvent<ShOnAppLoadCompletedEvent>(e => AppLoadCompleted(e));
            ReceiveEvent<ShAddAppViewWindowEvent>(e => AddAppViewWindow(e.ViewWindowControl));

            //ReceiveEvent<SolutionItemEvent>(e => OnSolutionItemEvent(e));
            ReceiveEvent<ShOnAppExitEvent>(e => OnAppExitEvent(e));
        }

        //private void OnSolutionItemEvent(SolutionItemEvent e)
        //{
        //    switch (e.EventType)
        //    {
        //        case SolutionItemEventType.Open:
        //            AddDocumentTab(e, e.Item);
        //            break;
        //    }
        //}

        private void OnAppExitEvent(ShOnAppExitEvent e)
        {
            Application.Exit();

            //SaveDockingLayout();
        }

        //private void AddDocumentTab(SolutionItemEvent e, object workspaceItem)
        //{
        //    var view = tvDocuments;
        //    var controlTypeValue = e.ControlType;

        //    if (string.IsNullOrEmpty(controlTypeValue))
        //    {
        //        // TODO


        //        //var controlEvent = new GetWorkspaceItemDocumentControlEvent
        //        //{
        //        //    WorkspaceItem = workspaceItem
        //        //};

        //        //RaiseEvent(controlEvent);
        //        //controlTypeValue = controlEvent.ControlType;
        //    }

        //    if (controlTypeValue == null)
        //        return;

        //    var document = view.Documents
        //                       .FirstOrDefault(p => (p.Control is ISolutionItemEditorControl)
        //                                            && (p.Control.Tag == workspaceItem));

        //    view.BeginUpdate();

        //    if (document == null)
        //    {
        //        var type = controlTypeValue;

        //        var controlType = Type.GetType(type);
        //        var control = Activator.CreateInstance(controlType) as ISolutionItemEditorControl;

        //        if (control is Control)
        //            document = view.AddDocument(control as Control);

        //        control.SolutionItemTag = workspaceItem;
        //        control.SolutionItem = workspaceItem;

        //        document.Tag = workspaceItem;
        //        document.Caption = control.SolutionItemTitle;
        //    }

        //    view.Controller.Activate(document);
        //    view.EndUpdate();
        //}

        private void AppLoadCompleted(ShOnAppLoadCompletedEvent e)
        {
            //RestoreDockingLayout();
            _postInitialiseActions.ForEach(a => a());

        }

        private void AddAppViewWindow(ShAppViewWindowControlDefinition appViewWindowControlDefinition)
        {
            try
            {
                var title = appViewWindowControlDefinition.Title;

                var type = Type.GetType(appViewWindowControlDefinition.AssemblyQualifiedName);
                var id = appViewWindowControlDefinition.Id;

                var control = Activator.CreateInstance(type) as UserControl;

                control.Dock = DockStyle.Fill;
                control.Tag = id;

                var panel = new Panel();

                panel.Text = title;
                //panel.BackColor = Color.White;

                panel.Dock = DockStyle.Fill;
                panel.Tag = string.Format("{0}_PanelContainer", id);

                

                panel.Controls.Add(control);

                AppForm.Controls.Add(panel);

                panel.BringToFront();

                // add to top-level view menu
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected List<Action> _postInitialiseActions = new List<Action>();

        private void EnsureViewWindowMenu(string winTitle, Guid winId)
        {
            //FindTopMenuSubItem(bTopBar, ShAppMenuIds.View)
            //    .IfNotNull(menu =>
            //    {
            //        var newItem = new BarButtonItem
            //        {
            //            Caption = winTitle,
            //            Tag = winId
            //        };

            //        newItem.ItemClick += delegate (object sender, ItemClickEventArgs e)
            //        {
            //            dmDockManager.Panels
            //                .FirstOrDefault(p => p.Tag is Guid && (Guid)p.Tag == winId)
            //                .IfNotNull(p => p.Show());
            //        };

            //        menu.ItemLinks.Add(newItem);
            //    });
        }

        private MenuStrip _appMenuStrip;
        private StatusStrip _appStatusStrip;

        private void InitUI(Form appForm)
        {
            _appMenuStrip = new MenuStrip();

            _appMenuStrip.Dock = DockStyle.Top;

            appForm.Controls.Add(_appMenuStrip);

            _appStatusStrip = new StatusStrip();

            _appStatusStrip.Dock = DockStyle.Bottom;

            appForm.Controls.Add(_appStatusStrip);
        }

        private void AddAppTopMenu(ShAppMenuItemDefinitionBase menu)
        {
            var item = new ToolStripMenuItem
            {
                Text = menu.Title,
                Tag = menu.Id
            };

            item.Click += (s, e) =>
            {
                if (menu.Click != null)
                    menu.Click(s, e);
            };

            var parentMenu = LookupParentMenuForTag(_appMenuStrip.Items, menu.ParentItemId);

            if (parentMenu != null)
            {
                if (menu.StartsGroup)
                    parentMenu.DropDownItems.Add(new ToolStripSeparator());

                parentMenu.DropDownItems.Add(item);
            }
            else
            {

                if (menu.StartsGroup)
                    parentMenu.DropDownItems.Add(new ToolStripSeparator());

                _appMenuStrip.Items.Add(item);
            }
        }

        private ToolStripMenuItem LookupParentMenuForTag(ToolStripItemCollection items, Guid? id)
        {
            if (!id.HasValue)
                return null;

            var result = items.OfType<ToolStripMenuItem>()
                              .FirstOrDefault(t => t.Tag is Guid && (Guid)t.Tag == id.Value);

            return result;
        }

        public Form AppForm { get; set; }
    }
}
