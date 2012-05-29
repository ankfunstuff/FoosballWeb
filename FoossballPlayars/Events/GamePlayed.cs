using System;
using System.Runtime.Serialization;
using EventSourcingTest;

namespace FoossballPlayars.Events
{
    [DataContract]
    public class GamePlayed : Event
	{
        [DataMember]
        public Guid RedOffensive { get; set; }
        [DataMember]
        public Guid RedDefensive { get; set; }
        [DataMember]
        public Guid BlueOffensive { get; set; }
        [DataMember]
        public Guid BlueDefensive { get; set; }
        [DataMember]
        public int ScoreRed { get; set; }
        [DataMember]
        public int ScoreBlue { get; set; }

        public bool RedWinner { get { return ScoreRed > ScoreBlue; } }
	}
}
