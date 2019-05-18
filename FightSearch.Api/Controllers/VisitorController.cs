﻿namespace FightSearch.Api.Controllers
{
    using System.Threading.Tasks;

    using FightSearch.Service;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [EnableCors("Cors")]
    [Produces("application/json")]
	[Route("api/[controller]")]
	public class VisitorController : Controller
	{
		private readonly IVisitorTrackingService _visitorTrackingService;

		public VisitorController(IVisitorTrackingService visitorTrackingService)
		{
			_visitorTrackingService = visitorTrackingService;
		}

		[Route("LinkClicked", Name = "LinkClicked")]
		[HttpPost]
		public async Task<ActionResult<bool>> LinkClicked([FromBody] int id)
		{
			await _visitorTrackingService.LinkClicked(id);
			return true;
		}

	}
}