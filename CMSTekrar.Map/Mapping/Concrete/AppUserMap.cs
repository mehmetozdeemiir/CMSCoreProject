using CMSTekrar.Entity.Entities.Concrete;
using CMSTekrar.Map.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace CMSTekrar.Map.Mapping.Concrete
{
    public  class AppUserMap:BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Occupation).IsRequired(true);
            base.Configure(builder);
        }
    }
}
