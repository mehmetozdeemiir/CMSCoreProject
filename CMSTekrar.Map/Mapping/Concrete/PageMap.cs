using CMSTekrar.Entity.Entities.Concrete;
using CMSTekrar.Map.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSTekrar.Map.Mapping.Concrete
{
    public class PageMap:BaseMap<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.Slug).IsRequired(true);
            builder.Property(x => x.Sorting).IsRequired(true);
            builder.Property(x => x.Content).IsRequired(true);
            base.Configure(builder);
        }
    }
}
