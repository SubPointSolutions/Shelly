using System;

namespace SubPointSolutions.Shelly.Core.Interfaces
{
    public interface IAppEventService
    {
        void Publish<T>(T message);
        IObservable<T> GetEvent<T>();
    }
}