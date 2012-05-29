using Ankiro.Framework.Tools.Bus;
using EventSourcingTest;
using EventSourcingTest.Interfaces;
using FoossballPlayars.Events;

namespace RavenMigration
{
	public class MigrationHandler :
		Handles<PlayarRegistered>,
		Handles<GamePlayed>,
		Handles<PlayerAddedToCompetition>
	{
		private readonly IEventStorage _eventStorage;

		public MigrationHandler(IEventStorage eventStorage)
		{
			_eventStorage = eventStorage;
		}

		public void Handle(PlayarRegistered command)
		{
			_eventStorage.Append<AggregateRoot>(command.RootId, new Event[] { command });
		}

		public void Handle(GamePlayed command)
		{
			_eventStorage.Append<AggregateRoot>(command.RootId, new Event[] { command });
		}

		public void Handle(PlayerAddedToCompetition command)
		{
			_eventStorage.Append<AggregateRoot>(command.RootId, new Event[] { command });
		}
	}
}