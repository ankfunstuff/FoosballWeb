using System;
using System.Collections.Generic;
using EventSourcingTest.Interfaces;

namespace EventSourcingTest
{
	public abstract class AggregateRoot : IAggregateRoot
	{
	    private readonly UncommittedEvents _uncommittedEvents = new UncommittedEvents();

		protected void Replay(IEnumerable<object> events)
		{
			dynamic me = this;
			foreach (var @event in events)
				me.Apply(@event);
		}

        protected void Append(Event @event)
		{
			_uncommittedEvents.Append(@event);
		}

        public abstract Guid Id { get; }

        IUncommittedEvents IAggregateRoot.UncommittedEvents
		{
			get { return _uncommittedEvents; }
		}
	}
}