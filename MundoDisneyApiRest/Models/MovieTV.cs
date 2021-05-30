using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.Models
{
    public class MovieTV
    {
        public MovieTV()
        {
            Genres = new HashSet<Genre>();
            Characters = new HashSet<Character>();
            Characterss = new HashSet<MovieTVsCharacter>();
        }
        public int IdMovieTV { get; set; }

        [StringLength(maximumLength: 100,
        ErrorMessage = "la url de la imagen puede tener un maximo de 100 caracteres")]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [StringLength(maximumLength: 50,
        ErrorMessage = "El titulo puede tener un maximo de 50 caracteres")]
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }

        [Range(0, 5, ErrorMessage = "la calificacion tiene un maximo de 5")]
        public int Calificacion { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<GenreMovieTVs> Genress { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<MovieTVsCharacter> Characterss { get; set; }
    }
}
