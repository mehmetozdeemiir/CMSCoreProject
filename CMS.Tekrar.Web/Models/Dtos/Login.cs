using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSTekrar.Web.Models.Dtos
{
    public class Login
    {
        [Required(ErrorMessage = "Must type into password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must type into user name")]
        [Display(Name = "User Name")]
        [MinLength(3, ErrorMessage = "Minimum lengnt is 3")]
        public string UserName { get; set; }

        public string ReturnUrl { get; set; }

    }
}
