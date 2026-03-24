using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherRadar.Data;
using WeatherRadar.Models;
using WeatherRadar.Services;

namespace WeatherRadar.Controllers;

// Controlador principal de WeatherRadar
// Maneja la búsqueda del clima y las ciudades favoritas
public class WeatherController : Controller
{
    private readonly WeatherService _weatherService;
    private readonly AppDbContext _context;

    // Inyectamos el servicio del clima y el contexto de la BD
    public WeatherController(WeatherService weatherService, AppDbContext context)
    {
        _weatherService = weatherService;
        _context = context;
    }

    // Página principal — muestra el formulario de búsqueda
    public IActionResult Index()
    {
        return View();
    }

    // Se ejecuta cuando el usuario busca una ciudad
    [HttpPost]
    public async Task<IActionResult> Buscar(string ciudad)
    {
        if (string.IsNullOrWhiteSpace(ciudad))
        {
            ViewBag.Error = "Por favor ingresa el nombre de una ciudad.";
            return View("Index");
        }

        // Llamamos a la API de OpenWeatherMap
        var clima = await _weatherService.ObtenerClimaAsync(ciudad);

        if (clima == null)
        {
            ViewBag.Error = "No se encontró la ciudad. Verifica el nombre e intenta de nuevo.";
            return View("Index");
        }

        return View("Resultado", clima);
    }

    // Guarda una ciudad como favorita en la base de datos
    [HttpPost]
    public async Task<IActionResult> GuardarFavorita(string ciudad, string pais)
    {
        // Verificamos que no esté ya guardada
        var existe = await _context.CiudadesFavoritas
            .AnyAsync(c => c.NombreCiudad.ToLower() == ciudad.ToLower());

        if (!existe)
        {
            _context.CiudadesFavoritas.Add(new CiudadFavorita
            {
                NombreCiudad = ciudad,
                Pais = pais,
                FechaGuardado = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Favoritas");
    }

    // Muestra todas las ciudades favoritas guardadas
    public async Task<IActionResult> Favoritas()
    {
        var favoritas = await _context.CiudadesFavoritas
            .OrderByDescending(c => c.FechaGuardado)
            .ToListAsync();

        return View(favoritas);
    }

    // Elimina una ciudad favorita
    [HttpPost]
    public async Task<IActionResult> EliminarFavorita(int id)
    {
        var ciudad = await _context.CiudadesFavoritas.FindAsync(id);
        if (ciudad != null)
        {
            _context.CiudadesFavoritas.Remove(ciudad);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Favoritas");
    }
}
