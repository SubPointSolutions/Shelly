using System;
using SubPointSolutions.Shelly.Core.Interfaces;

namespace SubPointSolutions.Shelly.Core.Extensibility
{
    public class ShPluginBase : IPlugin
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual void Init() { }
        public virtual void ShutDown() { }

        protected IAppEventService Hub
        {
            get
            {
                return ShServiceContainer.Instance.EventsService;
            }
        }

        protected void ReceiveEvent<TEvent>(Action<TEvent> action)
        {
            Hub.GetEvent<TEvent>().Subscribe(action);
        }

        protected void RaiseEvent<TEvent>()
            where TEvent : class,new()
        {
            RaiseEvent<TEvent>(new TEvent());
        }

        protected void RaiseEvent<TEvent>(TEvent message)
            where TEvent : class,new()
        {
            Hub.Publish(message);
        }

        #region utils


        //protected void AddViewControl(string title, Guid id, Type type)
        //{
        //    AddViewControl(new AppViewWindowControlDefinition
        //    {
        //        Title = title,
        //        Id = id,
        //        AssemblyQualifiedName = type.AssemblyQualifiedName
        //    });
        //}

        //protected void AddViewControl(AppViewWindowControlDefinition definition)
        //{
        //    RaiseEvent(new AddAppViewWindowEvent
        //    {
        //        ViewWindowControl = definition
        //    });
        //}

        //protected void AddTopMenu(AppMenuItemDefinitionBase menu)
        //{
        //    RaiseEvent(new AddAppTopMenuItemEvent
        //    {
        //        MenuItem = menu
        //    });
        //}

        protected ShServiceContainer Services
        {
            get { return ShServiceContainer.Instance; }
        }



        #endregion
    }
}
