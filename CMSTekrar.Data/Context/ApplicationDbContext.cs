using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CMSTekrar.Data.SeedData;
using CMSTekrar.Entity.Entities.Concrete;
using CMSTekrar.Entity.Entities.Interfaces;
using CMSTekrar.Map.Mapping.Concrete;


namespace CMSTekrar.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }//Constructor dan ınject edip devam

       
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new PageMap());
            builder.ApplyConfiguration(new ProductMap());

          
            builder.ApplyConfiguration(new SeedPage());

            base.OnModelCreating(builder);

        }

        //uygulamada lazy loading i açmak için paket yükleyip bunu açtık
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}