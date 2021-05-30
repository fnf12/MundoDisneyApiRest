using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class MovieTVDto
    {
        public int IdMovieTV { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
