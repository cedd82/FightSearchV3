using System.Globalization;
using FightSearch.Repository.SqlLight;
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
	public class VisitorTrackingServiceSqLite : IVisitorTrackingService
	{
        private readonly CultureInfo _provider;
        private readonly UfcContextLite _ufcContextLite;

		public VisitorTrackingServiceSqLite(UfcContextLite ufcContextLite)
        {
            _ufcContextLite = ufcContextLite;
            _provider = CultureInfo.InvariantCulture;
        }

		// keeps a count of how many times a video was clicked
		public async Task LinkClicked(int wikiFightId)
		{
            WatchCountSqLite watchCount = await _ufcContextLite.WatchCountSqLite.SingleOrDefaultAsync(e => e.WikiFightId == wikiFightId);
			if (watchCount == null)
            {
                watchCount = new WatchCountSqLite
                {
                    Count = 1,
                    UpdateDateTime = DateTime.UtcNow.Date.ToString("YYY-MM-dd"),
                    WikiFightId = wikiFightId
                };
                _ufcContextLite.WatchCountSqLite.Add(watchCount);
            }
            else
            {
                watchCount.Count++;
                watchCount.UpdateDateTime = DateTime.UtcNow.Date.ToString("YYY-MM-dd");

            }
            await _ufcContextLite.SaveChangesAsync();
		}
	}
}