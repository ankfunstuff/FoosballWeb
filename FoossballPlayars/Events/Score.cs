using System;
using System.Runtime.Serialization;

namespace FoossballPlayars.Events
{
    [DataContract]
    public class Score
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public double ScoreCount { get; set; }
    }
}