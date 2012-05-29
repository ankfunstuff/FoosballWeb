using System;
using System.Collections.Generic;

namespace FoossballPlayars.QueryContext
{
	public interface IScoreQuery
	{
		IEnumerable<Activity> GetActivities();
	    PlayarStatisistics GetStatistics(Guid id);
        Tuple<PlayarName, PlayarName> CurrentVinkekat { get;}
	    IEnumerable<PlayarStatisistics> GetTopPlayers();
	    IEnumerable<PlayarStatisistics> GetAllPlayers();
		double GetLowestHistoricalScore();
		double GetHighestHistoricalScore();
	}
}