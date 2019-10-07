using FightSearch.Service.Interfaces;

namespace FightSearch.Service
{
    using System;
    using System.Threading.Tasks;

	using FightSearch.Repository.Sql;
    using FightSearch.Repository.Sql.Entities;

	using Microsoft.EntityFrameworkCore;

	
	/// <summary>
	/// custom logic to track visitor behavior
	/// </summary>
	public class VisitorTrackingService : IVisitorTrackingService
	{
		private readonly IFightSearchEntities fightSearchEntities;

		public VisitorTrackingService(IFightSearchEntities fightSearchEntities)
		{
			this.fightSearchEntities = fightSearchEntities;
		}

		// keeps a count of how many times a video was clicked
		public async Task LinkClicked(int wikiFightId)
		{
            WatchCount watchCount = await fightSearchEntities.WatchCount.SingleOrDefaultAsync(e => e.WikiFightId == wikiFightId);
			if (watchCount == null)
            {
                watchCount = new WatchCount
                {
                    Count = 1,
                    UpdateDateTime = DateTime.UtcNow,
                    WikiFightId = wikiFightId
                };
                fightSearchEntities.WatchCount.Add(watchCount);
            }
            else
            {
                
                watchCount.Count++;
                watchCount.UpdateDateTime = DateTime.UtcNow;
                
            }
            await fightSearchEntities.SaveChangesAsync();
		}
	}
}