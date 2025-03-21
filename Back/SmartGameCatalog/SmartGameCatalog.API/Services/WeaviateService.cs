using System.Text;
using System.Text.Json;

namespace SmartGameCatalog.API.Services
{
    public class WeaviateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly JsonSerializerOptions _jsonOptions;

        public WeaviateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["Weaviate:ApiUrl"] ?? throw new ArgumentNullException("Weaviate:ApiUrl", "La URL de Weaviate no está configurada.");
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = false };
        }

public async Task<List<GameRecommendation>> GetRecommendations(int userId)
{
    try
    {
        var query = new
        {
            query = $@"
            {{
                Get {{
                    GameRecommendation(
                        where: {{
                            path: [""userId""],
                            operator: Equal,
                            valueInt: {userId}
                        }}
                    ) {{
                        userId
                        gameId
                        gameTitle
                        reason
                        createdAt
                    }}
                }}
            }}"
        };

        var jsonQuery = JsonSerializer.Serialize(query, _jsonOptions);
        Console.WriteLine("🔎 Consulta enviada a Weaviate: " + jsonQuery);

        var content = new StringContent(jsonQuery, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_apiUrl, content);

        var jsonString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("🔹 Respuesta de Weaviate: " + jsonString);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error en la consulta a Weaviate: {response.StatusCode} {jsonString}");
        }

        var result = JsonSerializer.Deserialize<WeaviateResponse>(jsonString, _jsonOptions);
        return result?.Data?.Get?.GameRecommendation ?? new List<GameRecommendation>();
    }
    catch (Exception ex)
    {
        throw new Exception($"Error en WeaviateService: {ex.Message}");
    }
}





    }

    // Clases auxiliares para deserializar la respuesta de Weaviate
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
