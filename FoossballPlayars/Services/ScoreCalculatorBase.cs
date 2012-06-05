using System;
using FoossballPlayars.Events;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
	public class ScoreCalculatorBase
	{
		protected ScoreResult GetScoreResult(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive,
											 PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive,
											 int goalsRed, int goalsBlue, double winningPrice, DateTime dateTime)
		{
			if (goalsRed > goalsBlue)
			{
				return new ScoreResult(
					GetWinningScore(redOffensive, winningPrice),
					GetWinningScore(redDefensive, winningPrice),
					GetLoosingScore(blueOffensive, winningPrice),
					GetLoosingScore(blueDefensive, winningPrice),
					winningPrice,
					GetFormattedStory(redOffensive, redDefensive, blueOffensive, blueDefensive, goalsRed, goalsBlue, winningPrice, dateTime)
					);
			}
			return new ScoreResult(
				GetLoosingScore(redOffensive, winningPrice),
				GetLoosingScore(redDefensive, winningPrice),
				GetWinningScore(blueOffensive, winningPrice),
				GetWinningScore(blueDefensive, winningPrice),
				winningPrice,
					GetFormattedStory(redOffensive, redDefensive, blueOffensive, blueDefensive, goalsRed, goalsBlue, winningPrice, dateTime)
				);
		}

		private Activity GetFormattedStory(PlayarStatisistics redOffensive, PlayarStatisistics redDefensive,
											 PlayarStatisistics blueOffensive, PlayarStatisistics blueDefensive,
											 int goalsRed, int goalsBlue, double winningPrice, DateTime dateTime)
		{
			if (goalsRed > goalsBlue)
			{
				if (goalsBlue == 0)
					return new Activity(string.Format("{0} and {1} gave vinkekat to {3} and {4} scoreing {2} points.",
													  redOffensive.Name,
													  redDefensive.Name,
													  winningPrice,
													  blueOffensive.Name,
													  blueDefensive.Name),
										dateTime);
				return new Activity(string.Format("{0} and {1} won {2} points against {3} and {4} ({5} - {6})",
												  redOffensive.Name,
												  redDefensive.Name,
												  winningPrice,
												  blueOffensive.Name,
												  blueDefensive.Name,
												  goalsRed,
												  goalsBlue),
									dateTime);
			}
			return new Activity(string.Format("{0} and {1} won {2} points against {3} and {4} ({5} - {6})",
											  blueOffensive.Name,
											  blueDefensive.Name,
											  winningPrice,
											  redOffensive.Name,
											  redDefensive.Name,
											  goalsBlue,
											  goalsRed),
								dateTime
				);
		}

		protected virtual Score GetWinningScore(PlayarStatisistics playar, double winningPrice)
		{
			return new Score() { Id = playar.Id, ScoreCount = playar.Score + winningPrice };
		}

		protected virtual Score GetLoosingScore(PlayarStatisistics playar, double winningPrice)
		{
			return new Score() { Id = playar.Id, ScoreCount = playar.Score - winningPrice };
		}
	}
}