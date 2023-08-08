using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("HotelCORS")]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService<Traveller, TravelAgent, StatusDTO> _filterService;

        public FilterController(IFilterService<Traveller,TravelAgent,StatusDTO> filterService)
        {
            _filterService = filterService;
        }

        [HttpGet("GetApprovedAgents")]
        [ProducesResponseType(typeof(ICollection<TravelAgent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<TravelAgent>>> GetApprovedAgents()
        {
            var agents = await _filterService.GetApprovedAgents();
            if (agents != null)
                return Ok(agents);
            return NotFound(new Error(1, "No availability of approved agents right now"));
        }

        [HttpGet("GetAllTravellers")]
        [ProducesResponseType(typeof(ICollection<Traveller>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<Traveller>>> GetAllTravellers()
        {
            var travellers = await _filterService.GetAllTravellers();
            if (travellers != null)
                return Ok(travellers);
            return NotFound(new Error(1, "No availability of travellers right now"));
        }

        [HttpPost("GetTravellerById")]
        [ProducesResponseType(typeof(ICollection<Traveller>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<Traveller>>> GetTravellerById(StatusDTO statusDTO)
        {
            var traveller = await _filterService.GetTravellerById(statusDTO);
            if (traveller != null)
                return Ok(traveller);
            return NotFound(new Error(1, "No traveller with this ID"));
        }

        [HttpPost("GetAgentById")]
        [ProducesResponseType(typeof(ICollection<TravelAgent>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<TravelAgent>>> GetAgentById(StatusDTO statusDTO)
        {
            var agent = await _filterService.GetAgentById(statusDTO);
            if (agent != null)
                return Ok(agent);
            return NotFound(new Error(1, "No agent with this ID"));
        }

        [HttpGet("GetNotApprovedAgents")]
        [ProducesResponseType(typeof(ICollection<TravelAgent?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<TravelAgent?>>> GetNotApprovedDoctors()
        {
            var agents = await _filterService.GetNotApprovedAgents();
            if (agents != null)
            {
                return Ok(agents);
            }
            return NotFound(new Error(1, "No availability of not-approved agents right now"));
        }
    }
}
