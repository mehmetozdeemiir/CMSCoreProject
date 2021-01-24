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
            //Proje i�erisinde; ba�lant�l� olan s�n�flar�m� kendi i�lerinde inject ediyorum.
            //M�tekiben IoC container a Register etmek i�in startup.cs i�erisinde bulunan ConfigureServices methodunun i�erisine ekliyorum.
            //IOC container da bana ba��ml�l�klar� tersine �evirmeyi temin ediyor.
            //Bu y�ntem s�n�flar�m�n s�k� s�k�ya ba��ml�l��� ortadan kald�rmak amac�yla yap�lm��t�r.B�ylece DIP e uyum sa�lam�� olduk.
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;//illa bir say� olsun mu?=> false.
                opt.Password.RequireLowercase = false;//k�c�k harf gereklili�i olsun mu?=> false. 
                opt.Password.RequireUppercase = false;//parolada b�y�k k���k harf zorunlu olsun mu? =>false.
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
                  "page", //Name=> Yolun ad�
                  "{slug?}",// "slug?" nedir slug yan�nda id de tas�r..=>pattern bunun yaz�lmas�n�n sebebi methodlar�n yap�laca��
                  defaults: new { controller = "Page", action = "Page" });

                endpoints.MapControllerRoute(
                    "product",
                    "product/{categorySlug}",
                    defaults: new { controller = "Product", action = "ProductByCategory" });//endpointleri methodlara y�nlendirmek i�in default kullan�l�r


                endpoints.MapControllerRoute( //=>area i�erisinde b�t�n controller �zerinde ki methodlar�n oldugu View sayfas�n�n g�r�nt�lenmesi i�in sadece bu endpointi kullanmak yeterlidir.
                      name: "areas", 
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                //exist=> kullan�ld�g�nda name i�erisinde yaz�l� olan b�t�n index ve metodlar �al���r
                //sa� taraf�na yaz�lmas� gereken adreslerin "{area:exists}/{controller=Home}/{action=Index}/{id?}" standart� belirtilmesi yeterlidir.
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
