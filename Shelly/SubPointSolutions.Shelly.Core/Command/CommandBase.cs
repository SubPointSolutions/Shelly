using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubPointSolutions.Shelly.Core.Command
{
    public class CommandBase : INotifyPropertyChanged
    {
        #region consturctors
        public CommandBase()
        {

        }
        #endregion

        #region propertis

        public event PropertyChangedEventHandler PropertyChanged;
        public string Key { get; protected set; }

        private string _displayName;

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DisplayName"));
            }
        }

        private bool _enabled;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                this._enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Enabled"));
            }
        }
        #endregion

        #region methods
        public virtual void Execute()
        {

        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;

            if (handler != null)
                handler(this, e);
        }
        #endregion
    }
}
