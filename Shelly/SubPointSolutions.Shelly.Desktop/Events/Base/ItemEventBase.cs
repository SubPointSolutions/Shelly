using System;

namespace SubPointSolutions.Shelly.Desktop.Events.Base
{
    public class ItemEventBase<TItem, TEventType>
    {
        public ItemEventBase()
        {

        }

        public TItem Item { get; set; }

        public TEventType EventType { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", GetType().Name, EventType);
        }
    }
}
