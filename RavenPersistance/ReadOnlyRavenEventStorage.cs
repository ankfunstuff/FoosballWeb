using System;
using System.Collections.Generic;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest;

namespace RavenPersistance
{
	public class ReadOnlyRavenEventStorage : RaventEventStorage
	{
		public ReadOnlyRavenEventStorage(IDocumentStoreFactory storeFactory, IBus eventBus) : base(storeFactory, eventBus)
		{
		}

		public override void Append<TAggregateRoot>(Guid id, IEnumerable<Event> events)
		{
			
		}
	}
}