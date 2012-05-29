using System;
using System.Collections.Generic;
using EventSourcingTest.Interfaces;

namespace EventSourcingTest
{
	public class InMemoryEventStorage : IEventStorage
	{
		private readonly IDictionary<Type, dynamic> _stores = new Dictionary<Type, dynamic>();

        public IEnumerable<dynamic> GetEventsForRoot<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            var store = GetAggregateRootStore<TAggregateRoot>();
            return store.GetEventsForRoot(id);
        }

        public void Append<TAggregateRoot>(Guid id, IEnumerable<Event> events) where TAggregateRoot : AggregateRoot
        {
            var store = GetAggregateRootStore<TAggregateRoot>();
            foreach (var @event in events)
            {
                @event.RootId = id;
                @event.Date = DateTime.Now;
            }
            store.Append(id, events);
        }

	    private IAggregateRootStorage GetAggregateRootStore<TAggregateRoot>() where TAggregateRoot : AggregateRoot
		{
			dynamic store;
			if (!_stores.TryGetValue(typeof(TAggregateRoot), out store))
			{
                store = new InMemoryAggregateRootStorage();
				_stores.Add(typeof(TAggregateRoot), store);
			}
			return store;
		}
        
	    public void Dispose()
	    {
	        _stores.Clear();
	    }
	}
}
