using System;
using System.Collections.Generic;
using MundoDisneyApiRest.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.Models
{
    public class Genre
    {
        public Genre()
        {
            MovieTVs = new HashSet<MovieTV>();
            MoviesTVs = new HashSet<GenreMovieTVs>();
        }
        public int IdGenre { get; set; }

        [StringLength(maximumLength: 100,
        ErrorMessage = "la url de la imagen puede tener un maximo de 100 caracteres")]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [StringLength(maximumLength: 50,
        ErrorMessage = "El nombre puede tener un maximo de 50 caracteres")]
        [Required(ErrorMessage = "Se requiere almenos el nombre")]
        public string Nombre { get; set; }
        public virtual ICollection<MovieTV> MovieTVs { get; set; }
        public virtual ICollection<GenreMovieTVs> MoviesTVs { get; set; }
    }
}
