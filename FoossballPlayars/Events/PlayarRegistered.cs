using System;
using System.Runtime.Serialization;
using EventSourcingTest;

namespace FoossballPlayars.Events
{
    [DataContract]
    public class PlayarRegistered : Event
	{
        [DataMember]
        public string PlayarName { get; set; }
	}
}