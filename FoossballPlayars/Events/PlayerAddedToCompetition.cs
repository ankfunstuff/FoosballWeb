using System;
using System.Runtime.Serialization;
using EventSourcingTest;

namespace FoossballPlayars.Events
{
    [DataContract]
    public class PlayerAddedToCompetition : Event
    {
        [DataMember]
        public Guid PlayarId { get; set; }
    }
}