# ⚡ WeatherRadar

Aplicación web desarrollada en ASP.NET Core MVC (.NET 8) que permite consultar el clima en tiempo real de cualquier ciudad del mundo, con pronóstico de 5 días y gestión de ciudades favoritas persistida en SQL Server.

---

## 🌍 Demo

Busca cualquier ciudad del mundo y obtén:
- Temperatura actual, sensación térmica, humedad y viento
- Pronóstico detallado de los próximos 5 días
- Guarda tus ciudades favoritas y accede a ellas rápidamente

---

## 🛠️ Tecnologías usadas

| Tecnología | Uso |
|---|---|
| .NET 8 / C# | Backend y lógica de negocio |
| ASP.NET Core MVC | Patrón de arquitectura |
| Entity Framework Core 8 | Acceso a datos |
| SQL Server (SQLEXPRESS) | Base de datos |
| OpenWeatherMap API | Datos del clima en tiempo real |
| Bootstrap 5.3 | Diseño responsive |

---

## ✅ Requisitos previos

| Herramienta | Versión |
|---|---|
| .NET SDK | 8.0 |
| SQL Server | SQLEXPRESS o LocalDB |
| Visual Studio Code | Cualquier versión reciente |
| Git | Cualquier versión reciente |

---

## 🚀 Pasos para ejecutar el proyecto

### 1. Clonar el repositorio
```bash
git clone https://github.com/Alitos3300K/WeatherRadar.git
cd WeatherRadar
```

### 2. Restaurar dependencias
```bash
dotnet restore
```

### 3. Configurar la cadena de conexión
Abre `appsettings.json` y ajusta el servidor según tu entorno:
```json
"DefaultConnection": "Server=(local)\\SQLEXPRESS;Database=WeatherRadarDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

### 4. Crear la base de datos y stored procedures
Abre SQL Server Management Studio, conéctate a tu servidor y ejecuta:
```
Scripts/database.sql
```

### 5. Aplicar migraciones de Entity Framework
```bash
dotnet ef database update
```

### 6. Ejecutar la aplicación
```bash
dotnet run
```

Abrir en el navegador: `http://localhost:5131`

---

## 📁 Estructura del proyecto
```
WeatherRadar/
├── Controllers/
│   └── WeatherController.cs       # Lógica principal de búsqueda y favoritas
├── Models/
│   ├── CiudadFavorita.cs          # Entidad de base de datos
│   ├── ClimaViewModel.cs          # Modelo para resultados del clima
│   └── PronosticoViewModel.cs     # Modelo para el pronóstico de 5 días
├── Data/
│   └── AppDbContext.cs            # Contexto de Entity Framework
├── Services/
│   └── WeatherService.cs          # Consumo de la API de OpenWeatherMap
├── Views/
│   ├── Weather/
│   │   ├── Index.cshtml           # Formulario de búsqueda
│   │   ├── Resultado.cshtml       # Resultados del clima y pronóstico
│   │   └── Favoritas.cshtml       # Tabla de ciudades favoritas
│   └── Shared/
│       └── _Layout.cshtml         # Layout general de la aplicación
├── Scripts/
│   └── database.sql               # Script SQL con tabla, índices y SPs
├── appsettings.json               # Configuración y cadena de conexión
└── Program.cs                     # Configuración de servicios y middleware
```

---

## 🎨 Decisiones de diseño

- **Paleta azul neón**: Fondo negro profundo (`#010a1a`) con acento azul cian (`#00d4ff`). Transmite modernidad y tecnología — ideal para una app de datos en tiempo real.
- **Tipografía**: Orbitron para títulos (futurista, técnica) combinada con Inter para el cuerpo (limpia y muy legible).
- **UX limpia**: Formulario centrado y minimalista. Tarjetas de información bien organizadas con datos clave visibles de un vistazo.
- **Pronóstico visual**: Cada día del pronóstico tiene su propio icono real de la API, temperatura y humedad para facilitar la lectura.
- **Responsive**: Bootstrap 5 garantiza que la app funcione bien en cualquier tamaño de pantalla.

---

## 📌 Funcionalidades implementadas

- ✅ Búsqueda de clima en tiempo real por nombre de ciudad
- ✅ Temperatura, sensación térmica, humedad, viento, mín/máx
- ✅ Pronóstico de 5 días con iconos reales de la API
- ✅ Guardar ciudades favoritas en SQL Server
- ✅ Ver clima de una ciudad favorita con un click
- ✅ Eliminar ciudades favoritas
- ✅ Entity Framework Core con migraciones
- ✅ Stored Procedures en la base de datos
- ✅ Validaciones frontend con JavaScript
- ✅ Manejo de errores si la ciudad no existe

---

## 🔮 Mejoras pendientes

- Geolocalización automática del usuario al abrir la app
- Mapa interactivo para seleccionar ciudades
- Notificaciones de alertas climáticas extremas
- Gráfica de temperatura de los últimos 7 días
- Soporte para múltiples idiomas
- Dockerizar la aplicación para despliegue portable
- Tests unitarios con xUnit