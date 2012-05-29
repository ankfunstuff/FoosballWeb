using System;
using FoossballPlayars.Events;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
    public class EloCalculatorGoal : ScoreCalculatorBase, IScoreCalculator
    {
        public ScoreResult Calculate(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive, PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive, int goalsRed, int goalsBlue, int gamesPrPlayer)
        {
            var rating = new EloRatingGoal(redOffensive.Score + redDefensive.Score, blueOffensive.Score + blueDefensive.Score, goalsRed, goalsBlue, gamesPrPlayer);
            return GetScoreResult(redOffensive, redDefensive, blueOffensive, blueDefensive, goalsRed, goalsBlue, Math.Max(rating.Point1, rating.Point2));
        }
    }
}