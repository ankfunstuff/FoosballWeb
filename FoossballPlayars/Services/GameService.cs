using System;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest.Interfaces;
using FoossballPlayars.CommandContext;
using FoossballPlayars.Commands;

namespace FoossballPlayars.Services
{
    public class GameService : IGameService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IBus _eventBus;

    	public GameService(ISessionFactory sessionFactory,  IBus eventBus)
        {
            _sessionFactory = sessionFactory;
            _eventBus = eventBus;
        }

        public void Handle(RegisterPlayarCommand command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var repository = new PlayarRepository(_eventBus);
                var competition = CompetitionRepository.GetOrCreate(_eventBus);
                var id = Guid.NewGuid();
                repository.Add(new Playar(id, command.Name));
                competition.AddPlayer(id);
                session.SubmitChanges();
            }
        }

        public void Handle(PlayGameCommand command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var competition = CompetitionRepository.GetOrCreate(_eventBus);
                competition.PlayGame(command.RedOffensive, command.RedDefensive, command.BlueOffensive, command.BlueDefensive, command.ScoreRed, command.ScoreBlue);
                session.SubmitChanges();
            }
        }
    }
}