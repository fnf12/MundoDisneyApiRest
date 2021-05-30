using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDisneyApiRest.DTOs;
using MundoDisneyApiRest.Models;

namespace MundoDisneyApiRest.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly DisneyContext _context;

        public GenresController(DisneyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets All Genres
        /// </summary>
        // GET: api/Genres
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<GenreDto>))]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var genres =  await _context.Genres.ToListAsync();
            var genresdto = genres.Select(x => new GenreDto {
                IdGenre = x.IdGenre,
                Nombre = x.Nombre
            });
            return Ok(genresdto);
        }

        /// <summary>
        /// Get a specific Genre
        /// </summary>
        /// <param name="id"></param> 
        // GET: api/Genres/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenreDetailDto))]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            //var genre = await _context.Genres.FindAsync(id);
            var genre = await _context.Genres
                .Include(x => x.MovieTVs)
                .SingleAsync(b => b.IdGenre == id);

            if (genre == null)
            {
                return NotFound();
            }

            var moviesdto = genre.MovieTVs.Select(x => new MovieTVDto
            {
                IdMovieTV = x.IdMovieTV,
                Imagen = x.Imagen,
                Titulo = x.Titulo,
                FechaCreacion = x.FechaCreacion
            });

            var genredto = new GenreDetailDto
            {
                IdGenre = genre.IdGenre,
                Imagen = genre.Imagen, 
                Nombre = genre.Nombre,
                MovieTVs = moviesdto
            };

            return Ok(genredto);
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.IdGenre == id);
        }
    }
}
