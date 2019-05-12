namespace FightSearch.Service
{
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
			WikiFight wikiFight = await fightSearchEntities.WikiFight.SingleOrDefaultAsync(e => e.Id == wikiFightId);
			if (wikiFight == null)
			{
				return;
			}

			wikiFight.WatchCount = wikiFight.WatchCount ?? 0;
			wikiFight.WatchCount++;
			await fightSearchEntities.CommitAsync();
		}
	}
}