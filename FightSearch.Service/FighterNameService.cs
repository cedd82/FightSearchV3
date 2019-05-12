namespace FightSearch.Service
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using FightSearch.Repository.Sql;
    using FightSearch.Repository.Sql.Entities;
    using FightSearch.Repository.Sql.EntitiesOld;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Caching.Memory;

	/// Retrieves unique fighter names dependent on the page if fighters exist for the fights being queried.
	/// There may be fight entries with no video associated. So the query firstly checks that fights have a video and are
	/// further filtered depending on the page being visited. This way only relevant names are returned for fights that will appear.
	public class FighterNameService : IFighterNameService
    {
	    private readonly IFightSearchEntities fightSearchEntities;
	    private readonly IMemoryCache memoryCache;

	    public FighterNameService(IFightSearchEntities fightSearchEntities, IMemoryCache memoryCache)
	    {
		    this.memoryCache = memoryCache;
		    this.fightSearchEntities = fightSearchEntities;
	    }

	    public async Task<IEnumerable<string>> GetAllUniqueNames()
	    {
		    IEnumerable<string> cacheEntry = await memoryCache.GetOrCreateAsync("UniqueFighterNames", async entry =>
			    {
				    entry.SlidingExpiration = TimeSpan.FromDays(5);
				    //entry.SlidingExpiration = TimeSpan.FromSeconds(1);
					Expression<Func<WikiFightWeb, bool>> query = wf => wf.FightId != null;
				    IEnumerable<string> fighterNames = await GetNames(query);
				    return fighterNames;
			    });
		    return cacheEntry;
	    }

	    public async Task<IEnumerable<string>> GetTitleFighterNames()
	    {
		    IEnumerable<string> cacheEntry = await memoryCache.GetOrCreateAsync("UniqueTitleFighterNames", async entry =>
			    {
				    entry.SlidingExpiration = TimeSpan.FromDays(5);
				    Expression<Func<WikiFightWeb, bool>> query = wf => wf.FightId != null && wf.TitleFight;
				    IEnumerable<string> fighterNames = await GetNames(query);
				    return fighterNames;
			    });
		    return cacheEntry;
	    }
		
	    public async Task<IEnumerable<string>> GetAwardFighterNames()
	    {
		    IEnumerable<string> cacheEntry = await memoryCache.GetOrCreateAsync("UniqueTitleFighterNames", async entry =>
			    {
				    entry.SlidingExpiration = TimeSpan.FromDays(5);
				    Expression<Func<WikiFightWeb, bool>> query = wf => wf.FightId != null && (wf.Potn || wf.Fotn);
				    IEnumerable<string> fighterNames = await GetNames(query);
				    return fighterNames;
			    });
		    return cacheEntry;
	    }

	    private async Task<IEnumerable<string>> GetNames(Expression<Func<WikiFightWeb, bool>> filterQuery)
	    {
		    IEnumerable<string> fighter1Names = await fightSearchEntities.WikiFightWeb.AsNoTracking().Where(filterQuery).Select(f => f.Fighter1Name).Distinct().ToListAsync();
		    IEnumerable<string> fighter2Names = await fightSearchEntities.WikiFightWeb.AsNoTracking().Where(filterQuery).Select(f => f.Fighter2Name).Distinct().ToListAsync();
		    IEnumerable<string> fighterNames = fighter2Names.Union(fighter1Names).OrderBy(name => name).ToList();
		    return fighterNames;
	    }
    }
}
