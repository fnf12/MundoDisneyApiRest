using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MundoDisneyApiRest.DTOs;
using MundoDisneyApiRest.Models;
using MundoDisneyApiRest.services;

namespace MundoDisneyApiRest.Controllers
{
    [Route("api/auth/register")]
    [ApiController]
    public class AuthRegisterController : ControllerBase
    {
        private readonly DisneyContext _context;

        public AuthRegisterController(DisneyContext context)
        {
            _context = context;
        }

        //POST: api/Users
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        /// <summary>
        /// Register a User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///      {
        ///         "userName": "string",
        ///         "mail": "string",
        ///         "pass": "string",
        ///         "confirmPass": "string"
        ///       }
        ///
        /// </remarks>
        /// <returns>A newly created Character</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRegisterDto userdto)
        {

            if (_context.Users.Any(x => x.UserName == userdto.UserName))
            {
                return BadRequest("El user ya existe");
            }

            var user = new User
            {
                UserName = userdto.UserName,
                Mail = userdto.Mail,
                Pass = userdto.Pass,
                ConfirmPass = userdto.ConfirmPass
            };

            _context.Users.Add(user);

            Logic logic = new();

            var subject = "Bienvenido api disney";
            var to = user.Mail;
            var message = "Bienvenido " + user.UserName + " a la api del mundo de disney";

            string msg = logic.SendEmail(to, subject, message);

            await _context.SaveChangesAsync();

            Console.WriteLine(msg);

            return Ok(new UserDto { 
                IdUser = user.IdUser,
                UserName = user.UserName
            });
        }
    }
}
