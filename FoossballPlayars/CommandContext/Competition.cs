using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingTest;
using FoossballPlayars.Events;

namespace FoossballPlayars.CommandContext
{
    public class Competition : AggregateRoot
    {
        private readonly Guid _id;
        private readonly IList<Guid> _players;

        public Competition(Guid id)
        {
            _id = id;
            _players = new List<Guid>();
        }

        public Competition(Guid id, IEnumerable<object> events)
            : this(id)
        {
            foreach (dynamic @event in events)
                Apply(@event);
        }

        public override Guid Id
        {
            get { return _id; }
        }

        public void PlayGame(Guid redOffensive,
                            Guid redDefensive,
                            Guid blueOffensive,
                            Guid blueDefensive,
                            int scoreRed,
                            int scoreBlue)
        {
            ValidatePlayers(redOffensive, redDefensive, blueOffensive, blueDefensive);
            var scoreValidator = new ScoreValidator(scoreRed, scoreBlue);
            if (!scoreValidator.IsValid)
            {
                throw new InvalidOperationException(scoreValidator.Message);
            }
            
            var @event = new GamePlayed
                             {
                                 RedOffensive = redOffensive,
                                 RedDefensive=redDefensive, 
                                 BlueOffensive=blueOffensive, 
                                 BlueDefensive = blueDefensive, 
                                 ScoreRed = scoreRed, 
                                 ScoreBlue = scoreBlue,
                             };
            Append(@event);
            Apply(@event);
        }

        public void Apply(GamePlayed @event)
        {
            
        }

        private void ValidatePlayers(Guid redOffensive, Guid redDefensive, Guid blueOffensive,
                                            Guid blueDefensive)
        {
            if (new[] { redOffensive, redDefensive, blueOffensive, blueDefensive }.Distinct().Count() != 4)
            {
                throw new InvalidOperationException("A playar can only play one position.");
            }
            CheckPlayar(redOffensive);
            CheckPlayar(redDefensive);
            CheckPlayar(blueOffensive);
            CheckPlayar(blueDefensive);
        }

        private void CheckPlayar(Guid id)
        {
            if (!_players.Contains(id))
            {
                throw new InvalidOperationException("Playar id not found " + id);
            }
        }

        public void AddPlayer(Guid id)
        {
            var @event = new PlayerAddedToCompetition { PlayarId = id};
            Apply(@event);
            Append(@event);
        }

        private void Apply(PlayerAddedToCompetition @event)
        {
            _players.Add(@event.PlayarId);
        }
    }
}