using API_Active_Directory.Interfaces;
using API_Active_Directory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Active_Directory.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IAuthenticationService authService;

        public SecurityController(IAuthenticationService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            if (credentials.Username.Equals("") || credentials.Password.Equals(""))
                return Unauthorized();

            var user = authService.Login(credentials.Username,credentials.Password);

            if (user != null)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
