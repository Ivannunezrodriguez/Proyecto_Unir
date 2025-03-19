using System.Text;
using System.Text.Json;

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
        public WeaviateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["Weaviate:ApiUrl"] ?? throw new ArgumentNullException("Weaviate:ApiUrl", "La URL de Weaviate no está configurada.");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        /// <summary>
        /// Obtiene recomendaciones basadas en los juegos que un usuario ha calificado.
        /// </summary>
        public async Task<List<GameRecommendation>> GetRecommendations(int userId)
        {
            try
            {
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

                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error al obtener recomendaciones: {response.StatusCode}");
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<WeaviateResponse>(jsonString, _jsonOptions);

                return result?.Data?.Get?.GameRecommendation ?? new List<GameRecommendation>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en WeaviateService: {ex.Message}");
            }
        }
    }

    // Clases auxiliares para deserializar la respuesta
    public class WeaviateResponse
    {
        public DataContainer? Data { get; set; }
    }

    public class DataContainer
    {
        public GetContainer? Get { get; set; }
    }

    public class GetContainer
    {
        public List<GameRecommendation>? GameRecommendation { get; set; }
    }

    public class GameRecommendation
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
