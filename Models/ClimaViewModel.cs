namespace WeatherRadar.Models;

// ViewModel con toda la información del clima de una ciudad
public class ClimaViewModel
{
    // Nombre de la ciudad
    public string Ciudad { get; set; } = string.Empty;

    // País
    public string Pais { get; set; } = string.Empty;

    // Temperatura actual en Celsius
    public double Temperatura { get; set; }

    // Sensación térmica
    public double SensacionTermica { get; set; }

    // Temperatura mínima y máxima del día
    public double TempMin { get; set; }
    public double TempMax { get; set; }

    // Humedad en porcentaje
    public int Humedad { get; set; }

    // Velocidad del viento en m/s
    public double VelocidadViento { get; set; }

    // Descripción del clima — ej: "cielo despejado"
    public string Descripcion { get; set; } = string.Empty;

    // Icono del clima que viene de la API
    public string Icono { get; set; } = string.Empty;

    // Pronóstico de los próximos 5 días
    public List<PronosticoViewModel> Pronostico { get; set; } = new();
}