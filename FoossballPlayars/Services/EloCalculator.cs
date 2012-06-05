using System;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
    public class EloCalculator : ScoreCalculatorBase, IScoreCalculator
    {
    	public ScoreResult Calculate(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive, PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive, int goalsRed, int goalsBlue, int gamesPrPlayer, DateTime dateTime)
        {
            var rating = new EloRating( redOffensive.Score + redDefensive.Score, blueOffensive.Score + blueDefensive.Score, goalsRed, goalsBlue, gamesPrPlayer);
			var winningPrice = Math.Max(rating.Point1, rating.Point2) + Math.Abs(goalsBlue - goalsRed);
        	return GetScoreResult(redOffensive, redDefensive, blueOffensive, blueDefensive, goalsRed, goalsBlue, winningPrice, dateTime);
        }

		protected override Events.Score GetWinningScore(PlayarStatisistics playar, double winningPrice)
		{
			if (playar.Total.GamesPlayed > PlayarStatisistics.InplayGames)
				return base.GetWinningScore(playar, winningPrice);
			return base.GetWinningScore(playar, 300);
		}

		protected override Events.Score GetLoosingScore(PlayarStatisistics playar, double winningPrice)
		{
			if (playar.Total.GamesPlayed > PlayarStatisistics.InplayGames)
				return base.GetLoosingScore(playar, winningPrice);
			return base.GetLoosingScore(playar, 0);
		}
    }
}