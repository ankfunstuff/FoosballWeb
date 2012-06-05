using FoossballPlayars.Events;
using FoossballPlayars.QueryContext;

namespace FoossballPlayars.Services
{
	public class ScoreResult
	{
        public ScoreResult(Score redOffensive, Score redDefensive, Score blueOffensive, Score blueDefensive, double points, Activity story)
	    {
	        RedOffensive = redOffensive;
	        RedDefensive = redDefensive;
	        BlueOffensive = blueOffensive;
	        BlueDefensive = blueDefensive;
            Points = points;
        	Story = story;
	    }

        public Score RedOffensive { get; private set; }
        public Score RedDefensive { get; private set; }
        public Score BlueOffensive { get; private set; }
        public Score BlueDefensive { get; private set; }

		private double Points { get; set; }
		public Activity Story { get; private set; }
	}
}