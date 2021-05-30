using MundoDisneyApiRest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class CharacterPostDto
    {
        public int IdCharacter { get; set; }
        [StringLength(maximumLength: 100,
        ErrorMessage = "la url de la imagen puede tener un maximo de 100 caracteres")]
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

        [StringLength(maximumLength: 50,
        ErrorMessage = "El nombre puede tener un maximo de 50 caracteres")]
        [Required(ErrorMessage = "Se requiere almenos el nombre")]
        public string Nombre { get; set; }

        [Range(0, 9999, ErrorMessage = "ingresar una edad maximo 9999")]
        public int Edad { get; set; }

        [RegularExpression(@"^\d+.\d{3,2}$", ErrorMessage = "El peso no puede tener mas de 2 decimales")]
        public double Peso { get; set; }

        [StringLength(maximumLength: 255,
        ErrorMessage = "la Histora puede tener un maximo de 255 caracteres")]
        public string Historia { get; set; }
        public virtual ICollection<MovieId> MovieTVs { get; set; }
    }
    public class MovieId
    {
        public int IdMovieTV { get; set; }
    }
}
