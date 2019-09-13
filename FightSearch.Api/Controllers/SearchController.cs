using System.Threading.Tasks;
using FightSearch.Service;
using FightSearch.Service.DomainModels;
using FightSearch.Service.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FightSearch.Api.Controllers
{
    [EnableCors("Cors")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private bool _scrapeMyself;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [Route("Search", Name = "Search")]
        [HttpPost]
        public async Task<ActionResult<SearchResultPaged>> All([FromBody] SearchQuery searchQuery)
        {
            if (searchQuery.SearchParams.Sfn != null) _scrapeMyself = true;

            SearchResultPaged searchResultPaged;
            if (_scrapeMyself)
                searchResultPaged =
                    await _searchService.FindFightsScrapeAsync(searchQuery, searchQuery.SearchParams.Sfn.Value);
            else
                searchResultPaged = await _searchService.FindFightsAsync(searchQuery);
            return searchResultPaged;
        }

        [Route("SearchAwardFights", Name = "SearchAwardFights")]
        [HttpPost]
#if !DEBUG
        [ResponseCache(Duration = 864000)]
#endif
        public async Task<ActionResult<SearchResultPaged>> AwardFights([FromBody] SearchQuery searchQuery)
        {
            var fightSearchResults = await _searchService.FindAwardFightsAsync(searchQuery);
            return fightSearchResults;
        }

        [Route("SearchTitleFights", Name = "SearchTitleFights")]
        [HttpPost]
#if !DEBUG
        [ResponseCache(Duration = 864000)]
#endif
        public async Task<ActionResult<SearchResultPaged>> TitleFights([FromBody] SearchQuery searchQuery)
        {
            var fightSearchResults = await _searchService.FindTitleFightsAsync(searchQuery);
            return fightSearchResults;
        }

        [Route("SearchTopRedditFights", Name = "SearchTopRedditFights")]
        [HttpPost]
#if !DEBUG
        [ResponseCache(Duration = 864000)]
#endif
        public async Task<ActionResult<SearchResultPaged>> TopRedditFights([FromBody] SearchQuery searchQuery)
        {
            var fightSearchResults = await _searchService.FindTopRedditFightsAsync(searchQuery);
            return fightSearchResults;
        }
    }
}