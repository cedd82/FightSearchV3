namespace FightSearch.Service.ViewModels
{
	using System.Collections.Generic;

	public class SearchResultPaged
	{
		public int Count { get; set; }
        public IEnumerable<SearchResult> FightSearchResultsPaged { get; set; }
	}
}