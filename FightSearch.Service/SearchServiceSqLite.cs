using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FightSearch.Common.Constants;
using FightSearch.Common.Enums;
using FightSearch.Common.Settings;
//using FightSearch.Repository.Sql.Entities;
using FightSearch.Repository.SqlLight;
using FightSearch.Service.DomainModels;
using FightSearch.Service.Interfaces;
using FightSearch.Service.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FightSearch.Service
{
    public class SearchServiceSqLite : ISearchService
    {
        private readonly IOptions<ImagePaths> _imagePathSettings;
        private readonly UfcContextLite _ufcContextLite;
        private readonly CultureInfo _provider;
        public SearchServiceSqLite(IOptions<ImagePaths> imagePathSettings, UfcContextLite ufcContextLite)
        {
            _imagePathSettings = imagePathSettings;
            _ufcContextLite = ufcContextLite;
            _provider = CultureInfo.InvariantCulture;
        }

        public async Task<SearchResultPaged> FindAwardFightsAsync(SearchQuery searchQuery)
        {
            //Expression<Func<WikiFightWebSqlLite, bool>> where = e => e.Fotn == 0 || e.Potn == 0;
            SearchResultPaged searchResultPaged = new SearchResultPaged();
            if (searchQuery.SearchParams.AwardType == AwardTypes.POTN)
            {
                Expression<Func<WikiFightWebSqlLite, bool>> where = e => e.Potn == 1;
                searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, where);
                return searchResultPaged;
            }
            else if (searchQuery.SearchParams.AwardType == AwardTypes.FOTN)
            {
                Expression<Func<WikiFightWebSqlLite, bool>> where = e => e.Fotn == 1;
                searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, where);
                return searchResultPaged;
            }
            Expression<Func<WikiFightWebSqlLite, bool>> whereNoFilter = e => e.Fotn == 1 || e.Potn == 1;
            searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, whereNoFilter);
            return searchResultPaged;
        }

        public async Task<SearchResultPaged> FindFightsAsync(SearchQuery searchQuery)
        {
            SearchResultPaged searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, true);
            return searchResultPaged;
        }

		public Task<SearchResultPaged> FindFightsScrapeAsync(SearchQuery searchQuery, bool swapFighterNamesForScrape)
        {
            return null;
        }

        public async Task<SearchResultPaged> FindTitleFightsAsync(SearchQuery searchQuery)
        {
            Expression<Func<WikiFightWebSqlLite, bool>> where = e => e.TitleFight == 1;
            SearchResultPaged searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, where);
            return searchResultPaged;
        }

        public async Task<SearchResultPaged> FindTopRedditFightsAsync(SearchQuery searchQuery)
        {
            Expression<Func<WikiFightWebSqlLite, bool>> where = e => e.RedditTopFights != 0;
            Expression<Func<WikiFightWebSqlLite, int>> orderBy = e => e.RedditTopFights;
            SearchResultPaged searchResultPaged = await FindFightsQueryBuilder(searchQuery.SearchParams, searchQuery.PagingInfo, false, where, orderBy);
            return searchResultPaged;
        }

        private static IQueryable<WikiFightWebSqlLite> FilterDuration(SearchParams searchQueryParams, IQueryable<WikiFightWebSqlLite> query)
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

        private static IQueryable<WikiFightWebSqlLite> FilterFighterName(SearchParams searchParams, IQueryable<WikiFightWebSqlLite> query)
        {
            if (string.IsNullOrEmpty(searchParams.Fighter1Name))
            {
                return query;
            }
            string fighterName = searchParams.Fighter1Name;
            Expression<Func<WikiFightWebSqlLite, bool>> where = e => e.Fighter1Name.Equals(fighterName) || e.Fighter2Name.Equals(fighterName);
            query = query.Where(where);
            //query = query.Where(e => e.Fighter1Name.Equals(fighterName) || e.Fighter2Name.Equals(fighterName));
            return query;
        }

        private static IQueryable<WikiFightWebSqlLite> FilterFinishType(SearchParams searchQueryParams, IQueryable<WikiFightWebSqlLite> query)
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

        private static IQueryable<WikiFightWebSqlLite> FilterFreeFights(SearchParams searchQueryParams, IQueryable<WikiFightWebSqlLite> query)
        {
            if (searchQueryParams.FreeFightsOnly)
            {
                query = query.Where(e => e.Promotion == Promotion.Bellator);
            }
            return query;
        }

        private static IQueryable<WikiFightWebSqlLite> FilterWeightDivision(SearchParams searchQueryParams, IQueryable<WikiFightWebSqlLite> query)
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

        private static IQueryable<WikiFightWebSqlLite> FilterYear(SearchParams searchQueryParams, IQueryable<WikiFightWebSqlLite> query)
        {
            if (searchQueryParams.Year.Equals(General.Any, StringComparison.OrdinalIgnoreCase))
            {
                return query;
            }

            //query = query.Where(e => e.DateHeld.Equals(searchQueryParams.Year));
            query = query.Where(e => e.YearHeld.Equals(searchQueryParams.Year));
            return query;
        }

        private async Task<SearchResultPaged> FindFightsQueryBuilder(SearchParams searchQueryParams, PagingInfo pagingInfo, bool preventScraping, Expression<Func<WikiFightWebSqlLite, bool>> additionalQuery = null, Expression<Func<WikiFightWebSqlLite, int>> orderBy = null)
        {
            

            List<WikiFightWebSqlLite> test = _ufcContextLite.WikiFightWebSqlLite.Take(10).ToList();
            var xxx = test;
            IQueryable<WikiFightWebSqlLite> query = _ufcContextLite.WikiFightWebSqlLite.AsNoTracking();
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
                    searchResult.DateHeld = DateTime.Now.Date;
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

        private async Task<SearchResultPaged> GetSearchResults(IQueryable<WikiFightWebSqlLite> query, PagingInfo pagingInfo, bool preventScraping)
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
                Round = e.Round.ToString(),
                Time = e.Time,
                TotalTime = TimeSpan.Parse(e.TotalTime),
                ImageForWeb = e.ImageForWeb,
                WEventName = e.EventName,
                //DateHeld = DateTime.ParseExact(e.DateHeld,_provider,
                DateHeld = DateTime.ParseExact(e.DateHeld, "yyyy-MM-dd", _provider, DateTimeStyles.None),
                VideoLink = e.VideoLink,
                ImgUrl = e.ImagePath,
                ImageForWebNameMixed = string.Empty,
                ImageForWebOriginal = string.Empty,
                Rank = e.RedditTopFights,
                Fotn = e.Fotn == 1,
                Potn = e.Potn == 1,
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
