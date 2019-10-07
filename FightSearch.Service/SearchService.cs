using FightSearch.Service.Interfaces;
using FightSearch.Service.ResponseModels;

namespace FightSearch.Service
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using FightSearch.Common.Constants;
	using FightSearch.Common.Enums;
	using FightSearch.Common.Settings;
	using FightSearch.Repository.Sql;
    using FightSearch.Repository.Sql.Entities;
    using FightSearch.Service.DomainModels;
    using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Options;

	/// <summary>
	/// Implements the logic used to find fights on a given page e.g. on the title fights page, the FindTitleFights method will be used.
	/// Custom logic to prevent web scraping:
	/// When returning the results, search all fights page will show content that is html markup or an image of the fight including
	/// the html markup that shows who is fighting whom, the result of the fight etc.
	/// This is to make scraping more difficult as it would require a combination of OCR and regular scraping.
	/// Everyone second fight i.e. mod%2==0 on wikifightid is a picture.
	/// </summary>
	public class SearchService : ISearchService
	{
		private readonly IFightSearchEntities _fightSearchEntities;
		private readonly IOptions<ImagePaths> _imagePathSettings;

		public SearchService(IOptions<ImagePaths> imagePathSettings, IFightSearchEntities fightSearchEntities)
		{
			_fightSearchEntities = fightSearchEntities;
			_imagePathSettings = imagePathSettings;
		}

		public async Task<SearchResultPaged> FindAwardFightsAsync(SearchQuery searchQuery)
		{
			Expression<Func<WikiFightWeb, bool>> where = e => e.Fotn || e.Potn;
			if (searchQuery.SearchParams.AwardType == AwardTypes.POTN)
			{
			    where = e => e.Potn;
			}
			else if (searchQuery.SearchParams.AwardType == AwardTypes.FOTN)
			{
			    where = e => e.Fotn;
			}

		    SearchResultPaged searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, where);
			return searchResultPaged;
		}

		public async Task<SearchResultPaged> FindFightsAsync(SearchQuery searchQuery)
		{
			SearchResultPaged searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, true);
			return searchResultPaged;
		}

		public async Task<SearchResultPaged> FindFightsScrapeAsync(SearchQuery searchQuery, bool swapFighterNamesForScrape)
		{
		    string fighterName = searchQuery.SearchParams.Fighter1Name;
		    IQueryable<SearchResult> query = (from wikiFightWeb in _fightSearchEntities.WikiFightWeb
		                                              join wikiFight in _fightSearchEntities.WikiFight on wikiFightWeb.WikiFightId equals wikiFight.Id
		                                              join fight in _fightSearchEntities.Fight on wikiFight.FightId equals fight.Id
		                                              orderby wikiFightWeb.Id
		                                              select new SearchResult
		                                              {
		                                                  //imgUrl
		                                                  //imageForWebOriginal    for spoilers
		                                                  //imageForWebNameMixed   for spoilers 
		                                                  WId = wikiFightWeb.WikiFightId,
		                                                  Fighter1Name = wikiFightWeb.Fighter1Name,
		                                                  Fighter1NameOriginal = wikiFightWeb.Fighter1Name,
		                                                  Fighter2Name = wikiFightWeb.Fighter2Name,
		                                                  Fighter2NameOriginal = wikiFightWeb.Fighter2Name,
		                                                  FightResultHow = wikiFightWeb.FightResultHow,
		                                                  Round = wikiFightWeb.Round,
		                                                  Time = wikiFightWeb.Time,
		                                                  TotalTime = wikiFightWeb.TotalTime,
		                                                  WEventName = wikiFightWeb.EventName,
		                                                  DateHeld = wikiFightWeb.DateHeld,
		                                                  VideoLink = wikiFightWeb.VideoLink,
		                                                  Rank = wikiFightWeb.RedditTopFights,
		                                                  Fotn = wikiFightWeb.Fotn,
		                                                  Potn = wikiFightWeb.Potn,
		                                                  WeightClass = wikiFightWeb.WeightClass,
		                                                  ImageForWeb = wikiFightWeb.ImageForWeb,
		                                                  ImgUrl = fight.ImagePath,
		                                                  ImageForWebNameMixed = string.Empty,
		                                                  ImageForWebOriginal = string.Empty,
		                                              });

		    int count = await query.CountAsync();
		    PagingInfo pagingInfo = searchQuery.PagingInfo;
		    query = query.Skip((pagingInfo.Page - 1) * pagingInfo.ItemsPerPage).Take(pagingInfo.ItemsPerPage);
		    List<SearchResult> searchResults = await query.ToListAsync();
		    foreach (SearchResult searchResult in searchResults)
		    {
		        searchResult.ImgUrl = searchResult.ImgUrl.Replace(@"\", "/").Replace("Assets","");
		    }
		    SearchResultPaged result = new SearchResultPaged
		    {
		        Count = count,
		        FightSearchResultsPaged = searchResults
		    };
		    return result;
		}

		public async Task<SearchResultPaged> FindTitleFightsAsync(SearchQuery searchQuery)
		{
			Expression<Func<WikiFightWeb, bool>> where = e => e.TitleFight == true;
			SearchResultPaged searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, where);
			return searchResultPaged;
		}

		public async Task<SearchResultPaged> FindTopRedditFightsAsync(SearchQuery searchQuery)
		{
			Expression<Func<WikiFightWeb, bool>> where = e => e.RedditTopFights != 0;
			Expression<Func<WikiFightWeb, int>> orderBy = e => e.RedditTopFights;
			SearchResultPaged searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, where, orderBy);
			return searchResultPaged;
		}

		private static IQueryable<WikiFightWeb> FilterDuration(SearchParams searchQueryParams, IQueryable<WikiFightWeb> query)
		{
			switch (searchQueryParams.Duration)
			{
				case Durations.Five:
					query = query.Where(e => e.TotalTimeEnum == (int) Durations.Five);
					break;
				case Durations.Ten:
					query = query.Where(e => e.TotalTimeEnum == (int) Durations.Ten);
					break;
				case Durations.FifteenOrLess:
					query = query.Where(e => e.TotalTimeEnum == (int) Durations.FifteenOrLess);
					break;
				case Durations.Fifteen:
					query = query.Where(e => e.TotalTimeEnum == (int) Durations.Fifteen);
					break;
				case Durations.FifteenPlus:
					query = query.Where(e => e.TotalTimeEnum == (int) Durations.FifteenPlus);
					break;
				case Durations.Any:
					break;
				default:
					break;
			}
			return query;
		}

		private static IQueryable<WikiFightWeb> FilterFighterName(SearchParams searchParams, IQueryable<WikiFightWeb> query)
		{
			if (string.IsNullOrEmpty(searchParams.Fighter1Name))
			{
				return query;
			}
			string fighterName = searchParams.Fighter1Name;
			Expression<Func<WikiFightWeb, bool>> where = e => e.Fighter1Name.Equals(fighterName) || e.Fighter2Name.Equals(fighterName);
			query = query.Where(where);
			//query = query.Where(e => e.Fighter1Name.Equals(fighterName) || e.Fighter2Name.Equals(fighterName));
			return query;
		}

		private static IQueryable<WikiFightWeb> FilterFinishType(SearchParams searchQueryParams, IQueryable<WikiFightWeb> query)
		{
			if (searchQueryParams.FinishType == FinishTypes.Any)
				return query;

			switch (searchQueryParams.FinishType)
			{
				case FinishTypes.Submission:
					{
						query = query.Where(e => e.FightResultType == FinishType.Submission);
						if (searchQueryParams.SubmissionType != FinishType.Any)
						{
							query = query.Where(e => e.FightResultSubType == searchQueryParams.SubmissionType);
						}
						break;
					}
				case FinishTypes.TKO:
				case FinishTypes.KO:
					{
						query = query.Where(e => e.FightResultType == FinishType.KO || e.FightResultType == FinishType.TKO);
						if (searchQueryParams.KoType != FinishType.Any)
						{
							query = query.Where(e => e.FightResultSubType == searchQueryParams.KoType);
						}
						break;
					}
				case FinishTypes.DecisionUnanimous:
					query = query.Where(e => e.FightResultType == FinishType.DecisionUnanimous);
					break;
				case FinishTypes.Decision:
					query = query.Where(e => e.FightResultSubType == FinishType.DecisionMajority || e.FightResultSubType == FinishType.DecisionSplit || e.FightResultSubType == FinishType.Decision);
					break;
				case FinishTypes.Others:
					query = query.Where(e => e.FightResultType == FinishType.Draw || e.FightResultType == FinishType.NoContest || e.FightResultType == FinishType.Disqualification);
					break;
			}

			return query;
		}

		private static IQueryable<WikiFightWeb> FilterFreeFights(SearchParams searchQueryParams, IQueryable<WikiFightWeb> query)
		{
			if (searchQueryParams.FreeFightsOnly)
			{
				query = query.Where(e => e.Promotion == Promotion.Bellator);
			}
			return query;
		}

		private static IQueryable<WikiFightWeb> FilterWeightDivision(SearchParams searchQueryParams, IQueryable<WikiFightWeb> query)
		{
			switch (searchQueryParams.WeightDivision)
			{
				case WeightDivisions.Flyweight:
					query = query.Where(e => e.WeightClass == WeightDivision.Flyweight);
					break;
				case WeightDivisions.Bantamweight:
					query = query.Where(e => e.WeightClass == WeightDivision.Bantamweight);
					break;
				case WeightDivisions.Featherweight:
					query = query.Where(e => e.WeightClass == WeightDivision.Featherweight);
					break;
				case WeightDivisions.Lightweight:
					query = query.Where(e => e.WeightClass == WeightDivision.Lightweight);
					break;
				case WeightDivisions.Welterweight:
					query = query.Where(e => e.WeightClass == WeightDivision.Welterweight);
					break;
				case WeightDivisions.Middleweight:
					query = query.Where(e => e.WeightClass == WeightDivision.Middleweight);
					break;
				case WeightDivisions.LightHeavyweight:
					query = query.Where(e => e.WeightClass == WeightDivision.LightHeavyweight);
					break;
				case WeightDivisions.Heavyweight:
					query = query.Where(e => e.WeightClass == WeightDivision.Heavyweight);
					break;
				case WeightDivisions.SuperHeavyweight:
					query = query.Where(e => e.WeightClass == WeightDivision.SuperHeavyweight);
					break;
				case WeightDivisions.WomensStrawweight:
					query = query.Where(e => e.WeightClass == WeightDivision.WomensStrawweight);
					break;
				case WeightDivisions.WomensFlyweight:
					query = query.Where(e => e.WeightClass == WeightDivision.WomensFlyweight);
					break;
				case WeightDivisions.WomensBantamweight:
					query = query.Where(e => e.WeightClass == WeightDivision.WomensBantamweight);
					break;
				case WeightDivisions.WomensFeatherweight:
					query = query.Where(e => e.WeightClass == WeightDivision.WomensFeatherweight);
					break;
				case WeightDivisions.Any:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return query;
		}

		private static IQueryable<WikiFightWeb> FilterYear(SearchParams searchQueryParams, IQueryable<WikiFightWeb> query)
		{
			if (searchQueryParams.Year.Equals(General.Any, StringComparison.OrdinalIgnoreCase))
			{
				return query;
			}

			query = query.Where(e => e.DateHeld.Year.Equals(int.Parse(searchQueryParams.Year)));
			return query;
		}

		private async Task<SearchResultPaged> FindFightsQueryBuilder(SearchParams searchQueryParams, PagingInfo pagingInfo, bool preventScraping, Expression<Func<WikiFightWeb, bool>> additionalQuery = null, Expression<Func<WikiFightWeb, int>> orderBy = null)
		{
			IQueryable<WikiFightWeb> query = _fightSearchEntities.WikiFightWeb.AsNoTracking();
			query = FilterFighterName(searchQueryParams, query);
			query = FilterFinishType(searchQueryParams, query);
			query = FilterYear(searchQueryParams, query);
			query = FilterDuration(searchQueryParams, query);
			query = FilterWeightDivision(searchQueryParams, query);
			query = FilterFreeFights(searchQueryParams, query);

			if (additionalQuery != null)
			{
				query = query.Where(additionalQuery);
			}

			// clustered index on f.id so order is always by when the date is held of fight
			query = orderBy != null ? query.OrderBy(orderBy) : query.OrderBy(f => f.Id);

			SearchResultPaged searchResultPaged = await GetSearchResults(query, pagingInfo, preventScraping);
			return searchResultPaged;
		}

		/// <summary>
		/// Formats the results, does so to prevent scraping, on even numbers from the wikifightid, set a screenshot of the content of the fight link
		/// In the front end this provides a list of images of the fight link with text and links with html markup
		/// </summary>
		/// <param name="preventScraping">if set to <c>true</c> [prevent scraping].</param>
		/// <param name="searchResults"></param>
		private void FormatResultsToPreventScraping(bool preventScraping, List<SearchResult> searchResults)
		{
			foreach (SearchResult searchResult in searchResults)
			{
				if (searchResult.WId % 2 == 0 && preventScraping)
				{
					searchResult.WeightClass = "";
					//searchResult.ImageForWeb = searchResult?.ImageForWeb == null ? "" : searchResult?.ImageForWeb;
					searchResult.ImageForWeb = searchResult?.ImageForWeb ?? "";
					searchResult.Fighter1Name = "";
					searchResult.Fighter1NameOriginal = "";
					searchResult.Fighter2Name = "";
					searchResult.Fighter2NameOriginal = "";
					searchResult.FightResultHow = "";
					searchResult.Round = "";
					searchResult.Time = "";
					searchResult.TotalTime = TimeSpan.MinValue;
					searchResult.DateHeld = DateTime.Now;
					searchResult.WEventName = "";
					searchResult.ImgUrl = "";
					searchResult.ImageForWebNameMixed = _imagePathSettings.Value.ImageForWeb + "s" + searchResult.ImageForWeb;
					searchResult.ImageForWebOriginal = _imagePathSettings.Value.ImageForWeb + searchResult.ImageForWeb;
					searchResult.ImageForWeb = "";
				}
				else
				{
					searchResult.ImgUrl = _imagePathSettings.Value.ImagePath + searchResult.ImgUrl;
					searchResult.ImageForWeb = "";
					searchResult.ImageForWebNameMixed = "";
					searchResult.ImageForWebOriginal = "";
					if (searchResult.Fotn.Equals(true))
					{
						searchResult.AwardType = "Fight Of The Night";
					}
					else if (searchResult.Potn.Equals(true))
					{
						searchResult.AwardType = "Performance Of The Night";
					}
				}
			}
		}

		/// <summary>
		/// Returns results from the search query from the database
		/// </summary>
		private async Task<SearchResultPaged> GetSearchResults(IQueryable<WikiFightWeb> query, PagingInfo pagingInfo, bool preventScraping)
		{
			int count = await query.CountAsync();
			// todo: this stops people getting all results from db, but i need to remove from api query instead
			pagingInfo.ItemsPerPage = 9;
			query = query.Skip((pagingInfo.Page - 1) * pagingInfo.ItemsPerPage).Take(pagingInfo.ItemsPerPage);

			// todo use automapper
			List<SearchResult> searchResults = await query.Select(e => new SearchResult {
					WId = e.WikiFightId,
					Fighter1Name = e.Fighter1Name,
					Fighter1NameOriginal = e.Fighter1Name,
					Fighter2Name = e.Fighter2Name,
					Fighter2NameOriginal = e.Fighter2Name,
					FightResultHow = e.FightResultHow,
					Round = e.Round,
					Time = e.Time,
					TotalTime = e.TotalTime,
					ImageForWeb = e.ImageForWeb,
					WEventName = e.EventName,
					DateHeld = e.DateHeld,
					VideoLink = e.VideoLink,
					ImgUrl = e.ImagePath,
					ImageForWebNameMixed = string.Empty,
					ImageForWebOriginal = string.Empty,
					Rank = e.RedditTopFights,
					Fotn = e.Fotn,
					Potn = e.Potn,
					WeightClass = e.WeightClass
				}).ToListAsync();

			FormatResultsToPreventScraping(preventScraping, searchResults);
			
			SearchResultPaged searchResultPaged = new SearchResultPaged
			{
				Count = count,
				FightSearchResultsPaged = searchResults
			};

			return searchResultPaged;
		}
	}
}
