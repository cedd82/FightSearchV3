using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FightSearch.Repository.SqlLight;
using FightSearch.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FightSearch.Service
{
    /// Retrieves unique fighter names dependent on the page if fighters exist for the fights being queried.
    /// There may be fight entries with no video associated. So the query firstly checks that fights have a video and are
    /// further filtered depending on the page being visited. This way only relevant names are returned for fights that will appear.
    public class FighterNameServiceSqLite : IFighterNameService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CultureInfo _provider;
        private readonly UfcContextLite _ufcContextLite;

        public FighterNameServiceSqLite(IMemoryCache memoryCache, UfcContextLite ufcContextLite)
        {
            _memoryCache = memoryCache;
            _ufcContextLite = ufcContextLite;
            _provider = CultureInfo.InvariantCulture;
        }

        public async Task<IEnumerable<string>> GetAllUniqueNames()
        {
            var cacheEntry = await _memoryCache.GetOrCreateAsync("UniqueFighterNames", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(5);
                //entry.SlidingExpiration = TimeSpan.FromSeconds(1);
                Expression<Func<WikiFightWebSqlLite, bool>> query = wf => true;
                var fighterNames = await GetNames(query);
                return fighterNames;
            });
            return cacheEntry;
        }

        public async Task<IEnumerable<string>> GetTitleFighterNames()
        {
            var cacheEntry = await _memoryCache.GetOrCreateAsync("UniqueTitleFighterNames", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(5);
                Expression<Func<WikiFightWebSqlLite, bool>> query = wf => wf.TitleFight == 1;
                var fighterNames = await GetNames(query);
                return fighterNames;
            });
            return cacheEntry;
        }

        public async Task<IEnumerable<string>> GetAwardFighterNames()
        {
            var cacheEntry = await _memoryCache.GetOrCreateAsync("UniqueTitleFighterNames", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(5);
                Expression<Func<WikiFightWebSqlLite, bool>> query = wf => wf.Potn == 1 || wf.Fotn == 1;
                var fighterNames = await GetNames(query);
                return fighterNames;
            });
            return cacheEntry;
        }

        private async Task<IEnumerable<string>> GetNames(Expression<Func<WikiFightWebSqlLite, bool>> filterQuery)
        {
            IEnumerable<string> fighter1Names = await _ufcContextLite.WikiFightWebSqlLite.AsNoTracking()
                .Where(filterQuery).Select(f => f.Fighter1Name).Distinct().ToListAsync();
            IEnumerable<string> fighter2Names = await _ufcContextLite.WikiFightWebSqlLite.AsNoTracking()
                .Where(filterQuery).Select(f => f.Fighter2Name).Distinct().ToListAsync();
            IEnumerable<string> fighterNames = fighter2Names.Union(fighter1Names).OrderBy(name => name);
            return fighterNames;
        }
    }
}