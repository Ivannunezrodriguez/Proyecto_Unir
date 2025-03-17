using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SmartGameCatalog.API.Models;

namespace SmartGameCatalog.API.Services
{
    /// <summary>
    /// Servicio para la comunicación con la base de datos vectorial Weaviate.
    /// </summary>
    public class WeaviateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor que inicializa el cliente HTTP y la configuración.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP para realizar solicitudes.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        public WeaviateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Agrega un videojuego a la base de datos Weaviate.
        /// </summary>
        /// <param name="title">Título del videojuego.</param>
        /// <param name="description">Descripción del videojuego.</param>
        /// <param name="releaseDate">Fecha de lanzamiento.</param>
        /// <param name="coverUrl">URL de la portada.</param>
        /// <param name="developer">Desarrollador del videojuego.</param>
        /// <param name="platform">Plataforma del videojuego.</param>
        /// <param name="rating">Calificación del videojuego.</param>
        /// <param name="categoryId">Identificador de la categoría.</param>
        /// <returns>Un booleano indicando el éxito o fallo de la operación.</returns>
        public async Task<bool> AddGameToWeaviateAsync(string title, string description, string releaseDate, string coverUrl, string developer, string platform, float rating, Guid? categoryId)
        {
            if (DateTime.TryParse(releaseDate, out DateTime parsedDate))
            {
                releaseDate = parsedDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            else
            {
                Console.WriteLine("Error: Fecha inválida.");
                return false;
            }

            var requestBody = new
            {
                @class = "VideoGame",
                properties = new
                {
                    title,
                    description,
                    releaseDate,
                    coverUrl,
                    developer,
                    platform,
                    rating,
                    categoryId
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:8080/v1/objects", content);

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Weaviate Response: {responseBody}");

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> CheckWeaviateConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:8080/v1/meta");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene recomendaciones de videojuegos en función de una consulta de búsqueda.
        /// </summary>
        /// <param name="searchQuery">Consulta de búsqueda para obtener recomendaciones.</param>
        /// <returns>Lista de videojuegos recomendados.</returns>
        public async Task<List<VideoGame>> GetRecommendationsAsync(string searchQuery)
        {
            var query = new
            {
                query = $@"
        {{
            Get {{
                VideoGame(
                    nearText: {{ concepts: [""{searchQuery}""] }},
                    limit: 5
                ) {{
                    title
                    description
                    coverUrl
                    developer
                    platform
                    rating
                    categoryId
                }}
            }}
        }}"
            };

            var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:8080/v1/graphql", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response from Weaviate: " + responseBody); // Depuración

            try
            {
                using var jsonDoc = JsonDocument.Parse(responseBody);
                var recommendations = new List<VideoGame>();

                foreach (var game in jsonDoc.RootElement.GetProperty("data").GetProperty("Get").GetProperty("VideoGame").EnumerateArray())
                {
                    recommendations.Add(new VideoGame
                    {
                        Title = game.TryGetProperty("title", out var title) && title.ValueKind == JsonValueKind.String
                            ? title.GetString() ?? "Unknown"
                            : "Unknown",

                        Description = game.TryGetProperty("description", out var description) && description.ValueKind == JsonValueKind.String
                            ? description.GetString() ?? "No description available"
                            : "No description available",

                        CoverUrl = game.TryGetProperty("coverUrl", out var coverUrl) && coverUrl.ValueKind == JsonValueKind.String
                            ? coverUrl.GetString() ?? ""
                            : "",

                        Developer = game.TryGetProperty("developer", out var developer) && developer.ValueKind == JsonValueKind.String
                            ? developer.GetString() ?? "Unknown"
                            : "Unknown",

                        Platform = game.TryGetProperty("platform", out var platform) && platform.ValueKind == JsonValueKind.String
                            ? platform.GetString() ?? "Unknown"
                            : "Unknown",

                        Rating = game.TryGetProperty("rating", out var rating) && rating.ValueKind == JsonValueKind.Number
                            ? (float)rating.GetDouble()
                            : 0.0f,

                        Category_id = game.TryGetProperty("categoryId", out var category) && category.ValueKind == JsonValueKind.String
                            ? Guid.Parse(category.GetString() ?? Guid.Empty.ToString())
                            : (Guid?)null
                    });
                }

                return recommendations;
            }
            catch (Exception ex)
            {
                Console.WriteLine("JSON Parsing Error: " + ex.Message);
                return new List<VideoGame>();
            }
        }


    }
}
