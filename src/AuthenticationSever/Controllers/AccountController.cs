using AuthenticationSever.DTOs;
using AuthenticationSever.Entities;
using AuthenticationSever.Repositories.Abstract;
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

        [HttpPost("Register")]
        public async Task<ActionResult<Status>> Register(RegisterDTO dto)
        {
            dto.Role = "business";
            var status = await _service.RegistrationAsync(dto);


            return status;
        }
        [HttpGet("test")]
        public async Task<ActionResult> Get()
        {
            string test = "test";
            return Ok(test);
        }
    }
}
