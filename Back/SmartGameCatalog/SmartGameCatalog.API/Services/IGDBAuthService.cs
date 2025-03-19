using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
        private DateTime _tokenExpiration = DateTime.MinValue; // Guarda el tiempo de expiración del token

        /// <summary>
        /// Constructor del servicio de autenticación de IGDB.
        /// </summary>
        public IGDBAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Obtiene el token de acceso de IGDB. Si ya está en memoria y no ha expirado, lo reutiliza.
        /// </summary>
        /// <returns>Token de acceso válido para realizar llamadas a la API de IGDB.</returns>
        public async Task<string> GetAccessToken()
        {
            // Si ya tenemos un token válido en memoria, lo reutilizamos
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiration)
            {
                return _accessToken;
            }

            // Obtener credenciales desde la configuración
            var clientId = _configuration["IGDB:ClientId"];
            var clientSecret = _configuration["IGDB:ClientSecret"];

            // Validar que las credenciales están configuradas
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new InvalidOperationException("Las credenciales de IGDB no están configuradas correctamente.");
            }

            // Crear la solicitud para obtener el token de acceso
            var requestBody = new StringContent(
                $"client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials",
                Encoding.UTF8, "application/x-www-form-urlencoded"
            );

            try
            {
                var response = await _httpClient.PostAsync("https://id.twitch.tv/oauth2/token", requestBody);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error obteniendo el token de acceso de IGDB: {response.ReasonPhrase}");
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<JsonElement>(responseData);

                // Obtener y almacenar el token y su tiempo de expiración
                _accessToken = json.GetProperty("access_token").GetString() ?? string.Empty;
                int expiresIn = json.GetProperty("expires_in").GetInt32();
                _tokenExpiration = DateTime.UtcNow.AddSeconds(expiresIn);

                return _accessToken;
            }
            catch (Exception ex)
            {
                return $"Error en IGDBAuthService: {ex.Message}";
            }
        }
    }
}
