using System;
using System.Collections.Generic;
using System.Linq;
using Ankiro.Framework.Extensions;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest.Interfaces;

namespace EventSourcingTest
{
	public interface IRepository<T>
	{
		void Add(T entity);
        T this[Guid id] { get; }
	}
	public abstract class Repository<TAggregateRoot> : ISessionItem
		where TAggregateRoot : AggregateRoot
	{
	    private readonly IBus _eventBus;
	    private readonly IDictionary<Guid, TAggregateRoot> _users = new Dictionary<Guid, TAggregateRoot>();
        private readonly IEventStorage _aggregateRootStorage;

		protected Repository(IBus eventBus)
		{
		    _eventBus = eventBus;
		    _aggregateRootStorage = Session.Enlist(this);
		}

	    public void Add(TAggregateRoot user)
		{
			_users.Add(user.Id, user);
		}

        public TAggregateRoot this[Guid id]
		{
			get { return Find(id) ?? Load(id); }
		}

	    private TAggregateRoot Find(Guid id)
		{
			TAggregateRoot user;
			return _users.TryGetValue(id, out user) ? user : null;
		}

		private TAggregateRoot Load(Guid id)
		{
		    var events = _aggregateRootStorage.GetEventsForRoot<TAggregateRoot>(id);
            if (!events.HasAny())
                return null;
			var user = CreateInstance(id, events);
			_users.Add(id, user);
			return user;
		}

        protected abstract TAggregateRoot CreateInstance(Guid id, IEnumerable<object> events);

		public void SubmitChanges()
		{
			foreach (IAggregateRoot user in _users.Values)
			{
				var uncomitedEvents = user.UncommittedEvents;
				if (uncomitedEvents.HasEvents)
				{
					_aggregateRootStorage.Append<TAggregateRoot>(user.Id, uncomitedEvents);
					PublishEvents(uncomitedEvents);
					uncomitedEvents.Commit();
				}
			}
			_users.Clear();
		}

		private void PublishEvents(IUncommittedEvents uncommittedEvents)
		{
			foreach (dynamic @event in uncommittedEvents)
                _eventBus.Raise(@event);
		}

	}
}