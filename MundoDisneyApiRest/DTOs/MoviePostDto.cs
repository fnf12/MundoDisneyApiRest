using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class MoviePostDto
    {
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
        public virtual ICollection<GenreID> Genres { get; set; }
    }
    public class GenreID
    {
        public int IdGenre { get; set; }
    }
}
