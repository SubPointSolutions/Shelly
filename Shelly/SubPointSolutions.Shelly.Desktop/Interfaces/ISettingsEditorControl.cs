
using System;

namespace SubPointSolutions.Shelly.Desktop.Interfaces
{
    public interface ISettingsEditorControl
    {
        void SetOptionsObject(object settings);


        event EventHandler OnOk;

        event EventHandler OnCancel;
    }
}
