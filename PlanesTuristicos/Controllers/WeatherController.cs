using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanesTuristicos.Controllers
{
    public class WeatherController : Controller
    {
        public async Task<IActionResult> Clima()
        {
            // Lista de municipios de Cundinamarca
            List<string> municipios = new List<string>
            {
                "Bogota",
                "Soacha",
                "Facatativa",
                "Fusagasuga",
                "Girardot",
                "Cajica",
                "Tocancipa",
                "Ubate",
                "Sibate",
                "Villeta",
                "Anolaima",
                "Tocaima",
                
                 
                
            };

            // Llama al método GetWeatherAsync y espera su finalización
            var weatherDataList = await GetWeatherForMultipleCitiesAsync(municipios);

            // Devuelve la vista "Clima" junto con los datos meteorológicos de todos los municipios
            return View("Clima", weatherDataList);
        }

        static async Task<List<string>> GetWeatherForMultipleCitiesAsync(List<string> cities)
        {
            string apiKey = "33e73b93843dd6505e841099faba4531"; // Reemplaza con tu clave API de Weatherstack

            // Crear una lista para almacenar la información meteorológica de cada ciudad
            List<string> weatherDataList = new List<string>();

            // Obtener información meteorológica para cada ciudad
            foreach (var city in cities)
            {
                var weatherData = await GetWeatherAsync(apiKey, city);
                weatherDataList.Add(weatherData);
            }

            return weatherDataList;
        }

        static async Task<string> GetWeatherAsync(string apiKey, string location)
        {
            string apiUrl = $"http://api.weatherstack.com/current?access_key={apiKey}&query={location}";


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine($"Error al consumir la API. Código de estado: {response.StatusCode}");
                        return $"Error al consumir la API. Código de estado: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return $"Error: {ex.Message}";
                }
            }
        }
    }
}
