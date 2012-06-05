using System;
using System.Collections.Generic;
using System.Linq;
using FoossballPlayars.Events;
using FoossballPlayars.Services;

namespace FoossballPlayars.QueryContext
{
    public class ScoreQuery : IScoreQuery
	{
	    private readonly ISignaler _signaler;
        private readonly IScoreCalculator _scoreCalculator;
        private readonly IDictionary<Guid, PlayarStatisistics> _playerCache = new Dictionary<Guid, PlayarStatisistics>();
		private readonly IList<Activity> _activitites = new List<Activity>();
        public Tuple<PlayarName, PlayarName> CurrentVinkekat { get; private set; }

    	private double _lowestHistoricalScore;
    	private double _highestHistoricalScore;

        private int _gamesPlayed;
        private int _playerCount;

	    public ScoreQuery(ISignaler signaler, IScoreCalculator scoreCalculator)
	    {
	        _signaler = signaler;
	        _scoreCalculator = scoreCalculator;
	        _gamesPlayed = 0;
	        _playerCount = 0;
	    	_highestHistoricalScore = PlayarStatisistics.InitialScore;
			_lowestHistoricalScore = PlayarStatisistics.InitialScore;
	    }

        public void AddPlayar(Guid id, PlayarName name, DateTime timestamp)
		{
            _playerCache.Add(id, new PlayarStatisistics(id, name));
            AddActivity(new Activity(name + " was registered.", timestamp));
            _playerCount++;
		}

        public void Handle(GamePlayed @event)
        {
			_gamesPlayed++;
			HandleVinkekatSituations(@event);
            var result = _scoreCalculator.Calculate(_playerCache[@event.RedOffensive],
                                                    _playerCache[@event.RedDefensive],
                                                    _playerCache[@event.BlueOffensive],
                                                    _playerCache[@event.BlueDefensive],
                                                    @event.ScoreRed,
                                                    @event.ScoreBlue,
													_gamesPlayed / _playerCount, @event.Date);
            _activitites.Add(result.Story);
            SetScore(result, @event.RedWinner);
        	UpdateMinMax(result);
        }

    	private void HandleVinkekatSituations(GamePlayed @event)
    	{
    		if (@event.ScoreBlue == 0)
    		{
    			CurrentVinkekat = new Tuple<PlayarName, PlayarName>(_playerCache[@event.BlueOffensive].Name,
    			                                                    _playerCache[@event.BlueDefensive].Name);
    		}
    		if (@event.ScoreRed == 0)
    		{
    			CurrentVinkekat = new Tuple<PlayarName, PlayarName>(_playerCache[@event.RedOffensive].Name,
    			                                                    _playerCache[@event.RedDefensive].Name);
    		}
    		if (@event.ScoreBlue == 1 || @event.ScoreRed == 1)
    		{
    			AddActivity(new Activity("A vinkekat was close!", @event.Date));
    		}
    	}

    	public IEnumerable<PlayarStatisistics> GetTopPlayers()
        {
            return _playerCache.Values.OrderByDescending(x=>x.Score).Take(10);
        }

		public IEnumerable<PlayarStatisistics> GetAllPlayers()
		{
			return _playerCache.Values.OrderByDescending(x => x.Score);
		}

    	public PlayarStatisistics GetStatistics(Guid id)
        {
            return _playerCache[id];
        }

    	public IEnumerable<Activity> GetActivities()
    	{
    		var list = new List<Activity>(_activitites);
    		list.Reverse();
    		return list;
    	}

		public double GetLowestHistoricalScore()
		{
			return _lowestHistoricalScore;
		}

		public double GetHighestHistoricalScore()
		{
			return _highestHistoricalScore;
		}

    	private void AddActivity(Activity activity)
	    {
	        _activitites.Add(activity);
            _signaler.Signal();
        }

    	private void UpdateMinMax(ScoreResult result)
    	{
    		var localMax = Math.Max(Math.Max(result.BlueDefensive.ScoreCount, result.BlueOffensive.ScoreCount), Math.Max(result.RedDefensive.ScoreCount, result.RedOffensive.ScoreCount));
    		var localMin = Math.Min(Math.Min(result.BlueDefensive.ScoreCount, result.BlueOffensive.ScoreCount), Math.Min(result.RedDefensive.ScoreCount, result.RedOffensive.ScoreCount));
    		if (localMax > _highestHistoricalScore)
    		{
    			_highestHistoricalScore = localMax;
    		}
    		if (localMin < _lowestHistoricalScore)
    		{
    			_lowestHistoricalScore = localMin;
    		}
    	}

    	private void SetScore(ScoreResult result, bool redWinner)
    	{
			_playerCache[result.RedOffensive.Id].UpdateScore(result.RedOffensive.ScoreCount, result.Story, redWinner, true);
			_playerCache[result.RedDefensive.Id].UpdateScore(result.RedDefensive.ScoreCount, result.Story, redWinner, false);
			_playerCache[result.BlueOffensive.Id].UpdateScore(result.BlueOffensive.ScoreCount, result.Story, !redWinner, true);
			_playerCache[result.BlueDefensive.Id].UpdateScore(result.BlueDefensive.ScoreCount, result.Story, !redWinner, false);
    	}
	}
}