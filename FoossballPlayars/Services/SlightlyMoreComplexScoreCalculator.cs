using System;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
    public class SlightlyMoreComplexScoreCalculator : ScoreCalculatorBase, IScoreCalculator
    {
        public ScoreResult Calculate(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive, PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive, int goalsRed, int goalsBlue, int gamesPrPlayer, DateTime dateTime)
        {
            var existingRed = redOffensive.Score + redDefensive.Score;
            var existingBlue = blueOffensive.Score + blueDefensive.Score;
            int winningPrice;
            double multiplier = Math.Abs(goalsBlue-goalsRed);
            if (goalsRed > goalsBlue)
            {
                winningPrice = (int) ((existingRed/Math.Max(existingBlue,1))*multiplier);
            }
            else
            {
                winningPrice = (int) ((existingBlue / Math.Max(existingRed, 1))*multiplier);
            }
            return GetScoreResult(redOffensive, redDefensive, blueOffensive, blueDefensive, goalsRed, goalsBlue, winningPrice, dateTime);
        }
    }
}