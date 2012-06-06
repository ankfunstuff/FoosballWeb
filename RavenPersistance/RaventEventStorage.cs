using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ankiro.Framework.Extensions;
using Ankiro.Framework.Tools;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest;
using EventSourcingTest.Interfaces;
using Raven.Client;
using Raven.Client.Linq;

namespace RavenPersistance
{
    public class RaventEventStorage : IEventStorage
    {
        private readonly IDocumentStore _documentStore;

        public RaventEventStorage(IDocumentStoreFactory storeFactory, IBus eventBus)
        {
            _documentStore = storeFactory.BuildStore;
            using (var session = _documentStore.OpenSession())
            {
                var events = GetAllEvents(session).WithPerformanceLogging(100, "events", x=>Debug.WriteLine(x)).ToList();
                foreach (var @event in events
					//.OrderByDescending(x => (x as Event).Date)
					)
                {
                    eventBus.Raise(@event);
                }
            }
        }

        private static IEnumerable<dynamic> GetAllEvents(IDocumentSession session)
        {
            Func<int, int, IList<dynamic>> batchFunction = 
                (offset, batchSize) => 
                session
                    .Query<dynamic>()
                    //.OrderBy(x => (x as Event).Date)
					//.OrderBy(x => x["@metadata"]["Last-Modified"])
                    .Take(batchSize)
                    .Skip(offset)
                    .ToList();
            return batchFunction.BatchToStream(128);
        }
        
        public IEnumerable<dynamic> GetEventsForRoot<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            using (var session = _documentStore.OpenSession())
            {
                var events = session.Query<Event>().Where(x => x.RootId == id).OrderBy(x => x.Date);
                foreach (var @event in events)
                {
                    yield return @event;
                }
            }
        }

        public virtual void Append<TAggregateRoot>(Guid id, IEnumerable<Event> events) where TAggregateRoot : AggregateRoot
        {
            using (var session = _documentStore.OpenSession())
            {
                foreach (var @event in events)
                {
                    @event.RootId = id;
					if (@event.Date == DateTime.MinValue)
						@event.Date = DateTime.Now;
                    session.Store(@event);
                }
                session.SaveChanges();
            }
        }

        public void Dispose()
        {
        }
    }
}