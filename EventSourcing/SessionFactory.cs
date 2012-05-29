using EventSourcingTest.Interfaces;

namespace EventSourcingTest
{
	public class SessionFactory : ISessionFactory
	{
		private readonly IEventStorage _eventStorage;

		public SessionFactory(IEventStorage eventStorage)
		{
			_eventStorage = eventStorage;
		}

		public ISession OpenSession()
		{
			return new Session(_eventStorage);
		}

		public void Dispose()
		{
			_eventStorage.Dispose();
		}
	}
}