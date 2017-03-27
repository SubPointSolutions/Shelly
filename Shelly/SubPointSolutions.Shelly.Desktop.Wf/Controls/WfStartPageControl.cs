using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SubPointSolutions.Shelly.Core;
using SubPointSolutions.Shelly.Desktop.Controls;
using SubPointSolutions.Shelly.Desktop.Definitions.UI;
using SubPointSolutions.Shelly.Desktop.Events.StartPage;
using SubPointSolutions.Shelly.Desktop.Plugins;

namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    public partial class WfStartPageControl : ShUserControlBase
    {
        #region constructors

        public WfStartPageControl()
        {
            InitializeComponent();

            InitProjectActions();
            InitProjectName();
            //InitEvents();
        }

        private void InitProjectName()
        {
            var metadataService = ShServiceContainer.Instance.AppMetadataService;

            if (metadataService != null)
            {
                lAppName.Text = metadataService.AppName;
            }
        }

        #endregion

        #region properties

        private Font _linkFont;

        public object SolutionItemTag { get; set; }

        public object SolutionItem
        {
            get { return null; }
            set
            {

            }
        }

        public string SolutionItemTitle
        {
            get { return "Start page"; }
            set
            {

            }
        }


        #endregion

        #region methods

        private void InitEvents()
        {
            ReceiveEvent<ShOnAppStartPageLoadCompletedEvent>(e =>
            {
                if (e.EventType == StartPageEventType.AddLink)
                {
                    AddLink(e.Item);
                }
            });
        }

        private void InitProjectActions()
        {
            pProjectActions.FlowDirection = FlowDirection.TopDown;


            var links = GetLinks();

            foreach (var link in links)
                AddLink(link);
        }

        private void AddLink(ShAppStartPageItemDefinition def)
        {
            var link = new LinkLabel();

            if (_linkFont == null)
                _linkFont = new Font(link.Font.FontFamily, 10);

            link.LinkColor = Color.FromArgb(17, 151, 222);
            link.Font = _linkFont;
            link.Text = def.Title;
            link.Width = pProjectActions.Width;

            if (def.Click != null)
                link.Click += def.Click;

            pProjectActions.Controls.Add(link);
        }

        private List<ShAppStartPageItemDefinition> GetLinks()
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
                var menuItemDefinitions = menuProvider.GetMenuItems();

                result.AddRange(menuItemDefinitions);
            }

            return result;
        }

        #endregion
    }
}
