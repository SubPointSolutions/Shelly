using System;

namespace SubPointSolutions.Shelly.Core.Utils
{
    public class ShBind
    {
        #region construtors

        public ShBind(Action a)
        {
            _action = a;
        }

        #endregion

        #region properties

        private Action _action;

        #endregion

        #region methods
        public static ShBind Cmd(Action a)
        {
            return new ShBind(a);
        }

        public void Bind(object s, EventArgs e)
        {
            if (_action != null)
                _action();
        }

        #endregion
    }
}
