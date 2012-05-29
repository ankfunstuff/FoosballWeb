using FoossballPlayars.Events;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
    public class SimpleScoreCalculator : ScoreCalculatorBase, IScoreCalculator
	{
        public ScoreResult Calculate(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive, PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive, int goalsRed, int goalsBlue, int gamesPrPlayer)
        {
            return GetScoreResult(redOffensive, redDefensive, blueOffensive, blueDefensive, goalsRed, goalsBlue, 10);
        }
	}
}