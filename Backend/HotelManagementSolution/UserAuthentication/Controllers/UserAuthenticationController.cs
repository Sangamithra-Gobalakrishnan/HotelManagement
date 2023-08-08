using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.DTO;

namespace UserAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("HotelCORS")]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserService<UserDTO, TravellerDTO, AgentDTO, StatusDTO> _userService;

        public UserAuthenticationController(IUserService<UserDTO, TravellerDTO, AgentDTO, StatusDTO> userService)
        {
            _userService = userService;
        }

        [HttpPost("TravellerRegistration")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>?> TravellerRegister(TravellerDTO travellerDTO)
        {
            var register = await _userService.TravellerRegister(travellerDTO);
            if (register != null)
                return Ok(register);
            return BadRequest(new Error(2, "Unable to register at this moment"));
        }

        [HttpPost("TravelAgentRegistration")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> TravelAgentRegister(AgentDTO agentDTO)
        {
            var register = await _userService.AgentRegister(agentDTO);
            if (register != null)
                return Ok(register);
            return BadRequest(new Error(2, "Unable to register at this moment"));
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO userDTO)
        {
            var login = await _userService.Login(userDTO);
            if (login != null)
                return Ok(login);
            return BadRequest(new Error(2, "Check your credentials and try again"));
        }

        [HttpPut("ApproveAgent")]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusDTO>> ApproveAgent(StatusDTO statusDTO)
        {
            var login = await _userService.ApproveAgent(statusDTO);
            if (login != null)
                return Ok(login);
            return BadRequest(new Error(2, "Cannot approve right now"));
        }
    }
}
