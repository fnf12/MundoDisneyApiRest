using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MundoDisneyApiRest.DTOs;
using MundoDisneyApiRest.Models;

namespace MundoDisneyApiRest.Controllers
{
    [Route("api/auth/login")]
    [ApiController]
    public class AuthLoginController : ControllerBase
    {
        private readonly DisneyContext _context;
        private readonly IConfiguration _config;

        public AuthLoginController(DisneyContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// Login a User and gets a token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///      {
        ///         "mail": "string",
        ///         "pass": "string"
        ///       }
        ///
        /// </remarks>
        /// <returns>A newly created Character</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserLoginDto login)
        {
            User user = await _context.Users.Where(x => x.Mail == login.Mail).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            if (user.Pass == login.Pass)
            {
                var secretkey = _config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretkey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Mail));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);
                return Ok(bearer_token);
            }
            else
            {
                return Forbid();
            }
        }

    }
}
