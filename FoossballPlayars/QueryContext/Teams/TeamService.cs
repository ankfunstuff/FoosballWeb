using System;
using System.Collections.Generic;
using System.Linq;
using Ankiro.Framework.Tools.Bus;
using FoossballPlayars.Events;
using FoossballPlayars.Services;

namespace FoossballPlayars.QueryContext.Teams
{
	public class TeamService
		:
		Handles<GamePlayed>,
		Handles<PlayarRegistered>,
		ITeamService
	{
		private readonly IList<Team> _teams;
		private readonly IDictionary<Guid, string> _playars = new Dictionary<Guid, string>();

		public TeamService()
		{
			_teams = new List<Team>();
		}

		public void Handle(PlayarRegistered @event)
		{
			_playars.Add(@event.RootId, @event.PlayarName);
		}

		public void Handle(GamePlayed @event)
		{
			var redTeam = GetTeam(@event.RedOffensive, @event.RedDefensive);
			var blueTeam = GetTeam(@event.BlueOffensive, @event.BlueDefensive);
			var rating = new EloRating(redTeam.Score, blueTeam.Score, @event.ScoreRed, @event.ScoreBlue, 0);
			redTeam.UpdateScore(rating.Point1);
			blueTeam.UpdateScore(rating.Point2);
		}

		public IEnumerable<Team> GetBestTeams()
		{
			return _teams
				.Where(x=>x.Percentage.GamesPlayed>=10)
				.OrderByDescending(x => x.Score)
				.Take(10)
				.ToList();
		}

		public IEnumerable<Team> GetAllTeams()
		{
			return _teams
				.OrderByDescending(x => x.Score)
				.ToList();
		}

		private Team GetTeam(Guid offensive, Guid defensive)
		{
			var offensiveName = _playars[offensive];
			var defensiveName = _playars[defensive];
			var team = _teams.FirstOrDefault(x => offensiveName == x.Offensive && defensiveName == x.Defensive);
			if (team == null)
			{
				team = new Team(offensiveName, defensiveName);
				_teams.Add(team);
			}
			return team;
		}
	}
}