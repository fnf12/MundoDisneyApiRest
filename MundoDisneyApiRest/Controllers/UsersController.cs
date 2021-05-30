using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDisneyApiRest.Models;
using MundoDisneyApiRest.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MundoDisneyApiRest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DisneyContext _context;

        public UsersController(DisneyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        // GET: api/Users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var user = await _context.Users.ToListAsync();
            var UserDto = user.Select(x => new UserDto
            {
                IdUser = x.IdUser,
                UserName = x.UserName
            });
            return Ok(UserDto);
        }

        /// <summary>
        /// Deletes a specific User (need token authorization)
        /// </summary>
        /// <param name="id"></param> 
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }
    }
}
