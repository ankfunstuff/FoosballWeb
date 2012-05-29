using System;

namespace FoossballPlayars.QueryContext
{
	public class GamePercentage
	{
		private int _gamesWon;
		private int _gamesLost;

		public void AddVictory()
		{
			_gamesWon++;
		}

		public void AddDefeat()
		{
			_gamesLost++;
		}

		public int GamesPlayed
		{
			get { return _gamesLost + _gamesWon; }
		}

		public int WinPercentage
		{
			get
			{
				var f = (float)_gamesWon / (Math.Max(GamesPlayed, 1));
				return (int)(f * 100);
			}
		}

		public int LoosingPercentage
		{
			get { return 100 - WinPercentage; }
		}


		public int GamesWon
		{
			get { return _gamesWon; }
		}
	}
}