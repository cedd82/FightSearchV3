using System.Collections.Generic;

namespace FightSearch.Service.ResponseModels
{
    public class SearchResultPaged
	{
		public int Count { get; set; }
        public IEnumerable<SearchResult> FightSearchResultsPaged { get; set; }
	}
}