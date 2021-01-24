using CMSTekrar.Data.Context;
using CMSTekrar.Data.Repositories.Concrete.EntityTypeRepositories;
using CMSTekrar.Data.Repositories.Interfaces.EntityTypeRepositories;
using CMSTekrar.Entity.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CMS.Tekrar.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Proje içerisinde; baðlantýlý olan sýnýflarýmý kendi içlerinde inject ediyorum.
            //Mütekiben IoC container a Register etmek için startup.cs içerisinde bulunan ConfigureServices methodunun içerisine ekliyorum.
            //IOC container da bana baðýmlýlýklarý tersine çevirmeyi temin ediyor.
            //Bu yöntem sýnýflarýmýn sýký sýkýya baðýmlýlýðý ortadan kaldýrmak amacýyla yapýlmýþtýr.Böylece DIP e uyum saðlamýþ olduk.
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;//illa bir sayý olsun mu?=> false.
                opt.Password.RequireLowercase = false;//kücük harf gerekliliði olsun mu?=> false. 
                opt.Password.RequireUppercase = false;//parolada büyük küçük harf zorunlu olsun mu? =>false.
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllerRoute(
                  "page", //Name=> Yolun adý
                  "{slug?}",// "slug?" nedir slug yanýnda id de tasýr..=>pattern bunun yazýlmasýnýn sebebi methodlarýn yapýlacaðý
                  defaults: new { controller = "Page", action = "Page" });

                endpoints.MapControllerRoute(
                    "product",
                    "product/{categorySlug}",
                    defaults: new { controller = "Product", action = "ProductByCategory" });//endpointleri methodlara yönlendirmek için default kullanýlýr


                endpoints.MapControllerRoute( //=>area içerisinde bütün controller üzerinde ki methodlarýn oldugu View sayfasýnýn görüntülenmesi için sadece bu endpointi kullanmak yeterlidir.
                      name: "areas", 
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                //exist=> kullanýldýgýnda name içerisinde yazýlý olan bütün index ve metodlar çalýþýr
                //sað tarafýna yazýlmasý gereken adreslerin "{area:exists}/{controller=Home}/{action=Index}/{id?}" standartý belirtilmesi yeterlidir.
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
