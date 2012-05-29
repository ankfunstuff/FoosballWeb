using System;

namespace FoossballPlayars.QueryContext
{
	public class PlayarScore
	{
        public Guid Id { get; private set; }
        public PlayarName PlayarName { get; private set; }
		public double Score { get; private set; }

        public PlayarScore(Guid id, PlayarName playarName, double score)
	    {
	        Id = id;
	        PlayarName = playarName;
	        Score = score;
	    }

        public void SetScore(double score)
        {
            Score = score;
        }
	}
}