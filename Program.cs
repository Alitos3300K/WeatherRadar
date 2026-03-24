using WeatherRadar.Data;
using WeatherRadar.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registramos el soporte para controladores y vistas Razor
builder.Services.AddControllersWithViews();

// Configuramos Entity Framework con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registramos el servicio del clima con HttpClient inyectado
builder.Services.AddHttpClient<WeatherService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta por defecto apunta al WeatherController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Weather}/{action=Index}/{id?}");

app.Run();