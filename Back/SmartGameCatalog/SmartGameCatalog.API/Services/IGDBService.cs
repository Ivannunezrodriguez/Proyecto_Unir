using System.Text;
using System.Text.Json;

namespace SmartGameCatalog.API.Services
{
    /// <summary>
    /// Servicio para obtener datos de videojuegos desde IGDB.
    /// </summary>
    public class IGDBService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IGDBAuthService _authService;

        public IGDBService(HttpClient httpClient, IConfiguration configuration, IGDBAuthService authService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _authService = authService;
        }

        /// <summary>
        /// Obtiene información de un videojuego por su ID.
        /// </summary>
        public async Task<string> GetGameById(int gameId)
        {
            var token = await _authService.GetAccessToken();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Client-ID", _configuration["IGDB:ClientId"]);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var requestBody = new { query = $"fields name, summary, cover.url; where id = {gameId};" };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://api.igdb.com/v4/games", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error en IGDB: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en IGDBService: {ex.Message}", ex);
            }
        }
    }
}
