using System.Threading.Tasks;
using FightSearch.Service;
using FightSearch.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FightSearch.Api.Controllers
{
    [EnableCors("Cors")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorTrackingService _visitorTrackingService;

        public VisitorController(IVisitorTrackingService visitorTrackingService)
        {
            _visitorTrackingService = visitorTrackingService;
        }

        [Route("LinkClicked", Name = "LinkClicked")]
        [HttpPost]
        //public async Task<ActionResult<bool>> LinkClicked([FromBody] int id)
        public IActionResult LinkClicked([FromBody] int id)
        {
            //await _visitorTrackingService.LinkClicked(id);
            return Ok(true);
        }
    }
}