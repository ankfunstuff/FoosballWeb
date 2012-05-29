using System;
using System.Runtime.Serialization;

namespace EventSourcingTest
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public Guid RootId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
    }
}