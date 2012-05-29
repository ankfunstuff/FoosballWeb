using FoossballPlayars.Events;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
	public interface IScoreCalculator
	{
        ScoreResult Calculate(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive, PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive, int goalsRed, int goalsBlue, int gamesPrPlayer);
	}
}