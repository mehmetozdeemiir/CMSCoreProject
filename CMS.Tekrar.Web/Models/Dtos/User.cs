using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSTekrar.Web.Models.Dtos
{
    public class User
    {
        [Required(ErrorMessage ="Must type into user Name")]
        [Display(Name ="User Name")]
        [MinLength(3, ErrorMessage ="Minimum length is 3")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Must type into password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must type into email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Must to allow email format")]
        [Display(Name = "Email Address")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Email { get; set; }


    }
}
