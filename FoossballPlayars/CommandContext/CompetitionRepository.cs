using System;
using System.Collections.Generic;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest;

namespace FoossballPlayars.CommandContext
{
    public class CompetitionRepository : Repository<Competition>, IRepository<Competition>
    {
        private static readonly Guid InternalId = Guid.Parse("{21EC2020-3AEA-1069-A2DD-08002B30309D}");

        public CompetitionRepository(IBus eventBus) : base(eventBus)
        {
        }

        public static Competition GetOrCreate(IBus eventBus)
        {
            var competitionRepository = new CompetitionRepository(eventBus);
            var competition = competitionRepository[InternalId];
            if (competition == null)
            {
                competition = new Competition(InternalId);
                competitionRepository.Add(competition);
            }
            return competition;
        }

        protected override Competition CreateInstance(Guid id, IEnumerable<object> events)
        {
            return new Competition(id, events);
        }
    }
}