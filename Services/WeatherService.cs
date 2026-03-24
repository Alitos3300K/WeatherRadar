using System.Text.Json;
using WeatherRadar.Models;

namespace WeatherRadar.Services;

// Servicio que consume la API de OpenWeatherMap
public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "fcd2d630a496ead27f69376e795ae19e";
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Obtiene el clima actual de una ciudad
    public async Task<ClimaViewModel?> ObtenerClimaAsync(string ciudad)
    {
        try
        {
            var url = $"{BaseUrl}/weather?q={Uri.EscapeDataString(ciudad)}&appid={_apiKey}&units=metric&lang=es";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<JsonElement>(json, options);

            // Obtenemos el pronóstico de 5 días
            var pronostico = await ObtenerPronosticoAsync(ciudad);

            var main = root.GetProperty("main");
            var weather = root.GetProperty("weather")[0];
            var wind = root.GetProperty("wind");
            var sys = root.GetProperty("sys");

            var clima = new ClimaViewModel
            {
                Ciudad = root.GetProperty("name").GetString() ?? ciudad,
                Pais = sys.GetProperty("country").GetString() ?? "",
                Temperatura = main.GetProperty("temp").GetDouble(),
                SensacionTermica = main.GetProperty("feels_like").GetDouble(),
                TempMin = main.GetProperty("temp_min").GetDouble(),
                TempMax = main.GetProperty("temp_max").GetDouble(),
                Humedad = main.GetProperty("humidity").GetInt32(),
                VelocidadViento = wind.GetProperty("speed").GetDouble(),
                Descripcion = weather.GetProperty("description").GetString() ?? "",
                Icono = weather.GetProperty("icon").GetString() ?? "",
                Pronostico = pronostico
            };

            return clima;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    // Obtiene el pronóstico de los próximos 5 días
    private async Task<List<PronosticoViewModel>> ObtenerPronosticoAsync(string ciudad)
    {
        try
        {
            var url = $"{BaseUrl}/forecast?q={Uri.EscapeDataString(ciudad)}&appid={_apiKey}&units=metric&lang=es";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<PronosticoViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var root = JsonSerializer.Deserialize<JsonElement>(json, options);

            var pronostico = new List<PronosticoViewModel>();
            var fechasAgregadas = new HashSet<string>();

            foreach (var item in root.GetProperty("list").EnumerateArray())
            {
                var fecha = DateTimeOffset.FromUnixTimeSeconds(item.GetProperty("dt").GetInt64()).LocalDateTime;
                var fechaKey = fecha.ToString("yyyy-MM-dd");

                if (!fechasAgregadas.Contains(fechaKey) && pronostico.Count < 5)
                {
                    fechasAgregadas.Add(fechaKey);
                    var itemMain = item.GetProperty("main");
                    var itemWeather = item.GetProperty("weather")[0];

                    pronostico.Add(new PronosticoViewModel
                    {
                        Fecha = fecha,
                        Temperatura = itemMain.GetProperty("temp").GetDouble(),
                        Descripcion = itemWeather.GetProperty("description").GetString() ?? "",
                        Icono = itemWeather.GetProperty("icon").GetString() ?? "",
                        Humedad = itemMain.GetProperty("humidity").GetInt32()
                    });
                }
            }

            return pronostico;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error pronóstico: {ex.Message}");
            return new List<PronosticoViewModel>();
        }
    }
}