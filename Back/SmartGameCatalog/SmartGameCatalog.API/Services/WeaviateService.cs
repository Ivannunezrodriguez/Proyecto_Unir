using System;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartGameCatalog.API.Services
{
    /// <summary>
    /// Servicio para obtener recomendaciones de videojuegos utilizando Weaviate.
    /// </summary>
    public class WeaviateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// Constructor que inicializa el servicio con la URL de la API de Weaviate.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP inyectado.</param>
        /// <param name="configuration">Configuración de la aplicación para obtener la URL.</param>
        public WeaviateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["Weaviate:ApiUrl"] ?? throw new ArgumentNullException("Weaviate:ApiUrl", "La URL de Weaviate no está configurada.");

            // Configuración de serialización JSON
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        /// <summary>
        /// Obtiene recomendaciones basadas en los juegos que un usuario ha calificado.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>JSON con los juegos recomendados.</returns>
        public async Task<string> GetRecommendations(int userId)
        {
            try
            {
                // Construcción del Query GraphQL
                var query = new
                {
                    query = $@"
                    {{
                        Get {{
                            GameRecommendation(where: {{ userId: {userId} }}) {{
                                gameId
                                gameTitle
                                reason
                                createdAt
                            }}
                        }}
                    }}"
                };

                var content = new StringContent(JsonSerializer.Serialize(query, _jsonOptions), Encoding.UTF8, "application/json");

                // Enviar la solicitud a Weaviate
                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    return $"Error al obtener recomendaciones: {response.StatusCode}";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error en WeaviateService: {ex.Message}";
            }
        }
    }
}
