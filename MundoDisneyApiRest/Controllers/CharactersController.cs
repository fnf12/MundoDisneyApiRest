using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDisneyApiRest.DTOs;
using MundoDisneyApiRest.Models;

namespace MundoDisneyApiRest.Controllers
{
    [Authorize]
    [Route("api/characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly DisneyContext _context;

        public CharactersController(DisneyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets All characters and filter by ("?name=","?age=","?idMovie=")
        /// </summary>
        // GET: api/Characters
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<CharacterDto>))]
        public async Task<ActionResult<List<CharacterDto>>> GetCharacters(string name, int? age, int? idMovie)
        {
            var characters = await _context.Characters
                .Include(x => x.MoviesTVs)
                .ToListAsync();
            if(name != null)
            {
              characters = characters.Where(x => x.Nombre.Contains(name)).ToList();
            }
            if (age != null)
            {
                characters = characters.Where(x => x.Edad == age).ToList();
            }
            if (idMovie != null)
            {
               characters = characters.Where(x => x.MoviesTVs.SingleOrDefault(y => y.IdMovieTV == idMovie) != null).ToList();
            }

             var chartersdto = characters.Select(x => new CharacterDto
            {
                IdCharacter = x.IdCharacter,
                Nombre = x.Nombre,
                Imagen = x.Imagen
            });
            return chartersdto.ToList();
        }
        /// <summary>
        /// Get a specific Character detail
        /// </summary> 
        // GET: api/Characters/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CharacterDetailDto))]
        public async Task<ActionResult<CharacterDetailDto>> GetCharacter(int id)
        {
            //var character = await _context.Characters.FindAsync(id);

            var character = await _context.Characters
                .Include(x => x.MovieTVs)
                .SingleAsync(b => b.IdCharacter == id);

            if (character == null)
            {
                return NotFound();
            }
            var moviesdto = character.MovieTVs.Select(x => new MovieTVDto
            {
                IdMovieTV = x.IdMovieTV,
                Imagen = x.Imagen,
                Titulo = x.Titulo,
                FechaCreacion = x.FechaCreacion
            });

            var characterdto = new CharacterDetailDto
            {
                IdCharacter = character.IdCharacter,
                Nombre = character.Nombre,
                Imagen = character.Imagen,
                Edad = character.Edad,
                Peso = character.Peso,
                Historia = character.Historia,
                MovieTVs = moviesdto
            };

            return characterdto;
        }
        /// <summary>
        /// Modifies a specific Character (need token authorization)
        /// </summary>
        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterPostDto characterdto)
        {
            var character = await _context.Characters
                .Include(x => x.MovieTVs)
                .SingleAsync(b => b.IdCharacter == id);

            if (id != character.IdCharacter)
            {
                return BadRequest();
            }

            if(await _context.Characters.Where(x => x.Nombre == characterdto.Nombre).AnyAsync())
            {
                return BadRequest("El nombre ya existe");
            }

            var MovChar = characterdto.MovieTVs.Select(x => new MovieTVsCharacter
            {
                IdCharacter = characterdto.IdCharacter,
                IdMovieTV = x.IdMovieTV
            });

            character.Nombre = characterdto.Nombre;
            character.Imagen = characterdto.Imagen;
            character.Edad = characterdto.Edad;
            character.Peso = characterdto.Peso;
            character.Historia = characterdto.Historia;
            character.MoviesTVs = MovChar.ToList();

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a Character (need token authorization)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///      {
        ///         "imagen": "link:image.jpg",
        ///         "nombre": "fiona",
        ///         "edad": 25,
        ///         "peso": 90,
        ///         "historia": "princesa que se tranforma en orco",
        ///         "movieTVs": [
        ///               {
        ///                 "idMovieTV": 1
        ///               },
        ///               {
        ///                 "idMovieTV": 4
        ///               },
        ///               ]
        ///       }
        ///
        /// </remarks>
        /// <returns>A newly created Character</returns>
        // POST: api/Characters
        [HttpPost]
        public async Task<ActionResult<CharacterPostDto>> PostCharacter(CharacterPostDto characterdto)
        {
            if (await _context.Characters.Where(x => x.Nombre == characterdto.Nombre).AnyAsync())
            {
                return BadRequest("El nombre ya existe");
            }

            var relacion = characterdto.MovieTVs.Select(x => new MovieTVsCharacter
            { 
                IdCharacter = characterdto.IdCharacter,
                IdMovieTV = x.IdMovieTV
            });

            var character = new Character
            {
                Nombre = characterdto.Nombre,
                Imagen = characterdto.Imagen,
                Edad = characterdto.Edad,
                Peso = characterdto.Peso,
                Historia = characterdto.Historia,
                MoviesTVs = relacion.ToList()
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            characterdto.IdCharacter = character.IdCharacter;

            return CreatedAtAction("GetCharacter", new { id = characterdto.IdCharacter }, characterdto);
        }

        /// <summary>
        /// Deletes a specific Character (need token authorization)
        /// </summary>
        /// <param name="id"></param> 
        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.IdCharacter == id);
        }
    }
}
