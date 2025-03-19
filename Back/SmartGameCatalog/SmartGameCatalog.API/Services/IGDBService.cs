using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartGameCatalog.API.Services
{
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

        public async Task<string> GetGameById(int gameId)
        {
            var token = await _authService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Client-ID", _configuration["IGDB:ClientId"]);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var requestBody = $"fields name, summary, cover.url; where id = {gameId};";
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.igdb.com/v4/games", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error en IGDB: {response.ReasonPhrase}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
