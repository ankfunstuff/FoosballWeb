using System;
using Ankiro.Framework.Extensions;

namespace FoossballPlayars.QueryContext
{
	public class Activity
	{
		public string Message { get; private set; }
		public DateTime Timestamp { get;  private set; }

		public Activity(string message, DateTime timestamp)
		{
			Message = message;
			Timestamp = timestamp;
		}

		public override string ToString()
		{
			return Message + " " + Timestamp.ToPrettyString();
		}
	}
}