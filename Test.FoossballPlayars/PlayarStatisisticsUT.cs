using System;
using FoossballPlayars.QueryContext;
using NUnit.Framework;

namespace Test.FoossballPlayars
{
    [TestFixture]
    public class PlayarStatisisticsUT
    {
        [Test]
        public void CanCalulateWinPercentage()
        {
            var p = new PlayarStatisistics(Guid.Empty, new PlayarName("Test"));
            p.UpdateScore(8,new Activity("", new DateTime(1,1,2)), true, false);
            p.UpdateScore(8,new Activity("", new DateTime(1,1,2)), true, true);
            p.UpdateScore(8,new Activity("", new DateTime(1,1,2)), true, true);
            p.UpdateScore(8,new Activity("", new DateTime(1,1,2)), false, true);
            p.UpdateScore(8,new Activity("", new DateTime(1,1,2)), false, false);
            p.UpdateScore(8,new Activity("", new DateTime(1,1,2)), false, false);

            Assert.AreEqual(50, p.Total.WinPercentage);
            //Assert.AreEqual(98, p.WinPercentageAsOffensive + p.WinPercentageAsDefensive + p.LoosePercentageAsOffensive + p.LoosePercentageAsDefensive);
            Assert.AreEqual(66, p.Offensive.WinPercentage);
            Assert.AreEqual(33, p.Defensive.WinPercentage);
            Assert.AreEqual(67, p.Defensive.LoosingPercentage);
			//Assert.AreEqual(16, p.LoosePercentageAsOffensive);
			//Assert.AreEqual(33, p.LoosePercentageAsDefensive);
        }
    }
}