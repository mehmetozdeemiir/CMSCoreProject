using CMSTekrar.Entity.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSTekrar.Map.Mapping.Abstract
{
    //Where T karşısına somut bir sınıf yazılmaz ise yani interface kullanılırsa somutlastırmak için class eki getirilir
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
        //IEntitiyTypeConfiguration=> Varlıkların özelliklerini ve varlık işlemlerini veritabanına eşitlemek için kullanılır.
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //Standart .net'de mapping işlemleri consturactor methodların içerisinde yapılır. Burada Core'a özel olan ve IEntityTypeConfiguration'dan gelen Configure methodunun içeriisinde yapılır.
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.DeleteDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);
        }
    }
}
