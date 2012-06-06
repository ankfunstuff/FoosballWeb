using System;
using System.Collections.Generic;

namespace FoossballPlayars.QueryContext
{
    public class PlayarStatisistics
    {
    	public const double InitialScore = 0;
    	public const int InplayGames = 10;
    	public const int InplayScore = 200;

    	public Guid Id { get; private set; }
        public PlayarName Name { get; private set; }
        public IList<Tuple<double, Activity>> ScoreHistory { get; private set; }
        public double Score { get; private set; }
    	public GamePercentage Offensive { get; private set; }
    	public GamePercentage Defensive { get; private set; }
    	public GamePercentage Total { get; private set; }
		private readonly Streak _winning;
    	private readonly Streak _loosing;

        public IEnumerable<string> Badges
        {
            get
            {
                if (_winning.IsStreak)
                {
                    yield return "On a winning streak";
                }
				if (_loosing.IsStreak)
                {
                    yield return "On a loosing streak";
                }
                if (Total.WinPercentage > 75)
                {
                    yield return "Playar";
                }
                if (Score < 0)
                {
                    yield return "Below expectation";
                }
				if (Total.GamesPlayed < 5)
                {
                    yield return "Should play some more";
                }
            }
        }

        public PlayarStatisistics(Guid id, PlayarName name)
        {
            Id = id;
            Name = name;
            Score = InitialScore;
            ScoreHistory = new List<Tuple<double, Activity>>();
			Offensive = new GamePercentage();
			Defensive = new GamePercentage();
			Total = new GamePercentage();
			_winning = new Streak(5);
			_loosing = new Streak(5);
        }

		public void UpdateScore(double score, Activity activity, bool winner, bool isOffence)
        {
            Score = score;
			ScoreHistory.Add(new Tuple<double, Activity>(score, activity));
            if (winner)
            {
				Total.AddVictory();
				_winning.Add();
				_loosing.Reset();
                if (isOffence)
                {
					Offensive.AddVictory();
                }
                else
                {
                	Defensive.AddVictory();
                }
            }
            else
            {
				Total.AddDefeat();
				_winning.Reset();
				_loosing.Add();
                if (isOffence)
                {
					Offensive.AddDefeat();
                }
                else
                {
                	Defensive.AddDefeat();
                }
            }
        }
    }
}
