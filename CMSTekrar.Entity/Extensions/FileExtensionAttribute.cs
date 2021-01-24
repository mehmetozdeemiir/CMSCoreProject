using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace CMSTekrar.Entity.Extensions
{
   public class FileExtensionAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg", "jfif" };
                bool result = extension.Any(x => extension.EndsWith(x));

                if (!result) new ValidationResult(ErrorMessage = "Allowed extension are jpg,png,jpeg,jfif");

            }

            return ValidationResult.Success;
        }

    }
}
