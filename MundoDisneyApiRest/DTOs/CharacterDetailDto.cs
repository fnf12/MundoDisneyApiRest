using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class CharacterDetailDto
    {
        public int IdCharacter { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public string Historia { get; set; }
        public virtual IEnumerable<MovieTVDto> MovieTVs { get; set; }
    }
}
