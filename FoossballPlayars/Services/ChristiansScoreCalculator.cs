using System;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
    public class ChristiansScoreCalculator : ScoreCalculatorBase, IScoreCalculator
    {
        public ScoreResult Calculate(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive, PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive, int goalsRed, int goalsBlue, int gamesPrPlayer)
        {
            var existingRed = redOffensive.Score + redDefensive.Score;
            var existingBlue = blueOffensive.Score + blueDefensive.Score;
            var a = Math.Abs(goalsBlue - goalsRed);
            var b = Math.Abs(existingBlue - existingRed);

            const int minimumScore = 10;

            var winningPrice = (a*b*0.1)/2;

            if (!FavoriteTeamHasWon(goalsRed, goalsBlue, existingBlue, existingRed))
            {
                winningPrice = b/2;
            }
            winningPrice = Math.Max(minimumScore, winningPrice);
            return GetScoreResult(redOffensive, redDefensive, blueOffensive, blueDefensive, goalsRed, goalsBlue, winningPrice);
        }

        private static bool FavoriteTeamHasWon(double scoreRed, double scoreBlue, double existingBlue, double existingRed)
        {
            return (scoreRed > scoreBlue && existingRed > existingBlue) || (scoreBlue> scoreRed && existingBlue>existingRed);
        }
    }
}