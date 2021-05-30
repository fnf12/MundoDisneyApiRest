using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}
