using BankSystem.Application.Dtos;
using BankSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Registration client
        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUpAsync(SignUpDto signUpDto)
        {
            return Ok(await _accountService.SignUpAsync(signUpDto));
        }
        //Login client
        [HttpPost("signin")]
        public async Task<ActionResult<string>> SignInAsync(SignInDto signInDto)
        {
            return Ok(await _accountService.SignInAsync(signInDto));
        }
    }
}
