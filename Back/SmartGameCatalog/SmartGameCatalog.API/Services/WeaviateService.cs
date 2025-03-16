using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartGameCatalog.API.Services
{
    public class WeaviateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeaviateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

       public async Task<bool> AddGameToWeaviateAsync(string title, string description, List<string> genres, List<string> platforms, string releaseDate, string coverUrl)
{
    // 🛠 Convertir `releaseDate` a formato RFC3339
    if (DateTime.TryParse(releaseDate, out DateTime parsedDate))
    {
        releaseDate = parsedDate.ToString("yyyy-MM-ddTHH:mm:ssZ"); // ✅ Formato correcto
    }
    else
    {
        Console.WriteLine("⚠ Error: Fecha inválida.");
        return false; // No continuar si la fecha es incorrecta
    }

    var requestBody = new
    {
        @class = "VideoGame",
        properties = new
        {
            title,
            description,
            genres,
            platforms,
            releaseDate,
            coverUrl
        }
    };

    var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
    var response = await _httpClient.PostAsync("http://localhost:8080/v1/objects", content);

    var responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Weaviate Response: {responseBody}");

    return response.IsSuccessStatusCode;
}



       public async Task<List<VideoGame>> GetRecommendationsAsync(string searchQuery)
{
    var playedGames = await _databaseService.GetPlayedGamesByUser(userId);
var concepts = playedGames.Select(g => g.Title).ToArray();

var query = new
{
    query = $@"
    {{
        Get {{
            VideoGame(
                nearText: {{ concepts: {JsonConvert.SerializeObject(concepts)} }},
                limit: 5
            ) {{
                title
                description
                coverUrl
            }}
        }}
    }}"
};


    }
}
