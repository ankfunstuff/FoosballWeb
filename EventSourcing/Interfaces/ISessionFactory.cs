using System;

namespace EventSourcingTest.Interfaces
{
	public interface ISessionFactory : IDisposable
	{
		ISession OpenSession();
	}
}