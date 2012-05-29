using System;
using System.Collections.Generic;

namespace EventSourcingTest.Interfaces
{
	public interface IEventStorage : IDisposable
	{
        IEnumerable<dynamic> GetEventsForRoot<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;
        void Append<TAggregateRoot>(Guid id, IEnumerable<Event> events) where TAggregateRoot : AggregateRoot;
	}
}