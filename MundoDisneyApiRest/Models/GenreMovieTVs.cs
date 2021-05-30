using MundoDisneyApiRest.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace MundoDisneyApiRest.Models
{
    public partial class GenreMovieTVs
    {
        public int IdGenre { get; set; }
        public int IdMovieTV { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual MovieTV MovieTV { get; set; }
    }
}
