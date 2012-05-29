using System;
using System.Collections.Generic;
using EventSourcingTest.Interfaces;

namespace EventSourcingTest
{
    public class InMemoryAggregateRootStorage : IAggregateRootStorage
    {
        private readonly IDictionary<Guid, List<dynamic>> _store = new Dictionary<Guid, List<dynamic>>();

        public void Append(Guid id, IEnumerable<dynamic> events)
        {
            var aggregateRootEvents = GetAggregateRootEvents(id);
            aggregateRootEvents.AddRange(events);
        }

        public void Append(Guid id, dynamic @event)
        {
            var aggregateRootEvents = GetAggregateRootEvents(id);
            aggregateRootEvents.Add(@event);
        }

        private List<dynamic> GetAggregateRootEvents(Guid id)
        {
            List<dynamic> aggregateRootEvents;
            if (!_store.TryGetValue(id, out aggregateRootEvents))
            {
                aggregateRootEvents = new List<dynamic>();
                _store.Add(id, aggregateRootEvents);
            }
            return aggregateRootEvents;
        }

        public IEnumerable<dynamic> GetEventsForRoot(Guid id)
        {
            List<dynamic> events;
            _store.TryGetValue(id, out events);
            return events;
        }
    }
}