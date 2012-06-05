using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoossballPlayars.QueryContext
{
	public class MinMaxCache<T>
		where T:IComparable
	{
		public T Max { get; private set; }
		public T Min { get; private set; }

		public MinMaxCache()
		{
		}

		public MinMaxCache(T max, T min)
		{
			Max = max;
			Min = min;
		}

		public void Update(params T[] values)
		{
			var localMin = values.Min();
			var localMax = values.Max();
			if (localMax.CompareTo(Max) > 0)
			{
				Max = localMax;
			}
			if (localMin.CompareTo(Min)< 0)
			{
				Min = localMin;
			}
		}
	}
}
