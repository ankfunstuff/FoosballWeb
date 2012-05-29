using System;

namespace FoossballPlayars.Commands
{
    public class PlayGameCommand
    {
        public Guid RedOffensive { get; set; }
        public Guid RedDefensive { get; set; }
        public Guid BlueOffensive { get; set; }
        public Guid BlueDefensive { get; set; }
        public int ScoreRed { get; set; }
        public int ScoreBlue { get; set; }

        public PlayGameCommand(
            Guid redOffensive,
            Guid redDefensive,
            Guid blueOffensive,
            Guid blueDefensive,
            int scoreRed,
            int scoreBlue)
        {
            RedOffensive = redOffensive;
            RedDefensive = redDefensive;
            BlueOffensive = blueOffensive;
            BlueDefensive = blueDefensive;
            ScoreRed = scoreRed;
            ScoreBlue = scoreBlue;
        }
    }
}