using Ankiro.Framework.Tools.Bus;
using FoossballPlayars.Events;

namespace FoossballPlayars.QueryContext
{
    public class GameHandler :
        Handles<PlayarRegistered>,
        Handles<GamePlayed>
    {
        private readonly ScoreQuery _query;

        public GameHandler(ScoreQuery query)
        {
            _query = query;
        }

        public void Handle(PlayarRegistered @event)
        {
            _query.AddPlayar(@event.RootId, new PlayarName(@event.PlayarName), @event.Date);
        }

        public void Handle(GamePlayed @event)
        {
            _query.Handle(@event);
        }
    }
}