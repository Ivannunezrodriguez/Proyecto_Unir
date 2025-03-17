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
            fields id, name, cover.url, genres.name, platforms.name, release_dates.human, summary, involved_companies.company.name, rating;
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
            // 📌 Verificamos si "id" existe y lo convertimos en un GUID válido
            var id = game.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.Number
                ? Guid.NewGuid() // Se genera un nuevo ID porque IGDB usa números en lugar de GUID
                : Guid.Empty;

            var title = game.TryGetProperty("name", out var titleProp) && titleProp.ValueKind == JsonValueKind.String
                ? titleProp.GetString() ?? "No Title"
                : "No Title";

            var description = game.TryGetProperty("summary", out var summaryProp) && summaryProp.ValueKind == JsonValueKind.String
                ? summaryProp.GetString() ?? "No description available"
                : "No description available";

            var coverUrl = game.TryGetProperty("cover", out var cover) &&
                           cover.TryGetProperty("url", out var url) &&
                           url.ValueKind == JsonValueKind.String
                ? "https:" + url.GetString()
                : "";

            var developer = game.TryGetProperty("involved_companies", out var companies) &&
                            companies.EnumerateArray().FirstOrDefault().TryGetProperty("company", out var company) &&
                            company.TryGetProperty("name", out var companyName) &&
                            companyName.ValueKind == JsonValueKind.String
                ? companyName.GetString() ?? "Unknown"
                : "Unknown";

            var platform = game.TryGetProperty("platforms", out var platforms) &&
                           platforms.EnumerateArray().FirstOrDefault().ValueKind == JsonValueKind.String
                ? platforms.EnumerateArray().FirstOrDefault().GetString() ?? "Unknown"
                : "Unknown";

            var rating = game.TryGetProperty("rating", out var ratingProp) && ratingProp.ValueKind == JsonValueKind.Number
                ? (float)ratingProp.GetDouble()
                : 0.0f;

            games.Add(new VideoGame
            {
                Id_VideoGame = id, // 📌 Ahora asignamos un GUID válido en lugar de dejarlo vacío
                Title = title,
                Description = description,
                CoverUrl = coverUrl,
                Developer = developer,
                Platform = platform,
                Rating = rating,
                Category_id = null // IGDB no proporciona directamente una categoría
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
