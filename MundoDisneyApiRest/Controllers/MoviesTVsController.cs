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
    [Route("api/movies")]
    [ApiController]
    public class MovieTVsController : ControllerBase
    {
        private readonly DisneyContext _context;

        public MovieTVsController(DisneyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all MovieTV and filter by ("?title=","?genre=") and order by date ("?order= asc|desc")
        /// </summary>
        // GET: api/MovieTVs
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<MovieTVDto>))]
        public async Task<ActionResult<List<MovieTVDto>>> GetMovieTVs(string title, string genre, string order)
        {
            var movies = await _context.MovieTVs
                .Include(x => x.Genres)
                .ToListAsync();

            if (title != null)
            {
                movies = movies.Where(x => x.Titulo.Contains(title)).ToList();
            }
            if (genre != null)
            {
                movies = movies.Where(x => x.Genres.SingleOrDefault(y => y.Nombre == genre) != null).ToList();
            }

            var moviesdto = movies.Select(x => new MovieTVDto
            {
                IdMovieTV = x.IdMovieTV,
                Imagen = x.Imagen,
                Titulo = x.Titulo,
                FechaCreacion = x.FechaCreacion
            });

            if (order == "asc" || order == "desc")
            {
                if (order == "asc")
                    moviesdto = moviesdto.OrderBy(x => x.FechaCreacion);
                else
                    moviesdto = moviesdto.OrderByDescending(x => x.FechaCreacion);
            }

            return moviesdto.ToList();
        }

        /// <summary>
        /// Get a specific MovieTV detail
        /// </summary> 
        // GET: api/MovieTVs/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MoviesTVdetailDto))]
        public async Task<ActionResult<MoviesTVdetailDto>> GetMovieTV(int id)
        {
            //var movieTV = await _context.MovieTVs.FindAsync(id);

            var movieTV = await _context.MovieTVs
                .Include(x => x.Characters)
                .Include(x => x.Genres)
                .SingleAsync(b => b.IdMovieTV == id);

            if (movieTV == null)
            {
                return NotFound();
            }

            var genredto = movieTV.Genres.Select(x => new GenreDto 
            {
                IdGenre = x.IdGenre,
                Nombre = x.Nombre
            });

            var characterdto = movieTV.Characters.Select(x => new CharacterDto
            {
                IdCharacter = x.IdCharacter,
                Imagen = x.Imagen,
                Nombre = x.Nombre
            });

            var movietvdto = new MoviesTVdetailDto
            {
                IdMovieTV = movieTV.IdMovieTV,
                Imagen = movieTV.Imagen,
                Titulo = movieTV.Titulo,
                Calificacion = movieTV.Calificacion,
                FechaCreacion = movieTV.FechaCreacion,
                Genres = genredto,
                Characters = characterdto
            };

            return movietvdto;
        }

        /// <summary>
        /// Modifies a specific MovieTv (need token authorization)
        /// </summary>
        // PUT: api/MovieTVs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieTV(int id, MoviePostDto movieTVdto)
        {
            var movieTV = await _context.MovieTVs
                .Include(x => x.Characters)
                .Include(x => x.Genres)
                .SingleAsync(b => b.IdMovieTV == id);

            if (id != movieTV.IdMovieTV)
            {
                return BadRequest();
            }

            if (await _context.MovieTVs.Where(x => x.Titulo == movieTVdto.Titulo).AnyAsync())
            {
                return BadRequest("El titulo ya existe");
            }

            var relacion = movieTVdto.Genres.Select(x => new GenreMovieTVs
            {
                IdMovieTV = movieTVdto.IdMovieTV,
                IdGenre = x.IdGenre
            });

            movieTV.Imagen = movieTVdto.Imagen;
            movieTV.Titulo = movieTVdto.Titulo;
            movieTV.FechaCreacion = movieTVdto.FechaCreacion;
            movieTV.Calificacion = movieTVdto.Calificacion;
            movieTV.Genress = relacion.ToList();

            _context.Entry(movieTV).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieTVExists(id))
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
        /// Creates a MovieTV (need token authorization)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///      {
        ///         "imagen": "link:image.jpg",
        ///         "titulo": "Shrek",
        ///         "fechaCreacion": "2021-05-26",
        ///         "calificacion": 3,
        ///         "genres": [
        ///               {
        ///                 "idGenre": 1
        ///               },
        ///               {
        ///                 "idGenre": 10
        ///               },
        ///               ]
        ///       }
        ///
        /// </remarks>
        /// <returns>A newly created Character</returns>
        // POST: api/MovieTVs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoviePostDto>> PostMovieTV(MoviePostDto movieTVdto)
        {
            if (await _context.MovieTVs.Where(x => x.Titulo == movieTVdto.Titulo).AnyAsync())
            {
                return BadRequest("El titulo ya existe");
            }

            var relacion = movieTVdto.Genres.Select(x => new GenreMovieTVs
            {
                IdMovieTV = movieTVdto.IdMovieTV,
                IdGenre = x.IdGenre
            });

            var movieTV = new MovieTV
            {
                Imagen = movieTVdto.Imagen,
                Titulo = movieTVdto.Titulo,
                FechaCreacion = movieTVdto.FechaCreacion,
                Calificacion = movieTVdto.Calificacion,
                Genress = relacion.ToList()
            };

            _context.MovieTVs.Add(movieTV);
            await _context.SaveChangesAsync();

            movieTVdto.IdMovieTV = movieTV.IdMovieTV;

            return CreatedAtAction("GetMovieTV", new { id = movieTV.IdMovieTV }, movieTVdto);
        }

        /// <summary>
        /// Deletes a specific MovieTV (need token authorization)
        /// </summary>
        /// <param name="id"></param> 
        // DELETE: api/MovieTVs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieTV(int id)
        {
            var movieTV = await _context.MovieTVs.FindAsync(id);
            if (movieTV == null)
            {
                return NotFound();
            }

            _context.MovieTVs.Remove(movieTV);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieTVExists(int id)
        {
            return _context.MovieTVs.Any(e => e.IdMovieTV == id);
        }
    }
}
