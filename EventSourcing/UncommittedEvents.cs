using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventSourcingTest.Interfaces;

namespace EventSourcingTest
{
	internal class UncommittedEvents : IUncommittedEvents
	{
        private readonly IList<Event> _events = new List<Event>();

        public void Append(Event @event)
		{
			_events.Add(@event);
		}

        IEnumerator<Event> IEnumerable<Event>.GetEnumerator()
		{
			return _events.GetEnumerator();
		}

		public bool HasEvents
		{
			get { return _events.Any(); }
		}

		void IUncommittedEvents.Commit()
		{
			_events.Clear();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _events.GetEnumerator();
		}
	}
}