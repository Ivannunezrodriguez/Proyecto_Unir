using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartGameCatalog.API.Services
{
    public class IGDBService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
   private string? _accessToken;



        public IGDBService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Método para obtener el Access Token desde Twitch
        public async Task AuthenticateAsync()
        {
            var clientId = _configuration["IGDB:ClientId"];
            var clientSecret = _configuration["IGDB:ClientSecret"];

            var tokenUrl = $"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials";

            var response = await _httpClient.PostAsync(tokenUrl, null);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(content);
       _accessToken = jsonDoc.RootElement.TryGetProperty("access_token", out var token) ? token.GetString() ?? string.Empty : string.Empty;

            
        }

        // Método para buscar videojuegos en IGDB
        public async Task<string> SearchGamesAsync(string query)
{
    if (string.IsNullOrEmpty(_accessToken))
    {
        await AuthenticateAsync();
    }

    var clientId = _configuration["IGDB:ClientId"];
    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.igdb.com/v4/games")
    {
        Headers =
        {
            { "Client-ID", clientId },
            { "Authorization", $"Bearer {_accessToken}" }
        },
        Content = new StringContent($"search \"{query}\"; fields name,cover.url,genres.name,platforms.name,release_dates.human,summary; limit 10;", System.Text.Encoding.UTF8, "application/json")
    };

    var response = await _httpClient.SendAsync(request);
    response.EnsureSuccessStatusCode();
    
    var content = await response.Content.ReadAsStringAsync();
    using var jsonDoc = JsonDocument.Parse(content);
    
    var games = new List<object>();

    foreach (var game in jsonDoc.RootElement.EnumerateArray())
    {
        var coverUrl = game.TryGetProperty("cover", out var cover) && cover.TryGetProperty("url", out var url)
            ? "https:" + url.GetString()
            : string.Empty;

        games.Add(new
        {
            Id = game.GetProperty("id").GetInt32(),
            Name = game.GetProperty("name").GetString(),
            CoverUrl = coverUrl,
            Summary = game.TryGetProperty("summary", out var summary) ? summary.GetString() : "No description available"
        });
    }

    return JsonSerializer.Serialize(games);
}
    }
}
