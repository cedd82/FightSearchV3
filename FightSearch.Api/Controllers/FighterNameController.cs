namespace FightSearch.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FightSearch.Service;

    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
	[Route("api/[controller]")]
	public class FighterNameController : Controller
	{
		private readonly IFighterNameService _fighterNameService;

		public FighterNameController(IFighterNameService fighterNameService)
		{
			_fighterNameService = fighterNameService;
		}

		[Route("GetUniqueFighterNames", Name = "GetUniqueFighterNames")]
		[HttpGet]
    #if !DEBUG
		[ResponseCache(Duration = 864000)]
    #endif
		public async Task<ActionResult<IEnumerable<string>>> GeFighterNames()
		{
			IEnumerable<string> result = await _fighterNameService.GetAllUniqueNames();
			return result.ToList();
		}

		[Route("GetAwardFighterNames", Name = "GetAwardFighterNames")]
		[HttpGet]
    #if !DEBUG
		[ResponseCache(Duration = 864000)]
    #endif
		public async Task<ActionResult<IEnumerable<string>>> GetAwardFighterNames()
		{
			IEnumerable<string> result = await _fighterNameService.GetAwardFighterNames();
			return result.ToList();
		}

		[Route("GetTitleFighterNames", Name = "GetTitleFighterNames")]
		[HttpGet]
    #if !DEBUG
		[ResponseCache(Duration = 864000)]
    #endif
		public async Task<ActionResult<IEnumerable<string>>> GetTitleFighterNames()
		{
			IEnumerable<string> result = await _fighterNameService.GetTitleFighterNames();
			return result.ToList();
		}
	}
}