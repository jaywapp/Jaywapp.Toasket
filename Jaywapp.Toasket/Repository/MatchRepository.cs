using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Service;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Toasket.Repository
{
    public class MatchRepository
    {
        #region Properties
        public List<Match> Matches { get; } = new List<Match>();
        #endregion

        #region Constructor
        public MatchRepository(Crawler crawler)
        {
            Matches = crawler.CrawlMatches();
        }
        #endregion
    }
}
