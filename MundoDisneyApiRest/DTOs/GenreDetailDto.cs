using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class GenreDetailDto
    {
        public int IdGenre { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public virtual IEnumerable<MovieTVDto> MovieTVs { get; set; }
    }
}
