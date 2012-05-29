using System;
using System.Collections.Generic;

namespace EventSourcingTest.Interfaces
{
	public interface IAggregateRootStorage
	{
        void Append(Guid id, IEnumerable<dynamic> events);
        IEnumerable<dynamic> GetEventsForRoot(Guid id);
	    void Append(Guid id, dynamic @event);
	}
}