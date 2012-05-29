using System;
using System.Collections.Generic;

namespace FoossballPlayars.QueryContext.Teams
{
	public interface ITeamService
	{
		IEnumerable<Team> GetBestTeams();
		IEnumerable<Team> GetAllTeams();
	}
}