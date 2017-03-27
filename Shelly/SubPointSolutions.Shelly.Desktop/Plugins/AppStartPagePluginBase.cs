using System.Collections.Generic;
using SubPointSolutions.Shelly.Core.Extensibility;
using SubPointSolutions.Shelly.Desktop.Definitions.UI;

namespace SubPointSolutions.Shelly.Desktop.Plugins
{
    public abstract class ShAppStartPagePluginBase : ShPluginBase
    {
        public abstract List<ShAppStartPageItemDefinition> GetMenuItems();
    }

}
