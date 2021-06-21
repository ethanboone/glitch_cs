using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using glitchserver.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace glitchserver.Controllers
{
    [ApiController]
    [Route("[route]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsService _accountService;

        public AccountsController(AccountsService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}