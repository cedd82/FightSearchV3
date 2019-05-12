namespace FightSearch.Api.Controllers
{
    using System.Threading.Tasks;

    using FightSearch.Service;
    using FightSearch.Service.DomainModels;
    using FightSearch.Service.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
	[Route("api/[controller]")]
	public class SearchController : Controller
	{
		private readonly ISearchService _searchService;
        private bool _scrapeMyself = false;

	    public SearchController(ISearchService searchService)
		{
			_searchService = searchService;
		}

		[Route("Search", Name = "Search")]
		[HttpPost]
		public async Task<ActionResult<SearchResultPaged>> Search([FromBody] SearchQuery searchQuery)
		{
		    if (searchQuery.SearchParams.Sfn != null)
		    {
		        _scrapeMyself = true;
		    }

		    SearchResultPaged searchResultPaged;
		    if (_scrapeMyself)
		    {
		        searchResultPaged = await _searchService.FindFightsScrapeAsync(searchQuery, searchQuery.SearchParams.Sfn.Value);
		    }
		    else
		    {
		        searchResultPaged = await _searchService.FindFightsAsync(searchQuery);
		    }
            return searchResultPaged;
		}

		[Route("SearchAwardFights", Name = "SearchAwardFights")]
		[HttpPost]
    #if !DEBUG
		[ResponseCache(Duration = 864000)]
    #endif
		public async Task<ActionResult<SearchResultPaged>> SearchAwardFights([FromBody] SearchQuery searchQuery)
		{
			SearchResultPaged fightSearchResults = await _searchService.FindAwardFightsAsync(searchQuery);
			return fightSearchResults;
		}

		[Route("SearchTitleFights", Name = "SearchTitleFights")]
		[HttpPost]
    #if !DEBUG
		[ResponseCache(Duration = 864000)]
    #endif
		public async Task<ActionResult<SearchResultPaged>> SearchTitleFights([FromBody] SearchQuery searchQuery)
		{
			SearchResultPaged fightSearchResults = await _searchService.FindTitleFightsAsync(searchQuery);
			return fightSearchResults;
		}

		[Route("SearchTopRedditFights", Name = "SearchTopRedditFights")]
		[HttpPost]

    #if !DEBUG
        [ResponseCache(Duration = 864000)]
    #endif
		public async Task<ActionResult<SearchResultPaged>> SearchTopRedditFights([FromBody] SearchQuery searchQuery)
		{
			SearchResultPaged fightSearchResults = await _searchService.FindTopRedditFightsAsync(searchQuery);
			return fightSearchResults;
		}
	}
}