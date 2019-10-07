using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightSearch.Service.DomainModels;
using FightSearch.Service.Interfaces;
using FightSearch.Service.ResponseModels;

namespace FightSearch.Service
{
    public class SearchServiceSqLite : ISearchService
    {
        public Task<SearchResultPaged> FindAwardFightsAsync(SearchQuery searchQuery)
        {
            throw new NotImplementedException();
        }

        public Task<SearchResultPaged> FindFightsAsync(SearchQuery searchQuery)
        {
            throw new NotImplementedException();
        }

        public Task<SearchResultPaged> FindFightsScrapeAsync(SearchQuery searchQuery, bool swapFighterNamesForScrape)
        {
            throw new NotImplementedException();
        }

        public Task<SearchResultPaged> FindTitleFightsAsync(SearchQuery searchQuery)
        {
            throw new NotImplementedException();
        }

        public Task<SearchResultPaged> FindTopRedditFightsAsync(SearchQuery searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}
