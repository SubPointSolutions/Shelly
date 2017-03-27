using System;
using System.Collections.Concurrent;
using System.Reactive.Subjects;
using SubPointSolutions.Shelly.Core.Interfaces;

namespace SubPointSolutions.Shelly.Core.Services
{
    public class ShAppEventDataService : ShAppDataServiceBase, IAppEventService
    {
        #region properties

        private readonly ConcurrentDictionary<Type, object> _subjects = new ConcurrentDictionary<Type, object>();

        #endregion

        #region methods

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject = (ISubject<TEvent>)_subjects.GetOrAdd(typeof(TEvent), t => new Subject<TEvent>());

            return subject;
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            object subject;

            if (_subjects.TryGetValue(typeof(TEvent), out subject))
            {
                ((ISubject<TEvent>)subject).OnNext(sampleEvent);
            }
        }

        #endregion
    }
}