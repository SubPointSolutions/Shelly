using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SubPointSolutions.Shelly.Core.Extensibility;
using SubPointSolutions.Shelly.Core.Interfaces;
using SubPointSolutions.Shelly.Core.Utils;

namespace SubPointSolutions.Shelly.Core.Services
{
    public abstract class ShAppMetadataService
    {
        #region constructors

        public ShAppMetadataService()
        {
            AppName = "Quark App";

            AppDataServiceAssemblies = new List<Assembly>();
            AppPluginsAssemblies = new List<Assembly>();
            AppUIServiceAssemblies = new List<Assembly>();

            AppPlugins = new List<IPlugin>();

            AppUIServices = new List<ShAppUIServiceBase>();
            AppDataServices = new List<ShAppDataServiceBase>();

            // base services from Quark.Core assembly + from the current one
            var defaultAssemblies = new[]
            {
                typeof(ShAppDataServiceBase).Assembly,
                GetType().Assembly
            };

            AppDataServiceAssemblies.AddRange(defaultAssemblies);
            AppUIServiceAssemblies.AddRange(defaultAssemblies);
            AppPluginsAssemblies.AddRange(defaultAssemblies);
        }

        #endregion

        #region properties

        public string AppName { get; set; }
        public string AppVersion { get; set; }

        //public QuarkAppUpdateService AppUpdateService
        //{
        //    get { return QuarkAppService.Current.AppUpdateService; }
        //}

        public List<ShAppUIServiceBase> AppUIServices { get; private set; }
        public List<ShAppDataServiceBase> AppDataServices { get; private set; }

        public List<IPlugin> AppPlugins { get; private set; }

        public List<Assembly> AppPluginsAssemblies { get; private set; }

        public List<Assembly> AppDataServiceAssemblies { get; private set; }
        public List<Assembly> AppUIServiceAssemblies { get; private set; }

        #endregion

        #region methods

        public TService GetAppUIService<TService>()
           where TService : ShAppUIServiceBase
        {
            return (TService)AppUIServices.First(s => s is TService);
        }

        public TService GetAppDataService<TService>()
           where TService : ShAppDataServiceBase
        {
            return (TService)AppDataServices.First(s => s is TService);
        }

        protected virtual void InitAppPlugins()
        {
            var types = ShReflectionUtils.GetTypesFromAssemblies<IPlugin>(AppPluginsAssemblies);

            foreach (var type in types.OrderByDescending(a => a.Name))
            {
                try
                {
                    if (!AppDataServices.Any(t => t.GetType() == type))
                    {
                        var instance = Activator.CreateInstance(type) as IPlugin;

                        instance.Init();

                        AppPlugins.Add(instance);
                    }
                }

                catch (System.Exception e)
                {
                    // TODO
                    throw;
                }
            }
        }

        protected virtual void CreateAppUIServices()
        {
            var types = ShReflectionUtils.GetTypesFromAssemblies<ShAppUIServiceBase>(AppUIServiceAssemblies);

            foreach (var type in types.OrderByDescending(a => a.Name))
            {
                try
                {
                    if (!AppDataServices.Any(t => t.GetType() == type))
                    {
                        var instance = Activator.CreateInstance(type) as ShAppUIServiceBase;
                        AppUIServices.Add(instance);
                    }
                }
                catch (Exception e)
                {
                    // TODO
                    throw;
                }
            }
        }

        protected virtual void InitAppUIServices(object uiHostControl)
        {
            foreach (var service in AppUIServices)
                service.Init(uiHostControl);
        }

        protected virtual void InitAppDataServices()
        {
            var types = ShReflectionUtils.GetTypesFromAssemblies<ShAppDataServiceBase>(AppDataServiceAssemblies)
                                       .ToList();

            // special init for event hub
            var eventDataService = types.First(t => typeof(IAppEventService).IsAssignableFrom(t));

            AppDataServices.Add(Activator.CreateInstance(eventDataService) as ShAppDataServiceBase);
            types.Remove(eventDataService);

            // the rest
            foreach (var type in types.OrderByDescending(a => a.Name))
            {
                try
                {
                    if (!AppDataServices.Any(t => t.GetType() == type))
                    {
                        var instance = Activator.CreateInstance(type) as ShAppDataServiceBase;

                        instance.Init();

                        AppDataServices.Add(instance);
                    }
                }
                catch (Exception e)
                {
                    // TODO
                    throw;
                }
            }
        }

        #endregion

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
            where TEvent : class, new()
        {
            RaiseEvent<TEvent>(new TEvent());
        }

        protected void RaiseEvent<TEvent>(TEvent message)
            where TEvent : class, new()
        {
            Hub.Publish(message);
        }

        public abstract void Run();

        //public abstract void Run();
    }
}
