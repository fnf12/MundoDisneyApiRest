using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required]
        public string Pass { get; set; }

        [Compare("Pass")]
        [NotMapped]
        [Required]
        public string ConfirmPass { get; set; }
    }
}
