using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightSearch.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FightSearch.Api.Controllers
{
    [EnableCors("Cors")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FighterNameController : ControllerBase
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
            var result = await _fighterNameService.GetAllUniqueNames();
            return result.ToList();
        }

        [Route("GetAwardFighterNames", Name = "GetAwardFighterNames")]
        [HttpGet]
#if !DEBUG
        [ResponseCache(Duration = 864000)]
#endif
        public async Task<ActionResult<IEnumerable<string>>> GetAwardFighterNames()
        {
            var result = await _fighterNameService.GetAwardFighterNames();
            return result.ToList();
        }

        [Route("GetTitleFighterNames", Name = "GetTitleFighterNames")]
        [HttpGet]
#if !DEBUG
        [ResponseCache(Duration = 864000)]
#endif
        public async Task<ActionResult<IEnumerable<string>>> GetTitleFighterNames()
        {
            var result = await _fighterNameService.GetTitleFighterNames();
            return result.ToList();
        }
    }
}