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
        private readonly IDictionary<Guid, PlayarStatisistics> _playars = new Dictionary<Guid, PlayarStatisistics>();
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
            _playars.Add(id, new PlayarStatisistics(id, name));
            AddActivity(new Activity(name + " was registered.", timestamp));
            _playerCount++;
		}

        public void Handle(GamePlayed @event)
        {
            if (@event.ScoreBlue == 0)
            {
                CurrentVinkekat = new Tuple<PlayarName, PlayarName>(GetName(@event.BlueOffensive), GetName(@event.BlueDefensive));
            }
            if (@event.ScoreRed == 0)
            {
                CurrentVinkekat = new Tuple<PlayarName, PlayarName>(GetName(@event.RedOffensive), GetName(@event.RedDefensive));
            }
            if (@event.ScoreBlue == 1 || @event.ScoreRed == 1)
            {
                AddActivity(new Activity("A vinkekat was close!", @event.Date));
            }
            _gamesPlayed++;
            var result = _scoreCalculator.Calculate(LoadScore(@event.RedOffensive),
                                                    LoadScore(@event.RedDefensive),
                                                    LoadScore(@event.BlueOffensive),
                                                    LoadScore(@event.BlueDefensive),
                                                    @event.ScoreRed,
                                                    @event.ScoreBlue,
                                                    _gamesPlayed/_playerCount);
            var story= GetFormattedStory(@event, result);
            _activitites.Add(story);
            SetScore(result, story, @event.RedWinner);
        	UpdateMinMax(result);
        }

    	public IEnumerable<PlayarStatisistics> GetTopPlayers()
        {
            return _playars.Values.OrderByDescending(x=>x.Score).Take(10);
        }

		public IEnumerable<PlayarStatisistics> GetAllPlayers()
		{
			return _playars.Values.OrderByDescending(x => x.Score);
		}

    	public PlayarStatisistics GetStatistics(Guid id)
        {
            return _playars[id];
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

    	private void SetScore(ScoreResult result, Activity story, bool redWinner)
    	{
    		_playars[result.RedOffensive.Id].UpdateScore(result.RedOffensive.ScoreCount, story, redWinner, true);
    		_playars[result.RedDefensive.Id].UpdateScore(result.RedDefensive.ScoreCount, story, redWinner, false);
    		_playars[result.BlueOffensive.Id].UpdateScore(result.BlueOffensive.ScoreCount, story, !redWinner, true);
    		_playars[result.BlueDefensive.Id].UpdateScore(result.BlueDefensive.ScoreCount, story, !redWinner, false);

    	}

    	private Activity GetFormattedStory(GamePlayed @event, ScoreResult result)
    	{
    		if (@event.ScoreRed > @event.ScoreBlue)
    		{
    			if (@event.ScoreBlue == 0)
    				return new Activity(string.Format("{0} and {1} gave vinkekat to {3} and {4} scoreing {2} points.",
    				                                  GetName(@event.RedOffensive),
    				                                  GetName(@event.RedDefensive),
    				                                  result.Points,
    				                                  GetName(@event.BlueOffensive),
    				                                  GetName(@event.BlueDefensive)),
    				                    @event.Date);
    			return new Activity(string.Format("{0} and {1} won {2} points against {3} and {4} ({5} - {6})",
    			                                  GetName(@event.RedOffensive),
    			                                  GetName(@event.RedDefensive),
    			                                  result.Points,
    			                                  GetName(@event.BlueOffensive),
    			                                  GetName(@event.BlueDefensive),
    			                                  @event.ScoreRed,
    			                                  @event.ScoreBlue),
    			                    @event.Date);
    		}
    		return new Activity(string.Format("{0} and {1} won {2} points against {3} and {4} ({5} - {6})",
    		                                  GetName(@event.BlueOffensive),
    		                                  GetName(@event.BlueDefensive),
    		                                  result.Points,
    		                                  GetName(@event.RedOffensive),
    		                                  GetName(@event.RedDefensive),
    		                                  @event.ScoreBlue,
    		                                  @event.ScoreRed),
    		                    @event.Date
    			);
    	}

    	private PlayarStatisistics LoadScore(Guid id)
    	{
    		return  _playars[id];
    	}

		private PlayarName GetName(Guid id)
		{
			return _playars[id].Name;
		}

	}


}