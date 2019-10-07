using System.Threading.Tasks;
using FightSearch.Service.DomainModels;
using FightSearch.Service.ResponseModels;

namespace FightSearch.Service.Interfaces
{
    /// <summary>
	/// Implements the logic used to find fights on a given page e.g. on the title fights page, the FindTitleFights method will be used.
	/// Custom logic to prevent web scraping:
	/// When returning the results, search all fights page will show content that is html markup or an image of the fight including
	/// the html markup that shows who is fighting whom, the result of the fight etc.
	/// This is to make scraping more difficult as it would require a combination of OCR and regular scraping.
	/// Everyone second fight i.e. mod%2==0 on wikifightid is a picture.
	/// </summary>
	public interface ISearchService
	{
		Task<SearchResultPaged> FindAwardFightsAsync(SearchQuery searchQuery);

		Task<SearchResultPaged> FindFightsAsync(SearchQuery searchQuery);

		Task<SearchResultPaged> FindFightsScrapeAsync(SearchQuery searchQuery, bool swapFighterNamesForScrape);

		Task<SearchResultPaged> FindTitleFightsAsync(SearchQuery searchQuery);

		Task<SearchResultPaged> FindTopRedditFightsAsync(SearchQuery searchQuery);
	}
}