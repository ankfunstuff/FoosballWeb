using System.Collections.Generic;

namespace EventSourcingTest.Interfaces
{
    public interface IUncommittedEvents : IEnumerable<Event>
	{
		bool HasEvents { get; }
		void Commit();
	}
}