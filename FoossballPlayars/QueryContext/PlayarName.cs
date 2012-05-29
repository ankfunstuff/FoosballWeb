using System;

namespace FoossballPlayars.QueryContext
{
    public class PlayarName :IComparable<PlayarName>
    {
        public readonly string Name;

        public PlayarName(string name)
        {
            Name = name;
        }

        public int CompareTo(PlayarName other)
        {
            return String.CompareOrdinal(Name, other.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(PlayarName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PlayarName)) return false;
            return Equals((PlayarName) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}