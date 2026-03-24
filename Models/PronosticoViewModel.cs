namespace WeatherRadar.Models;

// ViewModel para cada día del pronóstico de 5 días
public class PronosticoViewModel
{
    // Fecha del pronóstico
    public DateTime Fecha { get; set; }

    // Temperatura del día
    public double Temperatura { get; set; }

    // Descripción del clima ese día
    public string Descripcion { get; set; } = string.Empty;

    // Icono del clima ese día
    public string Icono { get; set; } = string.Empty;

    // Humedad ese día
    public int Humedad { get; set; }
}