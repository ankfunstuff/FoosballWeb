using System;
using System.Collections.Generic;
using EventSourcingTest;
using FoossballPlayars.Events;

namespace FoossballPlayars.CommandContext
{
    public class Playar : AggregateRoot
    {
        private readonly Guid _id;
        private string _name;

        public Playar(Guid id, string name)
        {
            _id = id;
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Playar name not specified.");
            }
            var @event = new PlayarRegistered{ RootId = id,PlayarName= name};
            Apply(@event);
            Append(@event);
        }

        public Playar(Guid id, IEnumerable<object> events)
        {
            _id = id;
            foreach (dynamic @event in events)
                Apply(@event);
        }

        public override Guid Id
        {
            get { return _id; }
        }
        
        private void Apply(PlayarRegistered @event)
        {
            _name = @event.PlayarName;
        }
    }
}