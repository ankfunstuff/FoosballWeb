using FoossballPlayars.CommandContext;
using NUnit.Framework;

namespace Test.FoossballPlayars
{
    [TestFixture]
    public class ScoreValidatorUT
    {
        // negatives
        [TestCase(-1, 1, false)]
        [TestCase(10, -1, false)]
        [TestCase(-1, 10, false)]
        [TestCase(-1, -1, false)]
        // equals
        [TestCase(5, 5, false)]
        [TestCase(10, 10, false)]
        [TestCase(12, 12, false)]
        //// biggest smaller than SmallestWinningScore
        [TestCase(9, 5, false)]
        [TestCase(5, 9, false)]
        // diff to small
        [TestCase(10, 9, false)]
        [TestCase(9, 10, false)]
        [TestCase(11, 10, false)]
        [TestCase(10, 11, false)]
        [TestCase(12, 11, false)]
        [TestCase(11, 12, false)]
        //// diff to big
        [TestCase(11, 0, false)]
        [TestCase(0, 11, false)]
        [TestCase(12, 9, false)]
        [TestCase(9, 12, false)]
        [TestCase(15, 12, false)]
        [TestCase(12, 15, false)]
        //// valids
        [TestCase(10, 0, true)]
        [TestCase(0, 10, true)]
        [TestCase(5, 10, true)]
        [TestCase(10, 8, true)]
        [TestCase(8, 10, true)]
        [TestCase(11, 9, true)]
        [TestCase(11, 9, true)]
        [TestCase(12, 10, true)]
        [TestCase(12, 10, true)]
        [TestCase(13, 11, true)]
        [TestCase(11, 13, true)]
        public void ValidateScore(int scoreRed, int scoreBlue, bool expect)
        {
            var validator = new ScoreValidator(scoreRed, scoreBlue);
            Assert.AreEqual(expect, validator.IsValid, validator.Message);
        }


       
    }
}