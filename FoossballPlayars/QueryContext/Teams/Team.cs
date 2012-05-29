using System;

namespace FoossballPlayars.QueryContext.Teams
{
	public class Team : IEquatable<Team>
	{
		public const int InitialScore = 1500;
		public string Offensive { get; private set; }
		public string Defensive { get; private set; }

		public GamePercentage Percentage { get; private set; }
		public double Score { get; private set; }

		public Team(string offesive, string defensive)
		{
			Offensive = offesive;
			Defensive = defensive;
			Percentage = new GamePercentage();
			Score = InitialScore;
		}

		public void UpdateScore(double score)
		{
			Score = Score+score;
			if (score > 0)
			{
				Percentage.AddVictory();

			}
			else
			{
				Percentage.AddDefeat();
			}
		}

		public bool Equals(Team other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Offensive, Offensive) && Equals(other.Defensive, Defensive);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Team)) return false;
			return Equals((Team) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Offensive != null ? Offensive.GetHashCode() : 0)*397) ^ (Defensive != null ? Defensive.GetHashCode() : 0);
			}
		}

		public static bool operator ==(Team left, Team right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Team left, Team right)
		{
			return !Equals(left, right);
		}
	}
}