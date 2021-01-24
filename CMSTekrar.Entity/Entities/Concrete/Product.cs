using CMSTekrar.Entity.Entities.Interfaces;
using CMSTekrar.Entity.Enums;
using CMSTekrar.Entity.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace CMSTekrar.Entity.Entities.Concrete
{
   public class Product:IBaseEntity
    {
       
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string Name { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum lenght is 2")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2")]
        public decimal UnitPrice { get; set; }

        public string Image { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "You must to choose a category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]//yazmasakta olur mapping yapıcaz zatenss
        public virtual Category Category { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
