using Jaywapp.Toasket.Model;
using Jaywapp.Toasket.Service;
using System.Collections.Generic;

namespace Jaywapp.Toasket.Repository
{
    public class MatchRepository
    {
        #region Properties
        /// <summary>
        /// 현재 회차의 매치 목록
        /// </summary>
        public List<Match> Matches { get; } = new List<Match>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="crawler"></param>
        public MatchRepository(Crawler crawler)
        {
            Matches = crawler.CrawlMatches();
        }
        #endregion
    }
}
