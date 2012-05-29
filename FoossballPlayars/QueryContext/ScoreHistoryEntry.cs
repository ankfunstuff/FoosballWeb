using System;
using System.Collections.Generic;
using System.Linq;

namespace FoossballPlayars.QueryContext
{
    public class ScoreHistory
    {
        private readonly IList<KeyValuePair<DateTime, IEnumerable<PlayarScore>>> _history = new List<KeyValuePair<DateTime, IEnumerable<PlayarScore>>>();

        public void Add(DateTime time, IEnumerable<PlayarScore> scores)
        {
            _history.Add(new KeyValuePair<DateTime, IEnumerable<PlayarScore>>(time, new List<PlayarScore>(scores)));
        }

        public IEnumerable<KeyValuePair<DateTime, IEnumerable<PlayarScore>>> GetScores()
        {
            var hc = new List<KeyValuePair<DateTime, IEnumerable<PlayarScore>>>(_history);
            hc.Reverse();
            foreach (var pair in hc)
            {
                var scores = (from playarScore in pair.Value
                              join score in _history.Last().Value on playarScore.PlayarName equals score.PlayarName
                              into k
                              from subScore in k.DefaultIfEmpty()
                              select playarScore);
                
                yield return new KeyValuePair<DateTime, IEnumerable<PlayarScore>>(pair.Key, scores);
            }
        }

        public IEnumerable<PlayarName> GetNames()
        {
            return _history.Last().Value.Select(x=>x.PlayarName);
        }

    }
}