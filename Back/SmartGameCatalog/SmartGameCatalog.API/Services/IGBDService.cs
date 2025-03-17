using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using SmartGameCatalog.API.Models;

namespace SmartGameCatalog.API.Services
{
    /// <summary>
    /// Servicio para la comunicación con la API de IGDB.
    /// </summary>
    public class IGDBService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string? _accessToken;

        /// <summary>
        /// Constructor que inicializa el cliente HTTP y la configuración.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP para realizar solicitudes.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        public IGDBService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Autentica la aplicación en IGDB y obtiene un Access Token desde Twitch.
        /// </summary>
        public async Task AuthenticateAsync()
        {
            try
            {
                var clientId = _configuration["IGDB:ClientId"];
                var clientSecret = _configuration["IGDB:ClientSecret"];

                var tokenUrl = $"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials";
                var response = await _httpClient.PostAsync(tokenUrl, null);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(content);
            if (jsonDoc.RootElement.TryGetProperty("access_token", out var token) && token.ValueKind == JsonValueKind.String)
{
    _accessToken = token.GetString() ?? string.Empty;
}
else
{
    _accessToken = string.Empty;
}


                if (string.IsNullOrEmpty(_accessToken))
                {
                    throw new Exception("No se pudo obtener el token de acceso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en autenticación de IGDB: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Busca videojuegos en IGDB en función de la consulta proporcionada.
        /// </summary>
        /// <param name="query">Término de búsqueda para encontrar videojuegos.</param>
        /// <returns>Lista de videojuegos encontrados.</returns>
        public async Task<List<VideoGame>> SearchGamesAsync(string query)
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
                Content = new StringContent($@"
                    search ""{query}"";
                    fields name, cover.url, genres.name, platforms.name, release_dates.human, summary, involved_companies.company.name, rating;
                    limit 10;", System.Text.Encoding.UTF8, "application/json")
            };

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(content);

                var games = new List<VideoGame>();

                foreach (var game in jsonDoc.RootElement.EnumerateArray())
                {
                    var coverUrl = game.TryGetProperty("cover", out var cover) && cover.TryGetProperty("url", out var url)
                        ? "https:" + url.GetString()
                        : string.Empty;

                  var developer = "Desconocido";
if (game.TryGetProperty("involved_companies", out var companies) && companies.ValueKind == JsonValueKind.Array)
{
    var firstCompany = companies.EnumerateArray().FirstOrDefault();
    if (firstCompany.ValueKind == JsonValueKind.Object &&
        firstCompany.TryGetProperty("company", out var company) &&
        company.TryGetProperty("name", out var companyName))
    {
        developer = companyName.GetString() ?? "Desconocido";
    }
}

var platform = "Desconocido";
if (game.TryGetProperty("platforms", out var platforms) && platforms.ValueKind == JsonValueKind.Array)
{
    var firstPlatform = platforms.EnumerateArray().FirstOrDefault();
    if (firstPlatform.ValueKind == JsonValueKind.String)
    {
        platform = firstPlatform.GetString() ?? "Desconocido";
    }
}


                    games.Add(new VideoGame
                    {
                        Title = game.GetProperty("name").GetString() ?? "Sin título",
Description = game.TryGetProperty("summary", out var summary) && summary.ValueKind == JsonValueKind.String
    ? summary.GetString() ?? "Descripción no disponible"
    : "Descripción no disponible",
                        CoverUrl = coverUrl,
                        Developer = developer ?? "Desconocido",
                        Platform = platform,
                   Rating = game.TryGetProperty("rating", out var rating) && rating.ValueKind == JsonValueKind.Number
    ? (float)rating.GetDouble()
    : 0f,

                        Category_id = null // IGDB no proporciona directamente una categoría equivalente
                    });
                }

                return games;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en búsqueda de juegos en IGDB: {ex.Message}");
                return new List<VideoGame>();
            }
        }
    }
}
