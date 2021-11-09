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
            if (string.IsNullOrWhiteSpace(credentials.Username) || string.IsNullOrWhiteSpace(credentials.Password) || string.IsNullOrWhiteSpace(credentials.LdapSection))
                return BadRequest(new { message = "Dados inválidos." });

            var user = authService.Login(credentials.Username,credentials.Password, credentials.LdapSection);

            if (user.Item2 == null && user.Item1 != null)
            {
                return Ok();
            }
            else 
            {
                return Unauthorized(new { message = user.Item2});
            }
        }
    }
}
