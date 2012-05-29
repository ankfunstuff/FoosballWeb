namespace FoossballPlayars.CommandContext
{
    public class ScoreValidator
    {
        public bool IsValid { get; private set; }
        public string Message { get; private set; }

        private readonly int _scoreRed;
        private readonly int _scoreBlue;

        public ScoreValidator(int scoreRed, int scoreBlue)
        {
            this._scoreRed = scoreRed;
            this._scoreBlue = scoreBlue;
            this.IsValid = true;

            Validate();
        }

        private void Validate()
        {
            ScoresNotNegative();
            if (!IsValid) { return; }
            ScoresNotEqual();
            if (!IsValid) { return; }
            WinningScoreAtLeastTen();
            if (!IsValid) { return; }
            ScoreDiffIsValid();
            if (!IsValid) { return; }
        }

        private void ScoresNotNegative()
        {
            IsValid = _scoreRed >= 0 && _scoreBlue >= 0;
            if (!IsValid)
                Message = "Scores are negative";
        }
        private void ScoresNotEqual()
        {
            IsValid = _scoreRed != _scoreBlue;
            if (!IsValid)
                Message = "Scores are equal";
        }
        private void WinningScoreAtLeastTen()
        {
            IsValid = _scoreRed > 9 || _scoreBlue > 9;
            if (!IsValid)
                Message = "Winning score must at least have the value ten";
        }
        private void ScoreDiffIsValid()
        {
            var winningScore = _scoreRed > _scoreBlue ? _scoreRed : _scoreBlue;
            var loosingScore = _scoreRed > _scoreBlue ? _scoreBlue : _scoreRed;
            var diff = winningScore - loosingScore;

            if (winningScore == 10 && diff < 2)
            {
                IsValid = false;
                Message = "Expected score diff of at at least two goals";
                return;
            }

            if (winningScore > 10 && diff != 2)
            {
                IsValid = false;
                Message = "Expected score diff of exactly two goals";
                return;
            }
        }
    }
}