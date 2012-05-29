using System.Linq;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest;
using FoossballPlayars.Commands;
using FoossballPlayars.QueryContext;
using FoossballPlayars.Services;
using Moq;
using NUnit.Framework;

namespace Test.FoossballPlayars
{
    [TestFixture]
    public class CompetitionIT
    {
        private ScoreQuery _scoreQuery;
        private IBus _commandbus;

        [TestFixtureSetUp]
        public void Setup()
        {
            var scoreCalculator = new SimpleScoreCalculator();
            _scoreQuery = new ScoreQuery(new Mock<ISignaler>().Object, scoreCalculator);
            var eventBus = new DomainBus();
            eventBus.RegisterHandler(() => new GameHandler(_scoreQuery));
            var eventStorage = new InMemoryEventStorage();
            var sessionFactory = new SessionFactory(eventStorage);
            var gameService = new GameService(sessionFactory, eventBus);
            _commandbus = new DomainBus();
            _commandbus.RegisterHandler(() => gameService);
        }

        [Test]
        public void CanPlay()
        {
            _commandbus.Raise(new RegisterPlayarCommand("Test1"));
            _commandbus.Raise(new RegisterPlayarCommand("Test2"));
            _commandbus.Raise(new RegisterPlayarCommand("Test3"));
            _commandbus.Raise(new RegisterPlayarCommand("Test4"));

            var playars = _scoreQuery.GetTopPlayers().ToArray();
            Assert.AreEqual(4, playars.Count());
            _commandbus.Raise(new PlayGameCommand(playars[0].Id, playars[1].Id, playars[2].Id, playars[3].Id, 10, 8));
            Assert.AreEqual(1, _scoreQuery.GetStatistics(playars[0].Id).Total.GamesPlayed);
        }

        [Test]
        public void NamesAreUnique()
        {
            _commandbus.Raise(new RegisterPlayarCommand("Test"));
            _commandbus.Raise(new RegisterPlayarCommand("Test"));
        }
    }
}