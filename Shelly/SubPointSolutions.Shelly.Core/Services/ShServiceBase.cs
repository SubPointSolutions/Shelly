using System;
using SubPointSolutions.Shelly.Core.Interfaces;

namespace SubPointSolutions.Shelly.Core.Services
{
    public class ShServiceBase
    {
        #region properties

        protected IAppEventService Hub
        {
            get
            {
                return ShServiceContainer.Instance.EventsService;
            }
        }

        #endregion

        #region methods

        public virtual void Init()
        {

        }

        public virtual void ShutDown()
        {

        }

        #endregion


        #region utils

        protected void ReceiveEvent<TEvent>(Action<TEvent> action)
        {
            Hub.GetEvent<TEvent>().Subscribe(action);
        }

        protected void RaiseEvent<TEvent>(TEvent message)
        {
            Hub.Publish(message);
        }

        #endregion
    }
}
