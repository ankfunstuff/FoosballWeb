using System;
using System.Collections.Generic;
using EventSourcingTest.Interfaces;

namespace EventSourcingTest
{
    public class Session : ISession
	{
		private readonly IEventStorage _eventStorage;
		private readonly HashSet<ISessionItem> _enlistedItems = new HashSet<ISessionItem>();

		[ThreadStatic]
		private static Session _current;

		internal Session(IEventStorage eventStorage)
		{
			_eventStorage = eventStorage;
			if (_current != null)
				throw new InvalidOperationException("Cannot nest unit of work");
			_current = this;
		}

		private static Session Current
		{
			get { return _current; }
		}
         
		public void SubmitChanges()
		{
            foreach (var enlisted in _enlistedItems)
                enlisted.SubmitChanges();
			_enlistedItems.Clear();
		}

		public void Dispose()
		{
			_current = null;
		}

        internal static IEventStorage Enlist<TAggregateRoot>
						(Repository<TAggregateRoot> repository)
			where TAggregateRoot : AggregateRoot
		{
            if (Current == null)
                throw new InvalidOperationException("No session");
			var unitOfWork = Current;
            
			unitOfWork._enlistedItems.Add(repository);
			return unitOfWork._eventStorage;
		}
	}
}