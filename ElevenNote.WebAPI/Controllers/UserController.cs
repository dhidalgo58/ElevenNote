using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevenNote.Models.User;
using ElevenNote.Services.Token;
using ElevenNote.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ElevenNote.Models.Token;

namespace ElevenNote.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userservice;
        private readonly ITokenService _tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userservice = userService;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister model)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            var registerResult = await _userservice.RegisterUserAsync(model);
            if (registerResult)
            {
                return Ok("User was registered.");
            }
            return BadRequest("User could not be registered.");
        }

        [Authorize]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
            var userDetail = await _userservice.GetUserByIdAsync(userId);

            if (userDetail is null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        [HttpPost("~/api/Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokenResponse = await _tokenService.GetTokenAsync(request);
            if (tokenResponse is null)
                return BadRequest("Invalid username or password.");

            return Ok(tokenResponse);
        }
    }
}
