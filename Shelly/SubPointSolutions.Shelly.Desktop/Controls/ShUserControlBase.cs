using SubPointSolutions.Shelly.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SubPointSolutions.Shelly.Core;
using SubPointSolutions.Shelly.Core.Interfaces;
using SubPointSolutions.Shelly.Desktop.Definitions.UI;
using SubPointSolutions.Shelly.Desktop.Plugins;

namespace SubPointSolutions.Shelly.Desktop.Controls
{
    public class ShUserControlBase : UserControl
    {
        #region constructors

        public ShUserControlBase()
        {
        }

        #endregion

        #region properties

        protected ShAppMetadataService AppMetadataService
        {
            get { return ShServiceContainer.Instance.AppMetadataService as ShAppMetadataService; }
        }

        #endregion

        protected IAppEventService Hub
        {
            get { return ShServiceContainer.Instance.EventsService; }
        }

        protected virtual void ReceiveEvent<TEvent>(Action<TEvent> action)
        {
            Hub.GetEvent<TEvent>().Subscribe(action);
        }

        protected virtual void RaiseEvent<TEvent>(TEvent message)
        {
            Hub.Publish(message);
        }

        protected void WithSafeUIUpdate(Action a)
        {
            InvokeIfRequired(this, () => { a(); });
        }

        public static void InvokeIfRequired(Control control, MethodInvoker action)
        {
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

        protected virtual TControl CreateControlInstance<TControl>()
             where TControl : class, new()
        {
            var uiService = ShServiceContainer.Instance.AppMetadataService.AppUIServices.FirstOrDefault();

            if (uiService != null)
                return uiService.CreateControlInstance<TControl>();

            return new TControl();
        }

        protected List<ShAppStartPageItemDefinition> GetActionLinks(string group)
        {
            var result = new List<ShAppStartPageItemDefinition>();

            var menuItems = new List<ToolStripItem>();
            //var menuDefs = new List<ExplorerMenuItemDefinition>();

            var extensibilityMenuItemProviderType = typeof(ShAppStartPagePluginBase);

            var menuProviders = AppMetadataService.AppPlugins
                                               .Where(p => p.GetType().IsSubclassOf(extensibilityMenuItemProviderType))
                                               .Cast<ShAppStartPagePluginBase>();

            foreach (var menuProvider in menuProviders)
            {
                var menuItemDefinitions = menuProvider.GetMenuItems()
                    .Where(l => l.Location != null && l.Location.ToUpper() == group.ToUpper());

                result.AddRange(menuItemDefinitions);
            }

            return result;
        }
    }
}