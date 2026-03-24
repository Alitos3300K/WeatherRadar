using Microsoft.EntityFrameworkCore;
using WeatherRadar.Models;

namespace WeatherRadar.Data;

// Contexto principal de Entity Framework
// Maneja la conexión con SQL Server y el mapeo de modelos
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Representa la tabla CiudadesFavoritas en la base de datos
    public DbSet<CiudadFavorita> CiudadesFavoritas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CiudadFavorita>(entity =>
        {
            entity.ToTable("CiudadesFavoritas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NombreCiudad).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Pais).HasMaxLength(100);
        });
    }
}