using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieHub.Data;
using MovieHub.Data.Cart;
using MovieHub.Data.Services;
using MovieHub.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddScoped<IActorsService, ActorsService>();
        builder.Services.AddScoped<IProducersService, ProducersService>();
        builder.Services.AddScoped<ICinemasService, CinemasService>();
        builder.Services.AddScoped<IMoviesService, MoviesService>();
        builder.Services.AddScoped<IOrdersService, OrdersService>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

        //Authentication and authorization
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        builder.Services.AddControllersWithViews();

        //DbContext config
        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession(); 
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        //Seed initializer
        AppDbInitializer.Seed(app);
        AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

        app.Run();
    }
}