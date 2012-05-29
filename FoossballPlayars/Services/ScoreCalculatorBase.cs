using FoossballPlayars.Events;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
    public class ScoreCalculatorBase
    {
        protected ScoreResult GetScoreResult(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive,
                                             PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive,
                                             int goalsRed, int goalsBlue, double winningPrice)
        {
            if (goalsRed > goalsBlue)
            {
                return new ScoreResult(
                    GetWinningScore(redOffensive, winningPrice),
                    GetWinningScore(redDefensive, winningPrice),
                    GetLoosingScore(blueOffensive, winningPrice),
                    GetLoosingScore(blueDefensive, winningPrice),
                    winningPrice
                    );
            }
            return new ScoreResult(
                GetLoosingScore(redOffensive, winningPrice),
                GetLoosingScore(redDefensive, winningPrice),
                GetWinningScore(blueOffensive, winningPrice),
                GetWinningScore(blueDefensive, winningPrice),
                winningPrice
                );
        }

    	protected virtual Score GetWinningScore(PlayarStatisistics playar, double winningPrice)
        {
            return new Score(){ Id = playar.Id, ScoreCount= playar.Score + winningPrice};
        }

    	protected virtual Score GetLoosingScore(PlayarStatisistics playar, double winningPrice)
        {
            return new Score() { Id = playar.Id, ScoreCount = playar.Score - winningPrice };
        }
    }
}