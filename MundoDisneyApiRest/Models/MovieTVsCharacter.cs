using System;
using System.Collections.Generic;

#nullable disable

namespace MundoDisneyApiRest.Models
{
    public partial class MovieTVsCharacter
    {
        public int IdMovieTV { get; set; }
        public int IdCharacter { get; set; }

        public virtual MovieTV MovieTV { get; set; }
        public virtual Character Character { get; set; }
    }
}
