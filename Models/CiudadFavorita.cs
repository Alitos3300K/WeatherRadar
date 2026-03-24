using System.ComponentModel.DataAnnotations;

namespace WeatherRadar.Models;

// Modelo que representa una ciudad favorita guardada por el usuario
public class CiudadFavorita
{
    public int Id { get; set; }

    // Nombre de la ciudad buscada
    [Required]
    [MaxLength(200)]
    public string NombreCiudad { get; set; } = string.Empty;

    // País al que pertenece la ciudad
    [MaxLength(100)]
    public string Pais { get; set; } = string.Empty;

    // Fecha en que se guardó como favorita
    public DateTime FechaGuardado { get; set; } = DateTime.Now;
}