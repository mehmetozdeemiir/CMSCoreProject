using CMSTekrar.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSTekrar.Web.Models.Dtos
{
    public class UserEdit
    {

        [Required(ErrorMessage = "Must type into user name")]
        [Display(Name = "User Name")]
        [MinLength(3, ErrorMessage = "Minimum lengnt is 3")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Must type into password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserEdit() { }

        public UserEdit(AppUser appUser)
        {
            UserName = appUser.UserName;
            Password = appUser.PasswordHash;
        }
    }
}
