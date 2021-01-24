using CMSTekrar.Entity.Entities.Interfaces;
using CMSTekrar.Entity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSTekrar.Entity.Entities.Concrete
{
    //IdendityUSer=>Microsoftun bize hazırladıgı bir sınıf user ile ilgili işlemlerde hızlı kullanabilmemiz için bize bir çok yapı sağlayan bir sınıf user role login registirition işlemlerin hazır yapılarını sunmaktadır.
    //Bu sınıfın hazır tabloların "Id" sutunu barındırdıgından alısık oldugumuz gibi ıbaseentity.cs arayuzunden varlıklarımıza ıd basmadık.

    public class AppUser : IdentityUser, IBaseEntity
    {
        //IdentityUser sınıfının içermediği ama projede ihtiyaç duyulan özellikler burada eklenilebilir.
        public string Occupation { get; set; }


        private DateTime _createDate = DateTime.Now;

        public DateTime CreateDate { get =>_createDate; set=>_createDate=value ; }
        
        public DateTime? UpdateDate { get ; set; }
        
        public DateTime? DeleteDate { get ; set ; }

        private Status _status = Status.Active;
        public Status Status { get=>_status ; set =>_status=value; }
    }
}
