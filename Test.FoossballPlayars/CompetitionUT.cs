using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoossballPlayars.CommandContext;
using NUnit.Framework;

namespace Test.FoossballPlayars
{
    [TestFixture]
    public class CompetitionUT
    {
        [Test]
        public void CanPlay()
        {
            var competiontion = new Competition(Guid.Empty);
            var p1 = new Guid("d912dfb3-f30b-4e93-acc1-ce66a0e17884");
            var p2 = new Guid("c7b5f0dd-8b0a-4d48-ae6f-75387d7fa8ec");
            var p3 = new Guid("50ab6599-c98b-4157-9aa0-7c7e9c6a63b7");
            var p4 = new Guid("c16bdaef-dccd-416d-bee8-91b2768186cc");
            competiontion.AddPlayer(p1);
            competiontion.AddPlayer(p2);
            competiontion.AddPlayer(p3);
            competiontion.AddPlayer(p4);
            competiontion.PlayGame(p1, p2, p3, p4, 10, 8);
        }
    }
}
