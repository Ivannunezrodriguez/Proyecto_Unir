using System.Text;
using System.Text.Json;

namespace SmartGameCatalog.API.Services
{
    /// <summary>
    /// Servicio para autenticación en IGDB.
    /// </summary>
    public class IGDBAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string? _accessToken = null;
        private DateTime _tokenExpiration = DateTime.MinValue;

        public IGDBAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Obtiene el token de acceso de IGDB. Si ya está en memoria y no ha expirado, lo reutiliza.
        /// </summary>
        public async Task<string> GetAccessToken()
        {
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiration)
            {
                return _accessToken;
            }

            var clientId = _configuration["IGDB:ClientId"];
            var clientSecret = _configuration["IGDB:ClientSecret"];

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new InvalidOperationException("Las credenciales de IGDB no están configuradas correctamente.");
            }

            // Crear el contenido de la solicitud en formato x-www-form-urlencoded
            var requestData = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", "client_credentials" }
            };

            var requestContent = new FormUrlEncodedContent(requestData);

            try
            {
                var response = await _httpClient.PostAsync("https://id.twitch.tv/oauth2/token", requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error obteniendo el token de acceso de IGDB: {response.StatusCode} - {response.ReasonPhrase}");
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<JsonElement>(responseData);

                _accessToken = json.GetProperty("access_token").GetString() ?? throw new Exception("No se recibió un token válido.");
                int expiresIn = json.GetProperty("expires_in").GetInt32();
                _tokenExpiration = DateTime.UtcNow.AddSeconds(expiresIn);

                return _accessToken;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en IGDBAuthService: {ex.Message}", ex);
            }
        }
    }
}
