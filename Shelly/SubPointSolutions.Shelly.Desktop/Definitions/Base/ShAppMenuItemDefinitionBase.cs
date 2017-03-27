using System;

namespace SubPointSolutions.Shelly.Desktop.Definitions.Base
{
    public class ShAppMenuItemDefinitionBase
    {
        public Guid Id { get; set; }
        public Guid? ParentItemId { get; set; }

        public int Order { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }
        public string Tag { get; set; }

        public bool StartsGroup { get; set; }

        public EventHandler Click { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Title))
                return Title;

            return base.ToString();
        }
    }
}
