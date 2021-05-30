using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class MoviesTVdetailDto
    {
        public int IdMovieTV { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Calificacion { get; set; }
        public virtual IEnumerable<GenreDto> Genres { get; set; }
        public virtual IEnumerable<CharacterDto> Characters { get; set; }
    }
}
