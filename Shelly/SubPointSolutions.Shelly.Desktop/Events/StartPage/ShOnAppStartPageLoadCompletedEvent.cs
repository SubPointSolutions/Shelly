using SubPointSolutions.Shelly.Desktop.Definitions.UI;
using SubPointSolutions.Shelly.Desktop.Events.Base;

namespace SubPointSolutions.Shelly.Desktop.Events.StartPage
{
    public enum StartPageEventType
    {
        Unknown,

        AddLink
    }

    public class ShOnAppStartPageLoadCompletedEvent : ItemEventBase<ShAppStartPageItemDefinition, StartPageEventType>
    {

    }
}
