using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Toasket.Repository
{
    public class MatchRepository
    {
        public List<Match> Matches { get; } = new List<Match>();

        public MatchRepository(Crawler crawler)
        {
            Matches = crawler.CrawlMatches(2023, 16).ToList();
        }

        public void Add(IEnumerable<Match> matchs)
        {
            Matches.AddRange(matchs);
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
