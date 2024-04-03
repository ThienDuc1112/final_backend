using AuthenticationSever.DTOs;
using AuthenticationSever.Entities;
using AuthenticationSever.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationSever.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAuthentication _service;
        private readonly UserManager<ManageUser> _userManager;
        public AccountController(IUserAuthentication userAuthentication, UserManager<ManageUser> userManager)
        {
            _service = userAuthentication;
            _userManager = userManager;
        }

        [HttpPost("candidate/register")]
        public async Task<ActionResult<Status>> Register([FromBody] RegisterDTO dto)
        {
            dto.Role = "candidate";
            var status = await _service.RegistrationAsync(dto);


            return status;
        }

        [HttpPost("employer/register")]
        public async Task<ActionResult<Status>> RegisterForEmployer([FromBody] RegisterDTO dto)
        {
            dto.Role = "employer";
            var status = await _service.RegistrationAsync(dto);

            return status;
        }
        [HttpGet("test")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            string test = "test";
            return Ok(test);
        }
    }
}
