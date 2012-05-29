using System;

namespace EventSourcingTest.Interfaces
{
	public interface IAggregateRoot
	{
        Guid Id { get; }
		IUncommittedEvents UncommittedEvents { get; }
	}
}