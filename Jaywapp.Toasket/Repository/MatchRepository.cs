using Jaywapp.Toasket.Model;
using System;
using System.Collections.Generic;

namespace Jaywapp.Toasket.Repository
{
    public class MatchRepository
    {
        public List<Match> Matches { get; } = new List<Match>();

        public MatchRepository()
        {
            Matches = new List<Match>()
            {
                new Match(1, DateTime.Today, "Seoul", "Suwon", 2.5, 2.95, 2.4),
                new Match(2, DateTime.Today, "Real Madrid", "Manchester City", 2.01, 3.05, 2.5),
                new Match(3, DateTime.Today.AddDays(1), "Arsenal", "Manchester United", 1.95, 3.05, 3.2),
                new Match(4, DateTime.Today.AddDays(1), "Tottenham", "Bunrey", 1.2, 3.4, 5.12),
            };
        }

        public IEnumerable<Match> Collect(DateTime day)
        {
            foreach (var match in Matches)
            {
                if (match.Time.Year == day.Year
                    && match.Time.Month == day.Month
                    && match.Time.Day == day.Day)
                    yield return match;
            }
        }
    }
}
