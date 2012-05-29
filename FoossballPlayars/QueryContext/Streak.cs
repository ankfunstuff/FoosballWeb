namespace FoossballPlayars.QueryContext
{
	public class Streak
	{
		private readonly int _streakValue;
		private int _value;

		public Streak(int streakValue)
		{
			_streakValue = streakValue;
		}

		public bool IsStreak
		{
			get { return _value >= _streakValue; }
		}

		public void Add()
		{
			_value++;
		}

		public void Reset()
		{
			_value = 0;
		}
	}
}