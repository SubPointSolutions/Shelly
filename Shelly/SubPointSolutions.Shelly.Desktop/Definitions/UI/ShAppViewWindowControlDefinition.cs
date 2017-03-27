using System;
using System.ComponentModel;

namespace SubPointSolutions.Shelly.Desktop.Definitions.UI
{

    public class ShAppViewWindowControlDefinition
    {
        public string Title { get; set; }

        public Guid Id { get; set; }
        public string AssemblyQualifiedName { get; set; }

        public EventHandler<CancelEventArgs> Click { get; set; }
    }
}
