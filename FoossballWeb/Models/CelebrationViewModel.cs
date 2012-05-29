using System;
using System.Collections.Generic;
using FoossballPlayars.QueryContext;

namespace FoossballWeb.Models
{
    public class CelebrationViewModel
    {
        public IEnumerable<PlayarStatisistics> Scores { get; set; }
        public Guid RedOffensive { get; set; }
        public Guid RedDefensive { get; set; }
        public Guid BlueOffensive { get; set; }
        public Guid BlueDefensive { get; set; }
        public int ScoreRed { get; set; }
        public int ScoreBlue { get; set; }
    }
}