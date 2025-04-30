
using Hakutron1337.Models;
using Hakutron1337.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//services area

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();


var app = builder.Build();


//middleware area

app.UseStaticFiles();
app.UseRouting();     // ❗ Necesario para que funcione MapControllerRoute
app.UseAuthorization();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}"); // ← ahora arranca en /Products

app.Run();