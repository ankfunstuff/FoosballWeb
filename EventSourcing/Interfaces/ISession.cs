using System;

namespace EventSourcingTest.Interfaces
{
	public interface ISession : IDisposable
	{
		void SubmitChanges();
	}
}