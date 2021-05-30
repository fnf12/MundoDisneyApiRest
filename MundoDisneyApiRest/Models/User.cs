using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisneyApiRest.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Pass { get; set; }

        [Compare("Pass")]
        [NotMapped]
        [Required]
        public string ConfirmPass { get; set; }
    }
}
