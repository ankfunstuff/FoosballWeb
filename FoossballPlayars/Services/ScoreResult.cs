using FoossballPlayars.Events;

namespace FoossballPlayars.Services
{
	public class ScoreResult
	{
        public ScoreResult(Score redOffensive, Score redDefensive, Score blueOffensive, Score blueDefensive, double points)
	    {
	        RedOffensive = redOffensive;
	        RedDefensive = redDefensive;
	        BlueOffensive = blueOffensive;
	        BlueDefensive = blueDefensive;
            Points = points;
	    }

        public Score RedOffensive { get; private set; }
        public Score RedDefensive { get; private set; }
        public Score BlueOffensive { get; private set; }
        public Score BlueDefensive { get; private set; }

        public double Points { get; private set; }
	}
}